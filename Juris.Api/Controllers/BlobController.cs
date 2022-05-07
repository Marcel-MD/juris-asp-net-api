using Juris.Bll.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeTypes;

namespace Juris.Api.Controllers;

[Route("api/blob")]
public class BlobController : BaseController
{
    private readonly IBlobService _service;

    public BlobController(IBlobService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetBlobNames()
    {
        var result = await _service.GetBlobNames();
        return Ok(result);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetBlobUri(string name)
    {
        var stream = await _service.GetBlob(name);
        var mime = MimeTypeMap.GetMimeType(Path.GetExtension(name));
        return File(stream, mime, name);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{name}")]
    public async Task<IActionResult> DeleteBlob(string name)
    {
        await _service.DeleteBlob(name);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> UploadBlob(IFormFile file)
    {
        var result = await _service.UploadBlob(file);
        return Ok(result);
    }
}