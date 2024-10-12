global using Microsoft.EntityFrameworkCore;
using Amplifund_API_Example.Endpoints;
using Microsoft.Extensions.Options;

namespace Amplifund_API_Example.Contexts
{
    public class SqlDataContext : DbContext
    {
        public SqlDataContext(DbContextOptions<SqlDataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("TestDb"));
        }

        //Add links to endpoints here
        public DbSet<Person> Person => Set<Person>();
        public DbSet<TestEntity> TestTable => Set<TestEntity>();
    }
}
