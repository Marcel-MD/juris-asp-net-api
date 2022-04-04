using Azure.Storage.Blobs.Models;

namespace Juris.Api.IServices;

public interface IBlobService
{
    public Task<Stream> GetBlob(string name);

    public Task<IEnumerable<string>> GetBlobNames();

    public Task<string> UploadBlob(IFormFile file);

    public Task DeleteBlob(string name);
}