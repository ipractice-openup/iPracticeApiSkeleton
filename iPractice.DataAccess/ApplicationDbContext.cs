using iPractice.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace iPractice.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Psychologist> Psychologists { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Availability> Availabilities { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Psychologist>().HasKey(psychologist => psychologist.Id);
            modelBuilder.Entity<Client>().HasKey(client => client.Id);
            modelBuilder.Entity<Psychologist>().HasMany(p => p.Clients).WithMany(b => b.Psychologists);
            modelBuilder.Entity<Client>().HasMany(p => p.Psychologists).WithMany(b => b.Clients);
            
            modelBuilder.Entity<Appointment>().HasKey(app => app.Id);
            modelBuilder.Entity<Appointment>().HasOne(app => app.Client);
            modelBuilder.Entity<Appointment>().HasOne(app => app.Psychologist);

            modelBuilder.Entity<Availability>().HasKey(av => av.Id);
            modelBuilder.Entity<Availability>().HasOne(av => av.Psychologist);
        }
    }
}
