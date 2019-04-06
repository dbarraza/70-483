using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Security;
using System.Runtime.InteropServices.;

namespace _13_Hashing
{
    class Program
    {4444444444444444444
        static void Main(string[] args)
        {

            #region Hashing
            //Pasos:
            //      - Obtener los bytes del Hash
            //      - Crear el algoritmo Hash
            //      - Crear el Hash en bytes
            //      - Transformar el arreglo de bytes en string
            
            var pass = "myPassword123";
            
            //password in  bytes
            var passBytes = Encoding.UTF8.GetBytes(pass);

            //Create the SHA512
            HashAlgorithm sha512 = SHA512.Create();

            //Generate the Hash
            byte[] hashInBytes = sha512.ComputeHash(passBytes);

            var hashedData = new StringBuilder();
            foreach (var item in hashInBytes)
            {
                hashedData.Append(item);
            }

            //En este caso no es posible generar el string decodificando el arreglod de bytes a un string
            var stringAgain = Encoding.UTF8.GetString(hashInBytes);
            Console.WriteLine("Hashed Password is: " + stringAgain);

            Console.WriteLine("Hashed Password is: " + hashedData.ToString());
            #endregion
        
            #region Salt Hashing
            Console.WriteLine("\n==========  Salt Hashing  =====================");
            var password2 = "MyPa55W0rd2";

            //Crear salt
            Guid salt = Guid.NewGuid();

            //Pasar de string a byte[]
            var bytePass2 = Encoding.UTF8.GetBytes(password2 + salt);

            //Crear algoritmo hash
            SHA512 alg = SHA512.Create();

            //Obtener hash de bytes
            var pass2Hashed = alg.ComputeHash(bytePass2);

            //Crear string de salida
            var strBuilder = new StringBuilder();
            foreach (var item in pass2Hashed)
            {
                strBuilder.Append(item);
            }

            Console.WriteLine($"Hashing password2: {strBuilder.ToString()}");

            #endregion

            #region Working with SecureString
            Console.WriteLine("\n==================  Working with SecureString  ====================");

            using(var secureStr = new SecureString())
            {           
                foreach (var c in password2.ToCharArray())
                {
                    secureStr.AppendChar(c);    
                }

                Console.WriteLine(secureStr.ToString());
            }
            
            #endregion
        }
    }
}
