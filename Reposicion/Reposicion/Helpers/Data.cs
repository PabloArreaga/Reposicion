using System.Collections.Generic;
using Reposicion.Lab0;
using Reposicion.Lab3;

namespace Reposicion.Helpers
{
    public class Data
    {
        private static Data _instance = null;
        public static Data Instance
        {
            get
            {
                if (_instance == null) _instance = new Data();
                {
                    return _instance;
                }
            }
        }
        //Lab0
        public Stack<ActuPeliculas> Peliculas = new Stack<ActuPeliculas>();
        //Lab3
        public List<CaracterCodigo> ListaCod = new List<CaracterCodigo>();
        public Dictionary<string, byte> DicCarcacteres = new Dictionary<string, byte>();
        // Models -> Lab3 y Lab4
        // Modelo -> Lab5
       

    }
}
