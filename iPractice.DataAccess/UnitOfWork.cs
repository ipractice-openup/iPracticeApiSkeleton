using iPractice.Interfaces.DataAccess;
using iPractice.Interfaces.Repositories;
using System;

namespace iPractice.DataAccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IAvailabilityRepository Availability => GetRepository<IAvailabilityRepository>();
        public IPsychologistRepository Psychologist => GetRepository<IPsychologistRepository>();
        public ITimeSlotRepository TimeSlot => GetRepository<ITimeSlotRepository>();

        protected IRepositoryProvider RepositoryProvider { get; set; }
        private ApplicationDbContext? DbContext { get; set; }

        public UnitOfWork(IRepositoryProvider repositoryProvider, ApplicationDbContext dbContext)
        {
            CreateDbContext(dbContext);

            repositoryProvider.DbContext = dbContext;
            RepositoryProvider = repositoryProvider;
        }

        public void Commit()
        {
            DbContext?.SaveChanges();
        }

        protected void CreateDbContext(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        private T GetRepository<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbContext?.Dispose();
            }
        }
    }
}
