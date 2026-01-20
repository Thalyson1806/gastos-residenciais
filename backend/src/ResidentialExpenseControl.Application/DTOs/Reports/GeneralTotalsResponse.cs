namespace ResidentialExpenseControl.Application.DTOs.Reports
{
    // DTO com totais gerais do sistema e lista de totais por pessoa
    // Fornece visão completa das finanças residenciais
    public class GeneralTotalsResponse
    {
        // Totais gerais (soma de todas as pessoas)
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance { get; set; }

        // Lista de totais individuais por pessoa
        public IEnumerable<PersonTotalsResponse> PersonTotals { get; set; } = new List<PersonTotalsResponse>();
    }
}