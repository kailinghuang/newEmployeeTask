using EmployeeTaskAPI;
using EmployeeTasks.Models;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTaskAPIIntegrationTest
{
    [TestClass]
    public class EmployeeTaskAPIIntegrationTest
    {
        
        private readonly DBContext _context;
        HttpClient client;
      
        public EmployeeTaskAPIIntegrationTest()
        {
        
            var builder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<StartupTest>();
            TestServer testServer = new TestServer(builder);

            _context = testServer.Host.Services.GetService(typeof(DBContext)) as DBContext;
            client = testServer.CreateClient();
            

        }

       

        [TestMethod]
        public async Task TaskCRUD()
        {
            DateTime thisDate = new DateTime(2008, 3, 15);
            TaskModel task = new TaskModel { employeeId = 1, taskId = 1, taskName = "eat", startTime = thisDate, dueTime = thisDate };
              
            var taskContent = new StringContent(JsonConvert.SerializeObject(task), UnicodeEncoding.UTF8, "application/json");

            //Create
            client.DefaultRequestHeaders.Clear();
            HttpResponseMessage ResPost = await client.PostAsync("api/Task", taskContent);

            //Get
            client.DefaultRequestHeaders.Clear();
            var response = await client.GetAsync("/api/Task/1");
            response.EnsureSuccessStatusCode();
            Assert.IsNotNull(response);
            var responseString = await response.Content.ReadAsStringAsync();
            var taskGet = JsonConvert.DeserializeObject<TaskModel>(responseString);
            Assert.IsTrue(taskGet.taskName == "eat");
            

            //Update
            client.DefaultRequestHeaders.Clear();
            TaskModel taskUpdate = new TaskModel { employeeId = 1, taskId = 1, taskName = "sleep", startTime = thisDate, dueTime = thisDate };
            var stringContent = new StringContent(JsonConvert.SerializeObject(taskUpdate), UnicodeEncoding.UTF8, "application/json");
            await client.PutAsync($"api/task/1", stringContent);
            var taskindb = await _context.Task.FindAsync(1);
            Assert.AreEqual(taskindb.taskName, taskUpdate.taskName);


            //Delete
            client.DefaultRequestHeaders.Clear();
            var delRsponse = await client.DeleteAsync($"api/task/1");
            delRsponse.EnsureSuccessStatusCode();
            var delRsponseString = await delRsponse.Content.ReadAsStringAsync();
            var deleteItemReturn = JsonConvert.DeserializeObject<TaskModel>(delRsponseString);
            Console.WriteLine(deleteItemReturn.taskName);
            Assert.IsTrue(deleteItemReturn.taskName == taskUpdate.taskName);

        }
        [TestMethod]
        public async Task EmployeeCRUD()
        {
            DateTime thisDate = new DateTime(2008, 3, 15);
            EmployeeModel employee = new EmployeeModel { employeeId = 1, firstName="Tim", lastName="Cook", hiredDate=thisDate };

            var employeeContent = new StringContent(JsonConvert.SerializeObject(employee), UnicodeEncoding.UTF8, "application/json");

            //Create
            client.DefaultRequestHeaders.Clear();
            HttpResponseMessage ResPost = await client.PostAsync("api/Employee", employeeContent);

            //Get
            client.DefaultRequestHeaders.Clear();
            var response = await client.GetAsync("/api/Employee/1");
            response.EnsureSuccessStatusCode();
            Assert.IsNotNull(response);
            var responseString = await response.Content.ReadAsStringAsync();
            var employeeGet = JsonConvert.DeserializeObject<EmployeeTaskDetail>(responseString);
            Assert.IsTrue(employeeGet.firstName == employee.firstName);


            //Update
            client.DefaultRequestHeaders.Clear();
            EmployeeModel employeeUpdate = new EmployeeModel { employeeId = 1, firstName = "Shit", lastName = "Cook", hiredDate = thisDate };
            var stringContent = new StringContent(JsonConvert.SerializeObject(employeeUpdate), UnicodeEncoding.UTF8, "application/json");
            await client.PutAsync($"api/Employee/1", stringContent);
            var employeeInDB = await _context.Employee.FindAsync(1);
            Assert.AreEqual(employeeInDB.firstName, employeeUpdate.firstName);


            //Delete
            client.DefaultRequestHeaders.Clear();
            var delRsponse = await client.DeleteAsync($"api/Employee/1");
            delRsponse.EnsureSuccessStatusCode();
            var delRsponseString = await delRsponse.Content.ReadAsStringAsync();
            var deleteItemReturn = JsonConvert.DeserializeObject<EmployeeModel>(delRsponseString);
            Assert.IsTrue(deleteItemReturn.firstName == employeeUpdate.firstName);

        }
    }
}
