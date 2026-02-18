# LibraryManagementSystem.API

# This project is the API layer of the Library Management System. It provides RESTful endpoints for managing books, authors, and users.

- For Many-Many relationships: 
	
	In previous versions of Entity Framework, this model definition was sufficient for EF to imply the correct type of relationship and to 
    generate the join table for it. In EF Core up to and including 3.x, it is necessary to include an entity in the model to represent the
    join table, and then add navigation properties to either side of the many-to-many relationships that point to the joining entity instead:
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
    }  
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
    }  
    public class BookCategory
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }


    The join table will be named after the joining entity (BookCategory in this case) by convention. The relationship also needs to 
    be configured via the Fluent API for EF Core to be able to map it successfully:

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookCategory>()
            .HasKey(bc => new { bc.BookId, bc.CategoryId });  
        modelBuilder.Entity<BookCategory>()
            .HasOne(bc => bc.Book)
            .WithMany(b => b.BookCategories)
            .HasForeignKey(bc => bc.BookId);  
        modelBuilder.Entity<BookCategory>()
            .HasOne(bc => bc.Category)
            .WithMany(c => c.BookCategories)
            .HasForeignKey(bc => bc.CategoryId);
    }

    
    The primary key for the join table is a composite key comprising both of the foreign key values. In addition, both sides of the many-to-many 
    relationship are configured using the HasOne, WithMany, and HasForeignKey Fluent API methods.

    This is sufficient if you want to access book category data via the Book or Category entities. If you want to query BookCategory data directly, 
    you should also add a DbSet for the join table to the context:

    public class SampleContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
    }
