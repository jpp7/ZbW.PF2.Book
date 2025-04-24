using My.Collections;
using System;

namespace DoublyLinkedList;

class Program
{
    static void Main()
    {
        var list = new DoublyLinkedList<string>();

        list.Add("Hans Muster");
        list.Add("Berta Müller");
        list.Add("Kurt Schmidt");

        list.InsertAfter("Hans Muster", "Frieda Roth");

        Console.WriteLine(list);

        list.Remove("Berta Müller");
        Console.WriteLine(list);
        list.Remove("Hans Muster");
        Console.WriteLine(list);
        list.Remove("Kurt Schmidt");
        Console.WriteLine(list);

        list.Clear();
    }
}