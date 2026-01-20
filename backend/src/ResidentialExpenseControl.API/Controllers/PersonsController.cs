using Microsoft.AspNetCore.Mvc;
using ResidentialExpenseControl.Application.DTOs.Person;
using ResidentialExpenseControl.Application.UseCases.Person.Interfaces;

namespace ResidentialExpenseControl.API.Controllers
{
    // Controller responsável por receber requisições HTTP e delegar para Use Cases
    
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly ICreatePersonUseCase _createPersonUseCase;
        private readonly IGetAllPersonsUseCase _getAllPersonsUseCase;
        private readonly IDeletePersonUseCase _deletePersonUseCase;

        // Injeção de dependência dos Use Cases
    
        public PersonsController(
            ICreatePersonUseCase createPersonUseCase,
            IGetAllPersonsUseCase getAllPersonsUseCase,
            IDeletePersonUseCase deletePersonUseCase)
        {
            _createPersonUseCase = createPersonUseCase;
            _getAllPersonsUseCase = getAllPersonsUseCase;
            _deletePersonUseCase = deletePersonUseCase;
        }

        // POST api/persons
        // Cria uma nova pessoa e retorna 201 Created com a URL do recurso
        [HttpPost]
        public async Task<ActionResult<PersonResponse>> Create([FromBody] CreatePersonRequest request)
        {
            var response = await _createPersonUseCase.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetAll), new { id = response.Id }, response);
        }

        // GET api/persons
        // Lista todas as pessoas cadastradas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonResponse>>> GetAll()
        {
            var response = await _getAllPersonsUseCase.ExecuteAsync();
            return Ok(response);
        }

        // DELETE api/persons/{id}
        // Remove uma pessoa pelo ID - retorna 204 No Content se sucesso
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _deletePersonUseCase.ExecuteAsync(id);
            return NoContent();
        }
    }
}