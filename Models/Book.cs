using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.API.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; }

        [Required, StringLength(100)]
        public string Author { get; set; }

        [Required, StringLength(13)]
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsAvailable { get; set; } = true;
        public ICollection<BookCategory> BookCategories { get; set; }
        public ICollection<Loan> Loans { get; set; }

    }
}
