using EmployeeManagement.Api.Models;
using EmployeeManagement.Core.Interfaces;
using EmployeeManagement.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace EmployeeManagement.Api.Controllers
{

    public class EmployeeController : ApiController
    {
        private readonly IEmployeeManager _iEmployeeManager;


        public EmployeeController(IEmployeeManager employeeManager)
        {
            _iEmployeeManager = employeeManager;
        }


        [HttpGet]
        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                var employees = await _iEmployeeManager.GetList();
                if (employees == null)
                {
                    HttpResponseMessage notFoundResponse = Request.CreateResponse(HttpStatusCode.NotFound);
                    notFoundResponse.Content = new StringContent("No Employees found", Encoding.Unicode);

                    //Return the error response.
                    return notFoundResponse;
                }
                var employeeList = new List<EmployeeModel>();
                foreach (var employee in employees)
                {
                    employeeList.Add(new EmployeeModel
                    {
                        Id = employee.Id,
                        GivenName = employee.GivenName,
                        SurName = employee.SurName,
                        DateOfBirth = employee.DateOfBirth,
                        ManagerName = employee.ManagerName

                    });
                }

                HttpResponseMessage okResponse = Request.CreateResponse(HttpStatusCode.OK, employeeList);

                //Return the success response.
                return okResponse;
            }
            catch
            {
                HttpResponseMessage internalServerErrorResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error occured while getting employee List");
                return internalServerErrorResponse;
            }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Get(int id)
        {
            if (id <= 0)
            {
                HttpResponseMessage badRequestResponse = Request.CreateResponse(HttpStatusCode.BadRequest);
                badRequestResponse.Content = new StringContent("Invalid Employee Id", Encoding.Unicode);

                //Return the error response.
                return badRequestResponse;
            }
            try
            {
                var employee = await _iEmployeeManager.Get(id);
                if (employee == null)
                {
                    HttpResponseMessage notFoundResponse = Request.CreateResponse(HttpStatusCode.NotFound);
                    notFoundResponse.Content = new StringContent("Employee Id not found", Encoding.Unicode);

                    //Return the error response.
                    return notFoundResponse;
                }
                var employeeModel = new EmployeeModel
                {
                    Id = employee.Id,
                    GivenName = employee.GivenName

                };

                HttpResponseMessage okResponse = Request.CreateResponse(HttpStatusCode.OK, employeeModel);

                //Return the success response.
                return okResponse;
            }
            catch
            {
                HttpResponseMessage internalServerErrorResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error occured while getting employee");
                return internalServerErrorResponse;
            }
        }




        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            if (id <= 0)
            {
                HttpResponseMessage badRequestResponse = Request.CreateResponse(HttpStatusCode.BadRequest);
                badRequestResponse.Content = new StringContent("Invalid Employee Id", Encoding.Unicode);

                //Return the error response.
                return badRequestResponse;
            }
            try
            {
                var employee = await _iEmployeeManager.Get(id);
                if (employee == null)
                {
                    HttpResponseMessage notFoundResponse = Request.CreateResponse(HttpStatusCode.NotFound);
                    notFoundResponse.Content = new StringContent("Employee Id not found", Encoding.Unicode);

                    //Return the error response.
                    return notFoundResponse;
                }

                var isDeleted = await _iEmployeeManager.Delete(id);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NoContent);
                response.Content = new StringContent("Deleted Successfully", Encoding.Unicode);
                //Return the success response.
                return response;
            }
            catch
            {
                HttpResponseMessage internalServerErrorResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error occured while deleting employee");
                return internalServerErrorResponse;
            }

        }
        [HttpPost]
        public async Task<HttpResponseMessage> Post(EmployeeModel employeeToBeAdded)
        {
            if (employeeToBeAdded == null)
            {
                HttpResponseMessage badRequestResponse = Request.CreateResponse(HttpStatusCode.BadRequest);
                badRequestResponse.Content = new StringContent("Employee details not provided", Encoding.Unicode);

                //Return the error response.
                return badRequestResponse;
            }
            else if (string.IsNullOrEmpty(employeeToBeAdded.GivenName))
            {
                HttpResponseMessage badRequestResponse = Request.CreateResponse(HttpStatusCode.BadRequest);
                badRequestResponse.Content = new StringContent("Employee Name can not be empty", Encoding.Unicode);

                //Return the error response.
                return badRequestResponse;
            }
            if (employeeToBeAdded.DateOfBirth.Equals(DateTime.MinValue))
            {
                HttpResponseMessage badRequestResponse = Request.CreateResponse(HttpStatusCode.BadRequest);
                badRequestResponse.Content = new StringContent("Invalid Date of birth", Encoding.Unicode);

                //Return the error response.
                return badRequestResponse;
            }
            try
            {
                var employee = await _iEmployeeManager.Get(employeeToBeAdded.Id);
                if (employee != null)
                {
                    HttpResponseMessage badRequestResponse = Request.CreateResponse(HttpStatusCode.BadRequest);
                    badRequestResponse.Content = new StringContent("An employee with Same ID already exists", Encoding.Unicode);

                    //Return the error response.
                    return badRequestResponse;
                }
                var employeeEntity = new Employee
                {
                    Id = employeeToBeAdded.Id,
                    GivenName = employeeToBeAdded.GivenName,
                    SurName = employeeToBeAdded.SurName,
                    DateOfBirth = employeeToBeAdded.DateOfBirth,
                    ManagerName = employeeToBeAdded.ManagerName
                };
                var employeeAdded = await _iEmployeeManager.Add(employeeEntity);

                HttpResponseMessage createdResponse = Request.CreateResponse(HttpStatusCode.Created);
                createdResponse.Content = new StringContent("Created Successfully", Encoding.Unicode);

                
                return createdResponse;
            }
            catch
            {
                HttpResponseMessage internalServerErrorResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error occured while adding new employee");
                return internalServerErrorResponse;
            }

        }

        [HttpPut]
        public async Task<HttpResponseMessage> Put([FromUri] int id, EmployeeModel employeeToBeUpdated)
        {
            if (id <= 0)
            {
                HttpResponseMessage badRequestResponse = Request.CreateResponse(HttpStatusCode.BadRequest);
                badRequestResponse.Content = new StringContent("Invalid Employee Id", Encoding.Unicode);

                //Return the error response.
                return badRequestResponse;
            }
            if (employeeToBeUpdated == null)
            {
                HttpResponseMessage badRequestResponse = Request.CreateResponse(HttpStatusCode.BadRequest);
                badRequestResponse.Content = new StringContent("Employee details not provided", Encoding.Unicode);

                //Return the error response.
                return badRequestResponse;
            }
            else if (!employeeToBeUpdated.Id.Equals(id))
            {
                HttpResponseMessage badRequestResponse = Request.CreateResponse(HttpStatusCode.BadRequest);
                badRequestResponse.Content = new StringContent("Employee Id provided in body does not match id in request URI", Encoding.Unicode);

                //Return the error response.
                return badRequestResponse;
            }
            else if (string.IsNullOrEmpty(employeeToBeUpdated.GivenName))
            {
                HttpResponseMessage badRequestResponse = Request.CreateResponse(HttpStatusCode.BadRequest);
                badRequestResponse.Content = new StringContent("Employee given name is mandatory", Encoding.Unicode);

                //Return the error response.
                return badRequestResponse;
            }
            if (employeeToBeUpdated.DateOfBirth.Equals(DateTime.MinValue))
            {
                HttpResponseMessage badRequestResponse = Request.CreateResponse(HttpStatusCode.BadRequest);
                badRequestResponse.Content = new StringContent("Invalid Date of birth", Encoding.Unicode);

                //Return the error response.
                return badRequestResponse;
            }
            try
            {
                var employee = await _iEmployeeManager.Get(id);
                if (employee == null)
                {
                    HttpResponseMessage badRequestResponse = Request.CreateResponse(HttpStatusCode.NotFound);
                    badRequestResponse.Content = new StringContent(string.Format("Employee with Id {0} does not exists", id), Encoding.Unicode);

                    //Return the error response.
                    return badRequestResponse;
                }
                var employeeEntity = new Employee
                {
                    Id = employeeToBeUpdated.Id,
                    GivenName = employeeToBeUpdated.GivenName,
                    SurName = employeeToBeUpdated.SurName,
                    DateOfBirth = employeeToBeUpdated.DateOfBirth,
                    ManagerName = employeeToBeUpdated.ManagerName
                };
                var employeeUpdated = await _iEmployeeManager.Update(id, employeeEntity);

                HttpResponseMessage createdResponse = Request.CreateResponse(HttpStatusCode.OK);
                createdResponse.Content = new StringContent("Updated Successfully", Encoding.Unicode);

                
                return createdResponse;
            }
            catch
            {
                HttpResponseMessage internalServerErrorResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error occured while updating employee");
                return internalServerErrorResponse;
            }

        }

    }
}
