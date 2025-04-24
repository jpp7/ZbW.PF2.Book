using My.Collections;
using System;

namespace BinarySearch;

class Program
{
    static void Main()
    {
        var list = new SortedArrayList<int>();

        list.AddRange(new int[] { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21 });

        for (int i = 0; i <= 25; i++)
        {
            int index = list.BinarySearch(i);

            Console.Write($"Zahl = {i} {index}");

            if (index < 0)
            {
                index = ~index;     // alternativ: -1 * index - 1;
                if (index < list.Count)
                    Console.Write($" Nächsthöhere: {list[index]}");
            }
            Console.WriteLine();
        }
    }
}