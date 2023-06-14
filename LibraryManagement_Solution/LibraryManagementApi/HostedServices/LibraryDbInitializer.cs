using LibraryManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApi.HostedServices
{
    public class LibraryDbInitializer
    {
        private readonly LibraryDbContext db;
        public LibraryDbInitializer(LibraryDbContext db)
        {
            this.db = db;
        }
        public async Task SeedAsync()
        {
           
            if(!await db.Database.CanConnectAsync())
            {
                await db.Database.EnsureCreatedAsync();
            }
            if (!db.Students.Any())
            {
                var s1 = new Student { Name = "S1", Class = "XII" };
                var s2 = new Student { Name = "S2", Class = "XII" };
                var s3 = new Student { Name = "S3", Class = "XII" };
                var s4 = new Student { Name = "S4", Class = "XII" };
                var s5 = new Student { Name = "S5", Class = "XII" };
                var s6 = new Student { Name = "S6", Class = "XII" };
                await db.Students.AddRangeAsync(

                 s1, s2, s3, s4, s5, s6
            );
            }
            if(!db.Books.Any()) {
                var b1 = new Book { Title = "Linear Algebra", Price = 900.00M, Author = "M Mortel" };
                var b2 = new Book { Title = "3-D Geometry", Price = 900.00M, Author = "K Aurthur" };
                var b3 = new Book { Title = "Applied Physics", Price = 900.00M, Author = "R Daniel" };
                var b4 = new Book { Title = "Everyday Science", Price = 900.00M, Author = "Pestro" };
                var b5 = new Book { Title = "Calculas", Price = 900.00M, Author = "Hacks S" };
                var b6 = new Book { Title = "3-D Geometry", Price = 900.00M, Author = "K Aurthur" };


                b1.BookIssues.Add(new BookIssue { StudentId = 1, IssueDate = new DateTime(2023, 3, 3), ReturnDate = new DateTime(2023, 3, 10), ActualReturnDate = new DateTime(2023, 3, 9) });
                b1.BookIssues.Add(new BookIssue { StudentId = 2, IssueDate = DateTime.Today.AddDays(-30), ReturnDate = DateTime.Today.AddDays(-15) });
                b3.BookIssues.Add(new BookIssue { StudentId = 2, IssueDate = DateTime.Today.AddDays(-30), ReturnDate = DateTime.Today.AddDays(-15) });
                b6.BookIssues.Add(new BookIssue { StudentId = 2, IssueDate = DateTime.Today.AddDays(-20), ReturnDate = DateTime.Today.AddDays(-10) });

                await db.Books.AddRangeAsync(
                     b1, b2, b3, b4, b5, b6
                );
            }
            await db.SaveChangesAsync();
        }
    }
}
