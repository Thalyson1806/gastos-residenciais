using ResidentialExpenseControl.Application.DTOs.Category;

namespace ResidentialExpenseControl.Application.UseCases.Category.Interfaces
{
    public interface IGetAllCategoriesUseCase
    {
        Task<IEnumerable<CategoryResponse>> ExecuteAsync();
    }
}