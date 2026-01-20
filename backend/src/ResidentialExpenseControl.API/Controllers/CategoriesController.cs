using Microsoft.AspNetCore.Mvc;
using ResidentialExpenseControl.Application.DTOs.Category;
using ResidentialExpenseControl.Application.UseCases.Category.Interfaces;

namespace ResidentialExpenseControl.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICreateCategoryUseCase _createCategoryUseCase;
        private readonly IGetAllCategoriesUseCase _getAllCategoriesUseCase;

        public CategoriesController(
            ICreateCategoryUseCase createCategoryUseCase,
            IGetAllCategoriesUseCase getAllCategoriesUseCase)
        {
            _createCategoryUseCase = createCategoryUseCase;
            _getAllCategoriesUseCase = getAllCategoriesUseCase;
        }

        // POST api/categories
        [HttpPost]
        public async Task<ActionResult<CategoryResponse>> Create([FromBody] CreateCategoryRequest request)
        {
            var response = await _createCategoryUseCase.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetAll), new { id = response.Id }, response);
        }

        // GET api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetAll()
        {
            var response = await _getAllCategoriesUseCase.ExecuteAsync();
            return Ok(response);
        }
    }
}