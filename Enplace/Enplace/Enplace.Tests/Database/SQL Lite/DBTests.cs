using Enplace.Service;
using Enplace.Service.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Enplace.Tests.Database.SQL_Lite
{
    [TestClass]
    public class DBTests
    {
        readonly IServiceCollection _services = new ServiceCollection();
        public DBTests()
        {

        }
        [TestMethod]
        public void DbContextShould_GenerateDB_IfNotPresent()
        {
            _services.AddEnplaceServices();
            ServiceProvider provider = _services.BuildServiceProvider();
            LiteDBContext? liteDBContext = provider.GetService<LiteDBContext>();
            Assert.IsNotNull(liteDBContext);
            liteDBContext.Database.EnsureDeleted();
            Assert.IsTrue(liteDBContext.Database.EnsureCreated());
        }
    }
}
