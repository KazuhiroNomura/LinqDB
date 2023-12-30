using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException
// ReSharper disable All
namespace LinqDB.Optimizers.ReturnExpressionTraverser.Profiling;
using static Common;
/// <summary>
/// 罫線だけを出力する
/// </summary>
[DebuggerDisplay("{Name} {Value}")]
public abstract class 計測{
    internal static class Reflection{
        public static readonly MethodInfo Start=typeof(計測).GetMethod(nameof(計測.Start),Instance_NonPublic_Public)!;
        public static readonly MethodInfo Stop=typeof(計測).GetMethod(nameof(計測.Stop),Instance_NonPublic_Public)!;
        public static readonly MethodInfo StopReturn=typeof(計測).GetMethod(nameof(計測.StopReturn),Instance_NonPublic_Public)!;
    }
    protected 計測(int 制御番号,string Name,string Value){
        this.制御番号=制御番号;
        this._Name=Name;
        this._Value=Value;
    }
    protected 計測(int 制御番号,string Name){
        this.制御番号=制御番号;
        this._Name=Name;
        this._Value="";
    }
    protected 計測(計測 制御計測,string Name){
        this.制御番号=制御計測.制御番号;
        this.制御計測=制御計測;
        this._Name=Name;
        this._Value="";
    }
    protected 計測(string Name){
        this.制御番号=-1;
        this._Name=Name;
        this._Value="";
    }
    protected 計測(int 制御番号){
        this.制御番号=制御番号;
        this._Name="";
        this._Value="";
    }
    //public string 親コメント { get; set; } = "";
    //public string 子コメント { get; set; } = "";
    internal 計測? 親演算;
    //internal string Name{get;set;}
    internal string _Name;
    internal string Name{
        get=>this._Name;
        set{
            this._Name=value;
        }
    }
    private string _Value;
    internal string Value{
        get=>this._Value;
        set{
            this._Value=value;
            Debug.Assert(value is not null);
        }
    }
    private protected long 呼出回数{get;set;}
    //internal string Value{get;set;}
    internal string? NameValue{
        get{
            var Name=this.Name;
            var Value=this.Value;
            var length=Name.Length+Value.Length;
            string Space;
            if(length%2==0)
                Space="  ";
            else
                Space=" ";
            return Name+Space+Value;
        }
    }
    internal readonly List<計測> List親辺=new();
    internal readonly List<計測> List子辺=new();
    public static void 接続(計測 親,計測 子){
        親.List子辺.Add(子);
        子.List親辺.Add(親);
    }
    [NonSerialized]internal ParameterExpression? Parameter;
    internal 計測? 制御計測=null;
    internal int 制御番号;
    /// <summary>
    /// 子要素のプロファイル│。
    /// </summary>
    public readonly List<計測> List子演算=new();
    public void Clear(){
        this.List子演算.Clear();
        this.List親辺.Clear();
        this.List子辺.Clear();
    }
    protected string? 数値表;
    protected private const string 空表=
        "│      │      │    │    │    │          │";
    protected const string フッター=
        "└───┴───┴──┴──┴──┴─────┘";
    public void Consoleに出力(List<string> List表とツリー,StringBuilder sb){
        List表とツリー.Add("┌───────┬────────┬─────┐");
        List表とツリー.Add("│ms            │割合            │ 呼出回数 │");
        List表とツリー.Add("├───┬───┼──┬──┬──┤          │");
        List表とツリー.Add("│部分  │節    │部分│ 節 │ 節 │          │");
        List表とツリー.Add("│      │      │──│──│──│          │");
        List表とツリー.Add("│      │      │全体│全体│部分│          │");
        List表とツリー.Add("├───┼───┼──┼──┼──┼─────┤");
        var 行offset=List表とツリー.Count;
        this.割合計算();
        var 制御罫線=new StringBuilder();
        var 列Array=new List<(計測? 移動元,計測? 移動先)>();
        //var 最大行数 = 0;
        var List制御=new List<string>();
        this.演算フロー(List表とツリー,"");
        this.制御フロー(列Array,List制御,制御罫線);
        Debug.Assert(List表とツリー.Count-行offset==List制御.Count);
        var Count=List制御.Count;
        var Span制御=CollectionsMarshal.AsSpan(List制御);
        var Span表とツリー=CollectionsMarshal.AsSpan(List表とツリー);
        var 最大半角文字数=-1;
        for(var a=0;a<Count;a++){
            if(Span制御[a].Length<=0) continue;
            var s=Span表とツリー[行offset+a];
            var 半角文字数=ShiftJIS半角換算文字数(s);
            if(最大半角文字数<半角文字数) 最大半角文字数=半角文字数;
        }
        if(最大半角文字数%2==1)
            最大半角文字数+=2;
        else
            最大半角文字数+=1;
        for(var a=0;a<Count;a++){
            if(Span制御[a].Length<=0) continue;
            ref var s=ref Span表とツリー[行offset+a];
            var 空き半角文字数=最大半角文字数-ShiftJIS半角換算文字数(s);
            sb.Clear();
            sb.Append(s);
            //if(空き半角文字数%2==1)
            //    sb.Append(' ');
            if(Span制御[a][0] is'┐' or'┼' or'┘' or'┬' or'┴' or'─'){
                if(空き半角文字数%2==1) sb.Append(' ');
                sb.Append(new string('─',空き半角文字数/2));
            } else
                sb.Append(new string(' ',空き半角文字数));
            sb.Append(Span制御[a]);
            s=sb.ToString();
        }
        List表とツリー.Add(フッター);
    }
    private bool 親フロー(List<(計測? 移動元,計測? 移動先)> 列Array,StringBuilder 制御罫線){
        var 親辺Array=this.List親辺.ToArray();
        var 親辺Array_Length=親辺Array.Length;
        for(var a=0;a<制御罫線.Length;a++){
            制御罫線[a]=制御罫線[a] switch{
                '┼' or'┐' or'┬'=>'│',
                '─' or'┘' or'┴'=>'　',
                _=>制御罫線[a]
            };
        }
        ////Debug.Assert(!(親辺Array_Length>0&&this.List子辺.Count>0));
        if(親辺Array_Length==0) return false;
        //} else {
        //    //Line初期化(列Array,制御罫線);
        //    //制御罫線[0]='┌';
        //    //Append(制御罫線,"┌");
        //}
        var 書き込み列=-1;
        var 列Array_Count=列Array.Count;
        for(var a=0;a<親辺Array_Length;a++){
            var 親辺=親辺Array[a];
            for(var b=0;b<列Array_Count;b++){
                var 列=列Array[b];
                if(列.移動元==親辺){
                    if(列.移動先==this){
                        //if(制御罫線[b]!='┘')
                        //    if(書き込みした右端列<b)
                        //        書き込みした右端列=b;
                        制御罫線[b]=右下(b);
                        if(書き込み列<b) 書き込み列=b;
                        //if(a<親辺Array_Length-1)
                        //    制御罫線[b]='┴';
                        //else
                        //    制御罫線[b]='┘';
                        列Array[b]=(null,null);
                        goto 終了;
                    }
                }
                //if(制御罫線[b]is'│'or'┐')
                //    制御罫線[b]='┼';
                //else if(制御罫線[b]is'┴'or'┘'or'　')
                //    制御罫線[b]='─';
            }
            for(var b=0;b<列Array_Count;b++){
                if(制御罫線[b] is'　'){
                    列Array[b]=(親辺,this);
                    //Debug.Assert(書き込みした右端列<b);
                    制御罫線[b]=右上(b);
                    if(書き込み列<b) 書き込み列=b;
                    //if(b<列Array_Count-1)
                    //    制御罫線[b]='┬';
                    //else
                    //    制御罫線[b]='┐';
                    goto 終了;
                }
            }
            列Array.Add((親辺,this));
            書き込み列=制御罫線.Length;
            Append(制御罫線,"┐");
            終了: ;
        }
        for(var c=0;c<書き込み列;c++)
            if(制御罫線[c] is'│')
                制御罫線[c]='┼';
            else if(制御罫線[c] is'┘')
                制御罫線[c]='┴';
            else if(制御罫線[c] is'┐')
                制御罫線[c]='┬';
            else if(制御罫線[c] is'　') 制御罫線[c]='─';
        return true;
        char 右上(int index)=>計測.右上(index,親辺Array_Length);
        char 右下(int index)=>計測.右下(index,親辺Array_Length);
    }
    private void 子フロー(List<(計測? 移動元,計測? 移動先)> 列Array,StringBuilder 制御罫線,bool 親に重ねるか){
        var 子辺Array=this.List子辺.ToArray();
        var 子辺Array_Length=子辺Array.Length;
        //if(子辺Array_Length<=0) return;
        //Line初期化(列Array,制御罫線);
        if(子辺Array_Length==0){
            //制御罫線[0]='　';
            if(!親に重ねるか){
                for(var a=0;a<制御罫線.Length;a++)
                    if(制御罫線[a] is'└' or'┴' or'┘' or'─')
                        制御罫線[a]='　';
                    else if(制御罫線[a] is'┼' or'┐') 制御罫線[a]='│';
            }
        }
        var 列Array_Count=列Array.Count;
        for(var a=0;a<子辺Array_Length;a++){
            var 子辺=子辺Array[a];
            for(var b=0;b<列Array_Count;b++){
                var 列=列Array[b];

                if(列.移動元==this){
                    if(列.移動先==子辺){
                        制御罫線[b]=右下(b);
                        列Array[b]=(null,null);
                        //for(var c=0;c<b;c++)
                        //    if(制御罫線[c]=='　')
                        //        制御罫線[c]='─';
                        //    else{
                        //        Debug.Fail(c.ToString());
                        //    }
                        if(親に重ねるか){
                            for(var c=0;c<b;c++)
                                if(制御罫線[c] is'┘')
                                    制御罫線[c]='┴';
                            for(b++;b<列Array_Count;b++)
                                if(制御罫線[b] is'│')
                                    制御罫線[b]='┼';
                        } else{
                            for(var c=0;c<b;c++)
                                if(制御罫線[c] is'┴' or'┘')
                                    制御罫線[c]='─';
                            for(b++;b<列Array_Count;b++)
                                if(制御罫線[b] is'┴' or'─' or'┘')
                                    制御罫線[b]='　';
                        }
                        //        else　if(制御罫線[c] is'┘') 制御罫線[c]='　';
                        goto 終了;
                    }
                }
            }
            for(var b=0;b<列Array_Count;b++){
                if(制御罫線[b] is'　'){
                    //Debug.Assert(列Array[b].移動元 is null);
                    列Array[b]=(this,子辺);
                    制御罫線[b]=右上(a);
                    //if(a<子辺Array_Length-1)
                    //    制御罫線[b]='┬';
                    //else
                    //    制御罫線[b]='┐';
                    goto 終了;
                }
                if(制御罫線[b] is'│')
                    制御罫線[b]='┼';
                else if(親に重ねるか){
                    if(制御罫線[b] is'┘')
                        制御罫線[b]='┴';
                    else if(制御罫線[b] is'│') 制御罫線[b]='┼';
                } else{
                    if(制御罫線[b]=='┘') 制御罫線[b]='─';
                }
            }
            列Array.Add((this,子辺));
            制御罫線.Append(右上(a));
//if(a<子辺Array_Length-1)
//    制御罫線.Append('┬');
//else
//    制御罫線.Append('┐');
            終了: ;
        }
        char 右上(int index)=>計測.右上(index,子辺Array_Length);
        char 右下(int index)=>計測.右下(index,子辺Array_Length);
    }
    private static char 右上(int index,int Length){
        if(index<Length-1)
            return'┬';
        else
            return'┐';
    }
    private static char 右下(int index,int Length){
        if(index<Length-1)
            return'┴';
        else
            return'┘';
    }

