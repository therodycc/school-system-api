using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using school_system_api.Dto;
using school_system_api.interfaces;
using school_system_api.models;

namespace school_system_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentController(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        public IActionResult GetStudents()
        {
            var students = _mapper.Map<List<StudentDto>>(_studentRepository.GetStudents());

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("error", "Error");
                return StatusCode(400, ModelState);
            }

            return Ok(students);
        }

        [HttpGet("{studentId}")]
        [ProducesResponseType(200, Type = typeof(Student))]
        [ProducesResponseType(400)]
        public IActionResult GetStudent(int studentId)
        {
            if (!_studentRepository.StudentExists(studentId))
                return NotFound();

            var student = _mapper.Map<StudentDto>(_studentRepository.GetStudent(studentId));

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("error", "Error");
                return StatusCode(400, ModelState);
            }

            return Ok(student);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateStudent([FromBody] StudentDto studentCreate)
        {
            if (studentCreate == null)
                return BadRequest(ModelState);

            var student = _studentRepository.GetStudents()
                .Where(c => c.Name.Trim().ToUpper() == studentCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (student != null)
            {
                ModelState.AddModelError("error", "Student already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var studentMap = _mapper.Map<Student>(studentCreate);

            if (!_studentRepository.CreateStudent(studentMap))
            {
                ModelState.AddModelError("error", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Successfully created" });
        }

        [HttpPut("{studentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateStudent(int studentId, [FromBody] StudentDto updatedStudent)
        {
            if (updatedStudent == null)
                return BadRequest(ModelState);

            if (studentId != updatedStudent.Id)
                return BadRequest(ModelState);

            if (!_studentRepository.StudentExists(studentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var studentMap = _mapper.Map<Student>(updatedStudent);

            if (!_studentRepository.UpdateStudent(studentMap))
            {
                ModelState.AddModelError("error", "Something went wrong updating student");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Successfully updated" });
        }

        [HttpDelete("{studentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteStudent(int studentId)
        {
            if (!_studentRepository.StudentExists(studentId))
            {
                return NotFound();
            }

            var studentToDelete = _studentRepository.GetStudent(studentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_studentRepository.DeleteStudent(studentToDelete))
            {
                ModelState.AddModelError("error", "Something went wrong deleting student");
                return StatusCode(400, ModelState);
            }

            return Ok(new { message = "Successfully deleted" });
        }


    }
}