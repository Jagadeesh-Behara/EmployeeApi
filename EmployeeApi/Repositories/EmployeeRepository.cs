using EmployeeApi.Data;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _db;
        public EmployeeRepository(AppDbContext db) => _db = db;

        public async Task<Employee> CreateAsync(Employee employee)
        {
            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();
            return employee;
        }

        public async Task DeleteAsync(Employee employee)
        {
            _db.Employees.Remove(employee);
            await _db.SaveChangesAsync();
        }

        public async Task<Result<Employee>> GetById(int id)
        {
            //return await _db.Employees.FindAsync(id);
            var employee = await _db.Employees.FindAsync(id);
            if (employee == null)
                return Result<Employee>.Fail("Employee not found");
            return Result<Employee>.Ok(employee,"Employee found");
        }

        public async Task UpdateAsync(Employee employee)
        {
            _db.Employees.Update(employee);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ExistsByEmailAsync(string email, int? excludingId = null)
        {
            var q = _db.Employees.AsQueryable().Where(e => e.Email == email);
            if (excludingId.HasValue) q = q.Where(e => e.Id != excludingId.Value);
            return await q.AnyAsync();
        }

        public async Task<Result<List<Employee>>> GetAllEmployees()
        {
            var employees = await _db.Employees.ToListAsync();
            if (employees == null || employees.Count == 0)
                return Result<List<Employee>>.Fail("No employees found");
            return Result<List<Employee>>.Ok(employees, null);
        }

        public async Task<Result<Employee?>> GetByIdAsync(int id)
        {
            var employee = await _db.Employees.FindAsync(id);
            if (employee == null)
                return Result<Employee?>.Fail("Employee not found");
            return Result<Employee?>.Ok(employee, "Employee found");
        }

    }
}
