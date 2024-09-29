using AutoMapper;
using Company.DAL.Models;
using Company.PL.ViewModels;

namespace Company.PL.MappingProfiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentViewModel , Department>().ReverseMap();
        }
    }
}
