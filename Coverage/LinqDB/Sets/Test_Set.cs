using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text;
using LinqDB.Databases;
using LinqDB.Helpers;
using LinqDB.Optimizers;
using LinqDB.Sets;
using LinqDB.Sets.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable EqualExpressionComparison
namespace CoverageCS.LinqDB.Sets;

public class Container2:Container<Container2>{
}
public static class PrimaryKeys{
    // ReSharper disable once MemberHidesStaticFromOuterClass
    [Serializable]
    [SuppressMessage("ReSharper","PossibleNullReferenceException")]
    public struct Entity:IEquatable<Entity>{
        public decimal ID1{get;}
        public decimal ID2{get;}
        public Entity(decimal ID1,decimal ID2){
            this.ID1=ID1;
            this.ID2=ID2;
        }
        public bool Equals(Entity other){
            // ReSharper disable once PossibleNullReferenceException
            if(!this.ID1.Equals(other.ID1)) return false;
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if(!this.ID2.Equals(other.ID2)) return false;
            return true;
        }
        public override bool Equals(object other){
            Contract.Assert(other!=null,"obj != null");
            return this.Equals((Entity)other);
        }
        public override int GetHashCode()=>this.ID1.GetHashCode()^this.ID2.GetHashCode();
        public static bool operator==(Entity x,Entity y)=>x.Equals(y);
        public static bool operator!=(Entity x,Entity y)=>!x.Equals(y);
        public int CompareTo(Entity other)=>this.GetHashCode()-other.GetHashCode();
        public override string ToString()=>"ID1="+this.ID1.ToString(CultureInfo.CurrentCulture)+",ID2="+this.ID2.ToString(CultureInfo.CurrentCulture);

    }
}
[TestClass]
public class Tables{
    [Serializable]
    public class Entity:Entity<PrimaryKeys.Entity,Container>,IEquatable<Entity>, IWriteRead<Entity> {
        public decimal ID1=>this.PrimaryKey.ID1;
        public decimal ID2=>this.PrimaryKey.ID2;
        public readonly decimal ID3;
        public Entity(decimal ID1):base(new PrimaryKeys.Entity(ID1,0)){
        }
        public Entity(decimal ID1,decimal ID2,decimal ID3):base(new PrimaryKeys.Entity(ID1,ID2)){
            this.ID3=ID3;
        }
        public bool Equals(Entity? other){
            if(other is null) return false;
            if(!this.PrimaryKey.Equals(other.PrimaryKey)) return false;
            return this.ID3.Equals(other.ID3);
        }
        public override bool Equals(object? obj){
            return obj is Entity other&&this.Equals(other);
        }
        public static implicit operator Entity(int a)=>new(a,a,a);
        public static bool operator==(Entity? a,Entity? b)=>a?.Equals(b)??b is null;
        public static bool operator!=(Entity? a,Entity? b)=>!(a==b);
        protected override void ToStringBuilder(StringBuilder sb){
            sb.Append("ID1=");
            sb.AppendLine(this.ID1.ToString());
            sb.Append("ID2=");
            sb.AppendLine(this.ID2.ToString());
            sb.Append("ID3=");
            sb.AppendLine(this.ID3.ToString());
        }