    //private string 前回のフロー = "";
    private static readonly Encoding Shift_JIS;
    static 計測(){
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        Shift_JIS=Encoding.GetEncoding("shift_jis");
    }
    private static int ShiftJIS半角換算文字数(string str){
        return Shift_JIS.GetByteCount(str);
    }

    private static void Append(StringBuilder sb,string s){
        sb.Append(s);
        //Trace.Write(s);
    }
    private static void Append(List<string> sb,string s){
        sb.Add(s);
        //Trace.WriteLine(s);
    }
    private void 演算フロー(List<string> List表とツリー,string ツリー罫線){
        //Append(全行,this.数値表);
        //全行.Append(this.数値表);
        var 行=$"{this.数値表}{this.制御番号,3}{ツリー罫線}";
        行+=this.NameValue;
        Append(List表とツリー,行);
        var List計測=this.List子演算;
        var List計測_Count_1=List計測.Count-1;
        if(List計測_Count_1>=0){
            if(ツリー罫線.Length>0){
                var ツリー罫線前半=ツリー罫線[..^1];
                var 罫線=ツリー罫線[^1];
                // ReSharper disable once ConvertIfStatementToSwitchStatement
                if(罫線=='├')
                    ツリー罫線=ツリー罫線前半+'│';
                else if(罫線=='└') ツリー罫線=ツリー罫線前半+'　';
            }
            var ツリー罫線0=ツリー罫線+'├';
            for(var a=0;a<List計測_Count_1;a++) List計測[a].演算フロー(List表とツリー,ツリー罫線0);
            List計測[List計測_Count_1].演算フロー(List表とツリー,ツリー罫線+'└');
        }
    }
    private void 制御フロー(List<(計測? 移動元,計測? 移動先)> 列Array,List<string> List制御,StringBuilder 制御罫線){
        if(List制御.Count==25){

        }
        if(this.親フロー(列Array,制御罫線))
            this.子フロー(列Array,制御罫線,true);
        else
            this.子フロー(列Array,制御罫線,false);
        //this.フロー(列Array,制御罫線);
        Append(List制御,制御罫線.ToString());
        var List計測=this.List子演算;
        var List計測_Count_1=List計測.Count-1;
        if(List計測_Count_1>=0){
            for(var a=0;a<List計測_Count_1;a++) List計測[a].制御フロー(列Array,List制御,制御罫線);
            List計測[List計測_Count_1].制御フロー(列Array,List制御,制御罫線);
        }
    }
    internal abstract long 部分100ns{get;}
    internal virtual long 割合計算(long 全体100ns){
        var 子の部分100ns合計=this.部分100ns;
        var 部分100ns=this.部分100ns;
        foreach(var 子 in this.List子演算)
            子.割合計算(全体100ns);
        var 節100ns=部分100ns;
        var sb=new StringBuilder();
        sb.Append('│');
        共通100ns(部分100ns);
        共通100ns(節100ns);
        共通割合(部分100ns,全体100ns);
        共通割合(節100ns,全体100ns);
        共通割合(節100ns,部分100ns);
        var 呼出回数=this.呼出回数;
        if(呼出回数>=10000000000)
            sb.Append("MAX       ");
        else
            sb.Append($"{呼出回数,10}");
        sb.Append('│');
        this.数値表=sb.ToString();
        return 子の部分100ns合計;
        void 共通100ns(long Value){
            sb.Append($"{Value/10000,6}");
            sb.Append('│');
        }
        void 共通割合(double 分子100ns,double 分母100ns){
            if(分母100ns==0){
                sb.Append("    ");
            } else{
                sb.Append(((double)分子100ns/分母100ns).ToString("0.00",CultureInfo.InvariantCulture));
            }
            sb.Append('│');
        }
    }
    //internal abstract long 割合計算(long 全体100ns);
    internal void 割合計算()=>this.割合計算(this.部分100ns);
    /// <summary>
    /// 計測開始
    /// </summary>
    private protected virtual 計測 Start()=>this;
    /// <summary>
    /// 計測終了
    /// </summary>
    private protected virtual void Stop(){
    }
    /// <summary>
    /// 戻り値のある計測終了
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item"></param>
    /// <returns></returns>
    private protected virtual T StopReturn<T>(T item)=>item;
}
