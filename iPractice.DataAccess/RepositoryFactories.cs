using iPractice.Application.Contract.Dtos;
using iPractice.DataAccess.Repositories;
using iPractice.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace iPractice.DataAccess
{
    public class RepositoryFactories
    {
        private readonly IDictionary<Type, Func<DbContext, object>> _repositoryFactories;

        public RepositoryFactories(ILoggerFactory loggerFactory)
        {
            _repositoryFactories = GetRepositoryFactories(loggerFactory);
        }

        public RepositoryFactories(IDictionary<Type, Func<DbContext, object>> factories)
        {
            _repositoryFactories = factories;
        }

        public Func<DbContext, object>? GetRepositoryFactory<T>()
        {
            _repositoryFactories.TryGetValue(typeof(T), out var factory);
            return factory;
        }

        public Func<DbContext, object> GetRepositoryFactoryForEntityType<T>(ILoggerFactory loggerFactory) where T : class
        {
            return GetRepositoryFactory<T>() ?? DefaultEntityRepositoryFactory<T>(loggerFactory.CreateLogger<T>());
        }

        protected virtual Func<DbContext, object> DefaultEntityRepositoryFactory<T>(ILogger<T> logger) where T : class
        {
            return dbContext => new GenericRepository<T>(dbContext, logger);
        }

        private IDictionary<Type, Func<DbContext, object>> GetRepositoryFactories(ILoggerFactory loggerFactory)
        {
            return new Dictionary<Type, Func<DbContext, object>>
            {
                   {typeof(IAvailabilityRepository), dbContext => new AvailabilityRepository((ApplicationDbContext)dbContext,loggerFactory.CreateLogger<AvailabilityDto>())},
                   {typeof(IPsychologistRepository), dbContext => new PsychologistRepository((ApplicationDbContext)dbContext, loggerFactory.CreateLogger<PsychologistDto>())},
                   {typeof(ITimeSlotRepository), dbContext => new TimeSlotRepository((ApplicationDbContext)dbContext, loggerFactory.CreateLogger<TimeSlotDto>())}
                };
        }
    }
}
