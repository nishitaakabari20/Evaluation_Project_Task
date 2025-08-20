using Recipe.Common;
using Recipe.Model.RecipeSPContext;
using Recipe.Service.Repository.Interface;
using Recipe.Service.UnitOFWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recipe.Model.CommonModel;
using Test_api.Recipe.Model.MyRecipeModel;
using Recipe.Service.RepositoryFactory;
using static Recipe.Common.enums;
using Recipe.Model.ResponseModel;
using Rec = Test_api.Recipe.Model.MyRecipeModel.Recipe;
using Recipe.Model.RequestModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AutoMapper;
using Recipe.Common;
using User.Model.SpDbContext;

namespace Recipe.Service.Repository.Implementation
{
    public class RecipeService : IRecipeService
    {
        private readonly IMapper _mapper;

        
        private readonly RecipeDB _context;
        private readonly RecipeSpContext _spContext;
        private readonly IUnitOfWork _unitOfWork;

        public RecipeService(IMapper mapper  ,RecipeDB context, RecipeSpContext spContext, IUnitOfWork unitOfWork)
        {
            _context = context;
            _spContext = spContext;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Page> List(Dictionary<string, object> parameters)
        {
            var xmlPara = CommonHelper.DictionaryToXml(parameters, "Search");
            string query = "[dbo].[sp_get_all_recipes] {0}";
            object[] param = { xmlPara };
            var result = await _spContext.ExecutreStoreProcedureResultList(query, param);
            return result;
        }
        //using automapper

        //public async Task<RecipeResponseModel> CreateAsync(RecipeRequestModel model)
        //{
        //    try
        //    {
        //        var recipeXmlModel = _mapper.Map<RecipeAutoMapper>(model);

        //        string xmlInput = CommonHelper.ObjectToXml(recipeXmlModel, "Recipe");

        //        string query = "[dbo].[sp_add_recipe] {0}";
        //        object[] param = { xmlInput };
        //        await _spContext.ExecutreStoreProcedureResultList(query, param);

        //        return _mapper.Map<RecipeResponseModel>(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Error creating recipe: {ex.Message}");
        //    }
        //}


        public async Task<RecipeResponseModel> CreateAsync(RecipeRequestModel model)
        {
            try
            {
                var dict = new Dictionary<string, object>
        {
            { "Title", model.Title },
            { "Ingredients", model.Ingredients },
            { "Instructions", model.Instructions },
            { "Category", model.Category }
        };

                string xmlInput = CommonHelper.DictionaryToXml(dict, "Recipe");

                string query = "[dbo].[sp_add_recipe] {0}";
                object[] param = { xmlInput };
                await _spContext.ExecutreStoreProcedureResultList(query, param);

                return new RecipeResponseModel
                {
                    RecipeSID="Rec"+Guid.NewGuid(),
                    Title = model.Title,
                    Ingredients = model.Ingredients,
                    Instructions = model.Instructions,
                    Category = model.Category,
                    Status=(int)StatusTypeDB.Active,
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,

                   
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating recipe: {ex.Message}");
            }
        }




        public async Task<RecipeResponseModel?> GetByRecipeSID(string recipeSID)
        {
            string query = "[dbo].[sp_get_recipe_by_sid] {0}";
            object[] param = { recipeSID };
            var recipe = await _spContext.ExecutreStoreProcedureResult<ExecutreStoreProcedureResult>(query, param);

            if(recipe==null)  throw new HttpStatusCodeException(400,"Recipe not found");

            if(recipe.ErrorMessage != null) throw new HttpStatusCodeException(404,$"Error fetching recipe: {recipe.ErrorMessage}");

            RecipeResponseModel recipeResponse = new RecipeResponseModel();
            if ( recipe.Result != null)
            {

            recipeResponse = JsonConvert.DeserializeObject<RecipeResponseModel>(recipe.Result);
            }

            

            return recipeResponse;
        }

        public async Task<bool> UpdateAsync(string recipeSID, RecipeRequestModel model)
        {
            var dict = new Dictionary<string, object>
    {
        { "RecipeSID", recipeSID },
        { "Title", model.Title },
        { "Ingredients", model.Ingredients },
        { "Instructions", model.Instructions },
        { "Category", model.Category },
        //{ "Status", model.Status }
    };

            string xmlInput = CommonHelper.DictionaryToXml(dict, "Recipe");
            string query = "[dbo].[sp_update_recipe] {0}";
            object[] param = { xmlInput };

            var page = await _spContext.ExecutreStoreProcedureResultList(query, param);

            int rowsAffected = page.Meta.TotalResults;

            return rowsAffected > 0;
        }
        

        public async Task<bool> DeleteAsync(string recipeSID)
        {
            string query = "[dbo].[sp_delete_recipe] {0}";
            object[] param = { recipeSID };

            var page = await _spContext.ExecutreStoreProcedureResultList(query, param);

           
            int rowsAffected = page.Meta.TotalResults;

            return rowsAffected > 0;
        }

    }
}
