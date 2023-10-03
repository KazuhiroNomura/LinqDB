using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using LinqDB.Sets;
using LinqDB.Sets.Exceptions;

using System.Text;
using IEnumerable=System.Collections.IEnumerable;
// ReSharper disable LocalizableElement

namespace TestLinqDB.Sets;


public class Test_Set1{
    private const int 要素数=8000;
    [Fact]
    public void Enumerator(){
        const int 繰り返し=100;
        var s=new Set<int>();
        for(var a=0;a<要素数;a++){
            s.Add(a);
        }
        {
            var count=0;
            var m=Stopwatch.StartNew();
            for(var i=0;i<繰り返し;i++){
                foreach(var a in s){
                    count+=a;
                }
            }
            Console.WriteLine("Enumerator "+m.ElapsedMilliseconds.ToString()+"ms");
        }
        {
            var count=0;
            var m=Stopwatch.StartNew();
            for(var i=0;i<繰り返し;i++){
                foreach(var a in(System.Collections.Generic.IEnumerable<int>)s){
                    count+=a;
                }
                //                    for(IEnumerator<Int32> a = s.GetEnumerator();a.MoveNext();) {
                //                      count+=a.Current;
                //                }
            }
            Console.WriteLine("IEnumerator<Int32> "+m.ElapsedMilliseconds.ToString()+"ms");
        }
        {
            var m=Stopwatch.StartNew();
            for(var i=0;i<繰り返し;i++){
                foreach(var a in(IEnumerable)s){
                }
            }
            Console.WriteLine("IEnumerator "+m.ElapsedMilliseconds.ToString()+"ms");
        }
    }
    [Fact]
    public void Add(){
        var s=new Set<int>();
        for(var a=0;a<要素数;a++) Assert.True(s.IsAdded(a));
        Assert.Equal(s.LongCount,要素数);
    }
    private readonly struct EntityKey:IEquatable<EntityKey>{
        public readonly long ID;
        public EntityKey(long ID){
            this.ID=ID;
        }
        public override bool Equals(object? obj)=>this.Equals((EntityKey)obj);
        public override int GetHashCode()=>this.ID.GetHashCode();
        public bool Equals(EntityKey other)=>this.ID==other.ID;
    }
    private class Container2:global::LinqDB.Databases.Container<Container2>{
    }
    private class Entity:Entity<EntityKey,Container2>,IEquatable<Entity>{
        public Entity(long ID):base(new EntityKey(ID)){
        }
        public bool Equals(Entity? other)=>other!=null&&this.PrimaryKey.Equals(other.PrimaryKey);

        protected override void ToStringBuilder(StringBuilder sb){
        }

