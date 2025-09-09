using System.ComponentModel.DataAnnotations;

namespace EmployeeApi.DTOs
{
    public class UpdateEmployeeDto
    {
        public string Name { get; set; } = null!;
        [EmailAddress]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = null!;
        public string? Department { get; set; }
        public decimal Salary { get; set; }
    }
}
