public class LinearLayer : INeuralLayer
{
    private readonly uint _inputLength;
    private readonly uint _outputLength;

    public LinearLayer(uint inputLength, uint outputLength)
    {
        _inputLength = inputLength;
        _outputLength = outputLength;
        OutputData = new DataVector(outputLength);
        Weights = new float[inputLength * outputLength];
    }

    /// <summary>
    /// OutData.Length * OutIndex + InIndex
    /// </summary>
    public float[] Weights { get; private set; }

    public DataVector InputData { get; private set; }

    public DataVector OutputData { get; private set; }

    public void Evaluate()
    {
        var outData = OutputData.Data;
        var inData = InputData.Data;
        for (int i = 0; i < outData.Length; i++)
        {
            outData[i] = 0;
            for (int j = 0; j < inData.Length; j++)
            {
                outData[i] += Weights[i * inData.Length + j] * inData[j];
            }
        }
    }

    public void SetInput(DataVector inputData)
    {
        if (inputData.TotalCount != _inputLength)
        {
            throw new System.Exception("Invalid data shape");
        }

        InputData = inputData;
    }
}
