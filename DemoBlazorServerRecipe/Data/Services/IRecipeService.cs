using DemoBlazorServerRecipe.Models;

namespace DemoBlazorServerRecipe.Data.Services
{
    public interface IRecipeService
    {
        // Category
        Task<int> AddOrUpdateCategoryAsync(CategoryModel categoryModel);
        Task<CategoryModel> GetCategoryByIdAsync(int id);
        Task<List<CategoryModel>> GetCategoriesAsync();
        Task<int> DeleteCategoryAsync(int id);

        // Recipe
        Task<int> AddOrUpdateRecipeAsync(RecipeModel recipeModel);
        Task<RecipeModel> GetRecipeByIdAsync(int id);
        Task<List<RecipeModel>> GetRecipeByCategoryIdAsync(int categoryId);
        Task<List<RecipeModel>> GetRecipesAsync();
        Task<int> DeleteRecipeAsync(int id);

        // Procedure
        Task<int> AddOrUpdateStepAsync(StepModels stepModels);
        Task<StepModels> GetStepByIdAsync(int id);
        Task<List<StepModels>> GetStepeByRecipeIdAsync(int recipeId);
        Task<List<StepModels>> GetStepsAsync();
        Task<int> DeleteStepAsync(int id);
    }
}
