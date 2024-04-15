using Microsoft.ML;
using Wheelingful.BLL.Models.Predictions;

namespace Wheelingful.BLL.Contracts.Books;

public interface IBookRecommenderService
{
    Task<PredictionEngine<ModelInput, ModelOutput>> BuildPredictionEngine(IEnumerable<ModelInput>? newData = null);
}
