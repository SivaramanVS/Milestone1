using BusinessService.Data.DBModel;
using BusinessService.Data.Repository;
using BusinessService.Domain.DomainModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessService.Domain.Services
{
    public class SchoolsService : ISchoolsService
    {
        private readonly ISchoolsRepository _schoolsRepository;

        public SchoolsService(ISchoolsRepository schoolsRepository)
        {
            _schoolsRepository = schoolsRepository;

        }

        public async Task<IActionResult> AddSchoolsAsync(School schoolsId)
        {
            try
            {
                var schoolList = await _schoolsRepository.AddSchoolsAsync(schoolsId);
                if (schoolsId != null)
                    return new OkObjectResult(new SchoolViewModel
                    {
                        Name = schoolList.Name.Trim()
                    });
                return new NotFoundResult();
            }
            catch
            {
                return new ConflictResult();
            }
        }

        public async Task<IActionResult> DeleteSchoolsAsync(int schoolsId)
        {
            try
            {
                var school = await _schoolsRepository.DeleteSchoolsAsync(schoolsId);

                if (school != null)
                    return new OkObjectResult(new SchoolViewModel
                    {
                        //Id = schoolsId,

                        Name = school.Name.Trim()
                    });
                return new NotFoundResult();
            }
            catch
            {
                return new ConflictResult();
            }
        }

        public async Task<IActionResult> FindSchoolsAsync(string schoolName)
        {
            try
            {
                var school = await _schoolsRepository.FindSchoolsAsync(schoolName);

                if (school != null)
                    return new OkObjectResult(school.Select(p => new SchoolViewModel
                    {
                        //Id = p.Id,

                        Name = p.Name.Trim()
                    }
                    ));
                return new NotFoundResult();
            }
            catch
            {
                return new ConflictResult();
            }
        }


        public async Task<IActionResult> GetAllSchoolsAsync()
        {
            try
            {

                var schools = await _schoolsRepository.GetAllSchoolsAsync();

                if (schools != null)
                    return new OkObjectResult(schools.Select(p => new SchoolViewModel
                    {
                        // Id = p.Id,

                        Name = p.Name.Trim()
                    }
                    ));
                return new NotFoundResult();
            }
            catch
            {
                return new ConflictResult();
            }
        }

        public async Task<IActionResult> GetSchoolsAsync(int schoolId)
        {
            try
            {
                var school = await _schoolsRepository.GetSchoolsAsync(schoolId);

                if (school != null)
                    return new OkObjectResult(new SchoolViewModel
                    {
                        //School = p.Value,
                        Name = school.Name.Trim()
                    });
                return new NotFoundResult();
            }
            catch
            {
                return new ConflictResult();
            }
        }

        public async Task<IActionResult> UpdateSchoolsAsync(int schoolsId, School schools)
        {
            try
            {
                var schoolsList = await _schoolsRepository.UpdateSchoolsAsync(schoolsId, schools);
                if (schools != null)
                    return new OkObjectResult(new SchoolViewModel
                    {
                        //Id = schoolsId,

                        Name = schoolsList.Name.Trim()
                    });
                return new NotFoundResult();
            }
            catch
            {
                return new ConflictResult();
            }
        }
    }
}