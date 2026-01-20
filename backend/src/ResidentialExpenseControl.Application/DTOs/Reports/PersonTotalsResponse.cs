namespace ResidentialExpenseControl.Application.DTOs.Reports
{
    // DTO que representa os totais financeiros de uma pessoa
    // Usado no relatório de totais por pessoa
    public class PersonTotalsResponse
    {
        public Guid PersonId { get; set; }
        public string PersonName { get; set; } = string.Empty;
        public int Age { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }

        // Saldo = Receitas - Despesas
        // Valor positivo indica que a pessoa tem mais receitas
        public decimal Balance { get; set; }
    }
}