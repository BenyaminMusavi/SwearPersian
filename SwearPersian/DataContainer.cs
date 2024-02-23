namespace SwearPersian;

public class DataContainer
{
    private List<string> dataList;

    public DataContainer()
    {
        dataList = new List<string>();
        dataList.Add("خر");
        dataList.Add("عن");
        dataList.Add("حساس");
    }

    public void AddData(string data)
    {
        dataList.Add(data);
    }
    public bool Any(Func<string, bool> predicate)
    {
        return dataList.Any(predicate);
    }

    public IReadOnlyList<string> GetStrings()
    {
        return dataList.ToArray();
    }
}
