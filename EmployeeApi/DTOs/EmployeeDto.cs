namespace EmployeeApi.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Department { get; set; }
        public decimal Salary { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
