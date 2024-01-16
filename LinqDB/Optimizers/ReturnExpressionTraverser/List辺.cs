using System.Diagnostics;
using Generic=System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using LinqDB.Optimizers.Comparer;
namespace LinqDB.Optimizers.ReturnExpressionTraverser;
public sealed class List辺:Generic.List<辺>{
    public string Analize {
        get {
            var Count = this.Count;
            var 列Array0=new Generic.List<(辺? 移動元,辺? 移動先)>{(null,null)};
            var Line=new StringBuilder();
            Line.Append('　');
            var 前回のLine=Line.ToString();
            var 辺に関する情報Array = this.ToArray();
            var sb = new StringBuilder();
            for(var a = 0;a<Count;a++) {
                var 辺 = 辺に関する情報Array[a];
                辺.辺番号=a;
                var 親辺Array = 辺.List親辺.ToArray();
                var 子辺Array = 辺.List子辺.ToArray();
                親(親辺Array,列Array0,ref 前回のLine,辺,sb,Line);
                子(子辺Array,列Array0,ref 前回のLine,辺,sb,Line);
            }
            return sb.ToString();
        }
    }
    private static readonly Generic.HashSet<Expression>EmptyHashSet=new(new ExpressionEqualityComparer());
    public void 一度出現したExpressionを上位に移動(){
        var 先頭=this[0];
        this.Reset();
        var ana=this.Analize;
        先頭.節二度出現Expressions追加();
        var 末尾=this[^1];
        //末尾.節祖先一度出現Expressions追加();
        //foreach(var a in this)
        //    a.節一度出現Expressions追加();
        //Reset();
        //末尾.節二度出現Expressions追加();
        //根.節二度出現Expressions追加();
        this.Reset();
        末尾.節二度出現Expressions除去();
        //foreach(var a in this)
        //    a.節二度出現Expressions除去();
        //根.作成1();
        this.Reset();
        //Reset();
        //foreach(var a in this)
        //    a.節一度出現Expressions追加();
        //Reset();
        //根.節二度出現Expressions追加();
        //Reset();
        //foreach(var a in this)
        //    a.節二度出現Expressions除去();
        ////根.作成1();
        //Reset();
        //根.作成2();
        //Reset();
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
    /// <param name="前回のLine"></param>
    /// <param name="辺"></param>
    /// <param name="sb"></param>
    /// <param name="Line"></param>
    private static void 親(辺[] 親辺Array,Generic.List<(辺? 移動元,辺? 移動先)> 列Array,ref string 前回のLine,辺 辺,StringBuilder sb,StringBuilder Line){
        var 親辺Array_Length=親辺Array.Length;
        Line初期化(列Array,Line);
        if(親辺Array_Length<=0){
            Line[0]='│';
        } else{
            Line[0]='┌';
            var 上Count=0;
            var 下Count=0;
            var 書き込みした右端列=-1;
            for(var a=0;a<親辺Array_Length;a++){
                var 親辺=親辺Array[a];
                var Count=列Array.Count;
                for(var b=0;b<Count;b++){
                    var 列=列Array[b];
                    //Debug.Assert(列.移動元==親辺);
                    if(列.移動元==親辺){
                        //Debug.Assert(列.移動先==辺);
                        if(Line[b]!='┘')
                            if(書き込みした右端列<b)
                                書き込みした右端列=b;
                        Line[b]='┘';
                        上Count++;
                        goto 終了;
                        //if(列.移動先==辺){
                        //    //Debug.Assert(Line[b]!='┘');
                        //    if(Line[b]!='┘')
                        //        if(書き込みした右端列<b)
                        //            書き込みした右端列=b;
                        //    Line[b]='┘';
                        //    上Count++;
                        //    goto 終了;
                        //}
                    }
                }
                for(var b=0;b<Count;b++){
                    if(Line[b] is'　'){
                        列Array[b]=(親辺,辺);
                        Debug.Assert(書き込みした右端列<b);
                        書き込みした右端列=b;
                        Line[b]='┐';
                        下Count++;
                        goto 終了;
                    }
                }
                書き込みした右端列=Line.Length;
                列Array.Add((親辺,辺));
                Line.Append('┐');
                下Count++;
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
        var 行=Line.ToString();
        前回のLine=行;
        sb.AppendLine($"{行}{辺.辺番号},{辺.親コメント}");
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
    private static void 子(辺[] 子辺Array,Generic.List<(辺? 移動元,辺? 移動先)> 列Array0,ref string 前回のLine,辺 辺,StringBuilder sb,StringBuilder Line){
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
                    //case '┘':
                    //    Line[a]='┴';
                    //    break;
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
        var 行=Line.ToString();
        前回のLine=行;
        sb.AppendLine($"{行}{辺.辺番号},{辺.子コメント}");
    }
}
