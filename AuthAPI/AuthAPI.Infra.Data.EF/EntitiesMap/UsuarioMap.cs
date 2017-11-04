using System.Data.Entity.ModelConfiguration;
using AuthAPI.Domain.Constants;
using AuthAPI.Domain.Entities;
using AuthAPI.Infra.Data.EF.Extensions;

namespace AuthAPI.Infra.Data.EF.EntitiesMap
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            ToTable("Usuarios");

            HasKey(x => x.Id);

            // properties
            Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(UsuarioSchemaInfo.NomeMaxLength);

            Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(UsuarioSchemaInfo.EmailMaxLength)
                .IsUniqueKey("unq_Usuarios_Email");

            Property(x => x.Senha)
                .IsRequired()
                .HasMaxLength(UsuarioSchemaInfo.SenhaMaxLength);

            Property(x => x.DataCriacao)
                .IsRequired();

            Property(x => x.DataUpdate)
                .IsOptional();

            Property(x => x.DataUltimoLogin)
                .IsOptional();

            Property(x => x.Token)
                .IsOptional()
                .HasMaxLength(UsuarioSchemaInfo.TokenMaxLength);

            // relationships
            HasMany(x => x.Telefones)
                .WithRequired(x => x.Usuario);
        }
    }
}