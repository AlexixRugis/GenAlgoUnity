public interface INeuralLayer
{
    DataVector InputData { get; }
    DataVector OutputData { get; }

    /// <summary>
    /// ������������� ����. ����� ������� ��� ����� ������ ��������������
    /// </summary>
    /// <param name="inputData"></param>
    void SetInput(DataVector inputData);

    /// <summary>
    /// ��������� ������ � OutputData
    /// </summary>
    void Evaluate();
}
