using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entity;
using Oracle.ManagedDataAccess;
using Microsoft.Extensions.Configuration;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using DataAccess.IRepository;
using System.Data;
using NetCoreOracle.DTO;
using DataAccess.Helper;

namespace DataAccess.RepositoryOracle
{
    public class EmployeeRepository : IEmployeeRepository
    {
        IConfiguration _configuration;
        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

       

        public List<Employee> GetList()
        {
            var connectionString = this.GetConnection();
            List<Employee> employees = new List<Employee>();

            using (var con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();
                    //using Result Cache
                    var query = "SELECT /*+ result_cache */  * FROM Employees";
                    employees = con.Query<Employee>(query).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return employees;
            }
        }

        public List<DepartmentSalaryDTO> GetDepartmentSalary()
        {
            var connectionString = this.GetConnection();
            List<DepartmentSalaryDTO> data = new List<DepartmentSalaryDTO>();
            using (var con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();
                    //https://medium.com/@CodeCoo/call-oracle-store-procedure-with-dapper-c-b4176f636e11
                    //https://gist.github.com/vijaysg/3096151
                    var p = new OracleDynamicParameters();
                    p.Add("CURSOR_", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    data = con.Query<DepartmentSalaryDTO>("departments_biggest_salary", param: p, commandType: CommandType.StoredProcedure).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return data;
            }
        }

        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            return connection;
        }
    }
}
