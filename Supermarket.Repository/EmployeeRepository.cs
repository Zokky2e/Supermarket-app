using Supermarket.Common;
using Supermarket.Model;
using Supermarket.Model.Common;
using Supermarket.Repository.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Xml.Linq;

namespace Supermarket.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public async Task<List<Employee>> GetAllEmployeesAsync(Paging paging, Sorting sorting, Filtering filtering)
        {
            List<Employee> employees = new List<Employee>();
            SqlConnection connection = new SqlConnection(WebConfigurationManager.AppSettings["connectionString"]);
            StringBuilder queryEmployees = new StringBuilder().Append("select * from Employees");
            SqlCommand getEmployees = new SqlCommand(queryEmployees.ToString(), connection);
            //get employees from db
            //filtering
            if (filtering.Query != ""
                || filtering.HasAddress == true
                || filtering.BornBefore != null
                || filtering.BornAfter != null)
            {
                queryEmployees.AppendLine(" where ");
            }
            if (filtering.Query != "")
            {
                queryEmployees.Append("LastName like @LastName and ");
                getEmployees.Parameters.AddWithValue("@LastName", "%" + filtering.Query + "%");
            }
            if (filtering.HasAddress == true)
            {
                queryEmployees.Append("Address = '' and ");
            }
            if (filtering.BornBefore != null)
            {
                queryEmployees.Append("Birthday <= @BornBefore ");
                getEmployees.Parameters.AddWithValue("@BornBefore", filtering.BornBefore);
            }
            if (filtering.BornAfter != null)
            {
                queryEmployees.AppendLine("and Birthday >= @BornAfter");
                getEmployees.Parameters.AddWithValue("@BornAfter", filtering.BornAfter);
            }
            //sorting
            queryEmployees.AppendLine($"Order By {sorting.SortBy} {sorting.SortOrder}");
            //paging
            queryEmployees.AppendLine("Offset @PageNumber rows");
            queryEmployees.AppendLine("FETCH NEXT @RowsOfPage ROWS ONLY");
            getEmployees.Parameters.AddWithValue("@PageNumber", ((paging.PageNumber - 1) * paging.PageSize)).ToString();
            getEmployees.Parameters.AddWithValue("@RowsOfPage", paging.PageSize);
            getEmployees.CommandText = queryEmployees.ToString();
            try
            {
                connection.Open();
                SqlDataReader employeeReader = await getEmployees.ExecuteReaderAsync();
                while (await employeeReader.ReadAsync())
                {
                    Employee currentEmployee = new Employee();
                    currentEmployee.Id = Guid.Parse(employeeReader[0].ToString());
                    currentEmployee.FirstName = employeeReader[1].ToString();
                    currentEmployee.LastName = employeeReader[2].ToString();
                    currentEmployee.OIB = employeeReader[4].ToString();
                    currentEmployee.Address = employeeReader[3].ToString();
                    currentEmployee.Birthday = DateTime.Parse(employeeReader[5].ToString());
                    employees.Add(currentEmployee);
                }
                employeeReader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return employees;
        }
        public async Task<List<Employee>> GetEmployeeAsync(string OIB)
        {
            List<Employee> employees = new List<Employee>();
            SqlConnection connection = new SqlConnection(WebConfigurationManager.AppSettings["connectionString"]);

            string queryEmployee = $"select * from Employees where OIB Like @OIB;";

            SqlCommand getEmployees = new SqlCommand(queryEmployee, connection);
            getEmployees.Parameters.AddWithValue("@OIB", "%"+OIB+"%");
            try
            {
                connection.Open();
                SqlDataReader employeeReader = await getEmployees.ExecuteReaderAsync();
                while (await employeeReader.ReadAsync())
                {
                    Employee currentEmployee = new Employee();
                    currentEmployee.Id = Guid.Parse(employeeReader[0].ToString());
                    currentEmployee.FirstName = employeeReader[1].ToString();
                    currentEmployee.LastName = employeeReader[2].ToString();
                    currentEmployee.OIB = employeeReader[4].ToString();
                    currentEmployee.Address = employeeReader[3].ToString();
                    currentEmployee.Birthday = DateTime.Parse(employeeReader[5].ToString());
                    employees.Add(currentEmployee);
                }
                employeeReader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return employees;
        }
        public async Task<bool> PostEmployeeAsync(IEmployee employee)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.AppSettings["connectionString"]);
            string queryInsertEmployee = $"insert into Employees values(default,@FirstName,@LastName, @Address, @OIB, @Birthday);";
            SqlCommand insertEmployee = new SqlCommand(queryInsertEmployee, connection);
            insertEmployee.Parameters.AddWithValue("@FirstName", employee.FirstName);
            insertEmployee.Parameters.AddWithValue("@LastName", employee.LastName);
            insertEmployee.Parameters.AddWithValue("@Address", employee.Address);
            insertEmployee.Parameters.AddWithValue("@OIB", employee.OIB);
            insertEmployee.Parameters.AddWithValue("@Birthday", employee.Birthday);
            try
            {
                connection.Open();
                SqlDataAdapter employeeInserter = new SqlDataAdapter(insertEmployee);
                employeeInserter.Fill(new System.Data.DataSet("Employees"));
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
        public async Task<bool> EditEmployeeAsync(string OIB, IEmployee employee)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.AppSettings["connectionString"]);
            string queryEditEmployee = $"update Employees set FirstName=@FirstName, LastName=@LastName, Address=@Address, OIB=@OIB, Birthday = @Birthday where OIB = @OldOIB;";
            SqlCommand editEmployee = new SqlCommand(queryEditEmployee, connection);
            editEmployee.Parameters.AddWithValue("@FirstName", employee.FirstName);
            editEmployee.Parameters.AddWithValue("@LastName", employee.LastName);
            editEmployee.Parameters.AddWithValue("@Address", employee.Address);
            editEmployee.Parameters.AddWithValue("@OIB", employee.OIB);
            editEmployee.Parameters.AddWithValue("@Birthday", employee.Birthday);
            editEmployee.Parameters.AddWithValue("@OldOIB", OIB);
            try
            {
                connection.Open();
                await editEmployee.ExecuteNonQueryAsync();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
        public async Task<bool> DeleteEmployeeAsync(string OIB)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.AppSettings["connectionString"]);
            string queryDeleteEmployee = $"delete Employees where OIB = @OIB";
            SqlCommand deleteEmployee = new SqlCommand(queryDeleteEmployee, connection);
            deleteEmployee.Parameters.AddWithValue("@OIB", OIB);
            try
            {
                connection.Open();
                SqlDataAdapter employeeDeleter = new SqlDataAdapter(deleteEmployee);
                employeeDeleter.Fill(new System.Data.DataSet("Employees"));
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
