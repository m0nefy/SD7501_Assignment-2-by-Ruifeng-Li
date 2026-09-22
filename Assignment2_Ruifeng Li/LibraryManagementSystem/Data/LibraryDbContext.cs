using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data;

public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Loan> Loans => Set<Loan>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasIndex(b => b.ISBN).IsUnique().HasFilter("[ISBN] IS NOT NULL");
        modelBuilder.Entity<Book>().HasOne(b => b.Category).WithMany(c => c.Books)
            .HasForeignKey(b => b.CategoryId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Loan>().Property(l => l.Status).HasConversion<string>();
    }
}
