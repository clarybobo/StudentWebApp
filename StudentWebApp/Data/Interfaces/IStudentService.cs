using StudentWebApp.Data.Models;

namespace StudentWebApp.Data.Interfaces
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsAsync(); 
    }
}
