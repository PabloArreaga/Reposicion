using System;
using static Reposicion.Lab6.Valores;
using Microsoft.AspNetCore.Http;

namespace Reposicion.Lab6
{
    public class Requisitos : IRequestModel
    {
        public int KeyA { get; set; }
        public int prim { get; set; } = 107;
        public int randNum { get; set; } = 43;
        // RSA
        public int Primo1 { get; set; }
        public int Primo2 { get; set; }
        public string KeyPP { get; set; }
        public int KeyCesar { get; set; }
        public IFormFile File { get; set; }
        public IFormFile FileKey { get; set; }

    }
}
