using EmployeeManagement.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Interfaces
{
    public interface IEmployeeManager
    {
        Task<Employee> Get(int id);
        Task<IList<Employee>> GetList();
        Task<bool> Update(int id, Employee employeeToUpdate);
        Task<bool> Delete(int id);
        Task<bool> Add(Employee employeeToBeAdded);
    }
}
