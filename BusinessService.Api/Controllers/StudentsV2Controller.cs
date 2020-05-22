using BusinessService.Api.Logger;
using BusinessService.Data.DBModel;
using BusinessService.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BusinessService.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("2")]
    [ApiExplorerSettings(GroupName = "v2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StudentsV2Controller : Controller
    {
        private readonly IStudentsService _studentsService;
        private readonly ILog _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentsService"></param>
        /// <param name="logger"></param>
        public StudentsV2Controller(IStudentsService studentsService, ILog logger)
        {

            _studentsService = studentsService;
            _logger = logger;
        }

        // GET /api/students
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            _logger.Information("Fetching Started");
            return await _studentsService.GetAllStudentsAsync();
        }


        // GET /api/students/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentAsync(int id)
        {
            _logger.Information("Fetching Started");
            return await _studentsService.GetStudentAsync(id);
        }

        // GET /api/students/find
        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("FindByName")]
        public async Task<IActionResult> FindStudentsAsync(string studentName)
        {
            _logger.Information("Searching Started");
            return await _studentsService.FindStudentsAsync(studentName);
        }

        // DELETE /api/students/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentAsync(int id)
        {
            _logger.Information("Deletion Started");
            return await _studentsService.DeleteStudentAsync(id);
        }

        // POST /api/students
        /// <summary>
        /// 
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddStudentAsync(Student student)
        {
            _logger.Information("Insertion Started");
            return await _studentsService.AddStudentAsync(student);
        }


        // PUT /api/students/1
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudentAsync(int id, Student student)
        {
            _logger.Information("Updation Started");
            return await _studentsService.UpdateStudentAsync(id, student);
        }
    }
}