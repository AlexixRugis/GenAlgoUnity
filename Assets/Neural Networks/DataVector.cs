public class DataVector
{
    public uint TotalCount { get; private set; }
    public float[] Data { get; private set; }

    public DataVector(uint totalCount)
    {
        TotalCount = totalCount;
        Data = new float[TotalCount];
    }
}
