using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Reposicion.Lab0;
using Reposicion.Servicios;

namespace Reposicion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ServicioPeli _servicioPeli;
        public MovieController(ServicioPeli servicioPeli)
        {
            _servicioPeli = servicioPeli;
        }

        // GET: api/Movie
        [HttpGet]
        public ActionResult<List<ActuPeliculas>> Get() =>
            _servicioPeli.Get();   

        // POST: api/Movie
        [HttpPost]
        public ActionResult<ActuPeliculas> Post(ActuPeliculas Pelicula)
        {
            _servicioPeli.Post(Pelicula);
            return Ok();
        }
    }
}
