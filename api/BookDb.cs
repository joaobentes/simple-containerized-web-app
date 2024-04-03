using BookApi;
using Microsoft.EntityFrameworkCore;

class BookDb : DbContext
{
  public BookDb(DbContextOptions<BookDb> options) : base(options)
  {
  }

  public DbSet<Book> Books { get; set; }
}