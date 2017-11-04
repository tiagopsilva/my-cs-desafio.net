using System;
using System.Linq;
using AuthAPI.Domain.Commands.Inputs.Usuarios;
using AuthAPI.Domain.Commands.Outputs.Telefones;
using AuthAPI.Domain.Commands.Outputs.Usuarios;
using AuthAPI.Domain.Entities;

namespace AuthAPI.Domain.Mapper
{
    public class UsuarioMapper
    {
        public Usuario MapFrom(CriarUsuarioCommand command)
        {
            var usuario = new Usuario(command.Nome, command.Email, command.Senha, DateTime.Now);

            command.Telefones.ForEach(telefone => usuario.Telefones.Add(new Telefone(usuario.Id, telefone.Ddd, telefone.Numero)));

            return usuario;
        }

        public UsuarioOutputCommand OutputFrom(Usuario usuario)
        {
            return new UsuarioOutputCommand
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataCriacao = usuario.DataCriacao,
                DataUpdate = usuario.DataUpdate,
                DataUltimoLogin = usuario.DataUltimoLogin,
                Token = usuario.Token,
                Telefones = usuario.Telefones.Select(telefone => new TelefoneOutputCommand(telefone.Ddd, telefone.Numero)).ToList()
            };
        }
    }
}