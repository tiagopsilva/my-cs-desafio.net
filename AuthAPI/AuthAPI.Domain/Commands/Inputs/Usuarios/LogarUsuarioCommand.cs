using AuthAPI.Domain.Commands.Interfaces;

namespace AuthAPI.Domain.Commands.Inputs.Usuarios
{
    public class LogarUsuarioCommand : ICommand
    {
        public LogarUsuarioCommand()
        {
            
        }

        public LogarUsuarioCommand(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        public string Email { get; set; }
        public string Senha { get; set; }
    }
}