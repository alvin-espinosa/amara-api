using Azure.Core;
using Data;
using Domain.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace API.Test
{
    [TestClass]
    public class InMemoryTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            using RentingContext context = SetupSQLiteMemoryContextWithOpenConnection();

            var customer = new Customer { };

            context.Customers.Add(customer);

            Assert.AreEqual(EntityState.Added, context.Entry(customer).State);
        }

        private static RentingContext SetupSQLiteMemoryContextWithOpenConnection()
        {
            var builder = new DbContextOptionsBuilder<RentingContext>().UseSqlite("Filename=:memory:");
            var context = new RentingContext(builder.Options);

            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            return context;
        }
    }
}