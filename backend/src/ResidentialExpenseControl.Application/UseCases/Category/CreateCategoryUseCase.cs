using ResidentialExpenseControl.Application.DTOs.Category;
using ResidentialExpenseControl.Application.UseCases.Category.Interfaces;
using ResidentialExpenseControl.Domain.Enums;
using ResidentialExpenseControl.Domain.Interfaces;
using CategoryEntity = ResidentialExpenseControl.Domain.Entities.Category;

namespace ResidentialExpenseControl.Application.UseCases.Category
{
    public class CreateCategoryUseCase : ICreateCategoryUseCase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryUseCase(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponse> ExecuteAsync(CreateCategoryRequest request)
        {
            // Converte int para enum - a validação acontece no Domain
            var purpose = (Purpose)request.Purpose;

            var category = new CategoryEntity(request.Description, purpose);

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();

            return new CategoryResponse
            {
                Id = category.Id,
                Description = category.Description,
                Purpose = (int)category.Purpose,
                PurposeName = category.Purpose.ToString()
            };
        }
    }
}