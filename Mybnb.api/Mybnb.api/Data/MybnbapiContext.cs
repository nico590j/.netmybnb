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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                         .HasIndex(user => user.Email)
                         .IsUnique();

            modelBuilder.Entity<User>().HasData(new User { UserID = -1, Email = "test@edu.ucl.dk", Password = "9F86D081884C7D659A2FEAA0C55AD015A3BF4F1B2B0B822CD15D6C15B0F00A08", FullName = "nico590j" });
            
            modelBuilder.Entity<BNB>().HasData
            (
                new BNB {
                    ID = -1,
                    Title = "test",
                    Description  = "test data beskrivelse",
                    Address  = "test",
                    ZipCode = "1234",
                    Country = "DK",
                    OwnerUserID = -1
                }
            );

            var renting = new PossibleRentingPeriod
            {
                DailyPrice = 123456,
                MinimumRentingDays = 12,
                PossibleRentingPeriodID = -1,
                StartDate = DateTime.Now,
                BNBID = -1,
                EndDate = DateTime.Now.AddDays(24)
            };
            modelBuilder.Entity<PossibleRentingPeriod>().HasData(renting);
        }
    }
}
