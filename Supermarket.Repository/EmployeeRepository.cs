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
        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            List<Employee> employees = new List<Employee>();
            SqlConnection connection = new SqlConnection(WebConfigurationManager.AppSettings["connectionString"]);
            //get employees from db
            string queryEmployees = "select * from Employees;";
            SqlCommand getEmployees = new SqlCommand(queryEmployees, connection);
            try
            {
                connection.Open();
                SqlDataReader employeeReader = await getEmployees.ExecuteReaderAsync();
                while (await employeeReader.ReadAsync())
                {
                    employees.Add(new Employee(
                        Guid.Parse(employeeReader[0].ToString()),
                        employeeReader[1].ToString(),
                        employeeReader[2].ToString(),
                        employeeReader[4].ToString(),
                        employeeReader[3].ToString()));
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

            string queryEmployee = $"select * from Employees where OIB Like \'%{OIB}%\';";
            SqlCommand getEmployees = new SqlCommand(queryEmployee, connection);
            try
            {
                connection.Open();
                SqlDataReader employeeReader = await getEmployees.ExecuteReaderAsync();
                while (await employeeReader.ReadAsync())
                {
                    employees.Add(new Employee(
                         Guid.Parse(employeeReader[0].ToString()),
                         employeeReader[1].ToString(),
                         employeeReader[2].ToString(),
                         employeeReader[4].ToString(),
                         employeeReader[3].ToString()));
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
            string queryInsertEmployee = $"insert into Employees values(default,@FirstName,@LastName, @Address, @OIB);";
            SqlCommand insertEmployee = new SqlCommand(queryInsertEmployee, connection);
            insertEmployee.Parameters.AddWithValue("@FirstName", employee.FirstName);
            insertEmployee.Parameters.AddWithValue("@LastName", employee.LastName);
            insertEmployee.Parameters.AddWithValue("@Address", employee.Address);
            insertEmployee.Parameters.AddWithValue("@OIB", employee.OIB);
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
            string queryEditEmployee = $"update Employees set FirstName=@FirstName, LastName=@LastName, Address=@Address, OIB=@OIB where OIB = @OldOIB;";
            SqlCommand editEmployee = new SqlCommand(queryEditEmployee, connection);
            editEmployee.Parameters.AddWithValue("@FirstName", employee.FirstName);
            editEmployee.Parameters.AddWithValue("@LastName", employee.LastName);
            editEmployee.Parameters.AddWithValue("@Address", employee.Address);
            editEmployee.Parameters.AddWithValue("@OIB", employee.OIB);
            editEmployee.Parameters.AddWithValue("@OldOIB", OIB);
            try
            {
                connection.Open();
                SqlDataAdapter employeeEditor = new SqlDataAdapter(editEmployee);
                employeeEditor.Fill(new System.Data.DataSet("Employees"));
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
            string queryDeleteEmployee = $"delete Employees where OIB = \'{OIB}\'";
            SqlCommand deleteEmployee = new SqlCommand(queryDeleteEmployee, connection);
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
