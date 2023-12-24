using System.Collections.Generic;
using System.Drawing;


namespace GraphElement
{
    public class Vertex : Graph
    {
        public int Id { get; set; }
        public Point Location { get; set; }
        public List<Edge> Edges { get; private set; }


        public Vertex(int id, Point location)
        {
            Id = id;
            Location = location;
            Edges = new List<Edge>();
        }
        public override void Draw(Graphics g)
        {
            int ellipseDiameter = 30;
            int ellipseRadius = ellipseDiameter / 2;

            int ellipseX = Location.X - ellipseRadius;
            int ellipseY = Location.Y - ellipseRadius;

            Color customColor = ColorTranslator.FromHtml("#d6dc6e");
            Color textColor = ColorTranslator.FromHtml("#ff0000");
            using (Brush customBrush = new SolidBrush(customColor))
            {
                g.FillEllipse(customBrush, ellipseX, ellipseY, ellipseDiameter, ellipseDiameter);
            }

            StringFormat stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            RectangleF textRect = new RectangleF(ellipseX, ellipseY, ellipseDiameter, ellipseDiameter);

            using (Brush textBrush = new SolidBrush(textColor))
            {
                g.DrawString(Id.ToString(), SystemFonts.DefaultFont, textBrush, textRect, stringFormat);
            }
        }


    }
}

