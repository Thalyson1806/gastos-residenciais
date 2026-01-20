using ResidentialExpenseControl.Application.DTOs.Person;

namespace ResidentialExpenseControl.Application.UseCases.Person.Interfaces
{
    // Interface para listagem de pessoas

    public interface IGetAllPersonsUseCase
    {
        Task<IEnumerable<PersonResponse>> ExecuteAsync();
    }
}