        public override bool Equals(object? obj)=>obj is Entity other&&this.Equals(other);
    }
    [Fact]
    [SuppressMessage("ReSharper","ConvertToConstant.Local")]
    public void Assin0(){
        using var Container=new Container2();
        var 本体=new Set<EntityKey,Entity,Container2>(Container);
        var 履歴=new Set<EntityKey,Entity,Container2>(Container);
        {
            long L=0,R=0x1FFFFFFFE;
            var M=(L+R)/2;
            本体.IsAdded(new Entity(M));
            R=M-1;
            M=(L+R)/2;
            本体.IsAdded(new Entity(M));
        }
        {
            long L=0,R=0x1FFFFFFFE;
            var M=(L+R)/2;
            履歴.IsAdded(new Entity(M));
            R=M-1;
            M=(L+R)/2;
            R=M-1;
            M=(L+R)/2;
            履歴.IsAdded(new Entity(M));
        }
        本体.Assign(履歴);
        var (元衝突数,元LinkedNode数)=本体.衝突数;
        var (新衝突数,新LinkedNode数)=履歴.衝突数;
        Assert.Equal(元衝突数,新衝突数);
        Assert.Equal(元LinkedNode数,新LinkedNode数);
    }
    [Fact]
    public void Assin1(){
        const int 要素数=1000;
        var Container=new Container2();
        var 本体=new Set<EntityKey,Entity,Container2>(Container);
        var 履歴=new Set<EntityKey,Entity,Container2>(Container);
        for(var a=0;a<要素数;a++){
            履歴.IsAdded(new Entity(a));
        }
        for(var a=0;a<要素数;a++){
            本体.IsAdded(new Entity(a+要素数/2));
        }
        本体.Assign(履歴);
        var (元衝突数,元LinkedNode数)=履歴.衝突数;
        var (新衝突数,新LinkedNode数)=本体.衝突数;
        Assert.Equal(元衝突数,新衝突数);
        Assert.Equal(元LinkedNode数,新LinkedNode数);
    }
    [Fact]
    public void Assin2(){
        const int 要素数=100000;
        var r=new Random(1);
        var Container=new Container2();
        var 本体=new Set<EntityKey,Entity,Container2>(Container);
        var 履歴=new Set<EntityKey,Entity,Container2>(Container);
        for(var a=0;a<要素数;a++){
            var 履歴Value=r.Next(要素数);
            履歴.IsAdded(new Entity(履歴Value));
        }
        for(var a=0;a<要素数;a++){
            var 本体Value=r.Next(要素数);
            本体.IsAdded(new Entity(本体Value));
        }
        本体.Assign(履歴);
        var (元衝突数,元LinkedNode数)=履歴.衝突数;
        var (新衝突数,新LinkedNode数)=本体.衝突数;
        Assert.Equal(元衝突数,新衝突数);
        Assert.Equal(元LinkedNode数,新LinkedNode数);
        Assert.True(本体.SetEquals(履歴));
    }
    [Fact]
    public void Clear(){
        var s=new Set<int>{1,2};
        s.Clear();
        foreach(var a in s){
            Assert.Fail(a+"は不要");
        }
        Assert.Equal(s.LongCount,0);
    }








