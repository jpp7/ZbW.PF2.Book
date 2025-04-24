using My.Collections;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace MusicDB;

public class Discs
{
    [JsonProperty]
    private RedBlackTree<string, Disc> discTitles;

    [JsonProperty]
    private string musicFolder;

    public Discs(string musicFolder)
    {
        this.musicFolder = musicFolder;
        this.discTitles = new RedBlackTree<string, Disc>();
    }

    public void Load()
    {
        foreach (var file in Directory.EnumerateFiles(musicFolder, "*.", SearchOption.AllDirectories))
            ReadDiscFile(file);
    }

    public Disc Read(string discId)
    {
        if (discTitles.Contains(discId))
            return discTitles[discId];

        return null;
    }

    private void ReadDiscFile(string file)
    {
        using var reader = new StreamReader(file, Encoding.UTF8);
        
        string line;
        string discTitle = null, genre = null, discId = null;
        int year = 0;

        while ((line = reader.ReadLine()) != null)
        {
            if (line[0] != '#')
            {
                var index = line.IndexOf('=');
                var key = line.Substring(0, index);
                var value = line.Substring(index + 1);

                if (key == "DISCID")
                    discId = value;
                else if (key == "DTITLE")
                    discTitle = value;
                else if (key == "DGENRE")
                    genre = value;
                else if (key == "DYEAR")
                {
                    if (int.TryParse(value, out year) == false)
                        year = 0;
                }
            }
        }
        if (discId != null)
            discTitles.Add(discId, new Disc(discId, discTitle, year, genre));
    }
}

public class Disc
{
    public Disc(string discId, string discTitle, int year, string genre)
    {
        DiscId = discId;
        DiscTitle = discTitle;
        Year = year;
        Genre = genre;
    }

    public string DiscId { get; private set; }
    public string DiscTitle { get; private set; }
    public int Year { get; private set; }
    public string Genre { get; private set; }

    public override string ToString()
    {
        return $"{DiscId} | {DiscTitle} | {Year} | {Genre}";
    }
}