using System.Diagnostics;

namespace LinqDB.Remote.Servers;

internal struct デシリアライズした{
    public readonly SingleReceiveSend SingleReceiveSend;
    public readonly Request Request;
    public readonly object? Object;
    public readonly XmlType XmlType;
    public デシリアライズした(SingleReceiveSend SingleReceiveSend,Request Request,object? Object,XmlType XmlType){
        Debug.Assert(XmlType.Head<=XmlType&&XmlType<=XmlType.Tail);
        this.SingleReceiveSend=SingleReceiveSend;
        this.Request=Request;
        this.Object=Object;
        this.XmlType=XmlType;
    }
}