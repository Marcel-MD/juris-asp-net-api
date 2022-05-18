using Microsoft.AspNetCore.Http;

namespace Juris.Bll.IServices;

public interface IBlobService
{
    /// <summary>
    ///     Gets file from blob storage as stream of data.
    /// </summary>
    /// <param name="name">Name of the file.</param>
    /// <returns>Stream.</returns>
    public Task<Stream> GetBlob(string name);

    /// <summary>
    ///     Gets the list of file names in blob storage.
    /// </summary>
    /// <returns>List of file names.</returns>
    public Task<IEnumerable<string>> GetBlobNames();

    /// <summary>
    ///     Uploads a file to blob storage.
    /// </summary>
    /// <param name="file">Form file to upload.</param>
    /// <returns>Name of the file in blob storage.</returns>
    public Task<string> UploadBlob(IFormFile file);

    /// <summary>
    ///     Deletes file by file name.
    /// </summary>
    /// <param name="name">Name of the file.</param>
    public Task DeleteBlob(string name);
}