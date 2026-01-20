
namespace ResidentialExpenseControl.Application.UseCases.Person.Interfaces
{
 public interface IDeletePersonUseCase
    {
        Task ExecuteAsync(Guid id);
    }
}
