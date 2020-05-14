using BusinessService.Data.DBModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BusinessService.Domain.Services
{
    public interface IStudentsService
    {
        Task<IActionResult> GetAllStudentsAsync();
        Task<IActionResult> GetStudentAsync(int studentId);
        Task<IActionResult> FindStudentsAsync(string sku);
        Task<IActionResult> AddStudentAsync(Student student);
        Task<IActionResult> UpdateStudentAsync(int studentId, Student student);
        Task<IActionResult> DeleteStudentAsync(int studentId);
    }
}