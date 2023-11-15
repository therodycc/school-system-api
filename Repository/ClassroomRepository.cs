using school_system_api.data;
using school_system_api.interfaces;
using school_system_api.models;

namespace school_system_api.Repository
{
    public class ClassroomRepository : IClassroomRepository
    {
        private DataContext _context;
        public ClassroomRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateClassroom(Classroom classroom)
        {
            _context.Add(classroom);
            return Save();
        }

        public bool DeleteClassroom(Classroom classroom)
        {
            _context.Remove(classroom);
            return Save();
        }

        public Classroom GetClassroom(int id)
        {
            return _context.Classrooms.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Classroom> GetClassrooms()
        {
            return _context.Classrooms.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool ClassroomExists(int id)
        {
            return _context.Classrooms.Any(c => c.Id == id);
        }

        public bool UpdateClassroom(Classroom classroom)
        {
            _context.Update(classroom);
            return Save();
        }
    }
}