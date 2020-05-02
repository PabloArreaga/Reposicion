using System.Collections.Generic;
using System.Linq;
using Reposicion.Lab0;
using Reposicion.Helpers;

namespace Reposicion.Servicios
{
    public class ServicioPeli
    {
    public List<ActuPeliculas> Get()
    {
        var listaPeliculas = new List<ActuPeliculas>();
        if (Data.Instance.Peliculas.Count() > 0)
        {
            for (int i = 1; i <= Data.Instance.Peliculas.Count(); i++)
            {
                int j = i-1;
                if (listaPeliculas.Count() == 10)
                {
                    return listaPeliculas;
                }
                listaPeliculas.Add(Data.Instance.Peliculas.ElementAt(j));
            }
        }
    return listaPeliculas;
    }
    public void Post(ActuPeliculas miPelicula)
        {
            Data.Instance.Peliculas.Push(miPelicula);
        }
    }
}
