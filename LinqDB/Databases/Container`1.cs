using System;
using System.IO;
using System.Globalization;
using System.Text;

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
    /// <summary>
    /// 上位トランザクションがない基底となるコンストラクタ。
    /// </summary>
    /// <param name="LogStream">TextWriterの場合BOFが入らない(例:UTF8)エンコード。Unicodeはだめ。</param>
    protected Container(Stream LogStream) : base(LogStream) {
        this.Parent=null;
    }
    /// <summary>
    /// コミット処理を実装。
    /// </summary>
    /// <param name="LogStream"></param>
    protected virtual void Commit(Stream LogStream) {
    }
    /// <summary>
    /// コンテナのメンバーのディープコピー処理。
    /// </summary>
    /// <param name="To">コピー先</param>
    protected virtual void Copy(TContainer To) {}
    public sealed override void Commit() {
        this.UpdateRelationship();
        if(this.Parent is null) {
            if(this.LogStream is not null) {
                var LogStream = this.LogStream;
                var buffer = Encoding.UTF8.GetBytes(DateTimeOffset.Now.ToString("yyyy/MM/dd HH:mm:ss.ff",CultureInfo.CurrentCulture));
                LogStream.Write(buffer,0,buffer.Length);
                //Writer.WriteStartElement("Container");
                //Writer.WriteAttributeString("DateTimeOffset",DateTimeOffset.Now.ToString("yyyy/MM/dd HH:mm:ss.ff",CultureInfo.CurrentCulture));
                this.Commit(LogStream);
                //Writer.WriteEndElement();
            }
        } else {
            this.Copy(this.Parent);
        }
    }
}