using My.Collections;
using System;

namespace ProblemInheritance;

class Program
{
    static void Main()
    {
        var list = new StringList();

        list.AddRange(new string[] { "Muster", "Schmidt", "Müller" });

        Console.WriteLine("Falsche  Anzahl: " + list.MyCount);
        Console.WriteLine("Korrekte Anzahl: " + list.Count);
    }
}

public class StringList : LinkedListSimple
{
    public int MyCount { get; private set; }

    public override void Add(object item)
    {
        MyCount++;
        base.Add(item);
    }

    public override void AddRange(object[] objects)
    {
        MyCount += objects.Length;
        base.AddRange(objects);
    }
}
