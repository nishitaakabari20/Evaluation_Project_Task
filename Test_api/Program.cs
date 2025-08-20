using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Recipe.Model.CommonModel;
using Recipe.Model.RecipeSPContext;
using Recipe.Service.Repository.Implementation;
using Recipe.Service.Repository.Interface;
using Recipe.Service.UnitOFWork;
using Serilog;
using Test_api.HelperFolder;
using Test_api.Recipe.Model.MyRecipeModel;

namespace Test_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug() 
                .WriteTo.Console()    
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) 
                .CreateLogger();

            builder.Host.UseSerilog();
            // Register DbContext for main DB
            builder.Services.AddDbContext<RecipeDB>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            
            builder.Services.AddDbContext<RecipeSpContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddAutoMapper(typeof(RecipeAutoMapper));

            builder.Services.AddUnitOfWork<RecipeDB>();
            builder.Services.AddScoped<IRecipeService, RecipeService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            var dbConnectionString = builder.Configuration["DefaultConnection"];
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var ex = exceptionHandlerPathFeature?.Error;

                    context.Response.StatusCode = ex switch
                    {
                        KeyNotFoundException => StatusCodes.Status404NotFound,
                        ArgumentException => StatusCodes.Status400BadRequest,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                    {
                        Message = ex?.Message
                    }));
                });
            });


            // Configure middleware pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