        public void BinaryWrite(BinaryWriter Writer) => throw new NotImplementedException();
        public void BinaryRead(BinaryReader Reader,Func<Entity> Create) => throw new NotImplementedException();
        public void TextWrite(IndentedTextWriter Writer) => throw new NotImplementedException();
        public void TextRead(StreamReader Reader,int Indent) => throw new NotImplementedException();
    }
    private static readonly Entity[] EnumerateData={
        new(1,1,1),new(2,2,2),new(3,3,3)
    };
    private static readonly Set<Entity> SetData=new(EnumerateData);
    private const int 試行回数=100;
    [TestMethod]
    public void Aggregate(){
        var r=new Random(1);
        for(var a=0;a<試行回数;a++){
            decimal seed=r.Next();
            EnumerateData.Aggregate(seed,(accumulate,entity)=>accumulate+entity.ID3,accumulate=>accumulate);
            {
                var 正解=EnumerateData.Aggregate(seed,(accumulate,entity)=>accumulate+entity.ID3,accumulate=>accumulate);
                var 回答=SetData.Aggregate(seed,(accumulate,entity)=>accumulate+entity.ID3,accumulate=>accumulate);
                Assert.AreEqual(正解,回答);
            }
            {
                var 正解=EnumerateData.Aggregate(seed,(accumulate,entity)=>accumulate+entity.ID3,accumulate=>accumulate);
                var 回答=SetData.Aggregate(seed,(accumulate,entity)=>accumulate+entity.ID3,accumulate=>accumulate);
                Assert.AreEqual(正解,回答);
            }
        }
        Assert.AreEqual(
            new Int(9),
            new Set<Int>{
                1,6
            }.Aggregate(new Int(1),(p,index)=>p+index,p=>p+1)
        );
    }
    private static readonly EnumerableSetEqualityComparer Comparer=new();
    [TestMethod]
    public void All(){
        void Equal(object a,object b)=>Assert.IsTrue(Comparer.Equals(a,b));
        void NotEqual(object a,object b)=>Assert.IsFalse(Comparer.Equals(a,b));
        for(var a=0;a<試行回数;a++){
            Equal(
                SetData.All(entity=>entity.ID1==entity.ID1),
                SetData.All(entity=>entity.ID2==entity.ID2)
            );
            Equal(
                EnumerateData.All(entity=>entity.ID1==entity.ID1),
                EnumerateData.All(entity=>entity.ID2==entity.ID2)
            );
            NotEqual(
                SetData.All(entity=>entity.ID1==entity.ID1),
                SetData.All(entity=>entity.ID1!=entity.ID1)
            );
            NotEqual(
                EnumerateData.All(entity=>entity.ID1==entity.ID1),
                EnumerateData.All(entity=>entity.ID1!=entity.ID1)
            );
        }
        Assert.IsFalse(new Set<long>{
            1,2
        }.All(p=>p%2==0));
        Assert.IsTrue(new Set<long>{
            2,4
        }.All(p=>p%2==0));
    }
    [TestMethod]
    public void Any(){
        for(var a=0;a<試行回数;a++){
            Assert.IsTrue(SetData.Any());
            Assert.IsTrue(EnumerateData.Any());
        }
        var d=new Set<int>();
        Assert.IsFalse(d.Any());
        d.Add(0);
        Assert.IsTrue(d.Any());
    }
    [TestMethod]
    public void Average(){
        for(var a=0;a<試行回数;a++){
            Assert.AreEqual(
                EnumerateData.Average(p=>(double)p.ID3),
                SetData.Average(p=>(double)p.ID3)
            );
        }
    }
    [TestMethod]
    public void AvedevDouble_selector(){
        //解答：算術平均値は (2+3+4+7+9) / 5=5 
        //平均偏差は (|1−6.2|+|2−6.2|+|4−6.2|+|8−6.2|+|16−6.2|)/5=(-5.2-4.2-2.2+1.8.9.8)/5=2.4
        Assert.AreEqual(4.6400000000000006,new Set<double>{
            1,
            2,
            4,
            8,
            16
        }.Avedev(p=>p));
    }
    [TestMethod]
    public void Contains(){
        {
            var s=new Entity(1,1,1);
            for(var a=0;a<試行回数;a++){
                Assert.AreEqual(
                    EnumerateData.Contains(s),
                    SetData.Contains(s)
                );
                Assert.IsTrue(SetData.Contains(s));
            }
        }
        {
            var s=new Set<int>{
                1,2
            };
            Assert.IsFalse(s.Contains(0));
            Assert.IsTrue(s.Contains(1));
        }
    }
    [TestMethod]
    public void Count(){
        for(var a=0;a<試行回数;a++){
            Assert.AreEqual(
                EnumerateData.LongCount(),
                SetData.LongCount()
            );
        }
    }
    [TestMethod]
    public void Delete(){
        foreach(var a in EnumerateData){
            Assert.IsTrue(SetData.Delete(p=>p==a).Count==SetData.Count-1);
            Assert.IsTrue(SetData.Where(p=>p!=a).Delete(q=>q.ID1%2==0).Count>0);
            Assert.IsTrue(
                Comparer.Equals(
                    EnumerateData.Where(p=>p!=a),
                    SetData.Delete(p=>p==a)
                )
            );
        }
        {
            var d=new Set<Int>{
                0,
                1,
                2,
                3,
                16,
                17
            };
            Assert.AreEqual(d,d.Delete(p=>p==4));
            Assert.AreEqual(new Set<Int>{
                0,
                1,
                2,
                3,
                17
            },d.Delete(p=>p==16));
            Assert.AreEqual(new Set<Int>{
                0,
                1,
                2,
                3,
                16
            },d.Delete(p=>p==17));
        }
    }
    private static Entity[] UnionExceptIntersectのEntity(Random r){
        var dn1=new Entity[r.Next(10)];
        var low1=0;
        for(var b=0;b<dn1.Length;b++){
            var value=r.Next(low1,low1+5);
            var ID1=b/10;
            var ID2=b%10;
            var ID3=b;
            dn1[b]=new Entity(ID1,ID2,ID3);
            low1=value+1;
        }
        return dn1;
    }
    [TestMethod,ExpectedException(typeof(OneTupleException))]
    public void DUnionException1(){
        var s1=new[]{
            new Entity(0,0,0)
        }.ToSet();
        var s2=new[]{
            new Entity(0,0,0)
        }.ToSet();
        Assert.AreEqual(1,s1.DUnion(s2));
    }
    [TestMethod,ExpectedException(typeof(OneTupleException))]
    public void DUnionException2(){
        var s1=new[]{
            new Entity(0,0,0),new Entity(0,0,1)
        }.ToSet();
        var s2=new[]{
            new Entity(0,0,1)
        }.ToSet();
        Assert.AreEqual(2,s1.DUnion(s2));
    }
    [TestMethod]
    public void Equals(){
        var r=new Random(1);
        for(var a=0;a<試行回数;a++){
            var dn1=new Entity[r.Next(2)];
            var low1=0;
            for(var b=0;b<dn1.Length;b++){
                var value=r.Next(low1,low1+2);
                var ID1=b/10;
                var ID2=b%10;
                var ID3=b;
                dn1[b]=new Entity(ID1,ID2,ID3);
                low1=value+1;
            }
            var dn2=new Entity[r.Next(2)];
            var low2=0;
            for(var b=0;b<dn2.Length;b++){
                var value=r.Next(low2,low2+2);
                var ID1=b/10;
                var ID2=b%10;
                var ID3=b;
                dn2[b]=new Entity(ID1,ID2,ID3);
                low2=value+1;
            }
            dn1.ToSet().Except(dn2.ToSet());
            dn2.ToSet().Except(dn1.ToSet());
        }
    }
    [TestMethod]
    public void Except(){
        var r=new Random(1);
        for(var a=0;a<試行回数;a++){
            var dn1=UnionExceptIntersectのEntity(r);
            var dn2=UnionExceptIntersectのEntity(r);
            var sn1=dn1.ToSet();
            var sn2=dn2.ToSet();
            dn1.Except(dn2).Count();
            dn2.Except(dn1).Count();
            sn1.Except(sn2).LongCount();
            sn2.Except(sn1).LongCount();
        }
        Assert.AreEqual(new Set<Int>{
            1,2,3
        },new Set<Int>{
            1,
            2,
            3,
            4,
            5,
            6
        }.Except(new Set<Int>{
            4,5,6
        }));
    }
    public void GroupBy(){
        for(var a=0;a<試行回数;a++){
            var EnumerateData=new Entity[4];
            for(var b=0;b<EnumerateData.Length;b++){
                var ID1=b/2;
                var ID2=b%2;
                var ID3=b;
                EnumerateData[b]=new Entity(ID1,ID2,ID3);
            }
            Assert.IsTrue(EnumerateData.GroupBy(p=>p.ID1).Count()>0);
            Assert.IsTrue(EnumerateData.GroupBy(p=>p.ID1).ToSet().Count()>0);
            Assert.IsTrue(EnumerateData.GroupBy(p=>p.ID1).Select(p=>p).Count()>0);
            Assert.IsTrue(EnumerateData.GroupBy(p=>p.ID1).Select(p=>p).ToSet().Count()>0);
            Assert.IsTrue(SetData.GroupBy(p=>p.ID1).Select(p=>p).Count()>0);
            Assert.IsTrue(EnumerateData.GroupBy(p=>p.ID1).Select(p=>p).ToSet().Count()>0);
            Assert.IsTrue(SetData.GroupBy(p=>p.ID1).Select(p=>p).ToSet().Count()>0);
        }
        var r=new Random(1);
        for(var a=0;a<試行回数;a++){
            var EnumerateData=new Entity[r.Next(100)];
            var low1=0;
            for(var b=0;b<EnumerateData.Length;b++){
                var value=r.Next(low1,low1+5);
                var ID1=b/10;
                var ID2=b%10;
                var ID3=b;
                EnumerateData[b]=new Entity(ID1,ID2,ID3);
                low1=value+1;
            }
            Assert.IsTrue(EnumerateData.GroupBy(p=>p.ID1).ToSet().Count>0);
            var SetData=EnumerateData.ToSet();
            Assert.IsTrue(EnumerateData.GroupBy(p=>p.ID1).Count()>0);
            Assert.IsTrue(EnumerateData.GroupBy(p=>p.ID1).Select(p=>p).Count()>0);
            Assert.IsTrue(EnumerateData.GroupBy(p=>p.ID1).Select(p=>p).ToSet().Count()>0);
            Assert.IsTrue(SetData.GroupBy(p=>p.ID1).Select(p=>p).Count>0);
            Assert.IsTrue(EnumerateData.GroupBy(p=>p.ID1).Select(p=>p).ToSet().Count()>0);
            Assert.IsTrue(SetData.GroupBy(p=>p.ID1).Select(p=>p).Count>0);
        }
    }
    [TestMethod]
    public void GroupJoin(){
        var r=new Random(1);
        for(var a=0;a<試行回数;a++){
            var a1=new Entity[r.Next(10)];
            for(var b=0;b<a1.Length;b++){
                var ID1=b/10;
                var ID2=b%10;
                var ID3=b;
                a1[b]=new Entity(ID1,ID2,ID3);
            }
            var a2=new Entity[r.Next(10)];
            for(var b=0;b<a2.Length;b++){
                var ID1=b/10;
                var ID2=b%10;
                var ID3=b;
                a2[b]=new Entity(ID1,ID2,ID3);
            }
            var k1=new Set<Entity>();
            var 要素数=1;
            var k3=new Entity[要素数];
            for(var b=0;b<要素数;b++){
                var v=b+1;
                var item=new Entity(v,v,v);
                k1.Add(item);
                k3[b]=item;
            }
            //A.GroupJoin(B.Where(b=>false),o=>o,i=>i,(o,i)=>new{o,i})
            //A.GroupJoin(InternalCondition(B,false),o=>o,i=>i,(o,i)=>new{o,i})
            //false?new Set<>:A.GroupJoin(B,o=>o,i=>i,(o,i)=>new{o,i})
            //にする。
            // ReSharper disable once InvokeAsExtensionMethod
            Assert.IsTrue(Enumerable.GroupJoin(new Set<int>{
                1
            },Enumerable.Where(new Set<int>{
                1
            },p=>false),o=>o,i=>i,(o,i)=>new{
                o,i
            }).Count()>0);
            Assert.IsTrue(k1.GroupJoin(k1,o=>o.PrimaryKey,i=>i.PrimaryKey,(outer,inner)=>new{
                outer,inner
            }).Count()>0);
            Assert.IsTrue(k3.GroupJoin(k3,o=>o.ID1,i=>i.ID1,(outer,inner)=>inner).Count()>0);
            Assert.IsTrue(k3.GroupJoin(k3,o=>o.PrimaryKey,i=>i.PrimaryKey,(outer,inner)=>inner).Count()>0);
        }
        var expected=Set作成(
            new{
                o=0L,i=(ImmutableSet<long>)new Set<long>()
            },
            new{
                o=1L,i=(ImmutableSet<long>)new Set<long>()
            },
            new{
                o=2L,
                i=(ImmutableSet<long>)new Set<long>{
                    2,3
                }
            },
            new{
                o=3L,
                i=(ImmutableSet<long>)new Set<long>{
                    2,3
                }
            },
            new{
                o=4L,
                i=(ImmutableSet<long>)new Set<long>{
                    4,5
                }
            },
            new{
                o=5L,
                i=(ImmutableSet<long>)new Set<long>{
                    4,5
                }
            }
        );
        var actual=new Set<long>{
            0,
            1,
            2,
            3,
            4,
            5
        }.GroupJoin(new Set<long>{
            2,
            3,
            4,
            5,
            6,
            7
        },o=>o/2,i=>i/2,(o,i)=>new{
            o,i
        });
        Assert.AreEqual(
            expected,
            actual
        );
    }
    [TestMethod]
    public void Geomean(){
        for(var a=0;a<試行回数;a++) SetData.Geomean(p=>(double)p.ID3);
    }
    [TestMethod]
    public void Intersect(){
        var r=new Random(1);
        for(var a=0;a<試行回数;a++){
            var dn1=UnionExceptIntersectのEntity(r);
            var dn2=UnionExceptIntersectのEntity(r);
            var sn1=dn1.ToSet();
            var sn2=dn2.ToSet();
            var 正解=dn1.Intersect(dn2).ToSet();
            var 回答=sn1.Intersect(sn2);
            Assert.AreEqual(正解,回答);
        }
        Assert.AreEqual(new Set<Int>{
            1,2,3
        },new Set<Int>{
            1,2,3
        }.Intersect(new Set<Int>{
            1,2,3
        }));
        Assert.AreEqual(new Set<Int>{
            2,3
        },new Set<Int>{
            1,2,3
        }.Intersect(new Set<Int>{
            2,3,4
        }));
    }
    [TestMethod]
    public void Join(){
        var r=new Random(1);
        for(var a=0;a<試行回数;a++){
            var dn1=new Entity[r.Next(10)];
            var low1=0;
            for(var b=0;b<dn1.Length;b++){
                var value=r.Next(low1,low1+5);
                var ID1=b/10;
                var ID2=b%10;
                var ID3=b;
                dn1[b]=new Entity(ID1,ID2,ID3);
                low1=value+1;
            }
            var dn2=new Entity[r.Next(10)];
            var low2=0;
            for(var b=0;b<dn2.Length;b++){
                var value=r.Next(low2,low2+5);
                var ID1=b/10;
                var ID2=b%10;
                var ID3=b;
                dn2[b]=new Entity(ID1,ID2,ID3);
                low2=value+1;
            }
            var sn1=dn1.ToSet();
            var sn2=dn2.ToSet();
            dn1.Join(dn2,o=>o.PrimaryKey,i=>i.PrimaryKey,(o,i)=>new{
                o,i
            });
            dn1.Join(dn2,o=>o.PrimaryKey,i=>i.PrimaryKey,(o,i)=>new{
                o,i
            }).ToSet();
            {
                var 正解=dn1.Join(dn2,o=>o.PrimaryKey,i=>i.PrimaryKey,(o,i)=>new{
                    o,i
                }).ToSet();
                var 回答=sn1.Join(sn2,o=>o.PrimaryKey,i=>i.PrimaryKey,(o,i)=>new{
                    o,i
                });
                Assert.AreEqual(正解,回答);
            }
        }
    }
    [TestMethod]
    public void Max(){
        var r=new Random(1);
        for(var a=0;a<試行回数;a++){
            var dn1=new Entity[r.Next(1,10)];
            var low1=0;
            for(var b=0;b<dn1.Length;b++){
                var value=r.Next(low1,low1+5);
                var ID1=b/10;
                var ID2=b%10;
                var ID3=b;
                dn1[b]=new Entity(ID1,ID2,ID3);
                low1=value+1;
            }
            var sn1=dn1.ToSet();
            var 正解=dn1.Max(p=>p.ID3);
            var 回答=sn1.Max(p=>p.ID3);
            Assert.AreEqual(正解,回答);
        }
        Assert.AreEqual(
            2,
            new Set<long>{
                0,
                1,
                2,
                3,
                4,
                5
            }.Max(p=>p/2)
        );
    }
    [TestMethod]
    public void Min(){
        var r=new Random(1);
        for(var a=0;a<試行回数;a++){
            var dn1=new Entity[r.Next(1,10)];
            var low1=0;
            for(var b=0;b<dn1.Length;b++){
                var value=r.Next(low1,low1+5);
                var ID1=b/10;
                var ID2=b%10;
                var ID3=b;
                dn1[b]=new Entity(ID1,ID2,ID3);
                low1=value+1;
            }
            var sn1=dn1.ToSet();
            var 正解=dn1.Min(p=>p.ID3);
            var 回答=sn1.Min(p=>p.ID3);
            Assert.AreEqual(正解,回答);
        }
        Assert.AreEqual(
            0,
            new Set<long>{
                17,
                0,
                1,
                2,
                3,
                4
            }.Min(p=>p/2)
        );
    }
    public class Entity1:Entity{
        public Entity1(decimal ID1,decimal ID2,decimal ID3)
            :base(ID1,ID2,ID3){
        }
    }
    [TestMethod]
    public void OfType(){
        var r=new Random(1);
        for(var a=0;a<試行回数;a++){
            var Length=r.Next(1,10);
            var ArrayClass=new Entity[Length];
            var ArrayStruct=new object[Length];
            var low1=0;
            for(var b=0;b<Length;b++){
                var value=r.Next(low1,low1+5);
                var ID1=b/10;
                var ID2=b%10;
                var ID3=b;
                if(r.Next(2)==0){
                    ArrayClass[b]=new Entity(ID1,ID2,ID3);
                    ArrayStruct[b]=b;
                } else{
                    ArrayClass[b]=new Entity1(ID1,ID2,ID3);
                    ArrayStruct[b]=(double)b;
                }
                low1=value+1;
            }
            {
                var SetClass=ArrayClass.ToSet();
                {
                    var 正解=ArrayClass.OfType<Entity>().ToSet();
                    var 回答=SetClass.OfType<Entity>();
                    Assert.AreEqual(正解,回答);
                }
                {
                    var 正解=ArrayClass.OfType<Entity1>().ToSet();
                    var 回答=SetClass.OfType<Entity1>();
                    Assert.AreEqual(正解,回答);
                }
            }
            {
                var SetStruct=ArrayStruct.ToSet();
                {
                    var 正解=ArrayStruct.OfType<Entity>().ToSet();
                    var 回答=SetStruct.OfType<Entity>();
                    Assert.AreEqual(正解,回答);
                }
                {
                    var 正解=ArrayStruct.OfType<Entity1>().ToSet();
                    var 回答=SetStruct.OfType<Entity1>();
                    Assert.AreEqual(正解,回答);
                }
            }
        }
        Assert.AreEqual(new Set<string>{
            "2"
        },new Set<object>{
            1,"2",3
        }.OfType<string>());
    }
    [TestMethod]
    public void Select0(){
        var r=new Random(1);
        for(var a=0;a<試行回数;a++){
            var EnumerateData=new Entity[r.Next(100)];
            var low1=0;
            for(var b=0;b<EnumerateData.Length;b++){
                var value=r.Next(low1,low1+5);
                var ID1=b/10;
                var ID2=b%10;
                var ID3=b;
                EnumerateData[b]=new Entity(ID1,ID2,ID3);
                low1=value+1;
            }
            var SetData=EnumerateData.ToSet();
            {
                var 正解=EnumerateData.Select(p=>new{
                    p.ID1,p.ID2,p.ID3
                }).ToSet();
                var 回答=SetData.Select(p=>new{
                    p.ID1,p.ID2,p.ID3
                });
                Assert.AreEqual(正解,回答);
            }
            {
                var 正解=EnumerateData.Select(p=>new Entity(p.ID1,p.ID2,p.ID3)).ToSet();
                var 回答=SetData.Select(p=>new Entity(p.ID1,p.ID2,p.ID3));
                Assert.AreEqual(正解,回答);
            }
        }
    }
    [TestMethod]
    public void Select1(){
        var r=new Random(1);
        var data1=UnionExceptIntersectのEntity(r);
        var data2=UnionExceptIntersectのEntity(r);
        var enu1=data1.AsEnumerable();
        var enu2=data2.AsEnumerable();
        enu1.Select(p=>enu2.Select(q=>new{
            q,p
        }));
    }
    [TestMethod]
    [SuppressMessage("ReSharper","PossibleMultipleEnumeration")]
    public void SelectMany(){
        var r=new Random(1);
        for(var a=0;a<試行回数;a++){
            var data1=UnionExceptIntersectのEntity(r);
            var data2=UnionExceptIntersectのEntity(r);
            var set1=data1.ToSet();
            var set2=data2.ToSet();
            var enu1=data1.AsEnumerable();
            var enu2=data2.AsEnumerable();
            enu1.SelectMany(p=>enu2.Select(q=>new{
                q,p
            }),(entity,collection)=>new{
                entity,collection
            });
            data1.SelectMany(p=>data2,(entity,collection)=>entity);
            enu1.SelectMany(p=>enu2,(entity,collection)=>collection);
            data1.SelectMany(p=>data2,(entity,collection)=>collection);
            {
                var 正解=data1.SelectMany(p=>data2).Distinct().ToSet();
                var 回答=set1.SelectMany(p=>set2);
                Assert.AreEqual(正解,回答);
            }
            {
                var 正解=data1.SelectMany(p=>data2,(entity,collection)=>new{
                    entity,collection
                }).Distinct().ToSet();
                var 回答=set1.SelectMany(p=>set2,(entity,collection)=>new{
                    entity,collection
                });
                Assert.AreEqual(正解,回答);
            }
        }
    }
    [TestMethod]
    public void Sum(){
        for(var a=0;a<試行回数;a++){
            {
                var 正解=SetData.Select(p=>p.ID1/2).Sum();
                var 回答=EnumerateData.Select(p=>p.ID1/2).Sum();
                Assert.AreEqual(正解,回答);
            }
            {
                var 正解=EnumerateData.Select(p=>p.ID1/2).Sum();
                var 回答=SetData.Select(p=>p.ID1/2).Sum();
                Assert.AreEqual(正解,回答);
            }
            {
                var 正解=SetData.Sum(p=>p.ID1);
                var 回答=EnumerateData.Sum(p=>p.ID1);
                Assert.AreEqual(正解,回答);
            }
            {
                var 正解=EnumerateData.Sum(p=>p.ID1);
                var 回答=SetData.Sum(p=>p.ID1);
                Assert.AreEqual(正解,回答);
            }
            {
                var 正解=SetData.Select(p=>p.ID1).Sum();
                var 回答=EnumerateData.Select(p=>p.ID1).Sum();
                Assert.AreEqual(正解,回答);
            }
            {
                var 正解=EnumerateData.Select(p=>p.ID1).Sum();
                var 回答=SetData.Select(p=>p.ID1).Sum();
                Assert.AreEqual(正解,回答);
            }
        }
    }
    [TestMethod]
    public void Single(){
        var dn1=new[]{
            new Entity(1,2,3)
        };
        var sn1=dn1.ToSet();
        {
            var 正解=dn1.Single();
            var 回答=sn1.Single();
            Assert.AreEqual(正解,回答);
        }
        Assert.AreEqual(
            2,
            new Set<long>{
                2
            }.Single()
        );
    }
    //[TestMethod]
    //public void Single1(){
    //    var dn1=new Entity[0];
    //    var sn1=dn1.ToSet();
    //    変数Cache.AssertExecute(()=>sn1.Select(p=>sn1.Single()));
    //}
    [TestMethod]
    public void SingleOrDefault(){
        var defaultValue=new Entity(9,9,9);
        var Value=new Entity(1,2,3);
        var dn0=Array.Empty<Entity>();
        var dn1=new[]{
            Value
        };
        var sn0=dn0.ToSet();
        var sn1=dn1.ToSet();
        {
            var 回答0defaultValue=sn0.SingleOrDefault(defaultValue);
            Assert.AreEqual(defaultValue,回答0defaultValue);
            var 回答1defaultValue=sn1.SingleOrDefault(defaultValue);
            Assert.AreEqual(Value,回答1defaultValue);
            var 正解0=dn0.SingleOrDefault();
            var 回答0=sn0.SingleOrDefault();
            Assert.AreEqual(正解0,回答0);
            var 正解1=dn1.SingleOrDefault();
            var 回答1=sn1.SingleOrDefault();
            Assert.AreEqual(正解1,回答1);
        }
    }
    [TestMethod]
    public void WhereAny()=>SetData.Where(p=>p.ID3==9).Any();
    [TestMethod]
    public void WhereContains()=>SetData.Where(p=>p.ID3==9).Contains(new Entity(0,0,9));
    [TestMethod]
    public void キー検索(){
        var expected=new Entity(1,2,2);
        var not_expected=new Entity(2,1,3);
        var key=new PrimaryKeys.Entity(1,2);
        var data=new Set<Entity,PrimaryKeys.Entity,Container>(null){
            new(1,1,1),expected,not_expected,new(2,2,4)
        };
        var r=data[key];
        Assert.AreEqual(expected,r);
        Assert.AreNotEqual(not_expected,r);
    }

