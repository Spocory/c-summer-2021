using System;

namespace SchoolEF
{
    class Program
    {
        static void Main()
        {
            // Database accessor. This opens the database automatically
            var school = new SchoolEntities();

            // This accesses the ClassMaster table

            foreach (var user in school.Users)
            {
                Console.WriteLine("{0}", user.UserName);
           
                    foreach (var classMaster in user.ClassMasters)
                {
                    Console.WriteLine($"{classMaster.ClassId} - {classMaster.ClassName}");
                }
            }
            Console.Write("Done.");
            Console.ReadLine();
        }
    }
}