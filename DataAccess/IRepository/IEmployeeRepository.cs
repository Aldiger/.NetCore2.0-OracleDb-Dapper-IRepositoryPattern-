using DataAccess.Entity;
using NetCoreOracle.DTO;
using System.Collections.Generic;

namespace DataAccess.IRepository
{
    public interface IEmployeeRepository
    {
        List<Employee> GetList();
        List<DepartmentSalaryDTO> GetDepartmentSalary();
    }
}
