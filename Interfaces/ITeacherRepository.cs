using school_system_api.models;

namespace school_system_api.interfaces
{
    public interface ITeacherRepository
    {
        ICollection<Teacher> GetTeachers();
        Teacher GetTeacher(int id);
        bool TeacherExists(int id);
        bool CreateTeacher(Teacher teacher);
        bool UpdateTeacher(Teacher teacher);
        bool DeleteTeacher(Teacher teacher);
        bool RemoveAllTeacherSubjects(Teacher teacher);
        ICollection<Subject> GetSubjectsByTeacher(int teacherId);
        bool Save();
    }
}