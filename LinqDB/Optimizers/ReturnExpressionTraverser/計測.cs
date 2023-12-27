﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Linq;

using LinqDB.Helpers;
using LinqDB.Optimizers.Comparer;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException
namespace LinqDB.Optimizers.ReturnExpressionTraverser;
using static Common;
/// <summary>
/// 罫線だけを出力する
/// </summary>
[DebuggerDisplay("{Name} {Value}")]
public abstract class A計測 {
    internal static class Reflection {
        public static readonly MethodInfo Start = typeof(A計測).GetMethod(nameof(A計測.Start),Instance_NonPublic_Public)!;
        public static readonly MethodInfo Stop = typeof(A計測).GetMethod(nameof(A計測.Stop),Instance_NonPublic_Public)!;
        public static readonly MethodInfo StopReturn = typeof(A計測).GetMethod(nameof(A計測.StopReturn),Instance_NonPublic_Public)!;
    }
    private static void Line初期化(List<(A計測? 移動元,A計測? 移動先)>列Array,StringBuilder 制御罫線){
        for(var b = 1;b<列Array.Count;b++)
            if(列Array[b].移動元 is null)
                制御罫線[b]='　';
            else
                制御罫線[b]='│';
    }
    internal void 行(List<(A計測? 移動元,A計測? 移動先)> 列Array,A計測 辺,StringBuilder sb,StringBuilder 制御フロー行){
        var 親辺Array=this.List親辺;
        var 子辺Array=this.List子辺;
        var 親辺Array_Length=親辺Array.Count;
        var 子辺Array_Length=子辺Array.Count;
        Line初期化(列Array,制御フロー行);
        for(var a=0;a<制御フロー行.Length;a++)
            制御フロー行[a]=制御フロー行[a]switch{
                '┤'or'┬'or'┐'or'│'=>'│',
                '┴'or'┘'=>'　',
                _=>制御フロー行[a]
            };
        var 書き込みした右端列=-1;
        for(var a=0;a<親辺Array_Length;a++){
            var 親辺=親辺Array[a];
            var Count=列Array.Count;
            for(var b=0;b<Count;b++){
                var 列=列Array[b];
                if(列.移動元==親辺) {
                    //Debug.Assert(列.移動先==辺);
                    if(列.移動先==辺){
                        if(制御フロー行[b]=='│')
                            if(書き込みした右端列<b)
                                書き込みした右端列=b;
                        制御フロー行[b]='┘';
                        列Array[b]=(null,null);
                        goto 終了;
                    }
                }
            }
            for(var b=0;b<Count;b++){
                if(列Array[b].移動元 is null){
                    列Array[b]=(親辺,辺);
                    //Debug.Assert(書き込みした右端列<b); 
                    書き込みした右端列=b;
                    制御フロー行[b]='┐';
                    goto 終了;
                }
            }
            書き込みした右端列=制御フロー行.Length;
            列Array.Add((親辺,辺));
            制御フロー行.Append('┐');
            終了: ;
        }
        if(書き込みした右端列>0){
            for(var a=0;a<書き込みした右端列;a++){
                switch(制御フロー行[a]){
                    case '　':
                        制御フロー行[a]='─';
                        break;
                    case '┐':
                        制御フロー行[a]='┬';
                        break;
                    case '┘':
                        制御フロー行[a]='┴';
                        break;
                    case '│':
                        制御フロー行[a]='┼';
                        break;
                }
            }
            if(制御フロー行[書き込みした右端列]=='┘') 列Array[書き込みした右端列]=(null,null);
            //if(Line[書き込みした右端列]=='┐') Line[書き込みした右端列]='│';
        }
        var ループか=false;
        var 書き換えLineIndexEnd=-1;
        for(var a=0;a<子辺Array_Length;a++){
            var 子辺=子辺Array[a];
            var Count=列Array.Count;
            for(var b=0;b<Count;b++){
                var 列=列Array[b];
                if(列.移動元==辺) {
                    if(列.移動先==子辺) {
                        if(制御フロー行[b]=='│') {
                            Debug.Assert(書き換えLineIndexEnd<b);
                            if(書き換えLineIndexEnd<b) 書き換えLineIndexEnd=b;
                            制御フロー行[b]='┘';
                            ループか=true;
                            goto 終了;
                        }
                    }
                }
            }
            for(var b=0;b<Count;b++){
                if(列Array[b].移動元 is null){
                    //Debug.Assert(列Array[b].移動元 is null);
                    列Array[b]=(辺,子辺);
                    Debug.Assert(書き換えLineIndexEnd<b);
                    if(書き換えLineIndexEnd<b)
                        書き換えLineIndexEnd=b;
                    if(制御フロー行[b]=='┘')
                        制御フロー行[b]='┤';
                    else
                        制御フロー行[b]='┐';
                    goto 終了;
                } 
            }
            書き換えLineIndexEnd=制御フロー行.Length;
            列Array.Add((辺,子辺));
            制御フロー行.Append('┐');
            終了: ;
        }
        for(var a=0;a<書き換えLineIndexEnd;a++){
            //switch(Line[a]){
            //    case '　':Line[a]='─'; break;
            //    case '┘':Line[a]='┴'; break;
            //    case '│':Line[a]='┼'; break;
            //    case '┐':Line[a]='┬'; break;
            //}
            制御フロー行[a]=制御フロー行[a] switch{
                '　'=>'─',
                '┘'=>'┤',
                '│'=>'┼',
                '┐'=>'┬',
                _=>制御フロー行[a]
            };
        }
        if(ループか)
            列Array[書き換えLineIndexEnd]=(null,null);
        var 行=制御フロー行.ToString();
        Trace.WriteLine($"{行}{辺.Name} {辺.Value}");
        sb.AppendLine($"{行}{辺.Name} {辺.Value}");
    }
    public string 親コメント{get;set;}="";
    public string 子コメント{get;set;}="";
    //internal A計測? 親演算;
    internal string Name{get;set;}
    internal string? Value{get;set;}
    internal readonly List<A計測>List親辺=new();
    internal readonly List<A計測>List子辺=new();
    public static void 接続(A計測 親,A計測 子){
        親.List子辺.Add(子);
        子.List親辺.Add(親);
    }
    internal object ? Object;
    [NonSerialized]
    internal ParameterExpression? Parameter;
    internal A計測? 属する制御計測;
    internal int 番号;
    protected A計測(int 番号,ParameterExpression Parameter,string Name,string? Value,object  Object) {
        this.番号=番号;
        this.Parameter=Parameter;
        this.Name=Name;
        this.Value=Value;
        this.Object=Object;
    }
    protected A計測(int 番号,string Name,string? Value,object  Object) {
        this.番号=番号;
        this.Name=Name;
        this.Value=Value;
        this.Object=Object;
    }
    protected A計測(int 番号,string Name,string? Value){
        this.番号=番号;
        this.Name=Name;
        this.Value=Value;
        this.Object=null;
    }
    protected A計測(int 番号,string Name){
        this.番号=番号;
        this.Name=Name;
        this.Object=default;
    }
    protected A計測(int 番号){
        this.番号=番号;
        this.Name="";
        this.Object=default;
    }
    /// <summary>
    /// 子要素のプロファイル│。
    /// </summary>
    public readonly List<A計測> List子演算 = new();
    public void Clear(){
        this.List子演算.Clear();
        this.List親辺.Clear();
        this.List子辺.Clear();
    }
    protected string? 数値表;
    private protected static void Append(string s) {
        Console.Write(s);
    }
    private static void AppendLine(string s) {
        Console.WriteLine(s);
    }
    private static void AppendLine() {
        Console.WriteLine();
    }
    /// <summary>
    /// プロファイル結果をConsoleに出力
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization","CA1303:Do not pass literals as localized parameters",Justification = "<保留中>")]
    public void Consoleに出力() {
        AppendLine("┌───┬───┬───────────┬─────┐");
        AppendLine("│包含ms│排他ms│割合                  │ 呼出回数 │");
        AppendLine("│      │      ├───┬───┬───┤          │");
        AppendLine("│      │      │部分木│  節  │  節  │          │");
        AppendLine("│      │      │───│───│───│          │");
        AppendLine("│      │      │全体木│全体木│部分木│          │");
        AppendLine("├───┼───┼───┼───┼───┼─────┤");
        this.割合計算();

        StringBuilder sb=new();
        var line=new StringBuilder();
        this.展開(sb,line);
        AppendLine("└───┴───┴───┴───┴───┴─────┘");
    }
    private const string 空表=
                      "│      │      │                      │          │";
    //public void Consoleに出力(StringBuilder 表) {
    //    表.AppendLine("┌───┬───┬───────────┬─────┐");
    //    表.AppendLine("│包含ms│排他ms│割合                  │ 呼出回数 │");
    //    表.AppendLine("│      │      ├───┬───┬───┤          │");
    //    表.AppendLine("│      │      │部分木│  節  │  節  │          │");
    //    表.AppendLine("│      │      │───│───│───│          │");
    //    表.AppendLine("│      │      │全体木│全体木│部分木│          │");
    //    表.AppendLine("├───┼───┼───┼───┼───┼─────┤");
    //    //0                                                  2425      5556  80   
    //    //├───┼───┼───┼───┼───┼─────┤└───┐　　│
    //    this.割合計算();
    //    var 制御罫線=new StringBuilder("　");
    //    var 列Array=new List<(A計測? 移動元,A計測? 移動先)>{(null,null)};
    //    this.データ制御フロー(列Array,表,制御罫線,"");
    //    表.AppendLine("└───┴───┴───┴───┴───┴─────┘");
    //}
    public void Consoleに出力(List<string>ListString) {
        ListString.Add("┌───┬───┬───────────┬─────┐");
        ListString.Add("│包含ms│排他ms│割合                  │ 呼出回数 │");
        ListString.Add("│      │      ├───┬───┬───┤          │");
        ListString.Add("│      │      │部分木│  節  │  節  │          │");
        ListString.Add("│      │      │───│───│───│          │");
        ListString.Add("│      │      │全体木│全体木│部分木│          │");
        ListString.Add("├───┼───┼───┼───┼───┼─────┤");
        var offset=ListString.Count;
        //0                                                  2425      5556  80   
        //├───┼───┼───┼───┼───┼─────┤└───┐　　│
        this.割合計算();
        var 制御罫線=new StringBuilder("　");
        var 列Array=new List<(A計測? 移動元,A計測? 移動先)>{(null,null)};
        var 最大行数=0;
        var List制御=new List<string>();
        this.データフロー(ListString,"",ref 最大行数);
        this.制御フロー(列Array,List制御,制御罫線);
        Debug.Assert(ListString.Count-offset==List制御.Count);
        var Count=List制御.Count;
        var Span制御=CollectionsMarshal.AsSpan(List制御);
        var SpanString=CollectionsMarshal.AsSpan(ListString);
        for(var a=0;a<Count;a++){
            ref var s=ref SpanString[offset+a];
            s+=new string(' ',最大行数-半角換算文字数(s))+Span制御[a];
            //var s=ListString[offset+a];
            //ListString[offset+a]=s+new string(' ',最大行数-半角換算文字数(s))+List制御[a];
        }
        //this.データ制御フロー(列Array,表,制御罫線,"");
        ListString.Add("└───┴───┴───┴───┴───┴─────┘");
    }
    public void Consoleに出力(StringBuilder 表, List計測 List制御計測) {
        表.AppendLine("┌───┬───┬───────────┬─────┐");
        表.AppendLine("│包含ms│排他ms│割合                  │ 呼出回数 │");
        表.AppendLine("│      │      ├───┬───┬───┤          │");
        表.AppendLine("│      │      │部分木│  節  │  節  │          │");
        表.AppendLine("│      │      │───│───│───│          │");
        表.AppendLine("│      │      │全体木│全体木│部分木│          │");
        表.AppendLine("├───┼───┼───┼───┼───┼─────┤");
        this.割合計算();
        //for(var a=List制御計測.GetLineEnumerator();a.MoveNext();){
        //    Trace.WriteLine(a.Current);
        //}
        var Enumerator=List制御計測.GetLineEnumerator();
        var r=Enumerator.MoveNext();
        var 行=new StringBuilder();
        var 列Array=new List<(A計測? 移動元,A計測? 移動先)>();
        this.データ制御フロー(列Array,表,行,"");
        表.AppendLine("└───┴───┴───┴───┴───┴─────┘");
    }
    private protected abstract void フロー(List<(A計測? 移動元,A計測? 移動先)> 列Array,StringBuilder 制御罫線);
    private bool 親フロー(List<(A計測? 移動元,A計測? 移動先)>列Array,StringBuilder 制御罫線){
        var 親辺Array=this.List親辺.ToArray();
        var 親辺Array_Length=親辺Array.Length;
        for(var a=0;a<制御罫線.Length;a++){
            制御罫線[a]=制御罫線[a] switch{
                '┌' or'┐' or'┬'=>'│',
                _=>制御罫線[a]
            };
        }
        //Debug.Assert(!(親辺Array_Length>0&&this.List子辺.Count>0));
        if(親辺Array_Length==0){
            //for(var a=0;a<制御罫線.Length;a++)
            //    if(制御罫線[a] is'┬' or'┐')
            //        制御罫線[a]='│';
            //for(var a = 1;a<制御罫線.Length;a++)
            //    if(制御罫線[a] is '─')
            //        制御罫線[a]='┼';
            return false;
        } else{
            //Line初期化(列Array,制御罫線);
            制御罫線[0]='┌';
            //Append(制御罫線,"┌");
        }
        var 書き込みした右端列=-1;
        for(var a=0;a<親辺Array_Length;a++){
            var 親辺=親辺Array[a];
            var Count=列Array.Count;
            for(var b=1;b<Count;b++){
                var 列=列Array[b];
                //Debug.Assert(列.移動元==親辺);
                if(列.移動元==親辺) {
                    if(列.移動先==this){
                        if(制御罫線[b]!='┘')
                            if(書き込みした右端列<b)
                                書き込みした右端列=b;
                        制御罫線[b]='┘';
                        goto 終了;
                    }
                }
            }
            for(var b=1;b<Count;b++){
                if(制御罫線[b]is'　'){
                    列Array[b]=(親辺,this);
                    //Debug.Assert(書き込みした右端列<b); 
                    書き込みした右端列=b;
                    制御罫線[書き込みした右端列]='┐';
                    goto 終了;
                }
            }
            書き込みした右端列=制御罫線.Length;
            列Array.Add((親辺,this));
            Append(制御罫線,"┐");
            終了: ;
        }
        if(書き込みした右端列>0){
            for(var a=1;a<書き込みした右端列;a++){
                switch(制御罫線[a]){
                    case '　':
                        制御罫線[a]='─';
                        break;
                    case '┐':
                        制御罫線[a]='┬';
                        break;
                    case '┘':
                        制御罫線[a]='┴';
                        列Array[a]=(null,null);
                        break;
                    case '│':
                        制御罫線[a]='┼';
                        break;
                }
            }
            if(制御罫線[書き込みした右端列]=='┘') 列Array[書き込みした右端列]=(null,null);
            //for(var a=書き込みした右端列+1;a<制御罫線.Length;a++){
            //    switch(制御罫線[a]){
            //        case '┐':
            //            制御罫線[a]='│';
            //            break;
            //    }
            //}
        }
        return true;
    }
    private void 子フロー(List<(A計測? 移動元,A計測? 移動先)>列Array,StringBuilder 制御罫線){
        var 子辺Array=this.List子辺.ToArray();
        var 子辺Array_Length=子辺Array.Length;
        //if(子辺Array_Length<=0)return;
        //Line初期化(列Array,制御罫線);
        if(子辺Array_Length==0){
            //制御罫線[0]='　';
            for(var a = 1;a<制御罫線.Length;a++)
                if(制御罫線[a] is '└'or'┴' or '┘'or'─')
                    制御罫線[a]='　';
                else if(制御罫線[a] is '\u253c')
                    制御罫線[a]='│';
            //if(制御罫線[0] is'│'){
            //    制御罫線[0]='└';
            //    //for(var a = 1;a<制御罫線.Length;a++)
            //    //    if(制御罫線[a] is '　')
            //    //        制御罫線[a]='┐';
            //    //else if(制御罫線[a] is '┼')
            //    //    制御罫線[a]='│';
            //} else 
            if(制御罫線[0] is '└') {
                制御罫線[0]='　';
                for(var a = 1;a<制御罫線.Length;a++)
                    if(制御罫線[a] is '┼')
                        制御罫線[a]='│';
            }else
                制御罫線[0]='│';
        } else{
            制御罫線[0]='└';
            for(var a = 1;a<制御罫線.Length;a++)
                if(制御罫線[a] is '└'or'┴' or '┘'or'─')
                    制御罫線[a]='　';
        }
        var ループか=false;
        var 書き換えLineIndexEnd=-1;
        for(var a=0;a<子辺Array_Length;a++){
            var 子辺=子辺Array[a];
            var Count=列Array.Count;
            for(var b=1;b<Count;b++){
                var 列=列Array[b];

                if(列.移動元==this) {
                    if(列.移動先==子辺) {
                        if(制御罫線[b]=='│') {
                            Debug.Assert(書き換えLineIndexEnd<b);
                            if(書き換えLineIndexEnd<b) 書き換えLineIndexEnd=b;
                            制御罫線[b]='┘';
                            ループか=true;
                            goto 終了;
                        }
                    }
                }
            }
            for(var b=1;b<Count;b++){
                if(制御罫線[b]=='　'){
                    Debug.Assert(列Array[b].移動元 is null);
                    列Array[b]=(this,子辺);
                    Debug.Assert(書き換えLineIndexEnd<b);
                    if(書き換えLineIndexEnd<b)
                        書き換えLineIndexEnd=b;
                    制御罫線[b]='┐';
                    goto 終了;
                } 
            }
            書き換えLineIndexEnd=制御罫線.Length;
            列Array.Add((this,子辺));
            制御罫線.Append('┐');
            終了: ;
        }
        for(var a=1;a<書き換えLineIndexEnd;a++){
            switch(制御罫線[a]){
                case '　':制御罫線[a]='─'; break;
                //case '┘':
                //    Line[a]='┴';
                //    break;
                case '│':制御罫線[a]='┼'; break;
                case '┐':制御罫線[a]='┬'; break;
            }
        }
        if(ループか)
            列Array[書き換えLineIndexEnd]=(null,null);
    }

