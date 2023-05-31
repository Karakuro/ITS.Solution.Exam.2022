using Microsoft.EntityFrameworkCore;

namespace ITS.Solution.Exam._2022.DAL
{
    public class FoodDbContext : DbContext
    {
        public FoodDbContext() : base() { }

        public FoodDbContext(DbContextOptions<FoodDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
