using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }

        // Used to create category table and property name is the table name
        // To create table, you have to do a migration to add table.
        // Go to package manager console, write "add-migration AddCategoryTableToDb"
        // It creates a migration class that logs the changes in entity framework syntax and when you update database, that migration is applied onto the database
        public DbSet<Category> Categories { get; set; }

        // Used to seed data within the database/table/notsure
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeds data in category entity/table
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1},
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 });
        }
    }
}
