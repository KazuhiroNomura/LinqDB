﻿using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection;

using LinqDB.Sets;

using MemoryPack;

using TestLinqDB.Optimizers;

using static TestLinqDB.特殊パターン.変換_局所Parameterの先行評価;
//using System.Reflection;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable All
namespace TestLinqDB.特殊パターン;
public class 変換_局所Parameterの先行評価 : 共通{
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
    [Fact]public void Condition0(){
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
    [Fact]public void Condition1(){
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
    [Fact]public void _2分岐0(){
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
    [Fact]public void _2分岐1(){
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
    [Fact]public void 経路1つから経路2つに分岐0(){
        //p&p 　　　　0
        //├────┐1 br_false 2 ifFalse
        //p&p 　　　│
        //└───┐│  goto 3 endif
        //┌───┼┘2 ifFalse:
        //p&p 　　│
        //├───┘　3 endif:
        //if(p&p)
        //    p&p
        //else
        //    p&p
        //p&p
        //↓
        //if(t=p&p)
        //    t
        //else
        //    t
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
    [Fact]public void 経路1つから経路2つに分岐1(){
        //p&p 　　　　0
        //p 　　　　　
        //├────┐1 br_false 2 ifFalse
        //p&p 　　　│
        //└───┐│  goto 3 endif
        //┌───┼┘2 ifFalse:
        //p&p 　　│
        //├───┘　3 endif:
        //p&p
        //if(p)
        //    p&p
        //else
        //    p&p
        //↓
        //t=p&p
        //if(p)
        //    t
        //else
        //    t
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
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void 経路1つから経路2つに分岐2(){
        //p&p 　　　　0
        //├────┐1 br_false 2 ifFalse
        //r=p&p 　　│
        //└───┐│  goto 3 endif
        //┌───┼┘2 ifFalse:
        //r=p&p 　│
        //├───┘　3 endif:

        //if(p&p)goto ifFalse
        //r=p&p
        //ifFalse:
        //r=p&p
        //r
        //↓
        //if((t=p&p))goto ifFalse
        //r=t
        //ifFalse:
        //r=t
        //r
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
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void 経路1つから経路2つに分岐3(){
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
    [Fact]public void 経路2つから経路1つに合流(){
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
    [Fact]public void 経路1つから経路2つから経路1つ0(){
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
    [Fact]public void 経路1つから経路2つから経路1つ1(){
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
    [Fact]public void 経路1つから経路2つから経路1つ2(){
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
    [Fact]public void 経路1つから経路2つから経路1つ3(){
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
    [Fact]public void 経路1つから経路2つから経路1つ4(){
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
    [Fact]public void 無条件ジャンプ1下上下0(){
        //            物理番号
        //              論隷番号
        //└──┐    0 0 goto L0
        //┌─┐│    2 0 L1:
        //1m  ││    
        //└┐││        goto L2
        //┌┼┼┘    1 0 L0:
        //1m││  
        //└┼┘          goto L1
        //┌┘        3 0 L2:
        //↓
        //└────┐0 0 goto L0
        //┌───┐│2 0 L1:
        //t0      ││
        //└──┐││    goto L2
        //┌──┼┼┘1 L0:
        //t0=1m ││  
        //└──┼┘      goto L1
        //┌──┘    3 L2:
        var L0=Expression.Label("L0");
        var L1=Expression.Label("L1");
        var L2=Expression.Label("L2");
        var Block=Expression.Block(
            Expression.Goto(L0),
            Expression.Label(L1),
            Expression.Constant(1m),
            Expression.Goto(L2),
            Expression.Label(L0),
            Expression.Constant(2m),
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
    [Fact]public void 無条件ジャンプ1下上下1(){
        //            物理番号
        //              論隷番号
        //└──┐    0 0 goto L0
        //┌─┐│    2 0 L1:
        //1m  ││    
        //└┐││        goto L2
        //┌┼┼┘    1 0 L0:
        //1m││  
        //└┼┘          goto L1
        //┌┘        3 0 L2:
        //↓
        //└────┐0 0 goto L0
        //┌───┐│2 0 L1:
        //t0      ││
        //└──┐││    goto L2
        //┌──┼┼┘1 L0:
        //t0=1m ││  
        //└──┼┘      goto L1
        //┌──┘    3 L2:
        //.Goto L0 { };    0   [1,[0]]
        //.Label
        //.LabelTarget L1:;2   [2,[1]]
        //1M;
        //.Goto L2 { };    2 3 [3,[2,[1,[0]]]]
        //.Label
        //.LabelTarget L0:;1   [1,[0]]
        //1M;
        //.Goto L1 { };    1 2 [2,[1]]
        //.Label
        //.LabelTarget L2:;3   [3,[2,[1,[0]]]]
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
        //└──┐0 br L0
        //┌─┐│1 L1:
        //1m  ││
        //└┐││  br L2
        //┌┼┼┘2 L0:
        //1m││  
        //└┼┘    br L1
        //┌┼┐  3 L3:
        //3m││
        //└┼┼┐  br L4
        //┌┘││4 L2:
        //2m  ││
        //└─┘│  br L3
        //┌──┘5 L4:
        //2m
        //↓
        //└────┐ 0 br L0
        //┌───┐│1 L1:
        //t0      ││
        //└──┐││  br L2
        //┌──┼┼┘2 L0:
        //t0=1m ││  
        //└──┼┘    br L1
        //┌──┼┐  3 L3:
        //3m    ││
        //└──┼┼┐  br L4
        //┌──┘││4 L2:
        //t1=2m   ││
        //└───┘│  br L3
        //┌────┘5 L4:
        //t1
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
    [Fact]public void Conditional内部がジャンプ0(){
        //p&p 　　　　0
        //├────┐1 br_false 2 ifFalse
        //r=p|p 　　│
        //└───┐│  goto 3 endif
        //┌───┼┘2 ifFalse:
        //r=p&p 　│
        //├───┘　3 endif:

        //if(p&p)goto ifFalse
        //r=p|p
        //ifFalse:
        //r=p&p
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
    [Fact]public void goto経路2つから経路1つに合流(){
        //│　　p&p
        //├─┐br_false ifFalse
        //│　│void
        //│　│p&p
        //└┐│goto endif
        //┌┼┘ifFalse:
        //││　p&p
        //├┘　endif:
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
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void Loop0(){
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
    [Fact]public void Loop1(){
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
    [Fact]public void Loop2(){
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
        var p = Expression.Parameter(typeof(bool), "p");
        var pp=Expression.And(p,p);
        var Block=Expression.Block(
            Expression.Loop(
                Expression.Block(
                    pp,
                    pp
                )
            ),
            pp
        );
        var 最適化Lambda=this.Optimizer.Lambda最適化(
            Expression.Lambda<Func<bool,bool>>(
                Block,
                p
            )
        );
    }
    [Fact]public void Loop3(){
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
        var q = Expression.Parameter(typeof(decimal), "q");
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
        var 最適化Lambda=this.Optimizer.Lambda最適化(
            Expression.Lambda<Func<decimal,decimal>>(
                Block,
                p
            )
        );
    }
    public class operator_true{
        private bool 内部のBoolean;
        public operator_true(bool 内部のBoolean){
            this.内部のBoolean=内部のBoolean;
        }
        public static bool operator false(operator_true a)=>!a.内部のBoolean;
        public static bool operator true(operator_true a)=>a.内部のBoolean;
        public static operator_true operator&(operator_true a,operator_true b)=>new(a.内部のBoolean&b.内部のBoolean);
        public static operator_true operator|(operator_true a,operator_true b)=>new(a.内部のBoolean|b.内部のBoolean);
    }
    private static operator_true op=new(true);
    [Fact]
    public void AndAlso2(){
        this.Optimizer.Lambda最適化(() =>
            Tuple.Create(op,op).Let(
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
        this.Optimizer.Lambda最適化(()=>
            new Tuple<operator_true,operator_true,operator_true,operator_true,operator_true,operator_true,operator_true,Tuple<operator_true,operator_true,operator_true,operator_true,operator_true,operator_true,operator_true>>(op,op,op,op,op,op,op,new Tuple<operator_true,operator_true,operator_true,operator_true,operator_true,operator_true,operator_true>(op,op,op,op,op,op,op)).Let(
                //new ValueTuple<bool,bool,bool,bool,bool,bool,bool,ValueTuple<bool>>(true,true,true,true,true,true,true,new ValueTuple<bool>(true)).Let(
                p=>
                    p.Item1&&p.Item2&&p.Item3&&p.Item4&&p.Item5&&p.Item6&&p.Item7&&p.Rest.Item1
                    &&p.Rest.Item2&&p.Rest.Item3&&p.Rest.Item4&&p.Rest.Item5&&p.Rest.Item6&&p.Rest.Item7
            )
        );
        this.Optimizer.Lambda最適化(()=>
            new ValueTuple<bool,bool,bool,bool,bool,bool,bool,ValueTuple<bool,bool,bool,bool,bool,bool,bool>>(true,true,true,true,true,true,true,new ValueTuple<bool,bool,bool,bool,bool,bool,bool>(true,true,true,true,true,true,true)).Let(
                p=>
                    p.Item1&&p.Item2&&p.Item3&&p.Item4&&p.Item5&&p.Item6&&p.Item7&&p.Rest.Item1
                    &&p.Rest.Item2&&p.Rest.Item3&&p.Rest.Item4&&p.Rest.Item5&&p.Rest.Item6&&p.Rest.Item7
            )
        );
    }
    [Fact]
    public void AndAlso50(){
        this.Optimizer.Lambda最適化(()=>
            new ValueTuple<int,int,int,int,int,int,int,ValueTuple<int,int,int,int,int,int,int>>(1,2,3,4,5,6,7,new ValueTuple<int,int,int,int,int,int,int>(81,82,83,84,85,86,87)).Let(
                p=>
                    p.Item1==0&&
                    //p.Item2==0&&
                    //p.Item3==0&&
                    p.Item4==0&&
                    p.Item5==0&&
                    p.Item6==0&&
                    p.Item7==0&&
                    p.Rest.Item1==0&&
                    p.Rest.Item2==0&&
                    p.Rest.Item3==0&&
                    p.Rest.Item4==0&&
                    p.Rest.Item5==0&&
                    p.Rest.Item6==0&&
                    p.Rest.Item7==0
            )
        );
    }
    [Fact]
    public void AndAlso60(){
        this.Optimizer.Lambda最適化(()=>
            new ValueTuple<int,int,int,int,int,int,int,ValueTuple<int,int,int,int,int,int,int>>(1,2,3,4,5,6,7,new ValueTuple<int,int,int,int,int,int,int>(81,82,83,84,85,86,87)).Let(
                p=>
                    p.Item1==0&&
                    p.Item2==0&&
                    p.Item3==0&&
                    p.Item4==0&&
                    p.Item5==0&&
                    p.Item6==0&&
                    p.Item7==0&&
                    p.Rest.Item1==0&&
                    p.Rest.Item2==0&&
                    p.Rest.Item3==0&&
                    p.Rest.Item4==0&&
                    p.Rest.Item5==0&&
                    p.Rest.Item6==0&&
                    p.Rest.Item7==0
            )
        );
    }
}
