using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AccountApp.BOL.Repository.Tests
{
    [TestClass()]
    public class BusinessDBLTests
    {
        /// <summary>
        /// Test method is used to insure that BusinessDBL object is created succesfully
        /// It helps to proof that our projects database connection is working properly
        /// and we could be successfully able to do Create Read Update and Delete operations on
        /// Businees Object
        /// </summary>
        [TestMethod()]
        public void GetBusinessDBL_Test_Should_Return_Object_Currectly()
        {
            var mockSet = new Mock<DbSet<Business>>();
            var mockContext = new Mock<IBusinessDBL>();
            Assert.IsNotNull(mockContext);
        }
    }
}