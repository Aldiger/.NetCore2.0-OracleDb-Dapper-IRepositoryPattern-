using DataAccess.Entity;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;
using NetCoreOracle.Model;

namespace NetCoreOracle.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _repository;
        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var data = _repository.GetList();
           
            return Ok(data);
        }
        [HttpGet("[action]")]
        public IActionResult GetDepartmentsSalary()
        {
            var data = _repository.GetDepartmentSalary();
            return Ok(data);
        }
    }
}
