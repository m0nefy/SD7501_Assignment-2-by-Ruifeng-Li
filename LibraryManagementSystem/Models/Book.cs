using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models;

public class Book
{
    public int Id { get; set; }

    [Required, StringLength(120)] public string Title { get; set; } = string.Empty;
    [Required, StringLength(100)] public string Author { get; set; } = string.Empty;
    [StringLength(20)] public string? ISBN { get; set; }
    [Range(0, 9999)] public int TotalCopies { get; set; }
    [Range(0, 9999)] public int AvailableCopies { get; set; }

    [Display(Name = "Category")]
    [Range(1, int.MaxValue, ErrorMessage = "Please choose a category.")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
