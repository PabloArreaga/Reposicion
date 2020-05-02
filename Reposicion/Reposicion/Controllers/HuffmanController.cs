using System.IO;
using Microsoft.AspNetCore.Mvc;
using Reposicion.Lab3;
using Microsoft.AspNetCore.Http;

namespace Reposicion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HuffmanController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromForm(Name = "file")] IFormFile File)
        {
            try
            {
                var extensionTipo = Path.GetExtension(File.FileName);
                CompressHuffman HuffmanCompress = new CompressHuffman();
                if (extensionTipo == ".txt")
                {
                    using (FileStream thisFile = new FileStream("TusArchivos/" + File.FileName, FileMode.OpenOrCreate))
                    {
                        HuffmanCompress.CompresionHuffmanImportar(thisFile);
                    }
                }
                else if (extensionTipo == ".huff")
                {
                    using (FileStream thisFile = new FileStream("TusArchivos/" + File.FileName, FileMode.OpenOrCreate))
                    {
                        HuffmanCompress.CompresionHuffmanExportar(thisFile);
                    }
                }
                else { return NotFound(); }
                return Ok();
            }
            catch (System.NullReferenceException)//No se envia nada
            {
                return NotFound();
            }
        }
    }
}