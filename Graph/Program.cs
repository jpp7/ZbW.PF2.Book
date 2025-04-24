using System;
using My.Collections;

namespace Graph;

class Program
{
    static void Main()
    {
        var graph = new Graph<string>();
        var a = graph.AddNode("a");
        var b = graph.AddNode("b");
        var c = graph.AddNode("c");
        var d = graph.AddNode("d");
        var e = graph.AddNode("e");

        graph.AddEdge(a, b);
        graph.AddEdge(a, e);
        graph.AddEdge(b, c);
        graph.AddEdge(b, d);
        graph.AddEdge(b, e);
        graph.AddEdge(d, e);

        Console.WriteLine(graph);
    }
}