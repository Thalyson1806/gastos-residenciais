namespace ResidentialExpenseControl.Application.DTOs.Category
{
    public class CreateCategoryRequest
    {
        public string Description { get; set; } = string.Empty;

        // Recebe como int para facilitar integração com frontend
        // 1 = Expense, 2 = Income, 3 = Both

        public int Purpose { get; set; }
    }
}