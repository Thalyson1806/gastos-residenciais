using ResidentialExpenseControl.Application.DTOs.Reports;

namespace ResidentialExpenseControl.Application.UseCases.Reports.Interfaces
{
    // Interface para o caso de uso de relatório de totais
    // Retorna totais por pessoa e totais gerais
    public interface IGetTotalsReportUseCase
    {
        Task<GeneralTotalsResponse> ExecuteAsync();
    }
}
