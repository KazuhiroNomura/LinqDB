using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using LinqDB.Helpers;
using LinqDB.Sets;
// ReSharper disable LoopCanBeConvertedToQuery
// ReSharper disable InheritdocConsiderUsage
namespace LinqDB.Optimizers;

/// <summary>
/// <summary>
/// SetとIEnumerableを比較するため
/// </summary>
/// </summary>
public sealed class EnumerableSetEqualityComparer : EqualityComparer<object>{
    //public EnumerableSetEqualityComparer(){
    //}
    private readonly Optimizer.ExpressionEqualityComparer ExpressionEqualityComparer;
    public EnumerableSetEqualityComparer(Optimizer.ExpressionEqualityComparer ExpressionEqualityComparer){
        this.ExpressionEqualityComparer=ExpressionEqualityComparer;
    }
    private static readonly Type[] Types1 = new Type[1];
    private static readonly Type[] Types2 = new Type[2];
    private static readonly Type[] Types3 = new Type[3];
    private static readonly Type[] Types4 = new Type[4];
    private static readonly Type[] Types5 = new Type[5];
    private static readonly Type[] Types6 = new Type[6];
    private static readonly Type[] Types7 = new Type[7];
    private static readonly Type[] Types8 = new Type[8];
    private static readonly Dictionary<Type,Func<object,object>> Dictionary_Anonymous_ValueTuple = new();
    private static Type Get_ValueTuple(ILGenerator I,PropertyInfo[]Properties,int Index) {
        Type 共通2(Type GenericTypeDifinition,Type[] GenericArguments) {
            var Result= GenericTypeDifinition.MakeGenericType(GenericArguments);
            I.Newobj(Result.GetConstructor(GenericArguments));
            return Result;
        }
        void 共通1(Type[]Types) {
            for(var Offset = 0;Offset<Types.Length;Offset++) {
                Types[Offset]=Properties[Index+Offset].PropertyType;
            }
        }
        var Case = Properties.Length-Index;
        switch(Case) {
            case 1:
                共通1(Types1);
                return 共通2(typeof(ValueTuple<>),Types1);
            case 2:
                共通1(Types2);
                //Types2[0]=Properties[Index+0].PropertyType;
                //Types2[1]=Properties[Index+1].PropertyType;
                return 共通2(typeof(ValueTuple<,>),Types2);
            case 3:
                共通1(Types3);
                return 共通2(typeof(ValueTuple<,,>),Types3);
            case 4:
                共通1(Types4);
                return 共通2(typeof(ValueTuple<,,,>),Types4);
            case 5:
                共通1(Types5);
                return 共通2(typeof(ValueTuple<,,,,>),Types5);
            case 6:
                共通1(Types6);
                return 共通2(typeof(ValueTuple<,,,,,>),Types6);
            case 7:
                共通1(Types7);
                return 共通2(typeof(ValueTuple<,,,,,,>),Types7);
            default:
                for(var Offset = 0;Offset<7;Offset++) {
                    Types8[Offset]=Properties[Index+Offset].PropertyType;
                }
                Types8[7]=Get_ValueTuple(I,Properties,Index+7);
                return 共通2(typeof(ValueTuple<,,,,,,,>),Types8);
        }
    }
    private static List<object> ToList(IEnumerable x) {
        var List = new List<object>();
        var x_Enumerator = x.GetEnumerator();
        if(x_Enumerator.MoveNext()) {
            var x_Type = x_Enumerator.Current!.GetType();
            if(x_Type.IsAnonymous()) {
                if(!Dictionary_Anonymous_ValueTuple.TryGetValue(x_Type,out var M)) {
                    Types1[0]=typeof(object);
                    var D = new DynamicMethod("",typeof(object),Types1,typeof(EnumerableSetEqualityComparer),true) {
                        InitLocals=false
                    };
                    var I = D.GetILGenerator();
                    var Properties = x_Type.GetProperties();
                    foreach(var Property in Properties){
                        I.Ldarg_0();
                        I.Call(Property.GetMethod);
                    }
                    var ValueTuple=Get_ValueTuple(I,Properties,0);
                    I.Box(ValueTuple);
                    I.Ret();
                    M=(Func<object,object>)D.CreateDelegate(typeof(Func<object,object>));
                    Dictionary_Anonymous_ValueTuple.Add(x_Type,M);
                }
                do {
                    List.Add(M(x_Enumerator.Current));
                } while(x_Enumerator.MoveNext());
            } else {
                do {
                    List.Add(x_Enumerator.Current);
                } while(x_Enumerator.MoveNext());
            }
        }
        return List;
    }
    private static bool Groupingか(Type Type)=>
        Type.GetInterface(CommonLibrary.IGrouping2_FullName) is not null;
    private bool 比較(List<object> List_x, List<object> List_y){
        var List_x_Count=List_x.Count;
        if(List_x_Count!=List_y.Count) return false;
        for(var a=0; a<List_x_Count; a++){
            var x=List_x[a];
            if(x is not null) {
                if(Groupingか(x.GetType())) {
                    var x_Key = x.GetType().GetProperty(nameof(IGrouping<int,int>.Key))!;
                    //Debug.Assert(x_Key is not null,"x_Key  is not null");
                    var x_KeyValue = x_Key.GetMethod!.Invoke(x,Array.Empty<object>())!;
                    var b = a;
                    while(true) {
                        if(b==List_x_Count) return false;
                        var y = List_y[b];
                        var y_Type = y.GetType();
                        if(Groupingか(y_Type)) {
                            var y_Key = y_Type.GetProperty(nameof(IGrouping<int,int>.Key))!;
                            //Debug.Assert(y_Key is not null,"y_Key  is not null");
                            var y_KeyValue = y_Key.GetMethod!.Invoke(y,Array.Empty<object>())!;
                            if(this.Equals(x_KeyValue,y_KeyValue)) {
                                List_y[b]=List_y[a];
                                List_y[a]=y;
                                break;
                            }
                        } else {
                            if(this.Equals(x,y)) {
                                List_y[b]=List_y[a];
                                List_y[a]=y;
                                break;
                            }
                        }
                        b++;
                    }
                } else {
                    for(var b=a;b<List_x_Count;b++){
                        var y = List_y[b];
                        if(this.Equals(x,y)) {
                            List_y[b]=List_y[a];
                            List_y[a]=y;
                            break;
                        }
                    }
                    //var b = a;
                    //while(true) {
                    //    if(b==List_x_Count) return false;
                    //    var y = List_y[b];
                    //    if(this.Equals(x,y)) {
                    //        List_y[b]=List_y[a];
                    //        List_y[a]=y;
                    //        break;
                    //    }
                    //    b++;
                    //}
                }
            } else {
                var y = List_y[a];
                if(y is not null) return false;
            }
        }
        return true;
    }

