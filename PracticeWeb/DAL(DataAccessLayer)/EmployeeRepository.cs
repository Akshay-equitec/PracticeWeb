using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using PracticeWeb.Models;

namespace PracticeWeb.DAL_DataAccessLayer_
{
    public class EmployeeRepository
    {
        private readonly DapperContext _context;

        public EmployeeRepository()
        {
            _context = new DapperContext();
        }

        // Get a list of all employees (excluding soft-deleted ones)
        public IEnumerable<Employee> GetAllEmployees(string searchTerm = "", int pageNumber = 1, int pageSize = 5)
        {
            var query = @"SELECT * FROM Employees 
                      WHERE IsDeleted = 0 
                      AND (@searchTerm = '' OR Name LIKE '%' + @searchTerm + '%')
                      ORDER BY Id
                      OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            using (var connection = _context.CreateConnection())
            {
                return connection.Query<Employee>(query, new
                {
                    searchTerm = searchTerm,
                    Offset = (pageNumber - 1) * pageSize,
                    PageSize = pageSize
                }).ToList();
            }
        }

        // Get employee by ID
        public Employee GetEmployeeById(int id)
        {
            var query = "SELECT * FROM Employees WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return connection.QuerySingleOrDefault<Employee>(query, new { Id = id });
            }
        }

        // Add a new employee
        public void AddEmployee(Employee employee)
        {
            var query = "INSERT INTO Employees (Name, Age, Department) VALUES (@Name, @Age, @Department)";

            using (var connection = _context.CreateConnection())
            {
                connection.Execute(query, employee);
            }
        }

        // Update an existing employee
        public void UpdateEmployee(Employee employee)
        {
            var query = "UPDATE Employees SET Name = @Name, Age = @Age, Department = @Department WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                connection.Execute(query, employee);
            }
        }

        // Soft delete an employee
        public void SoftDeleteEmployee(int id)
        {
            var query = "UPDATE Employees SET IsDeleted = 1 WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                connection.Execute(query, new { Id = id });
            }
        }

        // Count total employees for pagination
        public int GetTotalEmployees(string searchTerm = "")
        {
            var query = @"SELECT COUNT(*) FROM Employees WHERE IsDeleted = 0 AND (@searchTerm = '' OR Name LIKE '%' + @searchTerm + '%')";

            using (var connection = _context.CreateConnection())
            {
                return connection.ExecuteScalar<int>(query, new { searchTerm = searchTerm });
            }
        }
    }

}