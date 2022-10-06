using Supermarket.Model;
using Supermarket.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Repository
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            string connectionString = "Server=tcp:mono-supermarket-sql.database.windows.net,1433;Initial Catalog=supermarket;Persist Security Info=False;User ID=admin123;Password=adm!n123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);

            //get products from db
            string queryProducts = "select * from Products;";
            SqlCommand getProducts = new SqlCommand(queryProducts, connection);
            try
            {
                connection.Open();
                SqlDataReader productReader = getProducts.ExecuteReader();
                while (productReader.Read())
                {
                    products.Add(new Product(
                        Guid.Parse(productReader[0].ToString()),
                        productReader[1].ToString(),
                        decimal.Parse(productReader[2].ToString()),
                        productReader[3].ToString()));
                }
                productReader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return products;
        }
        public Product GetProduct(string name)
        {
            Product product = new Product();
            string connectionString = "Server=tcp:mono-supermarket-sql.database.windows.net,1433;Initial Catalog=supermarket;Persist Security Info=False;User ID=admin123;Password=adm!n123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            //get employee from db
            string queryProduct = $"select * from Products where Name = \'{name}\'";

            SqlCommand getProduct = new SqlCommand(queryProduct, connection);
            try
            {
                connection.Open();
                SqlDataReader employeeReader = getProduct.ExecuteReader();
                while (employeeReader.Read())
                {
                    product = new Product(
                         Guid.Parse(employeeReader[0].ToString()),
                         employeeReader[1].ToString(),
                         decimal.Parse(employeeReader[2].ToString()),
                         employeeReader[3].ToString());
                }
                employeeReader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return product;
        }
    }
}
