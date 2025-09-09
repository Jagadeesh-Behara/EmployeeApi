using EmployeeApi.Data;
using EmployeeApi.Models;
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

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _db.Employees.FindAsync(id);
        }

        public async Task<(IEnumerable<Employee>, int total)> GetPagedAsync(int page, int pageSize, string? q, string? department)
        {
            var query = _db.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                var term = q.Trim().ToLower();
                query = query.Where(e => e.Name.ToLower().Contains(term) || e.Email.ToLower().Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(department))
            {
                var dept = department.Trim().ToLower();
                query = query.Where(e => e.Department != null && e.Department.ToLower() == dept);
            }

            var total = await query.CountAsync();

            var items = await query
                .OrderByDescending(e => e.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, total);
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
    }
}
