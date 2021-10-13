using AutoMapper;
using TopLogic.DataSource;
using TopLogicApp.API.DTO;

namespace TopLogicApp.API.Extensions
{
    public class MappingExtension: Profile
    {
        public MappingExtension()
        {
            CreateMap<Employee, EmployeeDTO>()
                .ReverseMap();
        }
    }
}
