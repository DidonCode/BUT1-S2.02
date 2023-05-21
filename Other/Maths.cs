using SAE_Graphe.Graph;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;

namespace SAE_Graph.Other
{

    public class Maths
    {

        public static float CalculeDistance(Vertex start, Vertex end) {
			Vertex startCM = ConvertInCM(start);
			Vertex endCM = ConvertInCM(end);

            double distance = Math.Sqrt(Math.Pow(endCM.x - startCM.x, 2) + Math.Pow(endCM.y - startCM.y, 2));

            return (float)Math.Round(distance, 3);
        }

        public static Vertex ConvertInCM(Vertex vertex) {
            return new Vertex((int)ConvertPixelToCm(vertex.x), (int)ConvertPixelToCm(vertex.y));
        }

        public static Vertex CalculeMiddle(Vertex start, Vertex end) {
            return new Vertex((start.x + end.x) / 2, (start.y + end.y) / 2);
        }

        public static float ConvertPixelToCm(float pixel) {
            if (pixel > 0)
            {
                return pixel / 37;
            }
            return 0;
        }

        public static float ConvertCmToPixel(float cm) {
            return cm * 37;
        }

        public static Vertex GetMiddleBiome(List<Vertex> edges) {
            int xMax = 0;
            int yMax = 0;

            foreach (Vertex edge in edges) {
                if (edge.x > xMax) {
                    xMax = edge.x;
                }

                if (edge.y > yMax) {
                    yMax = edge.y;
                }
            }

            int xMin = xMax;
            int yMin = yMax;

            foreach (Vertex edge in edges) {
                if (edge.x < xMin) {
                    xMin = edge.x;
                }

                if (edge.y < yMin) {
                    yMin = edge.y;
                }
            }

            return new Vertex(xMin + xMax / 2, yMin + yMax / 2);
        }

        public static bool CrossVertex(List<Vertex> polygon, Edge edge)
        {
            int intersections = 0;

            for (int i = 0; i < polygon.Count - 1; i++)
            {
                int startX = polygon[i].x;
                int startY = polygon[i].y;
                int endX = polygon[i + 1].x;
                int endY = polygon[i + 1].y;

                Vertex middle = CalculeMiddle(edge.start.position, edge.end.position);

                int midX = middle.x;
                int midY = middle.y;

                if (startY <= midY && endY > midY || startY > midY && endY <= midY)
                {
                    int slope = (endX - startX) / (endY - startY);
                    int hitX = startX + (midY - startY) * slope;

                    if (hitX > midX)
                    {
                        intersections++;
                    }
                }
            }

            if (intersections % 2 != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsConvex(List<Vertex> edges)
        {
            int numEdge = edges.Count;
            if (numEdge < 3) return false;

            bool isPositive = false;
            bool isNegative = false;
            for (int i = 0; i < numEdge; i++)
            {
                Vertex p1 = edges[i];
                Vertex p2 = edges[(i + 1) % numEdge];
                Vertex p3 = edges[(i + 2) % numEdge];
                float crossProduct = CrossProductLength(p1, p2, p3);
                if (crossProduct > 0)
                {
                    isPositive = true;
                }
                else if (crossProduct < 0)
                {
                    isNegative = true;
                }
                if (isPositive && isNegative) return false;
            }
            return true;
        }

        public static float CrossProductLength(Vertex p1, Vertex p2, Vertex p3)
        {
            float vector1X = p2.X - p1.X;
            float vector1Y = p2.Y - p1.Y;
            float vector2X = p3.X - p2.X;
            float vector2Y = p3.Y - p2.Y;
            return vector1X * vector2Y - vector1Y * vector2X;
        }
    }
}
