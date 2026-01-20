using ResidentialExpenseControl.Application.DTOs.Transaction;
using ResidentialExpenseControl.Application.UseCases.Transaction.Interfaces;
using ResidentialExpenseControl.Domain.Interfaces;

namespace ResidentialExpenseControl.Application.UseCases.Transaction
{
    // Caso de uso para listagem de todas as transações
    // Inclui dados das entidades relacionadas para facilitar exibição
    public class GetAllTransactionsUseCase : IGetAllTransactionsUseCase
    {
        private readonly ITransactionRepository _transactionRepository;

        public GetAllTransactionsUseCase(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<TransactionResponse>> ExecuteAsync()
        {
            var transactions = await _transactionRepository.GetAllAsync();

            return transactions.Select(t => new TransactionResponse
            {
                Id = t.Id,
                Value = t.Value,
                Description = t.Description,
                Type = (int)t.Type,
                TypeName = t.Type.ToString(),
                CreatedAt = t.CreatedAt,
                PersonId = t.PersonId,
                PersonName = t.Person.Name,
                CategoryId = t.CategoryId,
                CategoryDescription = t.Category.Description
            });
        }
    }
}