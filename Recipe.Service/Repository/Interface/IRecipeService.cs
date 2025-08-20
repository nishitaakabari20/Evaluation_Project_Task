using Recipe.Model.CommonModel;
using Recipe.Model.RequestModel;
using Recipe.Model.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.Service.Repository.Interface
{
    public interface IRecipeService
    {
        Task<Page> List(Dictionary<string, object> parameters);
        Task<RecipeResponseModel?> GetByRecipeSID(string recipeSID);
        Task<RecipeResponseModel> CreateAsync(RecipeRequestModel model);
        Task<bool> UpdateAsync(string recipeSID, RecipeRequestModel model);
        Task<bool> DeleteAsync(string recipeSID);
    }
}
