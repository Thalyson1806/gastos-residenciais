using ResidentialExpenseControl.Application.DTOs.Transaction;

namespace ResidentialExpenseControl.Application.UseCases.Transaction.Interfaces
{
    public interface ICreateTransactionUseCase
    {
        Task<TransactionResponse> ExecuteAsync(CreateTransactionRequest request);
    }
}