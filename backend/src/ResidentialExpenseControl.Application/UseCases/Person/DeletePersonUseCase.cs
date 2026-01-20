using ResidentialExpenseControl.Application.UseCases.Category.Interfaces;
using ResidentialExpenseControl.Application.UseCases.Person.Interfaces;
using ResidentialExpenseControl.Domain.Exceptions;
using ResidentialExpenseControl.Domain.Interfaces;

namespace ResidentialExpenseControl.Application.UseCases.Person
{
    // Caso de uso para exclusão de pessoa
  
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

            // Verifica se a pessoa existe antes de tentar deletar
            // Lança exceção de domínio que será convertida em 400 Bad Request
            if (person is null)
                throw new DomainException("Person not found.");

          
            // A regra de negócio diz que ao deletar pessoa, suas transações também são removidas

            await _personRepository.DeleteAsync(person);
            await _personRepository.SaveChangesAsync();
        }
    }
}