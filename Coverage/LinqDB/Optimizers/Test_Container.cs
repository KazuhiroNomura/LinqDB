using System.Globalization;
using System.Text;
using LinqDB.CRC;
using LinqDB.Databases;
using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Optimizers{
    namespace テスト{
        [Serializable]
        public class Container:Container<Container>{
            public Schemas.dbo dbo{get;private set;}

            public override Container Transaction(){
                var Container=new Container(this);
                this.Copy(Container);
                return Container;
            }

            public Container(){
                this.Init();
            }

            public Container(Container? Parent):base(Parent){
            }

            public Container(Stream logStream):base(logStream){
            }

            private void Init(){
                this.dbo=new Schemas.dbo(this);
            }
        }
        namespace Schemas{
            [Serializable]
            public class dbo:Schema{
                //public Set<Int32> Int32 { get; private set; }
                private Set<Tables.dbo.Entity1,PrimaryKeys.dbo.Entity1,Container> _Entity1;
                public Set<Tables.dbo.Entity1,PrimaryKeys.dbo.Entity1,Container> Entity1=>this._Entity1;
                private Set<Tables.dbo.Entity1,PrimaryKeys.dbo.Entity1,Container> _Entity2;
                public Set<Tables.dbo.Entity1,PrimaryKeys.dbo.Entity1,Container> Entity2=>this._Entity2;
                public Struct比較対象にEquals struct比較対象にEquals;
                public Struct比較対象にIEquatableEquals struct比較対象にIEquatableEquals;
                public object Objectstruct比較対象にEquals=new Struct比較対象にEquals();
                public object Objectstruct比較対象にIEquatableEquals=new Struct比較対象にIEquatableEquals();
                private readonly Container Container;

                internal dbo(Container Container){
                    this.Container=Container;
                    this._Entity1=new Set<Tables.dbo.Entity1,PrimaryKeys.dbo.Entity1,Container>(this.Container);
                    this._Entity2=new Set<Tables.dbo.Entity1,PrimaryKeys.dbo.Entity1,Container>(this.Container);
                }
            }
        }
        namespace PrimaryKeys{
            namespace dbo{
                [Serializable]
                public struct Entity1:IEquatable<Entity1>{
                    public decimal ID1{get;}
                    public decimal ID2{get;}

                    public Entity1(decimal ID1,decimal ID2){
                        this.ID1=ID1;
                        this.ID2=ID2;
                    }

                    public bool Equals(Entity1 other){
                        // ReSharper disable once PossibleNullReferenceException
                        if(!this.ID1.Equals(other.ID1))
                            return false;
                        // ReSharper disable once ConvertIfStatementToReturnStatement
                        if(!this.ID2.Equals(other.ID2))
                            return false;
                        return true;
                    }

                    public override bool Equals(object other){
                        return this.Equals((Entity1)other);
                    }

                    public override int GetHashCode(){
                        var CRC=new CRC32();
                        CRC.Input(this.ID1);
                        CRC.Input(this.ID2);
                        return CRC.GetHashCode();
                    }

                    public static bool operator==(Entity1 x,Entity1 y)=>x.Equals(y);
                    public static bool operator!=(Entity1 x,Entity1 y)=>!x.Equals(y);

                    public override string ToString()
                        =>"ID1="+this.ID1.ToString(CultureInfo.CurrentCulture)+",ID2="+
                          this.ID2.ToString(CultureInfo.CurrentCulture);
                }
            }
        }
        namespace Tables{
            namespace dbo{
                [Serializable]
                public sealed class Entity1:Entity<PrimaryKeys.dbo.Entity1,Container>,IEquatable<Entity1>,
                    IWriteRead<Entity1>{
                    public decimal ID1=>this.ProtectedPrimaryKey.ID1;
                    public int C_ID{get;private set;}
                    public string? C_DATA{get;private set;}

                    public Entity1(int C_ID,string? C_DATA=default):base(new PrimaryKeys.dbo.Entity1(C_ID,C_ID)){
                        this.C_ID=C_ID;
                        this.C_DATA=C_DATA;
                    }

                    protected override void ToStringBuilder(StringBuilder sb){
                        sb.Append(",C_ID=");
                        sb.Append(this.C_ID);
                        sb.Append(",C_DATA=");
                        sb.Append(this.C_DATA);
                    }

                    private bool PrivateEquals(Entity1 other){
                        if(!this.C_ID.Equals(other.C_ID)) return false;
                        if(this.C_DATA!=other.C_DATA) return false;
                        return true;
                    }

                    public bool Equals(Entity1? other){
                        if(other is null) return false;
                        return this.PrivateEquals(other);
                    }

                    public override bool Equals(object? obj){
                        if(obj is Entity1 other)
                            return this.PrivateEquals(other);
                        return false;
                    }

                    public static bool operator==(Entity1 a,Entity1 b)=>a.Equals(b);
                    public static bool operator!=(Entity1 a,Entity1 b)=>!a.Equals(b);
                    public void BinaryWrite(BinaryWriter Writer){
                        throw new NotImplementedException();
                    }

                    public void BinaryRead(BinaryReader Reader,Func<Entity1> Create){
                        throw new NotImplementedException();
                    }
                }
            }
        }
    }

    [TestClass]
    public class Test_Container:ATest{
        [TestMethod]
        public void Containerキャプチャ(){
            using var Container=new テスト.Container();
            // ReSharper disable once AccessToDisposedClosure
            this.Execute2(()=>Container.dbo.Entity1.Select(p=>p.C_DATA));
        }
        [TestMethod]
        public void Container引数(){
            using var Container=new テスト.Container();
            this.Execute2(Container,c=>c.dbo.Entity1.Select(p=>p.C_DATA));
        }
    }
}
