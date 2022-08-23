using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace BoraWmsNew.EntityFrameworkCore
{
    public static class BoraWmsNewDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<BoraWmsNewDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<BoraWmsNewDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
