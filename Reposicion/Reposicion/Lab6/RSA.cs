using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Reposicion.Lab6
{
    public class RSA
    {
        public void CreacionLlaves(int primo1, int primo2)
        {
            int phi = (primo1 - 1) * (primo2 - 1);
            int N = primo1 * primo2;
            int e = Encontare(phi, (phi - 2));
            int inverso = InversoMultiplicativo(phi, e, 1, 0, 0, 1, 0, 0, 0, 0, 0);
            if (inverso < 0)
            {
                inverso = phi + inverso;
            }
            string[] llavePrivada = { e.ToString() + "," + N.ToString() };
            CreateFile(llavePrivada, "Llave_Privada");
            string[] llavePublica = { inverso.ToString() + "," + N.ToString() };
            CreateFile(llavePublica, "Llave_Publica");
            // llave publica ( e , N )
            // llave privada ( inverso , N )
        }

        public void CreateFile(string[] textoResultante, string tipo)
        {
            using (var Archivo = new StreamWriter(@"RSA\" + tipo + ".txt", true))
            {
                foreach (var item in textoResultante)
                {
                    Archivo.Write(item);
                }
            }
        }

        public FileResult Download(List<string> files)
        {
            var archive = "~/archive.zip";
            var temp = "~/temp";

            // clear any existing archive
            if (System.IO.File.Exists(archive))
            {
                System.IO.File.Delete(archive);
            }
            // empty the temp folder
            Directory.EnumerateFiles(temp).ToList().ForEach(f => System.IO.File.Delete(f));

            // copy the selected files to the temp folder
            files.ForEach(f => System.IO.File.Copy(f, Path.Combine(temp, Path.GetFileName(f))));

            // create a new archive
            ZipFile.CreateFromDirectory(temp, archive);

            return File(archive, "application/zip", "archive.zip");
        }

        private FileResult File(string archive, string v1, string v2)
        {
            throw new NotImplementedException();
        }

        public int Encontare(int phi, int e)
        {
            bool verificacion = VerificacionCoPrimos(phi, e);
            if (!verificacion)
            {
                return Encontare(phi, e - 1);
            }
            return e;
        }

        public bool VerificacionCoPrimos(int phi, int e)
        {
            int sumando = phi % e;
            if (sumando != 0)
            {
                return VerificacionCoPrimos(e, sumando);
            }
            if (e == 1)
            {
                return true;
            }
            return false;
        }
        public int InversoMultiplicativo(int g0, int g1, int u0, int u1, int v0, int v1, int iteracion, int entero, int ag, int au, int av)
        {
            if (g1 != 0)
            {
                entero = g0 / g1;
                ag = g1;
                g1 = g0 - (entero * g1);
                au = u1;
                u1 = u0 - (entero * u1);
                av = v1;
                v1 = v0 - (entero * v1);
                iteracion++;
                return InversoMultiplicativo(ag, g1, au, u1, av, v1, iteracion, entero, ag, au, av);
            }
            return v0;
        }

        // pendiente la validacion si una de las llaves es mayor a 255
        public void CifrarDescifrar(FileStream ArchivoImportado, string Llave)
        {
            string[] claves = Llave.Split(',');
            int e = int.Parse(claves[0]);
            BigInteger mod = new BigInteger(int.Parse(claves[1]));
            string nombreArchivo = Path.GetFileNameWithoutExtension(ArchivoImportado.Name);

            using (FileStream archivo = new FileStream("Mis Cifrados/" + nombreArchivo + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                var bufferLength = 80;
                var buffer = new byte[bufferLength];
                using (var file = new FileStream(nombreArchivo, FileMode.Open))
                {
                    using (var reader = new BinaryReader(file))
                    {
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            buffer = reader.ReadBytes(bufferLength);
                            foreach (var item in buffer)
                            {
                                BigInteger valor = new BigInteger(item);
                                archivo.WriteByte(Convert.ToByte(Metodo(valor, e, mod)));
                            }
                        }
                        reader.ReadBytes(bufferLength);
                    }
                }
            }

        }
        private BigInteger Metodo(BigInteger original, int e, BigInteger mod)
        {
            BigInteger bytecif = new BigInteger();
            bytecif = original;
            BigInteger div = 0;
            bytecif = BigInteger.Pow(original, e);
            div = bytecif / mod;
            bytecif = bytecif - (div * mod);
            if (bytecif == 0)
            {
                bytecif = mod;
            }
            return bytecif;
        }
    }
}
