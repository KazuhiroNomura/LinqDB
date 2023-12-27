
using System.Diagnostics;
using System.Reflection;
using LinqDB.Optimizers.Comparer;
using LinqDB.Optimizers.ReturnExpressionTraverser;
namespace TestLinqDB.特殊パターン;
public class 計測フロー: 共通
{
    protected override テストオプション テストオプション{get;}=テストオプション.None;
    [Fact]public void 辺に関する情報Conditional0(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //├┐　　　　　　　　　　　　　　　　　　0,IfTest:
        //└┼┐　　　　　　　　　　　　　　　　　1,IfTrue:
        //┌┘│　　　　　　　　　　　　　　　　　2,IfFalse:
        //├─┘　　　　　　　　　　　　　　　　　3,IfEnd:
        var Comparer=new ExpressionEqualityComparer();
        var 番号=0;
        var test=new 計測する親(0);
        var ifTrue=new 計測する親(1);
        var ifFalse=new 計測する親(2);
        var endif=new 計測する親(3);
        var l=new List計測();
        A計測.接続(test,ifTrue);
        A計測.接続(test,ifFalse);
        A計測.接続(ifTrue,endif);
        A計測.接続(ifFalse,endif);
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
        var L0=new 計測する親(0) { 親コメント="L0" };
        var L1=new 計測する親(1) { 親コメント="L1" };
        A計測.接続(L0,L1);
        var l=new List計測{L0,L1};
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
        var L0=new 計測する親(0) { 親コメント="L0" };
        var L1=new 計測する親(1) { 親コメント="L1" };
        var L2=new 計測する親(2) { 親コメント="L2" };
        A計測.接続(L0,L1);
        A計測.接続(L1,L2);
        var l=new List計測{L0,L1,L2};
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
            var L0=new 計測する親(0,"Label","L0");
            var L1=new 計測する親(1,"Label","L1");
            var L2=new 計測する親(2,"Label","L2");
            var L3=new 計測する親(3,"Label","L3");
            A計測.接続(L0,L2);
            A計測.接続(L2,L1);
            A計測.接続(L1,L3);
            var l=new List計測{L0,L1,L2,L3};
            Trace.WriteLine(l.Analize);
        }
    }
    [Fact]public void 辺に関する情報Goto2(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┐
        //└┼┐
        //┌┼┘
        //┌┘
        var Comparer=new ExpressionEqualityComparer();
        var 番号=0;
        var L0=new 計測する親(0) { 親コメント="L0" };
        var L1=new 計測する親(1) { 親コメント="L1" };
        var L2=new 計測する親(2) { 親コメント="L2" };
        var L3=new 計測する親(3) { 親コメント="L3" };
        A計測.接続(L0,L3);
        A計測.接続(L1,L2);
        var l=new List計測{L0,L1,L2,L3};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 辺に関する情報ループ00(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //├←┐
        //└─┘
        var Comparer=new ExpressionEqualityComparer();
        var 番号=0;
        var L0=new 計測する親(0) { 親コメント="L0" };
        A計測.接続(L0,L0);
        var l=new List計測{L0};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 辺に関する情報ループ01(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┐0,
        //┌┴┐1,L1
        //└─┘1,
        var Comparer=new ExpressionEqualityComparer();
        var 番号=0;
        var L0=new 計測する親(0) { 親コメント="L0" };
        var L1=new 計測する親(1) { 親コメント="L1" };
        A計測.接続(L0,L1);
        A計測.接続(L1,L1);
        var l=new List計測{L0,L1};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 辺に関する情報ループ1(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //├┐　　　　　　　　　0,Label,Root,親 (辺番号1 L0, 辺番号0 Root)
        //└┼┐　　　　　　　　0,Label,Root,子 (辺番号0 Root, 辺番号1 L0)
        //┌┼┘　　　　　　　　1,Label,L0,親 (辺番号0 Root, 辺番号1 L0)
        //└┘　　　　　　　　　1,Label,L0,子 
        var Comparer=new ExpressionEqualityComparer();
        var 番号=0;
        var L0=new 計測する親(0) { 親コメント="L0" };
        var L1=new 計測する親(1) { 親コメント="L1" };
        A計測.接続(L0,L1);
        A計測.接続(L1,L0);
        var l=new List計測{L0,L1};
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
        var 番号=0;
        var L0=new 計測する親(0) { 親コメント="L0" };
        var L1=new 計測する親(1) { 親コメント="L1" };
        var L2=new 計測する親(2) { 親コメント="L2" };
        A計測.接続(L0,L1);
        A計測.接続(L1,L2);
        A計測.接続(L2,L0);
        var l=new List計測{L0,L1,L2};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 辺に関する情報ループ3(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┐
        //┌┘
        //├←┐
        //└─┘
        var Comparer=new ExpressionEqualityComparer();
        var 番号=0;
        var L0=new 計測する親(0) { 親コメント="L0" };
        var L1=new 計測する親(1) { 親コメント="L1" };
        //辺に関する情報.接続(L0,L1);
        A計測.接続(L0,L1);
        A計測.接続(L1,L1);
        var l=new List計測{L0,L1};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void IfThenElseEnd0(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┬┐0子
        //┌┘│1子
        //└┐│1子
        //┌┼┘2,IfFalse,L2:親
        //└┼┐2,IfFalse,L2:親
        //┌┴┘3,IfEnd,L3:親
        var Comparer=new ExpressionEqualityComparer();
        var 番号=0;
        var If=new 計測する親(4,"If","If");
        var L0=new 計測する親(0,"IfTest","L0");
        var L1=new 計測する親(1,"IfTrue","L1");
        var L2=new 計測する親(2,"IfFalse","L2");
        var L3=new 計測する親(3,"IfEnd","L3");
        If.List子演算.Add(L0);
        If.List子演算.Add(L1);
        If.List子演算.Add(L2);
        A計測.接続(L0,L1);
        A計測.接続(L0,L2);
        A計測.接続(L1,L3);
        A計測.接続(L2,L3);
        var l=new List計測{

            If
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
        var 番号=0;
        var L0=new 計測する親(0) { 親コメント="L0" };
        var L1=new 計測する親(1) { 親コメント="L1" };
        var L2=new 計測する親(2) { 親コメント="L2" };
        A計測.接続(L0,L1);
        A計測.接続(L1,L2);
        A計測.接続(L1,L1);
        var l=new List計測{L0,L1,L2};
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
        var 番号=0;
        var L0=new 計測する親(0) { 親コメント="L0" };
        var L1=new 計測する親(1) { 親コメント="L1" };
        var L2=new 計測する親(2) { 親コメント="L2" };
        var L3=new 計測する親(3) { 親コメント="L3" };
        var L4=new 計測する親(4) { 親コメント="L4" };
        var L5=new 計測する親(5) { 親コメント="L5" };
        var L6=new 計測する親(6) { 親コメント="L6" };
        var L7=new 計測する親(7) { 親コメント="L7" };

        A計測.接続(L0,L1);
        A計測.接続(L0,L2);
        A計測.接続(L1,L3);
        A計測.接続(L2,L3);
        A計測.接続(L4,L5);
        A計測.接続(L4,L6);
        A計測.接続(L5,L7);
        A計測.接続(L6,L7);
        var l=new List計測{
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
        var 番号=0;
        var L0=new 計測する親(0) { 親コメント="L0" };
        var L00=new 計測する親(100) { 親コメント="L00" };
        var L01=new 計測する親(101) { 親コメント="L01" };
        var L02=new 計測する親(102) { 親コメント="L02" };
        var L03=new 計測する親(103) { 親コメント="L03" };
        var L1=new 計測する親(1) { 親コメント="L1" };
        var L2=new 計測する親(12) { 親コメント="L2" };
        var L3=new 計測する親(13) { 親コメント="L3" };
        A計測.接続(L0,L00);

        A計測.接続(L00,L01);
        A計測.接続(L00,L02);
        A計測.接続(L01,L03);
        A計測.接続(L02,L03);
        A計測.接続(L03,L1);

        A計測.接続(L0,L2);
        A計測.接続(L1,L3);
        A計測.接続(L2,L3);
        var l=new List計測{
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
        var 番号=0;
        var L0=new 計測する親(0) { 親コメント="L0" };
        var L1=new 計測する親(1) { 親コメント="L1" };
        var L2=new 計測する親(2) { 親コメント="L2" };
        var L20=new 計測する親(20) { 親コメント="L20" };
        var L21=new 計測する親(21) { 親コメント="L21" };
        var L22=new 計測する親(22) { 親コメント="L22" };
        var L23=new 計測する親(23) { 親コメント="L23" };
        var L3=new 計測する親(3) { 親コメント="L3" };
        A計測.接続(L0,L1);
        A計測.接続(L0,L2);
        A計測.接続(L1,L3);
        A計測.接続(L2,L20);
        A計測.接続(L20,L21);
        A計測.接続(L20,L22);
        A計測.接続(L21,L23);
        A計測.接続(L22,L23);
        A計測.接続(L23,L3);


        var l=new List計測{
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
        var 番号=0;
        var l=new List計測();
        共通(3,out var L0,out var L3,"");
        Trace.WriteLine(l.Analize);

        bool 共通(int 深さ,out A計測 out_L0,out A計測 out_L3,string c){
            if(深さ==0){
                out_L0=null!;
                out_L3=null!;
                return false;
            }
            var L0=new 計測する親(0){親コメント=$"L{c}0"};
            var L1=new 計測する親(1){親コメント=$"L{c}1"};
            var L2=new 計測する親(2){親コメント=$"L{c}2"};
            var L3=new 計測する親(3){親コメント=$"L{c}3" };
            l.Add(L0);
            if(共通(深さ-1,out var L00,out var L03,"0"+c)) {
                A計測.接続(L0,L00);
                A計測.接続(L03,L1);
            } else{
                A計測.接続(L0,L1);
            }
            A計測.接続(L0,L2);
            A計測.接続(L1,L3);
            l.Add(L1);
            l.Add(L2);
            if(共通(深さ-1,out var L20,out var L23,"2"+c)) {
                A計測.接続(L2,L20);
                A計測.接続(L23,L3);
            } else{
                A計測.接続(L2,L3);
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
            var Comparer=new ExpressionEqualityComparer();
            var L0=new 計測する親(0,"Label","L0");
            var L1=new 計測する親(1,"Label","L1");
            var L2=new 計測する親(2,"Label","L2");
            var L3=new 計測する親(3,"Label","L3");
            var L4=new 計測する親(4,"Label","L4");
            var L5=new 計測する親(5,"Label","L5");

            A計測.接続(L0,L1);
            A計測.接続(L0,L3);
            A計測.接続(L1,L5);
            A計測.接続(L2,L4);
            A計測.接続(L3,L4);
            A計測.接続(L4,L5);
            var l=new List計測{
                L0,
                L1,
                L2,
                L3,
                L4,
                L5
            };
            Trace.WriteLine(l.Analize);
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
        var 番号=0;
        var L0=new 計測する親(0) { 親コメント="L0" };
        var L1=new 計測する親(1) { 親コメント="L1" };
        var L2=new 計測する親(2) { 親コメント="L2" };
        var L3=new 計測する親(3) { 親コメント="L3" };
        var L4=new 計測する親(4) { 親コメント="L4" };
        var L5=new 計測する親(5) { 親コメント="L5" };
        A計測.接続(L0,L1);
        A計測.接続(L0,L2);
        A計測.接続(L1,L4);
        A計測.接続(L2,L3);
        A計測.接続(L3,L5);
        A計測.接続(L4,L5);
        var l=new List計測{
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
        var 番号=0;
        var L0=new 計測する親(0) { 親コメント="L0" };
        var L1=new 計測する親(1) { 親コメント="L1" };
        var L2=new 計測する親(2) { 親コメント="L2" };
        var L3=new 計測する親(3) { 親コメント="L3" };
        var L4=new 計測する親(4) { 親コメント="L4" };
        var L5=new 計測する親(5) { 親コメント="L5" };
        var L6=new 計測する親(6) { 親コメント="L6" };
        var L7=new 計測する親(7) { 親コメント="L7" };
        A計測.接続(L0,L7);
        A計測.接続(L1,L2);
        A計測.接続(L1,L3);
        A計測.接続(L2,L5);
        A計測.接続(L3,L4);
        A計測.接続(L4,L6);
        A計測.接続(L5,L6);
        var l=new List計測{
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
        var jump0=new 計測する親(0) { 親コメント="jump0" };
        var jump1=new 計測する親(1) { 親コメント="jump1" };
        var label=new 計測する親(2) { 親コメント="label" };
        A計測.接続(jump0,label);
        A計測.接続(jump1,label);
        var l=new List計測{jump0,jump1,label};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 複数3経路から単一経路に(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┐    goto jump
        //└┼┐  goto jump
        //└┼┼┐goto jump
        //┌┴┴┘jump:
        var jump0=new 計測する親(0) { 親コメント="jump0" };
        var jump1=new 計測する親(1) { 親コメント="jump1" };
        var jump=new 計測する親(2) { 親コメント="jump2" };
        var label=new 計測する親(3) { 親コメント="label" };
        A計測.接続(jump0,label);
        A計測.接続(jump1,label);
        A計測.接続(jump,label);
        var l=new List計測{jump0,jump1,jump,label};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 単一経路から複数2経路に(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┬┐　　　　　　　0,Label,jump,子 
        //┌┼┘　　　　　　　2,Label,label1,親 (辺番号0 jump, 辺番号2 label1)
        //┌┘　　　　　　　　1,Label,label0,親 (辺番号0 jump, 辺番号1 label0)
        var jump=new 計測する親(0) { 親コメント="jump" };
        var label0=new 計測する親(1) { 親コメント="label0" };
        var label1=new 計測する親(2) { 親コメント="label1" };
        A計測.接続(jump,label0);
        A計測.接続(jump,label1);
        var l=new List計測{jump,label0,label1};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 単一経路から複数3経路に(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┐    goto jump
        //└┼┐  goto jump
        //└┼┼┐goto jump
        //┌┴┴┘jump:
        var jump=new 計測する親(0) { 親コメント="jump" };
        var label0=new 計測する親(1) { 親コメント="label0" };
        var label1=new 計測する親(2) { 親コメント="label1" };
        var label2=new 計測する親(3) { 親コメント="label2" };
        A計測.接続(jump,label0);
        A計測.接続(jump,label1);
        A計測.接続(jump,label2);
        var l=new List計測{jump,label0,label1,label2};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 複数2経路から複数2経路に0(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┬┐  0
        //┌┴┘  1
        var Comparer=new ExpressionEqualityComparer();
        var 番号=0;
        var L0=new 計測する親(0) { 親コメント="L0" };
        var L1=new 計測する親(1) { 親コメント="L1" };
        A計測.接続(L0,L1);
        A計測.接続(L0,L1);
        var l=new List計測{L0,L1};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void 複数2経路から複数2経路に2(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┬┐  0
        //┌┴┘  1
        //└┬┐  2
        //┌┴┘  3
        var Comparer=new ExpressionEqualityComparer();
        var 番号=0;
        var L0=new 計測する親(0) { 親コメント="L0" };
        var L1=new 計測する親(1) { 親コメント="L1" };
        var L2=new 計測する親(2) { 親コメント="L2" };
        A計測.接続(L0,L1);
        A計測.接続(L0,L1);
        A計測.接続(L1,L2);
        A計測.接続(L1,L2);
        var l=new List計測{L0,L1,L2};
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
        var 番号=0;
        var L0=new 計測する親(0) { 親コメント="L0" };
        var L1=new 計測する親(1) { 親コメント="L1" };
        var L2=new 計測する親(2) { 親コメント="L2" };
        var L3=new 計測する親(3) { 親コメント="L3" };
        A計測.接続(L0,L1);
        A計測.接続(L0,L1);
        A計測.接続(L1,L2);
        A計測.接続(L2,L3);
        A計測.接続(L2,L3);
        var l=new List計測{L0,L1,L2,L3};
        Trace.WriteLine(l.Analize);
    }
    [Fact]public void Switch0(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┐0,
        //┌┘1,L1
        {
            var L0=new 計測する親(0,"Label","L0");
            var L1=new 計測する親(1,"Label","L1");
            A計測.接続(L0,L1);
            var l=new List計測{L0,L1};
            Trace.WriteLine(l.Analize);
        }
    }
    [Fact]public void Switch1(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //├┐　L0 switch
        //└┼┐L1 default
        //┌┘│L2 case 1
        //├─┘L3 end swtich
        {
            var L0=new 計測する親(0,"switch","SelectValue");
            var L1=new 計測する親(1,"case","1:");
            var L2=new 計測する親(2,"case","2:");
            var L3=new 計測する親(3,"end switch","");
            A計測.接続(L0,L1);
            A計測.接続(L0,L2);
            A計測.接続(L1,L3);
            A計測.接続(L2,L3);
            var l=new List計測{L0,L1,L2,L3};
            Trace.WriteLine(l.Analize);
        }
    }
    [Fact]public void Switch2(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┬┐0,switch
        //┌┴┘1,
        {
            var L0=new 計測する親(0,"switch","SelectValue");
            var L1=new 計測する親(1,"case","1:");
            var L2=new 計測する親(2,"case","2:");
            var L3=new 計測する親(3,"end switch","");
            A計測.接続(L0,L1);
            A計測.接続(L0,L2);
            A計測.接続(L1,L3);
            A計測.接続(L2,L3);
            var l=new List計測{L0,L1,L2,L3};
            Trace.WriteLine(l.Analize);
        }
    }
    [Fact]public void Switch3(){
        Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
        //└┬┐0,switch
        //┌┴┘1,
        {
            var L0=new 計測する親(0,"switch","SelectValue");
            var L1=new 計測する親(1,"case","1:");
            var L2=new 計測する親(2,"case","2:");
            var L3=new 計測する親(3,"case","3:");
            var L4=new 計測する親(4,"end switch","");
            A計測.接続(L0,L1);
            A計測.接続(L0,L2);
            A計測.接続(L0,L3);
            A計測.接続(L1,L4);
            A計測.接続(L2,L4);
            A計測.接続(L3,L4);
            var l=new List計測{L0,L1,L2,L3,L4};
            Trace.WriteLine(l.Analize);
        }
    }
    //[Fact]public void ラベルフォールスルー(){
    //    Trace.WriteLine(MethodBase.GetCurrentMethod()!.Name);
    //    //└┬┐  0
    //    //┌┴┘  1
    //    var Comparer=new ExpressionEqualityComparer();
    //    var 番号=0;
    //    var L0=new 計測するに関する情報(Comparer,ref 番号,"L0");
    //    var L1=new 計測するに関する情報(Comparer,ref 番号,"L1");
    //    var L1=new 計測するに関する情報(Comparer,ref 番号,"L2");
    //    var L1=new 計測するに関する情報(Comparer,ref 番号,"L3");
    //    辺に関する情報.接続(L0,L1);
    //    辺に関する情報.接続(L0,L1);
    //    var l=new LinqDB.Optimizers.ReturnExpressionTraverser.List計測に関する情報{
    //        L0,
    //        L1,
    //        L2
    //    };
    //    Trace.WriteLine(l.Analize);
    //}
}
