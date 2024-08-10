using Enplace.Service;
using Enplace.Service.Database;
using Enplace.Service.Entities;

namespace Enplace.Tests.Database.Context_Management
{
    [TestClass]
    public class ContextManagerTests
    {
        private readonly ContextManager _contextManager;
        public ContextManagerTests()
        {
            _contextManager = new([new SSDBContext(), new LiteDBContext()]);
        }

        [TestMethod]
        public async Task ContextManagerShould_BeAbleTo_WriteDataToBothContexts()
        {
            User user = new()
            {
                Name = $"ContextManagerTest_{DateTime.UtcNow}",
            };

            var exception = await _contextManager.Add(user);
            Assert.IsNull(exception);
            var liteRepo = new GenericRepository<User>(new LiteDBContext());
            var sqlRepo = new GenericRepository<User>(new SSDBContext());
            var liteUser = await liteRepo.Get(user.Name);
            var sqlUser = await sqlRepo.Get(user.Name);
            Assert.IsNotNull(sqlUser);
            Assert.IsNotNull(liteUser);
        }
    }
}
