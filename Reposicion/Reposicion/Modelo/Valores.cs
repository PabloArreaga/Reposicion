﻿using Microsoft.AspNetCore.Http;

namespace Reposicion.Modelo
{
    public class Valores
    {
        public interface IRequestModel<T>
        {
            IFormFile File { get; set; }
            T Key { get; set; }
            string Name { get; set; }
            public string Cifrado { get; set; }
        }
    }
}
