using BusinessService.Data.DBModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessService.Data.Repository
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly DefaultContext _context;


        // private EntityEntry<Student> _entityEntry;


        public StudentsRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetStudentAsync(int studentId)
        {
            return await _context.Students.Where(p => p.StudentId == studentId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Student>> FindStudentsAsync(string name)
        {
            return await _context.Students.Where(p => p.Name.Contains(name)).ToListAsync();
        }

        public async Task<Student> DeleteStudentAsync(int studentId)
        {
            var student = await GetStudentAsync(studentId);

            if (student != null)
            {
                _context.Students.Remove(student);

                await _context.SaveChangesAsync();
            }

            return student;
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            if (student != null)
            {
                await _context.Students.AddAsync(student);

                await _context.SaveChangesAsync();
            }

            return student;
        }

        public async Task<Student> UpdateStudentAsync(int id, Student student)
        {
            var studentId = await _context.Students.FindAsync(id);
            if (studentId != null)
            {
                studentId.Name = student.Name;
                studentId.Gender = student.Gender;
                studentId.School = student.School;
                //  studentId.School.Name = student.School.Name;
                await _context.SaveChangesAsync();
            }

            return student;
        }
    }
}