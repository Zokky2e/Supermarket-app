﻿using Supermarket.Model;
using Supermarket.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Supermarket.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            string connectionString = "Server=tcp:mono-supermarket-sql.database.windows.net,1433;Initial Catalog=supermarket;Persist Security Info=False;User ID=admin123;Password=adm!n123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            //get employees from db
            string queryEmployees = "select * from Employees;";
            SqlCommand getEmployees = new SqlCommand(queryEmployees, connection);
            try
            {
                connection.Open();
                SqlDataReader employeeReader = getEmployees.ExecuteReader();
                while (employeeReader.Read())
                {
                    employees.Add(new Employee(
                        Guid.Parse(employeeReader[0].ToString()),
                        employeeReader[1].ToString(),
                        employeeReader[2].ToString(),
                        employeeReader[3].ToString(),
                        employeeReader[4].ToString()));
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
        public Employee GetEmployee(string OIB)
        {
            Employee employee = new Employee();
            string connectionString = "Server=tcp:mono-supermarket-sql.database.windows.net,1433;Initial Catalog=supermarket;Persist Security Info=False;User ID=admin123;Password=adm!n123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            //get employee from db
            string queryEmployee = $"select * from Employees where OIB = \'{OIB}\';";
            SqlCommand getEmployees = new SqlCommand(queryEmployee, connection);
            try
            {
                connection.Open();
                SqlDataReader employeeReader = getEmployees.ExecuteReader();
                while (employeeReader.Read())
                {
                    employee = new Employee(
                         Guid.Parse(employeeReader[0].ToString()),
                         employeeReader[1].ToString(),
                         employeeReader[2].ToString(),
                         employeeReader[3].ToString(),
                         employeeReader[4].ToString());
                }
                employeeReader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return employee;
        }
        public bool PostEmployee(Employee employee)
        {
            string connectionString = "Server=tcp:mono-supermarket-sql.database.windows.net,1433;Initial Catalog=supermarket;Persist Security Info=False;User ID=admin123;Password=adm!n123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            string queryInsertEmployee = $"insert into Employees values(default,\'{employee.FirstName}\',\'{employee.LastName}\', \'{employee.Address}\', \'{employee.OIB}\');";
            SqlCommand insertEmployee = new SqlCommand(queryInsertEmployee, connection);
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
        public bool EditEmployee(string OIB, Employee employee)
        {
            string connectionString = "Server=tcp:mono-supermarket-sql.database.windows.net,1433;Initial Catalog=supermarket;Persist Security Info=False;User ID=admin123;Password=adm!n123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            string queryEditEmployee = $"update Employees set FirstName=\'{employee.FirstName}\', LastName=\'{employee.LastName}\', Address=\'{employee.Address}\', OIB=\'{employee.OIB}\' where OIB = \'{OIB}\';";
            SqlCommand editEmployee = new SqlCommand(queryEditEmployee, connection);
            try
            {
                Employee oldEmployee = GetEmployee(OIB);
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
        public bool DeleteEmployee(string OIB)
        {
            string connectionString = "Server=tcp:mono-supermarket-sql.database.windows.net,1433;Initial Catalog=supermarket;Persist Security Info=False;User ID=admin123;Password=adm!n123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            string queryDeleteEmployee = $"delete Employees where OIB = \'{OIB}\'";
            SqlCommand deleteEmployee = new SqlCommand(queryDeleteEmployee, connection);
            try
            {
                Employee oldEmployee = GetEmployee(OIB);
                connection.Open();
                SqlDataAdapter employeeDeleter = new SqlDataAdapter(deleteEmployee);
                employeeDeleter.Fill(new System.Data.DataSet("Employees"));
                connection.Close();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message) ;
                return false;
            }
            return true;
        }
    }

}
