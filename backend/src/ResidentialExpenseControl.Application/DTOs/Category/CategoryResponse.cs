namespace ResidentialExpenseControl.Application.DTOs.Category
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Purpose { get; set; }

        // Nome amigável do Purpose para exibição no front
        public string PurposeName { get; set; } = string.Empty;
    }
}