using LinqDB.Helpers;
using LinqDB.Sets;

using System.Collections;
using System.Text;
using LinqDB.Databases;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Sets;


public class ExtensionSet{
    private static Random r=new Random(1);
    private static Set<int>数列{
        get{
            var value=new Set<int>();
            for(var a=0;a<1000;a++){
                value.Add(a);
            }
            return value;
        }
    }
    //[Fact]public void Aggregate(){
    //    //0,1,2,3,4,5,6,7,8,9
    //    //0+1=1,1+1=2,2+2=4,4+3=7
    //    var expected=数列.Aggregate((a,b)=>a+b);
    //    //expected[0].
    //    var actual=数列.Lookup(p=>p/10).ToArray().OrderBy(p=>p.Key).ToArray();
    //}
    [Fact]public void Aggregate(){
        //0,1,2,3,4,5,6,7,8,9
        //0+1=1,1+1=2,2+2=4,4+3=7
        var expected=数列.Aggregate((a,b)=>a+b);
        //expected[0].
        var actual=数列.Lookup(p=>p/10).ToArray().OrderBy(p=>p.Key).ToArray();
    }
}