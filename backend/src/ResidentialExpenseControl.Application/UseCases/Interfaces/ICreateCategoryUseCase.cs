using ResidentialExpenseControl.Application.DTOs.Category;


namespace ResidentialExpenseControl.Application.UseCases.Category.Interfaces
{
    public interface ICreateCategoryUseCase
    {
        Task<CategoryResponse> ExecuteAsync(CreateCategoryRequest request);

    }
}