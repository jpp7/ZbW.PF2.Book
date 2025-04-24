using System;
using System.Windows.Forms;

namespace Paint;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new PaintForm());
    }
}