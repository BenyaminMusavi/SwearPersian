namespace SwearPersian;

public class NewObj
{
    FilterPersianWords p = new FilterPersianWords();

    [SwearPersianFilter]
    public string Name { get; set; }

    [SwearPersianFilter]
    public string Family { get; set; }
}
