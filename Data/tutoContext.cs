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
        public DbSet<LoginViewModel> loginViewModels { get; set; }
        public DbSet<tuto.Models.Client>? Client { get; set; }
        public DbSet<tuto.Models.Chambre> Chambre { get; set; }        
        public DbSet<tuto.Models.Reservation>? Reservation { get; set; }
        public DbSet<tuto.Models.Facture> Facture { get; set; }
        public DbSet<tuto.Models.Admin>? Admin { get; set; }
        public DbSet<tuto.Models.LoginViewModel>? LoginViewModel { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Reservation>()
            //    .HasOne(r => r.Facture)
            //    .WithOne(f => f.Reservation)
            //    .HasForeignKey<Facture>(f => f.IdReservation);

            modelBuilder.Entity<Client>()
                .HasOne(r => r.Facture)
                .WithOne(f => f.Client)
                .HasForeignKey<Facture>(f => f.IdClient);


            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Chambre)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.IdChambre);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Client)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.IdClient);

            //modelBuilder.Entity<Chambre>()
            //    .HasOne(r => r.TypeChambre)
            //    .WithMany(c => c.Chambres)
            //    .HasForeignKey(r => r.IdTypeChambre);
        }

        

        
    }
}
