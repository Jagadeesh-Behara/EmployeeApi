using AutoMapper;
using EmployeeApi.Data;
using EmployeeApi.DTOs;
using EmployeeApi.Models;
using EmployeeApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allEmps = await _repo.GetAllEmployees();
            return Ok(_mapper.Map<Result<List<EmployeeDto>>>(allEmps));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var emp = await _repo.GetByIdAsync(id);
            if (emp == null) return NotFound();
            return Ok(_mapper.Map<Result<EmployeeDto>>(emp));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
        {
            if (await _repo.ExistsByEmailAsync(dto.Email))
                return Conflict(new { message = "Email already exists" });

            var emp = _mapper.Map<Employee>(dto);
            var created = await _repo.CreateAsync(emp);
            return Ok(created);  
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeDto dto)
        {
            var emp = await _repo.GetByIdAsync(id);
            if (emp == null) return NotFound();

            if (await _repo.ExistsByEmailAsync(dto.Email, id))
                return Conflict(new { message = "Email already exists" });

            var updateEmployee = _mapper.Map<Employee>(emp);
            await _repo.UpdateAsync(updateEmployee);
            return Ok(emp);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var emp = await _repo.GetByIdAsync(id);
            if (emp == null) return NotFound();
            var deleteEmployee = _mapper.Map<Employee>(emp);
            await _repo.DeleteAsync(deleteEmployee);
            return Ok(emp);
        }
    }
}
