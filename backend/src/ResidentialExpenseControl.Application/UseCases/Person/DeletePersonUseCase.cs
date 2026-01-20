using ResidentialExpenseControl.Application.UseCases.Person.Interfaces;
using ResidentialExpenseControl.Domain.Exceptions;
using ResidentialExpenseControl.Domain.Interfaces;

namespace ResidentialExpenseControl.Application.UseCases.Person
{
    public class DeletePersonUseCase : IDeletePersonUseCase
    {
        private readonly IPersonRepository _personRepository;

        public DeletePersonUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task ExecuteAsync(Guid id)
        {
            var person = await _personRepository.GetByIdAsync(id);

            if (person is null)
                throw new DomainException("Person not found.");

          

            await _personRepository.DeleteAsync(person);
            await _personRepository.SaveChangesAsync();
        }
    }
}