using System;

namespace EmployeeManagement.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string SurName { get; set; }
        public  string ManagerName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
