using ResidentialExpenseControl.Application.UseCases.Person.Interfaces;
using ResidentialExpenseControl.Domain.Exceptions;
using ResidentialExpenseControl.Domain.Interfaces;

namespace ResidentialExpenseControl.Application.UseCases.Person
{
    // Caso de uso para exclusão de pessoa
    // Implementa a regra de exclusão em cascata das transações
    public class DeletePersonUseCase : IDeletePersonUseCase
    {
        private readonly IPersonRepository _personRepository;
        private readonly ITransactionRepository _transactionRepository;

      
        // Necessário para deletar transações antes de deletar a pessoa
        public DeletePersonUseCase(
            IPersonRepository personRepository,
            ITransactionRepository transactionRepository)
        {
            _personRepository = personRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task ExecuteAsync(Guid id)
        {
            var person = await _personRepository.GetByIdAsync(id);

            if (person is null)
                throw new DomainException("Person not found.");

            // REGRA DE NEGÓCIO: Ao deletar pessoa, suas transações são removidas
          
            await _transactionRepository.DeleteByPersonIdAsync(id);

            await _personRepository.DeleteAsync(person);
            await _personRepository.SaveChangesAsync();
        }
    }
}