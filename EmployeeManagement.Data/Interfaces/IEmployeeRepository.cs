using EmployeeManagement.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> Get(int id);
        Task<IList<Employee>> GetList();
        Task<bool> Update(int id, Employee employeeToUpdate);
        Task<bool> Delete(int id);
        Task<bool> Add(Employee employeeToBeAdded);

    }
}
