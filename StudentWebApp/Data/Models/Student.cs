using System.ComponentModel.DataAnnotations;

namespace StudentWebApp.Data.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; } = string.Empty;
        [Display(Name = "Efternamn")]
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