    //private static readonly PropertyInfo Length = typeof(ITuple).GetProperty("Length");
    //private static readonly PropertyInfo Item = typeof(ITuple).GetProperty("Item");
    //private readonly Object[] Objects1= new Object[1];
    public override bool Equals(object? x,object? y){
        if(x==y) return true;
        if(x is null||y is null) return false;
        {
            if(x is ImmutableSet x0&&y is ImmutableSet y0) {
                var xl = ToList(x0);
                var yl = ToList(y0);
                return this.比較(xl,yl);
            }
        }
        {
            if(x is IEnumerable x0&&y is IEnumerable y0) {
                var xl = ToList(x0);
                var yl = ToList(y0);
                return this.比較(xl,yl);
            }
        }
        var x_GetType = x.GetType();
        var y_GetType = y.GetType();
        if(x_GetType.IsAnonymous()) {
            if(x_GetType==y_GetType) {
                var x_Properties = x_GetType.GetProperties();
                var x_Properties_Length = x_Properties.Length;
                var y_Properties = y_GetType.GetProperties();
                var y_Properties_Length = y_Properties.Length;
                if(x_Properties_Length!=y_Properties_Length) return false;
                for(var Index = 0;Index<x_Properties_Length;Index++) {
                    var x0 = x_Properties[Index].GetMethod!.Invoke(x,Array.Empty<object>())!;
                    var y0 = y_Properties[Index].GetMethod!.Invoke(y,Array.Empty<object>())!;
                    if(!this.Equals(x0,y0)) return false;
                }
                return true;
            } else{
                return false;
            }
        } else if(x_GetType.IsDisplay()){
            if(x_GetType==y_GetType){
                Debug.Assert(x_GetType.GetFields(BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Static).Length==0);
                var Flag=BindingFlags.Public|BindingFlags.Instance;
                var x_Fields = x_GetType.GetFields(Flag);
                var x_Fields_Length = x_Fields.Length;
                var y_Fields = y_GetType.GetFields(Flag);
                var y_Fields_Length = y_Fields.Length;
                if(x_Fields_Length!=y_Fields_Length) return false;
                for(var Index = 0;Index<x_Fields_Length;Index++) {
                    var x0 = x_Fields[Index].GetValue(x)!;
                    var y0 = y_Fields[Index].GetValue(y)!;
                    if(!this.Equals(x0,y0)) return false;
                }
                return true;
            } else{
                return false;
            }
        }else{
            switch(x,y){
                case(Delegate x0,Delegate y0):{
                    if(x0.Method!=y0.Method) return false;
                    if(!this.Equals(x0.Target,y0.Target))return false;
                    return true;
                }
                case(Expression x0,Expression y0):
                    return this.ExpressionEqualityComparer.Equals(x0,y0);
            }
            return x.Equals(y);
        }
        //if(x_GetType.IsAnonymous()) {
        //    if(y_GetType.IsAnonymous()) {
        //        var x_Properties = x_GetType.GetProperties();
        //        var x_Properties_Length = x_Properties.Length;
        //        var y_Properties = y_GetType.GetProperties();
        //        var y_Properties_Length = y_Properties.Length;
        //        if(x_Properties_Length!=y_Properties_Length) return false;
        //        for(var Index = 0;Index<x_Properties_Length;Index++) {
        //            var x0 = x_Properties[Index].GetMethod!.Invoke(x,Array.Empty<object>())!;
        //            var y0 = y_Properties[Index].GetMethod!.Invoke(y,Array.Empty<object>())!;
        //            if(!this.Equals(x0,y0)) return false;
        //        }
        //        return true;
        //    } else {
        //        var x_Properties = x_GetType.GetProperties();
        //        foreach(var x_Property in x_Properties) {
        //            object y0;
        //            var y_Field = x_GetType.GetField(x_Property.Name,BindingFlags.Instance|BindingFlags.Public);
        //            if(y_Field is not null) {
        //                y0=y_Field.GetValue(y)!;
        //            } else {
        //                var y_Property = x_GetType.GetProperty(x_Property.Name,BindingFlags.Instance|BindingFlags.Public);
        //                if(y_Property is not null) {
        //                    y0=y_Property.GetValue(y)!;
        //                } else {
        //                    return false;
        //                }
        //            }
        //            var x0 = x_Property.GetValue(x);
        //            if(!this.Equals(x0,y0)) return false;
        //        }
        //        return true;
        //    }
        //} else {
        //    return x.Equals(y);
        //}
    }
    /// <summary>
    /// キーのハッシュコードを返す
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override int GetHashCode(object obj)=>0;
}