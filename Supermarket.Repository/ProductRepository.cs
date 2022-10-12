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
                    Product currentProduct = new Product();
                    currentProduct.Id = Guid.Parse(productReader[0].ToString());
                    currentProduct.Name = productReader[1].ToString();
                    currentProduct.Price = decimal.Parse(productReader[2].ToString());
                    currentProduct.Mark = productReader[3].ToString();
                    products.Add(currentProduct);
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
        public async Task<List<Product>> GetProductByNameAsync(string name)
        {
            List<Product> products = new List<Product>();
            SqlConnection connection = new SqlConnection(WebConfigurationManager.AppSettings["connectionString"]);
            //get employee from db
            string queryProduct = $"select * from Products where Name like @Name +\'%\'";

            SqlCommand getProduct = new SqlCommand(queryProduct, connection);
            getProduct.Parameters.AddWithValue("@Name", name);
            try
            {
                connection.Open();
                SqlDataReader productReader = getProduct.ExecuteReader();
                while (await productReader.ReadAsync())
                {
                    Product currentProduct = new Product();
                    currentProduct.Id = Guid.Parse(productReader[0].ToString());
                    currentProduct.Name = productReader[1].ToString();
                    currentProduct.Price = decimal.Parse(productReader[2].ToString());
                    currentProduct.Mark = productReader[3].ToString();
                    products.Add(currentProduct);
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
        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            Product product = new Product();
            SqlConnection connection = new SqlConnection(WebConfigurationManager.AppSettings["connectionString"]);
            //get employee from db
            string queryProduct = $"select * from Products where ProductID = @Id";

            SqlCommand getProduct = new SqlCommand(queryProduct, connection);
            getProduct.Parameters.AddWithValue("@Id", id);
            try
            {
                connection.Open();
                SqlDataReader productReader = getProduct.ExecuteReader();
                while (await productReader.ReadAsync())
                {
                    Product currentProduct = new Product();
                    currentProduct.Id = Guid.Parse(productReader[0].ToString());
                    currentProduct.Name = productReader[1].ToString();
                    currentProduct.Price = decimal.Parse(productReader[2].ToString());
                    currentProduct.Mark = productReader[3].ToString();
                    product = currentProduct;
                }
                productReader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return product;
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
                await insertProduct.ExecuteNonQueryAsync();
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
                await editProduct.ExecuteNonQueryAsync();
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
            string queryDeleteProduct = $"delete Products where Name = @Name";
            SqlCommand deleteProduct = new SqlCommand(queryDeleteProduct, connection);
            deleteProduct.Parameters.AddWithValue("@Name", name);
            try
            {
                connection.Open();
                await deleteProduct.ExecuteNonQueryAsync();
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
