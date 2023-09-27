using LinqDB.Helpers;
using LinqDB.Sets;

using System.Collections;
using System.Text;
using LinqDB.Databases;
namespace Sets;


public class Entity1{
    private class Entity実装:LinqDB.Sets.Entity<Container>{
    }
    [Fact]
    public void Test(){
        dynamic o=new NonPublicAccessor(new Entity実装());
        o.AddRelationship(new Container());
        o.RemoveRelationship();
    }
}