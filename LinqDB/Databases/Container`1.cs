using System;
using System.IO;
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
    public override void Commit() {
        this.UpdateRelationship();
        if(this.Parent is null) {
            if(this.LogStream is not null) {
                var LogStream = this.LogStream;
                {
                    //{"DateTime","2011/3/11 07:08:05"}
                    var j=new Utf8Json.JsonWriter();
                    j.WriteBeginObject();
                    j.WritePropertyName("DateTime");
                    j.WriteNameSeparator();
                    LogStream.Write(j.GetBuffer());
                    this.Serializer.Serialize(LogStream,DateTimeOffset.Now);
                    j=new Utf8Json.JsonWriter();
                    j.WriteEndObject();
                    j.WriteValueSeparator();
                    LogStream.Write(j.GetBuffer());
                }
                this.Commit(LogStream);
                {
                    var j=new Utf8Json.JsonWriter();
                    j.WriteValueSeparator();
                    LogStream.Write(j.GetBuffer());
                }
                //Writer.WriteEndElement();
            }
        } else {
            this.Copy(this.Parent);
        }
    }
}