    //private string 前回のフロー="";
    private static string 文字列(string input,int 最大文字数){
        return input+new string('　',最大文字数-input.Length);
    }
    private static readonly Encoding Shift_JIS;
    static A計測(){
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        Shift_JIS=Encoding.GetEncoding("shift_jis");
    }
    private static int 半角換算文字数(string str){
        return Shift_JIS.GetByteCount(str);
    }

    private protected static void Append(StringBuilder sb,string s){
        sb.Append(s);
        Trace.Write(s);
    }
    private static void Append(List<string>sb,string s){
        sb.Add(s);
        Trace.WriteLine(s);
    }
    private static void AppendLine(StringBuilder sb){
        sb.AppendLine();
        Trace.WriteLine("");
    }
    private void データ制御フロー(List<(A計測? 移動元,A計測? 移動先)> 列Array,StringBuilder 全行,StringBuilder 制御罫線,string ツリー罫線){
        const int ツリー最大文字数=50;
        Append(全行,this.数値表);
        //全行.Append(this.数値表);

        var 行=ツリー罫線+this.Name;
        //制御罫線.Append(ツリー罫線);
        //Debug.Assert(XElement is not null);
        //制御罫線.Append(this.Name);
        //制御罫線.Append(new string(' ',ツリー最大文字数-半角換算文字数(制御罫線.ToString())));
        //行.Append("@@@");
        //var ツリー罫線string=ツリー罫線.ToString();
        //全行.Append(ツリー罫線string);
        ////var 空文字数=半角換算文字数(制御罫線.ToString());
        ////sb.Append(" "+this.Value);
        //全行.Append(制御罫線.ToString());
        //全行.AppendLine();
        //制御罫線.Clear();
        //制御罫線.Append(空表);
        //制御罫線.Append(ツリー罫線);
        if(this.Value is not null)
            行+=' '+this.Value;
        Append(全行,行);
        //全行.Append(行);
        var count=半角換算文字数(行);
        Append(全行,new string(' ',ツリー最大文字数-count));
        //全行.Append(new string(' ',ツリー最大文字数-count));
        this.フロー(列Array,制御罫線);
        //if(this.親フロー(列Array,制御罫線)){
        //    //this.子フロー(列Array,制御罫線);

        //} else{
        //    this.子フロー(列Array,制御罫線);
        //}
        var l0=制御罫線.ToString();
        Append(全行,制御罫線.ToString());
        AppendLine(全行);
        var List計測 = this.List子演算;
        var List計測_Count_1 = List計測.Count-1;
        if(List計測_Count_1>=0) {
            if(ツリー罫線.Length>0){
                var ツリー罫線前半=ツリー罫線[..^1];
                var 罫線 = ツリー罫線[^1];
                // ReSharper disable once ConvertIfStatementToSwitchStatement
                if(罫線=='├')
                    ツリー罫線=ツリー罫線前半+'│';
                else if(罫線=='└')
                    ツリー罫線=ツリー罫線前半+'　';
            }
            var ツリー罫線0=ツリー罫線+'├';
            for(var a = 0;a<List計測_Count_1;a++)
                List計測[a].データ制御フロー(列Array,全行,制御罫線,ツリー罫線0);
            List計測[List計測_Count_1].データ制御フロー(列Array,全行,制御罫線,ツリー罫線+'└');
        }
    }
    private void データフロー(List<string> ListString,string ツリー罫線,ref int 最大行数){
        //Append(全行,this.数値表);
        //全行.Append(this.数値表);
        var 行=$"{this.数値表}{this.番号,3}{ツリー罫線}";
        if(this is not 仮想ノード){
            行+=this.Name;
            if(this.Value is not null) 行+=' '+this.Value;
        }
        Append(ListString,行);
        var 行数=半角換算文字数(行);
        if(最大行数<行数) 最大行数=行数;
        var List計測 = this.List子演算;
        var List計測_Count_1 = List計測.Count-1;
        if(List計測_Count_1>=0) {
            if(ツリー罫線.Length>0){
                var ツリー罫線前半=ツリー罫線[..^1];
                var 罫線 = ツリー罫線[^1];
                // ReSharper disable once ConvertIfStatementToSwitchStatement
                if(罫線=='├')
                    ツリー罫線=ツリー罫線前半+'│';
                else if(罫線=='└')
                    ツリー罫線=ツリー罫線前半+'　';
            }
            //var ツリー罫線0=ツリー罫線+'├';
            for(var a=0;a<List計測_Count_1;a++){
                char c;
                //if(List計測[a+1] is 仮想ノード){
                //    行+=ツリー罫線[..^1];
                //    c='└';
                //} else{
                //    行+=ツリー罫線;
                //    c='├';
                //}
                行+=ツリー罫線;
                c='├';
                List計測[a].データフロー(ListString,ツリー罫線+c,ref 最大行数);
            }
            {
                char c;
                //if(List計測[List計測_Count_1] is 仮想ノード){
                //    行+=ツリー罫線[..^1];
                //    c='　';
                //} else{
                //    行+=ツリー罫線;
                //    c='└';
                //}
                行+=ツリー罫線;
                c='└';
                List計測[List計測_Count_1].データフロー(ListString,ツリー罫線+c,ref 最大行数);
            }
        }
    }
    private void 制御フロー(List<(A計測? 移動元,A計測? 移動先)> 列Array,List<string> ListString,StringBuilder 制御罫線){
        if(this.親フロー(列Array,制御罫線)) {
            //this.子フロー(列Array,制御罫線);

        } else {
            this.子フロー(列Array,制御罫線);
        }
        //this.フロー(列Array,制御罫線);
        Append(ListString,制御罫線.ToString());
        var List計測 = this.List子演算;
        var List計測_Count_1 = List計測.Count-1;
        if(List計測_Count_1>=0) {
            for(var a = 0;a<List計測_Count_1;a++)
                List計測[a].制御フロー(列Array,ListString,制御罫線);
            List計測[List計測_Count_1].制御フロー(列Array,ListString,制御罫線);
        }
    }
    private void 展開2(List<(A計測? 移動元,A計測? 移動先)> 列Array,StringBuilder 罫線ツリー行,StringBuilder インデント,StringBuilder 制御フロー行) {
        var 親辺Array=this.List親辺;
        var 子辺Array=this.List子辺;
        var 親辺Array_Length=親辺Array.Count;
        var 子辺Array_Length=子辺Array.Count;
        Line初期化(列Array,制御フロー行);
        for(var a=0;a<制御フロー行.Length;a++)
            制御フロー行[a]=制御フロー行[a]switch{
                '┤'or'┬'or'┐'or'│'=>'│',
                '┴'or'┘'=>'　',
                _=>制御フロー行[a]
            };
        var 書き込みした右端列=-1;
        for(var a=0;a<親辺Array_Length;a++){
            var 親辺=親辺Array[a];
            var Count=列Array.Count;
            for(var b=0;b<Count;b++){
                var 列=列Array[b];
                if(列.移動元==親辺) {
                    //Debug.Assert(列.移動先==辺);
                    if(列.移動先==this){
                        if(制御フロー行[b]=='│')
                            if(書き込みした右端列<b)
                                書き込みした右端列=b;
                        制御フロー行[b]='┘';
                        列Array[b]=(null,null);
                        goto 終了;
                    }
                }
            }
            for(var b=0;b<Count;b++){
                if(列Array[b].移動元 is null){
                    列Array[b]=(親辺,this);
                    //Debug.Assert(書き込みした右端列<b); 
                    書き込みした右端列=b;
                    制御フロー行[b]='┐';
                    goto 終了;
                }
            }
            書き込みした右端列=制御フロー行.Length;
            列Array.Add((親辺,this));
            制御フロー行.Append('┐');
            終了: ;
        }
        if(書き込みした右端列>0){
            for(var a=0;a<書き込みした右端列;a++){
                switch(制御フロー行[a]){
                    case '　':
                        制御フロー行[a]='─';
                        break;
                    case '┐':
                        制御フロー行[a]='┬';
                        break;
                    case '┘':
                        制御フロー行[a]='┴';
                        break;
                    case '│':
                        制御フロー行[a]='┼';
                        break;
                }
            }
            if(制御フロー行[書き込みした右端列]=='┘') 列Array[書き込みした右端列]=(null,null);
            //if(Line[書き込みした右端列]=='┐') Line[書き込みした右端列]='│';
        }
        var 行0=制御フロー行.ToString();
        var ループか=false;
        var 書き換えLineIndexEnd=-1;
        for(var a=0;a<子辺Array_Length;a++){
            var 子辺=子辺Array[a];
            var Count=列Array.Count;
            for(var b=0;b<Count;b++){
                var 列=列Array[b];
                if(列.移動元==this) {
                    if(列.移動先==子辺) {
                        if(制御フロー行[b]=='│') {
                            Debug.Assert(書き換えLineIndexEnd<b);
                            if(書き換えLineIndexEnd<b) 書き換えLineIndexEnd=b;
                            制御フロー行[b]='┘';
                            ループか=true;
                            goto 終了;
                        }
                    }
                }
            }
            for(var b=0;b<Count;b++){
                if(列Array[b].移動元 is null){
                    //Debug.Assert(列Array[b].移動元 is null);
                    列Array[b]=(this,子辺);
                    Debug.Assert(書き換えLineIndexEnd<b);
                    if(書き換えLineIndexEnd<b)
                        書き換えLineIndexEnd=b;
                    if(制御フロー行[b]=='┘')
                        制御フロー行[b]='┤';
                    else
                        制御フロー行[b]='┐';
                    goto 終了;
                } 
            }
            書き換えLineIndexEnd=制御フロー行.Length;
            列Array.Add((this,子辺));
            制御フロー行.Append('┐');
            終了: ;
        }
        for(var a=0;a<書き換えLineIndexEnd;a++){
            //switch(Line[a]){
            //    case '　':Line[a]='─'; break;
            //    case '┘':Line[a]='┴'; break;
            //    case '│':Line[a]='┼'; break;
            //    case '┐':Line[a]='┬'; break;
            //}
            制御フロー行[a]=制御フロー行[a] switch{
                '　'=>'─',
                '┘'=>'┤',
                '│'=>'┼',
                '┐'=>'┬',
                _=>制御フロー行[a]
            };
        }
        if(ループか)
            列Array[書き換えLineIndexEnd]=(null,null);
        var 行1=制御フロー行.ToString();
        罫線ツリー行.Append(this.数値表??"");//ヘッダとは各種数値を表の1行としての文字列
        罫線ツリー行.Append(インデント);//インデントとは演算ツリー
        罫線ツリー行.AppendLine(行0);
        罫線ツリー行.Append(new String('　',this.数値表!.Length));
        罫線ツリー行.Append(new String('　',インデント.Length));
        罫線ツリー行.AppendLine(行1);
        //line.Append(this.Name);
        //if(this.Value is not null)
        //    line.Append(' '+this.Value);
        //sb.Append(" "+this.Value);
        罫線ツリー行.AppendLine();
        var List計測 = this.List子演算;
        var List計測_Count_1 = List計測.Count-1;
        if(List計測_Count_1>=0) {
            var インデント_Length_1 = インデント.Length-1;
            if(インデント_Length_1>=0) {
                var 罫線 = インデント[インデント_Length_1];
                // ReSharper disable once ConvertIfStatementToSwitchStatement
                if(罫線=='├')
                    インデント[^1]='│';
                else if(罫線=='└')
                    インデント[^1]='　';
            }
            インデント[^1]='├';
            for(var a = 0;a<List計測_Count_1;a++)
                List計測[a].展開(罫線ツリー行,インデント);
            インデント[^1]='├';
            List計測[List計測_Count_1].展開(罫線ツリー行,インデント);
        }
    }
    private void 展開(StringBuilder インデント,StringBuilder sb) {
        Append(this.数値表??"");
        Append(インデント.ToString());
        //Debug.Assert(XElement is not null);
        Append(this.Name);
        if(this.Value is not null) {
            Append(' '+this.Value);
        }
        AppendLine();
        var List計測 = this.List子演算;
        var List計測_Count_1 = List計測.Count-1;
        if(List計測_Count_1>=0){
            var インデント_Length_1 = インデント.Length-1;
            if(インデント_Length_1>=0) {
                var 罫線 = インデント[^1];
                // ReSharper disable once ConvertIfStatementToSwitchStatement
                if(罫線=='├')
                    インデント[^1]='│';
                else if(罫線=='└')
                    インデント[^1]='　';
            }
            インデント.Append('├');
            for(var a = 0;a<List計測_Count_1;a++)
                List計測[a].展開(インデント,sb);
            インデント[^1]='└';
            List計測[List計測_Count_1].展開(インデント,sb);
        }
    }
    /// <summary>
    /// Stopwatchのミリ秒を返す。
    /// </summary>
    internal abstract long ms {
        get;
    }
    /// <summary>
    /// 計測開始
    /// </summary>
    protected abstract A計測 Start();
    /// <summary>
    /// 計測終了
    /// </summary>
    protected abstract void Stop();
    internal abstract void 割合計算(long 全体木のms);
    internal abstract void 割合計算();
    /// <summary>
    /// 戻り値のある計測終了
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item"></param>
    /// <returns></returns>
    protected abstract T StopReturn<T>(T item);
}
[Serializable]
public  class 計測しない:A計測 {
    public 計測しない(int 番号,ParameterExpression Parameter,string Name,string? Value) : base(番号,Parameter,Name,Value,Parameter) {
    }
    public 計測しない(int 番号,string Name,string? Value,object Object) : base(番号,Name,Value,Object) {
    }
    public 計測しない(int 番号,string Name) : base(番号,Name) {
    }
    internal override long ms {
        get {
            long r = 0;
            foreach(var 子 in this.List子演算) {
                r+=子.ms;
            }
            return r;
        }
    }

