using System;
using System.Diagnostics;
using System.IO;
using Linq=System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using LinqDB.Optimizers;
using Sets=LinqDB.Sets;
using LinqDB.Sets.Exceptions;
using Collections=System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
namespace LinqDB.Helpers;
using Generic= Collections.Generic;

/// <summary>
/// 設定と定数。
/// </summary>
public static class CommonLibrary {
    static CommonLibrary(){
    }
    internal const SerializeType DefaultSerializeType=SerializeType.MemoryPack;
    public static (AssemblyBuilder AssemblyBuilder,ModuleBuilder ModuleBuilder) DefineAssemblyModule(string Name) {
        var DynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(
            new AssemblyName { Name=Name },
            AssemblyBuilderAccess.RunAndCollect
        );
        var Module = DynamicAssembly.DefineDynamicModule(Name);
        return (DynamicAssembly, Module);
    }
    //#endif
    [Conditional("TRACE")]
    internal static void Trace_WriteLine(int 番号,string s) {
        Trace.WriteLine($"{番号,-2} {DateTimeOffset.Now} {s}");
    }
    internal const int ReceiveTimeout = -1;
    internal const int SendTimeout = -1;
    public static readonly Type[] Types_String = { typeof(string) };
    /// <summary>
    /// 固定長のByte[]Buffer専用のMemoryStream
    /// </summary>
    internal class MemoryStream:Stream {
        private readonly byte[] Buffer;
        /// <summary>
        /// 既定コンストラクタ。
        /// </summary>
        /// <param name="Buffer"></param>
        public MemoryStream(byte[] Buffer) {
            this.Buffer=Buffer;
        }
        public override bool CanRead => true;
        public override bool CanSeek => true;
        public override bool CanWrite => true;
        private long _Length;
        public override long Length => this._Length;
        public override long Position {
            get;
            set;
        }
        public override void Flush() {
        }
        public override int Read(byte[] buffer,int offset,int count) {
            //Debug.Assert(offset+count<=buffer.Length);
            //Debug.Assert(this.Position+count<=this.Buffer.Length);
            var Position = this.Position;
            if(Position+count<=this._Length) {
                Array.Copy(this.Buffer,Position,buffer,offset,count);
                this.Position=Position+count;
            } else {
                count=(int)(this._Length-Position);
                Array.Copy(this.Buffer,Position,buffer,offset,count);
                this.Position=this._Length;
            }
            return count;
        }
        public override int ReadByte() {
            var Position = this.Position;
            if(Position<this._Length) {
                this.Position=Position+1;
                return this.Buffer[Position];
            } else {
                return -1;
            }
        }
        public override long Seek(long offset,SeekOrigin origin) {
            switch(origin) {
                case SeekOrigin.Begin:
                    this.Position=offset;
                    break;
                case SeekOrigin.Current:
                    this.Position+=offset;
                    break;
                default: {
                    this.Position=this.Length-offset;
                    break;
                }
            }
            return this.Position;
        }
        internal long 必要なLength;
        public override void SetLength(long value) {
            if(value>this.Buffer.Length) {
                var Buffer_Length = this.Buffer.Length;
                throw new IndexOutOfRangeException($"value≦Buffer.Lengthが満たされる必要がある。実際は{value}≦{Buffer_Length}だった。");
            }
            this._Length=value;
            this.必要なLength=0;
        }
        public override void Write(byte[] buffer,int offset,int count) {
            Debug.Assert(offset+count<=buffer.Length);
            //Debug.Assert(this.Position+count<=this.Buffer.Length);
            this.必要なLength+=count;
            var Position = this.Position;
            Array.Copy(buffer,offset,this.Buffer,Position,count);
            Position+=count;
            if(this._Length<Position){
                this._Length=Position;
            }
            this.Position=Position;
        }
        public override void WriteByte(byte value) {
            this.必要なLength++;
            var Position = this.Position;
            this.Buffer[Position]=value;
            Position++;
            if(this._Length<Position) {
                this._Length=Position;
            }
            this.Position=Position;
        }
    }
    ///// <summary>
    ///// 送受信で一度に読み込めるバイト数
    ///// </summary>
    //public const Int32 メモリバイト数 = 1024*1024*202;
    /// <summary>
    /// データの完全性をチェックするハッシュ関数によって出力されたハッシュバイト長
    /// </summary>
    internal const int HashLength = 256/8;
    internal const string シーケンスに要素が含まれていません_NoElements = "Sequence contains no elements";//"シーケンスに要素が含まれていません";
    internal static InvalidOperationException シーケンスに要素が含まれていません(MethodBase Method) => new($"{Method}:{シーケンスに要素が含まれていません_NoElements}");
    internal const string シーケンスに複数の要素が含まれています_MoreThanOneElement = "シーケンスに複数の要素が含まれています";
    internal static ManyTupleException シーケンスに複数の要素が含まれています(MethodBase Method) => new($"{Method}:{シーケンスに複数の要素が含まれています_MoreThanOneElement}");
    /// <summary>
    /// パスワードのハッシュを返す。
    /// </summary>
    /// <param name="Password"></param>
    /// <returns></returns>
    public static string GetPasswordHash(string Password){
        var Provider=SHA256.Create();
        Provider.ComputeHash(Encoding.Unicode.GetBytes(Password));
        return BitConverter.ToString(Provider.Hash).Replace("-","");
    }
    /// <summary>
    /// Typeが専有するメモリバイト数
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    public static int Sizeof(Type Type){
        Debug.Assert(Type is not null);
        var D=new DynamicMethod(
            nameof(Sizeof),
            typeof(int),
            Type.EmptyTypes,
            typeof(CommonLibrary),
            true){
            InitLocals=false
        };
        var I=D.GetILGenerator();
        I.Emit(
            OpCodes.Sizeof,
            Type);
        I.Emit(OpCodes.Ret);
        return ((Func<int>)D.CreateDelegate(typeof(Func<int>)))();
    }
    private static readonly IPEndPoint LocalIPEndPoint=new(
        new IPAddress(
            new byte[]{
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1
            }),
        0
    );
    /// <summary>
    /// InterNetwork,InterNetworkV6のいずれか
    /// </summary>
    internal const AddressFamily SocketAddressFamily = AddressFamily.InterNetwork;
    internal static readonly IPAddress SocketIPAddress=IPAddress.Any;
    internal static readonly LingerOption LingerState = new(false,0);
    /// <summary>
    /// WCFで公開するURIの空きポートを求める。
    /// </summary>
    /// <returns></returns>
    public static int 空きポート番号(){
        using var ListenerSocket = new Socket(AddressFamily.InterNetworkV6,SocketType.Stream,ProtocolType.Tcp);
        ListenerSocket.Bind(LocalIPEndPoint);
        ListenerSocket.Listen(0);
        var Port = ((IPEndPoint)ListenerSocket.LocalEndPoint!).Port;
        ListenerSocket.Close();
        return Port;
    }
    /// <summary>
    /// Windowsログに出力する。
    /// </summary>
    /// <param name="e"></param>
    [Conditional("TRACE")]
    public static void トレース(string e){
        // var StackFrame=new StackFrame(1);
        //var method=StackFrame.GetMethod();
        //Assert(method.DeclaringType is not null,"method.DeclaringType  is not null");
        //Trace.WriteLine(e+" "+method.DeclaringType.Name+"."+method.Name);
        // Trace.WriteLine(e);
    }
    /// <summary>
    /// デリゲートを確実に別スレッドで実行する。
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="isBackground"></param>
    /// <param name="threadStart"></param>
    public static Thread スレッド実行(string Name,bool isBackground,ThreadStart threadStart){
        var Thread=new Thread(threadStart){
            Name=Name,
            IsBackground=isBackground
        };
        Thread.Start();
        return Thread;
    }
    //internal const String IEnumerable1_FullName = "System.Collections.Generic.IEnumerable`1";
    //internal const String IEnumerable_FullName = "System.Collections.IEnumerable";
    //internal const String IGrouping2_FullName = "System.Linq.IGrouping`2";
    //internal const String IOutputSet1_FullName = "LinqDB.Sets.IOutputSet`1";
    //internal const String IGroupingSet2_FullName = "LinqDB.Sets.IGroupingSet`2";
    internal static readonly string Generic_ICollection1_FullName = typeof(Generic.ICollection<>).FullName!;
    internal static readonly string Generic_IEnumerable1_FullName = typeof(Generic.IEnumerable<>).FullName!;
    internal static readonly string Collections_IEnumerable_FullName = typeof(Collections.IEnumerable).FullName!;
    internal static readonly string Linq_IGrouping2_FullName = typeof(IGrouping<,>).FullName!;
    internal static readonly string Linq_ILookup2_FullName = typeof(ILookup<,>).FullName!;
    internal static readonly string Sets_IEnumerable1_FullName = typeof(Sets.IEnumerable<>).FullName!;
    internal static readonly string Sets_IGrouping2_FullName = typeof(Sets.IGrouping<,>).FullName!;
    internal static readonly string Sets_ILookup2_FullName = typeof(Sets.ILookup<,>).FullName!;
    private static Type? PrivateGetInterface(this Type 検索されるInterface,Type 検索したいInterfaceDifinition){
        if(検索されるInterface.IsGenericType&&検索されるInterface.GetGenericTypeDefinition()==検索したいInterfaceDifinition)return 検索されるInterface;
        foreach(var Interface in 検索されるInterface.GetInterfaces()){
            if(Interface.IsGenericType&&Interface.GetGenericTypeDefinition()==検索したいInterfaceDifinition)return Interface;
        }
        return null;
    }
    public static bool IsInheritInterface(this Type 検索されるInterface,Type 検索したいInterfaceDifinition)=>検索されるInterface.PrivateGetInterface(検索したいInterfaceDifinition)is not null;
    public static Type GetInterface(this Type 検索されるInterface,Type 検索したいInterfaceDifinition){
        var Interface=検索されるInterface.PrivateGetInterface(検索したいInterfaceDifinition);
        if(Interface is not null) return Interface;
        throw new Generic.KeyNotFoundException($"{検索されるInterface}には{検索したいInterfaceDifinition}インターフェースは継承していない。");
    }
    //internal const int CS匿名型名_Length18= 18;
    //internal const string CS匿名型名="<>f__AnonymousType";
    //internal const int VB匿名型名_Length16 = 16;
    //internal const string VB匿名型名="VB$AnonymousType";
    //internal const int CSクロージャー_Length17 = 17;
    //internal const string CSクロージャー="<>c__DisplayClass";
    //internal const int VBクロージャー_Length11 = 11;
    //internal const string VBクロージャー="_Closure$__";
    internal static readonly string IEquatable_FullName = typeof(IEquatable<>).FullName!;
    internal static readonly string IEqualityComparer_FullName = typeof(Generic.IEqualityComparer<>).FullName!;
    private const string CS匿名型名="<>f__AnonymousType";
    private const string VB匿名型名="VB$AnonymousType";
    //private sealed class <>c{
    //	public static readonly <>c <>9 = new <>c();
    //	public static Func<int, int, int, bool> <>9__31_0;
    //	public static Func<int, int, int, bool> <>9__31_1;
    //	internal bool <DynamicInvoke>b__31_0(int a, int b, int c){
    //		return a == b && a == c;
    //	}
    //	internal bool <DynamicInvoke>b__31_1(int a, int b, int c){
    //		return a == b && a == c;
    //	}
    //}
    //private const string CSクロージャー="<>c__DisplayClass";//staticの場合"<>c"だけになる
    private const string CSクロージャー="<>c";
    private const string VBクロージャー="_Closure$__";
    /// <summary>
    /// Typeが匿名型であるか判定する。
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    public static bool IsAnonymous(this Type Type)=>
        Type.Namespace==null&&Type.IsSealed&&Type.IsDefined(typeof(CompilerGeneratedAttribute),false)&&(
            Type.Name.StartsWith(CS匿名型名,StringComparison.Ordinal)||
            Type.Name.StartsWith(VB匿名型名,StringComparison.Ordinal)
        );
    /// <summary>
    /// Typeがクロージャ型であるか判定する。
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    public static bool IsDisplay(this Type Type)=>
        Type.IsSealed && Type.IsDefined(typeof(CompilerGeneratedAttribute),false)&&(
            Type.Name.StartsWith(CSクロージャー,StringComparison.Ordinal) || 
            Type.Name.StartsWith(VBクロージャー, StringComparison.Ordinal)
        );
    //    CS匿名型名,
    //    StringComparison.Ordinal)==0||Type.Name.IndexOf(
    //    VB匿名型名,
    //    StringComparison.Ordinal)==0);
    /// <summary>
    /// TypeがValueTupleであるか判定する。
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    //public static Boolean IsValueTuple(this Type Type) => Type.IsValueType&&typeof(ITuple).IsAssignableFrom(Type);
    public static bool IsValueTuple(this Type Type) => Type.IsValueType&&typeof(ITuple).IsAssignableFrom(Type);
    /// <summary>
    /// Typeが匿名型かValueTupleであるか判定する。
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    public static bool IsAnonymousValueTuple(this Type Type) => Type.IsAnonymous()||Type.IsValueTuple();
    /// <summary>
    /// TypeがNullableであるか判定する。
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    public static bool IsNullable(this Type Type) => Type.IsGenericType&&Type.GetGenericTypeDefinition()==typeof(Nullable<>);
    public static bool IsWriteable(this Expression expression){
        switch(expression.NodeType){
            case ExpressionType.Index:
                var indexer=((IndexExpression)expression).Indexer;
                if(indexer is null) return true;
                if(indexer.CanWrite) return true;
                break;
            case ExpressionType.MemberAccess:
                var Member=((MemberExpression)expression).Member;
                if(Member is PropertyInfo Property){
                    if(Property.CanWrite)
                        return true;
                    break;
                } else{
                    Debug.Assert(Member is FieldInfo);
                    var field=(FieldInfo)Member;
                    if(field.IsInitOnly)return false;
                    if(field.IsLiteral) return false;
                    return true;
                }
        }
        return false;
    }
    public static bool GetIEnumerableTGenericArguments(this Type type,out Type[] GenericArguments) {
        var GetEnumerator = type.GetMethod(nameof(Generic.IEnumerable<int>.GetEnumerator));
        var GenericTypeDefinition = typeof(Generic.IEnumerable<>);
        foreach(var Interface in type.GetInterfaces())
            if(Interface.IsGenericType&&Interface.GetGenericTypeDefinition()==GenericTypeDefinition) {
                if(Array.Exists(type.GetInterfaceMap(Interface).TargetMethods,m => m==GetEnumerator)) {
                    GenericArguments=Interface.GetGenericArguments();
                    return true;
                }
                //var Interface_GetEnumerator=Interface.GetMethod(nameof(Generic.IEnumerable<int>.GetEnumerator));
                //if(Interface_GetEnumerator==GetEnumerator){
                //    GenericArguments=Interface.GetGenericArguments();
                //    return true;
                //}
            }
        GenericArguments=Array.Empty<Type>();
        return false;
    }
    public static bool GetIEnumerableT(this Type type,out Type GenericInterface){
        var GetEnumerator=type.GetMethod(nameof(Generic.IEnumerable<int>.GetEnumerator));
        var GenericTypeDefinition=typeof(Generic.IEnumerable<>);
        var Interfaces=new Generic.List<Type>();
        foreach(var Interface in type.GetInterfaces())
            if(Interface.IsGenericType&&Interface.GetGenericTypeDefinition()==GenericTypeDefinition){
                if(Array.Exists(type.GetInterfaceMap(Interface).TargetMethods,m=>m==GetEnumerator)){
                    GenericInterface=Interface;
                    return true;
                }
                Interfaces.Add(Interface);
                //var Interface_GetEnumerator=Interface.GetMethod(nameof(Generic.IEnumerable<int>.GetEnumerator));
                //if(Interface_GetEnumerator==GetEnumerator){
                //    GenericArguments=Interface.GetGenericArguments();
                //    return true;
                //}
            }
        foreach(var Interface in Interfaces){
            GenericInterface=Interface;
            return true;
        }
        GenericInterface=default!;
        return false;
    }
    public static bool GetGenericArguments(this Type type,Type GenericTypeDefinition,out Type[]GenericArguments){
        foreach(var Interface in type.GetInterfaces())
            if(Interface.IsGenericType&&Interface.GetGenericTypeDefinition()==GenericTypeDefinition){
                GenericArguments=Interface.GetGenericArguments();
                return true;
            }
        GenericArguments=Array.Empty<Type>();
        return false;
    }
    ///// <summary>
    ///// 小文字か
    ///// </summary>
    ///// <param name="s"></param>
    ///// <returns></returns>
    //public static bool IsLower(this string s) =>s.All(c=>!char.IsUpper(c));
    ///// <summary>
    ///// TypeがNullableまたは参照型であるか判定する。
    ///// </summary>
    ///// <param name="Type"></param>
    ///// <returns></returns>
    //public static bool IsNullable又は参照型(this Type Type) => !Type.IsValueType&&Type.IsGenericType&&Type.GetGenericTypeDefinition()==typeof(Nullable<>);
    public static Type Nullableまたは参照型(this Type Type,Type[]Types){
        if(Type.IsValueType&&!Type.IsNullable()){
            Types[0]=Type;
            return typeof(Nullable<>).MakeGenericType(Types);
        }else return Type;
    }
    public static string ExceptionのString(Exception ex) {
        var sb = new StringBuilder(ex.StackTrace);
        while(true) {
            sb.AppendLine(ex.Message);
            var ex0=ex.InnerException;
            if(ex0 is null) break;
            ex=ex0;
        }
        return sb.ToString();
    }
    //internal static NewExpression ValueTupleでNewする(Optimizer.作業配列 作業配列,IList<Expression> 旧Arguments) {
    //    var 旧Arguments_Count = 旧Arguments.Count;
    //    var Switch=旧Arguments_Count%7;
    //    var Offset=旧Arguments_Count-Switch;
    //    NewExpression New;
    //    if(旧Arguments_Count<8){
    //        New=Switch switch{
    //            1=>Expression.New(作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple1,旧Arguments[Offset+0].Type),作業配列.Expressions設定(旧Arguments[Offset+0])),
    //            2=>Expression.New(作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple2,旧Arguments[Offset+0].Type,旧Arguments[Offset+1].Type),作業配列.Expressions設定(旧Arguments[Offset+0],旧Arguments[Offset+1])),
    //            3=>Expression.New(作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple3,旧Arguments[Offset+0].Type,旧Arguments[Offset+1].Type,旧Arguments[Offset+2].Type),作業配列.Expressions設定(旧Arguments[Offset+0],旧Arguments[Offset+1],旧Arguments[Offset+2])),
    //            4=>Expression.New(作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple4,旧Arguments[Offset+0].Type,旧Arguments[Offset+1].Type,旧Arguments[Offset+2].Type,旧Arguments[Offset+3].Type),作業配列.Expressions設定(旧Arguments[Offset+0],旧Arguments[Offset+1],旧Arguments[Offset+2],旧Arguments[Offset+3])),
    //            5=>Expression.New(作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple5,旧Arguments[Offset+0].Type,旧Arguments[Offset+1].Type,旧Arguments[Offset+2].Type,旧Arguments[Offset+3].Type,旧Arguments[Offset+4].Type),作業配列.Expressions設定(旧Arguments[Offset+0],旧Arguments[Offset+1],旧Arguments[Offset+2],旧Arguments[Offset+3],旧Arguments[Offset+4])),
    //            6=>Expression.New(作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple6,旧Arguments[Offset+0].Type,旧Arguments[Offset+1].Type,旧Arguments[Offset+2].Type,旧Arguments[Offset+3].Type,旧Arguments[Offset+4].Type,旧Arguments[Offset+5].Type),作業配列.Expressions設定(旧Arguments[Offset+0],旧Arguments[Offset+1],旧Arguments[Offset+2],旧Arguments[Offset+3],旧Arguments[Offset+4],旧Arguments[Offset+5])),
    //            _=>Expression.New(作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple7,旧Arguments[Offset+0].Type,旧Arguments[Offset+1].Type,旧Arguments[Offset+2].Type,旧Arguments[Offset+3].Type,旧Arguments[Offset+4].Type,旧Arguments[Offset+5].Type,旧Arguments[Offset+6].Type),作業配列.Expressions設定(旧Arguments[Offset+0],旧Arguments[Offset+1],旧Arguments[Offset+2],旧Arguments[Offset+3],旧Arguments[Offset+4],旧Arguments[Offset+5],旧Arguments[Offset+6]))
    //        };
    //    } else{
    //        New=Switch switch{
    //            1=>Expression.New(作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple1,旧Arguments[Offset+0].Type),作業配列.Expressions設定(旧Arguments[Offset+0])),
    //            2=>Expression.New(作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple2,旧Arguments[Offset+0].Type,旧Arguments[Offset+1].Type),作業配列.Expressions設定(旧Arguments[Offset+0],旧Arguments[Offset+1])),
    //            3=>Expression.New(作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple3,旧Arguments[Offset+0].Type,旧Arguments[Offset+1].Type,旧Arguments[Offset+2].Type),作業配列.Expressions設定(旧Arguments[Offset+0],旧Arguments[Offset+1],旧Arguments[Offset+2])),
    //            4=>Expression.New(作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple4,旧Arguments[Offset+0].Type,旧Arguments[Offset+1].Type,旧Arguments[Offset+2].Type,旧Arguments[Offset+3].Type),作業配列.Expressions設定(旧Arguments[Offset+0],旧Arguments[Offset+1],旧Arguments[Offset+2],旧Arguments[Offset+3])),
    //            5=>Expression.New(作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple5,旧Arguments[Offset+0].Type,旧Arguments[Offset+1].Type,旧Arguments[Offset+2].Type,旧Arguments[Offset+3].Type,旧Arguments[Offset+4].Type),作業配列.Expressions設定(旧Arguments[Offset+0],旧Arguments[Offset+1],旧Arguments[Offset+2],旧Arguments[Offset+3],旧Arguments[Offset+4])),
    //            6=>Expression.New(作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple6,旧Arguments[Offset+0].Type,旧Arguments[Offset+1].Type,旧Arguments[Offset+2].Type,旧Arguments[Offset+3].Type,旧Arguments[Offset+4].Type,旧Arguments[Offset+5].Type),作業配列.Expressions設定(旧Arguments[Offset+0],旧Arguments[Offset+1],旧Arguments[Offset+2],旧Arguments[Offset+3],旧Arguments[Offset+4],旧Arguments[Offset+5])),
    //            _=>Expression.New(作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple7,旧Arguments[Offset-=7].Type,旧Arguments[Offset+1].Type,旧Arguments[Offset+2].Type,旧Arguments[Offset+3].Type,旧Arguments[Offset+4].Type,旧Arguments[Offset+5].Type,旧Arguments[Offset+6].Type),作業配列.Expressions設定(旧Arguments[Offset+0],旧Arguments[Offset+1],旧Arguments[Offset+2],旧Arguments[Offset+3],旧Arguments[Offset+4],旧Arguments[Offset+5],旧Arguments[Offset+6]))
    //        };
    //        var 新Arguments=作業配列.Expressions8;
    //        while((Offset-=7)>=0){
    //            Debug.Assert(Offset%7==0);
    //            var Argument0=新Arguments[0]=旧Arguments[Offset+0];
    //            var Argument1=新Arguments[1]=旧Arguments[Offset+1];
    //            var Argument2=新Arguments[2]=旧Arguments[Offset+2];
    //            var Argument3=新Arguments[3]=旧Arguments[Offset+3];
    //            var Argument4=新Arguments[4]=旧Arguments[Offset+4];
    //            var Argument5=新Arguments[5]=旧Arguments[Offset+5];
    //            var Argument6=新Arguments[6]=旧Arguments[Offset+6];
    //            新Arguments[7]=New;
    //            New=Expression.New(作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple7,
    //                    Argument0.Type,Argument1.Type,Argument2.Type,Argument3.Type,Argument4.Type,Argument5.Type,Argument6.Type
    //                    ),
    //                新Arguments);
    //        }
    //    }
    //    return New;
    //}
    internal static NewExpression ValueTupleでNewする(作業配列 作業配列,Generic.IList<Expression> 旧Arguments)=>
        旧Arguments.Count==0
            ?Expression.New(Reflection.ValueTuple.ValueTuple0)
            :ValueTupleでNewする(作業配列,旧Arguments,0);
    private static NewExpression ValueTupleでNewする(作業配列 作業配列,Generic.IList<Expression> 旧Arguments,int Offset) {
        var 残りType数 = 旧Arguments.Count-Offset;
        switch(残りType数) {
            case 1:
                return Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple1,
                        旧Arguments[Offset+0].Type
                    ),
                    作業配列.Expressions設定(
                        旧Arguments[Offset+0]
                    )
                );
            case 2:
                return Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple2,
                        旧Arguments[Offset+0].Type,
                        旧Arguments[Offset+1].Type
                    ),
                    作業配列.Expressions設定(
                        旧Arguments[Offset+0],
                        旧Arguments[Offset+1]
                    )
                );
            case 3:
                return Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple3,
                        旧Arguments[Offset+0].Type,
                        旧Arguments[Offset+1].Type,
                        旧Arguments[Offset+2].Type
                    ),
                    作業配列.Expressions設定(
                        旧Arguments[Offset+0],
                        旧Arguments[Offset+1],
                        旧Arguments[Offset+2]
                    )
                );
            case 4:
                return Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple4,
                        旧Arguments[Offset+0].Type,
                        旧Arguments[Offset+1].Type,
                        旧Arguments[Offset+2].Type,
                        旧Arguments[Offset+3].Type
                    ),
                    作業配列.Expressions設定(
                        旧Arguments[Offset+0],
                        旧Arguments[Offset+1],
                        旧Arguments[Offset+2],
                        旧Arguments[Offset+3]
                    )
                );
            case 5:
                return Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple5,
                        旧Arguments[Offset+0].Type,
                        旧Arguments[Offset+1].Type,
                        旧Arguments[Offset+2].Type,
                        旧Arguments[Offset+3].Type,
                        旧Arguments[Offset+4].Type
                    ),
                    作業配列.Expressions設定(
                        旧Arguments[Offset+0],
                        旧Arguments[Offset+1],
                        旧Arguments[Offset+2],
                        旧Arguments[Offset+3],
                        旧Arguments[Offset+4]
                    )
                );
            case 6:
                return Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple6,
                        旧Arguments[Offset+0].Type,
                        旧Arguments[Offset+1].Type,
                        旧Arguments[Offset+2].Type,
                        旧Arguments[Offset+3].Type,
                        旧Arguments[Offset+4].Type,
                        旧Arguments[Offset+5].Type
                    ),
                    作業配列.Expressions設定(
                        旧Arguments[Offset+0],
                        旧Arguments[Offset+1],
                        旧Arguments[Offset+2],
                        旧Arguments[Offset+3],
                        旧Arguments[Offset+4],
                        旧Arguments[Offset+5]
                    )
                );
            case 7:
                return Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple7,
                        旧Arguments[Offset+0].Type,
                        旧Arguments[Offset+1].Type,
                        旧Arguments[Offset+2].Type,
                        旧Arguments[Offset+3].Type,
                        旧Arguments[Offset+4].Type,
                        旧Arguments[Offset+5].Type,
                        旧Arguments[Offset+6].Type
                    ),
                    作業配列.Expressions設定(
                        旧Arguments[Offset+0],
                        旧Arguments[Offset+1],
                        旧Arguments[Offset+2],
                        旧Arguments[Offset+3],
                        旧Arguments[Offset+4],
                        旧Arguments[Offset+5],
                        旧Arguments[Offset+6]
                    )
                );
            default: {
                var Arguments7 = ValueTupleでNewする(作業配列,旧Arguments,Offset+7);
                return Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple8,
                        旧Arguments[Offset+0].Type,
                        旧Arguments[Offset+1].Type,
                        旧Arguments[Offset+2].Type,
                        旧Arguments[Offset+3].Type,
                        旧Arguments[Offset+4].Type,
                        旧Arguments[Offset+5].Type,
                        旧Arguments[Offset+6].Type,
                        Arguments7.Type
                    ),
                    作業配列.Expressions設定(
                        旧Arguments[Offset+0],
                        旧Arguments[Offset+1],
                        旧Arguments[Offset+2],
                        旧Arguments[Offset+3],
                        旧Arguments[Offset+4],
                        旧Arguments[Offset+5],
                        旧Arguments[Offset+6],
                        Arguments7
                    )
                );
            }
        }
    }
    public static Type SQLのTypeからTypeに変換(string DBType) {
        //return DBType.ToLower()switch{
        //    "bit"=>typeof(bool),
        //    "char"or"varchar"or"nchar"or"nvarchar"or"text"or"ntext"or"sysname"or _=> 
        //        DBType[..4]=="char"||DBType[..4]=="text"||DBType[..5]=="nchar"||DBType[..5]=="ntext"||DBType[..7]=="varchar"||DBType[..7]=="sysname"||DBType[..8]=="nvarchar"
        //            ?typeof(string)
        //            :throw new NotSupportedException(DBType)
        //};
        return DBType.ToUpperInvariant() switch {
            "BIT"                                                                               => typeof(bool),
            "TINYINT"                                                                           => typeof(byte),
            "SMALLINT"                                                                          => typeof(short),
            "INT" or "INTEGER"                                                                  => typeof(int),
            "BIGINT"                                                                            => typeof(long),
            "REAL"                                                                              => typeof(float),
            "FLOAT"                                                                             => typeof(double),
            "DECIMAL" or "NUMERIC" or "SMALLMONEY" or "MONEY"                                   => typeof(decimal),
            "DATE" or "DATETIME" or "DATETIME2" or "SMALLDATETIME"                              => typeof(DateTime),
            "DATETIMEOFFSET"                                                                    => typeof(DateTimeOffset),
            "TIMESTAMP"                                                                         => typeof(DateTime),
            "BINARY" or "VARBINARY"                                                             => typeof(byte[]),
            "GEOGRAPHY"                                                                         => typeof(Microsoft.SqlServer.Types.SqlGeography),
            "GEOMETRY"                                                                          => typeof(Microsoft.SqlServer.Types.SqlGeometry),
            "IMAGE" or "SQL_VARIANT"                                                            => typeof(object),
            "XML"                                                                               => typeof(XDocument),
            "UNIQUEIDENTIFIER"                                                                  => typeof(Guid),
            "TIME"                                                                              => typeof(TimeSpan),
            "HIERARCHYID"                                                                       => typeof(Microsoft.SqlServer.Types.SqlHierarchyId),
            "CHAR" or "VARCHAR" or "NCHAR" or "NVARCHAR" or "TEXT" or "NTEXT" or "SYSNAME" or _ =>DBType[..4]=="char"||DBType[..4]=="text"||DBType[..5]=="nchar"||DBType[..5]=="ntext"||DBType[..7]=="varchar"||DBType[..7]=="sysname"||DBType[..8]=="nvarchar"
                    ? typeof(string)
                    : throw new NotSupportedException(DBType)
        };
        //DBType=DBType.ToLower();
        //switch(DBType) {
        //    case "bit":return typeof(bool);
        //    case "tinyint":return typeof(byte);
        //    case "smallint":return typeof(short);
        //    case "int":
        //    case "integer":return typeof(int);
        //    case "bigint":return typeof(long);
        //    case "real":return typeof(float);
        //    case "float":return typeof(double);
        //    case "decimal"or "numeric":
        //    case "smallmoney":
        //    case "money":return typeof(decimal);
        //    case "date":
        //    case "datetime":
        //    case "datetime2":
        //    case "smalldatetime":return typeof(DateTime);
        //    case "datetimeoffset":return typeof(DateTimeOffset);
        //    //case "timestamp":return typeof(DateTimeOffset);
        //    case "binary":
        //    case "varbinary":return typeof(byte[]);
        //    case "geography":return typeof(Microsoft.SqlServer.Types.SqlGeography);
        //    case "geometry":return typeof(Microsoft.SqlServer.Types.SqlGeometry);
        //    case "image":
        //    case "sql_variant":return typeof(object);
        //    case "xml":return typeof(XDocument);
        //    case "uniqueidentifier":return typeof(Guid);
        //    case "time":return typeof(TimeSpan);
        //    case "hierarchyid":return typeof(Microsoft.SqlServer.Types.SqlHierarchyId);
        //    case "char":
        //    case "varchar":
        //    case "nchar":
        //    case "nvarchar":
        //    case "text":
        //    case "ntext":
        //    case "sysname":
        //    default: 
        //        if(DBType[..4]=="char"||DBType[..4]=="text"||DBType[..5]=="nchar"||DBType[..5]=="ntext"||DBType[..7]=="varchar"||DBType[..7]=="sysname"||DBType[..8]=="nvarchar")
        //            return typeof(string);
        //        throw new NotSupportedException(DBType);
        //}
    }
    public static readonly string[] 日時Formats={
        "yyyyMMdd",
        "yyyy-M-d",
        "yyyy/M/d",
    };
    public static void Read(Stream Stream,byte[]ReadBuffer,int Count){
        var ReadOffset=0;
        do{
            var ReadしたBytes=Stream.Read(ReadBuffer,ReadOffset,Count);
            ReadOffset+=ReadしたBytes;
            Count-=ReadしたBytes;
        } while(Count>0);
    }
    //public static string DebugView(Expression Expression){
    //    dynamic n=new NonPublicAccessor(Expression);
    //    return n.DebugView;
    //}
    private static readonly Regex Regex単純な識別子 = new("[^ ,^$,^#,^<,^[,^>,^(]*[.]",RegexOptions.Compiled);
    public static string インラインラムダテキスト(Expression e) {
        //[^ ]*[.]
        dynamic NonPublicAccessor = new NonPublicAccessor(typeof(Expression),e);
        string 変換前 = NonPublicAccessor.DebugView;
        //変換前=変換前.Replace("LinqDB.Sets.","");
        //変換前=変換前.Replace("System.Collections.Generic.","");
        //変換前=変換前.Replace("System.Collections.","");
        //変換前=変換前.Replace("System.","");
        変換前=変換前.Replace("\r\n{","{");
        var KeyValues = new Generic.List<(string Key, Generic.List<string> Values)>();
        {
            var r = new StringReader(変換前);
            var ラムダ式定義 = new Generic.List<string>();
            var ラムダ定義の1行目 = new Regex(@"^\.Lambda .*\(.*$",RegexOptions.Compiled);
            while(true) {
                var Line = r.ReadLine();
                if(string.IsNullOrEmpty(Line)) {
                    KeyValues.Add(("", ラムダ式定義));
                    break;
                }
                ラムダ式定義.Add(Line);
            }
            while(true) {
                var Line = r.ReadLine();
                if(Line is null)
                    break;
                if(ラムダ定義の1行目.IsMatch(Line)) {
                    var Key = Line[..Line.IndexOf("(",StringComparison.Ordinal)];
                    ラムダ式定義=new Generic.List<string>();
                    KeyValues.Add((Key, ラムダ式定義));
                    ラムダ式定義.Add(Line);
                } else if(string.Equals(Line,"}",StringComparison.Ordinal)) {
                    ラムダ式定義.Add(Line);
                } else if(!string.Equals(Line,string.Empty,StringComparison.Ordinal)) {
                    if(Line[^2]=='>'&&Line[^1]==')') Line=Line.Substring(0,Line.Length-1);
                    ラムダ式定義.Add(Line);
                }
            }
        }
        {
            //Debug.Assert(ルートラムダ is not null,"ルートラムダ != null");
            //var ルートラムダ2=ルートラムダ;
            //再試行:
            //var r = new StringReader(ルートラムダ);
            //var sb = new StringBuilder();
            while(true) {
                for(var a = KeyValues.Count-1;a>=0;a--) {
                    var a_KeyValue = KeyValues[a];
                    var a_Values = a_KeyValue.Values;
                    for(var b = a_Values.Count-1;b>=0;b--) {
                        var 変換元Line = a_Values[b];
                        for(var c = KeyValues.Count-1;c>=0;c--) {
                            var 変換先Value = KeyValues[c];
                            var 変換先キー = 変換先Value.Key;
                            var Lambda位置Index = 変換元Line.IndexOf(変換先キー,StringComparison.Ordinal);
                            if(Lambda位置Index>0) {
                                var 変換先Values = 変換先Value.Values;
                                a_Values[b]=変換元Line[..Lambda位置Index]+変換先Values[0];
                                var Index = 変換元Line.TakeWhile(文字 => 文字==' ').Count();
                                for(var d = 1;d<変換先Values.Count-1;d++)
                                    a_Values.Insert(b+d,new string(' ',Index)+変換先Values[d]);
                                a_Values.Insert(b+変換先Values.Count-1,new string(' ',Index)+変換先Values[^1]+変換元Line[(Lambda位置Index+変換先キー.Length)..]);
                                break;
                            }
                        }
                    }
                }
                var sb = new StringBuilder();
                foreach(var Value in KeyValues[0].Values) sb.AppendLine(Value);
                //var RegxLambda = new Regex(@"\.Lambda #Lambda.*<",RegexOptions.Compiled);
                //foreach(var Value in KeyValues[0].Values) {
                //    var Value2 = RegxLambda.Replace(Value,"");
                //    if(Value2!=Value) {
                //        Value2=Value2.Replace(">(","(");
                //    }
                //    sb.AppendLine(Value2);
                //}
                var Result1 = sb.ToString();
                var Constant = new Regex(@"\.Constant<.*?>\(.*?\)",RegexOptions.Compiled);
                var Result2 = Constant.Replace(Result1,"");
                //var Result3 = Result2.Replace("ExtensionSet","");
                //var Result4 = Result3.Replace("ExtensionEnumerable","");
                //var NodeType= new Regex(@"#Lambda.*\(",RegexOptions.Compiled);
                //var Result5 = NodeType.Replace(Result4,"(");
                var NodeTypeを除く = new Regex(@" \..*? ",RegexOptions.Compiled);
                //var Regx5= new Regex(@"Lambda #Lambda.*<",RegexOptions.Compiled);
                var Result5 = NodeTypeを除く.Replace(Result2," ");
                //var Result6 = Result5.Replace(" ."," ");
                if(Result5[0]=='.') Result5=Result5[1..];
                var Result6 = Regex単純な識別子.Replace(Result5,"");
                return Result6;
            }
        }
    }
    public static string 整形したDebugView(Expression Lambda) {
        var 旧String = インラインラムダテキスト(Lambda);
        var 新StringBuilder = new StringBuilder();
        //var キー定義 = new Regex(@"^\..*>\( \{$",RegexOptions.Singleline);
        var キー本体 = new Regex(@"^\..*\}$",RegexOptions.Singleline);
        {
            var キー参照 = new Regex(@"\..*>\)",RegexOptions.Multiline);
            for(var キー定義_Match = キー本体.Match(旧String);キー定義_Match.Success;キー定義_Match=キー定義_Match.NextMatch()) {
                var 前index = 0;
                for(var キー参照_Match = キー参照.Match(旧String);キー参照_Match.Success;キー参照_Match=キー参照_Match.NextMatch()) {
                    var 後index = キー参照_Match.Index;
                    新StringBuilder.Append(旧String[前index..後index]);
                    前index=後index;
                    新StringBuilder.Append(キー定義_Match.Value);
                    新StringBuilder.Append(')');
                }
                旧String=新StringBuilder.ToString();
                新StringBuilder.Clear();
            }
        }
        {
            var キー参照 = new Regex(@"\..*>,",RegexOptions.Multiline);
            for(var キー定義_Match = キー本体.Match(旧String);キー定義_Match.Success;キー定義_Match=キー定義_Match.NextMatch()) {
                var 前index = 0;
                for(var キー参照_Match = キー参照.Match(旧String);キー参照_Match.Success;キー参照_Match=キー参照_Match.NextMatch()) {
                    var 後index = キー参照_Match.Index;
                    新StringBuilder.Append(旧String[前index..後index]);
                    前index=後index;
                    新StringBuilder.Append(キー定義_Match.Value);
                    新StringBuilder.Append(',');
                }
                旧String=新StringBuilder.ToString();
                新StringBuilder.Clear();
            }
        }
        return 旧String;
    }
}
//560