# Sistema de Controle de Gastos Residenciais

Sistema para gerenciamento de finanças residenciais desenvolvido com .NET e React, permitindo controlar receitas e despesas por pessoa e categoria.

## Sobre o Projeto

Este projeto foi desenvolvido como teste técnico demonstrando conhecimentos em:

- Arquitetura limpa (Clean Architecture)
- Desenvolvimento de API RESTful com .NET
- Frontend com React e TypeScript
- Princípios SOLID
- Boas práticas de desenvolvimento de software

## Arquitetura

O projeto segue os princípios da Clean Architecture, separando responsabilidades em camadas.

### Fluxo de Dependências

API → Application → Domain ← Infrastructure


A camada Domain não possui dependências externas. Infrastructure implementa as interfaces definidas em Domain, seguindo o princípio de Inversão de Dependência (DIP).

### Responsabilidades das Camadas

| Camada | Responsabilidade | Dependências |
|--------|-----------------|--------------|
| Domain | Entidades, regras de negócio, interfaces de repositório | Nenhuma |
| Application | Casos de uso, orquestração, DTOs | Domain |
| Infrastructure | Implementação de repositórios, EF Core, banco de dados | Domain, Application |
| API | Controllers REST, middlewares, configuração | Application, Infrastructure |
| Frontend | Interface do usuário | Consome a API |

## Tecnologias Utilizadas

### Backend
- .NET 
- C# 
- Entity Framework Core
- SQLite
- Swagger/OpenAPI

### Frontend
- React 
- TypeScript
- Vite
- Axios

## Funcionalidades

### Pessoas (Person)
- Cadastro com nome e idade
- Listagem de todas as pessoas
- Exclusão com remoção em cascata das transações
- Validações: nome obrigatório, idade maior que zero

### Categorias (Category)
- Cadastro com descrição e finalidade (Purpose)
- Finalidades disponíveis: Expense (Despesa), Income (Receita), Both (Ambos)
- Listagem de todas as categorias

### Transações (Transaction)
- Cadastro com valor, descrição, tipo, pessoa e categoria
- Tipos disponíveis: Expense (Despesa), Income (Receita)
- Listagem de todas as transações

#### Regras de Negócio
1. Valor da transação deve ser maior que zero
2. Pessoa menor de 18 anos não pode registrar receitas
3. Categoria deve permitir o tipo de transação selecionado

### Relatórios (Reports)
- Totais por pessoa: receitas, despesas e saldo
- Totais gerais do sistema

## Como Executar

### Pré-requisitos
- .NET 8 SDK
- Node.js 18+
- npm ou yarn

### Backend

```bash
cd backend/src/ResidentialExpenseControl.API
dotnet run
A API estará disponível na porta indicada no terminal (padrão: 5019).

Documentação Swagger disponível em: http://localhost:{porta}/swagger

Frontend

cd frontend
npm install
npm run dev
O frontend estará disponível em http://localhost:5173.

Observação: Verifique se a URL da API no arquivo frontend/src/services/api.ts corresponde à porta do backend.

Estrutura do Projeto

gastos-residenciais/
|
+-- backend/
|   +-- src/
|       +-- ResidentialExpenseControl.Domain/
|       |   +-- Entities/
|       |   +-- Enums/
|       |   +-- Exceptions/
|       |   +-- Interfaces/
|       |
|       +-- ResidentialExpenseControl.Application/
|       |   +-- DTOs/
|       |   +-- UseCases/
|       |
|       +-- ResidentialExpenseControl.Infrastructure/
|       |   +-- Data/
|       |   +-- Repositories/
|       |
|       +-- ResidentialExpenseControl.API/
|           +-- Controllers/
|           +-- Middlewares/
|
+-- frontend/
|   +-- src/
|       +-- pages/
|       +-- services/
|       +-- types/
|
+-- README.md
Endpoints da API
Persons
Método	Endpoint	Descrição
GET	/api/persons	Lista todas as pessoas
POST	/api/persons	Cria uma nova pessoa
DELETE	/api/persons/{id}	Remove uma pessoa e suas transações
Categories
Método	Endpoint	Descrição
GET	/api/categories	Lista todas as categorias
POST	/api/categories	Cria uma nova categoria
Transactions
Método	Endpoint	Descrição
GET	/api/transactions	Lista todas as transações
POST	/api/transactions	Cria uma nova transação
Reports
Método	Endpoint	Descrição
GET	/api/reports/totals	Retorna totais por pessoa e totais gerais
Decisões Técnicas
Clean Architecture
Escolhida para garantir separação de responsabilidades, facilitar testes unitários e manter o domínio independente de frameworks externos. Permite que mudanças em camadas externas não afetem as regras de negócio.

SQLite
Utilizado por oferecer persistência real sem necessidade de configuração de servidor de banco de dados. Ideal para demonstração e desenvolvimento, podendo ser facilmente substituído por outro banco relacional.

Validações no Domain
As regras de negócio estão centralizadas nas entidades do domínio, garantindo que objetos inválidos nunca sejam criados, independentemente do ponto de entrada (API, testes, etc.).

Use Cases Separados
Cada operação possui seu próprio Use Case, seguindo o Single Responsibility Principle. Isso facilita manutenção, testes e compreensão do código.

Exception Handling Middleware
Tratamento centralizado de exceções na API, convertendo DomainException em respostas HTTP 400 (Bad Request) com mensagens apropriadas.

Tratamento de Erros
O sistema utiliza um middleware global para tratamento de exceções:

DomainException: Retorna HTTP 400 com mensagem de erro de negócio
Outras exceções: Retorna HTTP 500 com mensagem genérica (detalhes logados no servidor)
