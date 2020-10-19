using EmployeeTasks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeTasksTest
{
    public static class DBhelper
    {
        
        private static DBContext myContext = null;
        // Creates an syn object.
        private static readonly object SynObject = new object();


        public static ServiceCollection Services=null;
        public static ServiceProvider ServiceProvider=null;
        public static DbContext DBContextInstance
        {
            
            get
            {
                // Double-Checked Locking
                if (null == myContext)
                {
                    lock (SynObject)
                    {
                        if (null == myContext)
                        {
                            Services = new ServiceCollection();

                            Services.AddDbContext<DBContext>(opt => opt.UseInMemoryDatabase(databaseName: "InMemoryDb"),
                                ServiceLifetime.Scoped,
                                ServiceLifetime.Scoped);

                            ServiceProvider = Services.BuildServiceProvider();
                            myContext = ServiceProvider.GetService<DBContext>();
                            DateTime thisDate = new DateTime(2008, 3, 15);
                            myContext.Employee.Add(new EmployeeModel { employeeId = 1, firstName = "Tim", lastName = "Cook", hiredDate = thisDate });
                            myContext.Employee.Add(new EmployeeModel { employeeId = 3, firstName = "Bill", lastName = "Gates", hiredDate = thisDate });
                            myContext.Task.Add(new TaskModel { employeeId = 1, taskId = 1, taskName = "test", startTime = thisDate, dueTime = thisDate });
                            myContext.Task.Add(new TaskModel { employeeId = 1, taskId = 3, taskName = "rest", startTime = thisDate, dueTime = thisDate });
                            myContext.SaveChanges();

                        }
                    }
                }
                return myContext;
            }
        }






    }
    
}
