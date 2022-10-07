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
        public async Task<List<Product>> GetAllProductsAsync()
        {
            List<Product> products = new List<Product>();
            string connectionString = "Server=tcp:mono-supermarket-sql.database.windows.net,1433;Initial Catalog=supermarket;Persist Security Info=False;User ID=admin123;Password=adm!n123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);

            string queryProducts = "select * from Products;";
            SqlCommand getProducts = new SqlCommand(queryProducts, connection);
            try
            {
                connection.Open();
                SqlDataReader productReader = await getProducts.ExecuteReaderAsync();
                while (await productReader.ReadAsync())
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
        public async Task<Product> GetProductAsync(string name)
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
                SqlDataReader employeeReader = await getProduct.ExecuteReaderAsync();
                while (await employeeReader.ReadAsync())
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
        public async Task<bool> PostProductAsync(Product product)
        {
            string connectionString = "Server=tcp:mono-supermarket-sql.database.windows.net,1433;Initial Catalog=supermarket;Persist Security Info=False;User ID=admin123;Password=adm!n123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            string queryInsertProduct = $"insert into Products values(default,\'{product.Name}\',{product.Price}, \'{product.Mark}\');";
            SqlCommand insertProduct = new SqlCommand(queryInsertProduct, connection);
            try
            {
                connection.Open();
                SqlDataAdapter productInserter = new SqlDataAdapter(insertProduct);
                productInserter.Fill(new System.Data.DataSet("Products"));
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
        public async Task< bool> EditProductAsync(string name, Product product)
        {
            string connectionString = "Server=tcp:mono-supermarket-sql.database.windows.net,1433;Initial Catalog=supermarket;Persist Security Info=False;User ID=admin123;Password=adm!n123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            string queryEditProduct = $"update Products set Name=\'{product.Name}\', Price={product.Price}, Mark=\'{product.Mark}\' where Name = \'{name}\';";
            SqlCommand editProduct = new SqlCommand(queryEditProduct, connection);
            try
            {
                Product oldProduct = await GetProductAsync(name);
                connection.Open();
                SqlDataAdapter productEditer = new SqlDataAdapter(editProduct);
                productEditer.Fill(new System.Data.DataSet("Products"));
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;

        }
        public async Task<bool> DeleteProductAsync(string name)
        {
            string connectionString = "Server=tcp:mono-supermarket-sql.database.windows.net,1433;Initial Catalog=supermarket;Persist Security Info=False;User ID=admin123;Password=adm!n123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            string queryDeleteProduct = $"delete Products where Name = \'{name}\'";
            SqlCommand deleteProduct = new SqlCommand(queryDeleteProduct, connection);
            try
            {
                Product oldProduct = await GetProductAsync(name);
                connection.Open();
                SqlDataAdapter productDeleter = new SqlDataAdapter(deleteProduct);
                productDeleter.Fill(new System.Data.DataSet("Products"));
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
    }
}
