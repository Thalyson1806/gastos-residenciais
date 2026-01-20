using ResidentialExpenseControl.Domain.Enums;
using ResidentialExpenseControl.Domain.Exceptions;

namespace ResidentialExpenseControl.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public decimal Value { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public TransactionType Type { get; private set; }
        public DateTime CreatedAt { get; private set; }

        // Chaves estrangeiras
        public Guid PersonId { get; private set; }
        public Guid CategoryId { get; private set; }

        // Propriedades de navegação para o EF Core
        public Person Person { get; private set; } = null!;
        public Category Category { get; private set; } = null!;

        // Construtor protegido para o EF Core
        protected Transaction() { }

        // Construtor com todas as validações de domínio
        // Recebe as entidades relacionadas para validar regras de negócio
        public Transaction(
            decimal value,
            string description,
            TransactionType type,
            Person person,
            Category category)
        {
            // Validação 1: Valor deve ser maior que zero
            if (value <= 0)
                throw new DomainException("Value must be greater than zero.");

            // Validação 2: Descrição é obrigatória
            if (string.IsNullOrWhiteSpace(description))
                throw new DomainException("Description is required.");

            // Validação 3: Tipo deve ser válido
            if (!Enum.IsDefined(typeof(TransactionType), type))
                throw new DomainException("Invalid transaction type.");

            // Validação 4: Menor de idade NÃO pode registrar receitas
            // Esta é uma REGRA DE NEGÓCIO que vive no domínio
            if (person.IsMinor() && type == TransactionType.Income)
                throw new DomainException("Minors cannot register income transactions.");

            // Validação 5: Categoria deve permitir o tipo de transação
            // Usa os métodos da entidade Category para verificar
            if (type == TransactionType.Expense && !category.AllowsExpense())
                throw new DomainException("This category does not allow expense transactions.");

            if (type == TransactionType.Income && !category.AllowsIncome())
                throw new DomainException("This category does not allow income transactions.");

            Id = Guid.NewGuid();
            Value = value;
            Description = description.Trim();
            Type = type;
            PersonId = person.Id;
            CategoryId = category.Id;
            Person = person;
            Category = category;
            CreatedAt = DateTime.UtcNow;
        }
    }
}