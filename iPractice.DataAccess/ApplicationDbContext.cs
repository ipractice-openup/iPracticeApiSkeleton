using iPractice.Application.Contract.Dtos;
using Microsoft.EntityFrameworkCore;

namespace iPractice.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<PsychologistDto> Psychologists { get; set; }
        public DbSet<ClientDto> Clients { get; set; }
        public DbSet<ClientPsychologistDto> ClientsPsychologists { get; set; }
        public DbSet<AvailabilityDto> Availabilities { get; set; }
        public DbSet<TimeSlotDto> TimeSlots { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PsychologistDto>().HasKey(x => x.Id);
            modelBuilder.Entity<ClientDto>().HasKey(x => x.Id);
            modelBuilder.Entity<AvailabilityDto>().HasKey(x => x.Id);
            modelBuilder.Entity<TimeSlotDto>().HasKey(x => x.Id);
            modelBuilder.Entity<ClientPsychologistDto>().HasNoKey();
            modelBuilder.Entity<PsychologistDto>().HasMany(x => x.Clients).WithMany(x => x.Psychologists);
            modelBuilder.Entity<PsychologistDto>().HasMany(x => x.Availabilities).WithOne(x => x.Psychologist);
            modelBuilder.Entity<ClientDto>().HasMany(x => x.Psychologists).WithMany(x => x.Clients);
            modelBuilder.Entity<AvailabilityDto>().HasOne(x => x.Psychologist).WithMany(x => x.Availabilities);
            modelBuilder.Entity<AvailabilityDto>().HasMany(x => x.TimeSlots).WithOne(x => x.Availability);
            modelBuilder.Entity<TimeSlotDto>().HasOne(x => x.Availability).WithMany(x => x.TimeSlots);
            modelBuilder.Entity<ClientPsychologistDto>().HasOne(x => x.Client);
            modelBuilder.Entity<ClientPsychologistDto>().HasOne(x => x.Psychologist);
        }
        //dotnet ef migrations add InitialCreate --project iPractice.DataAccess --startup-project iPractice.Api
        //dotnet ef database update --project iPractice.DataAccess --startup-project iPractice.Api
    }
}
