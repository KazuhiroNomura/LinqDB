using LinqDB.Databases;
namespace TestLinqDB.Sets;


public class Entity2{
    private readonly struct Key:IEquatable<Key>{
        private readonly int key0,key1;
        public Key(int key0,int key1){
            this.key0=key0;
            this.key1=key1;
        }
        public bool Equals(Key other)=>this.key0==other.key0&&this.key1==other.key1;
        public override bool Equals(object? obj)=>obj is Key other&&this.Equals(other);
        public override int GetHashCode()=>HashCode.Combine(this.key0,this.key1);
    }
    private class Entity実装:LinqDB.Sets.Entity<Key,Container>{
        public readonly int value0,value1;
        public Entity実装(int key0,int key1,int value0,int value1):base(new(key0,key1)){
            this.value0=value0;
            this.value1=value1;
        }
    }
    [Fact]
    public void Test(){
        int key0=0,key1=1,value0=2,value1=3;
        var o=new Entity実装(key0,key1,value0,value1);
        var expected=new Key(key0,key1);
        Assert.Equal(expected,o.Key);
        Assert.Equal(expected.GetHashCode(),o.Key.GetHashCode());
    }
}