using Supermarket.Model.Common;
using Supermarket.Model;
using Supermarket.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Xml.Linq;

namespace Supermarket.Repository
{
    public class EmployeeProductRepository : IEmployeeProductRepository
    {
        public async Task<List<EmployeeProduct>> GetAllEmployeeProductsAsync()
        {
            List<EmployeeProduct> employeeProducts = new List<EmployeeProduct>();
            SqlConnection connection = new SqlConnection(WebConfigurationManager.AppSettings["connectionString"]); ;
            string queryEmployeeProducts = "select * from EmployeeProduct";
            SqlCommand getEmployeeProducts = new SqlCommand(queryEmployeeProducts, connection);
            try
            {
                connection.Open();
                SqlDataReader employeeProductReader = await getEmployeeProducts.ExecuteReaderAsync();
                while (await employeeProductReader.ReadAsync())
                {
                    EmployeeProduct currentEmployeeProduct = new EmployeeProduct(
                        Guid.Parse(employeeProductReader[0].ToString()),
                        Guid.Parse(employeeProductReader[1].ToString()),
                        Guid.Parse(employeeProductReader[2].ToString()));
                    employeeProducts.Add(currentEmployeeProduct);
                }
                employeeProductReader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return employeeProducts;
        }
        public async Task<List<EmployeeProduct>> GetEmployeeProductAsync(Guid id)
        {
            List<EmployeeProduct> employeeProducts = new List<EmployeeProduct>();
            SqlConnection connection = new SqlConnection(WebConfigurationManager.AppSettings["connectionString"]); ;
            string queryEmployeeProduct = "select * from EmployeeProduct where SupermarketId like @Id";

            SqlCommand getEmplyeeProduct = new SqlCommand(queryEmployeeProduct, connection);
            getEmplyeeProduct.Parameters.AddWithValue("@Id", "%" + id + "%");
            try
            {
                connection.Open();
                SqlDataReader employeeProductReader = await getEmplyeeProduct.ExecuteReaderAsync();
                while (await employeeProductReader.ReadAsync())
                {
                    EmployeeProduct currentEmployeeProduct = new EmployeeProduct(
                        Guid.Parse(employeeProductReader[0].ToString()),
                        Guid.Parse(employeeProductReader[1].ToString()),
                        Guid.Parse(employeeProductReader[2].ToString()));
                    employeeProducts.Add(currentEmployeeProduct);
                }
                employeeProductReader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return employeeProducts;
        }
        public async Task<bool> PostEmployeeProductAsync(EmployeeProduct employeeProduct)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.AppSettings["connectionString"]); ;
            string queryEmployeeProduct = "insert into EmployeeProduct values(default, @EmployeeId, @ProductId);";

            SqlCommand insertProduct = new SqlCommand(queryEmployeeProduct, connection);
            insertProduct.Parameters.AddWithValue("@EmployeeId", employeeProduct.EmployeeId);
            insertProduct.Parameters.AddWithValue("@ProductId", employeeProduct.ProductId);
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
        public async Task<bool> EditEmployeeProductAsync(Guid id, EmployeeProduct employeeProduct)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.AppSettings["connectionString"]); ;
            string queryEmployeeProduct = "update EmployeeProduct" +
                " set EmployeeId = @EmployeeId, ProductId = @ProductId" +
                " where SupermarketId = @Id;";
            SqlCommand editEmployeeProduct = new SqlCommand(queryEmployeeProduct, connection);
            editEmployeeProduct.Parameters.AddWithValue("@Id", id);
            editEmployeeProduct.Parameters.AddWithValue("@EmployeeId", employeeProduct.EmployeeId);
            editEmployeeProduct.Parameters.AddWithValue("@ProductId", employeeProduct.ProductId);
            try
            {
                connection.Open();
                await editEmployeeProduct.ExecuteNonQueryAsync();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;

        }
        public async Task<bool> DeleteEmployeeProductAsync(Guid id)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.AppSettings["connectionString"]); ;
            string queryEmployeeProduct = "delete EmployeeProduct " +
                "where SupermarketId = @Id;";

            SqlCommand deleteEmployeeProduct = new SqlCommand(queryEmployeeProduct, connection);
            deleteEmployeeProduct.Parameters.AddWithValue("@Id", id);
            try
            {
                connection.Open();
                await deleteEmployeeProduct.ExecuteNonQueryAsync();
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
