using My.Collections;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;


namespace SpellCheck;

class Program
{
    static void Main(string[] args)
    {
        //1. Wörterbuch über http 
        //
        //var client = new HttpClient();

        //var task = client.GetStreamAsync("https://gist.githubusercontent.com/MarvinJWendt/2f4f4154b8ae218600eb091a5706b5f4/raw/36b70dd6be330aa61cd4d4cdfda6234dcb0b8784/wordlist-german.txt");
        //task.Wait();
        //var streamDict = task.Result;
        //var spelling = new Spelling(new StreamReader(streamDict, System.Text.Encoding.Default));

        //2. Wörterbuch als Datei lesen
        //
        //var spelling = new Spelling(new StreamReader(@"german.dic", Encoding.Default));

        //3.Wörterbuch über Eingabeumleitung

        Console.InputEncoding = Encoding.Default;
        var spelling = new Spelling(Console.In);

        var color = Console.ForegroundColor;

        using var stream = new StreamReader(args[0], Encoding.Default);

        string text = stream.ReadToEnd();

        foreach (Spelling.Word word in spelling.CheckText(text))
        {
            if (!word.IsCorrect)
                Console.ForegroundColor = ConsoleColor.Red;

            Console.Write(word + " ");
            Console.ForegroundColor = color;
        }
    }
}