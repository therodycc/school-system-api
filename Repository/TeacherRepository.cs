using school_system_api.data;
using school_system_api.interfaces;
using school_system_api.models;

namespace school_system_api.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private DataContext _context;
        public TeacherRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateTeacher(Teacher teacher)
        {
            _context.Add(teacher);
            return Save();
        }

        public bool DeleteTeacher(Teacher teacher)
        {
            _context.Remove(teacher);
            return Save();
        }

        public Teacher GetTeacher(int id)
        {
            return _context.Teachers.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Teacher> GetTeachers()
        {
            return _context.Teachers.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool TeacherExists(int id)
        {
            return _context.Teachers.Any(c => c.Id == id);
        }

        public bool UpdateTeacher(Teacher teacher)
        {
            _context.Update(teacher);
            return Save();
        }

        public ICollection<Subject> GetSubjectsByTeacher(int teacherId)
        {
            return _context.TeacherSubjects.Where(e => e.TeacherId == teacherId).Select(c => c.Subject).ToList();
        }

        public bool RemoveAllTeacherSubjects(Teacher teacher)
        {
            var teacherSubjectsToRemove = _context.TeacherSubjects.Where(ts => teacher.Id == ts.TeacherId).ToList();
            if (teacherSubjectsToRemove.Any())
            {
                _context.TeacherSubjects.RemoveRange(teacherSubjectsToRemove);
                return Save();
            }
            return false;
        }
    }
}