    internal override void 割合計算(long 全体木のms) {
        foreach(var 子 in this.List子演算) {
            子.割合計算(全体木のms);
        }
        var sb = new StringBuilder();
        sb.Append('│');
        sb.Append($"{this.ms,6}");
        sb.Append("│      │");
        sb.Append((全体木のms==0 ? 0 : (double)this.ms/全体木のms).ToString("0.0000",CultureInfo.InvariantCulture));
        sb.Append("│      │      │          │");
        this.数値表=sb.ToString();
    }
    private protected override void フロー(List<(A計測? 移動元,A計測? 移動先)> 列Array,StringBuilder 制御罫線){
    }
    internal override void 割合計算() => this.割合計算(this.ms);
    protected override A計測 Start() => this;
    protected override void Stop() { }
    protected override T StopReturn<T>(T item) => item;
}
[Serializable]
public class 仮想ノード:計測しない {
    public 仮想ノード(ParameterExpression Parameter,string Name,string? Value) : base(-1,Parameter,Name,Value) {
    }
    public 仮想ノード(string Name,string? Value,object Object) : base(-1,Name,Value,Object) {
    }
    public 仮想ノード(string Name) : base(-1,Name) {
    }
    internal override long ms=>0;

    internal override void 割合計算(long 全体木のms) {
        this.数値表="│      │      │　　　│　　　│　　　│          │";
    }
    internal override void 割合計算() {}
    protected override A計測 Start() => this;
    protected override void Stop() { }
    protected override T StopReturn<T>(T item) => item;
}
public abstract class 計測する:A計測{
    protected 計測する(int 番号,ParameterExpression Parameter,string Name,string? Value,object Object) : base(番号,Parameter,Name,Value,Object) {
    }
    protected 計測する(int 番号,string Name,string? Value,object Object) : base(番号,Name,Value,Object) {
    }
    protected 計測する(int 番号,string Name,string? Value) : base(番号,Name,Value) {
    }
    public 計測する(int 番号) : base(番号) {
    }
    /// <summary>
    /// 全体木に対する木のms割合
    /// </summary>
    [NonSerialized]
    private double 全体木に対する木のms割合;
    /// <summary>
    /// 全体木に対する節のms割合
    /// </summary>
    [NonSerialized]
    private double 全体木に対する節のms割合;
    /// <summary>
    /// 木に対する節のms割合
    /// </summary>
    [NonSerialized]
    private double 木に対する節のms割合;
    [NonSerialized]
    private readonly Stopwatch Stopwatch = new();
    private long 呼出回数;
    internal override long ms => this.Stopwatch.ElapsedMilliseconds;

