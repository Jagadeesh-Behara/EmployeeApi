using AutoMapper;
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
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? q = null, [FromQuery] string? department = null)
        {
            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var (items, total) = await _repo.GetPagedAsync(page, pageSize, q, department);
            var dtos = _mapper.Map<IEnumerable<EmployeeDto>>(items);

            return Ok(new { data = dtos, page, pageSize, total });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var emp = await _repo.GetByIdAsync(id);
            if (emp == null) return NotFound();
            return Ok(_mapper.Map<EmployeeDto>(emp));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
        {
            if (await _repo.ExistsByEmailAsync(dto.Email))
                return Conflict(new { message = "Email already exists" });

            var emp = _mapper.Map<Employee>(dto);
            var created = await _repo.CreateAsync(emp);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, _mapper.Map<EmployeeDto>(created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeDto dto)
        {
            var emp = await _repo.GetByIdAsync(id);
            if (emp == null) return NotFound();

            if (await _repo.ExistsByEmailAsync(dto.Email, id))
                return Conflict(new { message = "Email already exists" });

            _mapper.Map(dto, emp);
            await _repo.UpdateAsync(emp);
            return Ok(emp);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var emp = await _repo.GetByIdAsync(id);
            if (emp == null) return NotFound();
            await _repo.DeleteAsync(emp);
            return Ok(emp);
        }
    }
}
