using System.Collections;
namespace TestLinqDB.Sets;


[Serializable,MessagePack.MessagePackObject]
public abstract class TestImmutableSet{
    [MemoryPack.MemoryPackInclude]
    protected string @abstract;
    [MemoryPack.MemoryPackConstructor]
    protected TestImmutableSet(){
    }
    [MemoryPack.MemoryPackInclude]
    protected int count=0;
    public int Count=>this.count;
}
//[Serializable,MessagePack.MessagePackObject]
public abstract class TestImmutableSet<T>:TestImmutableSet,ICollection<T>{
    //[MemoryPack.MemoryPackInclude]
    public IEnumerator<T> GetEnumerator(){
        throw new NotImplementedException();
    }
    IEnumerator IEnumerable.GetEnumerator(){
        return this.GetEnumerator();
    }
    public void Add(T item){
        throw new NotImplementedException();
    }
    public void Clear(){
        throw new NotImplementedException();
    }
    public bool Contains(T item){
        throw new NotImplementedException();
    }
    public void CopyTo(T[] array,int arrayIndex){
        throw new NotImplementedException();
    }
    public bool Remove(T item){
        throw new NotImplementedException();
    }
    public bool IsReadOnly{get;}
}
[Serializable,MessagePack.MessagePackObject,MemoryPack.MemoryPackable]
public partial class TestSet<T>:TestImmutableSet<T>,ICollection<T>{
    [MemoryPack.MemoryPackConstructor]
    public TestSet(){
    }
    [NonSerialized,MessagePack.IgnoreMember,MemoryPack.MemoryPackIgnore]
    private readonly List<T> List=new();
    public void Add(T item){
        this.List.Add(item);
        this.count++;
    }
    public override string ToString(){
        return this.count.ToString();
    }
    public void Clear(){
        throw new NotImplementedException();
    }
    public bool Contains(T item){
        throw new NotImplementedException();
    }
    public void CopyTo(T[] array,int arrayIndex){
        throw new NotImplementedException();
    }
    public bool Remove(T item){
        throw new NotImplementedException();
    }
    int ICollection<T>.Count=>(int)this.List.Count;
    public bool IsReadOnly=>false;
    public IEnumerator<T> GetEnumerator(){
        return this.List.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator(){
        return this.GetEnumerator();
    }
}
