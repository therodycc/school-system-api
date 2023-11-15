using school_system_api.data;
using school_system_api.interfaces;
using school_system_api.models;

namespace school_system_api.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private DataContext _context;
        public SubjectRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateSubject(Subject subject)
        {
            _context.Add(subject);
            return Save();
        }

        public bool DeleteSubject(Subject subject)
        {
            _context.Remove(subject);
            return Save();
        }

        public Subject GetSubject(int id)
        {
            return _context.Subjects.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Subject> GetSubjects()
        {
            return _context.Subjects.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool SubjectExists(int id)
        {
            return _context.Subjects.Any(c => c.Id == id);
        }

        public bool UpdateSubject(Subject subject)
        {
            _context.Update(subject);
            return Save();
        }
    }
}