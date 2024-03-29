﻿using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using LinqDB.Optimizers.Comparer;
using LinqDB.Optimizers.ReturnExpressionTraverser;
using LinqDB.Optimizers.ReturnExpressionTraverser.Profiling;
namespace TestLinqDB.特殊パターン;
public class フロー: 共通
{
    private 計測Maneger 計測Maneger=>new();
    //protected override テストオプション テストオプション=>テストオプション.None;
    [Fact]public void 辺に関する情報Conditional0(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //├┐　　　　　　　　　　　　　　　　　　0,IfTest:
        //└┼┐　　　　　　　　　　　　　　　　　1,IfTrue:
        //┌┘│　　　　　　　　　　　　　　　　　2,IfFalse:
        //├─┘　　　　　　　　　　　　　　　　　3,IfEnd:
        var Comparer=new ExpressionEqualityComparer();
        var 番号=0;
        var test=new 辺(Comparer);
        var ifTrue=new 辺(Comparer);
        var ifFalse=new 辺(Comparer);
        var endif=new 辺(Comparer);
        var l=new List辺();
        辺.接続(test,ifTrue);
        辺.接続(test,ifFalse);
        辺.接続(ifTrue,endif);
        辺.接続(ifFalse,endif);
        l.Add(test);
        l.Add(ifTrue);
        l.Add(ifFalse);
        l.Add(endif);
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 辺に関する情報Goto00(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┐
        //┌┘
        var Comparer=new ExpressionEqualityComparer();
        var 番号=0;
        var L0=new 辺(Comparer) { 親コメント="L0" };
        var L1=new 辺(Comparer) { 親コメント="L1" };
        辺.接続(L0,L1);
        var l=new List辺{L0,L1};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 辺に関する情報Goto01(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┐
        //┌┘
        //└┐
        //┌┘
        var Comparer=new ExpressionEqualityComparer();
        var 番号=0;
        var L0=new 辺(Comparer) { 親コメント="L0" };
        var L1=new 辺(Comparer) { 親コメント="L1" };
        var L2=new 辺(Comparer) { 親コメント="L2" };
        辺.接続(L0,L1);
        辺.接続(L1,L2);
        var l=new List辺{L0,L1,L2};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 辺に関する情報Goto1(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┐
        //┌┼┐
        //└┼┼┐
        //┌┘││
        //└─┘│
        //┌──┘
        {
            var Comparer=new ExpressionEqualityComparer();
            var L0=new 辺(Comparer){親コメント="L0"};
            var L1=new 辺(Comparer){親コメント="L1"};
            var L2=new 辺(Comparer){親コメント="L2"};
            var L3=new 辺(Comparer){親コメント="L3"};
            辺.接続(L0,L2);
            辺.接続(L2,L1);
            辺.接続(L1,L3);
            var l=new List辺{L0,L1,L2,L3};
            Trace.WriteLine(l.Analize);
        }
        {
            var 計測Maneger=this.計測Maneger;
            var L0=new 計測(計測Maneger,0,"Label","L0","");
            var L1=new 計測(計測Maneger,1,"Label","L1","");
            var L2=new 計測(計測Maneger,2,"Label","L2","");
            var L3=new 計測(計測Maneger,3,"Label","L3","");
            計測.接続(L0,L2);
            計測.接続(L2,L1);
            計測.接続(L1,L3);
            Trace.WriteLine(計測Maneger.Analize(L0));
        }
    }
    [Fact]public void 辺に関する情報Goto2(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┐
        //└┼┐
        //┌┼┘
        //┌┘
        var Comparer=new ExpressionEqualityComparer();
        var L0=new 辺(Comparer) { 親コメント="L0" };
        var L1=new 辺(Comparer) { 親コメント="L1" };
        var L2=new 辺(Comparer) { 親コメント="L2" };
        var L3=new 辺(Comparer) { 親コメント="L3" };
        辺.接続(L0,L3);
        辺.接続(L1,L2);
        var l=new List辺{L0,L1,L2,L3};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 辺に関する情報ループ00(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //├←┐
        //└─┘
        var Comparer=new ExpressionEqualityComparer();
        var L0=new 辺(Comparer) { 親コメント="L0" };
        辺.接続(L0,L0);
        var l=new List辺{L0};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 辺に関する情報ループ01(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┐0,
        //┌┴┐1,L1
        //└─┘1,
        var Comparer=new ExpressionEqualityComparer();
        var L0=new 辺(Comparer) { 親コメント="L0" };
        var L1=new 辺(Comparer) { 親コメント="L1" };
        辺.接続(L0,L1);
        辺.接続(L1,L1);
        var l=new List辺{L0,L1};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 辺に関する情報ループ1(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //├┐　　　　　　　　　0,Label,Root,親 (辺番号1 L0, 辺番号0 Root)
        //└┼┐　　　　　　　　0,Label,Root,子 (辺番号0 Root, 辺番号1 L0)
        //┌┼┘　　　　　　　　1,Label,L0,親 (辺番号0 Root, 辺番号1 L0)
        //└┘　　　　　　　　　1,Label,L0,子 
        var Comparer=new ExpressionEqualityComparer();
        var L0=new 辺(Comparer) { 親コメント="L0" };
        var L1=new 辺(Comparer) { 親コメント="L1" };
        辺.接続(L0,L1);
        辺.接続(L1,L0);
        var l=new List辺{L0,L1};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 辺に関する情報ループ2(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //├┐　　　　　　　　　0,Label,Root,親 (辺番号2 L0, 辺番号0 Root)
        //└┼┐　　　　　　　　0,Label,Root,子 (辺番号0 Root, 辺番号1 L0)
        //┌┼┘　　　　　　　　1,Label,L0,親 (辺番号0 Root, 辺番号1 L0)
        //└┼┐　　　　　　　　1,Label,L0,子 (辺番号1 L0, 辺番号2 L0)
        //┌┼┘　　　　　　　　2,Label,L0,親 (辺番号1 L0, 辺番号2 L0)
        //└┘　　　　　　　　　2,Label,L0,子 
        var Comparer=new ExpressionEqualityComparer();
        var L0=new 辺(Comparer) { 親コメント="L0" };
        var L1=new 辺(Comparer) { 親コメント="L1" };
        var L2=new 辺(Comparer) { 親コメント="L2" };
        辺.接続(L0,L1);
        辺.接続(L1,L2);
        辺.接続(L2,L0);
        var l=new List辺{L0,L1,L2};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 辺に関する情報ループ3(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┐
        //┌┘
        //├←┐
        //└─┘
        var Comparer=new ExpressionEqualityComparer();
        var L0=new 辺(Comparer) { 親コメント="L0" };
        var L1=new 辺(Comparer) { 親コメント="L1" };
        //辺に関する情報.接続(L0,L1);
        辺.接続(L0,L1);
        辺.接続(L1,L1);
        var l=new List辺{L0,L1};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void IfThenElseEnd0(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //│     0,L0{}
        //└┬┐ 0,
        //┌┘│ 1,L1{}
        //└┐│ 1,
        //┌┼┘ 2,L2{}
        //└┼┐ 2,
        //┌┴┘ 3,L3{}
        //│　　 3,
        var Comparer=new ExpressionEqualityComparer();
        var L0=new 辺(Comparer) { 親コメント="L0" };
        var L1=new 辺(Comparer) { 親コメント="L1" };
        var L2=new 辺(Comparer) { 親コメント="L2" };
        var L3=new 辺(Comparer) { 親コメント="L3" };
        辺.接続(L0,L1);
        辺.接続(L0,L2);
        辺.接続(L1,L3);
        辺.接続(L2,L3);
        var l=new List辺{
            L0,
            L1,
            L2,
            L3
        };
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 辺に関する情報ループBreak(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┐0,
        //┌┴┐1,L1
        //└┬┘1,
        //┌┘　2,L2
        var Comparer=new ExpressionEqualityComparer();
        var L0=new 辺(Comparer) { 親コメント="L0" };
        var L1=new 辺(Comparer) { 親コメント="L1" };
        var L2=new 辺(Comparer) { 親コメント="L2" };
        辺.接続(L0,L1);
        辺.接続(L1,L2);
        辺.接続(L1,L1);
        var l=new List辺{L0,L1,L2};
        Trace.WriteLine(l.Analize);
    }
    /// <summary>
    /// if{
    /// }else{
    /// }
    /// if{
    /// }else{
    /// }
    /// </summary>
    [Fact]public void IfThenElseEnd1(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┬┐L0
        //┌┘│L1
        //└┐│L1
        //┌┼┘L2
        //└┼┐L2
        //┌┴┘L3
        //└┬┐L4
        //┌┘│L5
        //└┐│L5
        //┌┼┘L6
        //└┼┐L6
        //┌┴┘L7
        var Comparer=new ExpressionEqualityComparer();
        var L0=new 辺(Comparer) { 親コメント="L0" };
        var L1=new 辺(Comparer) { 親コメント="L1" };
        var L2=new 辺(Comparer) { 親コメント="L2" };
        var L3=new 辺(Comparer) { 親コメント="L3" };
        var L4=new 辺(Comparer) { 親コメント="L4" };
        var L5=new 辺(Comparer) { 親コメント="L5" };
        var L6=new 辺(Comparer) { 親コメント="L6" };
        var L7=new 辺(Comparer) { 親コメント="L7" };

        辺.接続(L0,L1);
        辺.接続(L0,L2);
        辺.接続(L1,L3);
        辺.接続(L2,L3);
        辺.接続(L4,L5);
        辺.接続(L4,L6);
        辺.接続(L5,L7);
        辺.接続(L6,L7);
        var l=new List辺{
            L0,
            L1,
            L2,
            L3,
            L4,
            L5,
            L6,
            L7,
        };
        Trace.WriteLine(l.Analize);
    }
    /// <summary>
    /// if(test){
    ///     if(test){
    ///     }else{
    ///     }
    /// }else{
    /// }
    /// </summary>
    [Fact]public void IfThenElseEnd2(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┬┐　　　　　　　0,IfTest,L0,子 
        //┌┘│　　　　　　　1,IfTest,L00,親 (辺番号0 L0, 辺番号1 L00)
        //└┬┼┐　　　　　　1,IfTest,L00,子 
        //┌┘││　　　　　　2,IfTrue,L01,親 (辺番号1 L00, 辺番号2 L01)
        //└┐││　　　　　　2,IfTrue,L01,子 
        //┌┼┼┘　　　　　　3,IfFalse,L02,親 (辺番号1 L00, 辺番号3 L02)
        //└┼┼┐　　　　　　3,IfFalse,L02,子 
        //┌┴┼┘　　　　　　4,IfEnd,L03,親 (辺番号3 L02, 辺番号4 L03)
        //└┐│　　　　　　　4,IfEnd,L03,子 
        //┌┘│　　　　　　　5,IfTrue,L1,親 (辺番号4 L03, 辺番号5 L1)
        //└┐│　　　　　　　5,IfTrue,L1,子 
        //┌┼┘　　　　　　　6,IfFalse,L2,親 (辺番号0 L0, 辺番号6 L2)
        //└┼┐　　　　　　　6,IfFalse,L2,子 
        //┌┴┘　　　　　　　7,IfEnd,L3,親 (辺番号6 L2, 辺番号7 L3)
        var Comparer=new ExpressionEqualityComparer();
        var L0=new 辺(Comparer) { 親コメント="L0" };
        var L00=new 辺(Comparer) { 親コメント="L00" };
        var L01=new 辺(Comparer) { 親コメント="L01" };
        var L02=new 辺(Comparer) { 親コメント="L02" };
        var L03=new 辺(Comparer) { 親コメント="L03" };
        var L1=new 辺(Comparer) { 親コメント="L1" };
        var L2=new 辺(Comparer) { 親コメント="L2" };
        var L3=new 辺(Comparer) { 親コメント="L3" };
        辺.接続(L0,L00);

        辺.接続(L00,L01);
        辺.接続(L00,L02);
        辺.接続(L01,L03);
        辺.接続(L02,L03);
        辺.接続(L03,L1);

        辺.接続(L0,L2);
        辺.接続(L1,L3);
        辺.接続(L2,L3);
        var l=new List辺{
            L0,
            L00,
            L01,
            L02,
            L03,
            L1,
            L2,
            L3
        };
        Trace.WriteLine(l.Analize);
    }
    /// <summary>
    /// if(test){
    /// }else{
    ///     if(test){
    ///     }else{
    ///     }
    /// }
    /// </summary>
    [Fact]public void IfThenElseEnd3(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┬┐　　　　　　　0,IfTest,L0,子 
        //┌┘│　　　　　　　1,IfTrue,L1,親 (辺番号0 L0, 辺番号1 L1)
        //└┐│　　　　　　　1,IfTrue,L1,子 
        //┌┼┘　　　　　　　2,IfFalse,L2,親 (辺番号0 L0, 辺番号2 L2)
        //└┼┐　　　　　　　2,IfFalse,L2,子 
        //┌┼┘　　　　　　　3,IfTest,L20,親 (辺番号2 L2, 辺番号3 L20)
        //└┼┬┐　　　　　　3,IfTest,L20,子 
        //┌┼┘│　　　　　　4,IfTrue,L21,親 (辺番号3 L20, 辺番号4 L21)
        //└┼┐│　　　　　　4,IfTrue,L21,子 
        //┌┼┼┘　　　　　　5,IfFalse,L22,親 (辺番号3 L20, 辺番号5 L22)
        //└┼┼┐　　　　　　5,IfFalse,L22,子 
        //┌┼┴┘　　　　　　6,IfEnd,L23,親 (辺番号5 L22, 辺番号6 L23)
        //└┼┐　　　　　　　6,IfEnd,L23,子 
        //┌┴┘　　　　　　　7,IfEnd,L3,親 (辺番号6 L23, 辺番号7 L3)
        var Comparer=new ExpressionEqualityComparer();
        var L0=new 辺(Comparer) { 親コメント="L0" };
        var L1=new 辺(Comparer) { 親コメント="L1" };
        var L2=new 辺(Comparer) { 親コメント="L2" };
        var L20=new 辺(Comparer) { 親コメント="L20" };
        var L21=new 辺(Comparer) { 親コメント="L21" };
        var L22=new 辺(Comparer) { 親コメント="L22" };
        var L23=new 辺(Comparer) { 親コメント="L23" };
        var L3=new 辺(Comparer) { 親コメント="L3" };
        辺.接続(L0,L1);
        辺.接続(L0,L2);
        辺.接続(L1,L3);
        辺.接続(L2,L20);
        辺.接続(L20,L21);
        辺.接続(L20,L22);
        辺.接続(L21,L23);
        辺.接続(L22,L23);
        辺.接続(L23,L3);


        var l=new List辺{
            L0,
            L1,
            L2,
            L20,
            L21,
            L22,
            L23,
            L3
        };
        Trace.WriteLine(l.Analize);
    }
    /// <summary>
    /// ifelseをネスト
    /// </summary>
    [Fact]public void IfThenElseEnd5(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //├┐　L0
        //└┼┐L1
        //┌┘│L2
        //├─┘L3
        var Comparer=new ExpressionEqualityComparer();
        var l=new List辺();
        共通(3,out var L0,out var L3,"");
        Trace.WriteLine(l.Analize);

        bool 共通(int 深さ,out 辺 out_L0,out 辺 out_L3,string c){
            if(深さ==0){
                out_L0=null!;
                out_L3=null!;
                return false;
            }
            var L0=new 辺(Comparer){親コメント=$"L{c}0"};
            var L1=new 辺(Comparer){親コメント=$"L{c}1"};
            var L2=new 辺(Comparer){親コメント=$"L{c}2"};
            var L3=new 辺(Comparer){親コメント=$"L{c}3" };
            l.Add(L0);
            if(共通(深さ-1,out var L00,out var L03,"0"+c)) {
                辺.接続(L0,L00);
                辺.接続(L03,L1);
            } else{
                辺.接続(L0,L1);
            }
            辺.接続(L0,L2);
            辺.接続(L1,L3);
            l.Add(L1);
            l.Add(L2);
            if(共通(深さ-1,out var L20,out var L23,"2"+c)) {
                辺.接続(L2,L20);
                辺.接続(L23,L3);
            } else{
                辺.接続(L2,L3);
            }
            l.Add(L3);
            out_L0=L0;
            out_L3=L3;
            return true;
        }
    }
    [Fact]public void IfTrueでジャンプ1(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //├┐　　L0
        //└┼┐　L1
        //└┼┼┐L2
        //┌┘││L3
        //├─┼┘L4
        //├─┘  L5
        {
            var 計測Maneger=this.計測Maneger;
            var Comparer=new ExpressionEqualityComparer();
            var L0=new 辺(Comparer){親コメント="L0"};
            var L1=new 辺(Comparer){親コメント="L1"};
            var L2=new 辺(Comparer){親コメント="L2"};
            var L3=new 辺(Comparer){親コメント="L3"};
            var L4=new 辺(Comparer){親コメント="L4"};
            var L5=new 辺(Comparer){親コメント="L5"};

            辺.接続(L0,L1);
            辺.接続(L0,L3);
            辺.接続(L1,L5);

            辺.接続(L2,L4);
            辺.接続(L3,L4);
            辺.接続(L4,L5);
            var List辺=new List辺{
                L0,
                L1,
                L2,
                L3,
                L4,
                L5
            };
            Trace.WriteLine(List辺.Analize);
        }
        {
            var 計測Maneger=this.計測Maneger;
            var L0=new 計測(計測Maneger,0,"Label","L0","");
            var L1=new 計測(計測Maneger,1,"Label","L1","");
            var L2=new 計測(計測Maneger,2,"Label","L2","");
            var L3=new 計測(計測Maneger,3,"Label","L3","");
            var L4=new 計測(計測Maneger,4,"Label","L4","");
            var L5=new 計測(計測Maneger,5,"Label","L5","");

            計測.接続(L0,L1);
            計測.接続(L0,L3);
            計測.接続(L1,L5);
            計測.接続(L2,L4);
            計測.接続(L3,L4);
            計測.接続(L4,L5);
            Trace.WriteLine(計測Maneger.Analize(L0));
        }
    }
    [Fact]public void IfFallseでジャンプ(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //├┐　　L0
        //└┼┐　L1
        //┌┘│　L2
        //└┐│　L3
        //┌┼┘　L4
        //├┘　　L5
        var Comparer=new ExpressionEqualityComparer();
        var L0=new 辺(Comparer) { 親コメント="L0" };
        var L1=new 辺(Comparer) { 親コメント="L1" };
        var L2=new 辺(Comparer) { 親コメント="L2" };
        var L3=new 辺(Comparer) { 親コメント="L3" };
        var L4=new 辺(Comparer) { 親コメント="L4" };
        var L5=new 辺(Comparer) { 親コメント="L5" };
        辺.接続(L0,L1);
        辺.接続(L0,L2);
        辺.接続(L1,L4);
        辺.接続(L2,L3);
        辺.接続(L3,L5);
        辺.接続(L4,L5);
        var l=new List辺{
            L0,
            L1,
            L2,
            L3,
            L4,
            L5,
        };
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void IfFallseでジャンプ先頭から末尾迄無条件ジャンプ1(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┐　　L0
        //├┼┐　L1
        //└┼┼┐L2
        //┌┼┘│L3
        //└┼┐│L4
        //┌┼┼┘L5
        //├┼┘　L6
        //┌┘　　L7
        var Comparer=new ExpressionEqualityComparer();
        var L0=new 辺(Comparer) { 親コメント="L0" };
        var L1=new 辺(Comparer) { 親コメント="L1" };
        var L2=new 辺(Comparer) { 親コメント="L2" };
        var L3=new 辺(Comparer) { 親コメント="L3" };
        var L4=new 辺(Comparer) { 親コメント="L4" };
        var L5=new 辺(Comparer) { 親コメント="L5" };
        var L6=new 辺(Comparer) { 親コメント="L6" };
        var L7=new 辺(Comparer) { 親コメント="L7" };
        辺.接続(L0,L7);
        辺.接続(L1,L2);
        辺.接続(L1,L3);
        辺.接続(L2,L5);
        辺.接続(L3,L4);
        辺.接続(L4,L6);
        辺.接続(L5,L6);
        var l=new List辺{
            L0,
            L1,
            L2,
            L3,
            L4,
            L5,
            L6,
            L7
        };
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 複数2経路から単一経路に(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┐    goto jump
        //└┼┐  goto jump
        //┌┴┘  jump:
        var Comparer=new ExpressionEqualityComparer();
        var jump0=new 辺(Comparer) { 親コメント="jump0" };
        var jump1=new 辺(Comparer) { 親コメント="jump1" };
        var label=new 辺(Comparer) { 親コメント="label" };
        辺.接続(jump0,label);
        辺.接続(jump1,label);
        var l=new List辺{jump0,jump1,label};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 複数3経路から単一経路に(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┐    goto jump
        //└┼┐  goto jump
        //└┼┼┐goto jump
        //┌┴┴┘jump:
        var Comparer=new ExpressionEqualityComparer();
        var jump0=new 辺(Comparer) { 親コメント="jump0" };
        var jump1=new 辺(Comparer) { 親コメント="jump1" };
        var jump=new 辺(Comparer) { 親コメント="jump2" };
        var label=new 辺(Comparer) { 親コメント="label" };
        辺.接続(jump0,label);
        辺.接続(jump1,label);
        辺.接続(jump,label);
        var l=new List辺{jump0,jump1,jump,label};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 単一経路から複数2経路に(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //│     0,jump{}
        //└┬┐ 0,
        //┌┘│ 1,label0{}
        //│　│ 1,
        //┌─┘ 2,label1{}
        //│　　 2,
        var Comparer=new ExpressionEqualityComparer();
        var L0=new 辺(Comparer) { 親コメント="L0" };
        var L1=new 辺(Comparer) { 親コメント="L1" };
        var L2=new 辺(Comparer) { 親コメント="L2" };
        辺.接続(L0,L1);
        辺.接続(L0,L2);
        var l=new List辺{L0,L1,L2};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 単一経路から複数3経路に(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //│       0,jump{}
        //└┬┬┐ 0,
        //┌┘││ 1,label0{}
        //│　││ 1,
        //┌─┘│ 2,label1{}
        //│　　│ 2,
        //┌──┘ 3,label2{}
        //│　　　 3,
        var Comparer=new ExpressionEqualityComparer();
        var jump=new 辺(Comparer) { 親コメント="jump" };
        var label0=new 辺(Comparer) { 親コメント="label0" };
        var label1=new 辺(Comparer) { 親コメント="label1" };
        var label2=new 辺(Comparer) { 親コメント="label2" };
        辺.接続(jump,label0);
        辺.接続(jump,label1);
        辺.接続(jump,label2);
        var l=new List辺{jump,label0,label1,label2};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 複数2経路から複数2経路に0(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┬┐  0
        //┌┴┘  1
        var Comparer=new ExpressionEqualityComparer();
        var L0=new 辺(Comparer) { 親コメント="L0" };
        var L1=new 辺(Comparer) { 親コメント="L1" };
        辺.接続(L0,L1);
        辺.接続(L0,L1);
        var l=new List辺{L0,L1};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 複数2経路から複数2経路に2(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┬┐  0
        //┌┴┘  1
        //└┬┐  2
        //┌┴┘  3
        var Comparer=new ExpressionEqualityComparer();
        var L0=new 辺(Comparer) { 親コメント="L0" };
        var L1=new 辺(Comparer) { 親コメント="L1" };
        var L2=new 辺(Comparer) { 親コメント="L2" };
        辺.接続(L0,L1);
        辺.接続(L0,L1);
        辺.接続(L1,L2);
        辺.接続(L1,L2);
        var l=new List辺{L0,L1,L2};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 複数2経路から複数2経路に3(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┬┐　　　　　　　　0,Goto:L0
        //┌┴┘　　　　　　　　1,Goto,goto L1:
        //└┐　　　　　　　　　1,Goto:L1
        //┌┘　　　　　　　　　2,Label,goto L2:
        //└┬┐　　　　　　　　2,Label:L2
        //┌┴┘　　　　　　　　3,Label,goto L3:        //└┼┐  0
        var Comparer=new ExpressionEqualityComparer();
        var L0=new 辺(Comparer) { 親コメント="L0" };
        var L1=new 辺(Comparer) { 親コメント="L1" };
        var L2=new 辺(Comparer) { 親コメント="L2" };
        var L3=new 辺(Comparer) { 親コメント="L3" };
        辺.接続(L0,L1);
        辺.接続(L0,L1);
        辺.接続(L1,L2);
        辺.接続(L2,L3);
        辺.接続(L2,L3);
        var l=new List辺{L0,L1,L2,L3};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void Switch0(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┐0,
        //┌┘1,L1
        {
            var Comparer=new ExpressionEqualityComparer();
            var L0=new 辺(Comparer){親コメント="L0"};
            var L1=new 辺(Comparer){親コメント="L1"};
            辺.接続(L0,L1);
            var l=new List辺{L0,L1,};
            Trace.WriteLine(l.Analize);
        }
        {
            var 計測Maneger=this.計測Maneger;
            var L0=new 計測(計測Maneger,0,"Label","L0","");
            var L1=new 計測(計測Maneger,1,"Label","L1","");
            計測.接続(L0,L1);
            Trace.WriteLine(計測Maneger.Analize(L0));
        }
    }
    [Fact]public void Switch1(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //├┐　L0 switch
        //└┼┐L1 default
        //┌┘│L2 case 1
        //├─┘L3 end swtich
        {
            var Comparer=new ExpressionEqualityComparer();
            var L0=new 辺(Comparer){親コメント="L0"};
            var L1=new 辺(Comparer){親コメント="L1"};
            var L2=new 辺(Comparer){親コメント="L2"};
            var L3=new 辺(Comparer){親コメント="L3"};
            辺.接続(L0,L1);
            辺.接続(L0,L2);
            辺.接続(L1,L3);
            辺.接続(L2,L3);
            var l=new List辺{L0,L1,L2,L3};
            Trace.WriteLine(l.Analize);
        }
        {
            var 計測Maneger=this.計測Maneger;
            var L0=new 計測(計測Maneger,0,"switch","SelectValue","");
            var L1=new 計測(計測Maneger,1,"case","1:","");
            var L2=new 計測(計測Maneger,2,"case","2:","");
            var L3=new 計測(計測Maneger,3,"end switch","","");
            計測.接続(L0,L1);
            計測.接続(L0,L2);
            計測.接続(L1,L3);
            計測.接続(L2,L3);
            Trace.WriteLine(計測Maneger.Analize(L0));
        }
    }
    [Fact]public void Switch2(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┬┐0,switch
        //┌┴┘1,
        {
            var Comparer=new ExpressionEqualityComparer();
            var L0=new 辺(Comparer){親コメント="L0"};
            var L1=new 辺(Comparer){親コメント="L1"};
            var L2=new 辺(Comparer){親コメント="L2"};
            辺.接続(L0,L1);
            辺.接続(L0,L1);
            var l=new List辺{L0,L1,L2,};
            Trace.WriteLine(l.Analize);
        }
        {
            var 計測Maneger=this.計測Maneger;
            var L0=new 計測(計測Maneger,0,"switch","SelectValue","");
            var L1=new 計測(計測Maneger,1,"case","1:","");
            var L2=new 計測(計測Maneger,2,"case","2:","");
            var L3=new 計測(計測Maneger,3,"end switch","","");
            計測.接続(L0,L1);
            計測.接続(L0,L2);
            計測.接続(L1,L3);
            計測.接続(L2,L3);
            Trace.WriteLine(計測Maneger.Analize(L0));
        }
    }
    [Fact]public void Switch3(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┬┐0,switch
        //┌┴┘1,
        {
            var 計測Maneger=this.計測Maneger;
            var L0=new 計測(計測Maneger,0,"switch","SelectValue","");
            var L1=new 計測(計測Maneger,1,"case","1:","");
            var L2=new 計測(計測Maneger,2,"case","2:","");
            var L3=new 計測(計測Maneger,3,"case","3:","");
            var L4=new 計測(計測Maneger,4,"end switch","","");
            計測.接続(L0,L1);
            計測.接続(L0,L2);
            計測.接続(L0,L3);
            計測.接続(L1,L4);
            計測.接続(L2,L4);
            計測.接続(L3,L4);
            Trace.WriteLine(計測Maneger.Analize(L0));
        }
    }
    [Fact]public void LabelLabel(){
        var Label0=Expression.Label("Label0");
        var Label1=Expression.Label("Label1");
        this.Expression実行AssertEqual(
            Expression.Lambda<Action>(
                Expression.Block(
                    Expression.Label(Label0),
                    Expression.Label(Label1)
                )
            )
        );
    }
    private void 式変形のみ(Expression<Action> input) {
        var Optimizer=this.Optimizer;
        Optimizer.IsInline=true;
        Optimizer.CreateDelegate(input);
        Trace.WriteLine(Optimizer.Analize);
    }
    [Fact]public void Loop(){
        this.式変形のみ(
            Expression.Lambda<Action>(
                Expression.Loop(
                    Expression.Add(Expression.Constant(1m),Expression.Constant(1m))
                )
            )
        );
        this.変換_局所Parameterの先行評価.TraverseNullable(
            Expression.Loop(
                Expression.Add(Expression.Constant(1m),Expression.Constant(1m))
            )
        );
    }
    [Fact]public void LoopBreak_外Continue(){
        var Break=Expression.Label(typeof(decimal),"Break");
        var Continue=Expression.Label("Continue");
        this.Expression実行AssertEqual(
            Expression.Lambda<Action>(
                Expression.Block(
                    Expression.Label(Continue),
                    Expression.Loop(
                        Expression.Block(
                            Expression.Add(Expression.Constant(1m),Expression.Constant(1m)),
                            Expression.Break(Break,Expression.Constant(1m)),
                            Expression.Continue(Continue)
                        ),
                        Break
                    )
                )
            )
        );
    }
    [Fact]public void LoopBreak_内Continue(){
        //│0,
        //└┐0,開始
        //┌┴┐1,Loop
        //└┐│1,
        //┌┴┼┐2,()Continue:
        //└┐││2,goto(局所0)Break
        //││││3,デッドコード先頭
        //└┼┼┘3,goto()Continue
        //│││　4,デッドコード先頭
        //└┼┘　4,End Loop
        //┌┘　　5,Loop Break Break:
        //│　　　5,goto(局所0)Break
        var Break=Expression.Label(typeof(decimal),"Break");
        var Continue=Expression.Label("Continue");
        this.Expression実行AssertEqual(
            Expression.Lambda<Action>(
                Expression.Loop(
                    Expression.Block(
                        Expression.Label(Continue),
                        Expression.Add(Expression.Constant(1m),Expression.Constant(1m)),
                        Expression.Break(Break,Expression.Constant(1m)),
                        Expression.Continue(Continue)
                    ),
                    Break
                )
            )
        );
    }
    [Fact]public void LoopBreak0(){
        var Break=Expression.Label(typeof(decimal),"Break");
        this.Expression実行AssertEqual(
            Expression.Lambda<Action>(
                Expression.Loop(
                    Expression.Block(
                        Expression.Add(Expression.Constant(1m),Expression.Constant(1m)),
                        Expression.Break(Break,Expression.Constant(1m))
                    ),
                    Break
                )
            )
        );
    }
    [Fact]public void LoopBreak1(){
        var Break=Expression.Label(typeof(decimal),"Break");
        this.Expression実行AssertEqual(
            Expression.Lambda<Action>(
                Expression.Loop(
                    Expression.Block(
                        Expression.Add(Expression.Constant(1m),Expression.Constant(1m)),
                        Expression.IfThen(
                            Expression.Constant(true),
                            Expression.Break(Break,Expression.Constant(1m))
                        )
                    ),
                    Break
                )
            )
        );
    }
    [Fact]public void LoopBreakContinue0(){
        var Label_decimal=Expression.Label(typeof(decimal),"Break decimal");
        var Label_void=Expression.Label("Continue void");
        this.Expression実行AssertEqual(
            Expression.Lambda<Action>(
                Expression.Loop(
                    Expression.Block(
                        Expression.Break(Label_decimal,Expression.Constant(1m)),
                        Expression.Continue(Label_void)
                    ),
                    Label_decimal,
                    Label_void
                )
            )
        );
    }
    [Fact]public void LoopBreakContinue1(){
        //│0,
        //└┐0,開始
        //┌┴┐1,Loop Continue void:
        //└┐│1,goto(局所0)Break decimal
        //│││2,デッドコード先頭
        //└┼┼┐2,goto()Continue void
        //││││3,デッドコード先頭
        //└┼┘│3,End Loop
        //┌┘　│4,Loop Break Break decimal:
        //│　　│4,goto(局所0)Break decimal
        var Label_decimal=Expression.Label(typeof(decimal),"Break decimal");
        var Label_void=Expression.Label("Continue void");
        this.Expression実行AssertEqual(
            Expression.Lambda<Action>(
                Expression.Loop(
                    Expression.Block(
                        Expression.Add(Expression.Constant(1m),Expression.Constant(1m)),
                        Expression.Break(Label_decimal,Expression.Constant(1m)),
                        Expression.Continue(Label_void)
                    ),
                    Label_decimal,
                    Label_void
                )
            )
        );
    }
    //[Fact]public void ラベルフォールスルー(){
    //    Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
    //    //└┬┐  0
    //    //┌┴┘  1
    //    var Comparer=new ExpressionEqualityComparer();
    //    var 番号=0;
    //    var L0=new 辺に関する情報(Comparer,ref 番号,"L0");
    //    var L1=new 辺に関する情報(Comparer,ref 番号,"L1");
    //    var L1=new 辺に関する情報(Comparer,ref 番号,"L2");
    //    var L1=new 辺に関する情報(Comparer,ref 番号,"L3");
    //    辺に関する情報.接続(L0,L1);
    //    辺に関する情報.接続(L0,L1);
    //    var l=new LinqDB.Optimizers.ReturnExpressionTraverser.List辺に関する情報{
    //        L0,
    //        L1,
    //        L2
    //    };
    //    Trace.WriteLine(l.フロー);
    //}
    [Fact]public void 変形確認2TryCatch(){
        var _1m = Expression.Constant(1m);
        //this.変換_局所Parameterの先行評価_実行(
        //    Expression.Condition(
        //        Expression.Constant(true),
        //        Expression.Add(_1m,_1m),
        //        _1m
        //    )
        //);
        this.Expression実行AssertEqual(
            Expression.TryCatch(
                Expression.Condition(
                    Expression.Constant(true),
                    Expression.Add(_1m,_1m),
                    _1m
                ),
                Expression.Catch(
                    typeof(Exception),
                    _1m
                )
            )
        );
        this.Expression実行AssertEqual(
            Expression.TryCatch(
                Expression.Condition(
                    Expression.Equal(_1m,_1m),
                    Expression.Add(_1m,_1m),
                    Expression.Subtract(_1m,_1m)
                ),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Condition(
                        Expression.NotEqual(_1m,_1m),
                        Expression.Divide(_1m,_1m),
                        Expression.Multiply(_1m,_1m)
                    )
                )
            )
        );
    }
}
