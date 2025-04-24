using System.IO;
using System;
using Newtonsoft.Json;
using System.Text;

namespace MusicDB;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 1)
            throw new ArgumentException("Aufruf: MusicDB <folder>");

        var discs = new Discs(args[0]);

        string json;
        string fileDb = Path.Combine(args[0], "musicDB.json");
        if (!File.Exists(fileDb))
        {
            // Musikdateien einlesen...
            discs.Load();
        }
        else
        {
            // ...oder vom Cache
            using var stream = File.OpenText(fileDb);
            JsonSerializer serializer = new JsonSerializer();
            discs = (Discs)serializer.Deserialize(stream, typeof(Discs));
        }

        Console.Write("DiscId eingeben, z.B. 000cd512: ");

        string discId;
        while ((discId = Console.ReadLine()) != "")
        {
            Disc title = discs.Read(discId);

            if (title == null)
                Console.WriteLine("Titel nicht gefunden!");
            else
                Console.WriteLine($"{title}\n");
        }

        json = JsonConvert.SerializeObject(discs);
        File.WriteAllText(fileDb, json, Encoding.UTF8);
    }
}