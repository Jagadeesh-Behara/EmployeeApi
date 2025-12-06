using EmployeeApi.Models;
using FluentValidation;
using System.Data;

namespace EmployeeApi.Validations
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

            RuleFor(e => e.Department)
                .NotEmpty().WithMessage("Department is required")
                .MaximumLength(50).WithMessage("Department cannot exceed 50 characters");

            RuleFor(e => e.Salary)
                .NotEmpty().WithMessage("Salary is required")
                .GreaterThan(0).WithMessage("Salary must be greater than zero");

        }
    }
}
