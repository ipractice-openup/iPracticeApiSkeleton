using iPractice.DataAccess;
using iPractice.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPractice.Domain.Tests
{
    public class InitHelpers
    {
        public static Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var mock = sourceList.AsQueryable().BuildMockDbSet();

            return mock;
        }

        public static Mock<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "FakeConnectionString")
                .Options;

            var mockContext = new Mock<ApplicationDbContext>(options);

            return mockContext;
        }
    }
}