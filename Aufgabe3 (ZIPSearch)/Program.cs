using System;

namespace Aufgabe3;

class Program
{
    static void Main()
    {
        var zip = new ZIP(@".\ZIPCodes.csv");

        int zipcode;
        do
        {
            Console.WriteLine("ZIPCode eingeben, z.B. 35036: ");
            zipcode = int.Parse(Console.ReadLine());
            ZIP.Location location = zip.SearchLocation(zipcode);
            if (location != null)
                Console.WriteLine(location);
            else
                Console.WriteLine("Nicht gefunden!");

        } while (zipcode != 0);
    }
}