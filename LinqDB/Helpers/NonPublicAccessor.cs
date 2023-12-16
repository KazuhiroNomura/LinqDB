using System;
using System.Dynamic;
using System.Reflection;
namespace LinqDB.Helpers;

/// <summary>
/// privateインスタンスメンバにアクセスできるdynamicクラス
/// </summary>
public class NonPublicAccessor:DynamicObject{
    private const BindingFlags Flags=BindingFlags.Instance|BindingFlags.Static|BindingFlags.NonPublic|BindingFlags.Public;
    private readonly object? Object;
    private readonly Type Type;
    private NonPublicAccessor(Type Type) {
        this.Object=null;
        this.Type=Type;
    }
    /// <summary>
    /// TypeとObjectを指定したコンストラクタ
    /// </summary>
    /// <param name="Type"></param>
    /// <param name="Object"></param>
    public NonPublicAccessor(Type Type,object Object) {
        this.Object=Object;
        this.Type=Type;
    }
    /// <summary>
    /// Objectを指定したコンストラクタ
    /// </summary>
    /// <param name="Object"></param>
    public NonPublicAccessor(object Object) {
        this.Object=Object;
        this.Type=Object.GetType();
    }
    private readonly object?[] SetObject=new object?[1];
    /// <summary>メンバー値を設定する演算の実装を提供します。<see cref="DynamicObject" /> クラスの派生クラスでこのメソッドをオーバーライドして、プロパティ値の設定などの演算の動的な動作を指定できます。</summary>
    /// <returns>操作が正常に終了した場合は true。それ以外の場合は false。このメソッドが false を返す場合、言語のランタイム バインダーによって動作が決まります (ほとんどの場合、言語固有の実行時例外がスローされます)。</returns>
    /// <param name="binder">動的演算を呼び出したオブジェクトに関する情報を提供します。binder.Name プロパティは、値の割り当て先のメンバーの名前を提供します。たとえば、sampleObject が <see cref="DynamicObject" /> クラスから派生したクラスのインスタンスである sampleObject.SampleProperty="Test" ステートメントの場合、binder.Name は "SampleProperty" を返します。メンバー名で大文字と小文字を区別するかどうかを binder.IgnoreCase プロパティで指定します。</param>
    /// <param name="value">メンバーに設定する値。たとえば、sampleObject が <see cref="DynamicObject" /> クラスから派生したクラスのインスタンスである sampleObject.SampleProperty="Test" の場合、<paramref name="value" /> は "Test" です。</param>
    public override bool TrySetMember(SetMemberBinder binder,object? value) {
        var Type=this.Type;
        var Name=binder.Name;
        var Field=Type.GetField(Name,Flags);
        if(Field is not null) {
            Field.SetValue(this.Object,value);
            return true;
        }
        var Property=Type.GetProperty(Name,Flags);
        if(Property is not null){
            var SetObject=this.SetObject;
            SetObject[0]=value;
            Property.SetMethod!.Invoke(this.Object,SetObject);
            return true;
        }
        var Method = Type.GetMethod(Name,Flags);
        if(Method is not null){
            var SetObject = this.SetObject;
            SetObject[0]=value;
            Method.Invoke(Method.IsStatic ? null : this.Object,SetObject);
            return true;
        }
        return false;
    }
    /// <summary>メンバー値を取得する演算の実装を提供します。<see cref="DynamicObject" /> クラスの派生クラスでこのメソッドをオーバーライドして、プロパティ値の取得などの演算の動的な動作を指定できます。</summary>
    /// <returns>操作が正常に終了した場合は true。それ以外の場合は false。このメソッドが false を返す場合、言語のランタイム バインダーによって動作が決まります (ほとんどの場合、実行時例外がスローされます)。</returns>
    /// <param name="binder">動的演算を呼び出したオブジェクトに関する情報を提供します。binder.Name プロパティは、動的演算の対象であるメンバーの名前を提供します。たとえば、sampleObject が <see cref="DynamicObject" /> クラスから派生したクラスのインスタンスである Console.WriteLine(sampleObject.SampleProperty) ステートメントの場合、binder.Name は "SampleProperty" を返します。メンバー名で大文字と小文字を区別するかどうかを binder.IgnoreCase プロパティで指定します。</param>
    /// <param name="result">取得操作の結果。たとえば、このメソッドがプロパティに対して呼び出された場合、プロパティ値を <paramref name="result" /> に割り当てることができます。</param>
    public override bool TryGetMember(GetMemberBinder binder,out object? result) {
        var Type=this.Type;
        var Name=binder.Name;
        {
            var Type0=Type;
            do{
                var Field=Type0.GetField(Name,Flags);
                if(Field is not null){
                    result=Field.GetValue(this.Object);
                    return true;
                }
                Type0=Type0.BaseType;
            } while(Type0 is not null);
        }
        {
            var Type0=Type;
            do{
                var Property=Type0.GetProperty(Name,Flags);
                if(Property is not null){
                    result=Property.GetMethod!.Invoke(this.Object,Array.Empty<object>());
                    return true;
                }
                Type0=Type0.BaseType;
            } while(Type0 is not null);
        }
        var NestedType=Type.GetNestedType(Name,Flags);
        if(NestedType is not null) {
            result=new NonPublicAccessor(NestedType);
            return true;
        }
        result=null;
        return false;
    }
    /// <summary>メンバーを呼び出す演算の実装を提供します。<see cref="DynamicObject" /> クラスの派生クラスでこのメソッドをオーバーライドして、メソッドの呼び出しなどの演算の動的な動作を指定できます。</summary>
    /// <returns>操作が正常に終了した場合は true。それ以外の場合は false。このメソッドが false を返す場合、言語のランタイム バインダーによって動作が決まります (ほとんどの場合、言語固有の実行時例外がスローされます)。</returns>
    /// <param name="binder">動的な演算に関する情報を提供します。binder.Name プロパティは、動的演算の対象であるメンバーの名前を提供します。たとえば、sampleObject が <see cref="DynamicObject" /> クラスから派生したクラスのインスタンスである sampleObject.SampleMethod(100) ステートメントの場合、binder.Name は "SampleMethod" を返します。メンバー名で大文字と小文字を区別するかどうかを binder.IgnoreCase プロパティで指定します。</param>
    /// <param name="args">呼び出し演算でオブジェクト メンバーに渡される引数。たとえば、sampleObject が <see cref="DynamicObject" /> クラスから派生している sampleObject.SampleMethod(100) ステートメントの場合、<paramref>
    ///         <name>args[0]</name>
    ///     </paramref>
    ///     と 100 は等価です。</param>
    /// <param name="result">メンバー呼び出しの結果。</param>
    public override bool TryInvokeMember(InvokeMemberBinder binder,object?[]? args,out object? result){
        //public override Boolean TryInvokeMember2(InvokeMemberBinder binder,Object[] args,out Object? result) {
        var args_Length=args!.Length;
        var Methods=this.Type.GetMethods(Flags|BindingFlags.Static);
        MethodInfo? 結果Method=null;
        foreach(var Method in Methods) {
            if(!string.Equals(Method.Name,binder.Name,StringComparison.Ordinal)) continue;
            var Parameters=Method.GetParameters();
            var Parameters_Length=Parameters.Length;
            if(Parameters_Length!=args_Length) continue;
            結果Method=Method;
        }
        if(結果Method is not null) {
            result=結果Method.Invoke(結果Method.IsStatic?null:this.Object,args);
            return true;
        }
        result=null;
        return false;
    }
}
