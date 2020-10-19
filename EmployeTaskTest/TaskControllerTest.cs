using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeTasks.Models;
using EmployeeTasksTest;
using System.Threading.Tasks;
using EmployeeTaskAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading;

namespace EmployeTaskAPIUnitTest
{
    [TestClass]
    public class TaskControllerTest
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
            var controller = new TaskController(dbContext);
            var result = await controller.GetTask();
            Assert.IsNotNull(result);

        }
        
        [TestMethod]
        public async Task VerifyGetTaskModel()
        {
            var controller = new TaskController(dbContext);
            var result = await controller.GetTaskModel(1);
            var badresult = await controller.GetTaskModel(2);
            Assert.IsNotNull(result);
            Assert.IsNotNull(badresult);
        }

        [TestMethod]
        public async Task VerifyPutTaskModel()
        {
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            foreach (var entity in dbContext.ChangeTracker.Entries())
            {
                entity.State = EntityState.Detached;
            }
            DateTime thisDate = new DateTime(2008, 3, 15);
            TaskModel task = new TaskModel { employeeId = 1, taskId = 1, taskName = "eat", startTime = thisDate, dueTime=thisDate };
            var controller = new TaskController(dbContext);
            var result = await controller.PutTaskModel(2, task);

            var okResult = await controller.PutTaskModel(1, task);
            Assert.IsNotNull(result);
            Assert.IsNotNull(okResult);

        }

        [TestMethod]
        public async Task VerifyPostTaskModel()
        {
            DateTime thisDate = new DateTime(2008, 3, 15);
            TaskModel task = new TaskModel { employeeId = 1, taskId = 10, taskName = "sleep", startTime = thisDate, dueTime = thisDate };
            var controller = new TaskController(dbContext);
            var result = await controller.PostTaskModel(task);
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public async Task VerifyDeleteTaskModel()
        {
            var controller = new TaskController(dbContext);
            var result = await controller.DeleteTaskModel(3);
            Assert.IsNotNull(result);

            var nullresult = await controller.DeleteTaskModel(18);
            Assert.IsNotNull(result);
        }


    }
}
