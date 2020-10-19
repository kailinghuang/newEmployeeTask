using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeTasks.Models;
using EmployeeTasksTest;
using System.Threading.Tasks;
using EmployeeTaskAPI.Controllers;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading;

namespace EmployeTaskTest
{
    [TestClass]
    public class EmployeeControllerTest
    {
        public DBContext dbContext;
        [TestInitialize]
        public void Initialize()
        {
            dbContext = (DBContext)DBhelper.DBContextInstance;

        }

        [TestMethod]
        public async Task VerifyIndex()
        {
            var controller = new EmployeeController(dbContext);
            var result = await controller.GetEmployee();
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public async Task VerifyGetEmployeeModel()
        {
            var controller = new EmployeeController(dbContext);
            var result = await controller.GetEmployeeModel(1);
            var badresult = await controller.GetEmployeeModel(2);
            Assert.IsNotNull(result);
            Assert.IsNotNull(badresult);
        }


        [TestMethod]
        public async Task VerifyPutEmployeeModel()
        {
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            foreach (var entity in dbContext.ChangeTracker.Entries())
            {
                entity.State = EntityState.Detached;
            }
            DateTime thisDate = new DateTime(2008, 3, 15);
            EmployeeModel employee = new EmployeeModel { employeeId = 1, firstName = "Jobs1", lastName = "Steven", hiredDate = thisDate };
            var controller = new EmployeeController(dbContext);
            var result = await controller.PutEmployeeModel(2,employee);
           
            var okResult = await controller.PutEmployeeModel(1, employee);
            Assert.IsNotNull(result);
            Assert.IsNotNull(okResult);

        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateConcurrencyException))]
        public async Task VerifyPutEmployeeModel_Exception()
        {
            DateTime thisDate = new DateTime(2008, 3, 15);
            EmployeeModel employee = new EmployeeModel { employeeId = 3, firstName = "Jobs", lastName = "Steven", hiredDate = thisDate };

            var contextMock = new Mock<DBContext>();
            contextMock.Setup(m => m.SaveChangesAsync((It.IsAny<CancellationToken>()))).Throws(new DbUpdateConcurrencyException());
            contextMock.Setup(m => m.SetModified(It.IsAny<EmployeeModel>()));
            var controller = new EmployeeController(contextMock.Object);

            var result = (RedirectToActionResult)await controller.PutEmployeeModel(3, employee);
            
        }


        [TestMethod]
        public async Task VerifyPostEmployeeModel()
        {
            DateTime thisDate = new DateTime(2008, 3, 15);
            EmployeeModel employee = new EmployeeModel { employeeId = 5, firstName = "xiao", lastName = "Steven", hiredDate = thisDate };
            var controller = new EmployeeController(dbContext);
            var result = await controller.PostEmployeeModel(employee);
            Assert.IsNotNull(result);
          
        }

        [TestMethod]
        public async Task VerifyDeleteEmployeeModel()
        {
            var controller = new EmployeeController(dbContext);
            var result = await controller.DeleteEmployeeModel(3);
            Assert.IsNotNull(result);

            var nullresult = await controller.DeleteEmployeeModel(18);
            Assert.IsNotNull(result);
        }


    }
}
