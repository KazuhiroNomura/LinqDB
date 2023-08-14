using System;
using System.Linq.Expressions;

namespace LinqDB.Optimizers;

/// <summary>
/// サーバーで実行したい式木とそれに絡む情報
/// </summary>
[Serializable]
public struct Compile情報2{
    /// <summary>
    /// 実行したい式木
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage","CA2235:すべてのシリアル化不可能なフィールドを設定します",Justification = "<保留中>")]
    public readonly Expression Expression;
    /// <summary>
    /// デリゲートに引き渡すオブジェクト
    /// </summary>
    public readonly object? Target;
    /// <summary>
    /// 式をわかりやすく表示
    /// </summary>
    public readonly string Name;
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="Expression"></param>
    /// <param name="Delegate番号">デバッグ情報,ラムダ跨ぎParameter,→この位置,デリゲート,定数,Quote,GetMember,SetMemberの合計数</param>
    /// <param name="Target"></param>
    /// <param name="プレビュー"></param>
    public Compile情報2(Expression Expression,int Delegate番号,object? Target,string プレビュー) {
        this.Expression=Expression;
        this.Target=Target;
        //this.RootConstant=RootConstant;
        //this.DictionaryParameterMember=DictionaryParameterMember;
        this.Name=プレビュー;
    }
}