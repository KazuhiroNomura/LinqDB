using System;
using System.Linq;
using System.Xml.Linq;
using System.Data.SqlClient;
using Microsoft.SqlServer.Types;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using LinqDB.Sets;
using LinqDB.Optimizers;
using LinqDB.Optimizers.Comparison;
using IEnumerable=System.Collections.IEnumerable;
public abstract class 共通 {
    protected static readonly 汎用Comparer ProtectedComparer=new(ExpressionEqualityComparer);
    protected static readonly ExpressionEqualityComparer ExpressionEqualityComparer=new();
    protected static void WriteLine(string s) {
        Trace.WriteLine(s);
        Console.WriteLine(s);
    }
    protected static void 比較<T>(Func<T> LINQ,Func<object> SQL,string メソッド名) {
        Console.WriteLine(メソッド名+"------------------------------------------------------------------------------------");
        T LINQResult;
        long LINQms;
        long LINQBytes;
        long LINQCount;
        {
            WriteLine("LINQ");
            var s = Stopwatch.StartNew();
            LINQResult=LINQ();
            LINQms=s.ElapsedMilliseconds;
            LINQBytes=GC.GetTotalMemory(true);
            if(LINQResult is IEnumerable Enumerable) {
                LINQCount=0;
                foreach(var a in Enumerable) {
                    //WriteLine(a.ToString());
                    LINQCount++;
                }
                WriteLine($"Count:{LINQCount}");
            } else {
                LINQCount=-1;
                WriteLine($"{LINQResult}");
            }
            WriteLine($"{LINQms,7}ms");
            WriteLine($"{LINQBytes/1024/1024,7}MB");
        }
        object SQLResult;
        long SQLms;
        long SQLBytes;
        long SQLCount;
        {
            WriteLine("SQL");
            var s = Stopwatch.StartNew();
            SQLResult=SQL();
            SQLms=s.ElapsedMilliseconds;
            SQLBytes=GC.GetTotalMemory(true);
            if(SQLResult is IEnumerable Enumerable) {
                SQLCount=0;
                foreach(var a in Enumerable) {
                    SQLCount++;
                }
                WriteLine($"Count:{SQLCount}");
            } else {
                SQLCount=-1;
                WriteLine($"{SQLResult}");
            }
            WriteLine($"{SQLms,7}ms");
            WriteLine($"{SQLBytes/1024/1024,7}MB");
        }
        Debug.Assert(LINQCount==SQLCount);
        long ComparerElapsedMilliseconds;
        {
            var s = Stopwatch.StartNew();
            Debug.Assert(ProtectedComparer.Equals(LINQResult!,SQLResult));
            ComparerElapsedMilliseconds=s.ElapsedMilliseconds;
        }
        //WriteLine($"Comparer{Comparer}");
        WriteLine($"{ComparerElapsedMilliseconds,7}ms");
        if(SQLCount>=0) {
            Debug.Assert(SQLCount==LINQCount,"SQLCount==LINQCount");
        }
    }
    protected static void WriteLine標準(Action D,string メソッド名) {
        var s = Stopwatch.StartNew();
        D();
        var ElapsedMilliseconds = s.ElapsedMilliseconds;
        var Bytes = GC.GetTotalMemory(true);
        WriteLine(
            $"{メソッド名},"+
            $"{ElapsedMilliseconds,7}ms,"+
            $"{Bytes/1024/1024,7}GC MB,"+
            $"{Environment.WorkingSet/1024/1024,7}WS MB"
        );
    }
    //private static Optimizer Optimizer = new Optimizer() { OptimizeLevel=OptimizeLevels.デバッグ };
    protected static List<T> SQL実行<T>(SqlCommand Command,string SQL,Func<SqlDataReader,T> f) {
        Command.CommandText=SQL;
        using var Reader = Command.ExecuteReader();
        var r = new List<T>();
        while(Reader.Read())
            r.Add(f(Reader));
        return r;
    }
    protected static Optimizer o = new()
    {
        //OptimizeLevel=OptimizeLevels.デバッグ^OptimizeLevels.プロファイル,
        AssemblyFileName = "共通.dll"
    };
    protected static void 比較<T>(Expression<Func<System.Collections.Generic.IEnumerable<T>>> LINQ,SqlCommand Command,string SQL,Func<SqlDataReader,T> SQLSelector) {
        var LINQ結果 = o.Execute(LINQ);
        var SQL結果 = SQL実行(
            Command,
            SQL,
            SQLSelector
        );
        WriteLine(new StackFrame(1).GetMethod()!.Name);
        var s = Stopwatch.StartNew();
        var LINQ結果1 = LINQ結果.ToArray();
        var SQL結果1 = SQL結果.ToArray();
        Array.Sort(LINQ結果1,(a,b) => string.CompareOrdinal(a!.ToString(),b!.ToString()));
        Array.Sort(SQL結果1,(a,b) => string.CompareOrdinal(a!.ToString(),b!.ToString()));
        Debug.Assert(LINQ結果1.Length==SQL結果1.Length);
        for(var a = 0;a<LINQ結果1.Length;a++) {
            if(!LINQ結果1[a]!.Equals(SQL結果1[a])) {
                Debug.Assert(LINQ結果1[a]!.ToString()==SQL結果1[a]!.ToString());
            }
        }
        var ComparerElapsedMilliseconds=s.ElapsedMilliseconds;
        WriteLine($"{ComparerElapsedMilliseconds,7}ms");
    }
    private static readonly Random r = new(1);
    protected static int ID<T>(Set<T> Set) => r.Next(((int)Set.LongCount+1)*2);
    [DebuggerDisplay("{"+nameof(Display)+"}")]
    protected struct 情報 {
        public string Display => $"{this.成功},{this.重複},{this.例外}";
        public int 成功, 重複, 例外;
    }
    protected struct AddDel情報 {
        public string 名前;
        public 情報 Add;
        public 情報 Del;
    }
    protected static void Add<T>(ref AddDel情報 情報,Set<T> Set,T Value) {
        情報.名前=typeof(T).Name;
        try {
            if(Set.IsAdded(Value)) {
                情報.Add.成功++;
            } else {
                情報.Add.重複++;
            }
        } catch(LinqDB.Databases.RelationshipException) {
            情報.Add.例外++;
        }
    }
    protected static void Del<TValue, TKey, TContainer>(ref AddDel情報 情報,Set<TKey,TValue,TContainer> Set)
        where TValue : Entity<TKey,TContainer>
        //where TValue : Entity<TKey,TContainer>, IPrimaryKey<TKey>, IEquatable<TValue>//,IWriteRead<TValue>
        where TKey : struct, IEquatable<TKey>
        where TContainer : LinqDB.Databases.Container {
        if(Set.LongCount==0) return;
        try {
            var Sampling = Set.Sampling;
            if(Set.RemoveKey(Sampling.Key)) {
                情報.Del.成功++;
            } else {
                情報.Del.重複++;
            }
        } catch(LinqDB.Databases.RelationshipException) {
            情報.Del.例外++;
        }
    }
    protected static void AddWriteLine(AddDel情報 AddRemove情報) {
        var 情報 = AddRemove情報.Add;
        var 分母 = 情報.成功+情報.重複+情報.例外;
        WriteLine($"+{AddRemove情報.名前,10} {情報.成功}/{分母}={(double)情報.成功/分母}");
    }
    protected static void DelWriteLine(AddDel情報 AddRemove情報) {
        var 情報 = AddRemove情報.Del;
        var 分母 = 情報.成功+情報.重複+情報.例外;
        WriteLine($"+{AddRemove情報.名前,10} {情報.成功}/{分母}={(double)情報.成功/分母}");
    }
    protected static void 列挙<T>(Expression<Func<ImmutableSet<T>>> Lambda) {
        var Method = Lambda.Compile();
        var Result = Method();
        var Count0 = 0;
        var s0 = Stopwatch.StartNew();
        foreach(var a in Result) Count0++;
        s0.Stop();
        var Count1 = 0;
        var s1 = Stopwatch.StartNew();
        foreach(var a in Result) {
            Trace.WriteLine(a);
            Console.WriteLine(a);
            Count1++;
        }
        s1.Stop();
        var Count2 = 0;
        var s2 = Stopwatch.StartNew();
        foreach(var a in (IEnumerable)Result) Count2++;
        s2.Stop();
        Debug.Assert(Count0==Result.LongCount);
        Debug.Assert(Count0==Count1);
        Debug.Assert(Count0==Count2);
        var t = Lambda.ToString();
        Console.WriteLine($"{t.Substring(t.LastIndexOf('.')+1),30}{Result.LongCount,7} Set<T>{s0.ElapsedMilliseconds,7}ms IEnumerable<T>{s1.ElapsedMilliseconds,7}ms IEnumerable{s2.ElapsedMilliseconds,7}ms");
    }
    protected static void E<T>(System.Collections.Generic.IEnumerable<T> Set,Action<T> Action){
        var Count=0L;
        Debug.Assert(Set!=null,nameof(Set)+" != null");
        foreach(var a in Set!) {
            Action(a);
            Count++;
        }
        Debug.Assert(Count==Set.Count());
    }
    protected static SqlHierarchyId? GetHierarchyidNullable(SqlDataReader r,int i) {
        if(r.IsDBNull(i)) {
            return null;
        } else {
            return GetHierarchyid(r,i);
        }
    }
    protected static SqlHierarchyId GetHierarchyid(SqlDataReader r,int i) {
        var SqlBytes = r.GetSqlBytes(i);
        {
            using var BinaryReader0=new System.IO.BinaryReader(SqlBytes.Stream,System.Text.Encoding.UTF8,true);
            var SqlHierarchyId0=new SqlHierarchyId();
            SqlHierarchyId0.Read(BinaryReader0);
            using var BinaryReader1=new System.IO.BinaryReader(SqlBytes.Stream,System.Text.Encoding.UTF8,true);
            var SqlHierarchyId1=new SqlHierarchyId();
            SqlHierarchyId1.Read(BinaryReader1);
            var x=SqlHierarchyId0.Equals(SqlHierarchyId1);
        }
        using var BinaryReader=new System.IO.BinaryReader(SqlBytes.Stream,System.Text.Encoding.UTF8,true);
        var SqlHierarchyId=new SqlHierarchyId();
        SqlHierarchyId.Read(BinaryReader);
        return SqlHierarchyId;
    }
    protected static SqlGeography? GetSqlGeography(SqlDataReader r,int i) {
        if(r.IsDBNull(i)) {
            return null;
        } else{
            var SqlBytes = r.GetSqlBytes(i);
            using var BinaryReader = new System.IO.BinaryReader(SqlBytes.Stream,System.Text.Encoding.UTF8,true);
            var SqlGeography = new SqlGeography();
            SqlGeography.Read(BinaryReader);
            return SqlGeography;
        }
    }
    protected static string? GetString(SqlDataReader r,int i) => r.IsDBNull(i) ? default : r.GetString(i);
    protected static byte? GetByte(SqlDataReader r,int i) => r.IsDBNull(i) ? default(byte?) : r.GetByte(i);
    protected static short? GetInt16(SqlDataReader r,int i) => r.IsDBNull(i) ? default(short?) : r.GetInt16(i);
    protected static int? GetInt32(SqlDataReader r,int i) => r.IsDBNull(i) ? default(int?) : r.GetInt32(i);
    protected static long? GetInt64(SqlDataReader r,int i) => r.IsDBNull(i) ? default(int?) : r.GetInt64(i);
    protected static float? GetSingle(SqlDataReader r,int i) => r.IsDBNull(i) ? default(float?) : r.GetFloat(i);
    protected static double? GetDouble(SqlDataReader r,int i) => r.IsDBNull(i) ? default(double?) : r.GetDouble(i);
    protected static decimal? GetDecimal(SqlDataReader r,int i) => r.IsDBNull(i) ? default(decimal?) : r.GetDecimal(i);
    protected static DateTime? GetDateTime(SqlDataReader r,int i) => r.IsDBNull(i) ? default(DateTime?) : r.GetDateTime(i);
    //protected static DateTimeOffset? GetDateTimeOffset(SqlDataReader r,int i) => r.IsDBNull(i) ? default(DateTimeOffset?) : r.GetDateTimeOffset(i);
    protected static byte[]? GetBytes(SqlDataReader r,int i) => r.IsDBNull(i) ? default : (byte[])r.GetValue(i);
    protected static bool? GetBoolean(SqlDataReader r,int i) => r.IsDBNull(i) ? default(bool?) : r.GetBoolean(i);
    protected static XDocument? GetXDocument(SqlDataReader r,int i) => r.IsDBNull(i) ? default : XDocument.Load(r.GetXmlReader(i));
    //protected static DateTimeOffset GetDateTimeOffset(SqlDataReader r,int i) =>  r.GetDateTime(i);
    //protected static void E(Int32 Count,Action<Int32> Action) {
    //    for(var a = 0;a<Count;a++) {
    //        Action(a);
    //    }
    //}
    //protected static void E<T>(IEnumerable<T> Set,Action<T> Action) {
    //    foreach(var a in Set) {
    //        Action(a);
    //    }
    //}
    //private static void 列挙<T>(Func<ImmutableSet<T>> D) {
    //    var Result = D();
    //    {
    //        var s = Stopwatch.StartNew();
    //        foreach(var a in Result) { }
    //        Console.WriteLine($"Set<T> {s.ElapsedMilliseconds,7}ms");
    //    }
    //    {
    //        var s = Stopwatch.StartNew();
    //        foreach(var a in (IEnumerable<T>)Result) { }
    //        Console.WriteLine($"IEnumerable<T> {s.ElapsedMilliseconds,7}ms");
    //    }
    //    {
    //        var s = Stopwatch.StartNew();
    //        foreach(var a in (IEnumerable)Result) { }
    //        Console.WriteLine($"IEnumerable {s.ElapsedMilliseconds,7}ms");
    //    }
    //}
}