    [TestMethod]
    public void AssertValidate(){
        for(var a=1;a<10000;a*=2){
            var d=new Set<int>();
            for(var b=0;b<=a;b++) d.Add(b);
            d.Assert();
        }
    }

    [TestMethod]
    public void Enumerator(){
        {
            var d=new Set<uint>{
                0x3FFFFFFF,0xBFFFFFFF
            };
            // ReSharper disable once UnusedVariable
            foreach(var a in d){

            }
            d.Assert();
        }
        {
            var d=new Set<uint>{
                0,0xFFFFFFFE
            };
            // ReSharper disable once UnusedVariable
            foreach(var a in d){

            }
            d.Assert();
        }
        {
            var d=new Set<int>{
                2,3
            };
            d.Assert();
        }
        var expected=new Set<int>();
        for(var b=0;b<1000;b++){
            expected.Clear();
            for(var a=0;a<b;a++) expected.Add(a);
            var c=0;
            foreach(var _ in expected){
                c++;
            }
            Assert.AreEqual(expected.Count,c);
        }
        expected.Assert();
    }
    [TestMethod]
    public void SymmetricExcept(){
        Assert.AreEqual(new Set<int>(),new Set<int>{
            0,1,2,3
        }.SymmetricExcept(new Set<int>{
            0,1,2,3
        }));
        Assert.AreEqual(new Set<int>{
            0,1,4,5
        },new Set<int>{
            0,1,2,3
        }.SymmetricExcept(new Set<int>{
            2,3,4,5
        }));
        Assert.AreEqual(new Set<int>{
            0,
            1,
            2,
            3,
            4,
            5,
            6,
            7
        },new Set<int>{
            0,1,2,3
        }.SymmetricExcept(new Set<int>{
            4,5,6,7
        }));
    }
    [TestMethod]
    public void Insert(){
        var d=new Set<Int>{
            1,2
        };
        var i=new Set<Int>{
            3,4
        };
        Assert.AreEqual(2L,d.Insert(i));
    }
    [TestMethod]
    public void Update(){
        var d=new Set<Int>{
            1,2,3
        };
        Assert.AreEqual(d.Update(p=>p==2,p=>p+2),new Set<Int>{
            1,3,4
        });
        Assert.AreEqual(d.Update(p=>p==2,p=>p+1),new Set<Int>{
            1,3
        });
    }
    [TestMethod]
    public void Union(){
        Assert.AreEqual(new Set<Int>{
            1,2,3
        },new Set<Int>{
            1,2,3
        }.Union(new Set<Int>{
            1,2,3
        }));
        Assert.AreEqual(new Set<Int>{
            1,2,3,4
        },new Set<Int>{
            1,2,3
        }.Union(new Set<Int>{
            2,3,4
        }));
        Assert.AreEqual(new Set<Int>{
            1,
            2,
            3,
            4,
            5,
            6
        },new Set<Int>{
            1,2,3
        }.Union(new Set<Int>{
            4,5,6
        }));
        var expected=new Set<SetGroupingSet<int,double>>();
        for(var a=1;a<=6;a++){
            var G=new SetGroupingSet<int,double>();
            AddKeyValue(G,a,a);
            expected.Add(G);
        }
        var actual0=new Set<SetGroupingSet<int,double>>();
        for(var a=1;a<=3;a++){
            var G=new SetGroupingSet<int,double>();
            AddKeyValue(G,a,a);
            actual0.Add(G);
        }
        var actual1=new Set<SetGroupingSet<int,double>>();
        for(var a=4;a<=6;a++){
            var G=new SetGroupingSet<int,double>();
            AddKeyValue(G,a,a);
            actual1.Add(G);
        }
        var actual=actual0.Union(actual1);
        Assert.AreEqual(expected,actual);
    }
    [TestMethod]
    public void DUnion(){
        Assert.AreEqual(new Set<Int>{
            1,
            2,
            3,
            4,
            5,
            6
        },new Set<Int>{
            1,2,3
        }.DUnion(new Set<Int>{
            4,5,6
        }));
    }
    [TestMethod]
    public void Join0(){
        var expected=new Set<Int>{
            8,10,12
        };
        var actual=new Set<Int>{
            1,
            2,
            3,
            4,
            5,
            6
        }.Join(new Set<Int>{
            4,5,6
        },o=>o,i=>i,(o,i)=>o+i);
        Assert.AreEqual(expected,actual);
    }
    [TestMethod]
    public void SelectMany1(){
        Assert.AreEqual(
            new Set<Int>{
                11,
                12,
                13,
                16,
                17,
                18
            },
            new Set<Int>{
                1,6
            }.SelectMany(p=>
                new Set<Int>{
                    10+p,11+p,12+p
                }
            )
        );
    }
    [TestMethod]
    public void SelectMany2(){
        Assert.AreEqual(
            new Set<Int>{
                11,
                12,
                13,
                16,
                17,
                18
            },
            new Set<Int>{
                1,6
            }.SelectMany(
                p=>new Set<Int>{
                    10+p,11+p,12+p
                },
                (p,q)=>q
            )
        );
    }
    [TestMethod]
    public void StdevDouble(){
        new Set<double>{
            1,
            2,
            4,
            8,
            16
        }.Stdev(p=>p);
    }
    [TestMethod]
    public void AverageDecimal(){
        //解答：算術平均値は (2+3+4+7+9) / 5=5 
        Assert.AreEqual(5m,new Set<decimal>{
            2,
            3,
            4,
            7,
            9
        }.Average(p=>p));
    }
    [TestMethod]
    public void AverageDouble(){
        //解答：算術平均値は (2+3+4+7+9) / 5=5 
        Assert.AreEqual(5d,new Set<double>{
            2,
            3,
            4,
            7,
            9
        }.Average(p=>p));
    }

