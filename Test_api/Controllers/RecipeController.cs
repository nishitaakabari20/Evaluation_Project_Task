using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Recipe.Common;
using Recipe.Model.CommonModel;
using Recipe.Model.RequestModel;
using Recipe.Model.ResponseModel;
using Recipe.Service.Repository.Interface;
using Serilog;

namespace Test_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : BaseController
    {
        private readonly IRecipeService _recipeService;
        private readonly ILogger<RecipesController> _logger;

        public RecipesController(IRecipeService recipeService, ILogger<RecipesController> logger)
        {
            _recipeService = recipeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] SearchRequestModel model)
        {
            try
            {
                _logger.LogInformation("Fetching recipes with parameters: {@Model}", model);

                var parameters = FillParamesFromModel(model);
                var list = await _recipeService.List(parameters);

                if (list != null)
                {
                    var result = JsonConvert.DeserializeObject<List<RecipeResponseModel>>(list.Result?.ToString() ?? throw new HttpStatusCodeException(400,"Not data found"))
                                 ?? new List<RecipeResponseModel>();

                    if (result.Count == 0)
                    {
                        _logger.LogWarning("No recipes found with parameters: {@Model}", model);
                        return NotFound(new { message = "No data found" });
                    }

                    _logger.LogInformation("Fetched {Count} recipes", result.Count);
                    list.Result = result;
                    return Ok(BindSearchResult(list, model, "Bind"));
                   // return Ok(result);
                }   

                _logger.LogWarning("No recipes found with parameters: {@Model}", model);
                return NoContent();
            }
            catch (HttpStatusCodeException ex)
            {
                _logger.LogError(ex, "HttpStatusCodeException while fetching recipes");
                return StatusCode(ex.StatusCode, new { ex.Message,ex.Data });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while fetching recipes");
                throw new HttpStatusCodeException(500, ex);
            }
        }

        [HttpPost("{recipeSID?}")]
        public async Task<IActionResult> CreateRecipe([FromRoute] string? recipeSID, [FromBody] RecipeRequestModel model)
        {
            if (model == null)
                throw new HttpStatusCodeException(400, "Recipe data is required.");

            try
            {
                if (string.IsNullOrEmpty(recipeSID) || recipeSID == "null")
                {
                    _logger.LogInformation("Creating recipe: {@Model}", model);

                    var createdRecipe = await _recipeService.CreateAsync(model);

                    _logger.LogInformation("Recipe created successfully with SID: {SID}", createdRecipe?.RecipeSID);
                    return Ok(createdRecipe);
                }
                else
                {
                    _logger.LogInformation("Updating recipe with SID: {SID}, Data: {@Model}", recipeSID, model);

                    var updated = await _recipeService.UpdateAsync(recipeSID, model);
                    if (!updated)
                        throw new HttpStatusCodeException(400, $"Recipe with SID '{recipeSID}' not found or already deleted.");

                    _logger.LogInformation("Recipe with SID {SID} updated successfully",recipeSID);
                    return Ok(new { Updated = updated, SID = recipeSID });
                }
            }
            catch (HttpStatusCodeException ex)
            {
                _logger.LogError(ex, "HttpStatusCodeException in Create/Update recipe");
                return StatusCode(ex.StatusCode, new { ex.Message, ex.Code, ex.Data });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in Create/Update recipe");
                throw new HttpStatusCodeException(500, ex);
            }
        }

        [HttpGet("{recipeSID}")]
        public async Task<IActionResult> GetBySID(string recipeSID)
        {
            try
            {
                _logger.LogInformation("Fetching recipe by SID: {SID}", recipeSID);

                var recipe = await _recipeService.GetByRecipeSID(recipeSID);
                if (recipe == null)
                    throw new HttpStatusCodeException(400, $"Recipe with SID '{recipeSID}' not found or has been deleted.");

                _logger.LogInformation("Recipe with SID {SID} fetched successfully", recipeSID);
                return Ok(recipe);
            }
            catch (HttpStatusCodeException ex)
            {
                _logger.LogError(ex, "HttpStatusCodeException in GetBySID");
                return StatusCode(ex.StatusCode, new { ex.Message, ex.Data });
            }
            //catch (HttpStatusCodeException ex)
            //{
            //    _logger.LogError(ex, "Unexpected error in GetBySID");
            //    throw new HttpStatusCodeException(500, ex);
            //}
        }

        [HttpDelete("{recipeSID}")]
        public async Task<IActionResult> Delete([FromRoute] string recipeSID)
        {
            try
            {
                _logger.LogInformation("Deleting recipe with SID: {SID}", recipeSID);

                var deleted = await _recipeService.DeleteAsync(recipeSID);
                if (!deleted)
                    throw new HttpStatusCodeException(400, $"Recipe with SID '{recipeSID}' not found or already deleted.");

                _logger.LogInformation("Recipe with SID {SID} deleted successfully", recipeSID);
                return Ok(new { Deleted = true, SID = recipeSID });
            }
            catch (HttpStatusCodeException ex)
            {
                _logger.LogError(ex, "HttpStatusCodeException in Delete");
                return StatusCode(ex.StatusCode, new { ex.Message, ex.Data });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in Delete");
                throw new HttpStatusCodeException(500, ex);
            }
        }
    }
}
