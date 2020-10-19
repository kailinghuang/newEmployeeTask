using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace EmployeeTasks.Models
{
    public class MyContext:DbContext
    {
        public MyContext(DbContextOptions<MyContext> options):base(options)
        {

        }
        public MyContext() : base()
        {

        }
        public DbSet<TaskModel> Task { get; set; }

        public DbSet<EmployeeModel> Employee { set; get; }
    }
}
