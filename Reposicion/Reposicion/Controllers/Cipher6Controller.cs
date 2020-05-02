using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Reposicion.Lab6;
using System;
using System.IO;
using System.IO.Compression;

namespace Reposicion.Controllers
{
    [Route("api/[controller]/caesar2")]
    [ApiController]
    public class Cipher6Controller : Controller
    {
        [Route("LLAVES")]
        public ActionResult<IEnumerable<string>> PostLLAVES([FromForm]Requisitos Tipos)
        {
            if (Tipos.Primo1 <= 0 || Tipos.Primo2 <=0)
            {
                return BadRequest("El valor de los numeros primos debe ser mayor a 0");
            }

            RSA Keys = new RSA();
            Keys.CreacionLlaves(Tipos.Primo1, Tipos.Primo2);
            return Ok("LLaves creadas con exito");
        }
        [Route("RSA")]
        public ActionResult<IEnumerable<string>> PostRSA([FromForm]Requisitos Tipos)
        {
            if (Tipos.File == null)
            {
                return BadRequest(new string[] { "El valor -File- es inválido" });
            }
            else if (Path.GetExtension(Tipos.File.FileName) != ".txt")
            {
                return BadRequest(new string[] { "Extensión no válida" });
            }
            else if (Tipos.KeyPP == null)
            {
                return BadRequest(new string[] { "El valor -Key- es inválido" });
            }
            else
            {
                using (FileStream thisFile = new FileStream("Mis Cifrados/" + Tipos.File.FileName, FileMode.OpenOrCreate))
                {
                    RSA Rsa = new RSA();
                    Rsa.CifrarDescifrar(thisFile, Tipos.KeyPP);
                }               
            }
            return new string[] { "Cifrado RSA satisfactorio" };
        }

        [Route("DIFFIE-HELLMAN")]
        public ActionResult<IEnumerable<string>> PostDIFFIE([FromForm]Requisitos Tipos)
        {
            if (Tipos.KeyA <= 0)
            {
                return BadRequest("Llave A debe ser mayor a 0");
            }
            DH Diffie = new DH();
            Random rnd = new Random();
            double keyB = Diffie.LlaveB(rnd.Next(10), Tipos.randNum, Tipos.prim);
            double communKey = Diffie.LlaveSecreta(Tipos.prim, Tipos.KeyA, keyB);
            return Ok("Cipher: Diffie-Hellman \nKey: " + communKey);
        }
    }
}