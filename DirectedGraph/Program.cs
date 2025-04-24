using System;
using My.Collections;

namespace Graph;

class Program
{
    static void Main()
    {
        var graph = new Graph<string>(true);
        var e = graph.AddNode("E");
        var a = graph.AddNode("A");
        var b = graph.AddNode("B");
        var c = graph.AddNode("C");
        var d = graph.AddNode("D");

        graph.AddEdge(a, b, 10);
        graph.AddEdge(a, c, 20);
        graph.AddEdge(b, c, 5);
        graph.AddEdge(c, a, 20);
        graph.AddEdge(c, d, 30);

        Console.WriteLine(graph);

        foreach (var node in graph.Nodes)
            Console.WriteLine(node);

        foreach (var node in graph.DFS())
            Console.WriteLine(node);
    }
}