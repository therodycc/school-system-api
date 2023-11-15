using AutoMapper;
using school_system_api.Dto;
using school_system_api.models;

namespace school_system_api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<TeacherDto, Teacher>();
            CreateMap<Teacher, TeacherDto>();
        }
    }
}