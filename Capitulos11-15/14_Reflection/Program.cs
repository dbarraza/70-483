using System;
using System.Reflection;

namespace _14_Reflection
{
    class Program
    {
        public int Age { get; set; }
        public string Name { get; set; }

        static void Main(string[] args)
        {
            #region Use Reflection to Read Current Assembly

            Console.WriteLine("\n=================== Use Reflection to Read Current Assembly ===================");

            //Get current loaded assembly
            Assembly assembly = Assembly.GetExecutingAssembly();

            //Get Full Name of the current Assembly
            string assemblyName = assembly.FullName;

            Console.WriteLine(assemblyName);
            #endregion

            #region Use Reflection to Read all Types of an Assembly
            Console.WriteLine("\n=================== Use Reflection to Read all Types of an Assembly ===================");

            //Get current loaded assembly
            Assembly assembly2 = Assembly.GetExecutingAssembly();

            //Types
            Type[] types = assembly2.GetTypes();

            //Get all types defined in a assembly
            for (var i = 0; i < types.Length; i++)
            {
                Type item = types[i];
                //Return name of the type and its base type
                Console.WriteLine($"Type Name:{item.Name}, Base Type:{item.BaseType}");
            }

            #endregion

            #region Use Reflection to Read Metadata of Properties and Methods
            Console.WriteLine("\n============================ Use Reflection to Read Metadata of Properties and Methods =================================");


            //Dig information of each type
            for (var i = 0; i < types.Length; i++)
            {
                Type type = types[i];
                //Return name of a type
                Console.WriteLine("Type Name:{0}, Base Type:{1}",
                type.Name, type.BaseType);
                
                //Get all properties defined in a type
                var properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    Console.WriteLine("\t{0} has {1} type",property.Name, property.PropertyType);
                }

                //Get all non-static methods of a type
                MethodInfo[] methods = type.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    Console.WriteLine("\tMethod Name:{0}, Return Type:{1}", method.Name, method.ReturnType);
                }

                #endregion

            }
        
            #region Use Reflection to Get and Set Value of Object's Property

            Console.WriteLine("\n==================  Use Reflection to Get and Set Value of Object's Property   ==================");

            //Create two Person object
            var person1 = new Person{ Age = 21, Name = "Persona 1"};
            var person2 = new Person{ Age = 21, Name = "Persona 2"};

            //Store Metadata of Person Type in Type's object
            var personType = typeof(Person);
            Console.WriteLine(personType);

            //Get Metadata
            var propertyInfo = personType.GetProperty("Name");

            //Specify the instance from which you will get the value
            var value = propertyInfo.GetValue(person1);
            Console.WriteLine($"Person1:   Property name {propertyInfo.Name} and the value is {value}");

            var value2 = propertyInfo.GetValue(person2);
            Console.WriteLine($"Person2:   Property Name {propertyInfo.Name} - Value {value2}");

            propertyInfo.SetValue(person2, "Daniel Barraza");
            value2 = propertyInfo.GetValue(person2);
            Console.WriteLine($"Persona2: new value for property '{propertyInfo.Name}' is '{value2}'");

            #endregion

            #region Use Reflection to Invoke a Method of an Object

            Console.WriteLine("\n======================  Use Reflection to Invoke a Method of an Object  =================");

            var methodInfo = personType.GetMethod("Greeting");
            methodInfo.Invoke(person1,null); //Si el método no recibe parámetros, se debe pasar un null

            #endregion

            #region Use Reflection to Get Private Members
            Console.WriteLine("\n===========  Use Reflection to Get Private Members   ==================");

            var propertiesInfo = personType.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
            Console.WriteLine(propertiesInfo.Length);
            foreach (PropertyInfo prop in propertiesInfo)
            {
                Console.WriteLine("{0} = {1}", prop.Name, prop.GetValue(person1));
                Console.WriteLine("{0} = {1}", prop.Name, prop.GetValue(person2));
            }
            
            #endregion

            #region Use Reflection to Get Static Members
            Console.WriteLine("\n==================   Use Reflection to Get Static Members  =================");

            var fieldsInfo = personType.GetFields();
            foreach (FieldInfo prop in fieldsInfo)
            {
                Console.WriteLine("{0} = {1}", prop.Name, prop.GetValue(person1));
                Console.WriteLine("{0} = {1}", prop.Name, prop.GetValue(person2));
            }

            #endregion
        }
    }

        class A
        {
            public int Random { get; set; }
        }
        class B : A { }

        class Person
        {
            private int id {get;set;}

            public static string company = "Microsoft";

            public int Age { get; set; }
            public string Name { get; set; }

            public void Greeting()
            {
                Console.WriteLine($"Hello my name is {Name}");
            }
        }

    }