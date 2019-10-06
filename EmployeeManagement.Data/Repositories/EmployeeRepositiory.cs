using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using EmployeeManagement.Data.Interfaces;
using EmployeeManagement.Entities;

namespace EmployeeManagement.Data.Repositories
{
    public class EmployeeRepositiory : IEmployeeRepository
    {       
        public async Task<bool> Add(Employee employeeToBeAdded)
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["employeeDatabase"].ConnectionString;

            using (var _dbConnection = new SqlConnection(_connectionString))
            {               
                _dbConnection.Open();
                try
                {
                    var command = _dbConnection.CreateCommand();
                    command.CommandText = string.Format("insert into dbo.employees values('{0}','{1}','{2}','{3}')", employeeToBeAdded.GivenName,
                        employeeToBeAdded.SurName,employeeToBeAdded.ManagerName,employeeToBeAdded.DateOfBirth.Date);

                    var result = await command.ExecuteNonQueryAsync();
                    if (result >= 1)
                    {
                        
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    _dbConnection.Close();
                    throw ex;
                }
            }
        }

        public async Task<bool> Delete(int id)
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["employeeDatabase"].ConnectionString;

            using (var _dbConnection = new SqlConnection(_connectionString))
            {
                _dbConnection.Open();
                try
                {
                    var command = _dbConnection.CreateCommand();
                    command.CommandText = string.Format("Delete from dbo.employees where id={0}", id);

                    var result = await command.ExecuteNonQueryAsync();
                    if (result >= 1)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    _dbConnection.Close();
                    throw ex;
                }
            }
        }

        public async Task<Employee> Get(int id)
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["employeeDatabase"].ConnectionString;

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                try
                {
                    var command = conn.CreateCommand();
                    command.CommandText = string.Format("select * from dbo.employees where id={0}", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            var employee = new Employee
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                GivenName = reader["GivenName"].ToString(),
                                SurName = reader["SurName"].ToString(),
                                DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                                ManagerName = reader["ManagerName"].ToString()

                            };
                            conn.Close();
                            return employee;
                        }
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    conn.Close();
                    throw ex;
                }
            }
        }

        public async Task<IList<Employee>> GetList()
        {
            var employeeList = new List<Employee>();
            string _connectionString = ConfigurationManager.ConnectionStrings["employeeDatabase"].ConnectionString;

            using (var _dbConnection = new SqlConnection(_connectionString))
            {
                _dbConnection.Open();
                try
                {
                    var command = _dbConnection.CreateCommand();
                    command.CommandText = string.Format("select * from dbo.employees");
                    
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            var employee = new Employee
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                GivenName = reader["GivenName"].ToString(),
                                SurName = reader["SurName"].ToString(),
                                DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                                ManagerName = reader["ManagerName"].ToString()

                            };

                            employeeList.Add(employee);
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    _dbConnection.Close();
                    throw ex;
                }
                _dbConnection.Close();
                return employeeList;
            }
        }

        public async Task<bool> Update(int id, Employee employeeToUpdate)
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["employeeDatabase"].ConnectionString;

            using (var _dbConnection = new SqlConnection(_connectionString))
            {
                _dbConnection.Open();
                try
                {
                    var command = _dbConnection.CreateCommand();
                    command.CommandText = string.Format("Update dbo.employees set GivenName='{0}',SurName='{1}',ManagerName='{2}',DateOfBirth='{3}' where id={4}", employeeToUpdate.GivenName,
                        employeeToUpdate.SurName, employeeToUpdate.ManagerName, employeeToUpdate.DateOfBirth.Date,id);

                    var result = await command.ExecuteNonQueryAsync();
                    if (result >= 1)
                    {

                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    _dbConnection.Close();
                    throw ex;
                }
            }
        }
    }
}
