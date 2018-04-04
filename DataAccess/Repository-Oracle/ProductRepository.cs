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

namespace DataAccess.RepositoryOracle
{
    public class ProductRepository : IProductRepository
    {
        IConfiguration _configuration;
        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int Add(Product product)
        {
            var connectionString = this.GetConnection();
            int count = 0;
            using (var con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = @"INSERT INTO Product(Name, Model, Price) VALUES (:Name, :Model, :Price)";
                    count = con.Execute(query, new { product.Name, product.Model,product.Price });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return count;
            }
        }

        public int AddWithProcedure(Product product)
        {
            var connectionString = this.GetConnection();
            int count = 0;
            using (var con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();
                    count = con.Execute("add_product", new { p_name = product.Name, p_model=product.Model, p_price = product.Price },commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return count;
            }
           
        }

        public int DeleteProdcut(int id)
        {
            var connectionString = this.GetConnection();
            var count = 0;

            using (var con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "DELETE FROM Product WHERE Id = :Id" ;
                    count = con.Execute(query, new { Id=id });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return count;
            }
        }

        public int EditProduct(Product product)
        {
            var connectionString = this.GetConnection();
            var count = 0;

            using (var con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = @"UPDATE Product SET Name = :Name, Model = :Model, Price = :Price WHERE Id = :Id";
                    count = con.Execute(query, new { product.Name,product.Model,product.Price, product.Id });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return count;
            }
        }

        public List<Product> GetList()
        {
            var connectionString = this.GetConnection();
            List<Product> products = new List<Product>();

            using (var con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM Product";
                    products = con.Query<Product>(query).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return products;
            }
        }

        public Product GetProduct(int id)
        {
            var connectionString = this.GetConnection();
            Product product = new Product();

            using (var con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM Product WHERE Id =:Id";
                    product = con.Query<Product>(query, new { Id=id }).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return product;
            }
        }

        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            return connection;
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
                    data = con.Query<DepartmentSalaryDTO>("departments_biggest_salary", commandType: CommandType.StoredProcedure).ToList();
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
    }
}
