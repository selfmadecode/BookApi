using BookApi.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Book_Author> Books_Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book_Author>()
                .HasOne(b => b.Book)
                .WithMany(ba => ba.Book_Authors)
                .HasForeignKey(bi => bi.BookId);

            modelBuilder.Entity<Book_Author>()
                .HasOne(b => b.Author)
                .WithMany(ba => ba.Book_Authors)
                .HasForeignKey(bi => bi.AuthorId);


            // seed the database with dummy data
            modelBuilder.Entity<Teacher>().HasData(
                new Teacher()
                {
                    Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    FirstName = "Berry",
                    LastName = "Griffin Beak Eldritch",
                    DateOfBirth = new DateTime(1650, 7, 23),
                    MainCategory = "Ships"
                },
                new Teacher()
                {
                    Id = Guid.Parse("5b3621c0-7b12-4e80-9c8b-3398cba7ee05"),
                    FirstName = "Seabury",
                    LastName = "Toxic Reyson",
                    DateOfBirth = new DateTime(1690, 11, 23),
                    MainCategory = "Maps"
                },
                new Teacher()
                {
                    Id = Guid.Parse("2aadd2df-7caf-45ab-9355-7f6332985a87"),
                    FirstName = "Rutherford",
                    LastName = "Fearless Cloven",
                    DateOfBirth = new DateTime(1723, 4, 5),
                    MainCategory = "General debauchery"
                },
                new Teacher()
                {
                    Id = Guid.Parse("2ee49fe3-edf2-4f91-8409-3eb25ce6ca51"),
                    FirstName = "Atherton",
                    LastName = "Crow Ridley",
                    DateOfBirth = new DateTime(1721, 10, 11),
                    MainCategory = "Rum"
                },
                new Teacher()
                {
                    Id = Guid.Parse("71838f8b-6ab3-4539-9e67-4e77b8ede1c0"),
                    FirstName = "Huxford",
                    LastName = "The Hawk Morris",
                    DateOfBirth = new DateTime(1969, 8, 11),
                    MainCategory = "Maps"
                },
                 new Teacher()
                 {
                     Id = Guid.Parse("119f9ccb-149d-4d3c-ad4f-40100f38e918"),
                     FirstName = "Dwennon",
                     LastName = "Rigger Quye",
                     DateOfBirth = new DateTime(1972, 1, 8),
                     MainCategory = "Maps"
                 });
        }
    }
}
