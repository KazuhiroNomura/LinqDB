﻿using System.Diagnostics;
using Generic=System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using LinqDB.Enumerables;
using LinqDB.Helpers;
using LinqDB.Optimizers.Comparer;
namespace LinqDB.Optimizers.ReturnExpressionTraverser;
public sealed class List辺:Generic.List<辺>{
    private static readonly Encoding Shift_JIS;
    static List辺(){
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        Shift_JIS=Encoding.GetEncoding("shift_jis");
    }
    private static int ShiftJIS半角換算文字数(string str)=>Shift_JIS.GetByteCount(str);
    public string Analize {
        get{
            var List制御フロー=new Generic.List<string>();
            var Listテキスト=new Generic.List<string>();
            var Count = this.Count;
            var 列Array0=new Generic.List<(辺? 移動元,辺? 移動先)>{(null,null)};
            var Line=new StringBuilder();
            Line.Append('　');
            var 辺に関する情報Array = this.ToArray();
            for(var a = 0;a<Count;a++) {
                var 辺 = 辺に関する情報Array[a];
                辺.辺番号=a;
                親(辺.List親辺,列Array0,List制御フロー,Listテキスト,辺,Line);
                子(辺.List子辺,列Array0,List制御フロー,Listテキスト,辺,Line);
            }
            var sb = new StringBuilder();
            var Listテキスト_Count=Listテキスト.Count;
            var 最大半角文字数=-1;
            for(var a=0;a<Listテキスト_Count;a++){
                var s=List制御フロー[a];
                var 半角文字数=ShiftJIS半角換算文字数(s);
                if(最大半角文字数<半角文字数) 最大半角文字数=半角文字数;
            }
            if(最大半角文字数%2==0)
                最大半角文字数+=1;
            for(var a=0;a<Listテキスト_Count;a++){
                var s=List制御フロー[a];
                var 半角文字数=ShiftJIS半角換算文字数(s);
                var 埋めたい半角文字数=最大半角文字数-半角文字数;
                sb.AppendLine(s+new string(' ',埋めたい半角文字数)+Listテキスト[a]);
            }
            return sb.ToString();
        }
    }
    private static int 最大深さ=-1;
    public void 一度出現したExpressionを上位に移動(){
        var 先頭=this[0];
        this.Reset();
        辺.最大深さ=-1;
        辺.深さ=0;
        先頭.節子孫二度出現Expressions作成0();
        if(最大深さ<辺.最大深さ){
            最大深さ=辺.最大深さ;
            Trace.WriteLine($"最大深さ{最大深さ}");
        }

        this.Reset();
        先頭.祖先二度出現Expressions作成1();
        this.Reset();
        先頭.親節祖先二度出現Expressions除去2();
    }
    internal void Reset(){
        foreach(var a in this) a.探索済みか=false;
    }
    public (Expression?Expression,辺?辺に関する情報) 二度出現したExpressionと辺(){
        foreach(var a in this) a.探索済みか=false;
        return this[0].二度出現したExpressionと辺();
    }
    private static void Line初期化(Generic.List<(辺? 移動元,辺? 移動先)>列Array0,StringBuilder Line){
        for(var b = 0;b<列Array0.Count;b++)
            if(列Array0[b].移動元 is null)
                Line[b]='　';
            else
                Line[b]='│';
    }
    /// <summary>
    /// 親とはラベルのことであり、このラベルを使うジャンプ命令の属する複数辺を保持する
    /// </summary>
    /// <param name="List親辺"></param>
    /// <param name="List列"></param>
    /// <param name="List制御フロー"></param>
    /// <param name="Listテキスト"></param>
    /// <param name="辺"></param>
    /// <param name="Line"></param>
    private static void 親(Generic.List<辺> List親辺,Generic.List<(辺? 移動元,辺? 移動先)> List列,Generic.List<string> List制御フロー,Generic.List<string> Listテキスト,辺 辺,StringBuilder Line){
        var 親辺Array_Count=List親辺.Count;
        Line初期化(List列,Line);
        if(親辺Array_Count<=0){
            Line[0]='│';
        } else{
            Line[0]='┌';
            var 書き込みした右端列=-1;
            for(var a=0;a<親辺Array_Count;a++){
                var 親辺=List親辺[a];
                var Count=List列.Count;
                for(var b=0;b<Count;b++){
                    var 列=List列[b];
                    if(列.移動元==親辺){
                        if(Line[b]!='┘')
                            if(書き込みした右端列<b)
                                書き込みした右端列=b;
                        Line[b]='┘';
                        goto 終了;
                    }
                }
                for(var b=0;b<Count;b++){
                    if(Line[b] is'　'){
                        List列[b]=(親辺,辺);
                        Debug.Assert(書き込みした右端列<b);
                        書き込みした右端列=b;
                        Line[b]='┐';
                        goto 終了;
                    }
                }
                書き込みした右端列=Line.Length;
                List列.Add((親辺,辺));
                Line.Append('┐');
                終了: ;
            }
            if(書き込みした右端列>0){
                for(var a=1;a<書き込みした右端列;a++){
                    switch(Line[a]){
                        case'　':
                            Line[a]='─';
                            break;
                        case'┐':
                            Line[a]='┬';
                            break;
                        case'┘':
                            Line[a]='┴';
                            List列[a]=(null,null);
                            break;
                        case'│':
                            Line[a]='┼';
                            break;
                    }
                }
                if(Line[書き込みした右端列]=='┘') List列[書き込みした右端列]=(null,null);
            }
        }
        List制御フロー.Add($"{Line}");
        var sb=new StringBuilder();
        foreach(var 節一度 in 辺.節一度出現Expressions) sb.Append(節一度.ToString()+',');
        if(sb.Length>0) sb.Length--;
        Listテキスト.Add($"{辺.辺番号},{辺.親コメント}{{{sb}}}");
    }
    /// <summary>
    /// 親とはジャンプ命令のことであり、この複数のジャンプ命令(if,switch,goto)の飛び先のラベルの属する複数辺を保持する
    /// </summary>
    /// <param name="List子辺"></param>
    /// <param name="List列"></param>
    /// <param name="List制御フロー"></param>
    /// <param name="Listテキスト"></param>
    /// <param name="辺"></param>
    /// <param name="Line"></param>
    private static void 子(Generic.List<辺> List子辺,Generic.List<(辺? 移動元,辺? 移動先)> List列,Generic.List<string> List制御フロー,Generic.List<string> Listテキスト,辺 辺,StringBuilder Line){
        var 子辺Array_Count=List子辺.Count;
        Line初期化(List列,Line);
        if(子辺Array_Count<=0){
            Line[0]='│';
        } else{
            Line[0]='└';
            var ループか=false;
            var 書き換えLineIndexEnd=-1;
            for(var a=0;a<子辺Array_Count;a++){
                var 子辺=List子辺[a];
                var Count=List列.Count;
                for(var b=0;b<Count;b++){
                    var 列=List列[b];

                    if(列.移動元==辺){
                        //Debug.Assert(列.移動先==子辺);
                        if(列.移動先==子辺){
                            if(Line[b]=='│'){
                                //Debug.Assert(書き換えLineIndexEnd<b);
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
                        Debug.Assert(List列[b].移動元 is null);
                        List列[b]=(辺,子辺);
                        //Debug.Assert(書き換えLineIndexEnd<b);
                        if(書き換えLineIndexEnd<b) 書き換えLineIndexEnd=b;
                        Line[b]='┐';
                        goto 終了;
                    }
                }
                書き換えLineIndexEnd=Line.Length;
                List列.Add((辺,子辺));
                Line.Append('┐');
                終了: ;
            }
            for(var a=0;a<書き換えLineIndexEnd;a++){
                switch(Line[a]){
                    case'　':
                        Line[a]='─';
                        break;
                    case'│':
                        Line[a]='┼';
                        break;
                    case'┐':
                        Line[a]='┬';
                        break;
                }
            }
            if(ループか) List列[書き換えLineIndexEnd]=(null,null);
        }
        List制御フロー.Add($"{Line}");
        Listテキスト.Add($"{辺.辺番号},{辺.子コメント}");
    }
}
//20240116 232
