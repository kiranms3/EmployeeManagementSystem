using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.Core.Interfaces;
using EmployeeManagement.Data.Interfaces;
using EmployeeManagement.Entities;

namespace EmployeeManagement.Core.Implementation
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository _employeeRespository;
        public EmployeeManager(IEmployeeRepository employeeRespository)
        {
            _employeeRespository = employeeRespository;

        }
        public async Task<bool> Add(Employee employeeToBeAdded)
        {
            return await _employeeRespository.Add(employeeToBeAdded);
        }

        public async Task<bool> Delete(int id)
        {
            return await _employeeRespository.Delete(id);
        }

        public async Task<Employee> Get(int id)
        {
            return await _employeeRespository.Get(id);
        }

        public async Task<IList<Employee>> GetList()
        {
            return await _employeeRespository.GetList();
        }

        public async Task<bool> Update(int id, Employee employeeToUpdate)
        {
            return await _employeeRespository.Update(id, employeeToUpdate);
        }
    }
}
