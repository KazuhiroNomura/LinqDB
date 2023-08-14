namespace LinqDB.Remote.Servers;

internal struct シリアライズしたい{
    public readonly SingleReceiveSend SingleReceiveSend;
    public readonly Response Response;
    public readonly XmlType XmlType;
    public readonly object? Object;
    public シリアライズしたい(SingleReceiveSend SingleReceiveSend,Response Response,XmlType XmlType,object? Object) {
        this.SingleReceiveSend=SingleReceiveSend;
        this.Response=Response;
        this.XmlType=XmlType;
        this.Object=Object;
    }
}