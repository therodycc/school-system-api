using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using school_system_api.Dto;
using school_system_api.interfaces;
using school_system_api.models;

namespace school_system_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : Controller
    {
        private readonly IClassroomRepository _classroomRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public ClassroomController(IClassroomRepository classroomRepository, IMapper mapper, ITeacherRepository teacherRepository)
        {
            _classroomRepository = classroomRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
            _teacherRepository = teacherRepository ?? throw new ArgumentNullException(nameof(teacherRepository));
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Classroom>))]
        public IActionResult GetClassrooms()
        {
            var classrooms = _mapper.Map<List<ClassroomDto>>(_classroomRepository.GetClassrooms());

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("error", "Error");
                return StatusCode(400, ModelState);
            }

            return Ok(classrooms);
        }

        [HttpGet("{classroomId}")]
        [ProducesResponseType(200, Type = typeof(Classroom))]
        [ProducesResponseType(400)]
        public IActionResult GetClassroom(int classroomId)
        {
            if (!_classroomRepository.ClassroomExists(classroomId))
                return NotFound();

            var classroom = _mapper.Map<ClassroomDto>(_classroomRepository.GetClassroom(classroomId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(classroom);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateClassroom([FromBody] ClassroomDto classroomCreate)
        {
            if (classroomCreate == null)
                return BadRequest(ModelState);

            var classroom = _classroomRepository.GetClassrooms()
                .Where(c => c.Name.Trim().ToUpper() == classroomCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (classroom != null)
            {
                ModelState.AddModelError("error", "Classroom already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var classroomMap = _mapper.Map<Classroom>(classroomCreate);

            if (!_classroomRepository.CreateClassroom(classroomMap))
            {
                ModelState.AddModelError("error", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Successfully created" });
        }

        [HttpPut("{classroomId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateClassroom(int classroomId, [FromBody] ClassroomDto updatedClassroom)
        {
            if (updatedClassroom == null)
                return BadRequest(ModelState);

            if (classroomId != updatedClassroom.Id)
                return BadRequest(ModelState);

            if (!_classroomRepository.ClassroomExists(classroomId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var classroomMap = _mapper.Map<Classroom>(updatedClassroom);

            if (!_classroomRepository.UpdateClassroom(classroomMap))
            {
                ModelState.AddModelError("error", "Something went wrong updating classroom");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Successfully updated" });
        }

        [HttpDelete("{classroomId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteClassroom(int classroomId)
        {
            if (!_classroomRepository.ClassroomExists(classroomId)) return NotFound();

            var classroomToDelete = _classroomRepository.GetClassroom(classroomId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_classroomRepository.DeleteClassroom(classroomToDelete))
            {
                ModelState.AddModelError("error", "Something went wrong deleting classroom");
                return BadRequest(ModelState);
            }

            return Ok(new { message = "Successfully deleted" });
        }
        [HttpPut("{classroomId}/teacher")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult AssignTeacher(int classroomId, [FromBody] AssignTeacherToClassroomDto payload)
        {

            var classroom = _classroomRepository.GetClassroom(classroomId);
            if (classroom == null) return NotFound("Classroom not found");

            var teacher = _teacherRepository.GetTeacher(payload.TeacherId);
            if (teacher == null) return NotFound("Teacher not found");

            classroom.Teacher = teacher;

            if (!_classroomRepository.UpdateClassroom(classroom))
            {
                ModelState.AddModelError("error", "Something went wrong updating classroom");
                return BadRequest(ModelState);
            }

            return Ok(new { message = "Classroom assigned successfully" });
        }
    }
}