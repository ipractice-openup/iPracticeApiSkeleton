using iPractice.Interfaces.DataAccess;
using iPractice.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace iPractice.DataAccess
{
    public class RepositoryProvider : IRepositoryProvider
    {
        public DbContext? DbContext { get; set; }
        protected Dictionary<Type, object> Repositories { get; private set; }
        private readonly RepositoryFactories _repositoryFactories;
        private readonly ILoggerFactory _loggerFactory;

        public RepositoryProvider(RepositoryFactories repositoryFactories, ILoggerFactory loggerFactory)
        {
            _repositoryFactories = repositoryFactories;
            Repositories = new Dictionary<Type, object>();
            _loggerFactory = loggerFactory;
        }

        public virtual T GetRepository<T>(Func<DbContext, object>? factory = null) where T : class
        {
            Repositories.TryGetValue(typeof(T), out var repoObj);

            if (repoObj != null)
            {
                return (T)repoObj;
            }

            return MakeRepository<T>(factory, DbContext);
        }

        public IGenericRepository<T> GetRepositoryForEntityType<T>() where T : class
        {
            return GetRepository<IGenericRepository<T>>(
                _repositoryFactories.GetRepositoryFactoryForEntityType<T>(_loggerFactory));
        }

        public void SetRepository<T>(T repository)
        {
            if (repository != null)
            {
                Repositories[typeof(T)] = repository;
            }
        }

        protected virtual T MakeRepository<T>(Func<DbContext, object>? factory, DbContext? dbContext)
        {
            if (dbContext == null)
            {
                throw new NotImplementedException("DBContext is null!");
            }

            var f = (factory ?? _repositoryFactories.GetRepositoryFactory<T>()) ?? throw new NotImplementedException("No factory for repository type, " + typeof(T).FullName);
            var repo = (T)f(dbContext);
            Repositories[typeof(T)] = repo;
            return repo;
        }
    }
}
