using AutoMapper;
using DemoBlazorServerRecipe.Models.Entities;
using DemoBlazorServerRecipe.Models;

namespace DemoBlazorServerRecipe.Data.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //from database - user
            CreateMap<Category, CategoryModel>();
            CreateMap<Recipe, RecipeModel>();
            CreateMap<Step, StepModels>();

            //from user - database
            CreateMap<CategoryModel, Category>();
            CreateMap<RecipeModel, Recipe>();
            CreateMap<StepModels, Step>();


        }
    }
}
