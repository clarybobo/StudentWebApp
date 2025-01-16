using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Data.Models
{
    public class Student
    {
        public int Id { get; set; }        
        public string FirstName { get; set; } = string.Empty;  
        public string LastName { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
