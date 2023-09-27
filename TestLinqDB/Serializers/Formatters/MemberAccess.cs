using System.Drawing;
using Serializers.Formatters;
using Expressions = System.Linq.Expressions;
namespace Serializers.Formatters;
[MemoryPack.MemoryPackable,global::MessagePack.MessagePackObject(true)]
public partial class MemberAccess対象:IEquatable<MemberAccess対象>{
    public int property=>1;
    public int field=4;
    public override bool Equals(object? obj){
        if(ReferenceEquals(null,obj)){
            return false;
        }
        if(ReferenceEquals(this,obj)){
            return true;
        }
        if(obj.GetType()!=this.GetType()){
            return false;
        }
        return this.Equals((MemberAccess対象)obj);
    }
    public bool Equals(MemberAccess対象? other){
        if(ReferenceEquals(null,other)){
            return false;
        }
        if(ReferenceEquals(this,other)){
            return true;
        }
        return this.field==other.field;
    }
    public override int GetHashCode(){
        return this.field;
    }
    public static bool operator==(MemberAccess対象? left,MemberAccess対象? right){
        return Equals(left,right);
    }
    public static bool operator!=(MemberAccess対象? left,MemberAccess対象? right){
        return!Equals(left,right);
    }
}
public class MemberAccess:共通 {
    [Fact]public void Serialize(){

        var Point=Expressions.Expression.Parameter(typeof(Point));
        this.MemoryMessageJson_Assert(new{a=default(Expressions.MemberExpression)});
        var input=Expressions.Expression.MakeMemberAccess(
            Expressions.Expression.Constant(new MemberAccess対象()),
            typeof(MemberAccess対象).GetProperty(nameof(MemberAccess対象.property))!
        );
        this.MemoryMessageJson_Assert(
            new{
                a=input,b=(Expressions.Expression)input
            }
        );
    }
}
