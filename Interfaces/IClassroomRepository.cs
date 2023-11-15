using school_system_api.models;


namespace school_system_api.interfaces
{
    public interface IClassroomRepository
    {
        ICollection<Classroom> GetClassrooms();
        Classroom GetClassroom(int id);
        bool ClassroomExists(int id);
        bool CreateClassroom(Classroom classroom);
        bool UpdateClassroom(Classroom classroom);
        bool DeleteClassroom(Classroom classroom);
        bool Save();
    }
}