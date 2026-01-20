using Microsoft.AspNetCore.Mvc;
using ResidentialExpenseControl.Application.DTOs.Reports;
using ResidentialExpenseControl.Application.UseCases.Reports.Interfaces;

namespace ResidentialExpenseControl.API.Controllers
{
    // Controller de relatórios
    // Fornece endpoints para consulta de totais e resumos financeiros
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IGetTotalsReportUseCase _getTotalsReportUseCase;

        public ReportsController(IGetTotalsReportUseCase getTotalsReportUseCase)
        {
            _getTotalsReportUseCase = getTotalsReportUseCase;
        }

        // GET api/reports/totals
        // Retorna totais por pessoa e totais gerais do sistema
        [HttpGet("totals")]
        public async Task<ActionResult<GeneralTotalsResponse>> GetTotals()
        {
            var response = await _getTotalsReportUseCase.ExecuteAsync();
            return Ok(response);
        }
    }
}