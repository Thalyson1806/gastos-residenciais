namespace ResidentialExpenseControl.Application.DTOs.Person
{
    // DTO de saída - representa o que a API retorna

    public class PersonResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public bool IsMinor { get; set; }
    }
}