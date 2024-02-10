using System;
using System.IO;
using System.Reflection;
namespace LinqDB.Databases;

/// <summary>
/// エンティティの基底クラス
/// </summary>
[Serializable]
public class Container<TContainer>:Container where TContainer:Container<TContainer>{
    /// <summary>
    /// トランザクション内のTを返す。
    /// </summary>
    public virtual TContainer Transaction() => throw new NotImplementedException();
    /// <summary>
    /// 上位トランザクションのT。
    /// </summary>
    protected readonly TContainer? Parent;
    /// <summary>
    /// 既定コンストラクタ。
    /// </summary>
    protected Container() {
        this.Parent=null;
    }
    /// <summary>
    /// 上位トランザクションを持つコンストラクタ。
    /// </summary>
    /// <param name="Parent"></param>
    protected Container(TContainer? Parent) {
        this.Parent=Parent;
    }
    ///// <summary>
    ///// 上位トランザクションがない基底となるコンストラクタ。
    ///// </summary>
    ///// <param name="Reader">TextReaderの場合BOFが入らない(例:UTF8)エンコード。Unicodeはだめ。</param>
    ///// <param name="Writer">TextWriterの場合BOFが入らない(例:UTF8)エンコード。Unicodeはだめ。</param>
    //protected Container(Stream Reader,Stream Writer) : base(Reader,Writer) {
    //    this.Parent=null;
    //}
    ///// <summary>
    ///// 上位トランザクションがない基底となるコンストラクタ。
    ///// </summary>
    ///// <param name="Reader">TextReaderの場合BOFが入らない(例:UTF8)エンコード。Unicodeはだめ。</param>
    //protected Container(Stream Reader) : base(Reader) {
    //    this.Parent=null;
    //}
    ///// <summary>
    ///// コミット処理を実装。
    ///// </summary>
    ///// <param name="LogStream"></param>
    //protected virtual void Commit(Stream LogStream) {
    //}

    /// <summary>
    /// コンテナのメンバーのディープコピー処理。
    /// </summary>
    /// <param name="To">コピー先</param>
    protected virtual void Copy(TContainer To) {}
    public override void Commit() {
        this.UpdateRelationship();
        if(this.Parent is null) {
            var FileName=$"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\{DateTime.Now:yyyyMMddhhmmssffff}.{拡張子}";
            using var Stream=new FileStream(FileName,FileMode.Create,FileAccess.Write);
            this.Commit(Stream);
        } else {
            this.Copy(this.Parent);
        }
    }
}