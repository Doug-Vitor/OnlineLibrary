using OnlineLibrary.Data;

namespace OnlineLibrary.Repositories
{
    public abstract class AbstractRepository
    {
        protected readonly AppDbContext _context;

        protected AbstractRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
