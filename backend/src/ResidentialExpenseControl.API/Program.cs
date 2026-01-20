using Microsoft.EntityFrameworkCore;
using ResidentialExpenseControl.API.Middlewares;
using ResidentialExpenseControl.Infrastructure;
using ResidentialExpenseControl.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuração da connection string do SQLite
// O arquivo será criado na pasta da API
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=residential_expense.db";

// Registra todos os serviços da Infrastructure (DbContext, Repositories, UseCases)
builder.Services.AddInfrastructure(connectionString);

// Configuração padrão de Controllers
builder.Services.AddControllers();

// Swagger para documentação e testes
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS para permitir chamadas do frontend React
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Cria o banco de dados automaticamente se não existir
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

// Pipeline de middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware de tratamento de exceções (deve ser um dos primeiros)
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();