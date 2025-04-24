using System.IO;
using System.Text;
using System;
using OneRClassfier;

namespace OneRClassifier;

class Program
{
    static void Main(string[] args)
    {
        var oneR = new OneR();

        oneR.Build(new StreamReader(args[0], Encoding.Default));

        Console.WriteLine("\nGültigen Wert zur Vorhersage eingeben (z.B. rot): ");

        string value;
        while ((value = Console.ReadLine()) != "")
        {
            var predicted = oneR.Classify(value);

            Console.WriteLine("Vorhersage: {0}", predicted ?? "<unbekannt>");
        }
    }
}