using AuthAPI.Domain.Commands.Interfaces;

namespace AuthAPI.Domain.Commands.Outputs.Telefones
{
    public class TelefoneOutputCommand : ICommand
    {
        public TelefoneOutputCommand()
        {
            
        }

        public TelefoneOutputCommand(string ddd, string numero)
        {
            Ddd = ddd;
            Numero = numero;
        }

        public string Ddd { get; set; }
        public string Numero { get; set; }
    }
}