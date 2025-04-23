using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models.Entities;

namespace TodoApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<Priority> Priorities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ToDoList>()
                .HasOne(tdl => tdl.Category)
                .WithMany(c => c.ToDoLists)
                .HasForeignKey(tdl => tdl.CategoryId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ToDoList>()
                .HasMany(tdl => tdl.ToDoItems)
                .WithOne(tdi => tdi.ToDoList)
                .HasForeignKey(tdi => tdi.ToDoListId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<ToDoItem>()
                .HasOne(tdi => tdi.Priority)
                .WithMany(p => p.ToDoItems)
                .HasForeignKey(tdi => tdi.PriorityId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<ToDoItem>()
                .HasOne(tdi => tdi.Category)
                .WithMany()
                .HasForeignKey(tdi => tdi.CategoryId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<ToDoList>()
                .HasOne(tdl => tdl.User)
                .WithMany()
                .HasForeignKey(tdl => tdl.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
