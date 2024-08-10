using Enplace.Service;
using Enplace.Service.Contracts;
using Enplace.Service.Database;
using Enplace.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void RepositoryShould_BeAbleTo_AddNewUsers()
        {
            User user = new() { Name = _guid.ToString() };
            var result = _repository.Add(user);

            Assert.IsTrue(result.Item1);
        }
        [TestMethod]
        public void RepositoryShould_BeAbleTo_DeleteNewUsers()
        {
            User user = new() { Name = _guid.ToString() };
            var result = _repository.Add(user);
            Assert.IsTrue(result.Item1);

            result = _repository.Delete(user);
            Assert.IsTrue(result.Item1);
        }

        [TestMethod]
        public void RepositoryShould_BeAbleTo_UpdateNewUsers()
        {
            User user = new() { Name = _guid.ToString() };
            var result = _repository.Add(user);
            Assert.IsTrue(result.Item1);
            user.Name = new Guid().ToString();
            result = _repository.Update(user);
            Assert.IsTrue(result.Item1);
        }
    }
}
