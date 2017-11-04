using System;
using AuthAPI.Domain.Commands.Interfaces;

namespace AuthAPI.Domain.Commands.Inputs.Usuarios
{
    public class ObterUsuarioPeloIdCommand : ICommand
    {
        public ObterUsuarioPeloIdCommand()
        {
            
        }

        public ObterUsuarioPeloIdCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}