using System;
using System.Windows.Forms;

namespace PostScriptViewer;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new ViewerForm());
    }
}