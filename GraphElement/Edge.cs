using System.Drawing;
using System.Drawing.Drawing2D;

namespace GraphElement
{
    public class Edge : Graph
    {
        public Vertex From { get; set; }
        public Vertex To { get; set; }


        Color customColor = ColorTranslator.FromHtml("#0e161d");
        public Edge(Vertex from, Vertex to)
        {
            From = from;
            To = to;
        }
        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(customColor, 2))
            {
                pen.CustomEndCap = new AdjustableArrowCap(5, 5);
                g.DrawLine(pen, From.Location, To.Location);
            }
        }
    }
}
