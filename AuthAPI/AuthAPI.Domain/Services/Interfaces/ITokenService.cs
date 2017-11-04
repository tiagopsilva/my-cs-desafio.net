using AuthAPI.Domain.Entities;

namespace AuthAPI.Domain.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Usuario usuario);
    }
}