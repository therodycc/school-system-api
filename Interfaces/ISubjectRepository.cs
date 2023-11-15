using school_system_api.models;

namespace school_system_api.interfaces
{
    public interface ISubjectRepository
    {
        ICollection<Subject> GetSubjects();
        Subject GetSubject(int id);
        bool SubjectExists(int id);
        bool CreateSubject(Subject subject);
        bool UpdateSubject(Subject subject);
        bool DeleteSubject(Subject subject);
        bool Save();
    }
}