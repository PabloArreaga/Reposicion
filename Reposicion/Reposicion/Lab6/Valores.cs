using Microsoft.AspNetCore.Http;
namespace Reposicion.Lab6
{
    public class Valores
    {
        public interface IRequestModel
        {
            int KeyA { get; set; }
            int prim { get; set; }
            int randNum { get; set; }
             // RSA
            int Primo1 { get; set; }
            int Primo2 { get; set; }
            string KeyPP { get; set; }
            int KeyCesar { get; set; }
            IFormFile File { get; set; }
            IFormFile FileKey { get; set; }
        }
    }
}

