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

            CreateMap<SubjectDto, Subject>();
            CreateMap<Subject, SubjectDto>();

            CreateMap<StudentDto, Student>();
            CreateMap<Student, StudentDto>();

            CreateMap<ClassroomDto, Classroom>();
            CreateMap<Classroom, ClassroomDto>();

            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();

            CreateMap<CreateUserDto, User>();
            CreateMap<User, CreateUserDto>();
        }
    }
}