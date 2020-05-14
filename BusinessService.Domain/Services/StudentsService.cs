using BusinessService.Data.DBModel;
using BusinessService.Data.Repository;
using BusinessService.Domain.DomainModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessService.Domain.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly IStudentsRepository _studentsRepository;

        public StudentsService(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        public async Task<IActionResult> FindStudentsAsync(string name)
        {
            try
            {
                var students = await _studentsRepository.FindStudentsAsync(name);

                if (students != null)
                    return new OkObjectResult(students.Select(p => new StudentViewModel
                    {
                        //Id = p.StudentId,

                        Name = p.Name.Trim(),
                        Gender = p.Gender.Trim(),
                        SchoolId = p.School
                    }
                    ));
                return new NotFoundResult();
            }
            catch
            {
                return new ConflictResult();
            }
        }

        public async Task<IActionResult> GetAllStudentsAsync()
        {
            try
            {
                var students = await _studentsRepository.GetAllStudentsAsync();

                if (students != null)
                    return new OkObjectResult(students.Select(p => new StudentViewModel
                    {
                        // Id = p.StudentId,
                        Gender = p.Gender.Trim(),
                        Name = p.Name.Trim(),
                        SchoolId = p.School
                    }
                    ));
                return new NotFoundResult();
            }
            catch
            {
                return new ConflictResult();
            }
        }

        public async Task<IActionResult> GetStudentAsync(int studentId)
        {
            try
            {
                var student = await _studentsRepository.GetStudentAsync(studentId);

                if (student != null)
                    return new OkObjectResult(new StudentViewModel
                    {
                        // Id = student.StudentId,
                        Gender = student.Gender.Trim(),
                        Name = student.Name.Trim(),
                        SchoolId = student.School
                    });
                return new NotFoundResult();
            }
            catch
            {
                return new ConflictResult();
            }
        }

        public async Task<IActionResult> DeleteStudentAsync(int studentId)
        {
            try
            {
                var student = await _studentsRepository.DeleteStudentAsync(studentId);

                if (student != null)
                    return new OkObjectResult(new StudentViewModel
                    {
                        // Id = student.StudentId,
                        Gender = student.Gender.Trim(),
                        Name = student.Name.Trim(),
                        SchoolId = student.School
                    });
                return new NotFoundResult();
            }
            catch
            {
                return new ConflictResult();
            }
        }

        public async Task<IActionResult> AddStudentAsync(Student student)
        {
            try
            {
                var studentList = await _studentsRepository.AddStudentAsync(student);
                if (student != null)
                    return new OkObjectResult(new StudentViewModel
                    {
                        // Id = studentList.StudentId,
                        Gender = studentList.Gender.Trim(),
                        Name = studentList.Name.Trim(),
                        SchoolId = studentList.School
                    });
                return new NotFoundResult();
            }
            catch
            {
                return new ConflictResult();
            }
        }

        public async Task<IActionResult> UpdateStudentAsync(int studentId, Student student)
        {
            try
            {
                var studentList = await _studentsRepository.UpdateStudentAsync(studentId, student);
                if (studentList != null)
                    return new OkObjectResult(new StudentViewModel
                    {
                        // Id = studentId,
                        Gender = studentList.Gender.Trim(),
                        Name = studentList.Name.Trim(),
                        SchoolId = studentList.School
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