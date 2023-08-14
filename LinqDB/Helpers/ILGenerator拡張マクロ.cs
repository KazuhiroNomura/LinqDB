using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Diagnostics;
// ReSharper disable MemberCanBePrivate.Global
namespace LinqDB.Helpers;

/// <summary>
/// シグネチャ付きのEmitとしてラップする。
/// </summary>
public static class ILGenerator拡張マクロ{
    /// <summary>
    /// 値型がpushされているときにその参照をpushさせる。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Type"></param>
    /// <returns></returns>
    public static void M_Valueは参照(this ILGenerator I,Type Type){
        Debug.Assert(Type.IsValueType&&!Type.IsClass&&!Type.IsInterface||!Type.IsValueType);
        if(Type.IsValueType) {
            var 変数 = I.DeclareLocal(Type);
            I.Stloc(変数);
            I.Ldloca(変数);
        }
    }
    /// <summary>
    /// 値型を初期化してスタックに積む
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Type"></param>
    public static void M_Initobjで値型を初期化してスタックに積む(this ILGenerator I,Type Type) {
        var 変数 = I.DeclareLocal(Type);
        I.Ldloca(変数);
        I.Initobj(Type);
        I.Ldloc(変数);
    }

    /// <summary>
    /// メッセージボックスを表示するILの生成。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="type"></param>
    /// <param name="title"></param>
    public static void M_MessageBox_スタックをStringとする(this ILGenerator I,Type type,string title) {
        var L = I.M_DeclareLocal_Stloc(type);
        M_MessageBox(I,L,title);
    }
    /// <summary>
    /// メッセージボックスを表示するILの生成。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="title"></param>
    public static void M_MessageBox(this ILGenerator I,string title) {
        var L = I.DeclareLocal(typeof(int));
        M_MessageBox(I,L,title);
    }
    /// <summary>
    /// メッセージボックスを表示するILの生成。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="L"></param>
    public static void M_MessageBox(this ILGenerator I,LocalBuilder L) => M_MessageBox(I,L,"タイトルなし");
    //private static readonly Type[] StringTypes = { typeof(String) };
    //private static readonly Type[] StringStringTypes = { typeof(String),typeof(String) };
    /// <summary>
    /// メッセージボックスを表示するILの生成。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="L"></param>
    /// <param name="title"></param>
    public static void M_MessageBox(this ILGenerator I,LocalBuilder L,string title) {
        var T = L.LocalType;
        if(T==typeof(string)) {
            I.Emit(OpCodes.Ldloc,L);
        } else if(T.IsByRef||T.IsPointer||T.IsValueType) {
            I.Emit(OpCodes.Ldloc,L);
            var Type = T;
            if(T.IsByRef||T.IsPointer) {
                if(IntPtr.Size==4) {
                    I.Emit(OpCodes.Conv_I4);
                    Type=typeof(int);
                } else {
                    I.Emit(OpCodes.Conv_I8);
                    Type=typeof(long);
                }
            }
            var V = I.DeclareLocal(Type);
            I.Emit(OpCodes.Stloc,V);
            I.Emit(OpCodes.Ldloca,V);
            if(T==typeof(byte*)){
                I.Emit(OpCodes.Ldstr,IntPtr.Size==4?"X8":"X16");
                I.Emit(OpCodes.Call,Type.GetMethod(nameof(ToString),CommonLibrary.Types_String));
            } else {
                I.Emit(OpCodes.Call,Type.GetMethod(nameof(ToString),Type.EmptyTypes));
            }
        }
        I.Emit(OpCodes.Ldstr,title);
        I.Emit(OpCodes.Call,typeof(Console).GetMethod(nameof(Console.WriteLine),CommonLibrary.Types_String));
        I.Emit(OpCodes.Call,typeof(Console).GetMethod(nameof(Console.WriteLine),CommonLibrary.Types_String));
        //I.Emit(OpCodes.Call,typeof(MessageBox).GetMethod(nameof(MessageBox.Show),StringStringTypes));
        I.Emit(OpCodes.Pop);
    }
    //private static readonly Type[] ObjectObjectTypes={typeof(Object),typeof(Object)};
    //[Conditional("DEBUG")]
    //private static void M_EmitWriteLine(this ILGenerator I,String キャプション,LocalBuilder L) {
    //    //var StackFrame変数0=new StackFrame(0);
    //    //var row=StackFrame変数0.GetFileLineNumber();
    //    //var StackFrame変数=new StackFrame(1);
    //    //var Method=StackFrame変数.GetMethod();
    //    //Method=MethodBase.GetCurrentMethod();
    //    //var 行番号=StackFrame変数.GetFileLineNumber();
    //    var T=L.LocalType;
    //    if(T.IsPointer||T.IsByRef||T==typeof(IntPtr)) {
    //        I.Emit(OpCodes.Ldloc,L);
    //        Type Type;
    //        if(IntPtr.Size==4) {
    //            I.Emit(OpCodes.Conv_I4);
    //            Type=typeof(Int32);
    //        } else {
    //            I.Emit(OpCodes.Conv_I8);
    //            Type=typeof(Int64);
    //        }
    //        var V=I.M_DeclareLocal_Stloc(Type);
    //        I.Emit(OpCodes.Ldloca,V);
    //        if(IntPtr.Size==4) {
    //            I.Emit(OpCodes.Ldstr,"X8");
    //        } else {
    //            I.Emit(OpCodes.Ldstr,"X16");
    //        }
    //        I.Emit(OpCodes.Call,Type.GetMethod(nameof(ToString),new[] { typeof(String) }));
    //    } else if(T.IsPrimitive) {
    //        I.Emit(OpCodes.Ldloca,L);
    //        I.Emit(OpCodes.Call,T.GetMethod(nameof(ToString),Type.EmptyTypes));
    //    } else if(T==typeof(String)) {
    //        I.Emit(OpCodes.Ldloc,L);
    //    } else {
    //        I.Emit(OpCodes.Ldloca,L);
    //        I.Emit(OpCodes.Call,T.GetMethod(nameof(ToString),Type.EmptyTypes));
    //    }
    //    var 結果=I.DeclareLocal(typeof(String));
    //    //        I.Emit(OpCodes.Ldstr,キャプション+":行番号:"+行番号.ToString()+" "+Method.DeclaringType.FullName+"."+Method.Name);
    //    I.Emit(OpCodes.Ldstr,":"+キャプション+"("+L.LocalIndex+")");
    //    I.Emit(OpCodes.Call,typeof(String).GetMethod(nameof(String.Concat),ObjectObjectTypes));
    //    I.Emit(OpCodes.Stloc,結果);
    //    I.EmitWriteLine(結果);
    //}
    //public static void M_Ldloc_Ldc_I4_1_Add_Stloc(this ILGenerator I,LocalBuilder LocalBuilder) {
    //    I.Emit(OpCodes.Ldloc,LocalBuilder);
    //    I.Emit(OpCodes.Ldc_I4_1);
    //    I.Emit(OpCodes.Add);
    //    I.Emit(OpCodes.Stloc,LocalBuilder);
    //}
    ////public static void M_Ldloc_Ldobj(this ILGenerator I,LocalBuilder 行) {
    ////    I.Emit(OpCodes.Ldloc,行);
    ////    I.Emit(OpCodes.Ldobj,行.LocalType.GetElementType());
    ////}
    //public static void M_Ldloc_Ldc_I4_Add_Stloc(this ILGenerator I,LocalBuilder A,Int32 増分) {
    //    // Assert(A.LocalType.IsPointer||A.LocalType==typeof(Int64)||A.LocalType==typeof(Int32));
    //    I.Ldloc(A);
    //    I.Ldc_I4(増分);
    //    I.Add();
    //    I.Stloc(A);
    //}
    //public static void M_Ldloc_Ldc_I4_Add(this ILGenerator I,LocalBuilder LocalBuilder,Int32 OFFSET) {
    //    I.Emit(OpCodes.Ldloc,LocalBuilder);
    //    I.Emit(OpCodes.Ldc_I4,OFFSET);
    //    I.Emit(OpCodes.Add);
    //}
    //public static void M_Ldloc_Brfalse(this ILGenerator I,LocalBuilder LocalBuilder,Label ジャンプ先) {
    //    I.Emit(OpCodes.Ldloc,LocalBuilder);
    //    I.Emit(OpCodes.Brfalse,ジャンプ先);
    //}
    //public static void M_PopEmitWrite(this ILGenerator I,Type Type,String caption) {
    //    var x=I.DeclareLocal(Type);
    //    I.Stloc(x);
    //    I.M_EmitWriteLine(caption,x);
    //    I.Ldloc(x);
    //}
    //public static void M_PopEmitWrite(this ILGenerator I,Type Type) {
    //    I.M_PopEmitWrite(Type,"M_スタックの値をEmitWrite");
    //}
    /// <summary>
    /// LocalBuilderの宣言とStlocを同時に行う。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Type">LocalBuilderのType</param>
    /// <returns>LocalBuilder</returns>
    public static LocalBuilder M_DeclareLocal_Stloc(this ILGenerator I,Type Type){
        var r=I.DeclareLocal(Type);
        I.Stloc(r);
        return r;
    }

