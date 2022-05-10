using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace Juris.Bll.IServices;

public interface IBlobService
{
    public Task<Stream> GetBlob(string name);

    public Task<IEnumerable<string>> GetBlobNames();

    public Task<string> UploadBlob(IFormFile file);

    public Task DeleteBlob(string name);
}