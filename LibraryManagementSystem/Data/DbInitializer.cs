using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
        await db.Database.EnsureCreatedAsync();
        if (await db.Categories.AnyAsync()) return;
        var fiction = new Category { Name = "Fiction", Description = "Novels and imaginative works" };
        var technology = new Category { Name = "Technology", Description = "Programming and computing" };
        var history = new Category { Name = "History", Description = "Historical studies" };
        db.Categories.AddRange(fiction, technology, history);
        db.Books.AddRange(
            new Book { Title = "The Midnight Library", Author = "Matt Haig", ISBN = "9780525559474", TotalCopies = 4, AvailableCopies = 4, Category = fiction },
            new Book { Title = "Clean Code", Author = "Robert C. Martin", ISBN = "9780132350884", TotalCopies = 3, AvailableCopies = 2, Category = technology },
            new Book { Title = "A Brief History of Time", Author = "Stephen Hawking", ISBN = "9780553380163", TotalCopies = 2, AvailableCopies = 2, Category = history });
        await db.SaveChangesAsync();
    }
}