    /// <summary>
    /// DeclareLocal,Stloc,Ldlocを順に行う。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Type"></param>
    /// <returns></returns>
    public static LocalBuilder M_DeclareLocal_Stloc_Ldloc(this ILGenerator I,Type Type){
        var r=I.DeclareLocal(Type);
        I.Stloc(r);
        I.Ldloc(r);
        return r;
    }
    /// <summary>
    /// DefineLabelとMarkLabelを同時に行う。
    /// </summary>
    /// <param name="I"></param>
    /// <returns></returns>
    public static Label M_DefineLabel_MarkLabel(this ILGenerator I){
        var Label=I.DefineLabel();
        I.MarkLabel(Label);
        return Label;
    }
    //public static void M_ループ(this ILGenerator I,LocalBuilder 行先頭内,LocalBuilder 行,LocalBuilder 行末尾外,Action ループ内処理) {
    //    var ループ開始=I.M_ループ開始(行先頭内,行);
    //    var ループ先頭=I.M_DefineLabel_MarkLabel();
    //    ループ内処理();
    //    I.M_インクリメント(行);
    //    I.MarkLabel(ループ開始);
    //    //I.M_EmitWriteLine("行",行);
    //    //I.M_EmitWriteLine("行末尾外",行末尾外);
    //    I.M_ループ末尾(行,行末尾外,ループ先頭);
    //}
    //public static void M_Array(this ILGenerator I,LocalBuilder 要素数,LocalBuilder Array,LocalBuilder 行先頭内,LocalBuilder 行,LocalBuilder 行末尾外) {
    //    I.Ldnull(); I.Stloc(行先頭内); I.Ldnull(); I.Stloc(行末尾外);
    //    var ElementType=Array.LocalType.GetElementType();
    //    I.Ldloc(要素数); I.Newarr(ElementType); I.Stloc(Array);
    //    var スキップ=I.DefineLabel(); I.M_Ldloc_Brfalse(要素数,スキップ);
    //    var ElementPointerType=ElementType.MakePointerType();
    //    I.Ldloc(Array); I.Ldc_I4_0(); I.Ldelema(ElementType); I.Stloc(行先頭内);
    //    I.Ldloc(行先頭内); I.Stloc(行);
    //    I.Ldloc(要素数); I.Sizeof(ElementType); I.Mul(); I.Ldloc(行先頭内); I.Add(); I.Stloc(行末尾外);
    //    I.MarkLabel(スキップ);
    //}
    //private static Label M_ループ開始(this ILGenerator I,LocalBuilder 行先頭,LocalBuilder 行) {
    //    I.Emit(OpCodes.Ldloc,行先頭);
    //    I.Emit(OpCodes.Stloc,行);
    //    var ループ開始=I.DefineLabel();
    //    I.Br(ループ開始);
    //    return ループ開始;
    //}
    //public static Label M_ループ先頭(this ILGenerator I,LocalBuilder 行) {
    //    var ループ先頭=I.DefineLabel();
    //    I.MarkLabel(ループ先頭);
    //    return ループ先頭;
    //}

