namespace ResidentialExpenseControl.Application.UseCases.Person.Interfaces
{
    // Interface para exclusão de pessoa
    // Retorna Task sem valor pois DELETE não precisa retornar dados
    public interface IDeletePersonUseCase
    {
        Task ExecuteAsync(Guid id);
    }
}