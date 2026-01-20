using ResidentialExpenseControl.Application.DTOs.Reports;
using ResidentialExpenseControl.Application.UseCases.Reports.Interfaces;
using ResidentialExpenseControl.Domain.Enums;
using ResidentialExpenseControl.Domain.Interfaces;

namespace ResidentialExpenseControl.Application.UseCases.Reports
{
    // Caso de uso que calcula totais financeiros
    // Agrupa transações por pessoa e calcula receitas, despesas e saldo
    public class GetTotalsReportUseCase : IGetTotalsReportUseCase
    {
        private readonly IPersonRepository _personRepository;
        private readonly ITransactionRepository _transactionRepository;

        public GetTotalsReportUseCase(
            IPersonRepository personRepository,
            ITransactionRepository transactionRepository)
        {
            _personRepository = personRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<GeneralTotalsResponse> ExecuteAsync()
        {
            var persons = await _personRepository.GetAllAsync();
            var transactions = await _transactionRepository.GetAllAsync();

            var personTotals = new List<PersonTotalsResponse>();

            // Calcula totais para cada pessoa
            foreach (var person in persons)
            {
                var personTransactions = transactions.Where(t => t.PersonId == person.Id);

                // Soma todas as receitas (Income) da pessoa
                var totalIncome = personTransactions
                    .Where(t => t.Type == TransactionType.Income)
                    .Sum(t => t.Value);

                // Soma todas as despesas (Expense) da pessoa
                var totalExpense = personTransactions
                    .Where(t => t.Type == TransactionType.Expense)
                    .Sum(t => t.Value);

                personTotals.Add(new PersonTotalsResponse
                {
                    PersonId = person.Id,
                    PersonName = person.Name,
                    Age = person.Age,
                    TotalIncome = totalIncome,
                    TotalExpense = totalExpense,
                    Balance = totalIncome - totalExpense
                });
            }

            // Calcula totais gerais (soma de todas as pessoas)
            var generalTotalIncome = personTotals.Sum(p => p.TotalIncome);
            var generalTotalExpense = personTotals.Sum(p => p.TotalExpense);

            return new GeneralTotalsResponse
            {
                TotalIncome = generalTotalIncome,
                TotalExpense = generalTotalExpense,
                Balance = generalTotalIncome - generalTotalExpense,
                PersonTotals = personTotals
            };
        }
    }
}