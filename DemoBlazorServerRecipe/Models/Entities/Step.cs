namespace DemoBlazorServerRecipe.Models.Entities
{
    public class Step
    {
        public int Id { get; set; }
        public int ProcedureNo { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TimeOnly TimeNeeded { get; set; }
        public Recipe? Recipe { get; set; }
        public int RecipeId { get; set; }
    }
}