    /// <summary>
    /// 計測開始
    /// </summary>
    protected override A計測 Start() {
        this.Stopwatch.Start();
        this.呼出回数++;
        return this;
    }

    /// <summary>
    /// 計測終了
    /// </summary>
    protected override void Stop() => this.Stopwatch.Stop();
    protected override T StopReturn<T>(T item) {
        this.Stopwatch.Stop();
        return item;
    }
    internal override void 割合計算(long 全体木のms) {
        var 木ms = this.ms;
        var 節ms = 木ms;
        foreach(var 子 in this.List子演算) {
            子.割合計算(全体木のms);
            節ms-=子.ms;
        }
        this.全体木に対する木のms割合=全体木のms==0 ? 0 : (double)this.ms/全体木のms;
        this.全体木に対する節のms割合=全体木のms==0 ? 0.0 : (double)節ms/全体木のms;
        this.木に対する節のms割合=木ms==0 ? 0.0 : (double)節ms/木ms;
        var sb = new StringBuilder();
        sb.Append('│');
        void 共通ms(long Value) {
            sb.Append($"{Value,6}");
            sb.Append('│');
        }
        void 共通割合(double 割合) {
            sb.Append(割合.ToString("0.0000",CultureInfo.InvariantCulture));
            sb.Append('│');
        }
        共通ms(木ms);
        共通ms(this.排他ms);
        共通割合(this.全体木に対する木のms割合);
        共通割合(this.全体木に対する節のms割合);
        共通割合(this.木に対する節のms割合);
        var 呼出回数 = this.呼出回数;
        sb.Append(呼出回数>=10000000000 ? "MAX       " : $"{呼出回数,10}");
        sb.Append('│');
        this.数値表=sb.ToString();
    }
    internal override void 割合計算() {
        this.全体木に対する木のms割合=0.0;
        this.全体木に対する節のms割合=0.0;
        this.木に対する節のms割合=0.0;
        this.割合計算(this.ms);
    }
    private long 排他ms {
        get {
            var ElapsedMilliseconds = this.Stopwatch.ElapsedMilliseconds;
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach(var 子 in this.List子演算)
                ElapsedMilliseconds-=子.ms;
            return ElapsedMilliseconds;
        }
    }
}
public  class 計測する親:計測する{
    public 計測する親(int 番号,ParameterExpression Parameter,string Name,string? Value,object  Object) : base(番号,Parameter,Name,Value,Object) {
    }
    public 計測する親(int 番号,string Name,string? Value,object Object) : base(番号,Name,Value,Object) {
    }
    public 計測する親(int 番号,string Name,string? Value) : base(番号,Name,Value) {
    }
    public 計測する親(int 番号) : base(番号) {
    }
    private protected override void フロー(List<(A計測? 移動元,A計測? 移動先)> 列Array,StringBuilder 制御罫線){
        var 親辺Array=this.List親辺.ToArray();
        var 親辺Array_Length=親辺Array.Length;
        for(var a=0;a<制御罫線.Length;a++){
            制御罫線[a]=制御罫線[a] switch{
                '┌' or'┐' or'┬'=>'│',
                _=>制御罫線[a]
            };
        }
        if(親辺Array_Length==0) return;
        //Line初期化(列Array,制御罫線);
        制御罫線[0]='┌';
        //Append(制御罫線,"┌");
        var 書き込みした右端列=-1;
        for(var a=0;a<親辺Array_Length;a++){
            var 親辺=親辺Array[a];
            var Count=列Array.Count;
            for(var b=1;b<Count;b++){
                var 列=列Array[b];
                //Debug.Assert(列.移動元==親辺);
                if(列.移動元==親辺) {
                    if(列.移動先==this){
                        if(制御罫線[b]!='┘')
                            if(書き込みした右端列<b)
                                書き込みした右端列=b;
                        制御罫線[b]='┘';
                        goto 終了;
                    }
                }
            }
            for(var b=1;b<Count;b++){
                if(制御罫線[b]is'　'){
                    列Array[b]=(親辺,this);
                    //Debug.Assert(書き込みした右端列<b); 
                    書き込みした右端列=b;
                    制御罫線[書き込みした右端列]='┐';
                    goto 終了;
                }
            }
            書き込みした右端列=制御罫線.Length;
            列Array.Add((親辺,this));
            Append(制御罫線,"┐");
            終了: ;
        }
        if(書き込みした右端列>0){
            for(var a=1;a<書き込みした右端列;a++){
                switch(制御罫線[a]){
                    case '　':
                        制御罫線[a]='─';
                        break;
                    case '┐':
                        制御罫線[a]='┬';
                        break;
                    case '┘':
                        制御罫線[a]='┴';
                        列Array[a]=(null,null);
                        break;
                    case '│':
                        制御罫線[a]='┼';
                        break;
                }
            }
            if(制御罫線[書き込みした右端列]=='┘') 列Array[書き込みした右端列]=(null,null);
            //for(var a=書き込みした右端列+1;a<制御罫線.Length;a++){
            //    switch(制御罫線[a]){
            //        case '┐':
            //            制御罫線[a]='│';
            //            break;
            //    }
            //}
        }
    }
    /// <summary>
    /// 全体木に対する木のms割合
    /// </summary>
    [NonSerialized]
    private double 全体木に対する木のms割合;
    /// <summary>
    /// 全体木に対する節のms割合
    /// </summary>
    [NonSerialized]
    private double 全体木に対する節のms割合;
    /// <summary>
    /// 木に対する節のms割合
    /// </summary>
    [NonSerialized]
    private double 木に対する節のms割合;
    [NonSerialized]
    private readonly Stopwatch Stopwatch = new();
    private long 呼出回数;
    internal override long ms => this.Stopwatch.ElapsedMilliseconds;

