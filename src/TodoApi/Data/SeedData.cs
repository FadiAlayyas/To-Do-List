using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models.Entities;

namespace TodoApi.Data;
public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();

        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // Seed roles
        await SeedRolesAsync(roleManager);

        // Seed users
        await SeedUsersAsync(userManager);

        // Seed other tables (Categories, Priorities, etc.)
        await SeedCategoriesAsync(context);
        await SeedPrioritiesAsync(context);
        await SeedToDoListsAsync(context);
        await SeedToDoItemsAsync(context);

        await context.SaveChangesAsync();
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        var roles = new[] { "Owner", "Guest" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    private static async Task SeedUsersAsync(UserManager<User> userManager)
    {
        var ownerEmail = "owner@todo.com";
        var ownerUser = await userManager.FindByEmailAsync(ownerEmail);
        if (ownerUser == null)
        {
            ownerUser = new User
            {
                FirstName = "Owner",
                LastName = "User",
                Email = ownerEmail,
                UserName = ownerEmail
            };

            await userManager.CreateAsync(ownerUser, "Owner@123");
            await userManager.AddToRoleAsync(ownerUser, "Owner");
        }

        var guestEmail = "guest@todo.com";
        var guestUser = await userManager.FindByEmailAsync(guestEmail);
        if (guestUser == null)
        {
            guestUser = new User
            {
                FirstName = "Guest",
                LastName = "User",
                Email = guestEmail,
                UserName = guestEmail
            };

            await userManager.CreateAsync(guestUser, "Guest@123");
            await userManager.AddToRoleAsync(guestUser, "Guest");
        }
    }

    private static async Task SeedCategoriesAsync(ApplicationDbContext context)
    {
        if (!context.Categories.Any())
        {
            context.Categories.AddRange(
                new Category { Name = "Work" },
                new Category { Name = "Personal" },
                new Category { Name = "Shopping" }
            );
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedPrioritiesAsync(ApplicationDbContext context)
    {
        if (!context.Priorities.Any())
        {
            context.Priorities.AddRange(
                new Priority { Name = "Low" },
                new Priority { Name = "Medium" },
                new Priority { Name = "High" }
            );
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedToDoListsAsync(ApplicationDbContext context)
    {
        if (!context.ToDoLists.Any())
        {
            var categories = await context.Categories.ToListAsync();
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == "owner@todo.com");

            if (user == null) return;

            context.ToDoLists.AddRange(
                new ToDoList
                {
                    Title = "Work Tasks",
                    UserId = user.Id,
                    CategoryId = categories.First(c => c.Name == "Work").Id,
                    CreatedAt = DateTime.UtcNow
                },
                new ToDoList
                {
                    Title = "Personal Tasks",
                    UserId = user.Id,
                    CategoryId = categories.First(c => c.Name == "Personal").Id,
                    CreatedAt = DateTime.UtcNow
                }
            );

            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedToDoItemsAsync(ApplicationDbContext context)
    {
        if (!context.ToDoItems.Any())
        {
            var todoList = await context.ToDoLists.FirstOrDefaultAsync(tdl => tdl.Title == "Work Tasks");
            if (todoList == null) return;

            var priorities = await context.Priorities.ToListAsync();
            var categories = await context.Categories.ToListAsync();

            context.ToDoItems.AddRange(
                new ToDoItem
                {
                    Title = "Finish project report",
                    Description = "Complete the monthly project report and send it to the team.",
                    IsCompleted = false,
                    DueDate = DateTime.UtcNow.AddDays(5),
                    ToDoList = todoList, // Set the ToDoList navigation property
                    Priority = priorities.First(p => p.Name == "High"), // Set the Priority navigation property
                    Category = categories.First(c => c.Name == "Work"), // Set the Category navigation property
                    CreatedAt = DateTime.UtcNow
                },
                new ToDoItem
                {
                    Title = "Prepare presentation",
                    Description = "Prepare a presentation for the upcoming meeting.",
                    IsCompleted = false,
                    DueDate = DateTime.UtcNow.AddDays(2),
                    ToDoList = todoList, // Set the ToDoList navigation property
                    Priority = priorities.First(p => p.Name == "Medium"), // Set the Priority navigation property
                    Category = categories.First(c => c.Name == "Work"), // Set the Category navigation property
                    CreatedAt = DateTime.UtcNow
                }
            );

            await context.SaveChangesAsync();
        }
    }

}

