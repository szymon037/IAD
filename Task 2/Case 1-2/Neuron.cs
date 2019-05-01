using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace task_2
{
    class Neuron
    {
        public int X {get; set;}
        public int Y { get; set; }
        public List<double> Weights { get; }

        public double EuclidianDistance(List<double> vector)
        {
            return Weights.Select(x => Math.Pow(x - vector[Weights.IndexOf(x)], 2)).Sum();
        }

        public Neuron(int numOfWeights)
        {
            var random = new Random();
            Weights = new List<double>();

            for (int i = 0; i < numOfWeights; i++)
            {
                Weights.Add(random.NextDouble());
            }
        }

        public double GetWeight( int index)
        {
            return Weights[index];
        }

        public void SetWeight(int index, double value)
        {
            Weights[index] = value;
        }

        public void UpdateWeights(List<Point> input, double distanceDecay, double learningRate)
        {
            for (int i = 0; i < Weights.Count; i++)
            {
                //Weights[i] += distanceDecay * learningRate * (input[i].X - Weights[i]);
            }
        }
    }
}
