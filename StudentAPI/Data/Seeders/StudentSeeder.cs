using StudentAPI.Data.Models;

namespace StudentAPI.Data.Seeders
{
    public class StudentSeeder
    {
        private readonly AppDbContext appDbContext;

        public StudentSeeder(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task SeedStudents()
        {
            if (!appDbContext.Students.Any())
            {
                await appDbContext.AddRangeAsync(
                    new Student { FirstName = "John", LastName = "Dutton", Email = "john@mail.se" },
                    new Student { FirstName = "Beth", LastName = "Dutton", Email = "beth@mail.se" },
                    new Student { FirstName = "Jamie", LastName = "Dutton", Email = "jamie@mail.se" },
                    new Student { FirstName = "Kayce", LastName = "Dutton", Email = "kayce@mail.se" },
                    new Student { FirstName = "Frodo", LastName = "Bagger", Email = "frodo@mail.se" },
                    new Student { FirstName = "Magika", LastName = "De Hex", Email = "magika@mail.se" }
                    );
                await appDbContext.SaveChangesAsync();
            }
        }
    }
}
