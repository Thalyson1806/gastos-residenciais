using ResidentialExpenseControl.Domain.Exceptions;

namespace ResidentialExpenseControl.Domain.Entities
{
    public class Person
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public int Age { get; private set; }

        // Construtor protegido para o EF Core conseguir instanciar
        protected Person() { }

        public Person(string name, int age)
        {
            // Validações de domínio - regras que SEMPRE devem ser verdadeiras
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name is required.");

            if (age <= 0)
                throw new DomainException("Age must be greater than zero.");

            Id = Guid.NewGuid();
            Name = name.Trim();
            Age = age;
        }

        // Método para verificar se a pessoa é menor de idade
        // Regra de negócio encapsulada na entidade
        public bool IsMinor()
        {
            return Age < 18;
        }
    }
}