    private static void AddKeyValue<TKey,TValue>(SetGroupingSet<TKey,TValue> s,TKey Key,TValue Value){
        dynamic d=new NonPublicAccessor(s);
        d.AddKeyValue(Key,Value);
        //            s.AddKeyValue(new KeyValue<TKey,TValue>(Key,Value));
    }
    [TestMethod]
    public void GroupBy1(){
        {
            var a=new SetGroupingSet<long,long>();
            AddKeyValue(a,0,1);
            var b=new Set<long>{
                1
            }.GroupBy(p=>p/2);
            Assert.AreEqual(
                a,
                b
            );
        }
        {
            var a=new SetGroupingSet<long,long>();
            AddKeyValue(a,0,1);
            var b=new Set<long>{
                1
            }.GroupBy(p=>p/2);
            Assert.AreEqual(
                a,
                b
            );
        }
        {
            var a=new SetGroupingSet<long,long>();
            AddKeyValue(a,0,0);
            AddKeyValue(a,0,1);
            var b=new Set<long>{
                0,1
            }.GroupBy(p=>p/2);
            Assert.AreEqual(
                a,
                b
            );
        }
        {
            var a=new SetGroupingSet<long,long>();
            AddKeyValue(a,1,2);
            AddKeyValue(a,1,3);
            var b=new Set<long>{
                2,3
            }.GroupBy(p=>p/2);
            Assert.AreEqual(
                a,
                b
            );
        }
        {
            var a=new SetGroupingSet<long,long>();
            AddKeyValue(a,0,0);
            AddKeyValue(a,0,1);
            AddKeyValue(a,1,2);
            AddKeyValue(a,1,3);
            var b=new Set<long>{
                0,1,2,3
            }.GroupBy(p=>p/2);
            Assert.AreEqual(
                a,
                b
            );
        }
    }
    [TestMethod]
    public void GroupBy1_Sum(){
        var enu=new[]{
            1,2,3,4,5,6,7,8
        };
        var set=new Set<int>(enu);
        var expected=enu.GroupBy(p=>p/2).Select(p=>p.Sum());
        var actual=set.GroupBy(p=>p/2).Select(p=>p.Sum());
        var Comparer=new EnumerableSetEqualityComparer();
        Assert.IsTrue(Comparer.Equals(expected,actual));
    }
    [TestMethod]
    public void GroupBy2(){
        var a=new SetGroupingSet<long,long>();
        AddKeyValue(a,0,10);
        AddKeyValue(a,0,11);
        AddKeyValue(a,1,12);
        AddKeyValue(a,1,13);
        var b=new Set<long>{
            0,1,2,3
        }.GroupBy(p=>p/2,p=>p+10);
        Assert.AreEqual(
            a,
            b
        );
    }
    private static Set<T> Set作成<T>(params T[] array){
        return new Set<T>(array);
    }
    [TestMethod]
    public void GeomeanDouble(){
        //正の数だけ
        //1,2,4,4
        //4/(1/1+1/2+1/4+1/4)=4/(4/4+2/4+1/4+1/4)=4/(8/4)=4/2=2
        Assert.AreEqual(
            4d,
            new[]{
                new{
                    Key=0,Value=1d
                },
                new{
                    Key=1,Value=1d
                },
                new{
                    Key=2,Value=1d
                },
                new{
                    Key=3,Value=256d
                }
            }.ToSet().Geomean(p=>p.Value)
        );
        //1,1,1,8
        //4/(1/1+1/1+1/1+1/8)=1.28
        Assert.AreEqual(
            2d,
            new[]{
                new{
                    Key=0,Value=1d
                },
                new{
                    Key=1,Value=1d
                },
                new{
                    Key=2,Value=1d
                },
                new{
                    Key=3,Value=16d
                }
            }.ToSet().Geomean(p=>p.Value)
        );
    }
    [TestMethod]
    public void HarmeanDecimal(){
        //正の数だけ
        //1,2,4
        //3/(1/1+1/2+1/4)=12/7
        Assert.AreEqual(12m/7m,new Set<decimal>{
            1,2,4
        }.Harmean(p=>p));
        //1,1,1,8
        //4/(1/1+1/1+1/1+1/8)=1.28
        Assert.AreEqual(
            1.28m,
            new[]{
                new{
                    Key=0,Value=1m
                },
                new{
                    Key=1,Value=1m
                },
                new{
                    Key=2,Value=1m
                },
                new{
                    Key=3,Value=8m
                }
            }.ToSet().Harmean(p=>p.Value)
        );
    }
    [TestMethod]
    public void HarmeanDouble(){
        //正の数だけ
        //1,2,4,4
        //4/(1/1+1/2+1/4+1/4)=4/(4/4+2/4+1/4+1/4)=4/(8/4)=4/2=2
        Assert.AreEqual(
            2d,
            new[]{
                new{
                    Key=0,Value=1d
                },
                new{
                    Key=1,Value=2d
                },
                new{
                    Key=2,Value=4d
                },
                new{
                    Key=3,Value=4d
                }
            }.ToSet().Harmean(p=>p.Value)
        );
        //1,1,1,8
        //4/(1/1+1/1+1/1+1/8)=1.28
        Assert.AreEqual(
            1.28d,
            new[]{
                new{
                    Key=0,Value=1d
                },
                new{
                    Key=1,Value=1d
                },
                new{
                    Key=2,Value=1d
                },
                new{
                    Key=3,Value=8d
                }
            }.ToSet().Harmean(p=>p.Value)
        );
    }
    [TestMethod]
    public void SingleOrDefault0(){
        Assert.AreEqual(
            default,
            new Set<long>().SingleOrDefault()
        );
    }
    [TestMethod]
    public void SingleOrDefault1(){
        Assert.AreEqual(
            long.MaxValue,
            new Set<long>().SingleOrDefault(long.MaxValue)
        );
        Assert.AreEqual(
            1L,
            new Set<long>{
                1
            }.SingleOrDefault(long.MaxValue)
        );
    }
    [TestMethod]
    public void SumInt32(){
        Assert.AreEqual(10,new Set<int>{
            1,2,3,4
        }.Sum(p=>p));
    }
    [TestMethod]
    public void SumInt64(){
        Assert.AreEqual(10L,new Set<long>{
            1,2,3,4
        }.Sum(p=>p));
    }
    [TestMethod]
    public void SumSingle(){
        Assert.AreEqual(10f,new Set<float>{
            1,2,3,4
        }.Sum(p=>p));
    }
    [TestMethod]
    public void SumDecimal(){
        Assert.AreEqual(10m,new Set<decimal>{
            1,2,3,4
        }.Sum(p=>p));
    }
    [TestMethod]
    public void SumDouble(){
        Assert.AreEqual(10d,new Set<double>{
            1,2,3,4
        }.Sum(p=>p));
    }
    [TestMethod]
    public void VarpDecimal(){
        Assert.AreEqual(1.25m,new Set<decimal>{
            1,2,3,4
        }.Varp(p=>p));
    }
    [TestMethod]
    public void VarpDouble(){
        Assert.AreEqual(1.25d,new Set<double>{
            1,2,3,4
        }.Varp(p=>p));
    }
    [TestMethod]
    public void VarDecimal(){
        Assert.AreEqual(1m,new Set<decimal>{
            1,2,3
        }.Var(p=>p));
    }
    [TestMethod]
    public void VarDouble(){
        Assert.AreEqual(1d,new Set<double>{
            1,2,3
        }.Var(p=>p));
    }
    [TestMethod]
    public void Select(){
        Assert.AreEqual(new Set<long>{
            1,2,3
        },new Set<long>{
            1,2,3
        }.Select(p=>p));
    }
    [TestMethod]
    public void Where(){
        Assert.AreEqual(new Set<long>(),new Set<long>{
            1,2,3
        }.Where(p=>p==999));
        Assert.AreEqual(new Set<long>{
            2
        },new Set<long>{
            1,2,3
        }.Where(p=>p%2==0));
    }
    [TestMethod]
    [ExpectedException(typeof(OneTupleException))]
    public void DUnion_UniqueTupleException0(){
        new Set<Int>{
            1,2,3
        }.DUnion(new Set<Int>{
            1,2,3
        });
    }
    [TestMethod]
    [ExpectedException(typeof(OneTupleException))]
    public void DUnion_UniqueTupleException1(){
        new Set<Int>{
            1,2,3
        }.DUnion(new Set<Int>{
            2,3,4
        });
    }
    [TestMethod]
    [ExpectedException(typeof(ZeroTupleException))]
    public void StdevDouble_InvalidOperationException(){
        new Set<double>().Stdev(p=>p);
    }
    [TestMethod]
    [ExpectedException(typeof(ZeroTupleException))]
    public void AverageDecimal_InvalidOperationException(){
        new Set<decimal>().Average(p=>p);
    }
    [TestMethod]
    [ExpectedException(typeof(ZeroTupleException))]
    public void AverageDouble_InvalidOperationException(){
        new Set<double>().Average(p=>p);
    }
    [TestMethod]
    [ExpectedException(typeof(ZeroTupleException))]
    public void GeomeanDouble_InvalidOperationException(){
        new Set<double>().Geomean(p=>p);
    }
    [TestMethod]
    [ExpectedException(typeof(ZeroTupleException))]
    public void HarmeanDecimal_InvalidOperationException(){
        new Set<decimal>().Harmean(p=>p);
    }
    [TestMethod]
    [ExpectedException(typeof(ZeroTupleException))]
    public void HarmeanDouble_InvalidOperationException(){
        new Set<double>().Harmean(p=>p);
    }
    [TestMethod]
    [ExpectedException(typeof(ZeroTupleException))]
    public void Max_InvalidOperationException(){
        new Set<long>().Max(p=>p/2);
    }
    [TestMethod]
    [ExpectedException(typeof(ZeroTupleException))]
    public void Min_InvalidOperationException(){
        new Set<long>().Min(p=>p/2);
    }
    [TestMethod]
    [ExpectedException(typeof(ZeroTupleException))]
    public void Single_0行InvalidOperationException(){
        new Set<long>().Single();
    }
    [TestMethod]
    [ExpectedException(typeof(ManyTupleException))]
    public void Single_2行InvalidOperationException(){
        new Set<long>{
            0,1
        }.Single();
    }
    [TestMethod]
    [ExpectedException(typeof(ManyTupleException))]
    public void SingleOrDefault0_InvalidOperationException(){
        new Set<long>{
            0,
            1,
            2,
            3,
            4,
            5
        }.SingleOrDefault();
    }
    [TestMethod]
    [ExpectedException(typeof(ManyTupleException))]
    public void SingleOrDefault1_InvalidOperationException(){
        new Set<long>{
            0,
            1,
            2,
            3,
            4,
            5
        }.SingleOrDefault(long.MaxValue);
    }
}