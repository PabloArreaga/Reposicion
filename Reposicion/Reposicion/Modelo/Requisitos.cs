﻿using Microsoft.AspNetCore.Http;
using static Reposicion.Modelo.Valores;

namespace Reposicion.Modelo
{
    public class Requisitos : IRequestModel<string>
    {
        public IFormFile File { get; set; }
        public string Key { get; set; }
        public string Cifrado { get; set; }
        public string Name { get; set; }
        public string Reloj { get; set; }
        public int Ancho { get; set; }
        public int Niveles { get; set; }
    }
}
