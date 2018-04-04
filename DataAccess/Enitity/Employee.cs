using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity
{
    public class Employee
    {
        [Range(0, 999999)]
        public int EMPLOYEE_ID { get; set; }
        [StringLength(20)]
        public string FIRST_NAME { get; set; }

        [Required]
        [StringLength(25)]
        public string LAST_NAME { get; set; }

        [Required]
        [StringLength(25)]
        public string EMAIL { get; set; }

        [StringLength(20)]
        public string PHONE_NUMBER { get; set; }

        [Required]
        public DateTime HIRE_DATE { get; set; }

        [Required]
        [StringLength(20)]
        public string JOB_ID { get; set; }

        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid Target Salary; Maximum Two Decimal Points.")]
        [Range(0, 999999.99, ErrorMessage = "Invalid Salary; Max 9 digits")]
        public decimal? SALARY { get; set; }

        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid Target  Commision; Maximum Two Decimal Points.")]
        [Range(0, 0.99, ErrorMessage = "Invalid  Commision; Max 2 digits")]
        public decimal? COMMISSION_PCT { get; set; }

        [Range(0, 999999)]
        public int? MANAGER_ID { get; set; }

        [Range(0,9999)]
        public int? DEPARTMENT_ID { get; set; }
    }
}
