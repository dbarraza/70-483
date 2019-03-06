using static System.Console;
using System.IO;

namespace _10_Reader
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Working With Drive
                
            WriteLine("================   Working With Drive   ===============");

            //One Drive using DriveInfo
            DriveInfo driveInfo = new DriveInfo(@"C:\");
            WriteLine($"Name {driveInfo.Name}");
            WriteLine($"Drive Type is {driveInfo.DriveType}");

            WriteLine();

            //More One Drive using DriveInfo
            DriveInfo[] driveInfoArg = DriveInfo.GetDrives();
            foreach (var item in driveInfoArg)
            {
                WriteLine($"Name {item.Name}");
                WriteLine($"Drive Type is {item.DriveType}");
                WriteLine();
            }

            #endregion  

            #region Working with Directories

            WriteLine("================   Working With Directories   ===============");

            //Create new directory/folder using Directory Class (if exist wont be created)
            DirectoryInfo directory = Directory.CreateDirectory("Directory Folder");

            //Create new directory/folder using DirectoryInfo Class (if exist wont be created)
            DirectoryInfo directoryInfo = new DirectoryInfo("DirectoryInfo Folder");
            directoryInfo.Create();

            //Check existence directory suing Directory
            if(Directory.Exists("DirectoryInfo Folder"))
            {
                WriteLine("Existe el directorio [Directory]");
            }

            WriteLine();

            //Check existence directory suing DirectoryInfo
            if(directoryInfo.Exists)
            {
                WriteLine("Existe el directorio [DirectoryInfo]");
            }

            WriteLine();
            #endregion

            #region Working with Files from Directories
                
            WriteLine("==================    Working with Directory and Files    ===================");
            //Get files from specific folder using Directory class
            WriteLine("Using Direcotry class");
            var fileNames = Directory.GetFiles("Directory Folder");
            foreach (var item in fileNames)
            {
                WriteLine(item);
            }

            WriteLine();

            //Get files from specific folder using DiretoryInfo class
            var directoryInfo2 = new DirectoryInfo("DirectoryInfo Folder");
            var filesInfo = directoryInfo2.GetFiles();
            foreach (var file in filesInfo)
            {
                WriteLine("Name is: {0}", file.Name);
                WriteLine("Directory Name: {0}", file.DirectoryName);
            }
            #endregion

            #region File and FileInfo

            WriteLine("==================    Working with Files    ===================");

            var fileName = @"Directory Folder\File.txt";
            var fileInfo = @"DirectoryInfo Folder\FileInfo.txt";
            var fileCopied = @"Directory Folder\CopiedFile.txt";

            //To Create a file in current location named "File" using File(Static Class)
            File.Create(fileName).Close();
            //To Write content in a file named "File"
            File.WriteAllText(fileName, "This is file created by File Class");
            //To Read the file named "File"
            var fileContent = File.ReadAllText(fileName);
            WriteLine(fileContent);
            //To Copy "File"
            if(!File.Exists(fileCopied))
                File.Copy(fileName,fileCopied);
            //To Create
            FileInfo info = new FileInfo(fileInfo);
            info.Create();
            #endregion

            #region Working with Stream
            WriteLine("======================   Working with Stream   =========================");

            //StringWriter and StringReader
            WriteLine("\nBinaryReader and BinaryWriter\n");
            StringWriter strWtr = new StringWriter();
            strWtr.Write("testeando StrinWriter");
            strWtr.WriteLine("Texto aggredado");
            strWtr.WriteLine("Nueva Línea");
            WriteLine(strWtr.ToString());

            StringReader strRdr = new StringReader("\nCadena de caracteres");
            WriteLine(strRdr.ReadLine());
            WriteLine(strRdr.ReadLine());

            //BinaryReader and BinaryWriter
            WriteLine("\nBinaryReader and BinaryWriter\n");
            FileStream fileSample = File.Create("Sample.dat");
            BinaryWriter bryWtr = new BinaryWriter(fileSample);
            bryWtr.Write("String Value");
            bryWtr.Write('A');
            bryWtr.Write(true);
            bryWtr.Close();

            FileStream fileStrOpen = File.Open("Sample.dat", FileMode.Open);
            BinaryReader bryRdr = new BinaryReader(fileStrOpen);
            WriteLine(bryRdr.ReadString());
            WriteLine(bryRdr.ReadChar());
            WriteLine(bryRdr.ReadBoolean());

            //StreamReader and StreamWriter
            WriteLine("\nStreamReader and StreamWriter\n");
            StreamWriter streamWtr = new StreamWriter("Sample.txt");
            streamWtr.Write('A');
            streamWtr.Close();

            StreamReader streamRdr = new StreamReader("Sample.txt");
            WriteLine(streamRdr.ReadLine());


            #endregion

        }
    }
}
