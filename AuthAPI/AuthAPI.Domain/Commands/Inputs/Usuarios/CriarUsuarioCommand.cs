using System.Collections.Generic;
using AuthAPI.Domain.Commands.Inputs.Telefones;
using AuthAPI.Domain.Commands.Interfaces;

namespace AuthAPI.Domain.Commands.Inputs.Usuarios
{
    public class CriarUsuarioCommand : ICommand
    {
        public CriarUsuarioCommand()
        {
            Telefones = new List<CriarTelefoneCommand>();
        }

        public CriarUsuarioCommand(string nome, string email, string senha)
            : this()
        {
            Nome = nome;
            Email = email;
            Senha = senha;
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public List<CriarTelefoneCommand> Telefones { get; set; }
    }
}