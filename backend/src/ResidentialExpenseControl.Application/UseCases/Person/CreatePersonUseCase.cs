using ReisdentialExpenseControl.Application.UseCases.Person.Interfaces;
using ResidentialExpenseControl.Application.DTOs.Person;
using ResidentialExpenseControl.Application.UseCases.Person.Interfaces;
using ResidentialExpenseControl.Domain.Interfaces;
using PersonEntity = ResidentialExpenseControl.Domain.Entities.Person;


namespace ResidentialExpenseControl.Application.UseCases.Person
{
    public class CreatePersonUseCase : ICreatePersonUseCase
    {
        private readonly IPersonRepository _personRepository;


        // Injeção de dependência via construtor

        public CreatePersonUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<PersonResponse> ExecuteAsync(CreatePersonRequest request)
        {
            // Cria a entidade 
            // Se Name ou Age forem inválidos, o Domain lança exceção
            var person = new PersonEntity(request.Name, request.Age);

            await _personRepository.AddAsync(person);
            await _personRepository.SaveChangesAsync();

            // Retorna DTO, nunca a entidade diretamente
            return new PersonResponse
            {
                Id = person.Id,
                Name = person.Name,
                Age = person.Age,
                IsMinor = person.IsMinor()
            };
        }
    }
}