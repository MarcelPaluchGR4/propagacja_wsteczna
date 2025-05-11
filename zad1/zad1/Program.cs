using System.IO.Compression;

class Program
{
    class Neuron
    {
        public double[] inputs = new double[2];
        public double[] weights = new double[2];
        public double error;

        private Random r = new Random();

        public void randomizeWeights()
        {
            for (int i = 0; i < weights.Length - 1; i++)
            {
                weights[i] = r.NextDouble();
            }
        }
        
    }

    public class NeuralNetwork()
    {
        public int Epoch = 20000;
    };

}
