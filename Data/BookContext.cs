namespace Kreissl.Showcase.Data
{
    using System.Data.Entity;

    using Kreissl.Showcase.Model;

    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
