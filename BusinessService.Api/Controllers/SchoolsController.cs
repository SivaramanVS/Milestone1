using BusinessService.Api.Logger;
using BusinessService.Data.DBModel;
using BusinessService.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; //using BusinessService..Domain.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;

namespace BusinessService.Api.Controllers
{
    //This is about Version 1 of School Controller
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ISchoolsService _schoolsService;
        private readonly ILog _logger;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="schoolsService"></param>
        /// <param name="distributedCache"></param>
        /// <param name="logger"></param>
        public SchoolsController(ISchoolsService schoolsService,
            ILog logger = default)
        {
            _schoolsService = schoolsService;
            this._logger = logger ?? new LogNLog();


        }

        // GET /api/Schools
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllStudentsAsync()
        {

            _logger.Information("Fetching Started: ");
            var school = await _schoolsService.GetAllSchoolsAsync();

            if (school!=null)
            {
                _logger.Information("Data Fetched Successfully ");

            }
            return school;
            

        }


        // GET /api/Schools/1
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchoolsAsync(int id)
        {                                                      
            _logger.Information("Fetching Started");
            return await _schoolsService.GetSchoolsAsync(id);
        }

        // GET /api/students/find
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schoolName"></param>
        /// <returns></returns>        
        [HttpGet]
        [Route("FindByName")]
        public async Task<IActionResult> FindStudentsAsync(string schoolName)
        {
            _logger.Information("Searching Started");
            return await _schoolsService.FindSchoolsAsync(schoolName);
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
            _logger.Information("Deleting Started");
            return await _schoolsService.DeleteSchoolsAsync(id);
        }

        // POST /api/students
        /// <summary>
        /// 
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddStudentAsync(School school)
        {
            _logger.Information("Insertion Started");
            return await _schoolsService.AddSchoolsAsync(school);
        }


        // PUT /api/students/1
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="school"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudentAsync(int id, School school)
        {
            _logger.Information("Updation Started");
            return await _schoolsService.UpdateSchoolsAsync(id, school);
        }
    }
}
