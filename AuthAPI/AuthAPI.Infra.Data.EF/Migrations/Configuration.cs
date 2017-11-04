using System.Data.Entity.Migrations;
using AuthAPI.Infra.Data.EF.EFContext;

namespace AuthAPI.Infra.Data.EF.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AuthAPIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
