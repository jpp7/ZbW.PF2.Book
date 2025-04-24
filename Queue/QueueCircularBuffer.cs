using System;

namespace My.Collections;

public class QueueCircularBuffer<T> : ICollection
{
    private T[] items;
    private int first = 0, last = 0;

    public int Count { get; private set; }

    public QueueCircularBuffer(int length = 10)
    {
        items = new T[length];
    }

    public void Enqueue(T item)
    {
        if (Count == items.Length)
            throw new InsufficientMemoryException("No more space");

        items[last] = item;

        last++;
        Count++;

        last = last % items.Length; // Umlaufprüfung
    }

    public T Peek()
    {
        if (Count == 0)
            throw new InvalidOperationException("No items");

        return items[first];
    }

    public T Dequeue()
    {
        if (Count == 0)
            throw new InvalidOperationException("No items");

        T item = items[first];

        items[first] = default;

        first++;
        Count--;

        first = first % items.Length; // Umlaufprüfung

        return item;
    }

    public void Clear()
    {
        items = new T[10];
        last = first = 0;
        Count = 0;
    }

    public override string ToString()
    {
        string s = "";

        int i = first;

        while (i != last)
        {
            s += items[i].ToString() + " -> ";
            i = i + 1 == items.Length ? 0 : i + 1;
        }
        s += "Count: " + Count.ToString();

        return s;
    }
}