    [Fact]
    public void ctor0(){
        Assert.NotNull(new Set<int>());
    }
    [Fact]
    public void ctor1(){
        Assert.NotNull(new Set<int>{10});
    }
    [Fact]
    public void ctor2(){
        Assert.NotNull(new Set<int>{1,2});
    }
    [Fact]
    public void ctor3(){
        Assert.NotNull(new Set<int>(new[]{1,2}.ToSet()));
    }
    [Fact]
    public void ctor4(){
        Assert.NotNull(new Set<int>(new[]{1,2}.AsEnumerable()));
    }
    [Fact]
    public void SymmetricExceptWith(){
        var a=new Set<int>{0,1,2,3};
        var w=new Set<int>(a);
        w.SymmetricExceptWith(a);
        Assert.Equal(new Set<int>(),w);
        w=new Set<int>(a);
        w.SymmetricExceptWith(new Set<int>{2,3,4,5});
        Assert.Equal(new Set<int>{0,1,4,5},w);
        w=new Set<int>(a);
        w.SymmetricExceptWith(new Set<int>{4,5,6,7});
        Assert.Equal(new Set<int>{
            0,
            1,
            2,
            3,
            4,
            5,
            6,
            7
        },w);
    }
    [Fact]
    public void IsSubsetOf(){
        Assert.True(new Set<int>{0,1}.IsSubsetOf(new Set<int>{0,1}));
        Assert.False(new Set<int>{0,1}.IsSubsetOf(new Set<int>{0}));
        Assert.True(new Set<int>{1}.IsSubsetOf(new Set<int>{0,1}));
        Assert.False(new Set<int>{0,1}.IsSubsetOf(new Set<int>{2}));
        Assert.True(new Set<int>().IsSubsetOf(new Set<int>()));
    }
    [Fact]
    public void IsProperSubsetOf(){
        Assert.False(new Set<int>{0,1}.IsProperSubsetOf(new Set<int>{0,1}));
        Assert.False(new Set<int>{0,1}.IsProperSubsetOf(new Set<int>{0}));
        Assert.True(new Set<int>{1}.IsProperSubsetOf(new Set<int>{0,1}));
        Assert.False(new Set<int>{0,1}.IsProperSubsetOf(new Set<int>{2}));
        Assert.False(new Set<int>().IsProperSubsetOf(new Set<int>()));
    }
    [Fact]
    public void IsSupersetOf(){
        Assert.True(new Set<int>{0,1}.IsSupersetOf(new Set<int>{0,1}));
        Assert.True(new Set<int>{0,1}.IsSupersetOf(new Set<int>{0}));
        Assert.False(new Set<int>{1}.IsSupersetOf(new Set<int>{0,1}));
        Assert.False(new Set<int>{0,1}.IsSupersetOf(new Set<int>{2}));
        Assert.True(new Set<int>().IsSupersetOf(new Set<int>()));
    }
    [Fact]
    public void IsProperSupersetOf(){
        Assert.False(new Set<int>{0,1}.IsProperSupersetOf(new Set<int>{0,1}));
        Assert.True(new Set<int>{0,1}.IsProperSupersetOf(new Set<int>{0}));
        Assert.False(new Set<int>{1}.IsProperSupersetOf(new Set<int>{0,1}));
        Assert.False(new Set<int>{0,1}.IsProperSupersetOf(new Set<int>{2}));
        Assert.False(new Set<int>().IsProperSupersetOf(new Set<int>()));
    }
    [Fact]
    public void Overlaps(){
        Assert.True(new Set<int>{0,1}.Overlaps(new Set<int>{0,1}));
        Assert.True(new Set<int>{0,1}.Overlaps(new Set<int>{0}));
        Assert.True(new Set<int>{1}.Overlaps(new Set<int>{0,1}));
        Assert.False(new Set<int>{0,1}.Overlaps(new Set<int>{2}));
        Assert.False(new Set<int>().Overlaps(new Set<int>()));
    }
    [Fact]
    public void Equals0(){
        var d0=new Set<Int>{1,2,1+(Int)int.MaxValue};
        var d1=new Set<Int>{1,2,2+(Int)int.MaxValue};
        var d2=new Set<Int>{1,2,1+(Int)int.MaxValue};
        var d3=new Set<Int>{1,2};
        var d4=new Set<Int>{1,2,1+(Int)int.MaxValue};
        Assert.False(d0.Equals(d1));
        Assert.True(d0.Equals(d2));
        Assert.False(d0.Equals(d3));
        Assert.True(d0.Equals(d4));
    }
    [Fact]
    public void Equals1(){
        var d0=new Set<Int>{1,2,1+(Int)int.MaxValue};
        var d1=new Set<Int>{1,2,2+(Int)int.MaxValue};
        var d2=new Set<Int>{1,2,1+(Int)int.MaxValue};
        var d3=new Set<Int>{1,2};
        var d4=new Set<Int>{1,2,1+(Int)int.MaxValue};
        object o0=d0;
        object o1=d1;
        object o2=d2;
        object o3=d3;
        object o4=d4;
        Assert.False(o0.Equals(o1));
        Assert.True(o0.Equals(o2));
        Assert.False(o0.Equals(o3));
        Assert.True(o0.Equals(o4));
        Assert.False(o0.Equals(null));
    }
    [Fact]
    public void Equals2(){
        Assert.True(new Set<int>{0,1}.Equals(new Set<int>{1,0}));
        Assert.False(new Set<int>{0,1}.Equals(new Set<int>{0}));
        Assert.False(new Set<int>{1,2}.Equals(new Set<int>{0,1}));
        Assert.True(new Set<int>().Equals(new Set<int>()));
    }
    [Fact]
    public void Equals3(){
        for(var Count=0;Count<100;Count++){
            var e=new Set<int>();
            for(var i=0;i<Count;i++){
                e.IsAdded(i);
            }
            var a=new Set<int>();
            for(var i=0;i<Count;i++){
                a.IsAdded(i);
            }
            Assert.Equal(e,a);
        }
    }
    [Fact]
    public void ToArray(){
        var d=new Set<Int>{1,2};
        var a=d.ToArray();
        Assert.Equal(d.LongCount(),a.Length);
    }
    [Fact]
    public void ToString0(){
        var d=new Set<int>{1,2};
        Assert.Equal($"{nameof(ImmutableSet.LongCount)}:2",d.ToString());
    }
    [Fact]
    public void UpdateWith(){
        var d=new Set<Int>{1,2};
        d.UpdateWith(p=>p+1,p=>p==2);
        Assert.Equal(d,new Set<Int>{1,3});
    }
    [Fact]
    public void DeleteWith(){
        Int offset=1L+uint.MaxValue;
        {
            var d=new Set<Int>{
                0,
                1,
                2,
                3,
                16,
                17
            };//バケット数17
            {
                var s=new Set<Int>(d);
                s.DeleteWith(p=>p==4);
                Assert.Equal(d,s);
            }
            {
                var s=new Set<Int>(d);
                s.DeleteWith(p=>p==16);
                Assert.Equal(new Set<Int>{
                    0,
                    1,
                    2,
                    3,
                    17
                },s);
            }
            {
                var s=new Set<Int>(d);
                s.DeleteWith(p=>p==17);
                Assert.Equal(new Set<Int>{
                    0,
                    1,
                    2,
                    3,
                    16
                },s);
            }
        }
        {
            var d=new Set<Int>{
                0,
                1,
                2,
                3,
                16,
                16+offset
            };
            {
                var s=new Set<Int>(d);
                s.DeleteWith(p=>p==16);
                Assert.Equal(new Set<Int>{
                    0,
                    1,
                    2,
                    3,
                    16+offset
                },s);
            }
            {
                var s=new Set<Int>(d);
                s.DeleteWith(p=>p==16+offset);
                Assert.Equal(new Set<Int>{
                    0,
                    1,
                    2,
                    3,
                    16
                },s);
            }
        }
        {
            var d=new Set<Int>{0,16,16+offset,16+offset+offset};
            {
                var s=new Set<Int>(d);
                s.DeleteWith(p=>p==16);
                Assert.Equal(new Set<Int>{0,16+offset,16+offset+offset},s);
            }
            {
                var s=new Set<Int>(d);
                s.DeleteWith(p=>p==16+offset);
                Assert.Equal(new Set<Int>{0,16,16+offset+offset},s);
            }
            {
                var s=new Set<Int>(d);
                s.DeleteWith(p=>p==16+offset+offset);
                Assert.Equal(new Set<Int>{0,16,16+offset},s);
            }
        }
    }
    [Fact]
    public void IntersectWith(){
        {
            var d=new Set<Int>{1,2,3};
            d.IntersectWith(new Set<Int>{1,2,3});
            Assert.Equal(new Set<Int>{1,2,3},d);
        }
        {
            var d=new Set<Int>{1,2,3};
            d.IntersectWith(new Set<Int>{2,3,4});
            Assert.Equal(new Set<Int>{2,3},d);
        }
        {
            var d=new Set<Int>{1,2,3};
            d.IntersectWith(new Set<Int>{4,5,6});
            Assert.Equal(new Set<Int>(),d);
        }
    }
    [Fact]
    public void UnionWith(){
        {
            var d=new Set<Int>{1,2,3};
            d.UnionWith(new Set<Int>{1,2,3});
            Assert.Equal(new Set<Int>{1,2,3},d);
        }
        {
            var d=new Set<Int>{1,2,3};
            d.UnionWith(new Set<Int>{2,3,4});
            Assert.Equal(new Set<Int>{1,2,3,4},d);
        }
        {
            var d=new Set<Int>{1,2,3};
            d.UnionWith(new Set<Int>{4,5,6});
            Assert.Equal(new Set<Int>{
                1,
                2,
                3,
                4,
                5,
                6
            },d);
        }
    }
    [Fact]
    public void DUnionWith(){
        var d=new Set<Int>{1,2,3};
        d.DUnionWith(new Set<Int>{4,5,6});
        Assert.Equal(new Set<Int>{
            1,
            2,
            3,
            4,
            5,
            6
        },d);
    }
    [Fact]
    public void ExceptWith(){
        {
            var s0=new Set<Int>();
            var s1=new Set<Int>(s0);
            Assert.Equal(s1,s0);
            s0.ExceptWith(new Set<Int>());
            Assert.Equal(new Set<Int>(),s0);
        }
        {
            var s0=new Set<Int>{1};
            var s1=new Set<Int>(s0);
            Assert.Equal(s1,s0);
            s0.ExceptWith(new Set<Int>{});
            Assert.Equal(new Set<Int>{1},s0);
        }
        {
            var s0=new Set<Int>{1,2};
            var s1=new Set<Int>(s0);
            Assert.Equal(s1,s0);
            s0.ExceptWith(new Set<Int>{2});
            Assert.Equal(new Set<Int>{1},s0);
        }
        {
            var s0=new Set<Int>{
                1,
                2,
                3,
                4,
                5,
                6
            };
            var s1=new Set<Int>(s0);
            Assert.Equal(s1,s0);
            s0.ExceptWith(new Set<Int>{4,5,6});
            Assert.Equal(new Set<Int>{1,2,3},s0);
        }
    }
    [Fact]
    public void Remove(){
        var s=new Set<int>();
        for(var a=0;a<要素数;a++) Assert.True(s.IsAdded(a));
        for(var a=0;a<要素数;a++) Assert.True(s.Remove(a));
        Assert.Equal(s.LongCount,0);
    }
    [Fact]
    public void Remove1(){
        var s=new Set<int>();
        const int MAX=3;
        for(var a=0;a<MAX;a++){
            Assert.True(s.IsAdded(a));
        }
        for(var a=0;a<MAX;a++){
            Assert.True(s.Remove(a));
        }
        s.Assert();
    }
    [Fact]
    public void Remove2(){
        var s=new Set<int>();
        for(var a=0;a<1000;a++){
            Assert.True(s.IsAdded(a));
        }
        for(var a=999;a>=0;a--){
            Assert.True(s.Remove(a));
        }
        s.Assert();
    }
    [Fact]
    public void Remove3(){
        var s=new Set<int>();
        for(var a=0;a<1000;a+=2){
            Assert.True(s.IsAdded(a));
        }
        for(var a=0;a<1000;a++){
            if(a==999){

            }
            if(a%2==0){
                Assert.True(s.Remove(a));
            } else{
                Assert.False(s.Remove(a));
            }
        }
        s.Assert();
    }
    [Fact]
    public void Remove4(){
        var s=new Set<int>();
        var r=new Random(1);
        const int MAX=10000;
        for(var a=0;a<MAX;a++){
            s.IsAdded(r.Next(MAX));
        }
        for(var a=0;a<10000;a+=2){
            s.Remove(r.Next(MAX));
        }
        s.Assert();
    }
    [Fact]
    public void SetAssert(){
        var r=new Random(1);
        for(var a=1;a<=100;a++){
            var Set=new Set<int>();
            for(var b=0;b<a;b++){
                Set.IsAdded(r.Next(a));
                Set.Remove(r.Next(a));
                Set.Assert();
            }
        }
    }
    [Fact]
    public void DUnionWith_UniqueTupleException()=>
        Assert.Throws<OneTupleException>(()=>{
            var d=new Set<Int>{1,2,3};
            d.DUnionWith(new Set<Int>{2,3,4});
        });
}
