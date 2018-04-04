using DataAccess.Entity;
using System.Collections.Generic;

namespace DataAccess.IRepository
{
    public interface IProductRepository
    {
        int Add(Product product);

        List<Product> GetList();

        Product GetProduct(int id);

        int EditProduct(Product product);

        int DeleteProdcut(int id);

        int AddWithProcedure(Product product);
    }
}
