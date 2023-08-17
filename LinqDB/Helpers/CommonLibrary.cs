using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using LinqDB.Optimizers;
using LinqDB.Sets;
using LinqDB.Sets.Exceptions;
namespace LinqDB.Helpers;

/// <summary>
/// 設定と定数。
/// </summary>
public static class CommonLibrary {
    static CommonLibrary(){
    }
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
        /// <inheritdoc/>
        public override bool CanRead => true;
        /// <inheritdoc/>
        public override bool CanSeek => true;
        /// <inheritdoc/>
        public override bool CanWrite => true;
        private long _Length;
        /// <inheritdoc/>
        public override long Length => this._Length;
        /// <inheritdoc/>
        public override long Position {
            get;
            set;
        }
        /// <inheritdoc/>
        public override void Flush() {
        }
        /// <inheritdoc/>
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
        /// <inheritdoc/>
        public override int ReadByte() {
            var Position = this.Position;
            if(Position<this._Length) {
                this.Position=Position+1;
                return this.Buffer[Position];
            } else {
                return -1;
            }
        }
        /// <inheritdoc/>
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
        /// <inheritdoc/>
        public override void SetLength(long value) {
            if(value>this.Buffer.Length) {
                var Buffer_Length = this.Buffer.Length;
                throw new IndexOutOfRangeException(
                    $"value≦Buffer.Lengthが満たされる必要がある。"+
                    $"実際は{value}≦{Buffer_Length}だった。"
                );
            }
            this._Length=value;
            this.必要なLength=0;
        }
        /// <inheritdoc/>
        public override void Write(byte[] buffer,int offset,int count) {
            Debug.Assert(offset+count<=buffer.Length);
            //Debug.Assert(this.Position+count<=this.Buffer.Length);
            this.必要なLength+=count;
            var Position = this.Position;
            if(Position+count<=this.Buffer.Length) {
                Array.Copy(buffer,offset,this.Buffer,Position,count);
                Position+=count;
                if(this._Length<Position) {
                    this._Length=Position;
                }
                this.Position=Position;
            } else {
                this.必要なLength+=count;
            }
        }
        /// <inheritdoc/>
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
    internal const int ハッシュバイト数 = 256/8;
    internal const string シーケンスに要素が含まれていません_NoElements = "シーケンスに要素が含まれていません";
    internal static ZeroTupleException シーケンスに要素が含まれていません(MethodBase Method) => new($"{Method}:{シーケンスに要素が含まれていません_NoElements}");
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
    internal const AddressFamily AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork;
    internal static readonly IPAddress IPAddress=IPAddress.Any;
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
    internal static readonly string IEnumerable1_FullName = typeof(IEnumerable<>).FullName!;
    internal static readonly string IEnumerable_FullName = typeof(IEnumerable).FullName!;
    internal static readonly string IGrouping2_FullName = typeof(IGrouping<,>).FullName!;
    internal static readonly string IOutputSet1_FullName = typeof(IOutputSet<>).FullName!;
    internal static readonly string IGroupingSet2_FullName = typeof(IGroupingSet<,>).FullName!;
    internal const int CS匿名型名_Length18= 18;
    internal const string CS匿名型名="<>f__AnonymousType";
    internal const int VB匿名型名_Length16 = 16;
    internal const string VB匿名型名="VB$AnonymousType";
    internal const int CSクロージャー_Length17 = 17;
    internal const string CSクロージャー="<>c__DisplayClass";
    internal const int VBクロージャー_Length11 = 11;
    internal const string VBクロージャー="_Closure$__";
    internal static readonly string IEquatable_FullName = typeof(IEquatable<>).FullName!;
    internal static readonly string IEqualityComparer_FullName = typeof(IEqualityComparer<>).FullName!;
    /// <summary>
    /// Typeが匿名型であるか判定する。
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    public static bool IsAnonymous(this Type Type){
        if (Type.Namespace == null && Type.IsSealed && (Type.Name.StartsWith("<>f__AnonymousType", StringComparison.Ordinal) || Type.Name.StartsWith("<>__AnonType", StringComparison.Ordinal) || Type.Name.StartsWith("VB$AnonymousType_", StringComparison.Ordinal)))
        {
            return Type.IsDefined(typeof(CompilerGeneratedAttribute), inherit: false);
        }
        return false;
    }
    //public static bool IsAnonymous(this Type Type)=>Type.IsClass&&Type.IsGenericType&&(Type.Name.IndexOf(
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
    /// <summary>
    /// 小文字か
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool IsLower(this string s) =>s.All(c=>!char.IsUpper(c));
    /// <summary>
    /// TypeがNullableまたは参照型であるか判定する。
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    public static bool IsNullable又は参照型(this Type Type) => !Type.IsValueType&&Type.IsGenericType&&Type.GetGenericTypeDefinition()==typeof(Nullable<>);
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
    internal static NewExpression ValueTupleでNewする(Optimizer.作業配列 作業配列,IList<Expression> Arguments,int Offset) {
        var 残りType数 = Arguments.Count-Offset;
        switch(残りType数) {
            case 1:
                return Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple1,
                        Arguments[Offset+0].Type
                    ),
                    作業配列.Expressions設定(
                        Arguments[Offset+0]
                    )
                );
            case 2:
                return Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple2,
                        Arguments[Offset+0].Type,
                        Arguments[Offset+1].Type
                    ),
                    作業配列.Expressions設定(
                        Arguments[Offset+0],
                        Arguments[Offset+1]
                    )
                );
            case 3:
                return Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple3,
                        Arguments[Offset+0].Type,
                        Arguments[Offset+1].Type,
                        Arguments[Offset+2].Type
                    ),
                    作業配列.Expressions設定(
                        Arguments[Offset+0],
                        Arguments[Offset+1],
                        Arguments[Offset+2]
                    )
                );
            case 4:
                return Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple4,
                        Arguments[Offset+0].Type,
                        Arguments[Offset+1].Type,
                        Arguments[Offset+2].Type,
                        Arguments[Offset+3].Type
                    ),
                    作業配列.Expressions設定(
                        Arguments[Offset+0],
                        Arguments[Offset+1],
                        Arguments[Offset+2],
                        Arguments[Offset+3]
                    )
                );
            case 5:
                return Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple5,
                        Arguments[Offset+0].Type,
                        Arguments[Offset+1].Type,
                        Arguments[Offset+2].Type,
                        Arguments[Offset+3].Type,
                        Arguments[Offset+4].Type
                    ),
                    作業配列.Expressions設定(
                        Arguments[Offset+0],
                        Arguments[Offset+1],
                        Arguments[Offset+2],
                        Arguments[Offset+3],
                        Arguments[Offset+4]
                    )
                );
            case 6:
                return Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple6,
                        Arguments[Offset+0].Type,
                        Arguments[Offset+1].Type,
                        Arguments[Offset+2].Type,
                        Arguments[Offset+3].Type,
                        Arguments[Offset+4].Type,
                        Arguments[Offset+5].Type
                    ),
                    作業配列.Expressions設定(
                        Arguments[Offset+0],
                        Arguments[Offset+1],
                        Arguments[Offset+2],
                        Arguments[Offset+3],
                        Arguments[Offset+4],
                        Arguments[Offset+5]
                    )
                );
            case 7:
                return Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple7,
                        Arguments[Offset+0].Type,
                        Arguments[Offset+1].Type,
                        Arguments[Offset+2].Type,
                        Arguments[Offset+3].Type,
                        Arguments[Offset+4].Type,
                        Arguments[Offset+5].Type,
                        Arguments[Offset+6].Type
                    ),
                    作業配列.Expressions設定(
                        Arguments[Offset+0],
                        Arguments[Offset+1],
                        Arguments[Offset+2],
                        Arguments[Offset+3],
                        Arguments[Offset+4],
                        Arguments[Offset+5],
                        Arguments[Offset+6]
                    )
                );
            default: {
                var Arguments7 = ValueTupleでNewする(作業配列,Arguments,Offset+7);
                return Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple8,
                        Arguments[Offset+0].Type,
                        Arguments[Offset+1].Type,
                        Arguments[Offset+2].Type,
                        Arguments[Offset+3].Type,
                        Arguments[Offset+4].Type,
                        Arguments[Offset+5].Type,
                        Arguments[Offset+6].Type,
                        Arguments7.Type
                    ),
                    作業配列.Expressions設定(
                        Arguments[Offset+0],
                        Arguments[Offset+1],
                        Arguments[Offset+2],
                        Arguments[Offset+3],
                        Arguments[Offset+4],
                        Arguments[Offset+5],
                        Arguments[Offset+6],
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
        return DBType.ToLower() switch {
            "bit" => typeof(bool),
            "tinyint" => typeof(byte),
            "smallint" => typeof(short),
            "int" or "integer" => typeof(int),
            "bigint" => typeof(long),
            "real" => typeof(float),
            "float" => typeof(double),
            "decimal" or "numeric" or "smallmoney" or "money" => typeof(decimal),
            "date" or "datetime" or "datetime2" or "smalldatetime" => typeof(DateTime),
            "datetimeoffset" => typeof(DateTimeOffset),
            "timestamp" => typeof(DateTime),
            //e "timestamp"=>typeof(DateTimeOffset),
            "binary" or "varbinary" => typeof(byte[]),
            "geography" => typeof(Microsoft.SqlServer.Types.SqlGeography),
            "geometry" => typeof(Microsoft.SqlServer.Types.SqlGeometry),
            "image" or "sql_variant" => typeof(object),
            "xml" => typeof(XDocument),
            "uniqueidentifier" => typeof(Guid),
            "time" => typeof(TimeSpan),
            "hierarchyid" => typeof(Microsoft.SqlServer.Types.SqlHierarchyId),
            "char" or "varchar" or "nchar" or "nvarchar" or "text" or "ntext" or "sysname" or _ =>
                DBType[..4]=="char"||DBType[..4]=="text"||DBType[..5]=="nchar"||DBType[..5]=="ntext"||DBType[..7]=="varchar"||DBType[..7]=="sysname"||DBType[..8]=="nvarchar"
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
}
//560