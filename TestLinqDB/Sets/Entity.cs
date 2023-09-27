using LinqDB.Helpers;
using LinqDB.Sets;

using System.Collections;
using System.Text;
namespace Sets;


public class Entity{
    private const string expected_Name="Name";
    private const string expected_Value="Value";
    private class Entity実装:LinqDB.Sets.Entity{
        protected override void ToStringBuilder(StringBuilder sb)=>ProtectedToStringBuilder(sb,"Name","Value");
    }
    [Fact]
    public void Test(){
        var expected=$"{expected_Name}:{expected_Value}";
        var actual=new Entity実装().ToString();
        Assert.Equal(expected,actual);
    }
}