using Microsoft.EntityFrameworkCore;
using Project_CL.Data.user;


namespace Project_CL.Data.context
{
    public class Project_CL_Context(DbContextOptions<Project_CL_Context> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Character> Characters { get; set; } = default!;




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(c => c.Characters)
                .WithOne()
                .HasForeignKey(c => c.UserId);


        }
    }
}
