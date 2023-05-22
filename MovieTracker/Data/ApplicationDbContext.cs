using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieTracker.Models;
using System.Reflection.Emit;
using MovieTracker.Models.DTO;

namespace MovieTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }
        public DbSet<MovieTracker.Models.Movie> Movies { get; set; } = default!;

        public DbSet<Category> Categories { get; set; }

        public DbSet<MovieTracker.Models.Comment> Comments { get; set; } = default!;
    }
}
