namespace ResidentialExpenseControl.Application.DTOs.Transaction
{

    // DTO de entrada para criação de transação
    // Recebe os IDs das entidades relacionadas
    public class CreateTransactionRequest
    {
        public decimal Value { get; set; }
        public string Description { get; set; } = string.Empty;

        //1 = Expense, 2 = Income
        public int Type { get; set; }

        public Guid PersonId { get; set; }
        public Guid CategoryId { get; set; }

    }
}