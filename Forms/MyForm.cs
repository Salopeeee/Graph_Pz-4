using Controllers;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static Controllers.GraphController;

public partial class MainForm : Form, IMainFormView
{
    private GraphController graphController;
    private Button addVertexButton, addEdgeButton, removeButton, displayInfoButton;
    private PictureBox graphPictureBox;
    private StatusBar statusBar;
    private Point? startPoint = null, endPoint = null;

    public MainForm()
    {
        InitializeComponent();
        graphController = new GraphController(this);
    }

    private void InitializeComponent()
    {
        this.AutoScaleMode = AutoScaleMode.Font;
        this.Text = "Graph Editor";
        this.ClientSize = new Size(800, 600);
        this.BackColor = ColorTranslator.FromHtml("#4e799c");

        InitializePictureBox();
        InitializeButtons();

        this.Controls.Add(graphPictureBox);
        this.Controls.AddRange(new Control[] { addVertexButton, addEdgeButton, removeButton, displayInfoButton, statusBar });
    }

    private void InitializePictureBox()
    {
        graphPictureBox = new PictureBox
        {
            Location = new Point(10, 60),
            Size = new Size(780, 500),
            BackColor = ColorTranslator.FromHtml("#6eaadc"),
            BorderStyle = BorderStyle.FixedSingle
        };
        graphPictureBox.Paint += new PaintEventHandler(GraphPictureBox_Paint);
        graphPictureBox.MouseClick += new MouseEventHandler(GraphPictureBox_MouseClick);
    }

    private void InitializeButtons()
    {
        addVertexButton = CreateButton("Add Vertex", new Point(10, 30), new Size(100, 25));
        addEdgeButton = CreateButton("Add Edge", new Point(120, 30), new Size(100, 25));
        removeButton = CreateButton("Remove", new Point(230, 30), new Size(100, 25));

        displayInfoButton = CreateButton("Display Info", new Point(340, 30), new Size(100, 25));

        addVertexButton.Click += new EventHandler(AddVertexButton_Click);
        addEdgeButton.Click += new EventHandler(AddEdgeButton_Click);
        removeButton.Click += new EventHandler(RemoveButton_Click);
        displayInfoButton.Click += new EventHandler(DisplayElementsButton_Click);
    }

    private Button CreateButton(string text, Point location, Size size)
    {
        return new Button
        {
            Text = text,
            Location = location,
            Size = size,
            ForeColor = Color.White,
            BackColor = ColorTranslator.FromHtml("#2e475c"),
            FlatStyle = FlatStyle.Flat
        };
    }

    private void AddVertexButton_Click(object sender, EventArgs e)
    {
        if (endPoint.HasValue)
        {
            graphController.AddVertex(endPoint.Value);
            endPoint = null;
            UpdateGraphDisplay();
        }
    }

    private void AddEdgeButton_Click(object sender, EventArgs e)
    {
        if (startPoint.HasValue && endPoint.HasValue)
        {
            graphController.AddEdge(startPoint.Value, endPoint.Value);
            startPoint = null;
            endPoint = null;
            UpdateGraphDisplay();
        }
    }

    private void RemoveButton_Click(object sender, EventArgs e)
    {
        if (endPoint.HasValue)
        {
            graphController.RemoveElement(endPoint.Value);
            endPoint = null;
            UpdateGraphDisplay();
        }
    }

    private void DisplayElementsButton_Click(object sender, EventArgs e)
    {
        graphController.DisplayGraphInfo();
    }

    private void GraphPictureBox_Paint(object sender, PaintEventArgs e)
    {
        graphController.DrawGraph(e.Graphics);
    }

    private void GraphPictureBox_MouseClick(object sender, MouseEventArgs e)
    {
        if (!startPoint.HasValue)
        {
            startPoint = e.Location;
        }
        else
        {
            endPoint = e.Location;
            UpdateGraphDisplay();
        }
    }

    public void UpdateGraphDisplay()
    {
        graphPictureBox.Refresh();
    }

    public void DisplayGraphInfo(GraphInfoDTO graphInfo)
    {
        MessageBox.Show($"Cycles found: {graphInfo.HasCycles}\n" +
                        $"Vertex Count: {graphInfo.VertexCount}\n" +
                        $"Edge Count: {graphInfo.EdgeCount}\n" +
                        $"Vertex Degrees: {string.Join(", ", graphInfo.VertexDegrees.Select(v => $"Id: {v.Key}, Degree: {v.Value}"))}\n");
    }
}
