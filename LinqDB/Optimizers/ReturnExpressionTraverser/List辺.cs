using System.Diagnostics;
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
                var 親辺Array = 辺.List親辺.ToArray();
                var 子辺Array = 辺.List子辺.ToArray();
                親(親辺Array,列Array0,List制御フロー,Listテキスト,辺,Line);
                子(子辺Array,列Array0,List制御フロー,Listテキスト,辺,Line);
            }
            var sb = new StringBuilder();
            var Listテキスト_Count=Listテキスト.Count;
            var 最大半角文字数=-1;
            for(var a=0;a<Listテキスト_Count;a++){
                if(Listテキスト[a].Length<=0) continue;
                var s=List制御フロー[a];
                var 半角文字数=ShiftJIS半角換算文字数(s);
                if(最大半角文字数<半角文字数) 最大半角文字数=半角文字数;
            }
            if(最大半角文字数%2==0)
                最大半角文字数+=1;
            for(var a=0;a<Listテキスト_Count;a++){
                if(Listテキスト[a].Length<=0) continue;
                var s=List制御フロー[a];
                var 半角文字数=ShiftJIS半角換算文字数(s);
                var 埋めたい半角文字数=最大半角文字数-半角文字数;
                sb.AppendLine(s+new string(' ',埋めたい半角文字数)+Listテキスト[a]);
            }
            return sb.ToString();
        }
    }
    public void 一度出現したExpressionを上位に移動(){
        var 先頭=this[0];
        this.Reset();
        先頭.節子孫二度出現Expressions作成0();
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
    /// <param name="親辺Array"></param>
    /// <param name="列Array"></param>
    /// <param name="List制御フロー"></param>
    /// <param name="Listテキスト"></param>
    /// <param name="辺"></param>
    /// <param name="Line"></param>
    private static void 親(辺[] 親辺Array,Generic.List<(辺? 移動元,辺? 移動先)> 列Array,Generic.List<string> List制御フロー,Generic.List<string> Listテキスト,辺 辺,StringBuilder Line){
        var 親辺Array_Length=親辺Array.Length;
        Line初期化(列Array,Line);
        if(親辺Array_Length<=0){
            Line[0]='│';
        } else{
            Line[0]='┌';
            var 書き込みした右端列=-1;
            for(var a=0;a<親辺Array_Length;a++){
                var 親辺=親辺Array[a];
                var Count=列Array.Count;
                for(var b=0;b<Count;b++){
                    var 列=列Array[b];
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
                        case'　':
                            Line[a]='─';
                            break;
                        case'┐':
                            Line[a]='┬';
                            break;
                        case'┘':
                            Line[a]='┴';
                            列Array[a]=(null,null);
                            break;
                        case'│':
                            Line[a]='┼';
                            break;
                    }
                }
                if(Line[書き込みした右端列]=='┘') 列Array[書き込みした右端列]=(null,null);
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
    /// <param name="子辺Array"></param>
    /// <param name="列Array0"></param>
    /// <param name="List制御フロー"></param>
    /// <param name="Listテキスト"></param>
    /// <param name="辺"></param>
    /// <param name="Line"></param>
    private static void 子(辺[] 子辺Array,Generic.List<(辺? 移動元,辺? 移動先)> 列Array0,Generic.List<string> List制御フロー,Generic.List<string> Listテキスト,辺 辺,StringBuilder Line){
        var 子辺Array_Length=子辺Array.Length;
        Line初期化(列Array0,Line);
        if(子辺Array_Length<=0){
            Line[0]='│';
        } else{
            Line[0]='└';
            var ループか=false;
            var 書き換えLineIndexEnd=-1;
            for(var a=0;a<子辺Array_Length;a++){
                var 子辺=子辺Array[a];
                var Count=列Array0.Count;
                for(var b=0;b<Count;b++){
                    var 列=列Array0[b];

                    if(列.移動元==辺){
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
                        Debug.Assert(列Array0[b].移動元 is null);
                        列Array0[b]=(辺,子辺);
                        //Debug.Assert(書き換えLineIndexEnd<b);
                        if(書き換えLineIndexEnd<b) 書き換えLineIndexEnd=b;
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
            if(ループか) 列Array0[書き換えLineIndexEnd]=(null,null);
        }
        List制御フロー.Add($"{Line}");
        Listテキスト.Add($"{辺.辺番号},{辺.子コメント}");
    }
}
//20240116 232
