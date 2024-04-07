using Microsoft.ML;
using Microsoft.ML.Trainers;
using Wheelingful.ML.Constants;
using Wheelingful.ML.Models.ML;
using Wheelingful.ML.Services.Db;

namespace Wheelingful.ML.Services.ML;

public class TeachModelService
{
    public void StartLearning()
    {
        MLContext mlContext = new MLContext();

        (IDataView trainingDataView, IDataView testDataView) = LoadData(mlContext);

        ITransformer model = BuildAndTrainModel(mlContext, trainingDataView);

        EvaluateModel(mlContext, testDataView, model);

        UseModelForSinglePrediction(mlContext, model);

        SaveModel(mlContext, trainingDataView.Schema, model);
    }

    public static (IDataView training, IDataView test) LoadData(MLContext mlContext)
    {
        using var dbContext = new WheelingfulContext();

        var reviews = dbContext.Reviews.Select(r => new BookRating
        {
          BookId = r.BookId,
          UserId = r.UserId,
          ReviewScore = r.Score,
        }).ToList();

        var splitData = mlContext.Data.TrainTestSplit(mlContext.Data.LoadFromEnumerable(reviews), testFraction: 0.2);

        var r = splitData.TrainSet.Preview();
        var u = splitData.TestSet.Preview();

        return (splitData.TrainSet, splitData.TestSet);
    }

    public static ITransformer BuildAndTrainModel(MLContext mlContext, IDataView trainingDataView)
    {
        IEstimator<ITransformer> estimator = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "UserIdEncoded", inputColumnName: "UserId")
            .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "BookIdEncoded", inputColumnName: "BookId"));
        
        var options = new MatrixFactorizationTrainer.Options
        {
            MatrixColumnIndexColumnName = "UserIdEncoded",
            MatrixRowIndexColumnName = "BookIdEncoded",
            LabelColumnName = "ReviewScore",
            NumberOfIterations = 10,
            ApproximationRank = 100
        };

        var trainerEstimator = estimator.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

        Console.WriteLine("=============== Training the model ===============");
        ITransformer model = trainerEstimator.Fit(trainingDataView);

        return model;
    }

    public static void EvaluateModel(MLContext mlContext, IDataView testDataView, ITransformer model)
    {
        Console.WriteLine("=============== Evaluating the model ===============");
        var prediction = model.Transform(testDataView);

        var metrics = mlContext.Regression.Evaluate(prediction, labelColumnName: "ReviewScore", scoreColumnName: "Score");

        Console.WriteLine("Root Mean Squared Error : " + metrics.RootMeanSquaredError.ToString());
        Console.WriteLine("RSquared: " + metrics.RSquared.ToString());
    }

    public static void UseModelForSinglePrediction(MLContext mlContext, ITransformer model)
    {
        Console.WriteLine("=============== Making a prediction ===============");
        var predictionEngine = mlContext.Model.CreatePredictionEngine<BookRating, BookRatingPrediction>(model);

        var testInput = new BookRating { UserId = "6e094ee3-7d99-41d0-b285-6d9e274f18a2", BookId = 10 };

        var movieRatingPrediction = predictionEngine.Predict(testInput);

        Console.WriteLine($"The predicted score is: {movieRatingPrediction.Score}");

        if (Math.Round(movieRatingPrediction.Score, 1) > 3.5)
        {
            Console.WriteLine($"Therefore, the book {testInput.BookId} is recommended for user {testInput.UserId}");
        }
        else
        {
            Console.WriteLine($"Therefore, the book {testInput.BookId} is not recommended for user {testInput.UserId}");
        }
    }

    public static void SaveModel(MLContext mlContext, DataViewSchema trainingDataViewSchema, ITransformer model)
    {
        var modelPath = MLConstants.ZipWithSavedModel;

        Console.WriteLine("=============== Saving the model to a file ===============");
        mlContext.Model.Save(model, trainingDataViewSchema, modelPath);
    }
}
