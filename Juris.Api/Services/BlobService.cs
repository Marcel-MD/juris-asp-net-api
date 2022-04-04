using System.Net;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Juris.Api.Exceptions;
using Juris.Api.IServices;
using Juris.Resource;

namespace Juris.Api.Services;

public class BlobService : IBlobService
{
    private readonly BlobContainerClient _containerClient;
    private readonly HashSet<string> _extensions = new(){".jpg", ".jpeg", ".png", ".webp"};

    public BlobService(BlobServiceClient blobClient, IConfiguration configuration)
    {
        var name = configuration.GetValue<string>("BlobContainer");
        _containerClient = blobClient.GetBlobContainerClient(name);
    }

    public async Task<Stream> GetBlob(string name)
    {
        var blobClient = _containerClient.GetBlobClient(name);

        if (!await blobClient.ExistsAsync())
            throw new HttpResponseException(HttpStatusCode.BadRequest, string.Format(GlobalResource.FileNotFound, name));

        return await blobClient.OpenReadAsync();
    }

    public async Task<IEnumerable<string>> GetBlobNames()
    {
        var items = new List<string>();

        await foreach (var blob in _containerClient.GetBlobsAsync())
        {
            items.Add(blob.Name);
        }

        return items;
    }

    public async Task<string> UploadBlob(IFormFile file)
    {
        if (file == null || file.Length is < 1 or > 3000000)
            throw new HttpResponseException(HttpStatusCode.BadRequest, string.Format(GlobalResource.FileSizeLess, 3));
        
        var extension = Path.GetExtension(file.FileName);

        if (!_extensions.Contains(extension))
            throw new HttpResponseException(HttpStatusCode.BadRequest, string.Format(GlobalResource.UnsupportedFileExtension, extension));

        var fileNameInStorage = Guid.NewGuid().ToString() + extension;
        
        var blobClient = _containerClient.GetBlobClient(fileNameInStorage);

        var httpHeaders = new BlobHttpHeaders()
        {
            ContentType = file.ContentType
        };

        var res = await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders);

        if (res == null)
            throw new HttpResponseException(HttpStatusCode.BadRequest, string.Format(GlobalResource.CantUpload, file.Name));
        
        return fileNameInStorage;
    }

    public async Task DeleteBlob(string name)
    {
        var blobClient = _containerClient.GetBlobClient(name);
        await blobClient.DeleteIfExistsAsync();
    }
}