namespace Kreissl.Showcase.Data
{

    using System.Data.Entity;
    using System.Linq;
    
    using Kreissl.Showcase.Model;

    public class BookContextInitializer : DropCreateDatabaseIfModelChanges<BookContext>
    {
        protected override void Seed(BookContext context)
        {
            context.Categories.Add(new Category { Name = "Trash" });
            context.Categories.Add(new Category { Name = "Science Fiction" });
            context.Categories.Add(new Category { Name = "Politics" });
            context.Categories.Add(new Category { Name = "History" });

            context.SaveChanges();

            context.Books.Add(new Book() { Autor = "John Doe", Category = context.Categories.Last(), Title = "The history of history." });
            context.Books.Add(new Book() { Autor = "Walter Simpson", Category = context.Categories.Last(), Title = "Another history book." });
            context.Books.Add(new Book() { Autor = "Paul Scott", Category = context.Categories.Last(), Title = "World War III." });

            context.SaveChanges();
        }

    }
}
