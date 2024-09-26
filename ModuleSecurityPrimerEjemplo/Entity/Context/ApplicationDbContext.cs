using Dapper;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection;
using System.Transactions;
using static System.Net.Mime.MediaTypeNames;

namespace Entity.Context
{
    public class ApplicationDbContext : DbContext

    {
        protected readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        }

        public override int SaveChanges()
        {
            EnsureAudit();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSucces, CancellationToken cancellationToken = default)
        {
            EnsureAudit();
            return base.SaveChangesAsync(acceptAllChangesOnSucces, cancellationToken);

        }
        public async Task<IEnumerable<T>> QueryAsync<T>(string text, object parameters = null, int? timeout = null, CommandType? type = null)
        {
            using var command = new DapperEFCoreCommand(this,
                                      text,
                                      parameters,
                                      timeout,
                                      type,
                                      CancellationToken.None);
            var connection = Database.GetDbConnection();
            return await connection.QueryAsync<T>(command.Definition);
        }
        public async Task<T> QueryFirstOrDefaultAsync<T>(string text, object parameters = null, int? timeout = null, CommandType? type = null)
        {
            using var command = new DapperEFCoreCommand(this, text, parameters, timeout, type, CancellationToken.None);
            var connection = Database.GetDbConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(command.Definition);
        }
        private void EnsureAudit()
        {
            ChangeTracker.DetectChanges();
        }
        //security 
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Modulo> Modulos => Set<Modulo>();
        public DbSet<Person> Person => Set<Person>();
        public DbSet<RoleView> RoleViews => Set<RoleView>();
        public DbSet<User> Users => Set<User>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<View> Views => Set<View>();
        public DbSet<Country> countrys => Set<Country>();
        public DbSet<Department> departments => Set<Department>();
        public DbSet<City> citys => Set<City>();

        public readonly struct DapperEFCoreCommand : IDisposable
        {
            public DapperEFCoreCommand(DbContext context, string text, object parameters, int? timeout, CommandType? type, CancellationToken ct)
            {
                var transaction = context.Database.CurrentTransaction?.GetDbTransaction();
                var commandType = type ?? CommandType.Text;
                var commandTimeout = timeout ?? context.Database.GetCommandTimeout() ?? 30;

                Definition =  new CommandDefinition( text, parameters, transaction, commandTimeout, commandType, cancellationToken: ct);
            }
            public CommandDefinition Definition { get; }

            public void Dispose() { }
        }
    
    }
}
