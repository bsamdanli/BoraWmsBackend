using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using BoraWmsNew.Authorization.Roles;
using BoraWmsNew.Authorization.Users;
using BoraWmsNew.MultiTenancy;
using BoraWmsNew.Receiving;

namespace BoraWmsNew.EntityFrameworkCore
{
    public class BoraWmsNewDbContext : AbpZeroDbContext<Tenant, Role, User, BoraWmsNewDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<BoraWmsNew.Clients.Client> Client { get; set; } //Yada using BoraWmsNew.Client olarak ekle.
        public DbSet<BoraWmsNew.Products.Product> Product { get; set; }
        public DbSet<BoraWmsNew.Storages.Storage> Storage { get; set; }

        public DbSet<BoraWmsNew.StockMovements.StockMovement> StockMovements { get; set; }

        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentDetail> DocumentDetails { get; set; }

        public BoraWmsNewDbContext(DbContextOptions<BoraWmsNewDbContext> options)
            : base(options)
        {
        }
    }
}
