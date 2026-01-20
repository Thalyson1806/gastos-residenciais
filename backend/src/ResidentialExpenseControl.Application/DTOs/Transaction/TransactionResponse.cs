namespace ResidentialExpenseControl.Application.DTOs.Transaction
{
    // DTO de saída com dados da transação e nomes das entidades relacionadas
    //Facilita exibição no frontend sem precisar de chamadas adicionais

    public class TransactionResponse
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Type { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Dados da pessoa
        public Guid PersonId { get; set; }
        public string PersonName { get; set; } = string.Empty;

        // Dados da categoria
        public Guid CategoryId { get; set; }
        public string CategoryDescription { get; set; } = string.Empty;
    }
}