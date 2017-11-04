using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using AuthAPI.Domain.Entities;
using AuthAPI.Infra.Data.EF.EntitiesMap;

namespace AuthAPI.Infra.Data.EF.EFContext
{
    public class AuthAPIContext : DbContext
    {
        public AuthAPIContext()
            : base("AuthAPIConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>().Configure(x => x.HasColumnType("varchar"));

            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new TelefoneMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}