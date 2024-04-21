using System;
using System.Collections.Generic;

public class AgentNetwork : INeuralLayer
{
    public DataVector InputData { get; private set; }
    public DataVector OutputData { get; private set; }

    private List<INeuralLayer> _layers = new List<INeuralLayer>();

    public AgentNetwork()
    {
        BuildNetwork();
    }

    private void BuildNetwork()
    {
        LinearLayer l1 = new LinearLayer(19, 2);
        Random r = new Random();
        for (int i = 0; i < l1.Weights.Length; i++) 
        {
            l1.Weights[i] = (float)r.NextDouble();
        }

        SigmoidLayer sigmoid1 = new SigmoidLayer();
        sigmoid1.SetInput(l1.OutputData);

        _layers.Add(l1);
        _layers.Add(sigmoid1);

        OutputData = sigmoid1.OutputData;
    }

    public void Evaluate()
    {
        for (int i = 0; i < _layers.Count; i++)
        {
            _layers[i].Evaluate();
        }
    }

    public void SetInput(DataVector inputData)
    {
        InputData = inputData;
        _layers[0].SetInput(InputData);
    }
}
