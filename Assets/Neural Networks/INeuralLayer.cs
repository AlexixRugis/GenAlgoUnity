public interface INeuralLayer
{
    DataVector InputData { get; }
    DataVector OutputData { get; }

    /// <summary>
    /// Устанавливает ввод. Нужно сделать это перед первым использованием
    /// </summary>
    /// <param name="inputData"></param>
    void SetInput(DataVector inputData);

    /// <summary>
    /// Обновляет данные в OutputData
    /// </summary>
    void Evaluate();
}
