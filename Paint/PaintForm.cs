using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Paint;

public partial class PaintForm : Form
{
    private ContextMenuStrip menu;
    private string color = Color.Black.Name;
    private Pen pen;

    private bool mouseDown = false;
    private Graphics graphicsBitmap;
    private Point point;

    private Bitmap cacheBitmap;

    public PaintForm()
    {
        InitializeComponent();
    }

    private void OnLoad(object sender, EventArgs e)
    {
        menu = new ContextMenuStrip();
        var h = new EventHandler(OnMenuClick);

        menu.Items.AddRange(new ToolStripMenuItem[]
        {
            new ToolStripMenuItem(Color.Black.Name, null, h),
            new ToolStripMenuItem(Color.Red.Name, null, h),
            new ToolStripMenuItem(Color.Green.Name, null, h),
            new ToolStripMenuItem(Color.Blue.Name, null, h),
            new ToolStripMenuItem(Color.Yellow.Name, null, h)
        });

        this.ContextMenuStrip = menu;

        pen = new Pen(Color.FromName(color), 5);

        // Cache
        cacheBitmap = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
        graphicsBitmap = Graphics.FromImage(cacheBitmap);

        // Trick, um die Aussengrenzen und das Vergrößern des Bildes nicht zu berücksichtigen
        graphicsBitmap.DrawRectangle(new Pen(Color.FromName("gray"), 2), 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
    }

    protected void OnMenuClick(object sender, EventArgs e)
    {
        color = (sender as ToolStripMenuItem).Text;
        pen = new Pen(Color.FromName(color), 5);
    }

private void OnMouseDown(object sender, MouseEventArgs e)
{
    if (e.Button == MouseButtons.Right)
        menu.Show(this, e.Location);

    if (e.Button != MouseButtons.Left)
        return;

    mouseDown = true;

    point = e.Location;
}

private void OnMouseMove(object sender, MouseEventArgs e)
{
    if (mouseDown == false)
        return;

    using var g = CreateGraphics();
    g.SmoothingMode = SmoothingMode.HighSpeed;
    g.DrawLine(pen, point, e.Location);

    // Zeichnen in Bitmap cachen
    graphicsBitmap.DrawLine(pen, point, e.Location);

    point = e.Location;
}

    private void OnMouseUp(object sender, MouseEventArgs e)
    {
        mouseDown = false;
    }

    private void OnPaint(object sender, PaintEventArgs e)
    {
        e.Graphics.DrawImage(cacheBitmap, new Point(0, 0));
    }

    private void OnMouseDoubleClick(object sender, MouseEventArgs e)
    {
        Cursor c = Cursor;
        Cursor = Cursors.WaitCursor;

        PaintTools.FloodFill(cacheBitmap, e.Location, Color.FromName(color));
        //PaintTools.FloodFillRecursive(cacheBitmap, e.Location, Color.FromName(color), cacheBitmap.GetPixel(e.Location.X, e.Location.Y));

        Invalidate();
        Cursor = c;
    }
}
