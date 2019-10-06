using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EmployeeManagement.Api.Controllers;
using EmployeeManagement.Api.Models;
using EmployeeManagement.Core.Interfaces;
using EmployeeManagement.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EmployeeManagement.Api.Tests.Controllers
{
    [TestClass]
    public class EmployeeControllerTest
    {
        private readonly Mock<IEmployeeManager> _mockEmployeeManager;

        public EmployeeControllerTest()
        {
            _mockEmployeeManager = new Mock<IEmployeeManager>();
        }

        [TestMethod]
        public async Task GetEmployees_Returns_OK()
        {
            List<Employee> employeeList = new List<Employee>
            {
                new Employee
                {
                    Id=1,
                    GivenName="Kiran",
                    SurName="Subrahmanya",
                    DateOfBirth = new DateTime(1990,3,3)
                }

            };
            _mockEmployeeManager.Setup(x => x.GetList()).ReturnsAsync(employeeList);

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Get();

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.IsNotNull(result);
            List<EmployeeModel> employeeModelList;
            Assert.IsTrue(result.TryGetContentValue<List<EmployeeModel>>(out employeeModelList));
        }
        [TestMethod]
        public async Task GetEmployees_Returns_NotFound()
        {
            List<Employee> employeeList = null;

            _mockEmployeeManager.Setup(x => x.GetList()).ReturnsAsync(employeeList);

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Get();

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.NotFound);            
        }

        [TestMethod]
        public async Task GetEmployees_Returns_InternalServerError()
        {  
            _mockEmployeeManager.Setup(x => x.GetList()).Throws<Exception>();

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Get();

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.InternalServerError);
        }


        [TestMethod]
        public async Task GetEmployee_Returns_OK()
        {
            var employee = new Employee
            {
                Id=1,
                GivenName="Kiran",
                SurName="Subrahmanya"
                

            };
            _mockEmployeeManager.Setup(x => x.Get(1)).ReturnsAsync(employee);

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Get(1);

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.IsNotNull(result);
            // Assert the result  
            EmployeeModel employeeFromResponse;
            Assert.IsTrue(result.TryGetContentValue<EmployeeModel>(out employeeFromResponse));

        }

        [TestMethod]
        public async Task GetEmployee_Returns_NotFound()
        {
            Employee employee = null;
            _mockEmployeeManager.Setup(x => x.Get(1)).ReturnsAsync(employee);

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Get(1);

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.NotFound);
            
        }

        [TestMethod]
        public async Task GetEmployee_Returns_InternalServerError()
        {
            _mockEmployeeManager.Setup(x => x.Get(1)).Throws<Exception>();

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Get(1);

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.InternalServerError);
        }

        [TestMethod]
        public async Task GetEmployee_Returns_BadRequest()
        {
            Employee employee = null;
            _mockEmployeeManager.Setup(x => x.Get(0)).ReturnsAsync(employee);

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Get(0);

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.BadRequest);

        }


        [TestMethod]
        public async Task DeleteEmployee_Returns_NotContent()
        {

            var employee = new Employee
            {
                Id = 1,
                GivenName = "Kiran",
                SurName = "Subrahmanya"


            };
            _mockEmployeeManager.Setup(x => x.Get(1)).ReturnsAsync(employee);

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Delete(1);

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.NoContent);

        }

        [TestMethod]
        public async Task DeleteEmployee_Returns_NotFound()
        {
            Employee employee = null;
            _mockEmployeeManager.Setup(x => x.Get(1)).ReturnsAsync(employee);

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Delete(1);

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.NotFound);

        }

        [TestMethod]
        public async Task DeleteEmployee_Returns_InternalServerError()
        {
            Employee employee = new Employee
            {
                Id=1,
                GivenName="Kiran",
                SurName="Kumar"

            };
            _mockEmployeeManager.Setup(x => x.Get(1)).ReturnsAsync(employee);
            _mockEmployeeManager.Setup(x => x.Delete(1)).Throws<Exception>();

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Delete(1);

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.InternalServerError);
        }

        [TestMethod]
        public async Task DeleteEmployee_Returns_BadRequest()
        {            

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Delete(0);

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.BadRequest);

        }


        [TestMethod]
        public async Task PostEmployee_Returns_Created()
        {
            Employee employee = new Employee
            {
                Id = 4,
                GivenName = "Kishor",
                SurName = "Kumar",
                DateOfBirth = new DateTime(1948, 03, 03)
            };

            EmployeeModel employeeModel = new EmployeeModel
            {
                Id = 4,
                GivenName = "Kishor",
                SurName = "Kumar",
                DateOfBirth = new DateTime(1948, 03, 03)
            };
            _mockEmployeeManager.Setup(x => x.Add(employee));

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Post(employeeModel);

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.Created);
            
        }

        [TestMethod]
        public async Task PostEmployee_Returns_BadRequestWhenGivenNameEmpty()
        {           

            EmployeeModel employeeModel = new EmployeeModel
            {              
                
                SurName = "Kumar",
                DateOfBirth = new DateTime(1948, 03, 03)
            };            

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Post(employeeModel);

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.BadRequest);

        }

        [TestMethod]
        public async Task PostEmployee_Returns_BadRequestWhenEmptyRequest()
        {

            EmployeeModel employeeModel = null;

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Post(employeeModel);

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.BadRequest);

        }

        
        [TestMethod]
        public async Task PostEmployee_Returns_BadRequestWhenDateOfBirthInvalid()
        {
            
            EmployeeModel employeeModel = new EmployeeModel
            {
                GivenName = "Kishor",
                DateOfBirth = DateTime.MinValue

            };

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Post(employeeModel);

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task PostEmployee_Returns_BadRequestWhenSameEmployeeExists()
        {
            Employee employee = new Employee
            {
                Id = 4,
                GivenName = "Kishor",
                SurName = "Kumar",
                DateOfBirth = new DateTime(1948, 03, 03)
            };

            EmployeeModel employeeModel = new EmployeeModel
            {
                Id = 4,
                GivenName = "Kishor",
                SurName = "Kumar",
                DateOfBirth = new DateTime(1948, 03, 03)
            };
            _mockEmployeeManager.Setup(x => x.Get(4)).ReturnsAsync(employee);

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Post(employeeModel);

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.BadRequest);

        }

        [TestMethod]
        public async Task PostEmployee_Returns_InternalServerError()
        {
            Employee employee = null;

            EmployeeModel employeeModel = new EmployeeModel
            {
                Id=4,
                GivenName="KIran",
                SurName = "Kumar",
                DateOfBirth = new DateTime(1948, 03, 03)
            };
            _mockEmployeeManager.Setup(x => x.Get(4)).Throws<Exception>();
            _mockEmployeeManager.Setup(x => x.Add(employee)).Throws<Exception>();

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Post(employeeModel);

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.InternalServerError);
        }

        [TestMethod]
        public async Task PutEmployee_Returns_BadRequestWhenDateOfBirthInvalid()
        {

            EmployeeModel employeeModel = new EmployeeModel
            {
                Id=1,
                GivenName = "Kishor",
                DateOfBirth = DateTime.MinValue

            };

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Put(1,employeeModel);

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task PutEmployee_Returns_BadRequestWhenGivenNameNotProvided()
        {

            EmployeeModel employeeModel = new EmployeeModel
            {
                Id = 1,             
                DateOfBirth = new DateTime(1990,3,3),
                SurName="KUmar"

            };

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Put(1, employeeModel);

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task PutEmployee_Returns_BadRequestWhenIdInRouteDoesNotMatchWithEmployeeIdInBody()
        {

            EmployeeModel employeeModel = new EmployeeModel
            {
                Id = 3,
                DateOfBirth = new DateTime(1990, 3, 3),
                SurName = "KUmar",
                GivenName="Kishor"

            };

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Put(1, employeeModel);

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task PutEmployee_Returns_BadRequestWhenRequestIsEmpty()
        {

            EmployeeModel employeeModel = null;

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Put(1, employeeModel);

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task PutEmployee_Returns_BadRequestWhenEmployeeIdLessThanZero()
        {

            EmployeeModel employeeModel = new EmployeeModel
            {
                Id=1,
                GivenName="Kiran",
                SurName="Kumar",
                DateOfBirth=new DateTime(1990,3,3)
            };

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Put(0, employeeModel);

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task PutEmployee_Returns_NotFound()
        {

            Employee employee = null;
            _mockEmployeeManager.Setup(x => x.Get(1)).ReturnsAsync(employee);

            EmployeeModel employeeModel = new EmployeeModel
            {
                Id = 1,
                GivenName = "Kiran",
                SurName = "Kumar",
                DateOfBirth = new DateTime(1990, 3, 3)

            };
            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Put(1, employeeModel);

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task PutEmployee_Returns_OK()
        {
            Employee employee = new Employee
            {
                Id = 4,
                GivenName = "Kishor",
                SurName = "Kumar",
                DateOfBirth = new DateTime(1948, 03, 03)
            };

            EmployeeModel employeeModel = new EmployeeModel
            {
                Id = 4,
                GivenName = "Kishor",
                SurName = "Kumar",
                DateOfBirth = new DateTime(1948, 03, 03)
            };
            _mockEmployeeManager.Setup(x => x.Get(4)).ReturnsAsync(employee);

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Put(4,employeeModel);

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.OK);

        }

        [TestMethod]
        public async Task PutEmployee_Returns_InternalServerError()
        {
            Employee employee = new Employee
            {
                Id = 4,
                GivenName = "Kishor",
                SurName = "Kumar",
                DateOfBirth = new DateTime(1948, 03, 03)
            };

            EmployeeModel employeeModel = new EmployeeModel
            {
                Id = 4,
                GivenName = "Kishor",
                SurName = "Kumar",
                DateOfBirth = new DateTime(1948, 03, 03)
            };
            _mockEmployeeManager.Setup(x => x.Get(4)).Throws<Exception>();
            _mockEmployeeManager.Setup(x => x.Update(4, employee)).Throws<Exception>();

            var employeeController = new EmployeeController(_mockEmployeeManager.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            var result = await employeeController.Put(4,employeeModel);

            //asert if returns ok status code
            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.InternalServerError);
        }

    }
}
