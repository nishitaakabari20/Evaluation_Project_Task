﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Recipe.Common;
using Recipe.Model.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Recipe.Common.Constants;
using Test_api.Recipe.Model.MyRecipeModel;
using Microsoft.EntityFrameworkCore;
using Recipe.Model.RecipeSPContext;
using User.Model.SpDbContext;

namespace Recipe.Service.RepositoryFactory
{
    public static class SpRepository
    {

        public static SearchPage<T> BindSearchList<T>(Dictionary<string, object> parameters, List<T> records)
        {
            SearchPage<T> result = new SearchPage<T>
            {
                List = records
            };


            int from = 0, size = 10;
            if (parameters != null)
            {
                if (parameters.ContainsKey(SearchParameters.PageSize) && parameters.ContainsKey(SearchParameters.PageStart))
                {
                    size = Convert.ToInt32(parameters[SearchParameters.PageSize]);
                    from = (Convert.ToInt32(parameters[SearchParameters.PageStart]) - 1) * size;
                    result.Meta.PageSize = size;
                    result.Meta.Page = Convert.ToInt32(parameters[SearchParameters.PageStart]);
                }
                else
                {
                    result.Meta.PageSize = size;
                    result.Meta.Page = from + 1;
                }
            }
            else
            {
                result.Meta.PageSize = size;
                result.Meta.Page = from + 1;
            }

            return result;
        }


        public static async Task ExecuteStoreProcedureQuery(this RecipeDB catalogDbContext, string sqlQuery, object[] param)
        {
            var saveResult = await catalogDbContext.Set<ExecutreStoreProcedureResult>().FromSqlRaw(sqlQuery, param).ToListAsync();

            if (saveResult != null && saveResult.Count > 0)
            {
                var errorResult = saveResult.FirstOrDefault();
                if (!string.IsNullOrEmpty(errorResult.ErrorMessage))
                {
                    throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, errorResult.ErrorMessage);
                }
            }
        }
        public static async Task<string> ExecuteStoreProcedureQueryWithSID(this RecipeDB catalogDbContext, string sqlQuery, object[] param)
        {
            var saveResult = await catalogDbContext.Set<ExecutreStoreProcedureResultWithSID>().FromSqlRaw(sqlQuery, param).ToListAsync();

            if (saveResult != null && saveResult.Count > 0)
            {
                var result = saveResult.FirstOrDefault();
                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, result.ErrorMessage);
                }
                return result.SID;
            }
            return null;
        }

        public static async Task<string> ExecuteStoreProcedure(this RecipeDB catalogDbContext, string sqlQuery, object[] param)
        {
            var response = await catalogDbContext.Set<ExecutreStoreProcedureResult>().FromSqlRaw(sqlQuery, param).ToListAsync(); ;

            if (response == null || response.Count <= 0) return string.Empty;

            var result = response.FirstOrDefault();

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, result.ErrorMessage);
            }
            return string.IsNullOrEmpty(result.Result) ? string.Empty : result.Result;

        }

        public static async Task<string> ExecuteStoreProcedureWithEntitySID(this RecipeDB catalogDbContext, string sqlQuery, object[] param)
        {
            var response = await catalogDbContext.Set<ExecutreStoreProcedureResultWithEntitySID>().FromSqlRaw(sqlQuery, param).ToListAsync();

            if (response == null || response.Count <= 0) return string.Empty;

            var result = response.FirstOrDefault();

            if (result == null) return string.Empty;

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, result.ErrorMessage);
            }
            return string.IsNullOrEmpty(result.Result) ? string.Empty : result.Result;

        }

        public static async Task<ExecutreStoreProcedureResult?> ExecutreStoreProcedureResult<T>(
        this RecipeSpContext spContext,
        string sqlQuery,
        params object[] param) where T : class
        {
            var response = await spContext.Set<ExecutreStoreProcedureResult>()
                                          .FromSqlRaw(sqlQuery, param)
                                          .ToListAsync();

            if (response == null || response.Count == 0)
                return null;

            var result = response.FirstOrDefault();
            if (result == null)
                return null;

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                throw new HttpStatusCodeException(
                    StatusCodes.Status500InternalServerError,
                    result.ErrorMessage);
            }

            if (string.IsNullOrWhiteSpace(result.Result))
                return null;
            ExecutreStoreProcedureResult data = new ExecutreStoreProcedureResult();
            data.Result = result.Result;

            // Deserialize directly into your typed model
            return data;
        }
        public static async Task<Page> ExecutreStoreProcedureResultList(this RecipeSpContext catalogDbContext, string sqlQuery, object[] param)
        {
            Page page = new Page();
            var response = await catalogDbContext.Set<ExecutreStoreProcedureResultList>().FromSqlRaw(sqlQuery, param).ToListAsync();
            if (response != null && response.Count > 0)
            {
                var result = response.FirstOrDefault();

                if (result == null) return page;

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, result.ErrorMessage);
                }

                page.Meta.TotalResults = result.TotalCount??0;
                if (!string.IsNullOrWhiteSpace(result.Result))
                {
                    var list = JsonConvert.DeserializeObject(result.Result);
                    page.Result = list;
                    return page;
                }
            }
            return page;
        }
    }
}
