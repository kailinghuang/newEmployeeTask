using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTasks.Models
{
    public class EmployeeModel
    {
        [DisplayName("Employee Id")]
        [Key]
        public int employeeId { set; get; }

        [DisplayName("First Name")]
        [Required]
        public string firstName { set; get; }
        
        [DisplayName("Last Name")]
        [Required]
        public string lastName { set; get; }

        [DisplayName("Hired Date")]
        [Required]
        public DateTime hiredDate { set; get; }
    }
}
