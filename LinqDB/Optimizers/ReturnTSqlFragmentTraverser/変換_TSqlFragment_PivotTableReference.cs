#pragma warning disable CA1822 // Mark members as static
//#define タイム出力
using System.Diagnostics;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using System.Reflection;
using LinqDB.Helpers;
using LinqDB.Sets;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    private partial (Expressions.Expression Set, Expressions.ParameterExpression Element) PivotedTableReference(PivotedTableReference x) {
        //Alias                       A
        //AggregateFunctionIdentifier SUM
        //PivotColumn                 FOR ss.B
        //TableReference              FROMの後
        //InColumns                   0,1,2,3
        //ValueColumns                ss.C
        //SELECT
        //A._0,
        //A._1,
        //A._2,
        //A._3
        //FROM Keyあり ss PIVOT(
        //    SUM(ss.C)
        //    FOR ss.B
        //    IN(_0,_1,_2,_3)
        //)A
        //SELECT ss.A,ss.D,
        //_0=(SELECT SUM(ss.C)FROM Keyあり ss WHERE ss.B=0),
        //_1=(SELECT SUM(ss.C)FROM Keyあり ss WHERE ss.B=1),
        //_2=(SELECT SUM(ss.C)FROM Keyあり ss WHERE ss.B=2),
        //_3=(SELECT SUM(ss.C)FROM Keyあり ss WHERE ss.B=3)
        //FROM Keyあり ss GROUP BY ss.A,ss.D
        //大事なことは"集約対称列以外すべてでグループ化する"
        //Keyあり.GroupBy(
        //    ss=>ss.A,ss.D
        //    (Key,Group)=>new{
        //        Key.Item1,Key.Item2
        //        _0=Group.Where(ss=>ss.B==0).Sum(ss=>ss.C)
        //        _1=Group.Where(ss=>ss.B==1).Sum(ss=>ss.C)
        //        _2=Group.Where(ss=>ss.B==2).Sum(ss=>ss.C)
        //        _3=Group.Where(ss=>ss.B==3).Sum(ss=>ss.C)
        //    }
        //)
        Debug.Assert(x.ValueColumns.Count==1);
        var StackSubquery単位の情報 = this._StackSubquery単位の情報;
        ref var RefPeek0 = ref StackSubquery単位の情報.RefPeek;
        var RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression = RefPeek0.Dictionary_DatabaseSchemaTable_ColumnExpression;
        var RefPeek0_Dictionary_TableAlias_ColumnAliases = RefPeek0.Dictionary_TableAlias_ColumnAliases;
        var RefPeek0_List_アスタリスクColumnAlias = RefPeek0.List_アスタリスクColumnAlias;
        var RefPeek0_List_アスタリスクColumnExpression = RefPeek0.List_アスタリスクColumnExpression;
        StackSubquery単位の情報.Push();
        ref var RefPeek1 = ref StackSubquery単位の情報.RefPeek;
        var RefPeek1_Dictionary_DatabaseSchemaTable_ColumnExpression = RefPeek1.Dictionary_DatabaseSchemaTable_ColumnExpression;
        var RefPeek1_List_アスタリスクColumnAlias = RefPeek1.List_アスタリスクColumnAlias;
        var RefPeek1_List_アスタリスクColumnExpression = RefPeek1.List_アスタリスクColumnExpression;
        var (Source, ss)=this.TableReference(x.TableReference);
        var x_ValueColumns_0 = x.ValueColumns[0];
        Debug.Assert(IEnumerable1のT(Source.Type)==ss.Type);
        var Element_Type = ss.Type;
        //var x_PivotColumn_MultiPartIdentifier =x_PivotColumn.MultiPartIdentifier;
        var x_PivotColumn = x.PivotColumn;
        var PivotColumnAlias =this.SQL取得(x_PivotColumn);
        //if(x_PivotColumn_MultiPartIdentifier.Count==3) {
        //    PivotColumnAlias=x_PivotColumn_MultiPartIdentifier[2].Value;
        //} else if(x_PivotColumn_MultiPartIdentifier.Count==2) {
        //    PivotColumnAlias=x_PivotColumn_MultiPartIdentifier[1].Value;
        //} else {
        //    PivotColumnAlias=x_PivotColumn_MultiPartIdentifier[0].Value;
        //}
        var x_Alias_Value = x.Alias.Value;
        var keySelector_Expressions_Length = RefPeek1_List_アスタリスクColumnExpression.Count-1;
        var keySelector_Expressions = new Expressions.Expression[keySelector_Expressions_Length];
        var keySelector_ColumnAlias= new string[keySelector_Expressions_Length];
        var ExpressionEqualityComparer = this.ExpressionEqualityComparer;
        var PivotColumnExpression = RefPeek1_Dictionary_DatabaseSchemaTable_ColumnExpression[PivotColumnAlias];
        Debug.Assert(PivotColumnExpression is not null);
        var ValueColumnAlias=this.SQL取得(x_ValueColumns_0);
        var ValueColumnExpression = RefPeek1_Dictionary_DatabaseSchemaTable_ColumnExpression[ValueColumnAlias];
        {
            keySelector_ColumnAlias[0]=PivotColumnAlias;
            keySelector_Expressions[0]=PivotColumnExpression;
            var index = 1;
            for(var a=0;a<RefPeek1_List_アスタリスクColumnExpression.Count;a++) {
                var ColumnExpression = RefPeek1_List_アスタリスクColumnExpression[a];
                if(ExpressionEqualityComparer.Equals(ValueColumnExpression,ColumnExpression)||ExpressionEqualityComparer.Equals(PivotColumnExpression,ColumnExpression)) continue;
                keySelector_ColumnAlias[index]=RefPeek1_List_アスタリスクColumnAlias[a];
                keySelector_Expressions[index]=ColumnExpression;
                index++;
            }
        }
        var 作業配列 = this.作業配列;
        var keySelector_Body = CommonLibrary.ValueTupleでNewする(作業配列,keySelector_Expressions);
        var Key = Expressions.Expression.Parameter(keySelector_Body.Type,"Key");
        var Group = Expressions.Expression.Parameter(作業配列.MakeGenericType(typeof(IEnumerable<>),Element_Type),"Group");
        var x_InColumns=x.InColumns;
        {
            var Item番号 = 1;
            Expressions.Expression ValueTuple = Key;
            for(var a = 0;a<keySelector_Expressions_Length;a++) {
                var keySelector_Expression=keySelector_Expressions[a];
                var Item = ValueTuple_Item(ref ValueTuple,ref Item番号);
                if(ExpressionEqualityComparer.Equals(PivotColumnExpression,keySelector_Expression)) continue;
                if(ExpressionEqualityComparer.Equals(ValueColumnExpression,keySelector_Expression)) continue;
                var ColumnAlias = keySelector_ColumnAlias[a];
                RefPeek0_List_アスタリスクColumnAlias.Add(ColumnAlias);
                RefPeek0_List_アスタリスクColumnExpression.Add(Item);
            }
        }
        var Key_Item1=Expressions.Expression.Field(Key,nameof(ValueTuple<int>.Item1));
        var WhereMethod=作業配列.MakeGenericMethod(Reflection.ExtensionSet.Where,Element_Type);
        var Where_Parameters = new []{ss};
        RefPeek1.集約関数のParameter=ss;
        var FunctionName = x.AggregateFunctionIdentifier.Identifiers[0].Value.ToUpperInvariant();
        foreach(var x_InColumn in x_InColumns) {
            RefPeek0_List_アスタリスクColumnAlias.Add(x_InColumn.Value);
            var Right=Stringをキャスト(Key_Item1,x_InColumn.Value);
            var Where = Expressions.Expression.Call(
                WhereMethod,
                Group,
                Expressions.Expression.Lambda(
                    Expressions.Expression.Equal(Key_Item1,Right),
                    Where_Parameters
                )
            );
            RefPeek1.集約関数のSource=Where;
            Debug.Assert(RefPeek1.集約関数のParameter==ss);
            var Sum_Lambd_Body = this.AggregateFunction(x_ValueColumns_0,FunctionName);
            RefPeek0_List_アスタリスクColumnExpression.Add(Sum_Lambd_Body);
        }
        var resultSelector_Body = CommonLibrary.ValueTupleでNewする(作業配列,RefPeek0_List_アスタリスクColumnExpression);
        var Result = Expressions.Expression.Call(
            作業配列.MakeGenericMethod(
                Reflection.ExtensionSet.GroupBy_keySelector_resultSelector,
                Element_Type,keySelector_Body.Type,resultSelector_Body.Type
            ),
            Source,
            Expressions.Expression.Lambda(keySelector_Body,作業配列.Parameters設定(ss)),
            Expressions.Expression.Lambda(resultSelector_Body,作業配列.Parameters設定(Key,Group))
        );
        var Result_Parameter = Expressions.Expression.Parameter(resultSelector_Body.Type,x_Alias_Value);
        StackSubquery単位の情報.Pop();
        {
            RefPeek0_Dictionary_TableAlias_ColumnAliases.Clear();
            Expressions.Expression ValueTuple = Result_Parameter;
            RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression.Clear();
            var RefPeek0_List_ColumnAlias_Count = RefPeek0_List_アスタリスクColumnAlias.Count;
            var ColumnAliases = new string[RefPeek0_List_ColumnAlias_Count];
            RefPeek0_Dictionary_TableAlias_ColumnAliases.Add(x_Alias_Value,ColumnAliases);
            var Item番号 = 1;
            for(var a = 0;a<RefPeek0_List_ColumnAlias_Count;a++) {
                var ColumnAlias= RefPeek0_List_アスタリスクColumnAlias[a];
                ColumnAliases[a]=ColumnAlias;
                var Item = ValueTuple_Item(ref ValueTuple,ref Item番号);
                RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression.Add(ColumnAlias,Item);                  //Column
                RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression.Add(x_Alias_Value+'.'+ColumnAlias,Item);//Table.Column
                RefPeek0_List_アスタリスクColumnExpression[a]=Item;
            }
        }
        return (Result,Result_Parameter);
    }
    private static Expressions.Expression ValueTuple_Item(ref Expressions.Expression ValueTuple,ref int Item番号) {
        if(Item番号==8) {
            ValueTuple=Expressions.Expression.Field(ValueTuple,"Rest");
            Item番号=1;
        }
        return Expressions.Expression.Field(ValueTuple,$"Item{Item番号++}");
    }
    private Expressions.Expression? AggregateFunction(ScalarExpression x,string FunctionName){
        ref var RefPeek = ref this.RefPeek;
        var 集約関数のParameter = RefPeek.集約関数のParameter!;
        var 集約関数のSource = RefPeek.集約関数のSource!;
        RefPeek.集約関数のParameter=null;
        //Debug.Assert(集約関数のParameter is not null&&集約関数のSource is not null);
        switch(FunctionName) {
            case "AVG": {
                var Body = 共通();
                var Body_Type =Int32に変換して(ref Body);
                if     (typeof(int?    )==Body_Type)return 共通Nullable(Body,Reflection.ExtensionSet.AverageNullableInt32_selector  );
                else if(typeof(long?   )==Body_Type)return 共通Nullable(Body,Reflection.ExtensionSet.AverageNullableInt64_selector  );
                else if(typeof(float?  )==Body_Type)return 共通Nullable(Body,Reflection.ExtensionSet.AverageNullableSingle_selector );
                else if(typeof(double? )==Body_Type)return 共通Nullable(Body,Reflection.ExtensionSet.AverageNullableDouble_selector );
                else if(typeof(decimal?)==Body_Type)return 共通Nullable(Body,Reflection.ExtensionSet.AverageNullableDecimal_selector);
                else throw new NotSupportedException(Body_Type.FullName);
                Expressions.Expression 共通Nullable(Expressions.Expression Body,MethodInfo Method) {
                    var 作業配列=this.作業配列;
                    Method=作業配列.MakeGenericMethod(Method,集約関数のParameter.Type);
                    var Lambda = Expressions.Expression.Lambda(Body,作業配列.Parameters設定(集約関数のParameter));
                    return Expressions.Expression.Call(Method,集約関数のSource,Lambda);
                }
            }
            case "SUM": {
                var Body = 共通();
                MethodInfo Method;
                var Body_Type =Int32に変換して(ref Body);
                if     (Body_Type==typeof(int     ))Method=Reflection.ExtensionSet.SumInt32_selector;
                else if(Body_Type==typeof(long    ))Method=Reflection.ExtensionSet.SumInt64_selector;
                else if(Body_Type==typeof(float   ))Method=Reflection.ExtensionSet.SumSingle_selector;
                else if(Body_Type==typeof(double  ))Method=Reflection.ExtensionSet.SumDouble_selector;
                else if(Body_Type==typeof(decimal ))Method=Reflection.ExtensionSet.SumDecimal_selector;
                else if(Body_Type==typeof(int?    ))Method=Reflection.ExtensionSet.SumNullableInt32_selector;
                else if(Body_Type==typeof(long?   ))Method=Reflection.ExtensionSet.SumNullableInt64_selector;
                else if(Body_Type==typeof(float?  ))Method=Reflection.ExtensionSet.SumNullableSingle_selector;
                else if(Body_Type==typeof(double? ))Method=Reflection.ExtensionSet.SumNullableDouble_selector;
                else if(Body_Type==typeof(decimal?))Method=Reflection.ExtensionSet.SumNullableDecimal_selector;
                else throw new NotSupportedException(Body_Type.FullName);
                var 作業配列=this.作業配列;
                return Expressions.Expression.Call(
                    作業配列.MakeGenericMethod(Method,集約関数のParameter.Type),
                    集約関数のSource,
                    Expressions.Expression.Lambda(Body,作業配列.Parameters設定(集約関数のParameter))
                );
            }
            case "COUNT":return Expressions.Expression.Convert(
                Expressions.Expression.Call(
                    this.作業配列.MakeGenericMethod(Reflection.ExtensionSet.Count,IEnumerable1のT(集約関数のSource.Type)),
                    集約関数のSource
                ),
                typeof(int?)
            );
            case "MAX":
            case "MIN":{
                var Body = 共通();
                var Body_Type =Int32に変換して(ref Body);
                if     (Body_Type==typeof(int     ))return 共通非Nullable用(Body,FunctionName=="min"?Reflection.ExtensionSet.MinInt32_selector          :Reflection.ExtensionSet.MaxInt32_selector          );
                else if(Body_Type==typeof(long    ))return 共通非Nullable用(Body,FunctionName=="min"?Reflection.ExtensionSet.MinInt64_selector          :Reflection.ExtensionSet.MaxInt64_selector          );
                else if(Body_Type==typeof(float   ))return 共通非Nullable用(Body,FunctionName=="min"?Reflection.ExtensionSet.MinSingle_selector         :Reflection.ExtensionSet.MaxSingle_selector         );
                else if(Body_Type==typeof(double  ))return 共通非Nullable用(Body,FunctionName=="min"?Reflection.ExtensionSet.MinDouble_selector         :Reflection.ExtensionSet.MaxDouble_selector         );
                else if(Body_Type==typeof(decimal ))return 共通非Nullable用(Body,FunctionName=="min"?Reflection.ExtensionSet.MinDecimal_selector        :Reflection.ExtensionSet.MaxDecimal_selector        );
                else if(Body_Type==typeof(int?    ))return 共通Nullable用  (Body,FunctionName=="min"?Reflection.ExtensionSet.MinNullableInt32_selector  :Reflection.ExtensionSet.MaxNullableInt32_selector  );
                else if(Body_Type==typeof(long?   ))return 共通Nullable用  (Body,FunctionName=="min"?Reflection.ExtensionSet.MinNullableInt64_selector  :Reflection.ExtensionSet.MaxNullableInt64_selector  );
                else if(Body_Type==typeof(float?  ))return 共通Nullable用  (Body,FunctionName=="min"?Reflection.ExtensionSet.MinNullableSingle_selector :Reflection.ExtensionSet.MaxNullableSingle_selector );
                else if(Body_Type==typeof(double? ))return 共通Nullable用  (Body,FunctionName=="min"?Reflection.ExtensionSet.MinNullableDouble_selector :Reflection.ExtensionSet.MaxNullableDouble_selector );
                else if(Body_Type==typeof(decimal?))return 共通Nullable用  (Body,FunctionName=="min"?Reflection.ExtensionSet.MinNullableDecimal_selector:Reflection.ExtensionSet.MaxNullableDecimal_selector);
                else if(Body_Type==typeof(object  )){
                    var Method=FunctionName=="min"?Reflection.ExtensionSet.MinTSource_selector        :Reflection.ExtensionSet.MaxTSource_selector;
                    var 作業配列=this.作業配列;
                    Method= 作業配列.MakeGenericMethod(Method,集約関数のParameter.Type,typeof(object));
                    var Lambda = Expressions.Expression.Lambda(Body,作業配列.Parameters設定(集約関数のParameter));
                    return Expressions.Expression.Call(Method,集約関数のSource,Lambda);
                }
                else throw new NotSupportedException(Body_Type.FullName);
                Expressions.Expression 共通非Nullable用(Expressions.Expression Body0,MethodInfo Method)=>this.ConvertNullable(共通Nullable用(Body0,Method));
                Expressions.Expression 共通Nullable用(Expressions.Expression Body0,MethodInfo Method){
                    var 作業配列=this.作業配列;
                    Method= 作業配列.MakeGenericMethod(Method,集約関数のParameter.Type);
                    var Lambda = Expressions.Expression.Lambda(Body0,作業配列.Parameters設定(集約関数のParameter));
                    return Expressions.Expression.Call(Method,集約関数のSource,Lambda);
                }
            }
            default:return null;
                //Expressions.Expression.Dynamic(
                //    Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(
                //        CSharpBinderFlags.None,FunctionName,)
                //    Binder.Binder.
                //    変換_メソッド正規化_取得インライン不可能定数.DynamicReflection.)
                //throw new NotSupportedException(FunctionName);
            static Type Int32に変換して(ref Expressions.Expression Body) {
                var Body_Type=Body.Type;
                if     (Body_Type==typeof(byte  ))Body=Expressions.Expression.Convert(Body,typeof(int ));
                else if(Body_Type==typeof(short ))Body=Expressions.Expression.Convert(Body,typeof(int ));
                else if(Body_Type==typeof(byte? ))Body=Expressions.Expression.Convert(Body,typeof(int?));
                else if(Body_Type==typeof(short?))Body=Expressions.Expression.Convert(Body,typeof(int?));
                return Body.Type;
            }
            Expressions.Expression 共通() {
                ref var RefPeek = ref this.RefPeek;
                RefPeek.集約関数のParameter = null;
                RefPeek.集約関数の内部か = true;
                var Body = this.ScalarExpression(x);
                RefPeek.集約関数の内部か = false;
                RefPeek.集約関数のParameter = 集約関数のParameter;
                return Body;
            }
        }
    }
    //private Expressions.Expression 集約関数(ScalarExpression x,string FunctionName){
    //    Expressions.Expression Result;
    //    ref var RefPeek = ref this._StackSubquery単位の情報.RefPeek;
    //    var 集約関数のParameter = RefPeek.集約関数のParameter!;
    //    var 集約関数のSource = RefPeek.集約関数のSource!;
    //    RefPeek.集約関数のParameter=null;
    //    //Debug.Assert(集約関数のParameter is not null&&集約関数のSource is not null);
    //    switch(FunctionName) {
    //        case "avg": {
    //            RefPeek.集約関数の内部か=true;
    //            var Body = this.ScalarExpression(x)!;
    //            RefPeek.集約関数の内部か=false;
    //            //Body=this.ConvertNullable(Body);
    //            var Body_Type =Int32に変換して(ref Body);
    //            if     (typeof(int?    )==Body_Type)Result=共通Nullable(Body,Reflection.ExtensionSet.AverageNullableInt32_selector  );
    //            else if(typeof(long?   )==Body_Type)Result=共通Nullable(Body,Reflection.ExtensionSet.AverageNullableInt64_selector  );
    //            else if(typeof(float?  )==Body_Type)Result=共通Nullable(Body,Reflection.ExtensionSet.AverageNullableSingle_selector );
    //            else if(typeof(double? )==Body_Type)Result=共通Nullable(Body,Reflection.ExtensionSet.AverageNullableDouble_selector );
    //            else if(typeof(decimal?)==Body_Type)Result=共通Nullable(Body,Reflection.ExtensionSet.AverageNullableDecimal_selector);
    //            else throw new NotSupportedException(Body_Type.FullName);
    //            break;
    //            Expressions.Expression 共通Nullable(Expressions.Expression Body,MethodInfo Method) {
    //                var 作業配列 = this.作業配列;
    //                Debug.Assert(共通Nullable is not null);
    //                Method=作業配列.MakeGenericMethod(Method,集約関数のParameter.Type);
    //                var Lambda = Expressions.Expression.Lambda(Body,作業配列.Parameters設定(集約関数のParameter));
    //                return Expressions.Expression.Call(Method,集約関数のSource,Lambda);
    //            }
    //        }
    //        case "sum": {
    //            RefPeek.集約関数の内部か=true;
    //            var Body = this.ScalarExpression(x)!;
    //            RefPeek.集約関数の内部か=false;
    //            var 作業配列 = this.作業配列;
    //            MethodInfo Method;
    //            var Body_Type =Int32に変換して(ref Body);
    //            if     (Body_Type==typeof(int     ))Method=Reflection.ExtensionSet.SumInt32_selector;
    //            else if(Body_Type==typeof(long    ))Method=Reflection.ExtensionSet.SumInt64_selector;
    //            else if(Body_Type==typeof(float   ))Method=Reflection.ExtensionSet.SumSingle_selector;
    //            else if(Body_Type==typeof(double  ))Method=Reflection.ExtensionSet.SumDouble_selector;
    //            else if(Body_Type==typeof(decimal ))Method=Reflection.ExtensionSet.SumDecimal_selector;
    //            else if(Body_Type==typeof(int?    ))Method=Reflection.ExtensionSet.SumNullableInt32_selector;
    //            else if(Body_Type==typeof(long?   ))Method=Reflection.ExtensionSet.SumNullableInt64_selector;
    //            else if(Body_Type==typeof(float?  ))Method=Reflection.ExtensionSet.SumNullableSingle_selector;
    //            else if(Body_Type==typeof(double? ))Method=Reflection.ExtensionSet.SumNullableDouble_selector;
    //            else if(Body_Type==typeof(decimal?))Method=Reflection.ExtensionSet.SumNullableDecimal_selector;
    //            else throw new NotSupportedException(Body_Type.FullName);
    //            Result=Expressions.Expression.Call(
    //                作業配列.MakeGenericMethod(Method,集約関数のParameter.Type),
    //                集約関数のSource,
    //                Expressions.Expression.Lambda(Body,作業配列.Parameters設定(集約関数のParameter))
    //            );
    //            break;
    //        }
    //        case "count": {
    //            Result=Expressions.Expression.Convert(
    //                Expressions.Expression.Call(
    //                    this.作業配列.MakeGenericMethod(Reflection.ExtensionSet.Count,IEnumerable1のT(集約関数のSource.Type)),
    //                    集約関数のSource
    //                ),
    //                typeof(int?)
    //            );
    //            break;
    //        }
    //        case "max":
    //        case "min":{
    //            RefPeek.集約関数の内部か=true;
    //            var Body = this.ScalarExpression(x);
    //            RefPeek.集約関数の内部か=false;
    //            //Body=this.Convert値型をNullable参照型は想定しない(Body);
    //            var Body_Type =Int32に変換して(ref Body);
    //            if     (Body_Type==typeof(int     ))Result=共通非Nullable用(Body,FunctionName=="min"?Reflection.ExtensionSet.MinInt32_selector          :Reflection.ExtensionSet.MaxInt32_selector          );
    //            else if(Body_Type==typeof(long    ))Result=共通非Nullable用(Body,FunctionName=="min"?Reflection.ExtensionSet.MinInt64_selector          :Reflection.ExtensionSet.MaxInt64_selector          );
    //            else if(Body_Type==typeof(float   ))Result=共通非Nullable用(Body,FunctionName=="min"?Reflection.ExtensionSet.MinSingle_selector         :Reflection.ExtensionSet.MaxSingle_selector         );
    //            else if(Body_Type==typeof(double  ))Result=共通非Nullable用(Body,FunctionName=="min"?Reflection.ExtensionSet.MinDouble_selector         :Reflection.ExtensionSet.MaxDouble_selector         );
    //            else if(Body_Type==typeof(decimal ))Result=共通非Nullable用(Body,FunctionName=="min"?Reflection.ExtensionSet.MinDecimal_selector        :Reflection.ExtensionSet.MaxDecimal_selector        );
    //            else if(Body_Type==typeof(int?    ))Result=共通Nullable用  (Body,FunctionName=="min"?Reflection.ExtensionSet.MinNullableInt32_selector  :Reflection.ExtensionSet.MaxNullableInt32_selector  );
    //            else if(Body_Type==typeof(long?   ))Result=共通Nullable用  (Body,FunctionName=="min"?Reflection.ExtensionSet.MinNullableInt64_selector  :Reflection.ExtensionSet.MaxNullableInt64_selector  );
    //            else if(Body_Type==typeof(float?  ))Result=共通Nullable用  (Body,FunctionName=="min"?Reflection.ExtensionSet.MinNullableSingle_selector :Reflection.ExtensionSet.MaxNullableSingle_selector );
    //            else if(Body_Type==typeof(double? ))Result=共通Nullable用  (Body,FunctionName=="min"?Reflection.ExtensionSet.MinNullableDouble_selector :Reflection.ExtensionSet.MaxNullableDouble_selector );
    //            else if(Body_Type==typeof(decimal?))Result=共通Nullable用  (Body,FunctionName=="min"?Reflection.ExtensionSet.MinNullableDecimal_selector:Reflection.ExtensionSet.MaxNullableDecimal_selector);
    //            else if(Body_Type==typeof(object  )){
    //                var Method=FunctionName=="min"?Reflection.ExtensionSet.MinTSource_selector        :Reflection.ExtensionSet.MaxTSource_selector;
    //                var 作業配列 = this.作業配列;
    //                Method=作業配列.MakeGenericMethod(Method,集約関数のParameter.Type,typeof(object));
    //                var Lambda = Expressions.Expression.Lambda(Body,作業配列.Parameters設定(集約関数のParameter));
    //                Result=Expressions.Expression.Call(Method,集約関数のSource,Lambda);
    //            }
    //            else throw new NotSupportedException(Body_Type.FullName);
    //            break;
    //            Expressions.Expression 共通非Nullable用(Expressions.Expression Body,MethodInfo Method)=>this.ConvertNullable(共通Nullable用(Body,Method));
    //            Expressions.Expression 共通Nullable用(Expressions.Expression Body,MethodInfo Method) {
    //                var 作業配列 = this.作業配列;
    //                Method=作業配列.MakeGenericMethod(Method,集約関数のParameter.Type);
    //                var Lambda = Expressions.Expression.Lambda(Body,作業配列.Parameters設定(集約関数のParameter));
    //                return Expressions.Expression.Call(Method,集約関数のSource,Lambda);
    //            }
    //        }
    //        default:
    //            //Expressions.Expression.Dynamic(
    //            //    Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(
    //            //        CSharpBinderFlags.None,FunctionName,)
    //            //    Binder.Binder.
    //            //    変換_メソッド正規化_取得インライン不可能定数.DynamicReflection.)
    //            throw new NotSupportedException(FunctionName);
    //        static Type Int32に変換して(ref Expressions.Expression Body) {
    //            var Body_Type=Body.Type;
    //            if     (Body_Type==typeof(byte  ))Body=Expressions.Expression.Convert(Body,typeof(int ));
    //            else if(Body_Type==typeof(short ))Body=Expressions.Expression.Convert(Body,typeof(int ));
    //            else if(Body_Type==typeof(byte? ))Body=Expressions.Expression.Convert(Body,typeof(int?));
    //            else if(Body_Type==typeof(short?))Body=Expressions.Expression.Convert(Body,typeof(int?));
    //            return Body.Type;
    //        }
    //    }
    //    RefPeek.集約関数のParameter=集約関数のParameter;
    //    return Result;
    //}
//}
}
//615