using AuthAPI.Domain.ValueObjects;

namespace AuthAPI.Infra.Data.ADO.NET
{
    public abstract class AbstractADORepository
    {
        protected readonly AuthApiDbConnection Connection;

        protected AbstractADORepository(AuthApiDbConnection connection)
        {
            Connection = connection;
        }

        protected MethodResult Success()
        {
            return new MethodResult();
        }

        protected MethodResult Error(string errorMessage)
        {
            return new MethodResult(errorMessage);
        }
    }
}