using ResidentialExpenseControl.Application.DTOs.Transaction;

namespace ResidentialExpenseControl.Application.UseCases.Transaction.Interfaces
{
    public interface IGetAllTransactionsUseCase
    {
        Task<IEnumerable<TransactionResponse>> ExecuteAsync();
    }
}