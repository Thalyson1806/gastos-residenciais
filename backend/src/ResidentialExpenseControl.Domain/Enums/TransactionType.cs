namespace ResidentialExpenseControl.Domain.Enums
{

    // Tipo de transação: despesa ou receita
    // usado em conjunto com Category.Purpose para validação
    public enum TransactionType
    {
        Expense = 1,
        Income = 2
    }
}