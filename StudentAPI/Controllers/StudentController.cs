using Microsoft.AspNetCore.Mvc;
using StudentAPI.Data.Interfaces;
using StudentAPI.Data.Models;

namespace StudentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudent studentRepo;
        private readonly string generalFaultMessage = "Ett oväntat fel uppstod vid hanteringen av förfrågan!";

        public StudentController(IStudent studentRepo)
        {
            this.studentRepo = studentRepo;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            try
            {
                var students = await studentRepo.GetStudentsAsync();

                if(students == null || students.Count == 0) 
                {
                    return NotFound("Det finns inga studenter att hämta"); 
                }

                return Ok(students);

            }
            catch (Exception)
            {
                return StatusCode(500, generalFaultMessage);
            }
        }      
    }
}
