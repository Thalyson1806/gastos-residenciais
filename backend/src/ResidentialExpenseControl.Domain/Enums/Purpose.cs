namespace ResidentialExpenseControl.Domain.Enums
{
    // define qual tipo de transação a categoria pode ser usada
    // Expense = apenas despesas, Income = apenas receitas, Both = ambos
    public enum Purpose
    {
        Expense = 1,
        Income = 2,
        Both = 3
    }
}