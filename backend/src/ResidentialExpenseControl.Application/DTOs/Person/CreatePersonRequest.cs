namespace ResidentialExpenseControl.Application.DTOs.Person
{
    // DTO de entrada - representa o que a API recebe
    // não tem lógica, apenas dados
    public class CreatePersonRequest
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}