using ResidentialExpenseControl.Application.DTOs.Category;
using ResidentialExpenseControl.Application.UseCases.Category.Interfaces;
using ResidentialExpenseControl.Domain.Interfaces;

namespace ResidentialExpenseControl.Application.UseCases.Category
{
    public class GetAllCategoriesUseCase : IGetAllCategoriesUseCase
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesUseCase(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryResponse>> ExecuteAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories.Select(c => new CategoryResponse
            {
                Id = c.Id,
                Description = c.Description,
                Purpose = (int)c.Purpose,
                PurposeName = c.Purpose.ToString()
            });
        }
    }
}