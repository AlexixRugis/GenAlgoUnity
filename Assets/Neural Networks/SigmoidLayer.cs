using System;

public class SigmoidLayer : INeuralLayer
{
    public DataVector InputData { get; private set; }

    public DataVector OutputData { get; private set; }

    public void Evaluate()
    {
        var outData = OutputData.Data;
        var inData = InputData.Data;
        for (int i = 0; i < InputData.TotalCount; i++)
        {
            outData[i] = Sigmoid(inData[i]);
        }
    }

    public void SetInput(DataVector inputData)
    {
        InputData = inputData;
        OutputData = new DataVector(inputData.TotalCount);
    }

    private float Sigmoid(float value)
    {
        return 1f / (1f + MathF.Exp(-value));
    }
}