    /// <summary>
    /// 計測開始
    /// </summary>
    protected override A計測 Start() {
        this.Stopwatch.Start();
        this.呼出回数++;
        return this;
    }

    /// <summary>
    /// 計測終了
    /// </summary>
    protected override void Stop() => this.Stopwatch.Stop();
    protected override T StopReturn<T>(T item) {
        this.Stopwatch.Stop();
        return item;
    }
    internal override void 割合計算(long 全体木のms) {
        var 木ms = this.ms;
        var 節ms = 木ms;
        foreach(var 子 in this.List子演算) {
            子.割合計算(全体木のms);
            節ms-=子.ms;
        }
        this.全体木に対する木のms割合=全体木のms==0 ? 0 : (double)this.ms/全体木のms;
        this.全体木に対する節のms割合=全体木のms==0 ? 0.0 : (double)節ms/全体木のms;
        this.木に対する節のms割合=木ms==0 ? 0.0 : (double)節ms/木ms;
        var sb = new StringBuilder();
        sb.Append('│');
        void 共通ms(long Value) {
            sb.Append($"{Value,6}");
            sb.Append('│');
        }
        void 共通割合(double 割合) {
            sb.Append(割合.ToString("0.0000",CultureInfo.InvariantCulture));
            sb.Append('│');
        }
        共通ms(木ms);
        共通ms(this.排他ms);
        共通割合(this.全体木に対する木のms割合);
        共通割合(this.全体木に対する節のms割合);
        共通割合(this.木に対する節のms割合);
        var 呼出回数 = this.呼出回数;
        sb.Append(呼出回数>=10000000000 ? "MAX       " : $"{呼出回数,10}");
        sb.Append('│');
        this.数値表=sb.ToString();
    }
    internal override void 割合計算() {
        this.全体木に対する木のms割合=0.0;
        this.全体木に対する節のms割合=0.0;
        this.木に対する節のms割合=0.0;
        this.割合計算(this.ms);
    }
    private long 排他ms {
        get {
            var ElapsedMilliseconds = this.Stopwatch.ElapsedMilliseconds;
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach(var 子 in this.List子演算)
                ElapsedMilliseconds-=子.ms;
            return ElapsedMilliseconds;
        }
    }
}
public  class 計測する子:計測する{
    public 計測する子(int 番号,ParameterExpression Parameter,string Name,string? Value,object  Object) : base(番号,Parameter,Name,Value,Object) {
    }
    public 計測する子(int 番号,string Name,string? Value,object Object) : base(番号,Name,Value,Object) {
    }
    public 計測する子(int 番号,string Name,string? Value) : base(番号,Name,Value) {
    }
    public 計測する子(int 番号) : base(番号) {
    }
    private protected override void フロー(List<(A計測? 移動元,A計測? 移動先)> 列Array,StringBuilder 制御罫線){
        var 子辺Array=this.List子辺.ToArray();
        var 子辺Array_Length=子辺Array.Length;
        //if(子辺Array_Length<=0)return;
        //Line初期化(列Array,制御罫線);
        if(子辺Array_Length==0){
            //制御罫線[0]='　';
            for(var a = 1;a<制御罫線.Length;a++)
                if(制御罫線[a] is '└'or'┴' or '┘'or'─')
                    制御罫線[a]='　';
                else if(制御罫線[a] is '┼')
                    制御罫線[a]='│';
            //if(制御罫線[0] is'│'){
            //    制御罫線[0]='└';
            //    //for(var a = 1;a<制御罫線.Length;a++)
            //    //    if(制御罫線[a] is '　')
            //    //        制御罫線[a]='┐';
            //    //else if(制御罫線[a] is '┼')
            //    //    制御罫線[a]='│';
            //} else 
            if(制御罫線[0] is '└') {
                制御罫線[0]='　';
                for(var a = 1;a<制御罫線.Length;a++)
                    if(制御罫線[a] is '┼')
                        制御罫線[a]='│';
            }else
                制御罫線[0]='│';
        } else{
            制御罫線[0]='└';
            for(var a = 1;a<制御罫線.Length;a++)
                if(制御罫線[a] is '└'or'┴' or '┘'or'─')
                    制御罫線[a]='　';
        }
        var ループか=false;
        var 書き換えLineIndexEnd=-1;
        for(var a=0;a<子辺Array_Length;a++){
            var 子辺=子辺Array[a];
            var Count=列Array.Count;
            for(var b=1;b<Count;b++){
                var 列=列Array[b];

                if(列.移動元==this) {
                    if(列.移動先==子辺) {
                        if(制御罫線[b]=='│') {
                            Debug.Assert(書き換えLineIndexEnd<b);
                            if(書き換えLineIndexEnd<b) 書き換えLineIndexEnd=b;
                            制御罫線[b]='┘';
                            ループか=true;
                            goto 終了;
                        }
                    }
                }
            }
            for(var b=1;b<Count;b++){
                if(制御罫線[b]=='　'){
                    Debug.Assert(列Array[b].移動元 is null);
                    列Array[b]=(this,子辺);
                    Debug.Assert(書き換えLineIndexEnd<b);
                    if(書き換えLineIndexEnd<b)
                        書き換えLineIndexEnd=b;
                    制御罫線[b]='┐';
                    goto 終了;
                } 
            }
            書き換えLineIndexEnd=制御罫線.Length;
            列Array.Add((this,子辺));
            制御罫線.Append('┐');
            終了: ;
        }
        for(var a=1;a<書き換えLineIndexEnd;a++){
            switch(制御罫線[a]){
                case '　':制御罫線[a]='─'; break;
                //case '┘':
                //    Line[a]='┴';
                //    break;
                case '│':制御罫線[a]='┼'; break;
                case '┐':制御罫線[a]='┬'; break;
            }
        }
        if(ループか)
            列Array[書き換えLineIndexEnd]=(null,null);
    }
    /// <summary>
    /// 全体木に対する木のms割合
    /// </summary>
    [NonSerialized]
    private double 全体木に対する木のms割合;
    /// <summary>
    /// 全体木に対する節のms割合
    /// </summary>
    [NonSerialized]
    private double 全体木に対する節のms割合;
    /// <summary>
    /// 木に対する節のms割合
    /// </summary>
    [NonSerialized]
    private double 木に対する節のms割合;
    [NonSerialized]
    private readonly Stopwatch Stopwatch = new();
    private long 呼出回数;
    internal override long ms => this.Stopwatch.ElapsedMilliseconds;

