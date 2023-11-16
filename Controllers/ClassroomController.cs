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
        private readonly IMapper _mapper;

        public ClassroomController(IClassroomRepository classroomRepository, IMapper mapper)
        {
            _classroomRepository = classroomRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Classroom>))]
        public IActionResult GetClassrooms()
        {
            var classrooms = _mapper.Map<List<ClassroomDto>>(_classroomRepository.GetClassrooms());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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
                ModelState.AddModelError("", "Classroom already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var classroomMap = _mapper.Map<Classroom>(classroomCreate);

            if (!_classroomRepository.CreateClassroom(classroomMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
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
                ModelState.AddModelError("", "Something went wrong updating classroom");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }

        [HttpDelete("{classroomId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteClassroom(int classroomId)
        {
            if (!_classroomRepository.ClassroomExists(classroomId))
            {
                return NotFound();
            }

            var classroomToDelete = _classroomRepository.GetClassroom(classroomId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_classroomRepository.DeleteClassroom(classroomToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting classroom");
            }

            return Ok("Successfully deleted");
        }


    }
}