using BakokiWeb.Shared;
using Microsoft.EntityFrameworkCore;

namespace BakokiWeb.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Cliente> Clientes { get; set; }
        
        public DbSet<Cuenta> Cuentas { get; set; }
        
        public DbSet<Transacion> Transaciones { get; set; }
    }

}
