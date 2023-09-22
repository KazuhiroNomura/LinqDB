using System.Diagnostics;

namespace LinqDB.Remote.Servers;

internal struct デシリアライズした{
    public readonly SingleReceiveSend SingleReceiveSend;
    public readonly Request Request;
    public readonly object? Object;
    public readonly SerializeType SerializeType;
    public デシリアライズした(SingleReceiveSend SingleReceiveSend,Request Request,object? Object,SerializeType SerializeType){
        Debug.Assert(SerializeType.Head<=SerializeType&&SerializeType<=SerializeType.Tail);
        this.SingleReceiveSend=SingleReceiveSend;
        this.Request=Request;
        this.Object=Object;
        this.SerializeType=SerializeType;
    }
}