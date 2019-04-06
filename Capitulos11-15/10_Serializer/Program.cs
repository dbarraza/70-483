using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace _10_Serializer
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Binary Serializer
            Teacher teacher = new Teacher
            {
                ID = 1,
                Name = "Profesor sustituto"
            };

            //Binary Serializer
            BinaryFormatter formatter = new BinaryFormatter();

            //Sample.bin (Binary File is created) to store binary serialized data
            using (FileStream file = File.Open("Sample.bin", FileMode.Create))// new FileStream("Sample.bin",FileMode.Create))
            {
                //this function serialize the teacher (object) into  "file" (file)
                formatter.Serialize(file, teacher);
            }

            Console.WriteLine("Binary Serialization is Succesfully Done!");
            //Binary Deserialization
            using (FileStream file = new FileStream("Sample.bin", FileMode.Open))
            {
                Teacher dteacher = formatter.Deserialize(file) as Teacher;
                Console.WriteLine($"Bin Teacher {dteacher.ID} - {dteacher.Name ?? "noname" }");
            }

            #endregion

            #region XML Serializer with Serializable attribute
            
            XmlSerializer xmlSerializerTeacher = new XmlSerializer(typeof(Teacher));
            using (FileStream file = File.Open("Teacher.XML", FileMode.Create))
            {
                xmlSerializerTeacher.Serialize(file, teacher);
            }

            Teacher xmlTeacher = null;
            using(FileStream stream = File.Open("Teacher.XML",FileMode.Open))
            {
                xmlTeacher = xmlSerializerTeacher.Deserialize(stream) as Teacher;
            }

            Console.WriteLine($"Teacher with non-serializable attribute. Id {xmlTeacher?.ID}. Name {xmlTeacher?.Name}");

            #endregion

            #region XML Serializer with XML Atributes

            //XML Serialize
            var profesor = new List<Profesor>
            {
                new Profesor {
                    ProfesorID = 1,
                    Nombre = "Profesor",
                    Edad = 30,
                    Alumno = new Alumno
                    {
                        AlumnoID = 30,
                        Nombre = "Alumno con rolId 30",
                        RUT = "1111111"
                    }
                },
                new Profesor
                {
                    ProfesorID = 1,
                    Nombre = "Profesor",
                    Edad = 30,
                    Alumno = new Alumno
                    {
                        AlumnoID = 30,
                        Nombre = "Alumno con rolId 30",
                        RUT = "1111111"
                    }
                }
            };

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Profesor>));
            using (var stream = new FileStream("Profesor.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(stream, profesor);
            }
            Console.WriteLine("XML Serialization is done!!");

            List<Profesor> teacherXML = null;
            using (var stream = new FileStream("Profesor.xml", FileMode.Open))
            {
                teacherXML = xmlSerializer.Deserialize(stream) as List<Profesor>;
            }

            Console.WriteLine($"Propiedades de TeacherXML: {teacherXML[0].Nombre} - Su alumno es: {teacherXML[0]?.Alumno?.Nombre}");
            #endregion

            #region DataContract Serializer
            var profesorWcf = new ProfesorWcf
            {
                ProfesorID = 1,
                Nombre = "ProfesorWcf",
                Edad = 30,
                Alumno = new AlumnoWcf
                {
                    AlumnoID = 30,
                    Nombre = "AlumnoWcf",
                    RUT = "1111111"
                }
            };

            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(ProfesorWcf));
            using (var stream = File.Open("ProfesorWcf.xml", FileMode.Create))
            {
                dataContractSerializer.WriteObject(stream, profesorWcf);
            }
            Console.WriteLine("DataContract XML serialize is done!!");

            ProfesorWcf profesorWcfRead = null;
            using (var stream = File.Open("ProfesorWcf.xml", FileMode.Open))
            {
                profesorWcfRead = dataContractSerializer.ReadObject(stream) as ProfesorWcf;
            }

            Console.WriteLine($"Profesor wcf nombre: {profesorWcfRead?.Nombre}");
            #endregion

            #region DataContractJsonSerializer

            var dataContractJsonSerializer = new DataContractJsonSerializer(typeof(ProfesorWcf));

            using (var stream = File.Open("ProfesorWcf.json", FileMode.Create))
            {
                dataContractJsonSerializer.WriteObject(stream, profesorWcf);
            }
            Console.WriteLine("DataContractJsonSerializer is done!!");


            ProfesorWcf profesorWcfJsonRead = null;
            using (var stream = File.Open("ProfesorWcf.json", FileMode.Open))
            {
                profesorWcfJsonRead = dataContractJsonSerializer.ReadObject(stream) as ProfesorWcf;
            }

            Console.WriteLine($"Profesor wcf json nombre: {profesorWcfJsonRead?.Nombre}");
            #endregion

            #region JavaScriptSerialization

            profesorWcf.Nombre = "Profesor JavaScript";
            var javaScriptSerialization = JsonConvert.SerializeObject(profesorWcf);
            Console.WriteLine(javaScriptSerialization);
            using (var streamWtr = new StreamWriter(File.Open("ProfesorJS.json", FileMode.Create)))
            {
                streamWtr.Write(javaScriptSerialization);
            }
            Console.WriteLine("JavaScriptSerialization it's done!!");

            using (var stream = new StreamReader(File.Open("ProfesorJS.json", FileMode.Open)))
            {
                profesorWcfJsonRead = JsonConvert.DeserializeObject<ProfesorWcf>(stream.ReadToEnd());
            }

            Console.WriteLine($"Javascript deserelizer - Profesor wcf json nombre: {profesorWcfJsonRead?.Nombre}");
            #endregion

            decimal a = 2 / 3;
            string s = string.Format("{0:N6}", a);//a.ToString("N6");
            Console.WriteLine(s);

        }
    }

    [Serializable]
    public class Teacher
    {
        public int ID { get; set; }
        
        [NonSerialized]
        public string Name;// { get; set; }
    }

    [XmlRoot]
    public class Profesor
    {
        [XmlAttribute("ID")]
        public int ProfesorID { get; set; }
        [XmlElement("Name")]
        public string Nombre { get; set; }
        [XmlIgnore]
        public int Edad { get; set; }
        [XmlElement("Alumno")]
        public Alumno Alumno { get; set; }
    }

    public class Alumno
    {
        public int AlumnoID { get; set; }
        public string RUT { get; set; }

        public string Nombre { get; set; }
    }

    [DataContract]
    public class ProfesorWcf
    {
        [DataMember]
        public int ProfesorID { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public int Edad { get; set; }


        public AlumnoWcf Alumno { get; set; }
    }

    [DataContract]
    public class AlumnoWcf
    {
        [DataMember]
        public int AlumnoID { get; set; }
        public string RUT { get; set; }

        public string Nombre { get; set; }
    }

    public class ProfesorSerializable : ISerializable
    {
        public int ProfesorID { get; set; }

        public string Nombre { get; set; }

        public int Edad { get; set; }

        public ProfesorSerializable()
        {
            
        }

        protected ProfesorSerializable(SerializationInfo info, StreamingContext context)
        {
            this.ProfesorID = info.GetInt32("IdKey");
            this.Nombre = info.GetString("NameKey");
            this.Edad = info.GetInt32("EdadKey");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("IdKey",1);
            info.AddValue("NameKey", "Daniel");
            info.AddValue("EdadKey",31);
        }
    }
}