    //public static void M_インクリメント(this ILGenerator I,LocalBuilder 行) {
    //    I.Emit(OpCodes.Ldloc,行);
    //    I.Emit(OpCodes.Sizeof,行.LocalType.GetElementType());
    //    I.Emit(OpCodes.Add);
    //    I.Emit(OpCodes.Stloc,行);
    //}
    //public static void M_ループ末尾(this ILGenerator I,LocalBuilder 行,LocalBuilder 行末尾,Label ループ先頭) {
    //    I.Emit(OpCodes.Ldloc,行); I.Emit(OpCodes.Ldloc,行末尾); I.Emit(OpCodes.Blt_Un,ループ先頭);
    //}
    //public static void M_ループBreak(this ILGenerator I,LocalBuilder 行,LocalBuilder 行末尾,Label ループ先頭) {
    //    I.Emit(OpCodes.Ldloc,行); I.Emit(OpCodes.Ldloc,行末尾); I.Emit(OpCodes.Bge_Un,ループ先頭);
    //}
    //public static Label M_DefineLabel_Br(this ILGenerator I) {
    //    var Label=I.DefineLabel();
    //    I.Br(Label);
    //    return Label;
    //}
    private static readonly MethodInfo GetFieldFromHandle=typeof(FieldInfo).GetMethod(nameof(FieldInfo.GetFieldFromHandle),new[]{typeof(RuntimeFieldHandle)})!;
    /// <summary>
    /// Fieldをpushする。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Field"></param>
    public static void M_Metadata(this ILGenerator I,FieldInfo Field){
        I.Ldtoken(Field);
        I.Call(GetFieldFromHandle);
    }
    private static readonly MethodInfo GetMethodFromHandle=typeof(MethodBase).GetMethod(nameof(MethodBase.GetMethodFromHandle),new[]{typeof(RuntimeMethodHandle)})!;
    /// <summary>
    /// Methodをpushする。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Method"></param>
    public static void M_Metadata(this ILGenerator I,MethodInfo Method){
        I.Ldtoken(Method);
        I.Call(GetMethodFromHandle);
    }
    private static readonly MethodInfo GetTypeFromHandle=typeof(Type).GetMethod(nameof(Type.GetTypeFromHandle),new[] {typeof(RuntimeTypeHandle)})!;
    /// <summary>
    /// Constructorをpushする。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Constructor"></param>
    public static void M_Metadata(this ILGenerator I,ConstructorInfo Constructor){
        I.Ldtoken(Constructor);
        I.Call(GetMethodFromHandle);
    }
    /// <summary>
    /// Typeをpushする。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="type"></param>
    public static void M_Metadata(this ILGenerator I,Type type){
        I.Ldtoken(type);
        I.Call(GetTypeFromHandle);
    }
    /// <summary>
    /// 本来存在しないCneを表す。falseか0であればtrue,1を返す。
    /// </summary>
    /// <param name="I"></param>
    public static void M_Cne(this ILGenerator I){
        I.Emit(OpCodes.Ldc_I4_0);
        I.Emit(OpCodes.Ceq);
    }
}