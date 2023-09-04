using System;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using LinqDB.Remote.Clients;
using LinqDB.Serializers;
using LinqDB.Sets;
using MessagePack;
using MessagePack.Formatters;
//using LinqDB.Serializers.Formatters;
//using LinqDB.Serializers.MessagePack;
//using MessagePack.Resolvers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Utf8Json;
// ReSharper disable PossibleNullReferenceException
//具体的なAnonymousTypeをそのままSerialize,DeserializeするときはAnonymousExpressionResolverを通過しない。AnonymousTypeを返す。
//具体的なAnonymousTypeをObjectでSerialize,DeserializeするときはAnonymousExpressionResolverを通過しない。Dictionaryを返す。
namespace CoverageCS.Serializers;
[TestClass]
public class Test_色んなデータ型:ATest_シリアライズ{
    private enum enum_sbyte:sbyte{a,b}
    [TestMethod]public void Enum0()=>共通object2(enum_sbyte.a);
    private enum enum_ushort:ushort{a,b}
    [TestMethod]public void Enum1()=>共通object2(enum_ushort.a);
    private enum enum_long:long{a,b}
    [TestMethod]public void Enum2()=>共通object2(enum_long.a);
    [TestMethod]
    public void Anonymous000(){
        共通object2(new{
            a=1,
            b=2.0,
            c=3m,
            d=4f,
            e="e"
        });
    }
    [TestMethod]
    public void Anonymous001(){
        共通object2(new{a=1});
    }
    [TestMethod]
    public void Anonymous002(){
        共通object((object)new{a=(object)1});
    }
    [TestMethod]
    public void Anonymous003(){
        共通object((object)new{a=(object)new{aa=11}});
    }
    [TestMethod]
    public void Anonymous004(){
        共通object2(new{a=(object)new{aa=1}});
    }
    [TestMethod]
    public void Anonymous022(){
        共通object2(new{x=new{a=111}});
    }
    [TestMethod]
    public void Anonymous023(){
        共通object2(
            new{
                x=new{
                    a=111,
                    b=222.0,
                    c=333m,
                    d=444f,
                    e="eee"
                }
            }
        );
    }
    [TestMethod]
    public void Anonymous030(){
        共通object2(
            Tuple.Create(1)
        );
    }
    [TestMethod]
    public void Anonymous031(){
        共通object2(
            Tuple.Create(
                new{
                    a=111,
                    b=222.0,
                    c=333m,
                    d=444f,
                    e="eee"
                }
            )
        );
    }
    [TestMethod]
    public void Anonymous040(){
        共通object2(new{
            a=new{aa=1},
            b=new{aa=1}
        });
    }
    [TestMethod]
    public void Anonymous041(){
        共通object2(new{
            a=new{aa=1},
            b=(object)new{aa=1}
        });
    }
    [TestMethod]
    public void Anonymous05(){
        共通object2(
            Tuple.Create(
                new{
                    a=1111
                }
            )
        );
    }
    [TestMethod]
    public void Anonymous06(){
        共通object2(
            new{
                a=(object)new{
                    a=1111
                }
            }
        );
    }
    [TestMethod]
    public void Anonymous07(){
        共通object(
            new[]{
                (object)new{
                    a=1111
                },
                (object)new{
                    a=2222
                }
            }
        );
    }
    [TestMethod]
    public void Anonymous08(){
        共通object(
            new[]{
                new{
                    a=1111
                },
                new{
                    a=2222
                }
            }
        );
    }
    [TestMethod]
    public void Anonymous09(){
        共通object2(
            new[]{
                new{
                    a=1111
                },
                new{
                    a=2222
                }
            }.ToList()
        );
    }
    [TestMethod]
    public void Anonymous10(){
        共通object2<object>(
            (object)new{
                a=1111
            }
        );
    }
    [TestMethod]public void Anonymous11(){
        共通object2(new{
            a=(object)new{aa=1}
        });
    }
    [TestMethod]
    public void Decimal(){
        共通object2(123m);
    }
    private static void ジェネリック0<T>()where T:new()=>共通Expression(Expression.Constant(new T()));
    private static void ジェネリック1<T>()where T:new()=>共通object2(Tuple.Create(new T()));
    private static void ジェネリック2<T>()where T:new()=>共通object2(new T());
    [TestMethod]public void classキーあり0()=>ジェネリック0<classキーあり>();
    [TestMethod]public void classキーあり1()=>ジェネリック1<classキーあり>();
    [TestMethod]public void classキーあり2()=>ジェネリック2<classキーあり>();
    [TestMethod]public void sealed_classキーあり0()=>ジェネリック0<sealed_classキーあり>();
    [TestMethod]public void sealed_classキーあり1()=>ジェネリック1<sealed_classキーあり>();
    [TestMethod]public void sealed_classキーあり2()=>ジェネリック2<sealed_classキーあり>();
    [TestMethod]
    public void ValueTuple(){
        共通object2((a:11,b:"bb",c:33m));
    }
    [TestMethod]
    public void Type_string(){
        共通object2(typeof(string));
    }
    [TestMethod]
    public void Type_Func(){
        共通object2(typeof(Func<int>));
    }
    [TestMethod]
    public void Type_カスタムデリゲート(){
        共通object2(typeof(Func<Func<int>>));
    }
    [TestMethod]
    public void Type(){
        共通object2(typeof(string));
    }
    [TestMethod]
    public void MethodInfo(){
        共通object(typeof(string).GetMethods());
    }
    [TestMethod]
    public void MemberInfo(){
        共通object(typeof(string).GetMembers(BindingFlags.Instance|BindingFlags.Static|BindingFlags.Public|BindingFlags.NonPublic));
    }
    class Enumerable1<T>:IEnumerable<T>,ICollection<T>, IEquatable<Enumerable1<T>>{
        private readonly LinkedList<T> data=new();
        //public Enumerable1(IEnumerable<T> input){
        //    foreach(var a in input) this.data.AddLast(a);
        //}
        //public Enumerable1(IEnumerable<T> input){
        //    foreach(var a in input) this.data.AddLast(a);
        //}
        public IEnumerator<T> GetEnumerator(){
            foreach(var a in this.data) yield return a;
        }
        IEnumerator IEnumerable.GetEnumerator(){
            foreach(var a in this.data) yield return a;
        }
        public bool Equals(Enumerable1<T>? other){
            if(ReferenceEquals(null,other)) return false;
            if(ReferenceEquals(this,other)) return true;
            return this.data.SequenceEqual(other.data);
        }
        public override bool Equals(object? obj){
            if(ReferenceEquals(null,obj)) return false;
            if(ReferenceEquals(this,obj)) return true;
            if(obj.GetType()!=this.GetType()) return false;
            return this.Equals((Enumerable1<T>)obj);
        }
        public override int GetHashCode(){
            return this.data.GetHashCode();
        }
        public static bool operator==(Enumerable1<T>? left,Enumerable1<T>? right){
            return Equals(left,right);
        }
        public static bool operator!=(Enumerable1<T>? left,Enumerable1<T>? right){
            return!Equals(left,right);
        }
        public void Add(T item){
            this.data.AddLast(item);
        }
        public void Clear(){
            this.data.Clear();
        }
        public bool Contains(T item){
            return this.data.Contains(item);
        }
        public void CopyTo(T[] array,int arrayIndex){
            this.data.CopyTo(array,arrayIndex);
        }
        public bool Remove(T item){
            return this.data.Remove(item);
        }
        public int Count=>this.data.Count;
        public bool IsReadOnly=>false;
    }
    [TestMethod]
    public void IEnumerable0(){
        var s=new LinkedList<int>(new[]{1,2,3});
        共通object2(s);
    }
    [TestMethod]
    public void IEnumerable1(){
        var s=new Enumerable1<int>{1,2,3};
        共通object2(s);
    }
    [TestMethod]
    public void Set1(){
        var s=new Set<int>{1,2,3};
        共通object2(s);
    }
    [MessagePackObject]public class MMM{
        public int b=1;
    }
    [MessagePackObject]public class MessagePackでシリアライズしたい2:MMM{
        public string aa;
        public MessagePackでシリアライズしたい2(string aa){this.aa=aa;}
    }
    [MessagePackObject(true)]public class MessagePackでシリアライズしたい{
        [IgnoreMember]
        private string a="A";
        public object obj;
        public MessagePackでシリアライズしたい2 aa;
        public MessagePackでシリアライズしたい(){
        }
        public MessagePackでシリアライズしたい(string a,string aa){
            this.a=a;
            this.aa=new(aa);
        }
    }
    [MessagePackObject(true)]
    public class MyClass
    {
        public string FirstName= "Hoge";
        public string LastName = "Piyo";
        // シリアル化させたくない場合には、[IgnoreMember]を付ける。
        [IgnoreMember]
        public string FullName { get { return FirstName + LastName; } }
    }
    [TestMethod]
    public void MessagePack標準シリアライズ(){
        {
            var s=new MessagePackでシリアライズしたい("#a","#aa");
            s.aa.b=11;
            s.obj="obj";
            var T0=JsonSerializer.Deserialize<MessagePackでシリアライズしたい>(JsonSerializer.Serialize(s));
            var T1=JsonSerializer.Deserialize<object>(JsonSerializer.Serialize<object>(s));
            var Options=MessagePackSerializerOptions.Standard.WithResolver(
                MessagePack.Resolvers.CompositeResolver.Create(
                    new IFormatterResolver[]{
                        MessagePack.Resolvers.DynamicObjectResolverAllowPrivate.Instance
                    }
                )
            );
            var M0=MessagePackSerializer.Deserialize<MessagePackでシリアライズしたい>(MessagePackSerializer.Serialize(s));
            var M1=MessagePackSerializer.Deserialize<MessagePackでシリアライズしたい>(MessagePackSerializer.Serialize(s,Options));
        }
    }
}
