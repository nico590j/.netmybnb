#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mybnb.api.Models;

namespace Mybnb.api.Data
{
    public class MybnbapiContext : DbContext
    {
        public MybnbapiContext (DbContextOptions<MybnbapiContext> options)
            : base(options)
        {
        }

        public DbSet<Mybnb.api.Models.User> Users { get; set; }

        public DbSet<Mybnb.api.Models.BNB> BNBs { get; set; }

        public DbSet<Mybnb.api.Models.PossibleRentingPeriod> PossibleRentingPeriods { get; set; }

        public DbSet<Mybnb.api.Models.TenantPeriod> TenantPeriods { get; set; }

        public DbSet<Mybnb.api.Models.BnbImage>  Images { get; set; }
    }
}
