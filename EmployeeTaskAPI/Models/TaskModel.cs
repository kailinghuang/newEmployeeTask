using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTasks.Models
{
    public class TaskModel
    {
        [DisplayName("Task Id")]
        [Key]
        [Required]
        public int taskId { set; get; }
        
        [DisplayName("Task Name")]
        [Required]
        public string taskName { set; get; }
        
        [DisplayName("Start Time")]
        [Required]
        public DateTime startTime { set; get; }

        [DisplayName("Due Time")]
        [Required]
        public DateTime dueTime { set; get; }

        [DisplayName("Employee ID")]
        public int employeeId { set; get; }


    }
}