    /// <summary>
    /// 計測開始
    /// </summary>
    protected override A計測 Start() {
        this.Stopwatch.Start();
        this.呼出回数++;
        return this;
    }

    /// <summary>
    /// 計測終了
    /// </summary>
    protected override void Stop() => this.Stopwatch.Stop();
    protected override T StopReturn<T>(T item) {
        this.Stopwatch.Stop();
        return item;
    }
    internal override void 割合計算(long 全体木のms) {
        var 木ms = this.ms;
        var 節ms = 木ms;
        foreach(var 子 in this.List子演算) {
            子.割合計算(全体木のms);
            節ms-=子.ms;
        }
        this.全体木に対する木のms割合=全体木のms==0 ? 0 : (double)this.ms/全体木のms;
        this.全体木に対する節のms割合=全体木のms==0 ? 0.0 : (double)節ms/全体木のms;
        this.木に対する節のms割合=木ms==0 ? 0.0 : (double)節ms/木ms;
        var sb = new StringBuilder();
        sb.Append('│');
        void 共通ms(long Value) {
            sb.Append($"{Value,6}");
            sb.Append('│');
        }
        void 共通割合(double 割合) {
            sb.Append(割合.ToString("0.0000",CultureInfo.InvariantCulture));
            sb.Append('│');
        }
        共通ms(木ms);
        共通ms(this.排他ms);
        共通割合(this.全体木に対する木のms割合);
        共通割合(this.全体木に対する節のms割合);
        共通割合(this.木に対する節のms割合);
        var 呼出回数 = this.呼出回数;
        sb.Append(呼出回数>=10000000000 ? "MAX       " : $"{呼出回数,10}");
        sb.Append('│');
        this.数値表=sb.ToString();
    }
    internal override void 割合計算() {
        this.全体木に対する木のms割合=0.0;
        this.全体木に対する節のms割合=0.0;
        this.木に対する節のms割合=0.0;
        this.割合計算(this.ms);
    }
    private long 排他ms {
        get {
            var ElapsedMilliseconds = this.Stopwatch.ElapsedMilliseconds;
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach(var 子 in this.List子演算)
                ElapsedMilliseconds-=子.ms;
            return ElapsedMilliseconds;
        }
    }
}
public sealed class List計測:List<A計測>{
    private readonly StringBuilder sb=new();
    //public string Analize{
    //    get{
    //        var sb=this.sb;
    //        sb.Clear();
    //        this[0].Consoleに出力(sb);
    //        return sb.ToString();
    //    }
    //}
    private void 制御フローを正規化(){
        var Count=this.Count;
        for(var a=0;a<Count;a++){
            var b=this[a];
        }
    }
    private readonly List<string>ListString=new();
    public string Analize{
        get{
            this.制御フローを正規化();
            var ListString=this.ListString;
            ListString.Clear();
            this[0].Consoleに出力(ListString);
            var sb=this.sb;
            foreach(var a in ListString) sb.AppendLine(a);
            return sb.ToString();
        }
    }
    public string データフローチャート(List計測 List制御計測){
        var sb=this.sb;
        sb.Clear();
        this[0].Consoleに出力(sb,List制御計測);
        return sb.ToString();
    }
    public string フロー0 {
        get {
            var 列Array0=new List<(A計測? 移動元,A計測? 移動先)>{(null,null)};
            var Line=new StringBuilder();
            Line.Append('　');
            var 前回のLine=Line.ToString();
            var Count = this.Count;
            var sb = new StringBuilder();
            for(var a = 0;a<Count;a++) {
                var 辺 = this[a];
                var 親辺Array = 辺.List親辺.ToArray();
                var 子辺Array = 辺.List子辺.ToArray();
                親(親辺Array,列Array0,ref 前回のLine,辺,sb,Line);
                子(子辺Array,列Array0,ref 前回のLine,辺,sb,Line);
            }
            return sb.ToString();
        }
    }
    public string フロー1 {
        get {
            var 列Array=new List<(A計測? 移動元,A計測? 移動先)>();
            var Line=new StringBuilder();
            var Count = this.Count;
            var sb = new StringBuilder();
            for(var a = 0;a<Count;a++) {
                var 辺 = this[a];
                var 親辺Array = 辺.List親辺.ToArray();
                var 子辺Array = 辺.List子辺.ToArray();
                親子(親辺Array,子辺Array,列Array,辺,sb,Line);
            }
            return sb.ToString();
        }
    }
    public IEnumerator<string> GetLineEnumerator(){
        var 列Array0=new List<(A計測? 移動元,A計測? 移動先)>{(null,null)};
        var Line=new StringBuilder();
        Line.Append('　');
        var 前回のLine=Line.ToString();
        var sb = new StringBuilder();
        var Count=this.Count-1;
        if(Count>=0){
            {
                var 辺=this[0];
                var 子辺Array=辺.List子辺.ToArray();
                子(子辺Array,列Array0,ref 前回のLine,辺,sb,Line);
                yield return Line.ToString();
            }
            for(var a=1;a<Count;a++){
                var 辺=this[a];
                var 親辺Array = 辺.List親辺.ToArray();
                var 子辺Array = 辺.List子辺.ToArray();
                親(親辺Array,列Array0,ref 前回のLine,辺,sb,Line);
                yield return Line.ToString();
                子(子辺Array,列Array0,ref 前回のLine,辺,sb,Line);
                yield return Line.ToString();
            }
            {
                var 辺=this[Count];
                var 親辺Array = 辺.List親辺.ToArray();
                親(親辺Array,列Array0,ref 前回のLine,辺,sb,Line);
                yield return Line.ToString();
            }
        }
    }
    private static void Line初期化(List<(A計測? 移動元,A計測? 移動先)>列Array0,StringBuilder Line){
        for(var b = 0;b<列Array0.Count;b++)
            if(列Array0[b].移動元 is null)
                Line[b]='　';
            else
                Line[b]='│';
    }
    /// <summary>
    /// 親とはラベルのことであり、このラベルを使うジャンプ命令の属する複数辺を保持する
    /// </summary>
    /// <param name="親辺Array"></param>
    /// <param name="列Array"></param>
    /// <param name="前回のLine"></param>
    /// <param name="辺"></param>
    /// <param name="sb"></param>
    /// <param name="Line"></param>
    private static void 親(A計測[] 親辺Array,List<(A計測? 移動元,A計測? 移動先)> 列Array,ref string 前回のLine,A計測 辺,StringBuilder sb,StringBuilder Line){
        var 親辺Array_Length=親辺Array.Length;
        if(親辺Array_Length<=0)return;
        Line初期化(列Array,Line);
        Line[0]='┌';
        var 書き込みした右端列=-1;
        for(var a=0;a<親辺Array_Length;a++){
            var 親辺=親辺Array[a];
            var Count=列Array.Count;
            for(var b=0;b<Count;b++){
                var 列=列Array[b];
                //Debug.Assert(列.移動元==親辺);
                if(列.移動元==親辺) {
                    if(列.移動先==辺){
                        if(Line[b]!='┘')
                            if(書き込みした右端列<b)
                                書き込みした右端列=b;
                        Line[b]='┘';
                        goto 終了;
                    }
                }
            }
            for(var b=0;b<Count;b++){
                if(Line[b]is'　'){
                    列Array[b]=(親辺,辺);
                    Debug.Assert(書き込みした右端列<b); 
                    書き込みした右端列=b;
                    Line[b]='┐';
                    goto 終了;
                }
            }
            書き込みした右端列=Line.Length;
            列Array.Add((親辺,辺));
            Line.Append('┐');
            終了: ;
        }
        if(書き込みした右端列>0){
            for(var a=1;a<書き込みした右端列;a++){
                switch(Line[a]){
                    case '　':
                        Line[a]='─';
                        break;
                    case '┐':
                        Line[a]='┬';
                        break;
                    case '┘':
                        Line[a]='┴';
                        列Array[a]=(null,null);
                        break;
                    case '│':
                        Line[a]='┼';
                        break;
                }
            }
            if(Line[書き込みした右端列]=='┘') 列Array[書き込みした右端列]=(null,null);
        }
        var 行=Line.ToString();
        前回のLine=行;
        sb.AppendLine($"{行},{辺.Name} {辺.Value}");
    }
    /// <summary>
    /// 親とはジャンプ命令のことであり、この複数のジャンプ命令(if,switch,goto)の飛び先のラベルの属する複数辺を保持する
    /// </summary>
    /// <param name="子辺Array"></param>
    /// <param name="列Array0"></param>
    /// <param name="前回のLine"></param>
    /// <param name="辺"></param>
    /// <param name="sb"></param>
    /// <param name="Line"></param>
    private static void 子(A計測[] 子辺Array,List<(A計測? 移動元,A計測? 移動先)> 列Array0,ref string 前回のLine,A計測 辺,StringBuilder sb,StringBuilder Line){
        var 子辺Array_Length=子辺Array.Length;
        if(子辺Array_Length<=0)return;
        Line初期化(列Array0,Line);
        Line[0]='└';
        var ループか=false;
        var 書き換えLineIndexEnd=-1;
        for(var a=0;a<子辺Array_Length;a++){
            var 子辺=子辺Array[a];
            var Count=列Array0.Count;
            for(var b=0;b<Count;b++){
                var 列=列Array0[b];

                if(列.移動元==辺) {
                    if(列.移動先==子辺) {
                        if(Line[b]=='│') {
                            Debug.Assert(書き換えLineIndexEnd<b);
                            if(書き換えLineIndexEnd<b) 書き換えLineIndexEnd=b;
                            Line[b]='┘';
                            ループか=true;
                            goto 終了;
                        }
                    }
                }
            }
            for(var b=0;b<Count;b++){
                if(Line[b]=='　'){
                    Debug.Assert(列Array0[b].移動元 is null);
                    列Array0[b]=(辺,子辺);
                    Debug.Assert(書き換えLineIndexEnd<b);
                    if(書き換えLineIndexEnd<b)
                        書き換えLineIndexEnd=b;
                    Line[b]='┐';
                    goto 終了;
                } 
            }
            書き換えLineIndexEnd=Line.Length;
            列Array0.Add((辺,子辺));
            Line.Append('┐');
            終了: ;
        }
        for(var a=0;a<書き換えLineIndexEnd;a++){
            switch(Line[a]){
                case '　':Line[a]='─'; break;
                //case '┘':
                //    Line[a]='┴';
                //    break;
                case '│':Line[a]='┼'; break;
                case '┐':Line[a]='┬'; break;
            }
        }
        if(ループか)
            列Array0[書き換えLineIndexEnd]=(null,null);
        var 行=Line.ToString();
        前回のLine=行;
        sb.AppendLine($"{行},{辺.Name} {辺.Value}");
    }
    private static void 親子(A計測[] 親辺Array,A計測[] 子辺Array,List<(A計測? 移動元,A計測? 移動先)> 列Array,A計測 辺,StringBuilder sb,StringBuilder Line){
        var 親辺Array_Length=親辺Array.Length;
        var 子辺Array_Length=子辺Array.Length;
        Line初期化(列Array,Line);
        for(var a=0;a<Line.Length;a++)
            Line[a]=Line[a]switch{
                '┤'or'┬'or'┐'or'│'=>'│',
                '┴'or'┘'=>'　',
                _=>Line[a]
            };
        var 書き込みした右端列=-1;
        for(var a=0;a<親辺Array_Length;a++){
            var 親辺=親辺Array[a];
            var Count=列Array.Count;
            for(var b=0;b<Count;b++){
                var 列=列Array[b];
                if(列.移動元==親辺) {
                    //Debug.Assert(列.移動先==辺);
                    if(列.移動先==辺){
                        if(Line[b]=='│')
                            if(書き込みした右端列<b)
                                書き込みした右端列=b;
                        Line[b]='┘';
                        列Array[b]=(null,null);
                        goto 終了;
                    }
                }
            }
            for(var b=0;b<Count;b++){
                if(列Array[b].移動元 is null){
                    列Array[b]=(親辺,辺);
                    //Debug.Assert(書き込みした右端列<b); 
                    書き込みした右端列=b;
                    Line[b]='┐';
                    goto 終了;
                }
            }
            書き込みした右端列=Line.Length;
            列Array.Add((親辺,辺));
            Line.Append('┐');
            終了: ;
        }
        if(書き込みした右端列>0){
            for(var a=0;a<書き込みした右端列;a++){
                switch(Line[a]){
                    case '　':
                        Line[a]='─';
                        break;
                    case '┐':
                        Line[a]='┬';
                        break;
                    case '┘':
                        Line[a]='┴';
                        break;
                    case '│':
                        Line[a]='┼';
                        break;
                }
            }
            if(Line[書き込みした右端列]=='┘') 列Array[書き込みした右端列]=(null,null);
            //if(Line[書き込みした右端列]=='┐') Line[書き込みした右端列]='│';
        }
        var ループか=false;
        var 書き換えLineIndexEnd=-1;
        for(var a=0;a<子辺Array_Length;a++){
            var 子辺=子辺Array[a];
            var Count=列Array.Count;
            for(var b=0;b<Count;b++){
                var 列=列Array[b];
                if(列.移動元==辺) {
                    if(列.移動先==子辺) {
                        if(Line[b]=='│') {
                            Debug.Assert(書き換えLineIndexEnd<b);
                            if(書き換えLineIndexEnd<b) 書き換えLineIndexEnd=b;
                            Line[b]='┘';
                            ループか=true;
                            goto 終了;
                        }
                    }
                }
            }
            for(var b=0;b<Count;b++){
                if(列Array[b].移動元 is null){
                    //Debug.Assert(列Array[b].移動元 is null);
                    列Array[b]=(辺,子辺);
                    Debug.Assert(書き換えLineIndexEnd<b);
                    if(書き換えLineIndexEnd<b)
                        書き換えLineIndexEnd=b;
                    if(Line[b]=='┘')
                        Line[b]='┤';
                    else
                        Line[b]='┐';
                    goto 終了;
                } 
            }
            書き換えLineIndexEnd=Line.Length;
            列Array.Add((辺,子辺));
            Line.Append('┐');
            終了: ;
        }
        for(var a=0;a<書き換えLineIndexEnd;a++){
            //switch(Line[a]){
            //    case '　':Line[a]='─'; break;
            //    case '┘':Line[a]='┴'; break;
            //    case '│':Line[a]='┼'; break;
            //    case '┐':Line[a]='┬'; break;
            //}
            Line[a]=Line[a] switch{
                '　'=>'─',
                '┘'=>'┤',
                '│'=>'┼',
                '┐'=>'┬',
                _=>Line[a]
            };
        }
        if(ループか)
            列Array[書き換えLineIndexEnd]=(null,null);
        var 行=Line.ToString();
        Trace.WriteLine($"{行}{辺.Name} {辺.Value}");
        sb.AppendLine($"{行}{辺.Name} {辺.Value}");
    }
}

