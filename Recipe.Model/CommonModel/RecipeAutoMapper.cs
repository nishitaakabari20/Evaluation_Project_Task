using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Recipe.Model.RequestModel;
using Recipe.Model.ResponseModel;
using Rec = Test_api.Recipe.Model.MyRecipeModel.Recipe;
namespace Recipe.Model.CommonModel
{
   public class RecipeAutoMapper:Profile
    {
        public RecipeAutoMapper() {
            CreateMap<Rec, RecipeResponseModel>();
            CreateMap<RecipeRequestModel, RecipeAutoMapper>();
            CreateMap<RecipeAutoMapper, RecipeResponseModel>();
        }
    }
}
