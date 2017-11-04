using System;
using System.Collections.Generic;
using AuthAPI.Domain.Commands.Outputs.Telefones;
using Newtonsoft.Json;

namespace AuthAPI.Domain.Commands.Outputs.Usuarios
{
    public class UsuarioOutputCommand
    {
        public UsuarioOutputCommand()
        {
            Telefones = new List<TelefoneOutputCommand>();
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha => "***";

        [JsonProperty(PropertyName = "data_criacao")]
        public DateTime DataCriacao { get; set; }

        [JsonProperty(PropertyName = "data_atualizacao")]
        public DateTime? DataUpdate { get; set; }

        [JsonProperty(PropertyName = "ultimo_login")]
        public DateTime? DataUltimoLogin { get; set; }

        public string Token { get; set; }

        public ICollection<TelefoneOutputCommand> Telefones { get; set; }
    }
}