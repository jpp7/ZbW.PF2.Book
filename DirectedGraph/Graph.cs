using System;
using System.Collections.Generic;
using System.Dynamic;

namespace My.Collections;

public class Node<T> : DynamicObject
{
    public T? Item { get; set; }
    public object? AdditionalData { get; set; }

    public List<Node<T>> Neighbors { get; private set; } = new();
    public List<double?> Weights { get; private set; } = new();

    public override string? ToString()
    {
        return Item?.ToString();
    }
    public override bool TryGetMember(GetMemberBinder binder, out object? result)
    {
        result = AdditionalData;
        return true;
    }
    public override bool TrySetMember(SetMemberBinder binder, object? value)
    {
        AdditionalData = value;
        return true;
    }
}

public class Graph<T>
{
    bool directed;
    public Graph(bool directed = false)
    {
        this.directed = directed;
    }
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

        if (!directed)
        {
            to.Neighbors.Add(from);
            to.Weights.Add(weight);
        }
    }
    public Node<T> this[T item]
    {
        get
        {
            var node = Nodes.Find(node => node.Item?.Equals(item) == true);

            if (node != null)
                return node;

            throw new IndexOutOfRangeException("Item not found");
        }
    }
    public bool Contains(T item)
    {
        var node = Nodes.Find(node => node.Item?.Equals(item) == true);

        return node != null;
    }
    public List<Node<T>> DFS()
    {
        var visited = new List<Node<T>>();

        Nodes.ForEach(n => (n as dynamic).IsVisited = null);

        var stack = new Stack<Node<T>>();

        (Nodes[0] as dynamic).IsVisited = true;
        stack.Push(Nodes[0]);
        visited.Add(Nodes[0]);

        while (stack.Count > 0)
        {
            var neighbor = GetNextUnvisitedNeighbor(stack.Peek());

            if (neighbor == null)
                stack.Pop();
            else
            {
                (neighbor as dynamic).IsVisited = true;
                visited.Add(neighbor);
                stack.Push(neighbor);
            }
        }

        Nodes.ForEach(n => (n as dynamic).IsVisited = null);

        return visited;
    }
    Node<T>? GetNextUnvisitedNeighbor(Node<T> node)
    {
        foreach (var neighbor in node.Neighbors)
        {
            if ((neighbor as dynamic).IsVisited == null)
                return neighbor;
        }
        return null;
    }
    public List<Node<T>> BFS()
    {
        var visited = new List<Node<T>>();

        Nodes.ForEach(n => (n as dynamic).IsVisited = null);

        var queue = new Queue<Node<T>>();

        (Nodes[0] as dynamic).IsVisited = true;
        queue.Enqueue(Nodes[0]);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            visited.Add(node);

            foreach (dynamic neighbor in node.Neighbors)
            {
                if (neighbor.IsVisited == null)
                {
                    neighbor.IsVisited = true;
                    queue.Enqueue(neighbor);
                }
            }
        }

        Nodes.ForEach(n => (n as dynamic).IsVisited = null);

        return visited;
    }
    public List<Node<T>> DFSRecursive()
    {
        var list = new List<Node<T>>();

        Nodes.ForEach(n => (n as dynamic).IsVisited = null);
        DFSRecursive(Nodes[0], list);
        Nodes.ForEach(n => (n as dynamic).IsVisited = null);

        return list;
    }
    void DFSRecursive(Node<T> node, List<Node<T>> list)
    {
        list.Add(node);
        (node as dynamic).IsVisited = true;

        foreach (var neighbor in node.Neighbors)
        {
            if ((neighbor as dynamic).IsVisited == null)
                DFSRecursive(neighbor, list);
        }
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
