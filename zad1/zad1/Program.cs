using System.IO.Compression;

class Program
{
    static int numberOfNeurons = 2;

    class Neuron
    {
        public double input;
        public double output;
        public double error;
        public double bias;
    }

    public class NeuralNetwork()
    {
        public int Epoch = 20000;
    };

    static void Main()
    {
        int[] layers = new[] { 2, 2, 1 };
    }

}
