#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

using LinqDB.Databases.Dom;
namespace VM;

[DebuggerDisplay("{this."+nameof(Name)+"}")]
public class Sequence(string Name,Schema Schema,object start_value,object increment,object current_value)
    :ISequence {
    public string Name { get; set; }=Name;
    public ISchema Schema {get;}=Schema;
    ISchema ISequence.Schema => this.Schema;
    public object start_value{get;}=start_value;
    public object increment{get;}=increment;
    public object current_value{get;}=current_value;
}