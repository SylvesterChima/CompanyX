using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTest.Models
{
    public class Student
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Comment { get; set; }
    }
}
