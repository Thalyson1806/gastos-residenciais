using ResidentialExpenseControl.Application.DTOs.Person;

namespace ReisdentialExpenseControl.Application.UseCases.Person.Interfaces
{
    public interface ICreatePersonUseCase
    {
        Task<PersonResponse> ExecuteAsync(CreatePersonRequest request);
    }
}