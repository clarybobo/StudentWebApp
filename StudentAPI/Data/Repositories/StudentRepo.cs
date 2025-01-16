using Microsoft.EntityFrameworkCore;
using StudentAPI.Data.Interfaces;
using StudentAPI.Data.Models;

namespace StudentAPI.Data.Repositories
{
    public class StudentRepo : IStudent
    {
        private readonly AppDbContext appDbContext;

        public StudentRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            try
            {
                var students = await appDbContext.Students
                       .OrderBy(s => s.LastName)
                       .ThenBy(s => s.FirstName)
                       .ToListAsync();

                if (students == null || students.Count == 0)
                {
                    return new List<Student>();
                }

                return students;
            }

            catch (Exception ex)
            {
                throw new Exception("Ett fel inträffade vid hämtning av studednter", ex);
            }
        }
    }
}

