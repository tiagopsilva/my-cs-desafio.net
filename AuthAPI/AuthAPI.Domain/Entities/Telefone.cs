using System;
using AuthAPI.Domain.Extensions;

namespace AuthAPI.Domain.Entities
{
    public class Telefone
    {
        private string _ddd;
        private string _numero;

        protected Telefone()
        {
            Id = Guid.NewGuid();
        }

        public Telefone(Guid usuarioId, string ddd, string numero)
            : this()
        {
            UsuarioId = usuarioId;
            Ddd = ddd;
            Numero = numero;
        }

        public Guid Id { get; set; }

        public Guid UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        public string Ddd
        {
            get { return _ddd; }
            set { _ddd = value.OnlyNumbers(); }
        }

        public string Numero
        {
            get { return _numero; }
            set { _numero = value.OnlyNumbers(); }
        }
    }
}