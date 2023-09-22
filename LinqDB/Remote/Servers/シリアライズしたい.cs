namespace LinqDB.Remote.Servers;

internal struct シリアライズしたい{
    public readonly SingleReceiveSend SingleReceiveSend;
    public readonly Response Response;
    public readonly SerializeType SerializeType;
    public readonly object? Object;
    public シリアライズしたい(SingleReceiveSend SingleReceiveSend,Response Response,SerializeType SerializeType,object? Object) {
        this.SingleReceiveSend=SingleReceiveSend;
        this.Response=Response;
        this.SerializeType=SerializeType;
        this.Object=Object;
    }
}