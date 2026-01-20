using Microsoft.AspNetCore.Mvc;
using ResidentialExpenseControl.Application.DTOs.Transaction;
using ResidentialExpenseControl.Application.UseCases.Transaction.Interfaces;


namespace ResidentialExpenseControl.API.Controllers
{
    // Controller de transações
    // Apenas recebe requisições e delega para Use Cases
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ICreateTransactionUseCase _createTransactionUseCase;
        private readonly IGetAllTransactionsUseCase _getAllTransactionsUseCase;

        public TransactionsController(
            ICreateTransactionUseCase createTransactionUseCase,
            IGetAllTransactionsUseCase getAllTransactionsUseCase)
        {
            _createTransactionUseCase = createTransactionUseCase;
            _getAllTransactionsUseCase = getAllTransactionsUseCase;
        }

        // POST api/transactions
        // Cria uma nova transação - validações de negócio são feitas no Domain
        [HttpPost]
        public async Task<ActionResult<TransactionResponse>> Create([FromBody] CreateTransactionRequest request)
        {
            var response = await _createTransactionUseCase.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetAll), new { id = response.Id }, response);
        }

        // GET api/transactions
        // Lista todas as transações com dados de pessoa e categoria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionResponse>>> GetAll()
        {
            var response = await _getAllTransactionsUseCase.ExecuteAsync();
            return Ok(response);
        }
    }
}