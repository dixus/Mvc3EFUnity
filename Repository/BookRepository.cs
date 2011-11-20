namespace Kreissl.Showcase.Repository
{
    using Kreissl.Showcase.Data.Interfaces;

    using Kreissl.Showcase.Infrastructure;
    using Kreissl.Showcase.Model;
    using Kreissl.Showcase.Repository.Interfaces;

    [Service(typeof(IBookRepository), ServiceBehaviour.Webrequest)]
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
