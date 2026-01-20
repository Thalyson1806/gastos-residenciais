using Microsoft.AspNetCore.Mvc;
using ReisdentialExpenseControl.Application.UseCases.Person.Interfaces;
using ResidentialExpenseControl.Application.DTOs.Person;
using ResidentialExpenseControl.Application.UseCases.Person.Interfaces;
using ResidentialExpenseCOntrol.Application.UseCases.Person.Interfaces;

namespace ResidentialExpenseControl.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly ICreatePersonUseCase _createPersonUseCase;
        private readonly IGetAllPersonsUseCase _getAllPersonsUseCase;
        private readonly IDeletePersonUseCase _deletePersonUseCase;

        // Injeção de dependência dos Use Cases
        // Controller não conhece repositórios, apenas Use Cases
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
        [HttpPost]
        public async Task<ActionResult<PersonResponse>> Create([FromBody] CreatePersonRequest request)
        {
            var response = await _createPersonUseCase.ExecuteAsync(request);

            // 201 Created com a URL do recurso criado
            return CreatedAtAction(nameof(GetAll), new { id = response.Id }, response);
        }

        // GET api/persons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonResponse>>> GetAll()
        {
            var response = await _getAllPersonsUseCase.ExecuteAsync();
            return Ok(response);
        }

        // DELETE api/persons/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _deletePersonUseCase.ExecuteAsync(id);

            // 204 No Content - padrão para DELETE bem-sucedido
            return NoContent();
        }
    }
}