using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using B1TestApp.Data.Entity;
using B1TestApp.Repositories.Interfaces;
using B1TestApp.Services.Interfaces;

namespace B1TestApp.Services.Implementations;

public class FileService(IUnitOfWork unitOfWork) : IFileSerivce
{
    public async Task<IEnumerable<string>> GetFilesNamesAsync(CancellationToken cancellationToken = default)
    {
        var files = await unitOfWork.Files.GetAllAsync();
        var fileNames = files.Select(f => f.Name);
        return fileNames;
    }
}