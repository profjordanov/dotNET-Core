using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace TennisBookings.Web.Data
{
    public class TennisBookingDbContext : IdentityDbContext<TennisBookingsUser, TennisBookingsRole, string>
    {
        public TennisBookingDbContext(DbContextOptions<TennisBookingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Court> Courts { get; set; }

        public DbSet<CourtBooking> CourtBookings { get; set; }

        public DbSet<Member> Members { get; set; }
        
        public DbSet<CourtMaintenanceSchedule> CourtMaintenance { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                .HasOne(x => x.User)
                .WithOne(x => x.Member)
                .HasForeignKey<Member>(x => x.UserId);

            modelBuilder.Entity<Court>().HasData(
                new Court { Id = 1, Name = "Court 1", Type = CourtType.Indoor },
                new Court { Id = 2, Name = "Court 2", Type = CourtType.Indoor },
                new Court { Id = 3, Name = "Court 3", Type = CourtType.Outdoor },
                new Court { Id = 4, Name = "Court 4", Type = CourtType.Outdoor },
                new Court { Id = 5, Name = "Court 5", Type = CourtType.Outdoor });
         
            modelBuilder.Entity<CourtMaintenanceSchedule>().HasData(
                new CourtMaintenanceSchedule
                {
                    Id = 1,
                    WorkTitle = "Resurface",
                    CourtIsClosed = true,
                    StartDate = new DateTime(2019, 03, 01, 06, 00, 00),
                    EndDate = new DateTime(2019, 03, 07, 22, 00, 00),
                    CourtId = 4
                },
                new CourtMaintenanceSchedule
                {
                    Id = 2,
                    WorkTitle = "Replace Seats",
                    CourtIsClosed = false,
                    StartDate = new DateTime(2019, 04, 15, 12, 00, 00),
                    EndDate = new DateTime(2019, 04, 15, 13, 15, 00),
                    CourtId = 1
                },
                new CourtMaintenanceSchedule
                {
                    Id = 3,
                    WorkTitle = "Replace Net",
                    CourtIsClosed = true,
                    StartDate = new DateTime(2019, 02, 08, 07, 00, 00),
                    EndDate = new DateTime(2019, 02, 08, 09, 00, 00),
                    CourtId = 2
                });
       
            base.OnModelCreating(modelBuilder);
        }
    }
}
