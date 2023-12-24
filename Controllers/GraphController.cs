using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Containers;
using GraphElement;

namespace Controllers
{
    public class GraphController
    {
        public class GraphInfoDTO
        {
            public bool HasCycles { get; set; }
            public int VertexCount { get; set; }
            public int EdgeCount { get; set; }
            public Dictionary<int, int> VertexDegrees { get; set; }
        }

        private readonly GraphContainer graphContainer;
        private readonly IMainFormView mainFormView;
        private int vertexIdCounter = 1;
        private const int SelectionRadius = 10;
        private const double EdgeSelectionThreshold = 5.0;

        public GraphController(IMainFormView mainFormView)
        {
            this.graphContainer = new GraphContainer();
            this.mainFormView = mainFormView;
        }

        public void AddVertex(Point location)
        {
            int newId = vertexIdCounter++;
            graphContainer.AddVertex(location, newId);
            mainFormView.UpdateGraphDisplay();
        }

        public void AddEdge(Point startLocation, Point endLocation)
        {
            var fromVertex = FindVertexAtLocation(startLocation);
            var toVertex = FindVertexAtLocation(endLocation);

            if (fromVertex != null && toVertex != null)
            {
                graphContainer.AddEdge(fromVertex, toVertex);
                mainFormView.UpdateGraphDisplay();
            }
        }

        public void RemoveElement(Point location)
        {
            var vertex = FindVertexAtLocation(location);
            if (vertex != null)
            {
                graphContainer.RemoveVertexAndConnectedEdges(vertex);
            }
            else
            {
                var edge = FindEdgeNearLocation(location);
                if (edge != null)
                {
                    graphContainer.RemoveElement(edge);
                }
            }
            mainFormView.UpdateGraphDisplay();
        }

        public void DisplayGraphInfo()
        {
            var graphInfo = graphContainer.GetGraphInfo();

            var dto = new GraphInfoDTO
            {
                HasCycles = graphInfo.HasCycles,
                VertexCount = graphInfo.VertexCount,
                EdgeCount = graphInfo.EdgeCount,
                VertexDegrees = graphInfo.VertexDegrees.ToDictionary(v => v.Key.Id, v => v.Value)
            };

            mainFormView.DisplayGraphInfo(dto);
        }

        public void DrawGraph(Graphics graphics)
        {
            if (graphics == null)
                throw new ArgumentNullException(nameof(graphics));

            var elements = graphContainer.GetAllElements();

            foreach (var element in elements)
            {
                element.Draw(graphics);
            }
        }

        private Vertex FindVertexAtLocation(Point location)
        {
            foreach (var vertex in graphContainer.GetVertices())
            {
                var distance = Math.Sqrt(Math.Pow(vertex.Location.X - location.X, 2) + Math.Pow(vertex.Location.Y - location.Y, 2));
                if (distance <= SelectionRadius)
                {
                    return vertex;
                }
            }
            return null;
        }

        private Edge FindEdgeNearLocation(Point location)
        {
            foreach (var edge in graphContainer.GetEdges())
            {
                Point from = edge.From.Location;
                Point to = edge.To.Location;

                double distance = PointToLineDistance(from, to, location);

                if (distance <= EdgeSelectionThreshold)
                {
                    return edge;
                }
            }
            return null;
        }

        private double PointToLineDistance(Point lineStart, Point lineEnd, Point point)
        {
            double num = Math.Abs((lineEnd.Y - lineStart.Y) * point.X - (lineEnd.X - lineStart.X) * point.Y + lineEnd.X * lineStart.Y - lineEnd.Y * lineStart.X);
            double den = Math.Sqrt(Math.Pow(lineEnd.Y - lineStart.Y, 2) + Math.Pow(lineEnd.X - lineStart.X, 2));

            return num / den;
        }
    }
}
