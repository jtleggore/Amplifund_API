using Amplifund_API_Example.Contexts;
using Amplifund_API_Example.Controllers;
using Amplifund_API_Example.Endpoints;
using Amplifund_API_Example.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Amplifund_API_Example.Tests
{
    [TestClass]
    public class EndpointTests
    {
        //needs changed to one in DB before tests, could be done better
        const string guid = "A320966C-FA19-483A-B6A8-0606C0068FD8";

        static TestTableController? controller;

        [ClassInitialize]
        public static void Setup(TestContext _)
        {
            controller = new TestTableController(
                new SQLRepo<TestEntity>(
                    new SqlDataContext(
                        new DbContextOptions<SqlDataContext>())), 
                new Validators.GenericValidator<TestEntity>());
        }

        private static IEnumerable<object[]> GetNewGuid()
        {
            yield return new object[] { new Guid(guid) };
        }

        private static IEnumerable<object[]> GetTestGuidsBad()
        {
            yield return new object[] { new Guid() };
        }

        private static IEnumerable<object[]> GetTestTables()
        {
            yield return new object[] { 
                new TestEntity{
                    TestString = "Guys",
                    TestInt = 75,
                    TestFloat = 85.7d,
                    TestBit = true
                }
            };
        }

        [TestMethod]
        public async Task Test_GetAll_Success()
        {
            var results = await controller.GetAll();
            Assert.IsTrue(results.Count() > 0);
        }

        [TestMethod]
        [DynamicData(nameof(GetNewGuid), DynamicDataSourceType.Method)]
        public async Task Test_GetById_Success(Guid id)
        {
            var results = await controller.Get(id);
            Assert.IsTrue(!results.TestString.IsNullOrEmpty());
        }

        [TestMethod]
        [DynamicData(nameof(GetTestGuidsBad), DynamicDataSourceType.Method)]
        public async Task Test_GetById_Fail(Guid id)
        {
            //var results = await controller.Get(id);
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () => await controller.Get(id));
        }

        [TestMethod]
        [DynamicData(nameof(GetTestTables), DynamicDataSourceType.Method)]
        public async Task Test_Add_Success(TestEntity p)
        {
            var results = await controller.Post(p);
            Assert.IsTrue(!results.TestString.IsNullOrEmpty());
        }

        [TestMethod]
        [DynamicData(nameof(GetTestTables), DynamicDataSourceType.Method)]
        public async Task Test_Update_Success(TestEntity p)
        {
            p.Id = new Guid(guid);
            p.TestInt += 55;
            var results = await controller.Put(p);
            Assert.IsTrue(!results.TestString.IsNullOrEmpty());
        }

        [TestMethod]
        [DynamicData(nameof(GetTestTables), DynamicDataSourceType.Method)]
        public async Task Test_Update_Fail(TestEntity p)
        {
            //not found ID
            p.Id = new Guid(guid);
            //var results = await controller.Put(p);
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () => await controller.Put(p));
        }

        [TestMethod]
        [DynamicData(nameof(GetNewGuid), DynamicDataSourceType.Method)]
        public async Task Test_Delete_Success(Guid id)
        {
            var results = await controller.Delete(id);
            Assert.IsTrue(!results.TestString.IsNullOrEmpty());
        }

        [TestMethod]
        [DynamicData(nameof(GetTestGuidsBad), DynamicDataSourceType.Method)]
        public async Task Test_Delete_Fail(Guid id)
        {
            //var results = await controller.Delete(id);
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(async() => await controller.Delete(id));
        }
    }
}