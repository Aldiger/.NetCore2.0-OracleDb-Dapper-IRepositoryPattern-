using System;
using System.ComponentModel.DataAnnotations;

namespace NetCoreOracle.DTO
{
    public class DepartmentSalaryDTO
    {
        public string DEPARTMENT_NAME { get; set; }
        public decimal? Highest_salary { get; set; }

    }
}
