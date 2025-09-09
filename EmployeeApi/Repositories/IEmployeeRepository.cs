using EmployeeApi.Models;

namespace EmployeeApi.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByIdAsync(int id);
        Task<(IEnumerable<Employee>, int total)> GetPagedAsync(int page, int pageSize, string? q, string? department);
        Task<Employee> CreateAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
        Task<bool> ExistsByEmailAsync(string email, int? excludingId = null);
    }
}
