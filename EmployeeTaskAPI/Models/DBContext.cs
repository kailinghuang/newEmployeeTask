using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace EmployeeTasks.Models
{
    public class DBContext:DbContext
    {
        public DBContext(DbContextOptions<DBContext> options):base(options)
        {

        }
        public DBContext() : base()
        {

        }
        public DbSet<TaskModel> Task { get; set; }

        public DbSet<EmployeeModel> Employee { set; get; }

        public virtual void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
    }
}
