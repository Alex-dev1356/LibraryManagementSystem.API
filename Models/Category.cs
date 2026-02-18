namespace LibraryManagementSystem.API.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategotyName { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
    }
}
