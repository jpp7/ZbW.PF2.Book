using System;

namespace My.Collections;
public class LinkedListSimple
{
    private class Node
    {
        public Object Item { get; set; }
        public Node Next { get; set; }
    }

    private Node first, last;

    public int Count { get; private set; }

    public virtual void Add(Object item)
    {
        var newNode = new Node() { Item = item, Next = null };

        if (first == null)
        {
            first = newNode;
            last = newNode;
        }
        else
        {
            last.Next = newNode;
            last = newNode;
        }
        Count++;
    }

    public virtual void AddRange(Object[] items)
    {
        foreach (object item in items)
            Add(item);
    }
}