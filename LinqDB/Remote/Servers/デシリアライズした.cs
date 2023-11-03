using System.Data.OracleClient;
using System.Diagnostics;

namespace LinqDB.Remote.Servers;

internal struct デシリアライズした{
    public readonly SingleReceiveSend SingleReceiveSend;
    public readonly Request Request;
    public readonly object? Object;
    public readonly SerializeType SerializeType;
    public デシリアライズした(SingleReceiveSend SingleReceiveSend,Request Request,object? Object,SerializeType SerializeType){
        Debug.Assert(SerializeType is SerializeType.MemoryPack or SerializeType.MessagePack or SerializeType.Utf8Json);
        this.SingleReceiveSend=SingleReceiveSend;
        this.Request=Request;
        this.Object=Object;
        this.SerializeType=SerializeType;
    }
}