﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace My.Collections;

public class LinkedList<T> : IEnumerable<T>
{
    private class Node
    {
        public T Item { get; set; }
        public Node Next { get; set; }
    }

    private Node first, last;

    public int Count { get; private set; }

    public void Add(T item)
    {
        var newNode = new Node() { Item = item, Next = null };

        if (first == null)	// erster einzufügender Knoten
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

    public void AddRange(T[] items)
    {
        foreach (T item in items)
            Add(item);
    }

    public bool Remove(T item)
    {
        Node node = Find(item);

        if (node == null)
            return false;

        Node previousNode = FindPrevious(item);

        // aus Mitte oder Ende entfernen
        if (previousNode != null)
        {
            previousNode.Next = node.Next;

            if (node == last)
                last = previousNode;
        }
        else // ersten entfernen, previousNode == null
        {
            first = node.Next;

            if (first == null) // Liste leer
                last = null;
        }

        Count--;

        return true;
    }

    public bool Contains(T item)
    {
        Node node = Find(item);

        if (node == null)
            return false;

        return true;
    }

    public void Clear()
    {
        first = last = null;
        Count = 0;
    }

    public T this[int index]
    {
        get
        {
            return FindByIndex(index).Item;
        }
        set
        {
            FindByIndex(index).Item = value;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node node = first;

        while (node != null)
        {
            yield return node.Item;
            node = node.Next;
        }
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        string s = "";

        Node node = first;

        while (node != null)
        {
            s += node.Item.ToString() + " -> ";
            node = node.Next;
        }
        s += "Count: " + Count.ToString();

        return s;
    }

    private Node Find(T item)
    {
        Node node = first;

        while (node != null)
        {
            if (node.Item.Equals(item))
                return node;

            node = node.Next;
        }
        return null;
    }

    private Node FindPrevious(T item)
    {
        Node previousNode = null;
        Node node = first;

        while (node != null)
        {
            if (node.Item.Equals(item))
                return previousNode;

            previousNode = node;
            node = node.Next;
        }
        return null;
    }

    private Node FindByIndex(int index)
    {
        if (index >= Count)
            throw new IndexOutOfRangeException();

        Node node = first;
        int i = 0;

        while (node != null)
        {
            if (i == index)
                return node;

            node = node.Next;
            i++;
        }
        return null;
    }
}