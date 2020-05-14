using BusinessService.Data.DBModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessService.Data.Repository
{
    public interface IStudentsRepository
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentAsync(int studentId);
        Task<IEnumerable<Student>> FindStudentsAsync(string sku);
        Task<Student> AddStudentAsync(Student student);
        Task<Student> UpdateStudentAsync(int studentId, Student student);
        Task<Student> DeleteStudentAsync(int studentId);
    }
}