using System;

namespace EmployeeManagement.Api.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string SurName { get; set; }
        public string ManagerName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}