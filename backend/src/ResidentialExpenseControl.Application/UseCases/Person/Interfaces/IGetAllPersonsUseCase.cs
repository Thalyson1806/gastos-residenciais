using ResidentialExpenseControl.Application.DTOs.Person;

namespace ResidentialExpenseCOntrol.Application.UseCases.Person.Interfaces
{
    public interface IGetAllPersonsUseCase
    {
        Task<IEnumerable<PersonResponse>> ExecuteAsync();
    }
}