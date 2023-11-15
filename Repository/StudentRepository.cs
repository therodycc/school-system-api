using school_system_api.data;
using school_system_api.interfaces;
using school_system_api.models;

namespace school_system_api.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private DataContext _context;
        public StudentRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateStudent(Student student)
        {
            _context.Add(student);
            return Save();
        }

        public bool DeleteStudent(Student student)
        {
            _context.Remove(student);
            return Save();
        }

        public Student GetStudent(int id)
        {
            return _context.Students.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Student> GetStudents()
        {
            return _context.Students.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool StudentExists(int id)
        {
            return _context.Students.Any(c => c.Id == id);
        }

        public bool UpdateStudent(Student student)
        {
            _context.Update(student);
            return Save();
        }
    }
}