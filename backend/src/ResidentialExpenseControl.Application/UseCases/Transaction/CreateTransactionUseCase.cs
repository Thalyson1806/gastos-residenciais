using ResidentialExpenseControl.Application.DTOs.Transaction;
using ResidentialExpenseControl.Application.UseCases.Transaction.Interfaces;
using ResidentialExpenseControl.Domain.Enums;
using ResidentialExpenseControl.Domain.Exceptions;
using ResidentialExpenseControl.Domain.Interfaces;
using TransactionEntity = ResidentialExpenseControl.Domain.Entities.Transaction;


namespace ResidentialExpenseControl.Application.UseCases.Transaction
{
    // Caso de uso para criação de transação
    // Orquestra a busca das entidades relacionadas e delega validação ao Domain
    public class CreateTransactionUseCase : ICreateTransactionUseCase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CreateTransactionUseCase(
            ITransactionRepository transactionRepository,
            IPersonRepository personRepository,
            ICategoryRepository categoryRepository)
        {
            _transactionRepository = transactionRepository;
            _personRepository = personRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<TransactionResponse> ExecuteAsync(CreateTransactionRequest request)
        {
            // Busca a pessoa - necessária para validar regra de menor de idade
            var person = await _personRepository.GetByIdAsync(request.PersonId);
            if (person is null)
                throw new DomainException("Person not found.");

            // Busca a categoria - necessária para validar se permite o tipo de transação
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category is null)
                throw new DomainException("Category not found.");

            // Converte o tipo - a validação completa acontece no construtor da entidade
            var type = (TransactionType)request.Type;

            // Cria a transação - TODAS as validações de negócio acontecem aqui
            // Se qualquer regra for violada, o Domain lança DomainException
            var transaction = new TransactionEntity(
                request.Value,
                request.Description,
                type,
                person,
                category);

            await _transactionRepository.AddAsync(transaction);
            await _transactionRepository.SaveChangesAsync();

            return new TransactionResponse
            {
                Id = transaction.Id,
                Value = transaction.Value,
                Description = transaction.Description,
                Type = (int)transaction.Type,
                TypeName = transaction.Type.ToString(),
                CreatedAt = transaction.CreatedAt,
                PersonId = transaction.PersonId,
                PersonName = person.Name,
                CategoryId = transaction.CategoryId,
                CategoryDescription = category.Description
            };
        }
    }
}