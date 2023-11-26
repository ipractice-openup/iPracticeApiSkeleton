using NUnit.Framework;
using iPractice.Interfaces.DataAccess;
using Moq;
using AutoMapper;
using iPractice.Application.Mapping;

namespace iPractice.UnitTests.Tests
{
    public class Base
    {
        protected IMapper Mapper;
        protected Mock<IUnitOfWork> UnitOfWork;

        [SetUp]
        public virtual void Setup()
        {
            if (Mapper == null)
            {
                var mapperConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MapperProfile());
                });
                Mapper = mapperConfig.CreateMapper();
            }

            UnitOfWork = new Mock<IUnitOfWork>();
        }
    }
}
