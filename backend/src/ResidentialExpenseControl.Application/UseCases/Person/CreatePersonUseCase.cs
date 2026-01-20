using ResidentialExpenseControl.Application.DTOs.Person;
using ResidentialExpenseControl.Application.UseCases.Category.Interfaces;
using ResidentialExpenseControl.Application.UseCases.Person.Interfaces;
using ResidentialExpenseControl.Domain.Interfaces;
using PersonEntity = ResidentialExpenseControl.Domain.Entities.Person;

namespace ResidentialExpenseControl.Application.UseCases.Person
{
    // Caso de uso para criação de pessoa
    // Responsável por orquestrar o fluxo, sem conter regras de negócio
    public class CreatePersonUseCase : ICreatePersonUseCase
    {
        private readonly IPersonRepository _personRepository;

        // Injeção de dependência via construtor
        // O Use Case depende da INTERFACE, não da implementação concreta
        public CreatePersonUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<PersonResponse> ExecuteAsync(CreatePersonRequest request)
        {
            // Cria a entidade - as validações de domínio acontecem no construtor
            // Se Name ou Age forem inválidos, o Domain lança DomainException
            var person = new PersonEntity(request.Name, request.Age);

            await _personRepository.AddAsync(person);
            await _personRepository.SaveChangesAsync();

            // Retorna DTO, nunca a entidade diretamente
            // Isso protege o domínio e permite controlar o que é exposto
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