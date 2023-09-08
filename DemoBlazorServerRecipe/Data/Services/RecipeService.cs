using AutoMapper;
using DemoBlazorServerRecipe.Models.Entities;
using DemoBlazorServerRecipe.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoBlazorServerRecipe.Data.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;
        public RecipeService(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

        // Category Service
        public async Task<int> AddOrUpdateCategoryAsync(CategoryModel categoryModel)
        {
            if (categoryModel is null)
                return (int)System.Net.HttpStatusCode.BadRequest;

            var category = mapper.Map<Category>(categoryModel);

            if (categoryModel.Id != 0)
            {
                var findCategory = await appDbContext.Categories.FindAsync(categoryModel.Id);
                if (findCategory is null)
                    return (int)System.Net.HttpStatusCode.NotFound;

                if (findCategory.CountryName == categoryModel.CountryName && findCategory.Image == categoryModel.Image)
                    return (int)System.Net.HttpStatusCode.NotImplemented;

                findCategory.CountryName = categoryModel.CountryName;
                findCategory.Image = categoryModel.Image;
                await appDbContext.SaveChangesAsync();
                return (int)System.Net.HttpStatusCode.OK;
            }

            var chk = await appDbContext.Categories.Where(_ => _.CountryName.ToLower().Equals(categoryModel.CountryName.ToLower())).FirstOrDefaultAsync();
            if (chk is not null)
                return (int)System.Net.HttpStatusCode.NotAcceptable;

            appDbContext.Categories.Add(category);
            await appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.Created;
        }

        public async Task<int> DeleteCategoryAsync(int id)
        {
            Category category = await appDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category is null)
                return (int)System.Net.HttpStatusCode.NotFound;

            appDbContext.Categories.Remove(category);
            await appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.OK;
        }

        public async Task<List<CategoryModel>> GetCategoriesAsync()
        {
            var categories = await appDbContext.Categories.ToListAsync();
            if (categories is null)
                return null!;

            var categoryModelList = categories.Select(mapper.Map<CategoryModel>);
            return categoryModelList.ToList();
        }

        public async Task<CategoryModel> GetCategoryByIdAsync(int id)
        {
            Category category = await appDbContext.Categories.FirstOrDefaultAsync(_ => _.Id == id);
            if (category is null) return null!;

            var categoryModel = mapper.Map<CategoryModel>(category);
            return categoryModel;
        }


        // Recipe Service
        public async Task<int> AddOrUpdateRecipeAsync(RecipeModel recipeModel)
        {
            if (recipeModel is null)
                return (int)System.Net.HttpStatusCode.BadRequest;

            var recipe = mapper.Map<Recipe>(recipeModel);

            if (recipeModel.Id != 0)
            {
                var findRecipe = await appDbContext.Recipes.FindAsync(recipeModel.Id);
                if (findRecipe is null)
                    return (int)System.Net.HttpStatusCode.NotFound;

                findRecipe.RecipeName = recipeModel.RecipeName;
                findRecipe.Rank = recipeModel.Rank;
                findRecipe.GeneralTimeNeeded = recipeModel.GeneralTimeNeeded;
                findRecipe.CategoryId = recipeModel.CategoryId;
                findRecipe.Description = recipeModel.Description;
                findRecipe.GeneralImage = recipeModel.GeneralImage;

                await appDbContext.SaveChangesAsync();
                return (int)System.Net.HttpStatusCode.OK;
            }

            var chk = await appDbContext.Recipes.Where(_ => _.RecipeName.ToLower().Equals(recipeModel.RecipeName.ToLower())).FirstOrDefaultAsync();
            if (chk is not null)
                return (int)System.Net.HttpStatusCode.NotAcceptable;

            appDbContext.Recipes.Add(recipe);
            await appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.Created;
        }

        public async Task<int> DeleteRecipeAsync(int id)
        {
            Recipe recipe = await appDbContext.Recipes.FirstOrDefaultAsync(c => c.Id == id);
            if (recipe is null)
                return (int)System.Net.HttpStatusCode.NotFound;

            appDbContext.Recipes.Remove(recipe);
            await appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.OK;
        }

        public async Task<List<RecipeModel>> GetRecipesAsync()
        {
            var recipes = await appDbContext.Recipes.ToListAsync();
            if (recipes is null)
                return null!;

            var recipeModelList = recipes.Select(mapper.Map<RecipeModel>);
            return recipeModelList.ToList();
        }

        public async Task<RecipeModel> GetRecipeByIdAsync(int id)
        {
            Recipe recipe = await appDbContext.Recipes.Include(_ => _.Category).Include(_ => _.Procedures).FirstOrDefaultAsync(_ => _.Id == id);
            if (recipe is null) return null!;

            var recipeModel = mapper.Map<RecipeModel>(recipe);
            return recipeModel;
        }

        public async Task<List<RecipeModel>> GetRecipeByCategoryIdAsync(int categoryId)
        {
            var results = await appDbContext.Recipes.Where(_ => _.CategoryId == categoryId).Include(_ => _.Category).ToListAsync();
            var list = results.Select(mapper.Map<RecipeModel>);
            return list.ToList();
        }


        // Step Service
        public async Task<int> AddOrUpdateStepAsync(StepModels stepModel)
        {
            if (stepModel is null)
                return (int)System.Net.HttpStatusCode.BadRequest;

            var step = mapper.Map<Step>(stepModel);

            if (stepModel.Id != 0)
            {
                var findstep = await appDbContext.Procedures.FindAsync(stepModel.Id);
                if (findstep is null)
                    return (int)System.Net.HttpStatusCode.NotFound;

                findstep.ProcedureNo = stepModel.ProcedureNo;
                findstep.Description = stepModel.Description;
                findstep.Title = stepModel.Title;
                findstep.TimeNeeded = stepModel.TimeNeeded;
                findstep.RecipeId = stepModel.RecipeId;

                await appDbContext.SaveChangesAsync();
                return (int)System.Net.HttpStatusCode.OK;
            }

            var chk = await appDbContext.Procedures.Where(_ => _.Title.ToLower().Equals(stepModel.Title.ToLower())).FirstOrDefaultAsync();
            if (chk is not null)
                return (int)System.Net.HttpStatusCode.NotAcceptable;

            appDbContext.Procedures.Add(step);
            await appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.Created;
        }
        public async Task<int> DeleteStepAsync(int id)
        {
            Step step = await appDbContext.Procedures.FirstOrDefaultAsync(c => c.Id == id);
            if (step is null)
                return (int)System.Net.HttpStatusCode.NotFound;

            appDbContext.Procedures.Remove(step);
            await appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.OK;
        }

        public async Task<List<StepModels>> GetStepsAsync()
        {
            var steps = await appDbContext.Procedures.Include(_ => _.Recipe).ToListAsync();
            if (steps is null)
                return null!;

            var stepModelList = steps.Select(mapper.Map<StepModels>);
            return stepModelList.ToList();
        }

        public async Task<StepModels> GetStepByIdAsync(int id)
        {
            Step step = await appDbContext.Procedures.FirstOrDefaultAsync(_ => _.Id == id);
            if (step is null) return null!;

            var stepModel = mapper.Map<StepModels>(step);
            return stepModel;
        }

        public async Task<List<StepModels>> GetStepeByRecipeIdAsync(int recipeId)
        {
            var results = await appDbContext.Procedures.Where(_ => _.RecipeId == recipeId).ToListAsync();
            var list = results.Select(mapper.Map<StepModels>);
            return list.ToList();
        }
    }
}
