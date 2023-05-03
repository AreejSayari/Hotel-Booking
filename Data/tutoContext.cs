using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tuto.Models;

namespace tuto.Data
{
    public class tutoContext : DbContext
    {
        public tutoContext (DbContextOptions<tutoContext> options)
            : base(options)
        {
        }

        public DbSet<tuto.Models.Chambre> Chambre { get; set; } = default!;

        public DbSet<tuto.Models.Client>? Client { get; set; }

        public DbSet<tuto.Models.Reservation>? Reservation { get; set; }
        public DbSet<tuto.Models.Facture> Facture { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Facture)
                .WithOne(f => f.Reservation)
                .HasForeignKey<Facture>(f => f.IdReservation);
       

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Chambre)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.IdChambre);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Client)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.IdClient);
        }
    }
}
