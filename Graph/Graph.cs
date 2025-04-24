using System;
using System.Collections.Generic;

namespace My.Collections;

public class Node<T>
{
    public T? Item { get; set; }

    public List<Node<T>> Neighbors { get; private set; } = new();
    public List<double?> Weights { get; private set; } = new();

    public override string? ToString()
    {
        return Item?.ToString();
    }
}

public class Graph<T>
{
    public List<Node<T>> Nodes { get; private set; } = new();

    public Node<T> AddNode(T item)
    {
        var node = new Node<T> { Item = item };
        Nodes.Add(node);
        return node;
    }
    public void AddEdge(Node<T> from, Node<T> to, double? weight = null)
    {
        from.Neighbors.Add(to);
        from.Weights.Add(weight);

        to.Neighbors.Add(from);
        to.Weights.Add(weight);
    }
    public override string ToString()
    {
        string s = "";

        Nodes.ForEach(node =>
        {
            s += $"{node} -> ";

            List<string> neighbors = new();
            for (int i = 0; i < node.Neighbors.Count; i++)
            {
                var neigbor = node.Neighbors[i];
                var weight = node.Weights[i];
                neighbors.Add($"{neigbor} ({weight})");
            }
            s += string.Join(", ", neighbors) + "\n";
        });
        return s;
    }
}
