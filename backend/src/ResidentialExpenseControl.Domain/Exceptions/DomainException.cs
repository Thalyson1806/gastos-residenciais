namespace ResidentialExpenseControl.Domain.Exceptions
{
    //Exceção customizada para erros de regra de negócio
    //Permite diferenciar erros de domínio de erros técnicos
    public class  DomainException : Exception
    {
        public DomainException(string message) : base(message)
        { 
        }
        
    }
}