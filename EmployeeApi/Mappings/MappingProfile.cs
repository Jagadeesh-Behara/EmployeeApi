using AutoMapper;
using EmployeeApi.Data;
using EmployeeApi.DTOs;
using EmployeeApi.Models;

namespace EmployeeApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            //CreateMap(typeof(Result<>), typeof(Result<>));
            CreateMap(typeof(Result<>), typeof(Result<>));
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<UpdateEmployeeDto, Employee>();
        }
    }
}
