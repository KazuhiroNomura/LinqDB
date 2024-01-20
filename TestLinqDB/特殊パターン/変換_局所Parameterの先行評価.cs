using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using LinqDB.Sets;
//using System.Reflection;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable All
namespace TestLinqDB.特殊パターン;
[MemoryPack.MemoryPackable,MessagePack.MessagePackObject(true),Serializable]
public partial class operator_true{
    [MemoryPack.MemoryPackInclude]
    private readonly bool 内部のBoolean;
    //public operator_true()=>this.内部のBoolean=true;
    public operator_true(){
        this.内部のBoolean=false;
    }
    [MemoryPack.MemoryPackConstructor]
    public operator_true(bool 内部のBoolean){
        this.内部のBoolean=内部のBoolean;
    }
    public static bool operator false(operator_true a)=>!a.内部のBoolean;
    public static bool operator true(operator_true a)=>a.内部のBoolean;
    public static operator_true operator&(operator_true a,operator_true b)=>new(a.内部のBoolean&b.内部のBoolean);
    public static operator_true operator|(operator_true a,operator_true b)=>new(a.内部のBoolean|b.内部のBoolean);
    public override bool Equals(object? obj){
        return obj is operator_true other&&this.内部のBoolean==other.内部のBoolean;
    }
    protected bool Equals(operator_true other){
        return this.内部のBoolean==other.内部のBoolean;
    }
    public override int GetHashCode(){
        return this.内部のBoolean.GetHashCode();
    }
    public static bool operator==(operator_true? left,operator_true? right){
        return Equals(left,right);
    }
    public static bool operator!=(operator_true? left,operator_true? right){
        return!Equals(left,right);
    }
}
public class 変換_局所Parameterの先行評価 : 共通{
    //protected override テストオプション テストオプション{get;}=テストオプション.MemoryPack_MessagePack_Utf8Json|テストオプション.ローカル実行;
    [Fact]public void Pattern0(){
        var p = Expression.Parameter(typeof(int), "p");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.Add(
                    Expression.Add(
                        p,
                        p
                    ),
                    Expression.Add(
                        p,
                        p
                    )
                ),
                p
            )
        );
    }
    [Fact]public void Pattern1(){
        var Label = Expression.Label( "Label");
        var p = Expression.Parameter(typeof(int), "p");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.Block(
                    Expression.Add(
                        p,
                        p
                    ),
                    Expression.Label(Label),
                    Expression.Add(
                        p,
                        p
                    )
                ),
                p
            )
        );
    }
    [Fact]public void Pattern2(){
        var Label = Expression.Label( "Label");
        var p = Expression.Parameter(typeof(int), "p");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.Block(
                    Expression.Add(
                        Expression.Add(
                            p,
                            p
                        ),
                        Expression.Add(
                            p,
                            p
                        )
                    ),
                    Expression.Label(Label),
                    Expression.Add(
                        p,
                        p
                    )
                ),
                p
            )
        );
    }
    [Fact]public void Pattern3(){
        var Label = Expression.Label(typeof(int), "Label");
        var p = Expression.Parameter(typeof(int), "p");
        var Add_pp=Expression.Add(
            p,
            p
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.Block(
                    Expression.Add(
                        Add_pp,
                        Add_pp
                    ),
                    Expression.Label(
                        Label,
                        Add_pp
                    ),
                    Add_pp
                ),
                p
            )
        );
    }
    [Fact]public void Pattern4(){
        var Label = Expression.Label(typeof(int), "Label");
        var p = Expression.Parameter(typeof(int), "p");
        var Add_pp=Expression.Add(
            p,
            p
        );
        var Multiply_pp=Expression.Multiply(
            p,
            p
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.Block(
                    Expression.Add(
                        Add_pp,
                        Add_pp
                    ),
                    Expression.Label(
                        Label,
                        Add_pp
                    ),
                    Expression.Add(
                        Multiply_pp,
                        Multiply_pp
                    )
                ),
                p
            )
        );
    }
    [Fact]public void Condition2分岐と1分岐の先行評価(){
        var p = Expression.Parameter(typeof(bool), "p");
        var pp=Expression.And(p,p);
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Expression.Condition(
                    pp,
                    pp,
                    pp
                ),
                p
            )
        );
    }
    [Fact]public void Condition0(){
        var p = Expression.Parameter(typeof(bool), "p");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Expression.Condition(
                    p,
                    p,
                    p
                ),
                p
            )
        );
    }
    [Fact]public void Condition1(){
        var p = Expression.Parameter(typeof(bool), "p");
        var pp=Expression.And(p,p);
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Expression.Condition(
                    p,
                    pp,
                    pp
                ),
                p
            )
        );
    }
    [Fact]public void Condition1分岐と2分岐の先行評価(){
        var p = Expression.Parameter(typeof(bool), "p");
        var pp=Expression.And(p,p);
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Expression.Block(
                    Expression.Condition(
                        p,
                        pp,
                        pp
                    ),
                    pp
                ),
                p
            )
        );
    }
    [Fact]public void Condition2連続(){
        var p = Expression.Parameter(typeof(bool), "p");
        var pp=Expression.And(p,p);
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Expression.Block(
                    Expression.Condition(pp,p,p),
                    Expression.Condition(p,p,pp)
                ),
                p
            )
        );
    }
    [Fact]public void Condition2入れ子(){
        var p = Expression.Parameter(typeof(bool), "p");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Expression.Condition(
                    Expression.Condition(
                        p,
                        p,
                        p
                    ),
                    Expression.Condition(
                        p,
                        p,
                        p
                    ),
                    Expression.Condition(
                        p,
                        p,
                        p
                    )
                ),
                p
            )
        );
    }
    [Fact]public void Pattern7(){
        var ifTrue=Expression.Label("ifTrue");
        var ifFalse=Expression.Label("ifFalse");
        var endif=Expression.Label(typeof(bool),"endif");
        var p = Expression.Parameter(typeof(bool), "p");
        var pp=Expression.And(p,p);
        var Block=Expression.Block(
            Expression.IfThenElse(
                p,
                Expression.Goto(ifTrue),
                Expression.Goto(ifFalse)
            ),
            Expression.Label(ifTrue,p),
            Expression.Goto(
                endif,
                pp
            ),
            Expression.Label(ifFalse,p),
            Expression.Label(
                endif,
                pp
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void 経路2_0(){
        //p&&p
        var ifFalse=Expression.Label("ifFalse");
        var endif=Expression.Label("endif");
        var p = Expression.Parameter(typeof(bool), "p");
        var r = Expression.Parameter(typeof(bool), "r");
        var Block=Expression.Block(
            new[]{r},
            Expression.IfThenElse(p,Expression.Default(typeof(void)),Expression.Goto(ifFalse)),
            Expression.Assign(r,Expression.And(p,p)),
            Expression.Goto(endif),
            Expression.Label(ifFalse),
            Expression.Assign(r,p),
            Expression.Label(endif),
            r
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }    
    [Fact]public void 経路2_1(){
        //│　r=p&p
        //└┐goto endif
        //　│ifFalse:
        //　│r=p
        //┌┘ifFalse:
        //│　r
        //ifFalse:ラベルの使用回数0

        var ifFalse=Expression.Label("ifFalse");
        var endif=Expression.Label("endif");
        var p = Expression.Parameter(typeof(bool), "p");
        var r = Expression.Parameter(typeof(bool), "r");
        var Block=Expression.Block(
            new[]{r},
            Expression.IfThenElse(p,Expression.Default(typeof(int)),Expression.Goto(ifFalse)),
            Expression.Assign(r,Expression.And(p,p)),
            Expression.Goto(endif),
            Expression.Label(ifFalse),
            Expression.Assign(r,p),
            Expression.Label(endif),
            r
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }    
    [Fact]public void 経路12_0(){
        //└┬┐0,IfTest (局所0 = (p And p))
        //┌┘│2,IfTrue 局所0
        //└┐│2,
        //┌┼┘3,IfFalse 局所0
        //└┼┐3,
        //┌┴┘1,End If
        var p = Expression.Parameter(typeof(bool), "p");
        var pp=Expression.And(p,p);
        var Block=Expression.Block(
            Expression.Block(
                Expression.Condition(
                    pp,
                    pp,
                    pp
                )
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void 経路12_1(){
        //└┬┐0,IfTest p
        //┌┘│2,IfTrue (p And p)
        //└┐│2,
        //┌┼┘3,IfFalse (p And p)
        //└┼┐3,
        //┌┴┘1,End If
        //$局所0 = $p & $p;
        //.If (
        //    $p
        //) {
        //    $局所0
        //} .Else {
        //    $局所0
        //}
        var p = Expression.Parameter(typeof(bool), "p");
        var pp=Expression.And(p,p);
        var Block=Expression.Block(
            Expression.Block(
                pp,
                Expression.Condition(
                    p,
                    pp,
                    pp
                )
            )
        );
        var Lambda=this.Optimizer.Lambda最適化(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void 経路12_手動共通部分式(){
        //└┬┐0,IfTest Not((p And p))
        //┌┘│2,IfTrue goto ifFalse
        //└┐│2,
        //└┼┼┐4,デッドコード
        //┌┼┘│5,IfFalse default(Void)
        //└┼┐│5,
        //┌┼┴┘1,End If
        //└┼┐　1,
        //┌┘│　3,()ifFalse:
        //┌─┘　6,()endif:
        //.If (
        //    !($p & $p)
        //) {
        //    .Goto ifFalse { }
        //} .Else {
        //    .Default(System.Void)
        //};
        //$r = $p & $p;
        //.Goto endif { };
        //.Label
        //.LabelTarget ifFalse:;
        //$r = $p & $p;
        //.Label
        //.LabelTarget endif:;
        //$r
        var r = Expression.Parameter(typeof(bool), "r");
        var p = Expression.Parameter(typeof(bool), "p");
        var pp=Expression.And(p,p);
        var ifFalse=Expression.Label("ifFalse");
        var endif=Expression.Label("endif");
        var Block=Expression.Block(
            new[]{r},
            Expression.IfThen(
                Expression.Not(pp),
                Expression.Goto(ifFalse)
            ),
            Expression.Assign(r,pp),
            Expression.Goto(endif),
            Expression.Label(ifFalse),
            Expression.Assign(r,pp),
            Expression.Label(endif),
            r
        );
        var Lambda=this.Optimizer.Lambda最適化(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void 経路12_自動共通部分式(){
        //└┬┐0,IfTest Not((p And p))
        //┌┘│2,IfTrue goto ifFalse
        //└┐│2,
        //└┼┼┐4,デッドコード
        //┌┼┘│5,IfFalse default(Void)
        //└┼┐│5,
        //┌┼┴┘1,End If
        //└┼┐　1,
        //┌┘│　3,()ifFalse:
        //┌─┘　6,()endif:
        //.If (
        //    !($p & $p)
        //) {
        //    .Goto ifFalse { }
        //} .Else {
        //    .Default(System.Void)
        //};
        //$r = $p & $p;
        //.Goto endif { };
        //.Label
        //.LabelTarget ifFalse:;
        //$r = $p & $p;
        //.Label
        //.LabelTarget endif:;
        //$r
        var r = Expression.Parameter(typeof(bool), "r");
        var p = Expression.Parameter(typeof(bool), "p");
        var pp=Expression.And(p,p);
        var ifFalse=Expression.Label("ifFalse");
        var endif=Expression.Label("endif");
        var Block=Expression.Block(
            new[]{r},
            Expression.IfThen(
                Expression.Not(pp),
                Expression.Goto(ifFalse)
            ),
            pp,
            Expression.Goto(endif),
            Expression.Label(ifFalse),
            pp,
            Expression.Label(endif),
            r
        );
        var Lambda=this.Optimizer.Lambda最適化(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void 経路12_3(){
        //p 　　　　　0
        //├────┐1 br_false 2 ifFalse
        //r=p&p 　　│
        //└───┐│  goto 3 endif
        //┌───┼┘2 ifFalse:
        //r=p&p 　│
        //├───┘　3 endif:
        //↓
        //かわらない
        var r = Expression.Parameter(typeof(bool), "r");
        var p = Expression.Parameter(typeof(bool), "p");
        var pp=Expression.And(p,p);
        var ifFalse=Expression.Label("ifFalse");
        var endif=Expression.Label("endif");
        var Block=Expression.Block(
            new[]{r},
            Expression.IfThen(
                Expression.Not(p),
                Expression.Goto(ifFalse)
            ),
            Expression.Assign(r,pp),
            Expression.Goto(endif),
            Expression.Label(ifFalse),
            Expression.Assign(r,pp),
            Expression.Label(endif),
            r
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void 経路21(){
        //p   　　　　0
        //├────┐1 br_false 2 ifFalse
        //p&p 　　　│
        //└───┐│  goto 3 endif
        //┌───┼┘2 ifFalse:
        //p&p 　　　│
        //├───┘　3 endif:
        //p&p
        //↓
        //p   　　　　0
        //├────┐1 br_false 2 ifFalse
        //t=p&p 　　│
        //└───┐│  goto 3 endif
        //┌───┼┘2 ifFalse:
        //t=p&p 　　│
        //├───┘　3 endif:
        //t
        var p = Expression.Parameter(typeof(bool), "p");
        var pp=Expression.And(p,p);
        var Block=Expression.Block(
            Expression.Block(
                Expression.Condition(
                    p,
                    pp,
                    pp
                ),
                pp
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void 経路21_1(){
        //└┬┐0,IfTest Not((p And p))
        //┌┘│2,IfTrue goto ifFalse
        //└┐│2,
        //└┼┼┐4,デッドコード
        //┌┼┘│5,IfFalse default(Void)
        //└┼┐│5,
        //┌┼┴┘1,End If
        //└┼┐　1,
        //┌┘│　3,()ifFalse:
        //┌─┘　6,((p And p))endif:
        //if(!p){
        //    goto ifFalse
        //}else{
        //    void
        //}
        //p&p
        //goto endif
        //ifFalse:
        //p&p
        //endif:
        //↓
        //if(p)
        //    t=p&p
        //else
        //    t=p&p
        //t
        var p = Expression.Parameter(typeof(bool), "p");
        var pp=Expression.And(p,p);
        var ifFalse=Expression.Label("ifFalse");
        var endif=Expression.Label(typeof(bool),"endif");
        var Block=Expression.Block(
            Expression.IfThen(
                Expression.Not(pp),
                Expression.Goto(ifFalse)
            ),
            Expression.Goto(endif,pp),
            Expression.Label(ifFalse),
            Expression.Label(endif,pp)
        );
        var Lambda=Expression.Lambda<Func<bool,bool>>(Block,p);
        var Lambda1=this.Optimizer.Lambda最適化(Lambda);
        this.Expression実行AssertEqual(Lambda);
    }
    [Fact]public void 経路121_0(){
        //p&p 　　　　0
        //├────┐1 br_false 2 ifFalse
        //p&p 　　　│
        //└───┐│  goto 3 endif
        //┌───┼┘2 ifFalse:
        //p&p 　　│
        //├───┘　3 endif:
        //↓
        //t=p&p 　　　0
        //├────┐1 br_false 2 ifFalse
        //t 　　　　│
        //└───┐│  goto 3 endif
        //┌───┼┘2 ifFalse:
        //t 　　　│
        //├───┘　3 endif:
        var p = Expression.Parameter(typeof(bool), "p");
        var pp=Expression.And(p,p);
        var Block=Expression.Block(
            Expression.Block(
                Expression.Condition(
                    pp,
                    pp,
                    pp
                )
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void 経路121_1(){
        //p&p 　　　　0
        //├────┐1 br_false 2 ifFalse
        //p&p 　　　│
        //└───┐│  goto 3 endif
        //┌───┼┘2 ifFalse:
        //p&p 　　│
        //├───┘　3 endif:
        //p&p
        //↓
        //t=p&p 　　　0
        //├────┐1 br_false 2 ifFalse
        //t 　　　　│
        //└───┐│  goto 3 endif
        //┌───┼┘2 ifFalse:
        //t 　　　│
        //├───┘　3 endif:
        //t
        var p = Expression.Parameter(typeof(bool), "p");
        var pp=Expression.And(p,p);
        var Block=Expression.Block(
            Expression.Block(
                Expression.Condition(
                    pp,
                    pp,
                    pp
                ),
                pp
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void 経路121_2(){
        //p&p 　　　　0
        //├────┐1 br_false 2 ifFalse
        //p&p 　　　│
        //└───┐│  goto 3 endif
        //┌───┼┘2 ifFalse:
        //p&p|p&p │
        //├───┘　3 endif:
        //p&p
        //↓
        //t=p&p 　　　0
        //├────┐1 br_false 2 ifFalse
        //t 　　　　│
        //└───┐│  goto 3 endif
        //┌───┼┘2 ifFalse:
        //t|t 　　│
        //├───┘　3 endif:
        //t
        var p = Expression.Parameter(typeof(bool), "p");
        var pp=Expression.And(p,p);
        var Block=Expression.Block(
            Expression.Block(
                Expression.Condition(
                    pp,
                    pp,
                    Expression.Or(pp,pp)
                ),
                pp
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void 経路121_3(){
        //p&p 　　　　0
        //├────┐1 br_false 2 ifFalse
        //p&p 　　　│
        //└───┐│  goto 3 endif
        //┌───┼┘2 ifFalse:
        //p&p|p&p │
        //├───┘　3 endif:
        //p&p|p&p
        //↓
        //t=p&p 　　　0
        //├────┐1 br_false 2 ifFalse
        //t 　　　　│
        //└───┐│  goto 3 endif
        //┌───┼┘2 ifFalse:
        //t|t 　　│
        //├───┘　3 endif:
        //t|t
        var p = Expression.Parameter(typeof(bool), "p");
        var pp=Expression.And(p,p);
        var Block=Expression.Block(
            Expression.Block(
                Expression.Condition(
                    pp,
                    pp,
                    Expression.Or(pp,pp)
                ),
                Expression.Or(pp,pp)
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void 経路121_31(){
        var p = Expression.Parameter(typeof(bool), "p");
        var pp=Expression.And(p,p);
        var Block=Expression.Block(
            Expression.Block(
                Expression.Condition(
                    p,
                    p,
                    pp
                ),
                pp
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void 経路121_4(){
        //p&p 　　　　0
        //├────┐1 br_false 2 ifFalse
        //p&p|p&p 　│
        //└───┐│  goto 3 endif
        //┌───┼┘2 ifFalse:
        //p&p|p&p │
        //├───┘　3 endif:
        //p&p|p&p
        //↓
        //t=p&p 　　　0
        //├────┐1 br_false 2 ifFalse
        //t0=t|t　　│
        //└───┐│  goto 3 endif
        //┌───┼┘2 ifFalse:
        //t0=t|t　　│
        //├───┘　3 endif:
        //t0
        var p = Expression.Parameter(typeof(bool), "p");
        var pp=Expression.And(p,p);
        var Block=Expression.Block(
            Expression.Block(
                Expression.Condition(
                    pp,
                    Expression.Or(pp,pp),
                    Expression.Or(pp,pp)
                ),
                Expression.Or(pp,pp)
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void 経路3(){
        //├L1:────────────┐
        //├Conditional───┬┐      │
        //│├Conditional──┼┼┬┐  │
        //││├Parameter p  ││││  │
        //││├AssignA計測─┼┼┘│  │
        //││└goto L0───┼┼─┴┐│
        //│├Conditional──┴┼┬┐││
        //││├Parameter p　　│││││
        //││├AssignA計測──┼┘│││
        //││└Assign ────┼─┘││
        //│├Conditional───┴┬┐││
        //││├Parameter p      ││││
        //││├AssignA計測───┘│││
        //││└Assign ──────┘││
        //├L0:───────────┘│
        //└goto L1──────────┘
        var p = Expression.Parameter(typeof(bool), "p");
        var L0=Expression.Label("L0");
        var L1=Expression.Label("L1");
        var ppp=Expression.Condition(
            p,
            p,p
        );
        var ppGoto=Expression.IfThenElse(
            p,
            p,
            Expression.Goto(L0)
        );
        var Block=Expression.Block(
            Expression.Label(L1),
            Expression.IfThenElse(
                ppp,
                ppGoto,
                ppp
            ),
            Expression.Label(L0),
            Expression.Goto(L1)
        );
        this.Optimizer_Lambda最適化(
            Expression.Lambda(
                Block,
                p
            )
        );
    }
    [Fact]public void 無条件ジャンプ0下(){
        //     物理番号
        //       論隷番号
        //1m   0
        //└┐     goto L2
        //││ 1
        //├┘ 2 0 L2:
        //1m
        //↓
        //t=1m 0
        //└┐     goto L2
        //││ 1
        //├┘ 2 0 L2:
        //t
        var L2=Expression.Label("L2");
        var Block=Expression.Block(
            Expression.Constant(1m),
            Expression.Goto(L2),
            Expression.Label(L2),
            Expression.Constant(1m)
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<decimal>>(
                Block
            )
        );
    }
    [Fact]public void 無条件下ジャンプのみ0(){
        var L0=Expression.Label("L0");
        var L1=Expression.Label("L1");
        var Block=Expression.Block(
            Expression.Goto(L0),
            Expression.Label(L0),
            Expression.Goto(L1),
            Expression.Label(L1)
        );
        this.Optimizer_Lambda最適化(
            Expression.Lambda(
                Block
            )
        );
    }
    [Fact]public void 無条件上ジャンプのみ0(){
        var L0=Expression.Label("L0");
        var L1=Expression.Label("L1");
        var Block=Expression.Block(
            Expression.Label(L0),
            Expression.Goto(L0),
            Expression.Label(L1),
            Expression.Goto(L1)
        );
        this.Optimizer_Lambda最適化(
            Expression.Lambda(
                Block
            )
        );
    }
    [Fact]public void 無条件下ジャンプのみ1(){
        var L0=Expression.Label("L0");
        var L1=Expression.Label("L1");
        var Block=Expression.Block(
            Expression.Goto(L0),
            Expression.Label(L0),
            Expression.Goto(L1),
            Expression.Label(L1)
        );
        this.Optimizer_Lambda最適化(
            Expression.Lambda(
                Block
            )
        );
    }
    [Fact]public void 無条件上ジャンプのみ1(){
        var L0=Expression.Label("L0");
        var L1=Expression.Label("L1");
        var Block=Expression.Block(
            Expression.Label(L0),
            Expression.Goto(L0),
            Expression.Label(L1),
            Expression.Goto(L1)
        );
        this.Optimizer_Lambda最適化(
            Expression.Lambda(
                Block
            )
        );
    }
    [Fact]public void 無条件下ジャンプのみ2(){
        var L0=Expression.Label("L0");
        var L1=Expression.Label("L1");
        var L2=Expression.Label("L2");
        var Block=Expression.Block(
            Expression.Goto(L0),
            Expression.Label(L0),
            Expression.Goto(L1),
            Expression.Label(L1),
            Expression.Goto(L2),
            Expression.Label(L2)
        );
        this.Optimizer_Lambda最適化(
            Expression.Lambda(
                Block
            )
        );
    }
    [Fact]public void 無条件ジャンプ0213制御文のみ0(){
        var L1=Expression.Label("L1");
        var L2=Expression.Label("L2");
        var Block=Expression.Block(
            Expression.Label(L1),
            Expression.Goto(L2),
            Expression.Goto(L1),
            Expression.Label(L2)
        );
        this.Optimizer_Lambda最適化(
            Expression.Lambda(
                Block
            )
        );
    }
    [Fact]public void 無条件ジャンプ0213制御文のみ1(){
        var L0=Expression.Label("L0");
        var L1=Expression.Label("L1");
        var Block=Expression.Block(
            Expression.Goto(L0),
            Expression.Label(L1),
            Expression.Label(L0),
            Expression.Goto(L1)
        );
        this.Optimizer_Lambda最適化(
            Expression.Lambda(
                Block
            )
        );
    }
    [Fact]public void 無条件ジャンプ0213制御文のみ2(){
        var L0=Expression.Label("L0");
        var L1=Expression.Label("L1");
        var L2=Expression.Label("L2");
        var Block=Expression.Block(
            Expression.Goto(L0),
            Expression.Label(L1),
            Expression.Goto(L2),
            Expression.Label(L0),
            Expression.Goto(L1),
            Expression.Label(L2)
        );
        this.Optimizer_Lambda最適化(
            Expression.Lambda(
                Block
            )
        );
    }
    [Fact]public void 無条件ジャンプ0213値有り(){
        var L0=Expression.Label("L0");
        var L1=Expression.Label("L1");
        var L2=Expression.Label("L2");
        var Block=Expression.Block(
            Expression.Goto(L0),
            Expression.Label(L1),
            Expression.Constant(1m),
            Expression.Goto(L2),
            Expression.Label(L0),
            Expression.Constant(1m),
            Expression.Goto(L1),
            Expression.Label(L2),
            Expression.Constant(true)
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool>>(
                Block
            )
        );
    }
    [Fact]public void 無条件ジャンプ20(){
        //└──┐0 br L0
        //┌─┐│1 L1:
        //1m  ││
        //└┐││  br L2
        //┌┼┼┘2 L0:
        //1m││  
        //└┼┘    br L1
        //┌┘    3 L2:
        //1m
        //↓
        //└────┐ 0 br L0
        //┌───┐│1 L1:
        //t0      ││
        //└──┐││  br L2
        //┌──┼┼┘2 L0:
        //t0=1m ││  
        //└──┼┘    br L1
        //┌──┘    3 L2:
        //t1
        var L0=Expression.Label("L0");
        var L1=Expression.Label("L1");
        var L2=Expression.Label("L2");
        var Block=Expression.Block(
            Expression.Goto(L0),
            Expression.Label(L1),
            Expression.Constant(1m),
            Expression.Goto(L2),
            Expression.Label(L0),
            Expression.Constant(1m),
            Expression.Goto(L1),
            Expression.Label(L2),
            Expression.Constant(1m)
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<decimal>>(
                Block
            )
        );
    }
    [Fact]public void 無条件ジャンプ21(){
        //└──┐0 br L0
        //┌─┐│1 L1:
        //2m  ││
        //└┐││  br L2
        //┌┼┼┘2 L0:
        //1m││  
        //└┼┘    br L1
        //┌┘    3 L2:
        //1m
        //↓
        //└────┐ 0 br L0
        //┌───┐│1 L1:
        //2m      ││
        //└──┐││  br L2
        //┌──┼┼┘2 L0:
        //t0=1m ││  
        //└──┼┘    br L1
        //┌──┘    3 L2:
        //t0
        var L0=Expression.Label("L0");
        var L1=Expression.Label("L1");
        var L2=Expression.Label("L2");
        var Block=Expression.Block(
            Expression.Goto(L0),
            Expression.Label(L1),
            Expression.Constant(2m),
            Expression.Goto(L2),
            Expression.Label(L0),
            Expression.Constant(1m),
            Expression.Goto(L1),
            Expression.Label(L2),
            Expression.Constant(1m)
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<decimal>>(
                Block
            )
        );
    }
    [Fact]public void 無条件ジャンプ3(){
        //└┐0,最上位
        //└┼┐2,Label2
        //┌┼┴┐3,()L1:
        //└┼┐│3,
        //┌┘││1,()L0:
        //└─┼┘1,goto()L0
        //└┐│　6,Label2
        //┌┴┼┐7,()L3:
        //└┐││7,
        //┌┼┘│4,()L2:
        //└┼─┘4,goto()L2
        //┌┘　　8,()L4:
        var L0=Expression.Label("L0");
        var L1=Expression.Label("L1");
        var L2=Expression.Label("L2");
        var L3=Expression.Label("L3");
        var L4=Expression.Label("L4");
        var Block=Expression.Block(
            Expression.Goto(L0),
            Expression.Label(L1),
            Expression.Constant(1m),
            Expression.Goto(L2),
            Expression.Label(L0),
            Expression.Constant(1m),
            Expression.Goto(L1),
            Expression.Label(L3),
            Expression.Constant(3m),
            Expression.Goto(L4),
            Expression.Label(L2),
            Expression.Constant(2m),
            Expression.Goto(L3),
            Expression.Label(L4),
            Expression.Constant(2m)
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<decimal>>(
                Block
            )
        );
    }
    [Fact]public void Conditional内部がジャンプ00(){
        var r = Expression.Parameter(typeof(bool), "r");
        var p = Expression.Parameter(typeof(bool), "p");
        var p_And_p=Expression.And(p,p);
        var p_Or_p=Expression.Or(p,p);
        var ifFalse=Expression.Label("ifFalse");
        var Block=Expression.Block(
            new[]{r},
            Expression.Assign(r,Expression.Constant(false)),
            Expression.IfThen(
                Expression.Not(p_And_p),
                Expression.Goto(ifFalse)
            ),
            Expression.Assign(r,p_Or_p),
            Expression.Label(ifFalse),
            r
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void Conditional内部がジャンプ01(){
        var p = Expression.Parameter(typeof(int), "p");
        var ifFalse=Expression.Label(typeof(int),"ifFalse");
        var Block=Expression.Block(
            Expression.IfThen(
                Expression.GreaterThan(
                    p,
                    Expression.Constant(0)
                ),
                Expression.Goto(
                    ifFalse,
                    Expression.Constant(1)
                )
            ),
            Expression.Label(
                ifFalse,
                Expression.Constant(2)
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Block,
                p
            )
        );
    }
    [Fact]public void Conditional内部がジャンプ02(){
        var r = Expression.Parameter(typeof(bool), "r");
        var p = Expression.Parameter(typeof(bool), "p");
        var p_And_p=Expression.And(p,p);
        var p_Or_p=Expression.Or(p,p);
        var ifFalse=Expression.Label("ifFalse");
        var endif=Expression.Label("endif");
        var Block=Expression.Block(
            new[]{r},
            Expression.IfThen(
                Expression.Not(p_And_p),
                Expression.Goto(ifFalse)
            ),
            Expression.Assign(r,p_Or_p),
            Expression.Goto(endif),
            Expression.Label(ifFalse),
            Expression.Assign(r,p_And_p),
            Expression.Label(endif),
            r
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void Conditional内部がジャンプ1(){
        //if(!p&p)goto ifFalse
        //r=p&p
        //ifFalse:
        //r=p|p
        //r
        var r = Expression.Parameter(typeof(bool), "r");
        var p = Expression.Parameter(typeof(bool), "p");
        var p_And_p=Expression.And(p,p);
        var p_Or_p=Expression.Or(p,p);
        var ifFalse=Expression.Label("ifFalse");
        var endif=Expression.Label("endif");
        var Block=Expression.Block(
            new[]{r},
            Expression.IfThen(
                Expression.Not(p_And_p),
                Expression.Goto(ifFalse)
            ),
            Expression.Assign(r,p_And_p),
            Expression.Goto(endif),
            Expression.Label(ifFalse),
            Expression.Assign(r,p_Or_p),
            Expression.Label(endif),
            r
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void Conditional内部がジャンプ2(){
        //└┬┐　　　　　　　0,最上位,子 
        //┌┘│　　　　　　　1,goto ifFalse,親 (辺番号0 最上位, 辺番号1 goto ifFalse)
        //└┬┼┐　　　　　　1,goto ifFalse,子 
        //┌┼┘│　　　　　　3,default(Void),親 (辺番号0 最上位, 辺番号3 default(Void))
        //└┼┐│　　　　　　3,default(Void),子 
        //┌┼┴┘　　　　　　4,IIF(Not((局所0 = (p And p))), goto ifFalse, default(Void)),親 (辺番号3 default(Void), 辺番号4 IIF(Not((局所0 = (p And p))), goto ifFalse, default(Void)))
        //└┼┐　　　　　　　4,IIF(Not((局所0 = (p And p))), goto ifFalse, default(Void)),子 
        //┌┘│　　　　　　　2,ifFalse,親 (辺番号1 goto ifFalse, 辺番号2 ifFalse)
        //┌─┘　　　　　　　5,endif,親 (辺番号4 IIF(Not((局所0 = (p And p))), goto ifFalse, default(Void)), 辺番号5 endif)

        //if(!p&p)goto ifFalse
        //r=p&p
        //ifFalse:
        //r=p&p
        //r
        //↓
        //if(!(t=p&p))goto ifFalse
        //r=t
        //ifFalse:
        //r=t
        //r
        var r = Expression.Parameter(typeof(bool), "r");
        var p = Expression.Parameter(typeof(bool), "p");
        var p_And_p=Expression.And(p,p);
        var ifFalse=Expression.Label("ifFalse");
        var endif=Expression.Label("endif");
        var Block=Expression.Block(
            new[]{r},
            Expression.IfThen(
                Expression.Not(p_And_p),
                Expression.Goto(ifFalse)
            ),
            Expression.Assign(r,p_And_p),
            Expression.Goto(endif),
            Expression.Label(ifFalse),
            Expression.Assign(r,p_And_p),
            Expression.Label(endif),
            r
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void Loop無限(){
        var p = Expression.Parameter(typeof(bool), "p");
        var Block=Expression.Block(
            Expression.Loop(
                p
            ),
            p
        );
        var 最適化Lambda=this.Optimizer.Lambda最適化(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void Loop2ステートメント無限(){
        //└┐　　　　　　　　0,最上位,子 
        //┌┴┐　　　　　　　1,Begin Loop,親 (辺番号1 , 辺番号1 )
        //└┬┘　　　　　　　1,,子 
        //┌┘　　　　　　　　2,End Loop,親 (辺番号1 , 辺番号2 )
        //.Block() {
        //    .Loop  {
        //        .Block() {
        //            $局所0 = $p & $p;
        //            $局所0
        //        }
        //    };
        //    $p
        //}
        var p = Expression.Parameter(typeof(bool), "p");
        var pp=Expression.And(p,p);
        var Block=Expression.Block(
            Expression.Loop(
                Expression.Block(
                    pp,
                    pp
                )
            ),
            p
        );
        var 最適化Lambda=this.Optimizer.Lambda最適化(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void Loop局所先行評価(){
        //└┐　　　　　　　　0,最上位,子 
        //┌┴┐　　　　　　　1,Begin Loop,親 (辺番号1 , 辺番号1 )
        //└┬┘　　　　　　　1,,子 
        //┌┘　　　　　　　　2,End Loop,親 (辺番号1 , 辺番号2 )
        //.Block() {
        //    .Loop  {
        //        .Block() {
        //            $局所0 = $p & $p;
        //                $局所0
        //        }
        //    };
        //    $局所0
        //}
        var p = Expression.Parameter(typeof(decimal), "p");
        var pp=Expression.Add(p,p);
        var Block=Expression.Block(
            Expression.Loop(
                Expression.Block(
                    Expression.Assign(
                        p,pp
                    ),
                    pp
                )
            ),
            pp
        );
        this.Optimizer.Lambda最適化(
            Expression.Lambda<Func<decimal,decimal>>(
                Block,
                p
            )
        );
    }
    [Fact]public void LoopBreak(){
        //└┐　　　　　　　　0,最上位,子 
        //┌┴┐　　　　　　　1,Begin Loop,親 (辺番号1 , 辺番号1 )
        //└┬┘　　　　　　　1,,子 
        //┌┘　　　　　　　　2,End Loop,親 (辺番号1 , 辺番号2 )
        //.Block() {
        //    .Loop  {
        //        .Block() {
        //            $局所0 = $p & $p;
        //            $局所0
        //            break
        //        }
        //    };
        //    $局所0
        //}
        var p = Expression.Parameter(typeof(decimal), "p");
        var pp=Expression.Add(p,p);
        var Break=Expression.Label("Break");
        var Block=Expression.Block(
            Expression.Loop(
                Expression.Block(
                    Expression.Assign(
                        p,pp
                    ),
                    pp,
                    Expression.Break(Break)
                ),
                Break
            ),
            pp
        );
        var 最適化Lambda=this.Optimizer.Lambda最適化(
            Expression.Lambda<Func<decimal,decimal>>(
                Block,
                p
            )
        );
    }
    [Fact]public void LoopBreakContinue0(){
        var p = Expression.Parameter(typeof(decimal), "p");
        var pp=Expression.Add(p,p);
        var Continue=Expression.Label("Continue");
        var Break=Expression.Label("Break");
        var Block=Expression.Block(
            Expression.Loop(
                Expression.Block(
                    Expression.Assign(
                        p,pp
                    ),
                    pp,
                    Expression.Break(Break),
                    Expression.Continue(Continue)
                ),
                Break,
                Continue
            ),
            pp
        );
        this.Optimizer.Lambda最適化(Expression.Lambda<Func<decimal,decimal>>(Block,p));
    }
    [Fact]public void LoopContinueBreak(){
        var p = Expression.Parameter(typeof(bool), "p");
        var Continue=Expression.Label("Continue");
        var Break=Expression.Label(typeof(bool),"Break");
        var Block=Expression.Loop(
            Expression.Block(
                p,
                Expression.IfThenElse(
                    p,
                    Expression.Continue(Continue),
                    Expression.Break(Break,Expression.Constant(true))
                )
            ),
            Break,
            Continue
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<bool,bool>>(Block,p));
    }
    [Fact]public void LoopBreakContinue1(){
        var p = Expression.Parameter(typeof(bool), "p");
        var Continue=Expression.Label("Continue");
        var Break=Expression.Label(typeof(bool),"Break");
        var Block=Expression.Loop(
            Expression.Block(
                p,
                Expression.IfThenElse(
                    Expression.Not(p),
                    Expression.Break(Break,Expression.Constant(true)),
                    Expression.Continue(Continue)
                )
            ),
            Break,
            Continue
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<bool,bool>>(Block,p));
    }
    public class Tuple2<T>{
        public T Item1=>default!;
        public T Item2=>default!;
        public static Tuple2<T> Create()=>new();
    }
    public struct ValueTuple2<T>{
        public T Item1=>default!;
        public T Item2=>default!;
        public static Tuple2<T> Create()=>new();
    }
    [Fact]public void 値型と参照型による先行評価の使い分け0(){
        var l0=this.Optimizer.Lambda最適化(() =>
            Tuple2<string>.Create().Item1+Tuple2<string>.Create().Item1
        );
        var l1=this.Optimizer.Lambda最適化(() =>
            ValueTuple2<string>.Create().Item1+ValueTuple2<string>.Create().Item1
        );
        var l2=this.Optimizer.Lambda最適化(() =>
            Tuple2<int>.Create().Item1+Tuple2<int>.Create().Item1
        );
        var l3=this.Optimizer.Lambda最適化(() =>
            ValueTuple2<int>.Create().Item1+ValueTuple2<string>.Create().Item1
        );
    }
    [Fact]public void 値型と参照型による先行評価の使い分け1(){
        var l0=this.Optimizer.Lambda最適化(() =>
            Tuple2<string>.Create().Item1+Tuple2<string>.Create().Item2
        );
        var l1=this.Optimizer.Lambda最適化(() =>
            ValueTuple2<string>.Create().Item1+ValueTuple2<string>.Create().Item2
        );
        var l2=this.Optimizer.Lambda最適化(() =>
            Tuple2<int>.Create().Item1+Tuple2<int>.Create().Item2
        );
        var l3=this.Optimizer.Lambda最適化(() =>
            ValueTuple2<int>.Create().Item1+ValueTuple2<string>.Create().Item2
        );
    }
    [Fact]public void 値型と参照型による先行評価の使い分け2(){
        var l0=this.Optimizer.Lambda最適化(() =>
            Tuple.Create(op).Let(
                p=>
                    p.Item1&p.Item1
            )
        );
        var l1=this.Optimizer.Lambda最適化(() =>
            ValueTuple.Create(op).Let(
                p=>
                    p.Item1&p.Item1
            )
        );
        var l2=this.Optimizer.Lambda最適化(() =>
            Tuple.Create(true).Let(
                p=>
                    p.Item1&p.Item1
            )
        );
        var l3=this.Optimizer.Lambda最適化(() =>
            ValueTuple.Create(true).Let(
                p=>
                    p.Item1&p.Item1
            )
        );
    }
    private static operator_true op=new(true);
    [Fact]
    public void AndAlso2(){
        this.Expression実行AssertEqual(() =>
            Tuple.Create(op,op).Let(
                p=>
                    p.Item1&&p.Item2
            )
        );
    }
    [Fact]
    public void AndAlso21(){
        var l1=this.Optimizer.Lambda最適化(() =>
            ValueTuple.Create(op,op).Let(
                p=>
                    p.Item1&&p.Item2
            )
        );
        var l2=this.Optimizer.Lambda最適化(() =>
            Tuple.Create(true,true).Let(
                p=>
                    p.Item1&&p.Item2
            )
        );
        var l3=this.Optimizer.Lambda最適化(() =>
            ValueTuple.Create(true,true).Let(
                p=>
                    p.Item1&&p.Item2
            )
        );
    }
    [Fact]
    public void AndAlso3(){
        this.Optimizer.Lambda最適化(()=>
            Tuple.Create(op,op,op).Let(
                p=>
                    p.Item1&&p.Item2&&p.Item3
            )
        );
    }
    [Fact]
    public void AndAlso4(){
        this.Optimizer.Lambda最適化(()=>
            Tuple.Create(op,op,op,op).Let(
                p=>
                    p.Item1&&p.Item2&&p.Item3&&p.Item4
            )
        );
    }
    [Fact]
    public void AndAlso5(){
        this.Optimizer.Lambda最適化(()=>
            Tuple.Create(op,op,op,op,op).Let(
                p=>
                    p.Item1&&p.Item2&&p.Item3&&p.Item4&&p.Item5
            )
        );
    }
    [Fact]
    public void AndAlso6(){
        this.Optimizer.Lambda最適化(()=>
            Tuple.Create(op,op,op,op,op,op).Let(
                p=>
                    p.Item1&&p.Item2&&p.Item3&&p.Item4&&p.Item5&&p.Item6
            )
        );
    }
    [Fact]
    public void AndAlso7(){
        this.Optimizer.Lambda最適化(()=>
            Tuple.Create(op,op,op,op,op,op,op).Let(
                //new ValueTuple<bool,bool,bool,bool,bool,bool,bool,ValueTuple<bool>>(true,true,true,true,true,true,true,new ValueTuple<bool>(true)).Let(
                p=>
                    p.Item1&&p.Item2&&p.Item3&&p.Item4&&p.Item5&&p.Item6&&p.Item7
            )
        );
    }
    [Fact]
    public void AndAlsoRest1(){
        this.Optimizer.Lambda最適化(()=>
            new Tuple<operator_true,operator_true,operator_true,operator_true,operator_true,operator_true,operator_true,Tuple<operator_true>>(op,op,op,op,op,op,op,new Tuple<operator_true>(op)).Let(
                p=>
                    p.Item1&&p.Item2&&p.Item3&&p.Item4&&p.Item5&&p.Item6&&p.Item7&&p.Rest.Item1
            )
        );
    }
    [Fact]
    public void AndAlsoRest7(){
        var l0=this.Optimizer.Lambda最適化(()=>
            new Tuple<operator_true,operator_true,operator_true,operator_true,operator_true,operator_true,operator_true,Tuple<operator_true,operator_true,operator_true,operator_true,operator_true,operator_true,operator_true>>(op,op,op,op,op,op,op,new Tuple<operator_true,operator_true,operator_true,operator_true,operator_true,operator_true,operator_true>(op,op,op,op,op,op,op)).Let(
                //new ValueTuple<bool,bool,bool,bool,bool,bool,bool,ValueTuple<bool>>(true,true,true,true,true,true,true,new ValueTuple<bool>(true)).Let(
                p=>
                    p.Item1&&p.Item2&&p.Item3&&p.Item4&&p.Item5&&p.Item6&&p.Item7&&p.Rest.Item1
                    &&p.Rest.Item2&&p.Rest.Item3&&p.Rest.Item4&&p.Rest.Item5&&p.Rest.Item6&&p.Rest.Item7
            )
        );
        var l1=this.Optimizer.Lambda最適化(()=>
            new ValueTuple<bool,bool,bool,bool,bool,bool,bool,ValueTuple<bool,bool,bool,bool,bool,bool,bool>>(true,true,true,true,true,true,true,new ValueTuple<bool,bool,bool,bool,bool,bool,bool>(true,true,true,true,true,true,true)).Let(
                p=>
                    p.Item1&&p.Item2&&p.Item3&&p.Item4&&p.Item5&&p.Item6&&p.Item7&&p.Rest.Item1
                    &&p.Rest.Item2&&p.Rest.Item3&&p.Rest.Item4&&p.Rest.Item5&&p.Rest.Item6&&p.Rest.Item7
            )
        );
    }
    [Fact]
    public void OrElse2(){
        this.Optimizer.Lambda最適化(() =>
            Tuple.Create(op,op).Let(
                p=>
                    p.Item1||p.Item2
            )
        );
    }
    [Fact]
    public void OrElse3(){
        this.Optimizer.Lambda最適化(()=>
            Tuple.Create(op,op,op).Let(
                p=>
                    p.Item1||p.Item2||p.Item3
            )
        );
    }
    [Fact]
    public void OrElse4(){
        this.Optimizer.Lambda最適化(()=>
            Tuple.Create(op,op,op,op).Let(
                p=>
                    p.Item1||p.Item2||p.Item3||p.Item4
            )
        );
    }
    [Fact]
    public void OrElse5(){
        this.Optimizer.Lambda最適化(()=>
            Tuple.Create(op,op,op,op,op).Let(
                p=>
                    p.Item1||p.Item2||p.Item3||p.Item4||p.Item5
            )
        );
    }
    [Fact]
    public void OrElse6(){
        this.Optimizer.Lambda最適化(()=>
            Tuple.Create(op,op,op,op,op,op).Let(
                p=>
                    p.Item1||p.Item2||p.Item3||p.Item4||p.Item5||p.Item6
            )
        );
    }
    [Fact]
    public void OrElse7(){
        this.Optimizer.Lambda最適化(()=>
            Tuple.Create(op,op,op,op,op,op,op).Let(
                //new ValueTuple<bool,bool,bool,bool,bool,bool,bool,ValueTuple<bool>>(true,true,true,true,true,true,true,new ValueTuple<bool>(true)).Let(
                p=>
                    p.Item1||p.Item2||p.Item3||p.Item4||p.Item5||p.Item6||p.Item7
            )
        );
    }
    [Fact]
    public void OrElseRest1(){
        this.Optimizer.Lambda最適化(()=>
            new Tuple<operator_true,operator_true,operator_true,operator_true,operator_true,operator_true,operator_true,Tuple<operator_true>>(op,op,op,op,op,op,op,new Tuple<operator_true>(op)).Let(
                p=>
                    p.Item1||p.Item2||p.Item3||p.Item4||p.Item5||p.Item6||p.Item7||p.Rest.Item1
            )
        );
    }
    [Fact]
    public void OrElseRest7(){
        this.Optimizer.Lambda最適化(()=>
            new Tuple<operator_true,operator_true,operator_true,operator_true,operator_true,operator_true,operator_true,Tuple<operator_true,operator_true,operator_true,operator_true,operator_true,operator_true,operator_true>>(op,op,op,op,op,op,op,new Tuple<operator_true,operator_true,operator_true,operator_true,operator_true,operator_true,operator_true>(op,op,op,op,op,op,op)).Let(
                //new ValueTuple<bool,bool,bool,bool,bool,bool,bool,ValueTuple<bool>>(true,true,true,true,true,true,true,new ValueTuple<bool>(true)).Let(
                p=>
                    p.Item1||p.Item2||p.Item3||p.Item4||p.Item5||p.Item6||p.Item7||p.Rest.Item1
                    ||p.Rest.Item2||p.Rest.Item3||p.Rest.Item4||p.Rest.Item5||p.Rest.Item6||p.Rest.Item7
            )
        );
        this.Optimizer.Lambda最適化(()=>
            new ValueTuple<bool,bool,bool,bool,bool,bool,bool,ValueTuple<bool,bool,bool,bool,bool,bool,bool>>(true,true,true,true,true,true,true,new ValueTuple<bool,bool,bool,bool,bool,bool,bool>(true,true,true,true,true,true,true)).Let(
                p=>
                    p.Item1||p.Item2||p.Item3||p.Item4||p.Item5||p.Item6||p.Item7||p.Rest.Item1
                    ||p.Rest.Item2||p.Rest.Item3||p.Rest.Item4||p.Rest.Item5||p.Rest.Item6||p.Rest.Item7
            )
        );
    }
    [Fact]public void Switch_Default(){
        var p = Expression.Parameter(typeof(int), "p");
        var e=Expression.Switch(p,p);
        var 最適化Lambda=this.Optimizer.Lambda最適化(
            Expression.Lambda<Func<int,int>>(
                e,
                p
            )
        );
    }
    [Fact]public void Switch_Case経路1N(){
        var p = Expression.Parameter(typeof(int), "p");
        var pp=Expression.And(p,p);
        var SwitchCases=new List<SwitchCase>();
        var TestValues=new List<Expression>();
        var r=new Random(1);
        var case定数=0;
        for(var a=0;a<10;a++){
            for(var b=r.Next(1,10);b>=0;b--) TestValues.Add(Expression.Constant(case定数++));
            var SwitchCase=Expression.SwitchCase(pp,TestValues);
            SwitchCases.Add(SwitchCase);
            TestValues.Clear();
        }
        var e=Expression.Switch(pp,pp,SwitchCases.ToArray());
        var 最適化Lambda=this.Optimizer.Lambda最適化(
            Expression.Lambda<Func<int,int>>(
                e,
                p
            )
        );
    }
    public static void Save(LambdaExpression Lambda){
        var Name=Lambda.Name;
        var AssemblyName = new AssemblyName { Name=Name};
        var DynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(AssemblyName,AssemblyBuilderAccess.RunAndCollect);
        var ModuleBuilder = DynamicAssembly.DefineDynamicModule(Name);
        var TypeBuilder = ModuleBuilder.DefineType(Name,TypeAttributes.Public);
        var MethodBuilder = TypeBuilder.DefineMethod(Name,MethodAttributes.Public,Lambda.ReturnType,Lambda.Parameters.Select(p=>p.Type).ToArray());
        MethodBuilder.InitLocals=false;
        var I=MethodBuilder.GetILGenerator();
        var _ = TypeBuilder.CreateType();
        new Lokad.ILPack.AssemblyGenerator().GenerateAssembly(DynamicAssembly,@$"{Environment.CurrentDirectory}\{Name}.dll");
    }
    [Fact]
    public void Switch_Case経路N1(){
        var p=Expression.Parameter(typeof(int),"p");
        var pp=Expression.And(p,p);
        var SwitchCases=new List<SwitchCase>();
        var TestValues=new List<Expression>();
        var r=new Random(1);
        var case定数=0;
        for(var a=0;a<10;a++){
            for(var b=r.Next(1,10);b>=0;b--) TestValues.Add(Expression.Constant(case定数++));
            TestValues.Add(pp);
            var SwitchCase=Expression.SwitchCase(pp,TestValues);
            SwitchCases.Add(SwitchCase);
            TestValues.Clear();
        }
        var e=Expression.Block(
            Expression.Switch(p,pp,SwitchCases.ToArray()),
            pp
        );
        var 最適化Lambda=this.Optimizer.Lambda最適化(
            Expression.Lambda<Func<int,int>>(
                e,
                p
            )
        );
    }
    [Fact]public void Conditional内部にAndAlso(){
        var p = Expression.Parameter(typeof(operator_true), "p");
        var pp=Expression.And(p,p);
        var AndAlso=Expression.AndAlso(
            p,Expression.And(p,p));
        var Block=Expression.Block(
            Expression.Block(
                Expression.Condition(
                    Expression.Call(
                        AndAlso.Type.GetMethod("op_True")!,
                        AndAlso
                    ),
                    p,
                    pp
                )
            )
        );
        var Lambda=this.Optimizer.Lambda最適化(
            Expression.Lambda<Func<operator_true,operator_true>>(
                Block,
                p
            )
        );
    }
}
