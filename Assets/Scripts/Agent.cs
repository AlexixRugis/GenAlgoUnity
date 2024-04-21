using System.Runtime.CompilerServices;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField] private float _maxEnergy;

    [SerializeField] private CharacterController _characterController;
    [SerializeField] private DistanceSensor _distanceSensor;

    public float CurrentEnergy { get; set; }

    private AgentNetwork _network;
    private DataVector _input;
    private DataVector _output;

    private void Start()
    {
        _network = new AgentNetwork();
        _input = new DataVector(19);
        _input.Data[17] = 1f;
        _network.SetInput(_input);
        _output = _network.OutputData;

        CurrentEnergy = _maxEnergy;
    }

    private void Update()
    {
        if (CurrentEnergy <= 0f)
        {
            Destroy(gameObject);
            return;
        }

        CurrentEnergy -= Time.deltaTime;

        _distanceSensor.Evaluate();
        for (int i = 0; i < _input.Data.Length - 2; i++)
        {
            _input.Data[i] = _distanceSensor.OutputData[i] / _distanceSensor.MaxDistance;
        }
        _input.Data[18] = CurrentEnergy / _maxEnergy;

        _network.Evaluate();
        float rotateInput = 2f * _output.Data[0] - 1f;
        float forwardInput = 2f * _output.Data[1] - 1f;

        Debug.Log(string.Join(' ', _output.Data));

        transform.Rotate(0, rotateInput * 30f * Time.deltaTime, 0f);
        _characterController.Move(transform.forward * forwardInput * 3f * Time.deltaTime);
    }

    public void AddEnergy(float delta)
    {
        CurrentEnergy += delta;
        CurrentEnergy = Mathf.Max(CurrentEnergy, _maxEnergy);
    }
}
