using ResidentialExpenseControl.Application.DTOs.Person;
using ResidentialExpenseControl.Application.UseCases.Person.Interfaces;
using ResidentialExpenseControl.Domain.Interfaces;
using ResidentialExpenseCOntrol.Application.UseCases.Person.Interfaces;

namespace ResidentialExpenseControl.Application.UseCases.Person
{
    public class GetAllPersonsUseCase : IGetAllPersonsUseCase
    {
        private readonly IPersonRepository _personRepository;

        public GetAllPersonsUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<PersonResponse>> ExecuteAsync()
        {
            var persons = await _personRepository.GetAllAsync();

            // Mapeia entidades para DTOs
          
            return persons.Select(p => new PersonResponse
            {
                Id = p.Id,
                Name = p.Name,
                Age = p.Age,
                IsMinor = p.IsMinor()
            });
        }
    }
}