using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 

namespace Antoine.Delaby.Ocr.WebApi;

[ApiController]
[Route("[controller]")]
public class OcrController : ControllerBase
{
    private readonly csproj.Ocr _ocr;

    public OcrController(csproj.Ocr ocr)
    {
        _ocr = ocr;
    }

    [HttpPost]
    public async Task<IList<csproj.OcrResult>>
        OnPostUploadAsync([FromForm(Name = "files")] IList<IFormFile> files)
    {
        var images = new List<byte[]>();
        foreach (var formFile in files)
        {
            using var sourceStream = formFile.OpenReadStream();
            using var memoryStream = new MemoryStream();
            sourceStream.CopyTo(memoryStream);
            images.Add(memoryStream.ToArray());
        }

        // Utilisation de ma bibliothèque et retour du résultat
        return _ocr.Read(images);
    }
}