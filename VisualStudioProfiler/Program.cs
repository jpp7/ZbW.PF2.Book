namespace VisualStudioProfiler;

class Program
{
    static void Main()
    {
        // Starte "Debuggen->Leistungsprofiler", wähle "CPU-Auslastung" und dann "Starten"

        var listSys = new System.Collections.Generic.LinkedList<int>();
        var listMy = new My.Collections.LinkedList<int>();

        for (int i = 0; i < 10000000; i++)
        {
            listSys.AddLast(i);
            listMy.Add(i);
        }
        for (int i = 0; i < 10000000; i++)
        {
            listSys.Remove(i);
            listMy.Remove(i);
        }
    }
}