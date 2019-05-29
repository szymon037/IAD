using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kmeans
{
    public struct Point
    {
        public Point(double x,double y, double z, double q)
        {
            X = x;
            Y = y;
            Z = z;
            Q = q;
        }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Q { get; set; }
    }

    class KMeans
    {
        public List<Centroid> Centroids = new List<Centroid>();
        private readonly List<Point> Data;
        private readonly int lCentroid;



        public KMeans(int num, List<Point> data, Random rnd)
        {
            lCentroid = num;
            Data = data;
            for (int i = 0; i < num; i++)
            {
                Centroids.Add(new Centroid(rnd,data[rnd.Next(0,90)]));
            }
        }

        public void ClearPoints()
        {
            foreach (Centroid centroid in Centroids)
            {
                centroid.SavePoints();
            }
        }

        public double EDistance(Point a, Centroid c)
        {
            return Math.Sqrt(Math.Pow(a.X - c.X, 2) + Math.Pow(a.Y - c.Y, 2) + Math.Pow(a.Z - c.Z, 2) + Math.Pow(a.Q - c.Q, 2));
        }

        public void UpdatePoints()
        {
            ClearPoints();
            double[] PointDistance;
            foreach (Point p in Data)
            {
                PointDistance = new double[lCentroid];
                for(int i = 0; i < Centroids.Count; i++)
                {
                    PointDistance[i] = EDistance(p, Centroids[i]);
                }

                Centroids[Array.IndexOf(PointDistance, PointDistance.Min())].AddPoint(p);
            }
        }

        public void Train()
        {
            while (true)
            {

                UpdatePoints();
                foreach (Centroid c in Centroids)
                {
                    c.Move();
                    UpdatePoints();
                }

                bool hasChanged = false;

                foreach (Centroid c in Centroids)
                {
                    if (c.Moved())
                    {
                        hasChanged = true;
                        break;
                    }
                }
                if (!hasChanged)
                    break;
            }
        }
    }
}
