using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Models.Predictions;
using Wheelingful.DAL.Contracts;
using Wheelingful.DAL.DbContexts;
using Wheelingful.DAL.Entities;
using Wheelingful.DAL.Helpers;

namespace Wheelingful.BLL.Services.Books;

public class BookRecommenderService(
    ICurrentUser currentUser,
    ILogger<BookRecommenderService> logger,
    ICacheService cacheService,
    WheelingfulDbContext dbContext) : IBookRecommenderService
{
    public async Task<PredictionEngine<ModelInput, ModelOutput>> BuildPredictionEngine(IEnumerable<ModelInput>? newData = null)
    {
        var prefix = nameof(Review).ToCachePrefix();

        var selected = dbContext.Reviews
            .Select(r => new ModelInput
            {
                UserId = r.UserId,
                BookId = r.BookId,
                ReviewScore = r.Score
            });

        var fetchValue = () => selected.ToListAsync();

        var reviews = await cacheService.GetAndSet(prefix, fetchValue, CacheHelper.DefaultCacheExpiration);

        if (newData != null)
        {
            reviews.InsertRange(0, newData);
        }

        var mlContext = new MLContext();

        var splitData = mlContext.Data.TrainTestSplit(
            data: mlContext.Data.LoadFromEnumerable(reviews), 
            testFraction: 0.2
        );

        IEstimator<ITransformer> estimator = mlContext.Transforms.Conversion
            .MapValueToKey(outputColumnName: "UserIdEncoded", inputColumnName: "UserId")
            .Append(mlContext.Transforms.Conversion
            .MapValueToKey(outputColumnName: "BookIdEncoded", inputColumnName: "BookId"));

        var options = new MatrixFactorizationTrainer.Options
        {
            MatrixColumnIndexColumnName = "UserIdEncoded",
            MatrixRowIndexColumnName = "BookIdEncoded",
            LabelColumnName = "ReviewScore",
            NumberOfIterations = 20,
            ApproximationRank = 100
        };

        var trainerEstimator = estimator
            .Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

        ITransformer model = trainerEstimator.Fit(splitData.TrainSet);

        var prediction = model.Transform(splitData.TestSet);

        var metrics = mlContext.Regression
            .Evaluate(prediction, labelColumnName: "ReviewScore", scoreColumnName: "Score");

        logger.LogInformation("User {UserId} finished model training: {@Metrics}",
            currentUser.Id, metrics);

        return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);
    }
}
