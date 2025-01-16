using StudentAPI.Data.Models;

namespace StudentAPI.Data.Interfaces
{
    public interface IStudent
    {
        public Task<List<Student>> GetStudentsAsync();
    }
}
