using System;
using System.Collections.Generic;
using AuthAPI.Domain.Services;

namespace AuthAPI.Domain.Entities
{
    public class Usuario
    {
        protected Usuario()
        {
            Id = Guid.NewGuid();
            Telefones = new List<Telefone>();
        }

        public Usuario(string nome, string email, string password)
            : this()
        {
            Nome = nome;
            Email = email;
            AlterarSenha(password);
        }

        public Usuario(string nome, string email, string password, DateTime dataCriacao)
            : this()
        {
            Nome = nome;
            Email = email;
            DataCriacao = dataCriacao;
            AlterarSenha(password);
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; private set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataUpdate { get; set; }
        public DateTime? DataUltimoLogin { get; set; }
        public string Token { get; set; }

        public virtual ICollection<Telefone> Telefones { get; set; }

        public void AlterarSenha(string novaSenha)
        {
            Senha = new PasswordEncryptionService().EncryptPassword(novaSenha);
        }

        public bool ValidarSenha(string senha)
        {
            return new PasswordEncryptionService().ComparePasswords(senha, Senha);
        }
    }
}