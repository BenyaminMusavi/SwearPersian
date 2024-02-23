using System.Reflection;
using Xunit.Sdk;

namespace SwearPersian.Test;

public class UnitTest1
{
    [Theory]
    //[MemberData("GetDataDrivenValues", MemberType = typeof(TestDataProvider))]
    [TestDataProvicer]
    public void Test1(string name,string family)
    {
        FilterPersianWords filterPersianWords = new FilterPersianWords();

        var result = filterPersianWords.Test(name, family);

        Assert.Equal(true, result);
    }
}

public class TestDataProvicerAttribute : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        var result = new List<object[]>
        {
            new object[] {"Ali" , "mousavi"},
            new object[] {"" , ""},
            new object[] {"خر" , "عن"},
        };
        return result;
    }
}

public class TestDataProvider
{
    public static List<object[]> GetDataDrivenValues()
    {
        var result = new List<object[]>
        {
            new object[] {"" , ""},
        };
        return result;
    }
}
