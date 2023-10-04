using Expressions=System.Linq.Expressions;
using Microsoft.CSharp.RuntimeBinder;
namespace LinqDB.Serializers;
internal static class Common {
    public static readonly CSharpArgumentInfo CSharpArgumentInfo1 = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
    public static readonly CSharpArgumentInfo[]CSharpArgumentInfos1={CSharpArgumentInfo1};
    public static readonly CSharpArgumentInfo[]CSharpArgumentInfos2={CSharpArgumentInfo1,CSharpArgumentInfo1};
    public static readonly CSharpArgumentInfo[]CSharpArgumentInfos3={CSharpArgumentInfo1,CSharpArgumentInfo1,CSharpArgumentInfo1};
    public static readonly CSharpArgumentInfo[]CSharpArgumentInfos4={CSharpArgumentInfo1,CSharpArgumentInfo1,CSharpArgumentInfo1,CSharpArgumentInfo1};
    public static CSharpArgumentInfo[]CreateCSharpArgumentInfo(Expressions.Expression[] Arguments){
        var Length=Arguments.Length;
        var value=new CSharpArgumentInfo[Length];
        for(var a=0;a<Length;a++) value[a]=CSharpArgumentInfo1;
        return value;
    }
    //public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos) GetBinder(BinaryOperationBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    return(d._callingContext,d._argumentInfo);
    //}
    //public static Type GetBinder(ConvertBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    return d._callingContext;
    //}
    //public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos,int Flags) GetBinder(CreateInstanceBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    return(d._callingContext,d._argumentInfo,0);
    //}
    //public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos,int Flags) GetBinder(DeleteIndexBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    return(d._callingContext,d._argumentInfo,0);
    //}
    //public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos,int Flags) GetBinder(DeleteMemberBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    return(d._callingContext,d._argumentInfo,0);
    //}
    //public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos) GetBinder(GetIndexBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    return(d._callingContext,d._argumentInfo);
    //}
    //public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos) GetBinder(GetMemberBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    return(d._callingContext,d._argumentInfo);
    //}
    //public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos) GetBinder(InvokeBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    return(d._callingContext,d._argumentInfo);
    //}
    //public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos,int CSharpBinderFlags) GetBinder(InvokeMemberBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    var s = d.Flags.ToString();
    //    var m_argumentInfo = d._argumentInfo;
    //    var value = s switch {
    //        "None" => CSharpBinderFlags.None,
    //        "ResultDiscarded" => CSharpBinderFlags.ResultDiscarded,
    //        _ => throw new NotSupportedException(s)
    //    };
    //    return (d.CallingContext, m_argumentInfo,(int)value);
    //}
    //public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos) GetBinder(SetIndexBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    return(d._callingContext,d._argumentInfo);
    //}
    //public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos) GetBinder(SetMemberBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    return(d._callingContext,d._argumentInfo);
    //}
    //public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos) GetBinder(UnaryOperationBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    return(d._callingContext,d._argumentInfo);
    //}
    //public static (CSharpArgumentInfoFlags flags, string name) GetFlagsName(CSharpArgumentInfo v1){
    //    dynamic d=new NonPublicAccessor(v1);
    //    return(d.Flags,d.Name);
    //}
}
internal enum BinderType:byte{
    BinaryOperationBinder,
    ConvertBinder,
    CreateInstanceBinder,
    DeleteIndexBinder,
    DeleteMemberBinder,
    GetIndexBinder,
    GetMemberBinder,
    InvokeBinder,
    InvokeMemberBinder,
    SetIndexBinder,
    SetMemberBinder,
    UnaryOperationBinder,
}
