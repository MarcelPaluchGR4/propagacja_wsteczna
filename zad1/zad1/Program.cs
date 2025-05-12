class Program
{
    static int inputLayerNeurons = 2;
    static int hiddenLayerNeurons = 2;
    static int outputLayerNeurons = 1;
    static double learningParameter = 0.3;
    static int Epoch = 50000;

    static double Sigmoid(double x) => 1.0 / (1.0 + Math.Exp(-x));

    static void Main()
    {
        Random rand = new Random();
        int[] layers = [inputLayerNeurons, hiddenLayerNeurons, outputLayerNeurons];
        var xorTrainingSet = new double[][]
        {
            [0, 0, 0],
            [0, 1, 1],
            [1, 0, 1],
            [1, 1, 0]
        };

        double[,] weightsInputHidden = new double[hiddenLayerNeurons, inputLayerNeurons];
        double[] biasHidden = new double[hiddenLayerNeurons];

        double[] weightsHiddenOutput = new double[hiddenLayerNeurons];
        double biasOutput = rand.NextDouble() * 2 - 1;

        for (int i = 0; i < hiddenLayerNeurons; i++)
        {
            biasHidden[i] = rand.NextDouble() * 5 - 5;
            weightsHiddenOutput[i] = rand.NextDouble() * 5 - 5;
            for (int j = 0; j < inputLayerNeurons; j++)
                weightsInputHidden[i, j] = rand.NextDouble() * 5 - 5;
        }

        for (int epoch = 0; epoch < Epoch; epoch++)
        {
            foreach (var data in xorTrainingSet)
            {
                double[] inputs = { data[0], data[1] };
                double expected = data[2];

                double[] hidden = new double[hiddenLayerNeurons];
                for (int i = 0; i < hiddenLayerNeurons; i++)
                {
                hidden[i] = biasHidden[i];
                for (int j = 0; j < inputLayerNeurons; j++)
                hidden[i] += inputs[j] * weightsInputHidden[i, j];
                hidden[i] = Sigmoid(hidden[i]);
                }

                double output = biasOutput;
                for (int i = 0; i < hiddenLayerNeurons; i++)
                output += hidden[i] * weightsHiddenOutput[i];
                output = Sigmoid(output);
                }
                }

                // co z tego wyjdzie
                foreach (var data in xorTrainingSet)
                {
                double[] inputs = { data[0], data[1] };

                double[] hidden = new double[hiddenLayerNeurons];
                for (int i = 0; i < hiddenLayerNeurons; i++)
                {
                hidden[i] = biasHidden[i];
                for (int j = 0; j < inputLayerNeurons; j++)
                hidden[i] += inputs[j] * weightsInputHidden[i, j];
                hidden[i] = Sigmoid(hidden[i]);
            }

            double output = biasOutput;
            for (int i = 0; i < hiddenLayerNeurons; i++)
                output += hidden[i] * weightsHiddenOutput[i];
                output = Sigmoid(output);

                Console.WriteLine($"{inputs[0]} XOR {inputs[1]} = {Math.Round(output)} ({output:F4})");
        }

        }

}

