using Microsoft.EntityFrameworkCore;
using GestionPlantule.Models;

namespace GestionPlantule.Models
{
    public class ArganaweedContext : DbContext
    {
       
        public DbSet<User> users { get; set; }
        public DbSet<Provenance> provenances { get; set; }
        public DbSet<Emplacement> emplacements { get; set; }
        public DbSet<Variete> varietes { get; set; }
        public DbSet<Plantule> plantules { get; set; }
        public DbSet<Event> events { get; set; }
        public DbSet<Note> notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=Isidore\\SQLEXPRESS;Database=arganaweed;Encrypt=False;Trusted_Connection=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Provenance>().ToTable("provenances");
            modelBuilder.Entity<Emplacement>().ToTable("emplacements");
            modelBuilder.Entity<Variete>().ToTable("varietes");
            modelBuilder.Entity<Plantule>().ToTable("plantules");
            modelBuilder.Entity<Event>().ToTable("events");
            modelBuilder.Entity<Note>().ToTable("notes");

            modelBuilder.Entity<Plantule>()
                .Property(p => p.Stade)
                .HasDefaultValue("Initiation")
                .HasConversion<string>();

            modelBuilder.Entity<Plantule>()
                .Property(p => p.Sante)
                .HasDefaultValue("Bon")
                .HasConversion<string>();

            modelBuilder.Entity<Plantule>()
                .Property(p => p.Statut)
                .HasDefaultValue(true);

            modelBuilder.Entity<Plantule>()
                .Property(p => p.ArchiveStatut)
                .HasDefaultValue(false);
        }
    }
}
