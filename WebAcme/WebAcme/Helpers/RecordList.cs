namespace ACME.Helper;

public class RecordList<T> where T : class
{
    public RecordList()
    {
        Data = new List<T>();
    }

    public List<T> Data { get; }
}
