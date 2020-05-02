using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.IO.Compression;
using System.Text;
using Microsoft.AspNetCore.Http;
using Reposicion.Lab5;

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
            string llavePrivada = e.ToString() + "," + N.ToString();
            CreateFile(llavePrivada, "Llave_Privada");
            string llavePublica = inverso.ToString() + "," + N.ToString();
            CreateFile(llavePublica, "Llave_Publica");
            // llave publica ( e , N )
            // llave privada ( inverso , N )
        }

        public void CreateFile(string textoResultante, string tipo)
        {
            using (FileStream Archivo = File.Create(@"RSA\" + tipo + ".txt"))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(textoResultante);
                Archivo.Write(info, 0, info.Length);
                byte[] data = new byte[] { 0x0 };
                Archivo.Write(data, 0, data.Length);
            }
        }
        public void CompressFile()
        {
            string startPath = @"RSA";
            string zipPath = @"RSA.zip";
            if (File.Exists(zipPath))
            {
                File.Delete(zipPath);
            }
            System.IO.Compression.ZipFile.CreateFromDirectory(startPath, zipPath);
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

        public void CifrarDescifrar(IFormFile ArchivoImportado, IFormFile ArchivoLlave,  string Llave, int LlaveCesar)
        {
            string[] claves = Llave.Split(',');
            int e = int.Parse(claves[0]);
            BigInteger mod = new BigInteger(int.Parse(claves[1]));

            if (ArchivoLlave == null) // Cifrar
            {
                using (FileStream thisFile = new FileStream("Mis Cifrados/" + ArchivoImportado, FileMode.OpenOrCreate))
                {
                    Cesar Cesar = new Cesar();
                    //Archivo-Llave-Desifrado
                    Cesar.TodoCesar(thisFile, LlaveCesar, "Cifrado");
                }
                byte[] llaveC = Encoding.ASCII.GetBytes(LlaveCesar.ToString());
                using (var writeStream = new FileStream(("Mis Cifrados/" + "Llave_Cif.txt"), FileMode.OpenOrCreate))
                {
                    foreach (var item in llaveC)
                    {
                        BigInteger valor = new BigInteger(item);
                        byte n = Convert.ToByte(Metodo(valor, e, mod));
                        writeStream.WriteByte(n);
                    }
                }
                
            }
            else // Descifrar
            {
                var archivoByte = new byte[ArchivoLlave.Length];

                using (FileStream KeyFile = new FileStream("Mis Cifrados/" + ArchivoLlave.FileName, FileMode.OpenOrCreate))
                {
                    var nombreArchivo = Path.GetFileNameWithoutExtension(KeyFile.Name);
                    var extrencion = Path.GetExtension(KeyFile.Name);
                    var i = 0;
                    using (var lectura = new BinaryReader(KeyFile))
                    {
                        while (lectura.BaseStream.Position != lectura.BaseStream.Length)
                        {
                            BigInteger valor = new BigInteger(lectura.ReadByte());
                            byte n = Convert.ToByte(Metodo(valor, e, mod));
                            archivoByte[i] = n;
                            i++;

                        }
                    }
                }
                var clave = System.Text.Encoding.Default.GetString(archivoByte);
                var key = Int32.Parse(clave);

                using (FileStream thisFile = new FileStream("Mis Cifrados/" + ArchivoImportado.FileName, FileMode.OpenOrCreate))
                {
                    Cesar Cesar = new Cesar();
                    //Archivo-Llave-Desifrado
                    Cesar.TodoCesar(thisFile, key, "Desifrado");
                }

               
            }


            //string nombreArchivo = Path.GetFileNameWithoutExtension(ArchivoImportado.Name);
            //ArchivoImportado.Close();
            //using (FileStream archivo = new FileStream("Mis Cifrados/" + nombreArchivo + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            //{
            //    var bufferLength = 80;
            //    var buffer = new byte[bufferLength];
            //    using (var file = new FileStream(nombreArchivo, FileMode.Open))
            //    {
            //        using (var reader = new BinaryReader(file))
            //        {
            //            while (reader.BaseStream.Position != reader.BaseStream.Length)
            //            {
            //                buffer = reader.ReadBytes(bufferLength);
            //                foreach (var item in buffer)
            //                {
            //                    BigInteger valor = new BigInteger(item);
            //                    archivo.WriteByte(Convert.ToByte(Metodo(valor, e, mod)));
            //                }
            //            }
            //            reader.ReadBytes(bufferLength);
            //        }
            //    }
            //}

        }
        private string Metodo(BigInteger original, int e, BigInteger mod)
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
            return bytecif.ToString();
        }
    }
}
