using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Kmeans
{
    class Centroid
    {
        public double X;
        public double Y;
        public double Z;
        public double Q;
        public List<Point> CentroidPoints;
        public List<Point> OldCentroidPoints;
        private double oldX;
        private double oldY;
        private double oldZ;
        private double oldQ;
        private  readonly Random rng;

        public void AddPoint(Point point)
        {
            CentroidPoints.Add(point);
        }

        public Centroid(Random rnd, Point p)
        {
            rng = rnd;
            X = p.X;
            Y = p.Y;
            Z = p.Z;
            Q = p.Q;
            CentroidPoints = new List<Point>();
            OldCentroidPoints = new List<Point>();
        }

        //Function which move Centroid position to the new position computed by average centroid points position
        public void Move()
        {
            double sumX = 0;
            double sumY = 0;
            double sumZ = 0;
            double sumQ = 0;

            if (CentroidPoints.Count == 0)
            {
                //X = rng.Next(4, 6);
                //Y = rng.Next(3, 4);
               //// Z = rng.Next(3, 6);
                //Q = rng.Next(1, 2);
                oldX = X;
                oldY = Y;
                oldZ = Z;
                oldQ = Q;
                return;
            }

            for (int i = 0; i < CentroidPoints.Count; i++)
            {
                sumX += CentroidPoints[i].X;
                sumY += CentroidPoints[i].Y;
                sumZ += CentroidPoints[i].Z;
                sumY += CentroidPoints[i].Q;
            }
            sumX /= CentroidPoints.Count;
            sumY /= CentroidPoints.Count;
            sumZ /= CentroidPoints.Count;
            sumQ /= CentroidPoints.Count;
            oldX = X;
            oldY = Y;
            oldZ = Z;
            oldQ = Q;
            X = sumX;
            Y = sumY;
            Z = sumZ;
            Q = sumQ;
        }

        public double Error()
        {
            double result = 0;
            if (CentroidPoints.Count != 0)
            {

                foreach (Point p in CentroidPoints)
                {
                    result += Math.Sqrt(Math.Pow((p.X - X), 2) + Math.Pow((p.Y - Y), 2));
                }
                return result / CentroidPoints.Count;
            }
            else
                return 0;
        }

        public bool Moved()
        {

            if (oldX == X && oldY == Y && oldZ == Z && oldQ == Q)
                return false;
            else
                return true;
        }

        public void SavePoints()
        {
            OldCentroidPoints.Clear();
            OldCentroidPoints = CentroidPoints;
            CentroidPoints.Clear();
        }
    }
}
