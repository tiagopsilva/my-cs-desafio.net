using System.Data.Entity.ModelConfiguration;
using AuthAPI.Domain.Constants;
using AuthAPI.Domain.Entities;
using AuthAPI.Infra.Data.EF.Extensions;

namespace AuthAPI.Infra.Data.EF.EntitiesMap
{
    public class TelefoneMap : EntityTypeConfiguration<Telefone>
    {
        public TelefoneMap()
        {
            ToTable("Telefones");

            HasKey(x => x.Id);

            // properties
            Property(x => x.UsuarioId)
                .IsRequired()
                .IsUniqueKey("unq_Telefone", order: 0);

            Property(x => x.Ddd)
                .IsRequired()
                .HasMaxLength(TelefoneSchemaInfo.DddMaxLength)
                .IsUniqueKey("unq_Telefone", order: 1);

            Property(x => x.Numero)
                .IsRequired()
                .HasMaxLength(TelefoneSchemaInfo.TelefoneMaxLength)
                .IsUniqueKey("unq_Telefone", order: 2);

            // relationships
            HasRequired(x => x.Usuario)
                .WithMany(x => x.Telefones)
                .HasForeignKey(x => x.UsuarioId);
        }
    }
}