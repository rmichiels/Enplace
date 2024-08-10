using Enplace.Service;
using Enplace.Service.Database;
using Enplace.Service.Entities;

namespace Enplace.Tests.Database.SQL_Lite
{
    [TestClass]
    public class UserTests
    {
        private readonly GenericRepository<User> _repository;
        private readonly Guid _guid = new();
        public UserTests()
        {
            _repository = new(new LiteDBContext());
        }

        [TestMethod]
        public async Task RepositoryShould_BeAbleTo_AddNewUsers()
        {
            User user = new() { Name = _guid.ToString() };
            var result = await _repository.Add(user);

            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task RepositoryShould_BeAbleTo_DeleteNewUsers()
        {
            User user = new() { Name = _guid.ToString() };
            var result = await _repository.Add(user);
            Assert.IsNull(result);

            result = await _repository.Delete(user);
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task RepositoryShould_BeAbleTo_UpdateNewUsers()
        {
            User user = new() { Name = _guid.ToString() };
            var result = await _repository.Add(user);
            Assert.IsNull(result);
            user.Name = new Guid().ToString();
            result = await _repository.Update(user);
            Assert.IsNull(result);
        }
    }
}
