using System;
using System.Collections.Generic;
using System.Reflection;

namespace ObjectToConsole
{
    public static class OTC
    {
        static string space = "";
        public static void ObjectToConsole<T>(T obj, bool child)
        {
            Type type = typeof(T);
            PropertyInfo[] properties;

            if (child)
                space += "    ";
            else
                space = "";

            properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType.Equals(typeof(int))
                    || property.PropertyType.Equals(typeof(string))
                    || property.PropertyType.Equals(typeof(float))
                    || property.PropertyType.Equals(typeof(bool)))
                {


                    Console.WriteLine($"{space}{property.Name}: {property.GetValue(obj, null)}");
                }
                else
                {
                    Console.WriteLine($"{property.Name}");
                    var tt = property.GetValue(obj, null);
                    MethodInfo method = typeof(OTC).GetMethod("ObjectToConsole");
                    MethodInfo generic = method.MakeGenericMethod(tt.GetType());
                    generic.Invoke(null, new object[2] { tt, true });
                    space = "";
                }
            }

        }

        public static void ObjectListToConsole<T>(IEnumerable<T> obj, bool child)
        {
            Type type = typeof(T);
            PropertyInfo[] properties;

            if (child)
                space += "    ";
            else
                space = "";

            foreach (var item in obj)
            {
                type = item.GetType();
                properties = type.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    if (property.PropertyType.Equals(typeof(int))
                        || property.PropertyType.Equals(typeof(string))
                        || property.PropertyType.Equals(typeof(float))
                        || property.PropertyType.Equals(typeof(bool)))
                    {


                        Console.WriteLine($"{space}{property.Name}: {property.GetValue(item, null)}");
                    }
                    else
                    {
                        Console.WriteLine($"{property.Name}");
                        var tt = property.GetValue(item, null);
                        if (tt != null)
                        {
                            MethodInfo method = typeof(OTC).GetMethod("ObjectToConsole");
                            MethodInfo generic = method.MakeGenericMethod(tt.GetType());
                            generic.Invoke(null, new object[2] { tt, true });
                        }
                        else
                            Console.WriteLine($"{space + "    "}null");
                        space = "";
                    }
                }
                Console.WriteLine("----------------------------------");
            }
        }
    }
}
