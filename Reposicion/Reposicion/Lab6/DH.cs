using System;

namespace Reposicion.Lab6
{
    public class DH
    {
        /// <summary>
        /// Operador utilizado para obtención de llave
        /// </summary>
        /// <param name="KeyB"></param>
        /// <param name="rand"></param>
        /// <param name="primNum"></param>
        /// <returns></returns>
        public double LlaveB(int KeyB, int rand, int primNum)
        {
            double bKey = Math.Pow(rand, KeyB) % primNum;
            return bKey;
        }
        /// <summary>
        /// Operador matemático que retorna llave en común
        /// </summary>
        /// <param name="primNum"></param>
        /// <param name="KeyA"></param>
        /// <param name="KeyB"></param>
        /// <returns></returns>
        public double LlaveSecreta(int primNum, int KeyA, double KeyB)
        {
            double communKey = Math.Pow(KeyB, KeyA) % primNum;
            return communKey;
        }
    }
}
