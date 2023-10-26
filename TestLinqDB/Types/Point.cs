using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
namespace Types;
[MemoryPack.MemoryPackable,MessagePack.MessagePackObject(true),Serializable]
internal partial class Point:IEquatable<Point>{
    [MessagePack.IgnoreMember]
    private int x;
    [MessagePack.IgnoreMember]
    private int y;
    public int X{
        get=>this.x;
        set=>this.x=value;
    }
    public int Y{
        get=>this.y;
        set=>this.y=value;

    }
//    [MessagePack.SerializationConstructor]
    public Point(int x,int Y){
        this.X=x;
        this.Y=Y;
    }
    public bool Equals(Point? other){
        if(ReferenceEquals(null,other)){
            return false;
        }
        if(ReferenceEquals(this,other)){
            return true;
        }
        return this.x==other.x&&this.y==other.y;
    }
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
        return this.Equals((Point)obj);
    }
    public override int GetHashCode(){
        return HashCode.Combine(this.X,this.Y);
    }
    public static bool operator==(Point? left,Point? right){
        return Equals(left,right);
    }
    public static bool operator!=(Point? left,Point? right){
        return!Equals(left,right);
    }
}
[MemoryPack.MemoryPackable,MessagePack.MessagePackObject(true),Serializable]
internal partial class Single{
    [MessagePack.IgnoreMember]
    private int x;
    //public int X;
    public int X{
        get=>this.x;
        set=>this.x=value;
    }
    //[MessagePack.SerializationConstructor]
    public Single(int X){
        this.X=X;
    }
}
