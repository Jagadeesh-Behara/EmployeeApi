using EmployeeApi.Data;
using EmployeeApi.Models;

namespace EmployeeApi.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Result<List<Employee>>> GetAllEmployees();
        Task<Result<Employee?>> GetByIdAsync(int id);
        Task<Employee> CreateAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
        Task<bool> ExistsByEmailAsync(string email, int? excludingId = null);
    }
}
