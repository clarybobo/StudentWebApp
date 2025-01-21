using StudentWebApp.Data.Interfaces;
using StudentWebApp.Data.Models;

namespace StudentWebApp.Data.Services
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient httpClient;

        public StudentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<Student>> GetStudentsAsync()
        {
            try
            {
                var response = await httpClient.GetAsync("/api/Student");

                if (response.IsSuccessStatusCode)
                {
                    var students = await response.Content.ReadFromJsonAsync<List<Student>>();
                    return students ?? new List<Student>();
                }
                else
                {
                    throw new Exception($"API-fel: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Något gick fel vid hämtning av studenter", ex);
            }
        }
    }
}
