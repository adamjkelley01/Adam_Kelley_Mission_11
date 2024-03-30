using Microsoft.EntityFrameworkCore.Query;

namespace Adam_Kelley_Mission_11.Models
{
    public interface IBookStoreRepository
    {
        public IQueryable<Book> Books { get; }
    }
}
