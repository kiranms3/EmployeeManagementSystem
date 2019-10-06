using System;
using System.Collections.Generic;
using EmployeeManagement.Core.Implementation;
using EmployeeManagement.Data.Interfaces;
using EmployeeManagement.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EmployeeManagement.Core.Tests
{
    [TestClass]
    public class EmployeeManagerTest
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;

        public EmployeeManagerTest()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();

        }
        [TestMethod]
        public async System.Threading.Tasks.Task GetEmployees_ReturnsDataAsync()
        {
            List<Employee> employeeList = new List<Employee>
            {
               new Employee
               {
                   Id=1,
                   GivenName="Kiran",
                   SurName="Kumar",
                   DateOfBirth= new DateTime(1990,3,3)

               }
            };

            _mockEmployeeRepository.Setup(x => x.GetList()).ReturnsAsync(employeeList);

            var employeeManager = new EmployeeManager(_mockEmployeeRepository.Object);

            var result = await employeeManager.GetList();

            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result[0].Id == 1);

        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetEmployees_ReturnsNoData()
        {
            List<Employee> employeeList = null;

            _mockEmployeeRepository.Setup(x => x.GetList()).ReturnsAsync(employeeList);

            var employeeManager = new EmployeeManager(_mockEmployeeRepository.Object);

            var result = await employeeManager.GetList();

            Assert.IsNull(result);           

        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetEmployee_ReturnsDataAsync()
        {
            Employee employee = new Employee
            { 
                   Id=1,
                   GivenName="Kiran",
                   SurName="Kumar",
                   DateOfBirth= new DateTime(1990,3,3)

              
            };

            _mockEmployeeRepository.Setup(x => x.Get(1)).ReturnsAsync(employee);

            var employeeManager = new EmployeeManager(_mockEmployeeRepository.Object);

            var result = await employeeManager.Get(1);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id == 1);

        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetEmployee_ReturnsNoData()
        {
           Employee employee = null;

            _mockEmployeeRepository.Setup(x => x.Get(1)).ReturnsAsync(employee);

            var employeeManager = new EmployeeManager(_mockEmployeeRepository.Object);

            var result = await employeeManager.Get(1);

            Assert.IsNull(result);

        }

        [TestMethod]
        public async System.Threading.Tasks.Task DeleteEmployee_Success()
        {


            _mockEmployeeRepository.Setup(x => x.Delete(1)).ReturnsAsync(true);

            var employeeManager = new EmployeeManager(_mockEmployeeRepository.Object);

            var result = await employeeManager.Delete(1);

            Assert.IsTrue(result);

        }

        [TestMethod]
        public async System.Threading.Tasks.Task DeleteEmployee_Failure()
        {


            _mockEmployeeRepository.Setup(x => x.Delete(1)).ReturnsAsync(false);

            var employeeManager = new EmployeeManager(_mockEmployeeRepository.Object);

            var result = await employeeManager.Delete(1);

            Assert.IsTrue(!result);

        }

        [TestMethod]
        public async System.Threading.Tasks.Task AddEmployee_Success()
        {
            Employee employeeToBeAdded = new Employee
            {
                GivenName = "Kiran",
                SurName = "Kumar",
                DateOfBirth = new DateTime(1990, 3, 3)

            };


            _mockEmployeeRepository.Setup(x => x.Add(employeeToBeAdded)).ReturnsAsync(true);

            var employeeManager = new EmployeeManager(_mockEmployeeRepository.Object);

            var result = await employeeManager.Add(employeeToBeAdded);

            Assert.IsTrue(result);

        }

        [TestMethod]
        public async System.Threading.Tasks.Task AddEmployee_Failure()
        {
            Employee employeeToBeAdded = new Employee
            {
                GivenName = "Kiran",
                SurName = "Kumar",
                DateOfBirth = new DateTime(1990, 3, 3)

            };


            _mockEmployeeRepository.Setup(x => x.Add(employeeToBeAdded)).ReturnsAsync(false);

            var employeeManager = new EmployeeManager(_mockEmployeeRepository.Object);

            var result = await employeeManager.Add(employeeToBeAdded);

            Assert.IsTrue(!result);

        }

        [TestMethod]
        public async System.Threading.Tasks.Task UpdateEmployee_Success()
        {
            Employee employeeToBeAdded = new Employee
            {
                Id=1,
                GivenName = "Kiran",
                SurName = "Kumar",
                DateOfBirth = new DateTime(1990, 3, 3)

            };


            _mockEmployeeRepository.Setup(x => x.Update(1,employeeToBeAdded)).ReturnsAsync(true);

            var employeeManager = new EmployeeManager(_mockEmployeeRepository.Object);

            var result = await employeeManager.Update(1,employeeToBeAdded);

            Assert.IsTrue(result);

        }

        [TestMethod]
        public async System.Threading.Tasks.Task UpdateEmployee_Failure()
        {
            Employee employeeToBeAdded = new Employee
            {
                Id=1,
                GivenName = "Kiran",
                SurName = "Kumar",
                DateOfBirth = new DateTime(1990, 3, 3)

            };


            _mockEmployeeRepository.Setup(x => x.Update(1,employeeToBeAdded)).ReturnsAsync(false);

            var employeeManager = new EmployeeManager(_mockEmployeeRepository.Object);

            var result = await employeeManager.Update(1,employeeToBeAdded);

            Assert.IsTrue(!result);

        }
                          
    }
}
