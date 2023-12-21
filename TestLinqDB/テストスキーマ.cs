//using System.Linq.Expressions;
//using Binder = Microsoft.CSharp.RuntimeBinder;
//using MessagePack;
using LinqDB.Databases;
using LinqDB.Sets;
namespace TestLinqDB{
    namespace Keys{
        [MemoryPack.MemoryPackable,MessagePack.MessagePackObject(true),Serializable]
        public readonly partial struct Key:IEquatable<Key>{
            public int a{get;}
            public Key(int a)=>this.a=a;
            public bool Equals(Key other)=>this.a==other.a;
        }
    }
    namespace Tables{
        [MemoryPack.MemoryPackable,MessagePack.MessagePackObject(true),Serializable]
        public partial class Table:Entity<Keys.Key,Container>,IEquatable<Table>{
            //public Keys.Key PrimaryKey{get;}
            public int field1{get;private set;}=0;
            public int field2{get;private set;}=0;
            public Table():base(default){}
            [MemoryPack.MemoryPackConstructor]
            public Table(Keys.Key Key):base(Key){}
            public Table(int a):base(new Keys.Key(a)){}
            public bool Equals(Table? other)=>other!=null&&this.Key.Equals(other.Key);
        }
    }
}