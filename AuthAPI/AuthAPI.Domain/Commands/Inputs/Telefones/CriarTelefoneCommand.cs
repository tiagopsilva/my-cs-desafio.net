using AuthAPI.Domain.Commands.Interfaces;

namespace AuthAPI.Domain.Commands.Inputs.Telefones
{
    public class CriarTelefoneCommand : ICommand
    {
        public CriarTelefoneCommand()
        {
            
        }

        public CriarTelefoneCommand(string ddd, string numero)
        {
            Ddd = ddd;
            Numero = numero;
        }

        public string Ddd { get; set; }
        public string Numero { get; set; }
    }
}