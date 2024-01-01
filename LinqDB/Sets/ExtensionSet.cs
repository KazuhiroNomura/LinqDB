using System;
using Generic=System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using LinqDB.Sets.Exceptions;
using static LinqDB.Helpers.CommonLibrary;
using System.Collections.Generic;
// ReSharper disable ParameterTypeCanBeEnumerable.Global
// ReSharper disable RedundantAssignment
// ReSharper disable HeuristicUnreachableCode
// ReSharper disable PossibleNullReferenceException

// ReSharper disable LoopCanBeConvertedToQuery
namespace LinqDB.Sets;

/// <summary>
/// 集合演算用拡張メソッド。
/// </summary>
public static class ExtensionSet{
    /// <summary>
    /// 集合にアキュムレータ関数を適用します。 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="func">各要素に対して呼び出すアキュムレータ関数。</param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns>集約結果。</returns>
    public static TSource Aggregate<TSource>(this IEnumerable<TSource> source,Func<TSource,TSource,TSource> func) {
        using var Enumerator = source.GetEnumerator();
        if(!Enumerator.MoveNext())throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        var seed = Enumerator.Current;
        while(Enumerator.MoveNext())seed=func(seed,Enumerator.Current);
        return seed;
    }
    /// <summary>
    /// 集合にアキュムレータ関数を適用します。 指定されたシード値が最初のアキュムレータ値として使用されます。
    /// </summary>
    /// <param name="source"></param>
    /// <param name="seed">最初のアキュムレータ値。</param>
    /// <param name="func">各要素に対して呼び出すアキュムレータ関数。</param>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TAccumulate"></typeparam>
    /// <returns>集約結果。</returns>
    public static TAccumulate Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> source,TAccumulate seed,Func<TAccumulate,TSource,TAccumulate> func) {
        foreach(var a in source)seed=func(seed,a);
        return seed;
    }
    /// <summary>
    /// 集合にアキュムレータ関数を適用します。 指定したシード値は最初のアキュムレータ値として使用され、指定した関数は結果値の選択に使用されます。
    /// </summary>
    /// <param name="source"></param>
    /// <param name="seed">最初のアキュムレータ値。</param>
    /// <param name="func">各要素に対して呼び出すアキュムレータ関数。</param>
    /// <param name="resultSelector">最終的なアキュムレータ値を結果値に変換する関数。</param>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TAccumulate"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns>集約結果。</returns>
    public static TResult Aggregate<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source,TAccumulate seed,Func<TAccumulate,TSource,TAccumulate> func,Func<TAccumulate,TResult> resultSelector) {
        foreach(var a in source)seed=func(seed,a);
        return resultSelector(seed);
    }

    /// <summary>集合のすべての要素が条件を満たしているかどうかを判断します。</summary>
    /// <returns>指定された述語でソース 集合のすべての要素がテストに合格する場合は true。それ以外の場合は false。</returns>
    /// <param name="source">述語を適用する要素を格納している <see cref="IEnumerable{TSource}" />。</param>
    /// <param name="predicate">各要素が条件を満たしているかどうかをテストする関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    public static bool All<TSource>(this IEnumerable<TSource> source,Func<TSource,bool> predicate) {
        foreach(var a in source)
            if(!predicate(a))
                return false;
        return true;
    }
    /// <summary>集合に要素が含まれているかどうかを判断します。</summary>
    /// <returns>ソース 集合に要素が含まれている場合は true。それ以外の場合は false。</returns>
    /// <param name="source">空かどうかを確認する <see cref="IEnumerable{TSource}" />。</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any<T>(this IEnumerable<T> source)=>source.LongCount>0;

    /// <summary>null 許容の <see cref="decimal?" /> 値の集合の算術平均値を計算します。</summary>
    /// <returns>値の集合の算術平均値。ソース 集合が空か null 値のみを含む場合は null。</returns>
    /// <param name="source">平均値計算の対象となる null 許容の <see cref="decimal?" /> 値の集合。</param>
    /// <exception cref="OverflowException">集合内の要素の合計が <see cref="decimal.MaxValue" /> を超えています。</exception>
    public static decimal? Average(this IEnumerable<decimal?> source) {
        decimal Sum = 0;
        long Count = 0;
        foreach(var a in source){
            if(!a.HasValue) continue;
            Sum+=a.Value;
            Count++;
        }
        return Count==0 ? null:Sum/Count;
    }
    /// <summary>
    ///   <see cref="decimal" /> 値の集合の平均値を計算します。</summary>
    /// <returns>値の集合の平均値。</returns>
    /// <param name="source">平均値計算の対象となる <see cref="decimal" /> 値の集合。</param>
    /// <exception cref="InvalidOperationException">
    ///   <paramref name="source" /> に要素が含まれていません。</exception>
    /// <exception cref="OverflowException">集合内の要素の合計が <see cref="decimal.MaxValue" /> を超えています。</exception>
    public static decimal Average(this IEnumerable<decimal> source) {
        var Count = source.LongCount;
        if(Count==0)throw new InvalidOperationException(MethodBase.GetCurrentMethod()!.Name);
        decimal Sum = 0;
        foreach(var a in source)Sum+=a;
        return Sum/Count;
    }
    /// <summary>null 許容の <see cref="double?" /> 値の集合の算術平均を計算します。一般的に平均と言えばこれ。Selectorがないので重複除去してから集計する。</summary>
    /// <returns>値の集合の平均値。ソース 集合が空か null 値のみを含む場合は null。</returns>
    /// <param name="source">平均値計算の対象となる null 許容の <see cref="double?" /> 値の集合。</param>
    public static double? Average(this IEnumerable<double?> source) {
        double Sum = 0;
        long Count = 0;
        foreach(var a in source){
            if(!a.HasValue) continue;
            Sum+=a.Value;
            Count++;
        }
        return Count==0 ? null:Sum/Count;
    }
    /// <summary>
    ///   <see cref="double" /> 値の集合の算術平均を計算します。一般的に平均と言えばこれ。Selectorがないので重複除去してから集計する。</summary>
    /// <returns>値の集合の平均値。</returns>
    /// <param name="source">平均値計算の対象となる <see cref="double" /> 値の集合。</param>
    /// <exception cref="InvalidOperationException">
    ///   <paramref name="source" /> に要素が含まれていません。</exception>
    public static double Average(this IEnumerable<double> source) {
        var Count = source.LongCount;
        if(Count==0)throw new InvalidOperationException(MethodBase.GetCurrentMethod()!.Name);
        double Sum = 0;
        foreach(var a in source)Sum+=a;
        return Sum/Count;
    }
    /// <summary>null 許容の <see cref="float?" /> 値の集合の算術平均を計算します。一般的に平均と言えばこれ。Selectorがないので重複除去してから集計する。</summary>
    /// <returns>値の集合の平均値。ソース 集合が空か null 値のみを含む場合は null。</returns>
    /// <param name="source">平均値計算の対象となる null 許容の <see cref="float?" /> 値の集合。</param>
    public static float? Average(this IEnumerable<float?> source) {
        double Sum = 0;
        long Count = 0;
        foreach(var a in source){
            if(!a.HasValue) continue;
            Sum+=a.Value;
            Count++;
        }
        return Count==0 ? null:(float)(Sum/Count);
    }
    /// <summary>
    ///   <see cref="float" /> 値の集合の算術平均を計算します。一般的に平均と言えばこれ。Selectorがないので重複除去してから集計する。</summary>
    /// <returns>値の集合の平均値。</returns>
    /// <param name="source">平均値計算の対象となる <see cref="float" /> 値の集合。</param>
    /// <exception cref="InvalidOperationException">
    ///   <paramref name="source" /> に要素が含まれていません。</exception>
    public static float Average(this IEnumerable<float> source) {
        var Count = source.LongCount;
        if(Count==0)throw new InvalidOperationException(MethodBase.GetCurrentMethod()!.Name);
        double Sum = 0;
        foreach(var a in source)Sum+=a;
        return (float)(Sum/Count);
    }
    /// <summary>null 許容の <see cref="long?" /> 値の集合の算術平均を計算します。一般的に平均と言えばこれ。Selectorがないので重複除去してから集計する。</summary>
    /// <returns>値の集合の平均値。ソース 集合が空か null 値のみを含む場合は null。</returns>
    /// <param name="source">平均値計算の対象となる null 許容の <see cref="long?" /> 値の集合。</param>
    public static double? Average(this IEnumerable<long?> source) {
        long Sum = 0;
        long Count = 0;
        foreach(var a in source){
            if(!a.HasValue) continue;
            Sum+=a.Value;
            Count++;
        }
        return Count==0 ? null:(double)Sum/Count;
    }
    /// <summary>
    ///   <see cref="long" /> 値の集合の算術平均を計算します。一般的に平均と言えばこれ。Selectorがないので重複除去してから集計する。</summary>
    /// <returns>値の集合の平均値。</returns>
    /// <param name="source">平均値計算の対象となる <see cref="long" /> 値の集合。</param>
    /// <exception cref="InvalidOperationException">
    ///   <paramref name="source" /> に要素が含まれていません。</exception>
    public static double Average(this IEnumerable<long> source) {
        var Count = source.LongCount;
        if(Count==0)throw new InvalidOperationException(MethodBase.GetCurrentMethod()!.Name);
        long Sum = 0;
        foreach(var a in source)Sum+=a;
        return (double)Sum/Count;
    }
    /// <summary>null 許容の <see cref="int?" /> 値の集合の算術平均を計算します。一般的に平均と言えばこれ。Selectorがないので重複除去してから集計する。</summary>
    /// <returns>値の集合の平均値。ソース 集合が空か null 値のみを含む場合は null。</returns>
    /// <param name="source">平均値計算の対象となる null 許容の <see cref="int?" /> 値の集合。</param>
    public static double? Average(this IEnumerable<int?> source) {
        var Sum = 0;
        var Count = 0L;
        foreach(var a in source){
            if(!a.HasValue) continue;
            Sum+=a.Value;
            Count++;
        }
        return Count==0 ? null:(double)Sum/Count;
    }
    /// <summary>
    ///   <see cref="int" /> 値の集合の算術平均を計算します。一般的に平均と言えばこれ。Selectorがないので重複除去してから集計する。</summary>
    /// <returns>値の集合の平均値。</returns>
    /// <param name="source">平均値計算の対象となる <see cref="int" /> 値の集合。</param>
    /// <exception cref="InvalidOperationException">
    ///   <paramref name="source" /> に要素が含まれていません。</exception>
    public static double Average(this IEnumerable<int> source) {
        var Count = source.LongCount;
        if(Count==0)throw new InvalidOperationException(MethodBase.GetCurrentMethod()!.Name);
        var Sum = 0L;
        foreach(var a in source)Sum+=a;
        return (double)Sum/Count;
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する null 許容の <see cref="decimal" /> 値の集合の算術平均を計算します。一般的に平均と言えばこれ。Selectorがないので重複除去してから集計する。</summary>
    /// <returns>値の集合の平均値。ソース 集合が空か null 値のみを含む場合は null。</returns>
    /// <param name="source">平均値計算の対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <exception cref="OverflowException">集合内の要素の合計が <see cref="decimal.MaxValue" /> を超えています。</exception>
    public static decimal? Average<TSource>(this IEnumerable<TSource> source,Func<TSource,decimal?> selector) {
        decimal Sum = 0;
        var Count = 0L;
        foreach(var a in source) {
            var value = selector(a);
            if(!value.HasValue) continue;
            Sum+=value.Value;
            Count++;
        }
        return Count==0 ? null:Sum/Count;
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する <see cref="decimal" /> 値の集合の平均値を計算します。</summary>
    /// <returns>値の集合の平均値。</returns>
    /// <param name="source">平均値の計算に使用される値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <exception cref="InvalidOperationException">
    ///   <paramref name="source" /> に要素が含まれていません。</exception>
    /// <exception cref="OverflowException">集合内の要素の合計が <see cref="decimal.MaxValue" /> を超えています。</exception>
    public static decimal Average<TSource>(this IEnumerable<TSource> source,Func<TSource,decimal> selector) {
        var Count = source.LongCount;
        if(Count==0)throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        decimal Sum = 0;
        foreach(var a in source)Sum+=selector(a);
        return Sum/Count;
    }

    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する null 許容の <see cref="double?" /> 値の集合の平均値を計算します。</summary>
    /// <returns>値の集合の算術平均値。ソース 集合が空か null 値のみを含む場合は null。</returns>
    /// <param name="source">算術平均値計算の対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    public static double? Average<TSource>(this IEnumerable<TSource> source,Func<TSource,double?> selector) {
        double Sum = 0;
        long Count = 0;
        foreach(var a in source) {
            var SelectorNullable = selector(a);
            if(!SelectorNullable.HasValue) continue;
            Sum+=SelectorNullable.Value;
            Count++;
        }
        return Count==0 ? null:Sum/Count;
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する <see cref="double" /> 値の集合の平均値を計算します。</summary>
    /// <returns>値の集合の算術平均値。</returns>
    /// <param name="source">算術平均値計算の対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <exception cref="InvalidOperationException">
    ///   <paramref name="source" /> に要素が含まれていません。</exception>
    public static double Average<TSource>(this IEnumerable<TSource> source,Func<TSource,double> selector) {
        var Count = source.LongCount;
        if(Count==0)throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        double Sum = 0;
        foreach(var a in source)Sum+=selector(a);
        return Sum/Count;
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する null 許容の <see cref="float?" /> 値の集合の平均値を計算します。</summary>
    /// <returns>値の集合の算術平均値。ソース 集合が空か null 値のみを含む場合は null。</returns>
    /// <param name="source">算術平均値計算の対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    public static float? Average<TSource>(this IEnumerable<TSource> source,Func<TSource,float?> selector) {
        float Sum = 0;
        long Count = 0;
        foreach(var a in source) {
            var value = selector(a);
            if(!value.HasValue) continue;
            Sum+=value.Value;
            Count++;
        }
        return Count==0 ? null:(float)((double)Sum/Count);
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する <see cref="float" /> 値の集合の平均値を計算します。</summary>
    /// <returns>値の集合の算術平均値。</returns>
    /// <param name="source">算術平均値計算の対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <exception cref="InvalidOperationException">
    ///   <paramref name="source" /> に要素が含まれていません。</exception>
    public static float Average<TSource>(this IEnumerable<TSource> source,Func<TSource,float> selector) {
        var Count = source.LongCount;
        if(Count==0)throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        float Sum = 0;
        foreach(var a in source)Sum+=selector(a);
        return (float)((double)Sum/Count);
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する null 許容の <see cref="long?" /> 値の集合の平均値を計算します。</summary>
    /// <returns>値の集合の算術平均値。ソース 集合が空か null 値のみを含む場合は null。</returns>
    /// <param name="source">算術平均値計算の対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    public static double? Average<TSource>(this IEnumerable<TSource> source,Func<TSource,long?> selector) {
        var Sum = 0L;
        var Count = 0L;
        foreach(var a in source) {
            var SelectorNullable = selector(a);
            if(!SelectorNullable.HasValue) continue;
            Sum+=SelectorNullable.Value;
            Count++;
        }
        return Count==0 ? null:(double)Sum/Count;
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する <see cref="long" /> 値の集合の平均値を計算します。</summary>
    /// <returns>値の集合の算術平均値。</returns>
    /// <param name="source">算術平均値計算の対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <exception cref="InvalidOperationException">
    ///   <paramref name="source" /> に要素が含まれていません。</exception>
    public static double Average<TSource>(this IEnumerable<TSource> source,Func<TSource,long> selector) {
        var Count = source.LongCount;
        if(Count==0)throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        var Sum = 0L;
        foreach(var a in source)Sum+=selector(a);
        return (double)Sum/Count;
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する null 許容の <see cref="int?" /> 値の集合の平均値を計算します。</summary>
    /// <returns>値の集合の算術平均値。ソース 集合が空か null 値のみを含む場合は null。</returns>
    /// <param name="source">算術平均値計算の対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    public static double? Average<TSource>(this IEnumerable<TSource> source,Func<TSource,int?> selector) {
        var Sum = 0;
        var Count = 0L;
        foreach(var a in source) {
            var value = selector(a);
            if(!value.HasValue) continue;
            Sum+=value.Value;
            Count++;
        }
        return Count==0 ? null: (double)Sum/Count;
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する <see cref="int" /> 値の集合の平均値を計算します。</summary>
    /// <returns>値の集合の算術平均値。</returns>
    /// <param name="source">算術平均値計算の対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <exception cref="InvalidOperationException">
    ///   <paramref name="source" /> に要素が含まれていません。</exception>
    public static double Average<TSource>(this IEnumerable<TSource> source,Func<TSource,int> selector) {
        var Count = source.LongCount;
        if(Count==0)throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        var Sum = 0L;
        foreach(var a in source)Sum+=selector(a);
        return (double)Sum/Count;
    }
    private static (decimal[] Array, decimal 合計) 合計値を求める<TSource>(IEnumerable<TSource> source,Func<TSource,decimal> selector,long Count,MethodBase Method) {
        if(Count==0)throw シーケンスに要素が含まれていません(Method);
        var Array = new decimal[Count];
        decimal Sum = 0;
        var index = 0;
        foreach(var a in source) {
            var Value = selector(a);
            Array[index++]=Value;
            Sum+=Value;
        }
        return (Array, Sum);
    }
    private static (double[] Array, double 合計) 合計値を求める<TSource>(IEnumerable<TSource> source,Func<TSource,double> selector,long Count,MethodBase Method) {
        if(Count==0)throw シーケンスに要素が含まれていません(Method);
        var Array = new double[Count];
        double Sum = 0;
        var index = 0;
        foreach(var a in source) {
            var Value = selector(a);
            Array[index++]=Value;
            Sum+=Value;
        }
        return (Array, Sum);
    }

    /// <summary>
    /// 平均偏差
    /// </summary>
    /// <param name="source">平均偏差計算の対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>値の集合の平均偏差値。</returns>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <exception cref="InvalidOperationException">
    ///   <paramref name="source" /> に要素が含まれていません。</exception>
    public static double Avedev<TSource>(this IEnumerable<TSource> source,Func<TSource,double> selector) {
        //解答：算術平均値は (2+3+4+7+9) / 5=5 
        //平均偏差は (|2−5|+|3−5|+|4−5|+|7−5|+|9−5|)/5=(3+2+1+2+4) / 5=2.4
        var Count = source.LongCount;
        var (Array, Sum)=合計値を求める(source,selector,Count,MethodBase.GetCurrentMethod()!);
        double DoubleCount = Count;
        var Average = Sum/DoubleCount;
        Sum=0;
        foreach(var a in Array) {
            var Subtract = a-Average;
            if(Subtract<0)Subtract=-Subtract;
            Sum+=Subtract;
        }
        return Sum/DoubleCount;
    }
    /// <summary>
    ///   <see cref="IEnumerable" /> の要素を、指定した型に変換します。</summary>
    /// <returns>指定した型に変換されたソース 集合の各要素が格納されている <see cref="IEnumerable{TResult}" />。</returns>
    /// <param name="source">変換する要素が格納されている <see cref="IEnumerable" />。</param>
    /// <typeparam name="TResult">
    ///   <paramref name="source" /> の要素の変換後の型。</typeparam>
    /// <exception cref="InvalidCastException">集合の要素を <paramref>
    ///         <name>TResult</name>
    ///     </paramref>
    ///     型にキャストできません。</exception>
    public static IEnumerable<TResult> Cast<TResult>(this IEnumerable source) {
        var r = new Set<TResult>();
        long Count = 0;
        foreach(var a in source)
            if(r.InternalIsAdded((TResult)a!))
                Count++;
        r._LongCount=Count;
        return r;
    }
    /// <summary>既定の等値比較子を使用して、指定した要素が集合に含まれているかどうかを判断します。</summary>
    /// <returns>指定した値を持つ要素がソース 集合に含まれている場合は true。それ以外は false。</returns>
    /// <param name="source">値の検索対象となる集合。</param>
    /// <param name="value">集合内で検索する値。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    public static bool Contains<TSource>(this IEnumerable<TSource> source,TSource value)=>source is ImmutableSet<TSource> Set&&Set.InternalContains(value);
    /// <summary>指定された集合の要素を返します。集合が空の場合はシングルトン コレクションにある型パラメーターの既定値を返します。</summary>
    /// <returns>
    ///   <paramref name="source" /> が空の場合は<see cref="IEnumerable{TSource}" />。それ以外の場合は <paramref name="source" />。</returns>
    /// <param name="source">集合が空の場合に、指定された値を返す集合。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    public static IEnumerable<TSource?> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source){
        if(source.LongCount>0) return source;
        return new Set<TSource?>{_LongCount=1,TreeRoot={R=new Set<TSource?>.TreeNodeT(null){_LinkedNodeItem=new Set<TSource?>.LinkedNodeItemT(default)}}};
    }
    //=> (IEnumerable<TSource?>)DefaultIfEmpty(source,default!);
    /// <summary>指定された集合の要素を返します。集合が空の場合はシングルトン コレクションにある型パラメーターの既定値を返します。</summary>
    /// <returns>
    ///   <paramref name="source" /> が空の場合は <paramref name="defaultValue" /> が格納されている <see cref="IEnumerable{TSource}" />。それ以外の場合は <paramref name="source" />。</returns>
    /// <param name="source">集合が空の場合に、指定された値を返す集合。</param>
    /// <param name="defaultValue">集合が空の場合に返す値。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source,TSource defaultValue)=>source.LongCount>0
        ?source:new Set<TSource> {
            _LongCount=1,
            TreeRoot={
                R = new Set<TSource>.TreeNodeT(null){
                    _LinkedNodeItem = new Set<TSource>.LinkedNodeItemT(defaultValue)
                }
            }
        };

    /// <summary>
    /// 最適化Optimizer内で使用されるデータ構造。
    /// </summary>
    /// <param name="source">ハッシュインデックスを作りたい入力</param>
    /// <param name="keySelector">HashCodeを求める元のキー値</param>
    /// <typeparam name="TSource">入力集合型</typeparam>
    /// <typeparam name="TKey">キー型</typeparam>
    /// <returns></returns>
    // ReSharper disable once ParameterTypeCanBeEnumerable.Global
    public static ILookup<TKey, TSource>ToLookup<TSource, TKey>(this IEnumerable<TSource> source,Func<TSource,TKey> keySelector)=>
        ToLookup(source,keySelector,EqualityComparer<TKey>.Default);
    public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer){
        var r = new SetGroupingSet<TKey,TSource>(comparer);
        foreach(var a in source)r.AddKeyValue(keySelector(a),a);
        return r;
    }

    public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector) =>
        ToLookup(source, keySelector, elementSelector, EqualityComparer<TKey>.Default);

    public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey>? comparer){
        var r = new SetGroupingSet<TKey,TElement>(comparer);
        foreach(var a in source)r.AddKeyValue(keySelector(a),elementSelector(a));
        return r;
    }
    /// <summary>
    /// 和集合。重複したら例外。
    /// </summary>
    /// <param name="source"></param>
    /// <param name="second"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns>和集合。</returns>
    /// <exception cref="OneTupleException"></exception>
    public static IEnumerable<TSource> DUnion<TSource>(this IEnumerable<TSource> source,IEnumerable<TSource> second) {
        var r = new Set<TSource>(source);
        long Count = 0;
        foreach(var a in second) {
            if(!r.InternalIsAdded(a)) throw new OneTupleException(MethodBase.GetCurrentMethod()!.Name);
            Count++;
        }
        r._LongCount+=Count;
        return r;
    }
    /// <summary>既定の等値比較子を使用して値を比較することにより、2 つの集合の差集合を生成します。</summary>
    /// <returns>2 つの集合の要素の差集合が格納されている集合。</returns>
    /// <param name="first">
    ///   <paramref name="second" /> には含まれていないが、返される要素を含む <see cref="IEnumerable{TSource}" />。</param>
    /// <param name="second">最初の集合にも含まれ、返された集合からは削除される要素を含む <see cref="IEnumerable{TSource}" />。</param>
    /// <typeparam name="TSource">入力集合の要素の型。</typeparam>
    public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first,IEnumerable<TSource> second) {
        var r = new Set<TSource>(first);
        r.ExceptWith(second);
        return r;
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する <see cref="double" /> 値の集合の平均値を計算します。</summary>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns>値の集合の幾何平均値。</returns>
    public static double? Geomean<TSource>(this IEnumerable<TSource> source,Func<TSource,double?> selector) {
        //A,B,Cのデータがあった場合、
        //理論上は、
        //Math.Pow(
        //  2,
        //  (Math.Log2(A)+Math.Log2(B)+Math.Log2(C))/3,
        //)
        double Sum = 0;
        long Count = 0;
        foreach(var a in source) {
            var SelectNullable = selector(a);
            if(!SelectNullable.HasValue) continue;
            Count++;
            Sum+=Math.Log10(SelectNullable.Value);
        }
        return Count==0 ? null:Math.Pow(10,Sum/Count);
    }
    /// <summary>
    /// 相乗平均。幾何平均ともいう。成長率、増加率の平均に適している。
    /// </summary>
    /// <param name="source">相乗平均値計算の対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>値の集合の相乗平均値。</returns>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <exception cref="InvalidOperationException">
    ///   <paramref name="source" /> に要素が含まれていません。</exception>
    public static double Geomean<TSource>(this IEnumerable<TSource> source,Func<TSource,double> selector) {
        var Count = source.LongCount;
        if(Count==0)throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        double Sum = 0;
        //A,B,Cのデータがあった場合、
        //理論上は、
        //Math.Pow(
        //  2,
        //  (Math.Log2(A)+Math.Log2(B)+Math.Log2(C))/3,
        //)
        foreach(var a in source)Sum+=Math.Log10(selector(a));
        return Math.Pow(10,Sum/Count);
    }
    /// <summary>指定されたキー セレクター関数に従って集合の要素をグループ化し、指定された関数を使用して各グループの要素を射影します。</summary>
    /// <returns>C# では IEnumerable&lt;ImmutableGroupingSet&lt;TKey,TElement>>、Visual Basic では IEnumerable(Of IGrouping(Of TKey,TElement))。ここでは、各 <see cref="System.Linq.IGrouping{TKey,TElement}" /> オブジェクトに、<paramref>
    ///<name>TElement</name>
    ///</paramref>型のオブジェクトのコレクション、およびキーが格納されています。</returns>
    /// <param name="source">グループ化する要素を含む <see cref="IEnumerable{TSource}" />。</param>
    /// <param name="keySelector">各要素のキーを抽出する関数。</param>
    /// <param name="elementSelector">ソースの各要素を <see cref="Grouping{TKey,TElement}" /> の要素に割り当てる関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <typeparam name="TKey">
    ///   <paramref name="keySelector" /> によって返されるキーの型。</typeparam>
    /// <typeparam name="TElement">
    ///   <see cref="IGrouping{TKey,TElement}" /> の要素の型。</typeparam>
    public static IEnumerable<IGrouping<TKey,TElement>> GroupBy<TSource, TKey,TElement>(this IEnumerable<TSource> source,Func<TSource,TKey> keySelector,Func<TSource,TElement> elementSelector) {
        var r = new SetGroupingSet<TKey,TElement>();
        foreach(var a in source)r.AddKeyValue(keySelector(a),elementSelector(a));
        Debug.Assert(r.LongCount<=source.LongCount);
        return r;
    }
    /// <summary>指定されたキー セレクター関数に従って集合の要素をグループ化します。</summary>
    /// <returns>C# では IEnumerable&lt;ImmutableGroupingSet&lt;TKey, TSource>>、Visual Basic では IEnumerable(Of GroupingSet(Of TKey, TSource))。ここでは、各 <see cref="System.Linq.IGrouping{TKey,TElement}" /> オブジェクトに、オブジェクトの集合、およびキーが格納されています。</returns>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <typeparam name="TKey">
    ///   <paramref name="keySelector" /> によって返されるキーの型。</typeparam>
    /// <typeparam name="TResult">
    ///   <paramref name="resultSelector" /> によって返される結果値の型。</typeparam>
    public static IEnumerable<TResult> GroupBy<TSource,TKey, TResult>(this IEnumerable<TSource> source,Func<TSource,TKey> keySelector,Func<TKey,IEnumerable<TSource>,TResult> resultSelector) {
        var r = new SetGroupingSet<TKey,TSource>();
        foreach(var a in source)r.AddKeyValue(keySelector(a),a);
        Debug.Assert(r.LongCount<=source.LongCount);
        var r0 = new Set<TResult>();
        foreach(var a in r)r0.Add(resultSelector(a.Key,a));
        return r0;
    }
    /// <summary>指定されたキー セレクター関数に従って集合の要素をグループ化します。</summary>
    /// <returns>C# では IEnumerable&lt;ImmutableGroupingSet&lt;TKey, TSource>>、Visual Basic では IEnumerable(Of GroupingSet(Of TKey, TSource))。ここでは、各 <see cref="System.Linq.IGrouping{TKey,TElement}" /> オブジェクトに、オブジェクトの集合、およびキーが格納されています。</returns>
    /// <param name="source">グループ化する要素を含む <see cref="IEnumerable{TSource}" />。</param>
    /// <param name="keySelector">各要素のキーを抽出する関数。</param>
    /// <param name="elementSelector"></param>
    /// <param name="resultSelector"></param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <typeparam name="TKey">
    ///   <paramref name="keySelector" /> によって返されるキーの型。</typeparam>
    /// <typeparam name="TElement">
    ///   <see cref="IGrouping{TKey,TElement}" /> の要素の型。</typeparam>
    /// <typeparam name="TResult">
    ///   <paramref name="resultSelector" /> によって返される結果値の型。</typeparam>
    public static IEnumerable<TResult> GroupBy<TSource, TKey,TElement, TResult>(this IEnumerable<TSource> source,Func<TSource,TKey> keySelector,Func<TSource,TElement> elementSelector,Func<TKey,IEnumerable<TElement>,TResult> resultSelector) {
        var r = new SetGroupingSet<TKey,TElement>();
        foreach(var a in source)r.AddKeyValue(keySelector(a),elementSelector(a));
        Debug.Assert(r.LongCount<=source.LongCount);
        var r0 = new Set<TResult>();
        foreach(var a in r)r0.Add(resultSelector(a.Key,a));
        return r0;
    }
    /// <summary>指定されたキー セレクター関数に従ってシーケンスの要素をグループ化し、各グループとそのキーから結果値を作成します。</summary>
    /// <returns>C# では IEnumerable&lt;IGrouping&lt;TKey, TSource>>、Visual Basic では IEnumerable(Of IGrouping(Of TKey, TSource))。ここでは、各 <see cref="System.Linq.IGrouping{TKey,TElement}" /> オブジェクトに、オブジェクトの集合、およびキーが格納されています。</returns>
    /// <param name="source">グループ化する要素を含む <see cref="IEnumerable{TSource}" />。</param>
    /// <param name="keySelector">各要素のキーを抽出する関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <typeparam name="TKey">
    ///   <paramref name="keySelector" /> によって返されるキーの型。</typeparam>
    public static IEnumerable<IGrouping<TKey,TSource>> GroupBy<TSource, TKey>(this IEnumerable<TSource> source,Func<TSource,TKey> keySelector) {
        var r = new SetGroupingSet<TKey,TSource>();
        foreach(var a in source)r.AddKeyValue(keySelector(a),a);
        Debug.Assert(r.LongCount<=source.LongCount);
        return r;
    }
    /// <summary>キーが等しいかどうかに基づいて 2 つの集合の要素を相互に関連付け、その結果をグループ化します。</summary>
    /// <returns>2つの集合に対してグループ化結合を実行して取得する、TResult型の要素が格納されている <see cref="IEnumerable{TResult}" />。</returns>
    /// <param name="outer">結合する最初の集合。</param>
    /// <param name="inner">最初の集合に結合する集合。</param>
    /// <param name="outerKeySelector">最初の集合の各要素から結合キーを抽出する関数。</param>
    /// <param name="innerKeySelector">2番目の集合の各要素から結合キーを抽出する関数。</param>
    /// <param name="resultSelector">最初の集合の要素と、2番目の集合の一致する要素のコレクションから結果の要素を作成する関数。</param>
    /// <typeparam name="TOuter">最初の集合の要素の型。</typeparam>
    /// <typeparam name="TInner">2番目の集合の要素の型。</typeparam>
    /// <typeparam name="TKey">キー セレクター関数によって返されるキーの型。</typeparam>
    /// <typeparam name="TResult">結果の要素の型。</typeparam>
    public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer,IEnumerable<TInner> inner,Func<TOuter,TKey> outerKeySelector,Func<TInner,TKey> innerKeySelector,Func<TOuter,IEnumerable<TInner>,TResult> resultSelector) {
        var EmptySet = ImmutableSet<TInner>.EmptySet;
        var Dictionary = inner.ToLookup(innerKeySelector);
        //IEnumerable<TInner> innerValue = null!;
        var r = new Set<TResult>();
        long count = 0;
        foreach(var a in outer)
            if(r.InternalIsAdded(resultSelector(a,Dictionary[outerKeySelector(a)])))
                count++;
        //if(
        //    r.InternalAdd(
        //        Dictionary.TryGetValue(outerKeySelector(a),ref innerValue)
        //            ? resultSelector(a,innerValue)
        //            : resultSelector(a,EmptySet)
        //    )
        //)count++;
        Debug.Assert(outer.Count==count);
        r._LongCount=count;
        Debug.Assert(r.LongCount<=outer.LongCount);
        return r;
    }
    /// <summary>
    /// 調和平均。行きは40km/h、帰りは60km/h。その平均速度を求めるときに使える。
    /// </summary>
    /// <param name="source">操作対象</param>
    /// <param name="selector">調和平均の1要素の計算式</param>
    /// <returns>調和平均値</returns>
    public static decimal? Harmean<TSource>(this IEnumerable<TSource> source,Func<TSource,decimal?> selector) {
        decimal Sum = 0;
        long Count = 0;
        foreach(var a in source) {
            var SelecterNullable = selector(a);
            if(!SelecterNullable.HasValue) continue;
            Count++;
            Sum+=1/SelecterNullable.Value;
        }
        return Count==0 ? null : Count/Sum;
    }
    /// <summary>
    /// 調和平均。行きは40km/h、帰りは60km/h。その平均速度を求めるときに使える。
    /// </summary>
    /// <param name="source">調和平均値計算の対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>値の集合の調和平均値。</returns>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <exception cref="InvalidOperationException">
    ///   <paramref name="source" /> に要素が含まれていません。</exception>
    public static decimal Harmean<TSource>(this IEnumerable<TSource> source,Func<TSource,decimal> selector) {
        var Count = source.LongCount;
        if(Count==0)throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        decimal Sum = 0;
        foreach(var a in source)Sum+=1/selector(a);
        return Count/Sum;
    }
    /// <summary>
    /// 調和平均。行きは40km/h、帰りは60km/h。その平均速度を求めるときに使える。
    /// </summary>
    /// <param name="source">操作対象</param>
    /// <param name="selector">調和平均の1要素の計算式</param>
    /// <returns>調和平均値</returns>
    public static double? Harmean<TSource>(this IEnumerable<TSource> source,Func<TSource,double?> selector) {
        double Sum = 0;
        long Count = 0;
        foreach(var a in source) {
            var SelecterNullable = selector(a);
            if(!SelecterNullable.HasValue) continue;
            Count++;
            Sum+=1/SelecterNullable.Value;
        }
        return Count==0 ? null: Count/Sum;
    }
    /// <summary>
    /// 調和平均。行きは40km/h、帰りは60km/h。その平均速度を求めるときに使える。
    /// </summary>
    /// <param name="source">調和平均値計算の対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>値の集合の調和平均値。</returns>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <exception cref="InvalidOperationException">
    ///   <paramref name="source" /> に要素が含まれていません。</exception>
    public static double Harmean<TSource>(this IEnumerable<TSource> source,Func<TSource,double> selector) {
        var Count = source.LongCount;
        if(Count==0)throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        double Sum = 0;
        foreach(var a in source)Sum+=1/selector(a);
        return Count/Sum;
    }
    /// <summary>既定の等値比較子を使用して値を比較することにより、2 つの集合の積集合を生成します。</summary>
    /// <returns>2 つの集合の積集合を構成する要素が格納されている集合。</returns>
    /// <param name="first">
    ///   <paramref name="second" /> にも含まれる、返される一意の要素を含む <see cref="IEnumerable{TSource}" />。</param>
    /// <param name="second">最初の集合にも含まれる、返される一意の要素を含む <see cref="IEnumerable{TSource}" />。</param>
    /// <typeparam name="TSource">入力集合の要素の型。</typeparam>
    public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first,IEnumerable<TSource> second) {
        var r = new Set<TSource>(first);
        r.IntersectWith(second);
        return r;
    }
    /// <summary>
    /// 一致するキーに基づいて 2 つの集合の要素を相互に関連付けます。キーの比較には既定の等値比較子が使用されます。
    /// </summary>
    /// <returns>2 つの集合に対して内部結合を実行して取得する、TResult型の要素が格納されている <see cref="IEnumerable{TResult}" />。</returns>
    /// <param name="outer">結合する最初の集合。</param>
    /// <param name="inner">最初の集合に結合する集合。</param>
    /// <param name="outerKeySelector">最初の集合の各要素から結合キーを抽出する関数。</param>
    /// <param name="innerKeySelector">2番目の集合の各要素から結合キーを抽出する関数。</param>
    /// <param name="resultSelector">一致する 2 つの要素から結果の要素を作成する関数。</param>
    /// <typeparam name="TOuter">最初の集合の要素の型。</typeparam>
    /// <typeparam name="TInner">2番目の集合の要素の型。</typeparam>
    /// <typeparam name="TKey">キー セレクター関数によって返されるキーの型。</typeparam>
    /// <typeparam name="TResult">結果の要素の型。</typeparam>
    /// <example>SetA.Join(SetB,o=>o+o,i=>i+i,(o,i)=>new{o,i})</example>
    public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer,IEnumerable<TInner> inner,Func<TOuter,TKey> outerKeySelector,Func<TInner,TKey> innerKeySelector,Func<TOuter,TInner,TResult> resultSelector) {
        var r = new Set<TResult>();
        long count = 0;
        if(inner.LongCount<outer.LongCount) {
            var outerDictionary = outer.ToLookup(outerKeySelector);
            foreach(var innerValue in inner)
                foreach(var outerValue in outerDictionary[innerKeySelector(innerValue)])
                    if(r.InternalIsAdded(resultSelector(outerValue,innerValue)))count++;
        } else {
            var innerDictionary = inner.ToLookup(innerKeySelector);
            foreach(var outerValue in outer)
                    foreach(var innerValue in innerDictionary[outerKeySelector(outerValue)])
                        if(r.InternalIsAdded(resultSelector(outerValue,innerValue)))count++;
        }
        r._LongCount=count;
        return r;
        //var r = new Set<TResult>();
        //long count = 0;
        //if(inner.LongCount<outer.LongCount) {
        //    var outerDictionary = outer.ToLookup(outerKeySelector);
        //    IEnumerable<TOuter> outer2 = null!;
        //    foreach(var innerValue in inner)
        //        if(outerDictionary.TryGetValue(innerKeySelector(innerValue),ref outer2))
        //            foreach(var outerValue in outer2)
        //                if(r.InternalAdd(resultSelector(outerValue,innerValue)))count++;
        //} else {
        //    var innerDictionary = inner.ToLookup(innerKeySelector);
        //    IEnumerable<TInner> inner2 = null!;
        //    foreach(var outerValue in outer)
        //        if(innerDictionary.TryGetValue(outerKeySelector(outerValue),ref inner2))
        //            foreach(var innerValue in inner2)
        //                if(r.InternalAdd(resultSelector(outerValue,innerValue)))count++;
        //}
        //r._LongCount=count;
        //return r;
    }
    /// <summary>集合内の要素の合計数を表す <see cref="int" /> を返します。</summary>
    /// <returns>ソース 集合の要素数。</returns>
    /// <param name="source">カウントする要素が格納されている <see cref="IEnumerable{TSource}" />。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    public static int Count<TSource>(this IEnumerable<TSource> source) => (int)source.LongCount;
    /// <summary>集合内の要素の合計数を表す <see cref="long" /> を返します。</summary>
    /// <returns>ソース 集合の要素数。</returns>
    /// <param name="source">カウントする要素が格納されている <see cref="IEnumerable{TSource}" />。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    public static long LongCount<TSource>(this IEnumerable<TSource> source) => source.LongCount;
    /// <summary>ジェネリック 集合の最大値を返します。</summary>
    /// <returns>集合の最大値。</returns>
    /// <param name="source">最大値を確認する対象となる値の集合。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    public static TSource Max<TSource>(this IEnumerable<TSource> source) {
        using var Enumerator = source.GetEnumerator();
        if(!Enumerator.MoveNext()) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        var Result = Enumerator.Current;
        var Default =Comparer<TSource>.Default;
        while(Enumerator.MoveNext())
            if(Default.Compare(Result,Enumerator.Current)<0)Result=Enumerator.Current;
        return Result;
    }
    /// <summary>ジェネリック 集合の最小値を返します。</summary>
    /// <returns>集合の最小値。</returns>
    /// <param name="source">最小値を確認する対象となる値の集合。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    public static TSource Min<TSource>(this IEnumerable<TSource> source) {
        if(!PrivateMaxMin(source,out var Enumerator)) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        var Default =Comparer<TSource>.Default;
        var Result = Enumerator.Current;
        while(Enumerator.MoveNext())
            if(Default.Compare(Result,Enumerator.Current)>0)Result=Enumerator.Current;
        return Result;
    }
    private static (IEnumerator<TSource?> Enumerator, TSource? Result) PrivateMaxMin<TSource>(this IEnumerable<TSource?> source) where TSource : struct {
        TSource? Result = null;
        var Enumerator = source.GetEnumerator();
        while(Enumerator.MoveNext()) {
            var Item = Enumerator.Current;
            if(!Item.HasValue) continue;
            Result=Item;
            break;
        }
        return (Enumerator, Result);
    }
    /// <summary>decimal? 値の集合の最大値を返します。</summary>
    /// <returns>集合の最大値に対応する decimal? 型の値。</returns>
    /// <param name="source">最大値を確認する対象となる decimal? 値の集合。</param>
    public static decimal? Max(this IEnumerable<decimal?> source) {
        var (Enumerator, Result)=PrivateMaxMin(source);
        while(Enumerator.MoveNext()) {
            var Item = Enumerator.Current;
            if(Result<Item)Result=Item;
        }
        return Result;
    }
    /// <summary>decimal? 値の集合の最小値を返します。</summary>
    /// <returns>集合の最小値に対応する decimal? 型の値。</returns>
    /// <param name="source">最小値を確認する対象となる decimal? 値の集合。</param>
    public static decimal? Min(this IEnumerable<decimal?> source) {
        var (Enumerator, Result)=PrivateMaxMin(source);
        while(Enumerator.MoveNext()) {
            var Item = Enumerator.Current;
            if(Result>Item)Result=Item;
        }
        return Result;
    }
    /// <summary>double? 値の集合の最大値を返します。</summary>
    /// <returns>集合の最大値に対応する double? 型の値。</returns>
    /// <param name="source">最大値を確認する対象となる double? 値の集合。</param>
    public static double? Max(this IEnumerable<double?> source) {
        var (Enumerator, Result)=PrivateMaxMin(source);
        while(Enumerator.MoveNext()) {
            var Item = Enumerator.Current;
            if(Result<Item)Result=Item;
        }
        return Result;
    }
    /// <summary>double? 値の集合の最小値を返します。</summary>
    /// <returns>集合の最小値に対応する double? 型の値。</returns>
    /// <param name="source">最小値を確認する対象となる double? 値の集合。</param>
    public static double? Min(this IEnumerable<double?> source) {
        var (Enumerator, Result)=PrivateMaxMin(source);
        while(Enumerator.MoveNext()) {
            var Item = Enumerator.Current;
            if(Result>Item)Result=Item;
        }
        return Result;
    }
    /// <summary>float? 値の集合の最大値を返します。</summary>
    /// <returns>集合の最大値に対応する float? 型の値。</returns>
    /// <param name="source">最大値を確認する対象となる float? 値の集合。</param>
    public static float? Max(this IEnumerable<float?> source) {
        var (Enumerator, Result)=PrivateMaxMin(source);
        while(Enumerator.MoveNext()) {
            var Item = Enumerator.Current;
            if(Result<Item)Result=Item;
        }
        return Result;
    }
    /// <summary>float? 値の集合の最小値を返します。</summary>
    /// <returns>集合の最小値に対応する float? 型の値。</returns>
    /// <param name="source">最小値を確認する対象となる float? 値の集合。</param>
    public static float? Min(this IEnumerable<float?> source) {
        var (Enumerator, Result)=PrivateMaxMin(source);
        while(Enumerator.MoveNext()) {
            var Item = Enumerator.Current;
            if(Result>Item)Result=Item;
        }
        return Result;
    }
    /// <summary>long? 値の集合の最大値を返します。</summary>
    /// <returns>集合の最大値に対応する long? 型の値。</returns>
    /// <param name="source">最大値を確認する対象となる long? 値の集合。</param>
    public static long? Max(this IEnumerable<long?> source) {
        var (Enumerator, Result)=PrivateMaxMin(source);
        while(Enumerator.MoveNext()) {
            var Item = Enumerator.Current;
            if(Result<Item)Result=Item;
        }
        return Result;
    }
    /// <summary>long? 値の集合の最小値を返します。</summary>
    /// <returns>集合の最小値に対応する long? 型の値。</returns>
    /// <param name="source">最小値を確認する対象となる long? 値の集合。</param>
    public static long? Min(this IEnumerable<long?> source) {
        var (Enumerator, Result)=PrivateMaxMin(source);
        while(Enumerator.MoveNext()) {
            var Item = Enumerator.Current;
            if(Result>Item)Result=Item;
        }
        return Result;
    }
    /// <summary>int? 値の集合の最大値を返します。</summary>
    /// <returns>集合の最大値に対応する int? 型の値。</returns>
    /// <param name="source">最大値を確認する対象となる int? 値の集合。</param>
    public static int? Max(this IEnumerable<int?> source) {
        var (Enumerator, Result)=PrivateMaxMin(source);
        while(Enumerator.MoveNext()) {
            var Item = Enumerator.Current;
            if(Result<Item)Result=Item;
        }
        return Result;
    }
    /// <summary>int? 値の集合の最小値を返します。</summary>
    /// <returns>集合の最小値に対応する int? 型の値。</returns>
    /// <param name="source">最小値を確認する対象となる int? 値の集合。</param>
    public static int? Min(this IEnumerable<int?> source) {
        var (Enumerator, Result)=PrivateMaxMin(source);
        while(Enumerator.MoveNext()) {
            var Item = Enumerator.Current;
            if(Result>Item)Result=Item;
        }
        return Result;
    }
    private static bool PrivateMaxMin<TSource>(this IEnumerable<TSource> source,out IEnumerator<TSource> Enumerator) {
        Enumerator = source.GetEnumerator();
        return Enumerator.MoveNext();
    }
    /// <summary>decimal 値の集合の最大値を返します。</summary>
    /// <returns>集合の最大値に対応する decimal 型の値。</returns>
    /// <param name="source">最大値を確認する対象となる decimal 値の集合。</param>
    public static decimal Max(this IEnumerable<decimal> source) {
        if(!PrivateMaxMin(source,out var Enumerator)) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        var Result= Enumerator.Current;
        while(Enumerator.MoveNext()) {
            var Item = Enumerator.Current;
            if(Result<Item)Result=Item;
        }
        return Result;
    }
    /// <summary>decimal 値の集合の最小値を返します。</summary>
    /// <returns>集合の最小値に対応する decimal 型の値。</returns>
    /// <param name="source">最小値を確認する対象となる decimal 値の集合。</param>
    public static decimal Min(this IEnumerable<decimal> source) {
        if(!PrivateMaxMin(source,out var Enumerator)) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        var Result = Enumerator.Current;
        while(Enumerator.MoveNext()) {
            var Item = Enumerator.Current;
            if(Result>Item)Result=Item;
        }
        return Result;
    }
    /// <summary>double 値の集合の最大値を返します。</summary>
    /// <returns>集合の最大値に対応する double 型の値。</returns>
    /// <param name="source">最大値を確認する対象となる double 値の集合。</param>
    public static double Max(this IEnumerable<double> source) {
        if(!PrivateMaxMin(source,out var Enumerator)) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        var Result = Enumerator.Current;
        while(Enumerator.MoveNext()) {
            var Item = Enumerator.Current;
            if(Result<Item)Result=Item;
        }
        return Result;
    }
    /// <summary>double 値の集合の最小値を返します。</summary>
    /// <returns>集合の最小値に対応する double 型の値。</returns>
    /// <param name="source">最小値を確認する対象となる double 値の集合。</param>
    public static double Min(this IEnumerable<double> source) {
        if(!PrivateMaxMin(source,out var Enumerator)) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        var Result = Enumerator.Current;
        while(Enumerator.MoveNext()) {
            var Item = Enumerator.Current;
            if(Result>Item)Result=Item;
        }
        return Result;
    }
    /// <summary>float 値の集合の最大値を返します。</summary>
    /// <returns>集合の最大値に対応する float 型の値。</returns>
    /// <param name="source">最大値を確認する対象となる float 値の集合。</param>
    public static float Max(this IEnumerable<float> source) {
        if(!PrivateMaxMin(source,out var Enumerator)) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        var Result = Enumerator.Current;
        while(Enumerator.MoveNext()) {
            var Item = Enumerator.Current;
            if(Result<Item)Result=Item;
        }
        return Result;
    }
    /// <summary>float 値の集合の最小値を返します。</summary>
    /// <returns>集合の最小値に対応する float 型の値。</returns>
    /// <param name="source">最小値を確認する対象となる float 値の集合。</param>
    public static float Min(this IEnumerable<float> source) {
        if(!PrivateMaxMin(source,out var Enumerator)) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        var Result = Enumerator.Current;
        while(Enumerator.MoveNext()) {
            var Item = Enumerator.Current;
            if(Result>Item)Result=Item;
        }
        return Result;
    }
    /// <summary>long 値の集合の最大値を返します。</summary>
    /// <returns>集合の最大値に対応する long 型の値。</returns>
    /// <param name="source">最大値を確認する対象となる long 値の集合。</param>
    public static long Max(this IEnumerable<long> source) {
        if(!PrivateMaxMin(source,out var Enumerator)) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        var Result = Enumerator.Current;
        while(Enumerator.MoveNext()) {
            var Item = Enumerator.Current;
            if(Result<Item)Result=Item;
        }
        return Result;
    }
    /// <summary>long 値の集合の最小値を返します。</summary>
    /// <returns>集合の最小値に対応する long 型の値。</returns>
    /// <param name="source">最小値を確認する対象となる long 値の集合。</param>
    public static long Min(this IEnumerable<long> source) {
        if(!PrivateMaxMin(source,out var Enumerator)) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        var Result = Enumerator.Current;
        while(Enumerator.MoveNext()) {
            var Item = Enumerator.Current;
            if(Result>Item)Result=Item;
        }
        return Result;
    }
    /// <summary>int 値の集合の最大値を返します。</summary>
    /// <returns>集合の最大値に対応する int 型の値。</returns>
    /// <param name="source">最大値を確認する対象となる int 値の集合。</param>
    public static int Max(this IEnumerable<int> source) {
        if(!PrivateMaxMin(source,out var Enumerator)) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        var Result = Enumerator.Current;
        while(Enumerator.MoveNext()) {
            var Item = Enumerator.Current;
            if(Result<Item)Result=Item;
        }
        return Result;
    }
    /// <summary>int 値の集合の最小値を返します。</summary>
    /// <returns>集合の最小値に対応する int 型の値。</returns>
    /// <param name="source">最小値を確認する対象となる int 値の集合。</param>
    public static int Min(this IEnumerable<int> source) {
        if(!PrivateMaxMin(source,out var Enumerator)) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        var Result = Enumerator.Current;
        while(Enumerator.MoveNext()) {
            var Item = Enumerator.Current;
            if(Result>Item)Result=Item;
        }
        return Result;
    }
    /// <summary>ジェネリック 集合の各要素に対して変換関数を呼び出し、結果の最大値を返します。</summary>
    /// <returns>集合の最大値。</returns>
    /// <param name="source">最大値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <typeparam name="TResult">
    ///   <paramref name="selector" /> によって返される値の型。</typeparam>
    public static TResult Max<TSource, TResult>(this IEnumerable<TSource> source,Func<TSource,TResult> selector) {
        using var Enumerator = source.GetEnumerator();
        var Default =Comparer<TResult>.Default;
        while(Enumerator.MoveNext()) {
            var Item0 = selector(Enumerator.Current);
            while(Enumerator.MoveNext()) {
                var Item1 = selector(Enumerator.Current);
                if(Default.Compare(Item0,Item1)<0)Item0=Item1;
            }
            return Item0;
        }
        throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
    }
    /// <summary>ジェネリック 集合の各要素に対して変換関数を呼び出し、結果の最大値を返します。</summary>
    /// <returns>集合の最大値。</returns>
    /// <param name="source">最大値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <typeparam name="TResult">
    ///   <paramref name="selector" /> によって返されるTResult?の型。</typeparam>
    public static TResult? Max<TSource, TResult>(this IEnumerable<TSource> source,Func<TSource,TResult?> selector)where TResult:struct {
        using var Enumerator = source.GetEnumerator();
        var Default =Comparer<TResult>.Default;
        while(Enumerator.MoveNext()) {
            var Item0 = selector(Enumerator.Current);
            if(!Item0.HasValue) continue;
            while(Enumerator.MoveNext()) {
                var Item1 = selector(Enumerator.Current);
                if(Item1.HasValue&&Default.Compare(Item0.Value,Item1.Value)<0)Item0=Item1;
            }
            return Item0;
        }
        return default;
    }
    /// <summary>ジェネリック 集合の各要素に対して変換関数を呼び出し、結果の最小値を返します。</summary>
    /// <returns>集合の最小値。</returns>
    /// <param name="source">最小値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <typeparam name="TResult">
    ///   <paramref name="selector" /> によって返される値の型。</typeparam>
    public static TResult Min<TSource, TResult>(this IEnumerable<TSource> source,Func<TSource,TResult> selector) {
        using var Enumerator = source.GetEnumerator();
        var Default =Comparer<TResult>.Default;
        while(Enumerator.MoveNext()) {
            var Item0 = selector(Enumerator.Current);
            while(Enumerator.MoveNext()) {
                var Item1 = selector(Enumerator.Current);
                if(Default.Compare(Item0,Item1)>0)Item0=Item1;
            }
            return Item0;
        }
        throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
    }
    /// <summary>ジェネリック 集合の各要素に対して変換関数を呼び出し、結果の最小値を返します。</summary>
    /// <returns>集合の最小値。</returns>
    /// <param name="source">最小値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <typeparam name="TResult">
    ///   <paramref name="selector" /> によって返されるTResult?の型。</typeparam>
    public static TResult? Min<TSource, TResult>(this IEnumerable<TSource> source,Func<TSource,TResult?> selector)where TResult:struct {
        using var Enumerator = source.GetEnumerator();
        var Default =Comparer<TResult>.Default;
        while(Enumerator.MoveNext()) {
            var Item0 = selector(Enumerator.Current);
            if(!Item0.HasValue) continue;
            while(Enumerator.MoveNext()) {
                var Item1 = selector(Enumerator.Current);
                if(Item1.HasValue&&Default.Compare(Item0.Value,Item1.Value)>0)
                    Item0=Item1;
            }
            return Item0;
        }
        return default;
    }
    private static bool PrivateMaxMin<TSource,TResult>(this IEnumerable<TSource> source,Func<TSource,TResult?> selector,out IEnumerator<TSource> Enumerator,out TResult? Result) where TResult:struct{
        var Enumerator0 = source.GetEnumerator();
        while(Enumerator0.MoveNext()) {
            var value = selector(Enumerator0.Current);
            if(!value.HasValue) continue;
            Result=value;
            Enumerator=Enumerator0;
            return true;
        }
        Result=default;
        Enumerator=default!;
        return false;
    }
    /// <summary>集合の各要素に対して変換関数を呼び出し、decimal? の最大値を返します。</summary>
    /// <typeparam name="TSource">source の要素の型。</typeparam>
    /// <param name="source">最大値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>集合の最大値に対応する decimal? 型の値。</returns>
    public static decimal? Max<TSource>(this IEnumerable<TSource> source,Func<TSource,decimal?> selector) {
        if(!PrivateMaxMin(source,selector,out var Enumerator,out var Result)) return Result;
        while(Enumerator.MoveNext()) {
            var Item = selector(Enumerator.Current);
            if(Result<Item)Result=Item;
        }
        return Result;
    }
    /// <summary>集合の各要素に対して変換関数を呼び出し、decimal? の最小値を返します。</summary>
    /// <typeparam name="TSource">source の要素の型。</typeparam>
    /// <param name="source">最小値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>集合の最小値に対応する decimal? 型の値。</returns>
    public static decimal? Min<TSource>(this IEnumerable<TSource> source,Func<TSource,decimal?> selector) {
        if(!PrivateMaxMin(source,selector,out var Enumerator,out var Result)) return Result;
        while(Enumerator.MoveNext()) {
            var Item = selector(Enumerator.Current);
            if(Result>Item)Result=Item;
        }
        return Result;
    }
    /// <summary>集合の各要素に対して変換関数を呼び出し、double? の最大値を返します。</summary>
    /// <typeparam name="TSource">source の要素の型。</typeparam>
    /// <param name="source">最大値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>集合の最大値に対応する double? 型の値。</returns>
    public static double? Max<TSource>(this IEnumerable<TSource> source,Func<TSource,double?> selector) {
        if(!PrivateMaxMin(source,selector,out var Enumerator,out var Result)) return Result;
        while(Enumerator.MoveNext()) {
            var value = selector(Enumerator.Current);
            if(Result<value)Result=value;
        }
        return Result;
    }
    /// <summary>集合の各要素に対して変換関数を呼び出し、double? の最小値を返します。</summary>
    /// <typeparam name="TSource">source の要素の型。</typeparam>
    /// <param name="source">最小値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>集合の最小値に対応する double? 型の値。</returns>
    public static double? Min<TSource>(this IEnumerable<TSource> source,Func<TSource,double?> selector) {
        if(!PrivateMaxMin(source,selector,out var Enumerator,out var Result)) return Result;
        while(Enumerator.MoveNext()) {
            var value = selector(Enumerator.Current);
            if(Result>value)Result=value;
        }
        return Result;
    }
    /// <summary>集合の各要素に対して変換関数を呼び出し、float? の最大値を返します。</summary>
    /// <typeparam name="TSource">source の要素の型。</typeparam>
    /// <param name="source">最大値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>集合の最大値に対応する float? 型の値。</returns>
    public static float? Max<TSource>(this IEnumerable<TSource> source,Func<TSource,float?> selector) {
        if(!PrivateMaxMin(source,selector,out var Enumerator,out var Result)) return Result;
        while(Enumerator.MoveNext()) {
            var value = selector(Enumerator.Current);
            if(Result<value)Result=value;
        }
        return Result;
    }
    /// <summary>集合の各要素に対して変換関数を呼び出し、float? の最小値を返します。</summary>
    /// <typeparam name="TSource">source の要素の型。</typeparam>
    /// <param name="source">最小値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>集合の最小値に対応する float? 型の値。</returns>
    public static float? Min<TSource>(this IEnumerable<TSource> source,Func<TSource,float?> selector) {
        if(!PrivateMaxMin(source,selector,out var Enumerator,out var Result)) return Result;
        while(Enumerator.MoveNext()) {
            var value = selector(Enumerator.Current);
            if(Result>value)Result=value;
        }
        return Result;
    }
    /// <summary>集合の各要素に対して変換関数を呼び出し、long? の最大値を返します。</summary>
    /// <typeparam name="TSource">source の要素の型。</typeparam>
    /// <param name="source">最大値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>集合の最大値に対応する long? 型の値。</returns>
    public static long? Max<TSource>(this IEnumerable<TSource> source,Func<TSource,long?> selector) {
        if(!PrivateMaxMin(source,selector,out var Enumerator,out var Result)) return Result;
        while(Enumerator.MoveNext()) {
            var value = selector(Enumerator.Current);
            if(Result<value)Result=value;
        }
        return Result;
    }
    /// <summary>集合の各要素に対して変換関数を呼び出し、long? の最小値を返します。</summary>
    /// <typeparam name="TSource">source の要素の型。</typeparam>
    /// <param name="source">最小値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>集合の最小値に対応する long? 型の値。</returns>
    public static long? Min<TSource>(this IEnumerable<TSource> source,Func<TSource,long?> selector) {
        if(!PrivateMaxMin(source,selector,out var Enumerator,out var Result)) return Result;
        while(Enumerator.MoveNext()) {
            var value = selector(Enumerator.Current);
            if(Result>value)Result=value;
        }
        return Result;
    }
    /// <summary>集合の各要素に対して変換関数を呼び出し、int? の最大値を返します。</summary>
    /// <typeparam name="TSource">source の要素の型。</typeparam>
    /// <param name="source">最大値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>集合の最大値に対応する int? 型の値。</returns>
    public static int? Max<TSource>(this IEnumerable<TSource> source,Func<TSource,int?> selector) {
        if(!PrivateMaxMin(source,selector,out var Enumerator,out var Result)) return Result;
        while(Enumerator.MoveNext()) {
            var value = selector(Enumerator.Current);
            if(Result<value)Result=value;
        }
        return Result;
    }
    /// <summary>集合の各要素に対して変換関数を呼び出し、int? の最小値を返します。</summary>
    /// <typeparam name="TSource">source の要素の型。</typeparam>
    /// <param name="source">最小値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>集合の最小値に対応する int? 型の値。</returns>
    public static int? Min<TSource>(this IEnumerable<TSource> source,Func<TSource,int?> selector) {
        if(!PrivateMaxMin(source,selector,out var Enumerator,out var Result)) return Result;
        while(Enumerator.MoveNext()) {
            var value = selector(Enumerator.Current);
            if(Result>value)Result=value;
        }
        return Result;
    }
    ///// <summary>集合の各要素に対して変換関数を呼び出し、int? の最大値を返します。</summary>
    ///// <typeparam name="TSource">source の要素の型。</typeparam>
    ///// <param name="source">最大値を確認する対象となる値の集合。</param>
    ///// <param name="selector">各要素に適用する変換関数。</param>
    ///// <returns>集合の最大値に対応する object 型の値。</returns>
    //public static object Max<TSource>(this IEnumerable<TSource> source,Func<TSource,object> selector) {
    //    if(!PrivateMaxMin(source,selector,out var Enumerator,out var Result)) return Result;
    //    var Default=Comparer<object>.Default;
    //    while(Enumerator.MoveNext()) {
    //        var value = selector(Enumerator.Current);
    //        if(Default.Compare(Result,value)<0)Result=value;
    //    }
    //    return Result;
    //}
    ///// <summary>集合の各要素に対して変換関数を呼び出し、int? の最小値を返します。</summary>
    ///// <typeparam name="TSource">source の要素の型。</typeparam>
    ///// <param name="source">最小値を確認する対象となる値の集合。</param>
    ///// <param name="selector">各要素に適用する変換関数。</param>
    ///// <returns>集合の最小値に対応する object 型の値。</returns>
    //public static object Min<TSource>(this IEnumerable<TSource> source,Func<TSource,object> selector) {
    //    if(!PrivateMaxMin(source,selector,out var Enumerator,out var Result)) return Result;
    //    var Default=Comparer<object>.Default;
    //    while(Enumerator.MoveNext()) {
    //        var value = selector(Enumerator.Current);
    //        if(Default.Compare(Result,value)>0)Result=value;
    //    }
    //    return Result;
    //}
    private static bool PrivateMaxMin<TSource, TResult>(this IEnumerable<TSource> source,Func<TSource,TResult> selector,out IEnumerator<TSource> Enumerator,out TResult Result) {
        //private static Boolean PrivateMaxMin<TSource, TResult>(this IOutIEnumerable<TSource> source,Func<TSource,TResult> selector,out IOutIEnumerable<TSource>.Enumerator Enumerator,out TResult Result){
        var Enumerator0 = source.GetEnumerator();
        if(Enumerator0.MoveNext()) {
            Result=selector(Enumerator0.Current);
            Enumerator=Enumerator0;
            return true;
        }
        Result=default!;
        Enumerator=default!;
        return false;
    }
    /// <summary>集合の各要素に対して変換関数を呼び出し、decimal の最大値を返します。</summary>
    /// <typeparam name="TSource">source の要素の型。</typeparam>
    /// <param name="source">最大値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>集合の最大値に対応する decimal 型の値。</returns>
    public static decimal Max<TSource>(this IEnumerable<TSource> source,Func<TSource,decimal> selector) {
        if(!PrivateMaxMin(source,selector,out var Enumerator,out var Result)) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        while(Enumerator.MoveNext()) {
            var Item = selector(Enumerator.Current);
            if(Result<Item)Result=Item;
        }
        return Result;
    }
    /// <summary>集合の各要素に対して変換関数を呼び出し、decimal の最小値を返します。</summary>
    /// <typeparam name="TSource">source の要素の型。</typeparam>
    /// <param name="source">最小値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>集合の最小値に対応する decimal 型の値。</returns>
    public static decimal Min<TSource>(this IEnumerable<TSource> source,Func<TSource,decimal> selector) {
        if(!PrivateMaxMin(source,selector,out var Enumerator,out var Result)) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        while(Enumerator.MoveNext()) {
            var Item = selector(Enumerator.Current);
            if(Result>Item)Result=Item;
        }
        return Result;
    }
    /// <summary>集合の各要素に対して変換関数を呼び出し、double の最大値を返します。</summary>
    /// <typeparam name="TSource">source の要素の型。</typeparam>
    /// <param name="source">最大値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>集合の最大値に対応する double 型の値。</returns>
    public static double Max<TSource>(this IEnumerable<TSource> source,Func<TSource,double> selector) {
        if(!PrivateMaxMin(source,selector,out var Enumerator,out var Result)) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        while(Enumerator.MoveNext()) {
            var value = selector(Enumerator.Current);
            if(Result<value)Result=value;
        }
        return Result;
    }
    /// <summary>集合の各要素に対して変換関数を呼び出し、double の最小値を返します。</summary>
    /// <typeparam name="TSource">source の要素の型。</typeparam>
    /// <param name="source">最小値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>集合の最小値に対応する double 型の値。</returns>
    public static double Min<TSource>(this IEnumerable<TSource> source,Func<TSource,double> selector) {
        if(!PrivateMaxMin(source,selector,out var Enumerator,out var Result)) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        while(Enumerator.MoveNext()) {
            var value = selector(Enumerator.Current);
            if(Result>value)Result=value;
        }
        return Result;
    }
    /// <summary>集合の各要素に対して変換関数を呼び出し、float の最大値を返します。</summary>
    /// <typeparam name="TSource">source の要素の型。</typeparam>
    /// <param name="source">最大値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>集合の最大値に対応する float 型の値。</returns>
    public static float Max<TSource>(this IEnumerable<TSource> source,Func<TSource,float> selector) {
        if(!PrivateMaxMin(source,selector,out var Enumerator,out var Result)) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        while(Enumerator.MoveNext()) {
            var value = selector(Enumerator.Current);
            if(Result<value)Result=value;
        }
        return Result;
    }
    /// <summary>集合の各要素に対して変換関数を呼び出し、float の最小値を返します。</summary>
    /// <typeparam name="TSource">source の要素の型。</typeparam>
    /// <param name="source">最小値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>集合の最小値に対応する float 型の値。</returns>
    public static float Min<TSource>(this IEnumerable<TSource> source,Func<TSource,float> selector) {
        if(!PrivateMaxMin(source,selector,out var Enumerator,out var Result)) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        while(Enumerator.MoveNext()) {
            var value = selector(Enumerator.Current);
            if(Result>value)Result=value;
        }
        return Result;
    }
    /// <summary>集合の各要素に対して変換関数を呼び出し、long の最大値を返します。</summary>
    /// <typeparam name="TSource">source の要素の型。</typeparam>
    /// <param name="source">最大値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>集合の最大値に対応する long 型の値。</returns>
    public static long Max<TSource>(this IEnumerable<TSource> source,Func<TSource,long> selector) {
        if(!PrivateMaxMin(source,selector,out var Enumerator,out var Result)) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        while(Enumerator.MoveNext()) {
            var value = selector(Enumerator.Current);
            if(Result<value)Result=value;
        }
        return Result;
    }
    /// <summary>集合の各要素に対して変換関数を呼び出し、long の最小値を返します。</summary>
    /// <typeparam name="TSource">source の要素の型。</typeparam>
    /// <param name="source">最小値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>集合の最小値に対応する long 型の値。</returns>
    public static long Min<TSource>(this IEnumerable<TSource> source,Func<TSource,long> selector) {
        if(!PrivateMaxMin(source,selector,out var Enumerator,out var Result)) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        while(Enumerator.MoveNext()) {
            var value = selector(Enumerator.Current);
            if(Result>value)Result=value;
        }
        return Result;
    }
    /// <summary>集合の各要素に対して変換関数を呼び出し、int の最大値を返します。</summary>
    /// <typeparam name="TSource">source の要素の型。</typeparam>
    /// <param name="source">最大値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>集合の最大値に対応する int 型の値。</returns>
    public static int Max<TSource>(this IEnumerable<TSource> source,Func<TSource,int> selector) {
        if(!PrivateMaxMin(source,selector,out var Enumerator,out var Result)) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        while(Enumerator.MoveNext()) {
            var value = selector(Enumerator.Current);
            if(Result<value)Result=value;
        }
        return Result;
    }
    /// <summary>集合の各要素に対して変換関数を呼び出し、int の最小値を返します。</summary>
    /// <typeparam name="TSource">source の要素の型。</typeparam>
    /// <param name="source">最小値を確認する対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>集合の最小値に対応する int 型の値。</returns>
    public static int Min<TSource>(this IEnumerable<TSource> source,Func<TSource,int> selector) {
        if(!PrivateMaxMin(source,selector,out var Enumerator,out var Result)) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        while(Enumerator.MoveNext()) {
            var value = selector(Enumerator.Current);
            if(Result>value)Result=value;
        }
        return Result;
    }
    /// <summary>指定された型に基づいて <see cref="IEnumerable" /> の要素をフィルター処理します。</summary>
    /// <returns>
    ///   TResult型の入力集合の要素を格納する <see cref="IEnumerable{TSource}" />。</returns>
    /// <param name="source">フィルター処理する要素を含む <see cref="IEnumerable" />。</param>
    /// <typeparam name="TResult">集合の要素をフィルター処理する型。</typeparam>
    public static IEnumerable<TResult> OfType<TResult>(this IEnumerable source) {
        var Result = new Set<TResult>();
        long Count = 0;
        foreach(var a in source) {
            if(a is TResult Current) {
                var r = Result.InternalIsAdded(Current);
                Debug.Assert(r);
                Count++;
            }
        }
        Result._LongCount=Count;
        return Result;
    }
    /// <summary>
    /// 指定した範囲内の整数のシーケンスを生成します。
    /// </summary>
    /// <param name="start">シーケンス内の最初の整数の値。</param>
    /// <param name="count">生成する連続した整数の数。</param>
    /// <returns>連続した整数の範囲を含む IEnumerable&lt;Int32> (C# の場合) または IEnumerable(Of Int32) (Visual Basicの場合)。</returns>
    public static IEnumerable<int> Range(int start,int count) {
        Debug.Assert(count>=0);
        var r = new Set<int>();
        var index = count;
        while(index-->0)r.InternalIsAdded(start++);
        r._LongCount=count;
        return r;
    }
    /// <summary>集合の各要素を新しいフォームに射影します。</summary>
    /// <returns>
    ///   <paramref name="source" /> の各要素に対して変換関数を呼び出した結果として得られる要素を含む <see cref="IEnumerable{TResult}" />。</returns>
    /// <param name="source">変換関数を呼び出す対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <typeparam name="TResult">
    ///   <paramref name="selector" /> によって返される値の型。</typeparam>
    public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source,Func<TSource,TResult> selector) {
        var r = new Set<TResult>();
        var Count = 0L;
        foreach(var a in source)
            if(r.InternalIsAdded(selector(a)))Count++;
        r._LongCount=Count;
        return r;
    }
    /// <summary>
    /// 集合の各要素を <see cref="IEnumerable{TResult}" />に射影し、結果の集合を 1つの集合に平坦化して、その各要素に対して結果のセレクター関数を呼び出します。
    /// </summary>
    /// <returns>
    ///   <paramref name="source" /> の各要素で一対多の変換関数 <paramref name="collectionSelector" /> を呼び出し、こうした集合の各要素とそれに対応するソース要素を結果の要素に割り当てた結果として得られる要素を含む <see cref="IEnumerable{TResult}" />。</returns>
    /// <param name="source">射影する値の集合。</param>
    /// <param name="collectionSelector">入力集合の各要素に適用する変換関数。</param>
    /// <param name="resultSelector">中間集合の各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <typeparam name="TCollection">
    ///   <paramref name="collectionSelector" /> によって収集される中間要素の型。</typeparam>
    /// <typeparam name="TResult">結果の集合の要素の型。</typeparam>
    public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source,Func<TSource,IEnumerable<TCollection>> collectionSelector,Func<TSource,TCollection,TResult> resultSelector) {
        var r = new Set<TResult>();
        var Count = 0L;
        foreach(var a in source)
            foreach(var b in collectionSelector(a))
                if(r.InternalIsAdded(resultSelector(a,b)))Count++;
        r._LongCount=Count;
        return r;
    }
    /// <summary>集合の各要素を <see cref="IEnumerable{TResult}" /> に射影し、結果の集合を 1つの集合に平坦化します。</summary>
    /// <returns>入力集合の各要素に対して一対多の変換関数を呼び出した結果として得られる要素を含む <see cref="IEnumerable{TResult}" />。</returns>
    /// <param name="source">射影する値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <typeparam name="TResult">
    ///   <paramref name="selector" /> によって返される集合の要素の型。</typeparam>
    public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source,Func<TSource,IEnumerable<TResult>> selector) {
        var r = new Set<TResult>();
        var Count = 0L;
        foreach(var a in source)
            foreach(var b in selector(a))
                if(r.InternalIsAdded(b))Count++;
        r._LongCount=Count;
        return r;
    }
    /// <summary>集合の唯一の要素を返します。集合内の要素が 1つだけではない場合は、例外をスローします。</summary>
    /// <returns>入力集合の 1つの要素。</returns>
    /// <param name="source">1つの要素を返す <see cref="IEnumerable{TSource}" />。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <exception cref="InvalidOperationException">入力集合に複数の要素が含まれています。または入力集合が空です。</exception>
    public static TSource Single<TSource>(this IEnumerable<TSource> source) {
        using var Enumerator = source.GetEnumerator();
        if(!Enumerator.MoveNext()) throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        var Result = Enumerator.Current;
        if(Enumerator.MoveNext())throw シーケンスに複数の要素が含まれています(MethodBase.GetCurrentMethod()!);
        return Result;
    }
    /// <summary>集合の唯一の要素を返します。集合が空の場合、既定値を返します。集合内に要素が複数ある場合、このメソッドは例外をスローします。</summary>
    /// <returns>入力集合の 1つの要素。集合に要素が含まれない場合は default (<paramref name="source" />)。</returns>
    /// <param name="source">1つの要素を返す <see cref="IEnumerable{TSource}" />。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source){//where TSource:class {
        using var Enumerator = source.GetEnumerator();
        if(!Enumerator.MoveNext()) return default!;
        var Result = Enumerator.Current;
        if(Enumerator.MoveNext())throw シーケンスに複数の要素が含まれています(MethodBase.GetCurrentMethod()!);
        return Result;
    }
    /// <summary>集合の唯一の要素を返します。集合が空の場合、既定値を返します。集合内に要素が複数ある場合、このメソッドは例外をスローします。</summary>
    /// <returns>入力集合の 1つの要素。集合に要素が含まれない場合は default (<paramref name="source" />)。</returns>
    /// <param name="source">1つの要素を返す <see cref="IEnumerable{TSource}" />。</param>
    /// <param name="defaultValue">要素が空の場合の規定値</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source,TSource defaultValue) {
        using var Enumerator = source.GetEnumerator();
        if(!Enumerator.MoveNext()) return defaultValue;
        var Result = Enumerator.Current;
        if(Enumerator.MoveNext())throw シーケンスに複数の要素が含まれています(MethodBase.GetCurrentMethod()!);
        return Result;
    }
    /// <summary>
    /// 標準偏差
    /// </summary>
    /// <param name="source">標準偏差計算の対象となる値の集合。</param>
    /// <returns>値の集合の標準偏差値。</returns>
    /// <exception cref="InvalidOperationException">
    ///   <paramref name="source" /> に要素が含まれていません。</exception>
    public static double Stdev(this IEnumerable<double> source) {
        var Count = source.LongCount;
        if(Count==0)throw シーケンスに要素が含まれていません(MethodBase.GetCurrentMethod()!);
        var Array = new double[Count];
        double sum = 0;
        var index = 0;
        foreach(var a in source) {
            Array[index++]=a;
            sum+=a;
        }
        double DoubleCount = Count;
        var average = sum/DoubleCount;
        sum=0;
        foreach(var a in Array) {
            var sub = average-a;
            sum+=sub*sub;
        }
        return Math.Sqrt(sum/(DoubleCount-1));
    }
    /// <summary>
    /// 標準偏差
    /// </summary>
    /// <param name="source">標準偏差計算の対象となる値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <returns>値の集合の標準偏差値。</returns>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <exception cref="InvalidOperationException">
    ///   <paramref name="source" /> に要素が含まれていません。</exception>
    public static double Stdev<TSource>(this IEnumerable<TSource> source,Func<TSource,double> selector) {
        var Count = source.LongCount;
        var (Array, sum)=合計値を求める(source,selector,Count,MethodBase.GetCurrentMethod()!);
        double DoubleCount = Count;
        var average = sum/DoubleCount;
        sum=0;
        foreach(var a in Array) {
            var sub = average-a;
            sum+=sub*sub;
        }
        return Math.Sqrt(sum/(DoubleCount-1));
    }
    /// <summary>
    ///   <see cref="decimal?" /> 値の集合の合計を計算します。</summary>
    /// <returns>集合の値の合計。</returns>
    /// <param name="source">合計を計算する対象となる <see cref="decimal" /> 値の集合。</param>
    /// <exception cref="OverflowException">合計が <see cref="decimal.MaxValue" /> を超えています。</exception>
    public static decimal? Sum(this IEnumerable<decimal?> source) {
        decimal? sum = 0;
        foreach(var a in source)sum+=a;
        return sum;
    }
    /// <summary>
    ///   <see cref="decimal" /> 値の集合の合計を計算します。</summary>
    /// <returns>集合の値の合計。</returns>
    /// <param name="source">合計を計算する対象となる <see cref="decimal" /> 値の集合。</param>
    /// <exception cref="OverflowException">合計が <see cref="decimal.MaxValue" /> を超えています。</exception>
    public static decimal Sum(this IEnumerable<decimal> source) {
        decimal sum = 0;
        foreach(var a in source)sum+=a;
        return sum;
    }
    /// <summary>
    ///   <see cref="double?" /> 値の集合の合計を計算します。</summary>
    /// <returns>集合の値の合計。</returns>
    /// <param name="source">合計を計算する対象となる <see cref="double" /> 値の集合。</param>
    public static double? Sum(this IEnumerable<double?> source) {
        double? sum = 0;
        foreach(var a in source)sum+=a;
        return sum;
    }
    /// <summary>
    ///   <see cref="double" /> 値の集合の合計を計算します。</summary>
    /// <returns>集合の値の合計。</returns>
    /// <param name="source">合計を計算する対象となる <see cref="double" /> 値の集合。</param>
    public static double Sum(this IEnumerable<double> source) {
        double sum = 0;
        foreach(var a in source)sum+=a;
        return sum;
    }
    /// <summary>
    ///   <see cref="float?" /> 値の集合の合計を計算します。</summary>
    /// <returns>集合の値の合計。</returns>
    /// <param name="source">合計を計算する対象となる <see cref="float?" /> 値の集合。</param>
    public static float? Sum(this IEnumerable<float?> source) {
        float? sum = 0;
        foreach(var a in source)sum+=a;
        return sum;
    }
    /// <summary>
    ///   <see cref="float" /> 値の集合の合計を計算します。</summary>
    /// <returns>集合の値の合計。</returns>
    /// <param name="source">合計を計算する対象となる <see cref="float" /> 値の集合。</param>
    public static float Sum(this IEnumerable<float> source) {
        float sum = 0;
        foreach(var a in source)sum+=a;
        return sum;
    }
    /// <summary>
    ///   <see cref="int?" /> 値の集合の合計を計算します。</summary>
    /// <returns>集合の値の合計。</returns>
    /// <param name="source">合計を計算する対象となる <see cref="int?" /> 値の集合。</param>
    public static int? Sum(this IEnumerable<int?> source) {
        int? sum = 0;
        foreach(var a in source)sum+=a;
        return sum;
    }
    /// <summary>
    ///   <see cref="int" /> 値の集合の合計を計算します。</summary>
    /// <returns>集合の値の合計。</returns>
    /// <param name="source">合計を計算する対象となる <see cref="int" /> 値の集合。</param>
    public static int Sum(this IEnumerable<int> source) {
        var sum = 0;
        foreach(var a in source)sum+=a;
        return sum;
    }
    /// <summary>
    ///   <see cref="long?" /> 値の集合の合計を計算します。</summary>
    /// <returns>集合の値の合計。</returns>
    /// <param name="source">合計を計算する対象となる <see cref="long?" /> 値の集合。</param>
    public static long? Sum(this IEnumerable<long?> source) {
        long? sum = 0;
        foreach(var a in source)sum+=a;
        return sum;
    }
    /// <summary>
    ///   <see cref="long" /> 値の集合の合計を計算します。</summary>
    /// <returns>集合の値の合計。</returns>
    /// <param name="source">合計を計算する対象となる <see cref="long" /> 値の集合。</param>
    public static long Sum(this IEnumerable<long> source) {
        long sum = 0;
        foreach(var a in source)sum+=a;
        return sum;
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する null 許容の <see cref="decimal?" /> 値の集合の合計を計算します。</summary>
    /// <returns>射影された値の合計。</returns>
    /// <param name="source">合計の計算に使用される値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <exception cref="ArgumentNullException">
    ///   <paramref name="source" /> または <paramref name="selector" /> が null です。</exception>
    /// <exception cref="OverflowException">合計が <see cref="decimal.MaxValue" /> を超えています。</exception>
    public static decimal? Sum<TSource>(this IEnumerable<TSource> source,Func<TSource,decimal?> selector) {
        var num = 0m;
        foreach(var current in source) {
            var v = selector(current);
            if(v.HasValue)num+=v.Value;
        }
        return num;
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する <see cref="decimal" /> 値の集合の合計を計算します。</summary>
    /// <returns>射影された値の合計。</returns>
    /// <param name="source">合計の計算に使用される値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <exception cref="OverflowException">合計が <see cref="decimal.MaxValue" /> を超えています。</exception>
    public static decimal Sum<TSource>(this IEnumerable<TSource> source,Func<TSource,decimal> selector) {
        decimal sum = 0;
        foreach(var a in source)sum+=selector(a);
        return sum;
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する <see cref="double?" /> 値の集合の合計を計算します。</summary>
    /// <returns>射影された値の合計。</returns>
    /// <param name="source">合計の計算に使用される値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    public static double? Sum<TSource>(this IEnumerable<TSource> source,Func<TSource,double?> selector) {
        double? sum = 0;
        foreach(var a in source)sum+=selector(a);
        return sum;
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する <see cref="double" /> 値の集合の合計を計算します。</summary>
    /// <returns>射影された値の合計。</returns>
    /// <param name="source">合計の計算に使用される値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    public static double Sum<TSource>(this IEnumerable<TSource> source,Func<TSource,double> selector) {
        double sum = 0;
        foreach(var a in source)sum+=selector(a);
        return sum;
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する <see cref="float?" /> 値の集合の合計を計算します。</summary>
    /// <returns>射影された値の合計。</returns>
    /// <param name="source">合計の計算に使用される値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    public static float? Sum<TSource>(this IEnumerable<TSource> source,Func<TSource,float?> selector) {
        float? sum = 0;
        foreach(var a in source)sum+=selector(a);
        return sum;
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する <see cref="float" /> 値の集合の合計を計算します。</summary>
    /// <returns>射影された値の合計。</returns>
    /// <param name="source">合計の計算に使用される値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    public static float Sum<TSource>(this IEnumerable<TSource> source,Func<TSource,float> selector) {
        float sum = 0;
        foreach(var a in source)sum+=selector(a);
        return sum;
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する <see cref="int?" /> 値の集合の合計を計算します。</summary>
    /// <returns>射影された値の合計。</returns>
    /// <param name="source">合計の計算に使用される値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <exception cref="OverflowException">合計が <see cref="int.MaxValue" /> を超えています。</exception>
    public static int? Sum<TSource>(this IEnumerable<TSource> source,Func<TSource,int?> selector) {
        int? sum = 0;
        foreach(var a in source)sum+=selector(a);
        return sum;
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する <see cref="int" /> 値の集合の合計を計算します。</summary>
    /// <returns>射影された値の合計。</returns>
    /// <param name="source">合計の計算に使用される値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <exception cref="OverflowException">合計が <see cref="int.MaxValue" /> を超えています。</exception>
    public static int Sum<TSource>(this IEnumerable<TSource> source,Func<TSource,int> selector) {
        var sum = 0;
        foreach(var a in source)sum+=selector(a);
        return sum;
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する <see cref="long?" /> 値の集合の合計を計算します。</summary>
    /// <returns>射影された値の合計。</returns>
    /// <param name="source">合計の計算に使用される値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    public static long? Sum<TSource>(this IEnumerable<TSource> source,Func<TSource,long?> selector) {
        long? sum = 0;
        foreach(var a in source)sum+=selector(a);
        return sum;
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する <see cref="long" /> 値の集合の合計を計算します。</summary>
    /// <returns>射影された値の合計。</returns>
    /// <param name="source">合計の計算に使用される値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    public static long Sum<TSource>(this IEnumerable<TSource> source,Func<TSource,long> selector) {
        long sum = 0;
        foreach(var a in source)sum+=selector(a);
        return sum;
    }
    /// <summary>既定の等値比較子を使用して、2 つの集合の和集合を生成します。</summary>
    /// <returns>2 つの入力集合の要素 (重複する要素は除く) を格納している <see cref="IEnumerable{TSource}" />。</returns>
    /// <param name="first">和集合の最初のセットを形成する一意の要素を含む <see cref="IEnumerable{TSource}" />。</param>
    /// <param name="second">和集合の 2番目のセットを形成する一意の要素を含む <see cref="IEnumerable{TSource}" />。</param>
    /// <typeparam name="TSource">入力集合の要素の型。</typeparam>
    public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first,IEnumerable<TSource> second) {
        var r = new Set<TSource>(first);
        r.UnionWith(second);
        return r;
    }
    /// <summary>述語に基づいて値の集合をフィルター処理します。</summary>
    /// <returns>条件を満たす、入力集合の要素を含む <see cref="IEnumerable{TSource}" />。</returns>
    /// <param name="source">フィルター処理する <see cref="IEnumerable{TSource}" />。</param>
    /// <param name="predicate">各要素が条件を満たしているかどうかをテストする関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source,Func<TSource,bool> predicate) {
        var r = new Set<TSource>();
        long Count = 0;
        foreach(var a in source){
            if(!predicate(a)) continue;
            var b = r.InternalIsAdded(a);
            Debug.Assert(b);
            Count++;
        }
        r._LongCount=Count;
        return r;
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する <see cref="decimal" /> 値の集合の標本分散を計算します。</summary>
    /// <returns>値の集合の標本分散。</returns>
    /// <param name="source">標本分散の計算に使用される値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <exception cref="InvalidOperationException">
    ///   <paramref name="source" /> に要素が含まれていません。</exception>
    /// <exception cref="OverflowException">集合内の要素の合計が <see cref="decimal.MaxValue" /> を超えています。</exception>
    public static decimal Var<TSource>(this IEnumerable<TSource> source,Func<TSource,decimal> selector) {
        var Count = source.LongCount;
        return PrivateVariance(source,selector,Count,Count-1);
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する <see cref="double" /> 値の集合の標本分散を計算します。</summary>
    /// <returns>値の集合の標本分散。</returns>
    /// <param name="source">標本分散の計算に使用される値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <exception cref="InvalidOperationException">
    ///   <paramref name="source" /> に要素が含まれていません。</exception>
    /// <exception cref="OverflowException">集合内の要素の合計が <see cref="decimal.MaxValue" /> を超えています。</exception>
    public static double Var<TSource>(this IEnumerable<TSource> source,Func<TSource,double> selector) {
        var Count = source.LongCount;
        return PrivateVariance(source,selector,Count,Count-1);
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する <see cref="decimal" /> 値の集合の母分散を計算します。</summary>
    /// <returns>値の集合の母分散。</returns>
    /// <param name="source">母分散の計算に使用される値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <exception cref="InvalidOperationException">
    ///   <paramref name="source" /> に要素が含まれていません。</exception>
    /// <exception cref="OverflowException">集合内の要素の合計が <see cref="decimal.MaxValue" /> を超えています。</exception>
    public static decimal Varp<TSource>(this IEnumerable<TSource> source,Func<TSource,decimal> selector) {
        var Count = source.LongCount;
        return PrivateVariance(source,selector,Count,Count);
    }
    /// <summary>入力集合の各要素に対して変換関数を呼び出して取得する <see cref="double" /> 値の集合の母分散を計算します。</summary>
    /// <returns>値の集合の母分散。</returns>
    /// <param name="source">母分散の計算に使用される値の集合。</param>
    /// <param name="selector">各要素に適用する変換関数。</param>
    /// <typeparam name="TSource">
    ///   <paramref name="source" /> の要素の型。</typeparam>
    /// <exception cref="InvalidOperationException">
    ///   <paramref name="source" /> に要素が含まれていません。</exception>
    /// <exception cref="OverflowException">集合内の要素の合計が <see cref="decimal.MaxValue" /> を超えています。</exception>
    public static double Varp<TSource>(this IEnumerable<TSource> source,Func<TSource,double> selector) {
        var Count = source.LongCount;
        return PrivateVariance(source,selector,Count,Count);
    }
    /// <summary>
    /// インラインテスト用メソッド。
    /// </summary>
    /// <param name="input">デリゲートへの引数。</param>
    /// <param name="func">デリゲート。</param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public static TResult Inline<T, TResult>(this T input,Func<T,TResult> func) => func(input);
    /// <summary>
    /// インラインテスト用メソッド。
    /// </summary>
    /// <param name="func">デリゲート。</param>
    /// <typeparam name="TResult"></typeparam>
    /// <returns>デリゲートの戻り値</returns>
    public static TResult Inline<TResult>(Func<TResult> func) => func();
    private static decimal PrivateVariance<TSource>(IEnumerable<TSource> source,Func<TSource,decimal> selector,long Count,long 割る数) {
        var (Array, 合計)=合計値を求める(source,selector,Count,MethodBase.GetCurrentMethod()!);
        var 算術平均 = 合計/Count;
        合計=0;
        foreach(var a in Array) {
            var 差 = 算術平均-a;
            合計+=差*差;
        }
        return 合計/割る数;
    }
    private static double PrivateVariance<TSource>(IEnumerable<TSource> source,Func<TSource,double> selector,long Count,long 割る数) {
        var (Array, 合計)=合計値を求める(source,selector,Count,MethodBase.GetCurrentMethod()!);
        var 算術平均 = 合計/Count;
        合計=0;
        foreach(var a in Array) {
            var 差 = 算術平均-a;
            合計+=差*差;
        }
        return 合計/割る数;
    }
}
//2679
