using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using school_system_api.Dto;
using school_system_api.interfaces;
using school_system_api.models;

namespace school_system_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherRepository teacherRepository, IMapper mapper, ISubjectRepository subjectRepository)
        {
            _teacherRepository = teacherRepository;
            _subjectRepository = subjectRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Teacher>))]
        public IActionResult GetTeachers()
        {
            var teachers = _mapper.Map<List<TeacherDto>>(_teacherRepository.GetTeachers());

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("error", "Error");
                return StatusCode(400, ModelState);
            }

            return Ok(teachers);
        }

        [HttpGet("{teacherId}")]
        [ProducesResponseType(200, Type = typeof(Teacher))]
        [ProducesResponseType(400)]
        public IActionResult GetTeacher(int teacherId)
        {
            if (!_teacherRepository.TeacherExists(teacherId))
                return NotFound();

            var teacher = _mapper.Map<TeacherDto>(_teacherRepository.GetTeacher(teacherId));

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("error", "Error");
                return StatusCode(400, ModelState);
            }

            return Ok(teacher);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTeacher([FromBody] TeacherDto teacherCreate)
        {
            if (teacherCreate == null)
                return BadRequest(ModelState);

            var teacher = _teacherRepository.GetTeachers()
                .Where(c => c.Name.Trim().ToUpper() == teacherCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (teacher != null)
            {
                ModelState.AddModelError("error", "Teacher already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var teacherMap = _mapper.Map<Teacher>(teacherCreate);

            if (!_teacherRepository.CreateTeacher(teacherMap))
            {
                ModelState.AddModelError("error", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Successfully created" });
        }

        [HttpPut("{teacherId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTeacher(int teacherId, [FromBody] TeacherDto updatedTeacher)
        {
            if (updatedTeacher == null)
                return BadRequest(ModelState);

            if (teacherId != updatedTeacher.Id)
                return BadRequest(ModelState);

            if (!_teacherRepository.TeacherExists(teacherId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var teacherMap = _mapper.Map<Teacher>(updatedTeacher);

            if (!_teacherRepository.UpdateTeacher(teacherMap))
            {
                ModelState.AddModelError("", "Something went wrong updating teacher");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Successfully updated" });
        }

        [HttpDelete("{teacherId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTeacher(int teacherId)
        {
            if (!_teacherRepository.TeacherExists(teacherId))
                return NotFound();

            var teacherToDelete = _teacherRepository.GetTeacher(teacherId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_teacherRepository.DeleteTeacher(teacherToDelete))
                ModelState.AddModelError("", "Something went wrong deleting teacher");

            return Ok(new { message = "Successfully deleted" });
        }

        [HttpGet("subject/{teacherId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Subject>))]
        [ProducesResponseType(400)]
        public IActionResult GetSubjectsByTeacherId(int teacherId)
        {
            var subjects = _mapper.Map<List<SubjectDto>>(
                _teacherRepository.GetSubjectsByTeacher(teacherId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(subjects);
        }

        [HttpPut("{teacherId}/subjects")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult AssignSubjectsToTeacher(int teacherId, [FromBody] List<SubjectsToTeacherDto> payload)
        {
            var teacher = _teacherRepository.GetTeacher(teacherId);

            if (teacher == null)
            {
                return NotFound("Teacher not found");
            }

            _teacherRepository.RemoveAllTeacherSubjects(teacher);

            var teacherSubjectsList = teacher.TeacherSubjects?.ToList();

            foreach (var _sub in payload)
            {

                var subject = _subjectRepository.GetSubject(_sub.SubjectId);

                if (subject == null)
                {
                    return NotFound($"Subject with ID {_sub.SubjectId} not found");
                }

                var teacherSubject = new TeacherSubject
                {
                    Teacher = teacher,
                    Subject = subject
                };

                if (teacherSubjectsList == null)
                {
                    teacherSubjectsList = new List<TeacherSubject>();
                }

                teacherSubjectsList.Add(teacherSubject);
            }

            teacher.TeacherSubjects = teacherSubjectsList;

            if (!_teacherRepository.UpdateTeacher(teacher))
            {
                ModelState.AddModelError("error", "Something went wrong updating");
                return BadRequest(ModelState);
            }

            return Ok(new { message = "Subjects were assigned successfully" });
        }

    }
}