using Whs.Persistence;

namespace Whs.Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly WhsDbContext Context;

        public TestCommandBase()
        {
            Context = WhsContextFactory.Create();
        }

        public void Dispose()
        {
            WhsContextFactory.Destroy(Context);
        }
    }
}
