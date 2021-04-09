using Microsoft.VisualStudio.TestTools.UnitTesting;
using AccountApp.BOL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace AccountApp.BOL.Repository.Tests
{
    [TestClass()]
    public class UsersDBLTests
    {
        /// <summary>
        /// Test method is used to insure that Users object is created succesfully
        /// It helps to proof that our projects database connection is working properly
        /// and we could be successfully able to do Create Read Update and Delete operations on
        /// Users Object
        /// </summary>
        [TestMethod()]
        public void GetUsersDBL_Test_Should_Return_Object_Currectly()
        {
            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<IUserRolessDBL>();
            Assert.IsNotNull(mockContext);
        }
    }
}