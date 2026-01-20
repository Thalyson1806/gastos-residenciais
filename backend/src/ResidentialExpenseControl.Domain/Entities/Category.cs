using ResidentialExpenseControl.Domain.Enums;
using ResidentialExpenseControl.Domain.Exceptions;

namespace ResidentialExpenseControl.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public Purpose Purpose { get; private set; }

        // Construtor protegido para o EF Core
        protected Category() { }

        public Category(string description, Purpose purpose)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new DomainException("Description is required.");

            // Valida se o Purpose é um valor válido do enum
            if (!Enum.IsDefined(typeof(Purpose), purpose))
                throw new DomainException("Invalid purpose value.");

            Id = Guid.NewGuid();
            Description = description.Trim();
            Purpose = purpose;
        }

        // Verifica se a categoria permite despesas
        public bool AllowsExpense()
        {
            return Purpose == Purpose.Expense || Purpose == Purpose.Both;
        }

        // Verifica se a categoria permite receitas
        public bool AllowsIncome()
        {
            return Purpose == Purpose.Income || Purpose == Purpose.Both;
        }
    }
}
