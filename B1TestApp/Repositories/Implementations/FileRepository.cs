using B1TestApp.Data;
using B1TestApp.Data.Entity;
using B1TestApp.Repositories.Interfaces;

namespace B1TestApp.Repositories.Implementations;

public class FileRepository : BaseRepository<Files, long>, IFileRepository
{
    public FileRepository(AppDbContext context) : base(context)
    {
    }
}