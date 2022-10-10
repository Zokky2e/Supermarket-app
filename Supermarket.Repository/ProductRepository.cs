using Supermarket.Model;
using Supermarket.Model.Common;
using Supermarket.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Supermarket.Repository
{
    public class ProductRepository : IProductRepository
    {
        public async Task<List<Product>> GetAllProductsAsync()
        {
            List<Product> products = new List<Product>();
            SqlConnection connection = new SqlConnection(WebConfigurationManager.AppSettings["connectionString"]);

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
        public async Task<List<Product>> GetProductAsync(string name)
        {
            List<Product> products = new List<Product>();
            SqlConnection connection = new SqlConnection(WebConfigurationManager.AppSettings["connectionString"]);
            //get employee from db
            string queryProduct = $"select * from Products where Name like \'{name}%\'";

            SqlCommand getProduct = new SqlCommand(queryProduct, connection);
            try
            {
                connection.Open();
                SqlDataReader employeeReader = await getProduct.ExecuteReaderAsync();
                while (await employeeReader.ReadAsync())
                {
                    products.Add(new Product(
                         Guid.Parse(employeeReader[0].ToString()),
                         employeeReader[1].ToString(),
                         decimal.Parse(employeeReader[2].ToString()),
                         employeeReader[3].ToString()));
                }
                employeeReader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return products;
        }
        public async Task<bool> PostProductAsync(IProduct product)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.AppSettings["connectionString"]);
            string queryInsertProduct = $"insert into Products values(default,@Name,@Price,@Mark);";
            SqlCommand insertProduct = new SqlCommand(queryInsertProduct, connection);
            insertProduct.Parameters.AddWithValue("@Name", product.Name);
            insertProduct.Parameters.AddWithValue("@Price", product.Price);
            insertProduct.Parameters.AddWithValue("@Mark", product.Mark);
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
        public async Task<bool> EditProductAsync(string name, IProduct product)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.AppSettings["connectionString"]);
            string queryEditProduct = $"update Products set Name=@Name, Price=@Price, Mark=@Mark where Name = @OldName;";
            SqlCommand editProduct = new SqlCommand(queryEditProduct, connection);
            editProduct.Parameters.AddWithValue("@Name", product.Name);
            editProduct.Parameters.AddWithValue("@Price", product.Price);
            editProduct.Parameters.AddWithValue("@Mark", product.Mark);
            editProduct.Parameters.AddWithValue("@OldName", name);
            try
            {
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
            SqlConnection connection = new SqlConnection(WebConfigurationManager.AppSettings["connectionString"]);
            string queryDeleteProduct = $"delete Products where Name = \'{name}\'";
            SqlCommand deleteProduct = new SqlCommand(queryDeleteProduct, connection);
            try
            {
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
