﻿using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System;
using My.Collections;

namespace MergeSortParallel;

class Program
{
    static void Main(string[] args)
    {
        var lists = new SortedArrayList<string>[] {
                new SortedArrayList<string>(),
                new SortedArrayList<string>(),
                new SortedArrayList<string>(),
                new SortedArrayList<string>()};

        using (var stream = new StreamReader(args[0], Encoding.Default))
        {
            string line;
            while ((line = stream.ReadLine()) != null)
            {
                lists[0].Add(line);
                lists[1].Add(line);
            }
        }

        // jeweils Hälfte der Liste in 2 und 3 kopieren
        for (int i = 0; i < lists[0].Count; i++)
            lists[2 + (i % 2)].Add(lists[0][i]);

        var w = new Stopwatch();

        w.Start();
        lists[0].MergeSort();
        w.Stop();
        Console.WriteLine($"MergeSort: {w.Elapsed}");

        w.Restart();
        lists[1].MergeSortParallel();
        w.Stop();
        Console.WriteLine($"MergeSortParallel: {w.Elapsed}");

        w.Restart();
        Task.WaitAll(new Task[] {
            Task.Run(() => { lists[2].MergeSort(); }),
            Task.Run(() => { lists[3].MergeSort(); })
        });

        lists[3] = SortedArrayList<string>.Merge(new SortedArrayList<string>[] { lists[2], lists[3] });

        w.Stop();
        Console.WriteLine($"Merge (2 Tasks): {w.Elapsed}");

        Console.ReadLine();
    }
}