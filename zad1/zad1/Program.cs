// TODO refactor this low-quality code in your free time

class Program
{
    static int inputLayerNeurons = 2;
    static int hiddenLayerNeurons = 2;
    static int outputLayerNeurons = 1;
    static double learningParameter = 0.3;
    static int Epoch = 50000;
    static double learningStopError = 0.3;

    static double Sigmoid(double x) => 1.0 / (1.0 + Math.Exp(-x));
    static double SigmoidDerivative(double x) => x * (1 - x);

    static void Main()
    {
        Random rand = new Random();
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
            biasHidden[i] = rand.NextDouble() * 2 - 1;
            weightsHiddenOutput[i] = rand.NextDouble() * 2 - 1;
            for (int j = 0; j < inputLayerNeurons; j++)
                weightsInputHidden[i, j] = rand.NextDouble() * 2 - 1;
        }

        for (int epoch = 0; epoch < Epoch; epoch++)
        {
            double totalError = 0;

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

                double outputError = expected - output;
                totalError += Math.Pow(outputError, 2);
                double outputDelta = outputError * SigmoidDerivative(output);

                double[] hiddenDeltas = new double[hiddenLayerNeurons];
                for (int i = 0; i < hiddenLayerNeurons; i++)
                {
                    hiddenDeltas[i] = outputDelta * weightsHiddenOutput[i] * SigmoidDerivative(hidden[i]);
                }

                for (int i = 0; i < hiddenLayerNeurons; i++)
                    weightsHiddenOutput[i] += learningParameter * outputDelta * hidden[i];
                biasOutput += learningParameter * outputDelta;

                for (int i = 0; i < hiddenLayerNeurons; i++)
                {
                    for (int j = 0; j < inputLayerNeurons; j++)
                        weightsInputHidden[i, j] += learningParameter * hiddenDeltas[i] * inputs[j];
                    biasHidden[i] += learningParameter * hiddenDeltas[i];
                }
            }

            // wyprintuj blad co 1000 epok
            if (epoch % 1000 == 0)
                Console.WriteLine($"Epoch {epoch}, Error: {totalError:F6}");

            if (totalError < learningStopError)
            {
                Console.WriteLine($"Training stopped at epoch {epoch}, total error: {totalError:F6}");
                break;
            }
        }


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