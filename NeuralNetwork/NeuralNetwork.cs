using System;
using System.IO;

/// <summary>
/// Classic example of Neural Network
/// Read about it on: https://stevenmiller888.github.io/mind-how-to-build-a-neural-network/
/// Learn about it on: https://www.youtube.com/watch?v=9aHJ-FAzQaE
/// Game idea and architecture of classes are all mine
/// End serri fir mi Englesh:)
/// </summary>

namespace NeuralNetwork
{
    public class Neural
    {
        public delegate float ActiveFuncDelegate(float x);
        ActiveFuncDelegate ActivationFunction;
        ActiveFuncDelegate DerivateActivationFunction;

        public delegate void OutputGenerated();
        public event OutputGenerated NewOutputEvent;

        private static readonly int countOfInputs = 2;
        private static readonly int countOfHiddenNeurons = 3;

        public float[] Inputs { get; set; } = new float[countOfInputs];
        public float[] FirstWeights { get; private set; } = new float[countOfInputs * countOfHiddenNeurons];
        public float[] Hiddens { get; private set; } = new float[countOfHiddenNeurons];
        public float[] HiddenSums { get; private set; } = new float[countOfHiddenNeurons];
        public float[] SecondWeights { get; private set; } = new float[countOfHiddenNeurons];

        public Neural(TextReader weightsfile, ActiveFuncDelegate activefunc, ActiveFuncDelegate derivfunc)
        {
            ActivationFunction = activefunc; DerivateActivationFunction = derivfunc;
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < countOfInputs * countOfHiddenNeurons; i++)
            {
                FirstWeights[i] = (float)Convert.ToDouble(weightsfile.ReadLine());
            }
            for (int i = 0; i < countOfHiddenNeurons; i++)
            {
                SecondWeights[i] = (float)Convert.ToDouble(weightsfile.ReadLine());
            }
            //for (int i = 0; i < countOfInputs * countOfHiddenNeurons; i++) FirstWeights[i] = (float)rnd.NextDouble() / 50.0f;
            //for (int i = 0; i < countOfHiddenNeurons; i++) SecondWeights[i] = (float)rnd.NextDouble() / 50.0f;

            // if you comment a file-reading part and uncomment these two cycles then you receive a start random weights
            // be careful! I give you trained weights in file, so if you will use random ones and save it
            // then you will have to train network from the beggining
            // or just copy my weights again:)
        }

        public float Output { get; private set; }
        public float OutputSum { get; private set; }

        public float Target { get; set; }

        public float Margin { get; private set; }
        public float DeltaSum { get; private set; }
        float[] deltaHiddenSum = new float[countOfHiddenNeurons];


        static public float Sigmoid(float x)
        {
            float res = (float)(1 / (1 + (1 / Math.Pow(Math.E, x))));
            return res;
        }
        static public float DerivateSigmoid(float x)
        {
            //float eX = (1 / (float)Math.Pow(Math.E, x));
            //return (float)(eX / Math.Pow(1 + eX, 2));
            float y = Sigmoid(x);
            return y * (1 - y);
        }

        public void CleanAll()
        {
            OutputSum = 0;
            for (int i = 0; i < countOfHiddenNeurons; i++)
            {
                HiddenSums[i] = 0;
            }
        }
        public void FindingHiddenValues()
        {
            for (int i = 0; i < countOfInputs; i++)
            {
                for (int j = i * countOfHiddenNeurons; j < i * countOfHiddenNeurons + countOfHiddenNeurons; j++)
                {
                    HiddenSums[j % countOfHiddenNeurons] += (Inputs[i] * FirstWeights[j]);
                }
            }

            for (int i = 0; i < countOfHiddenNeurons; i++)
            {
                Hiddens[i] = ActivationFunction(HiddenSums[i]);
            }
        }
        public void FindingOutput()
        {
            for (int i = 0; i < countOfHiddenNeurons; i++)
            {
                OutputSum += (Hiddens[i] * SecondWeights[i]);
            }
            Output = ActivationFunction(OutputSum);

            NewOutputEvent();
        }

        public void FrontPropagation()
        {
            // this part is clear, i promise!
            CleanAll();
            FindingHiddenValues();
            FindingOutput();
            // just kiddin`. Go read some theory, I can't help you.
        }
        public void BackPropagation()
        {
            Margin = (Target - Output);
            // you will see a lot of exception-handlings here
            // I wrote about it in Form, just go and read it 
            DeltaSum = (DerivateActivationFunction(OutputSum) * Margin);
            if (float.IsNaN(Margin) || float.IsNaN(DeltaSum))
            {
                throw new ArithmeticException();
            }
            for (int i = 0; i < countOfHiddenNeurons; i++)
            {
                deltaHiddenSum[i] = (DeltaSum / SecondWeights[i] * DerivateActivationFunction(HiddenSums[i]));
                if (float.IsNaN(deltaHiddenSum[i]))
                {
                    throw new ArithmeticException();
                }
            }

            for (int i = 0; i < countOfHiddenNeurons; i++)
            {
                SecondWeights[i] += (DeltaSum * Hiddens[i] * 0.7f);

                if (float.IsInfinity(SecondWeights[i]))
                {
                    throw new ArithmeticException();
                }
            }

            for (int i = 0; i < countOfInputs; i++)
            {
                for (int j = 0; j < countOfHiddenNeurons; j++)
                {
                    FirstWeights[i * countOfHiddenNeurons + j] += (deltaHiddenSum[j] * Inputs[i] * 0.7f);
                    if (float.IsNaN(FirstWeights[i * countOfHiddenNeurons + j]))
                    {
                        throw new ArithmeticException();
                    }
                }
            }

        }
        
        public void PrintAllWeightsInFile(TextWriter txtWrt)
        {
            // it's clear
            for (int i = 0; i < countOfHiddenNeurons * countOfInputs; i++)
            {
                txtWrt.WriteLine(FirstWeights[i]);
            }
            for (int i = 0; i < countOfHiddenNeurons; i++)
            {
                txtWrt.WriteLine(SecondWeights[i]);
            }
        }
    }

}
