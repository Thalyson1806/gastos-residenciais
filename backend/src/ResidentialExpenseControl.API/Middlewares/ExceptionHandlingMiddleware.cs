using System.Net;
using System.Text.Json;
using ResidentialExpenseControl.Domain.Exceptions;

namespace ResidentialExpenseControl.API.Middlewares
{
    // Middleware global para tratar exceções
    // Converte exceções em respostas HTTP apropriadas
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "An unexpected error occurred.";

            // DomainException vira 400 Bad Request
            // Isso permite que regras de negócio retornem erros amigáveis
            if (exception is DomainException domainException)
            {
                statusCode = HttpStatusCode.BadRequest;
                message = domainException.Message;
            }
            else
            {
                // Loga apenas erros inesperados
                _logger.LogError(exception, "Unexpected error occurred");
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                error = message,
                statusCode = (int)statusCode
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}