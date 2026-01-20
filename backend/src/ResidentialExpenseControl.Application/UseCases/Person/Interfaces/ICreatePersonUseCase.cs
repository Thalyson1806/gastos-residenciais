using ResidentialExpenseControl.Application.DTOs.Person;

namespace ResidentialExpenseControl.Application.UseCases.Person.Interfaces
{
    // Interface que define o contrato do caso de uso
    // Facilita testes unitários com mocks e inversão de dependência
    public interface ICreatePersonUseCase
    {
        Task<PersonResponse> ExecuteAsync(CreatePersonRequest request);
    }
}