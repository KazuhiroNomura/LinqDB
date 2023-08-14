using System;
using System.IO;
namespace LinqDB.Sets;

public interface IWriteRead<in TEntity> {
    void BinaryWrite(BinaryWriter Writer);
    void BinaryRead(BinaryReader Reader,Func<TEntity> Create);
    //void TextWrite(IndentedTextWriter Writer);
    //void TextRead(StreamReader Reader,Int32 Indent);
}