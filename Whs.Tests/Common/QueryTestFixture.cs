using AutoMapper;
using Whs.Application.Common.Mappings;
using Whs.Application.Interfaces;
using Whs.Persistence;

namespace Whs.Tests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public WhsDbContext Context;
        public IMapper Mapper;

        public QueryTestFixture()
        {
            Context = WhsContextFactory.Create();

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(typeof(IWhsDbContext).Assembly));
            });
            Mapper = mapperConfiguration.CreateMapper();
        }

        public void Dispose()
        {
            WhsContextFactory.Destroy(Context);
        }

        [CollectionDefinition("QueryCollection")]
        public class QueryCollection : IClassFixture<QueryTestFixture> { }
    }
}
