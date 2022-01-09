using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RESTApp.Model
{
    public class Employee

    {

        public int EmployeeId { get; set; }

        [Required]
        [StringLength(100,MinimumLength =3)]
        public String FirstName { get; set; }

        [Required]
        public String LastName { get; set; }

        [Required, EmailAddress]
       
        public String Email { get; set; }

        public DateTime DatofBirth { get; set; }

        public Gender Gender { get; set; }

        public int DepartmentId { get; set; }

        public String PhotoPath { get; set; }

        public Department Department { get; set; }



    }
}
