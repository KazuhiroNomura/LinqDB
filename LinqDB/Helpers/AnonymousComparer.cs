using System;
using System.Collections.Generic;
namespace LinqDB.Helpers;

/// <summary>
/// 匿名型の比較クラスを簡易的に作る。
/// </summary>
public static class AnonymousComparer {
    #region IComparer<T>












    /// <summary>Example:AnonymousComparer.Create&lt;Int32>((T x,T y)=>y-x)</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="compare"></param>
    /// <returns></returns>
    public static IComparer<T> Create<T>(Func<T,T,int> compare)=>new Comparer<T>(compare);
    private class Comparer<T>:IComparer<T>{
        private readonly Func<T,T,int> compare;
        public Comparer(Func<T,T,int> compare)=>this.compare=compare;
#pragma warning disable CS8767 // パラメーターの型における参照型の NULL 値の許容が、暗黙的に実装されるメンバーと一致しません。おそらく、NULL 値の許容の属性が原因です。
        public int Compare(T x,T y)=> this.compare(x,y);
#pragma warning restore CS8767 // パラメーターの型における参照型の NULL 値の許容が、暗黙的に実装されるメンバーと一致しません。おそらく、NULL 値の許容の属性が原因です。
    }
    #endregion
    #region IEqualityComparer<T>
    /// <summary>Example:AnonymousComparer.Create((T t)=> 比較対象になる式)</summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="CompareKeySelector"></param>
    /// <returns></returns>
    public static EqualityComparer<T> Create<T,TKey>(Func<T,TKey> CompareKeySelector)=>new(
        (x,y)=> {
            if(ReferenceEquals(x,y)) return true;
            if(x is null||y is null) return false;
            return CompareKeySelector(x)!.Equals(CompareKeySelector(y));
        },
        obj => obj is null?0:CompareKeySelector(obj)!.GetHashCode()
    );
    /// <summary>
    /// 簡単にEqualityComparerを作る
    /// </summary>
    /// <param name="Equals">等価かどうか判定するデリゲート</param>
    /// <param name="GetHashCode">HashCodeを返すデリゲート</param>
    /// <typeparam name="T">対象のType</typeparam>
    /// <returns></returns>
    public static EqualityComparer<T> Create<T>(Func<T,T,bool> Equals,Func<T,int> GetHashCode)=> new(Equals,GetHashCode);
    /// <summary>
    /// 匿名型用EqualityComparer
    /// </summary>
    /// <typeparam name="T">匿名型</typeparam>
    public class EqualityComparer<T>:System.Collections.Generic.EqualityComparer<T>{
        private readonly Func<T,T,bool> _Equals;
        private readonly Func<T,int> _GetHashCode;
        internal EqualityComparer(Func<T,T,bool> Equals,Func<T,int> GetHashCode) {
            this._Equals=Equals;
            this._GetHashCode=GetHashCode;
        }
        /// <summary>デリゲートで比較する</summary>
        /// <returns>指定したオブジェクトが等しい場合は true。それ以外の場合は false。</returns>
        /// <param name="x">比較する最初のオブジェクト。</param>
        /// <param name="y">比較する 2 番目のオブジェクト。</param>
#pragma warning disable CS8765 // パラメーターの型の NULL 値の許容が、オーバーライドされたメンバーと一致しません。おそらく、NULL 値の許容の属性が原因です。
        public override bool Equals(T x,T y)=>this._Equals(x,y);
#pragma warning restore CS8765 // パラメーターの型の NULL 値の許容が、オーバーライドされたメンバーと一致しません。おそらく、NULL 値の許容の属性が原因です。
        /// <summary>ハッシュデリゲートとして機能します。</summary>
        /// <returns>指定したオブジェクトのハッシュ コード。</returns>
        /// <param name="obj">ハッシュ コードを取得する対象となるオブジェクト。</param>
        public override int GetHashCode(T obj)=>this._GetHashCode(obj);
    }
    #endregion
}