using System.Linq.Expressions;
using LinqDB.Sets;
using static LinqDB.Optimizers.Optimizer;
using TestLinqDB.Serializers.Formatters.Sets;
using TestLinqDB.Sets;
using テスト.PrimaryKeys.dbo;
using Expression = System.Linq.Expressions.Expression;
using ExtensionSet=LinqDB.Sets.ExtensionSet;
using Key=TestLinqDB.Keys.Key;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable All
namespace TestLinqDB.Optimizers;
public class 変換_跨ぎParameterの先行評価:共通{
    struct OI{
        public readonly int o,i;
        public OI(int o,int i){
            this.o=o;
            this.i=i;
        }
    }
    class SetT:Set<OI>{

    }
    [Fact]public void 変換_先行評価式_Traverse(){
        var a = Expression.Parameter(typeof(int), "a");
        var s = new Set<int>();
        //    if(this.現在探索場所==this.希望探索場所&&this.ExpressionEqualityComparer.Equals(Expression0,this.旧Expression!)) {
        //        if(this.書き込み項か)this.書き戻しがあるか=true;
        this.共通コンパイル実行(
            Expression.Lambda<Action>(
                Expression.Lambda<Func<int,int>>(
                    Expression.Block(
                        Expression.Lambda<Func<int>>(
                            Expression.Assign(a,Expression.Constant(0))
                        ),
                        a
                    ),
                    a
                )
            )
        );
        //        else                 this.読み込みがあるか=true;
        this.共通コンパイル実行(
            Expression.Lambda<Action>(
                Expression.Lambda<Func<int,int>>(
                    Expression.Block(
                        Expression.Lambda<Func<int>>(
                            a
                        ),
                        a
                    ),
                    a
                )
            )
        );
        //    }
        //    switch(Expression0.NodeType) {
        //        case ExpressionType.Block: {
        this.共通コンパイル実行(
            Expression.Lambda<Action>(
                Expression.Block(
                    Expression.Constant(0),
                    Expression.Constant(0)
                )
            )
        );
        //        }
        //        case ExpressionType.Lambda: {
        this.共通コンパイル実行(
            Expression.Lambda<Action>(
                Expression.Lambda<Func<int,int>>(
                    Expression.Block(
                        Expression.Lambda<Func<int>>(
                            a
                        ),
                        a
                    ),
                    a
                )
            )
        );
        //        }
        //        case ExpressionType.Call: {
        //            if(Reflection.Helpers.NoLoopUnrolling==MethodCall_GenericMethodDefinition)
        this.共通コンパイル実行(()=>new{
            a=s.Select(p=>s).NoLoopUnrolling(),
            b=s.Select(p=>s)
        });
        //            if(this.ループ跨ぎを使うか&&ループ展開可能メソッドか(MethodCall0)) {
        //                if(Reflection.ExtensionSet.Inline1==MethodCall_GenericMethodDefinition) {
        //                    if(MethodCall0_Arguments_0 is LambdaExpression Lambda0) {
        this.共通コンパイル実行(()=>"".Let(p=>ExtensionSet.Inline(()=>1m)));
        //                    } else {
        this.共通コンパイル実行(
            ()=>
                "".Let(p=>
                    ExtensionSet.Inline((Func<decimal>)(()=>1m))+ExtensionSet.Inline((Func<decimal>)(()=>1m))
                )
        );
        //this.共通コンパイル実行(()=>ExtensionSet.Inline((Func<string>)(()=>"")));
        //                    }
        //                } else if(Reflection.ExtensionSet.Inline2==MethodCall_GenericMethodDefinition) {
        //                    if(MethodCall0_Arguments_1 is LambdaExpression Lambda0) {
        this.共通コンパイル実行(()=>"".Inline(s=>s+"x"));
        //                    } else {
        this.共通コンパイル実行(()=>"".Inline((Func<string,string>)(s=>s+"x")));
        //                    }
        //                } else {
        //                    for(var a = 1;a<MethodCall0_Arguments_Count;a++) {
        //                        if(MethodCall0_Arguments_a is LambdaExpression Lambda0) {
        this.共通コンパイル実行(()=>s.Select(p=>p+p));
        //                        } else {
        this.共通コンパイル実行(()=>s.Select((Func<int,int>)(p=>p+p)));
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }
    [Fact]public void 取得_先行評価式_Call(){
        var s = new Set<int>();
        //if(Reflection.Helpers.NoLoopUnrolling==MethodCall_GenericMethodDefinition)
        this.共通コンパイル実行(()=>ExtensionSet.Inline(()=>"").NoLoopUnrolling());
        //if(this.IsInline&&ループ展開可能メソッドか(MethodCall_GenericMethodDefinition)) {
        //    switch(MethodCall_GenericMethodDefinition.Name) {
        //        case nameof(ExtensionSet.Inline): {
        //            if(MethodCall0_Arguments.Count==1) {
        this.共通コンパイル実行(()=>ExtensionSet.Inline(()=>""));
        //            }else{
        this.共通コンパイル実行(()=>"".Inline(a=>""));
        //            }
        //        }
        //        case nameof(ExtensionSet.Except): {
        //            if(this.結果Expression is not null)
        this.共通コンパイル実行(()=>"".Let(b=>s.Select(a=>a+a)).Except(s.Select(a=>a+a)));
        //            if(this.結果Expression is not null)
        this.共通コンパイル実行(()=>s.Select(a=>a+a).Except(s.Select(a=>a+a)));
        //            if(MethodCall.Arguments.Count==3)
        this.共通コンパイル実行(()=>s.Except(s,EqualityComparer<int>.Default));
        //            break;
        //        }
        //        default: {
        //            if(this.結果Expression is not null)
        this.共通コンパイル実行(()=>"".Let(b=>s.Select(a=>a+a)).Select(a=>a+a));
        //            for(var a = 1;a<MethodCall0_Arguments_Count;a++)
        //                if(巻き上げ処理(MethodCall0_Arguments[a])) 
        this.共通コンパイル実行(()=>s.Select(a=>1m));
        this.共通コンパイル実行(()=>s.Select(a=>a+1));
        //        }
        //    }
        //} else {
        //}
        //bool 巻き上げ処理(Expression Expression0) {
        //    if(Expression0 is LambdaExpression Lambda0) {
        //    }
        //    return false;
        //}
    }
    [Fact]public void 変換_先行評価式_MakeAssign(){
        //if(Binary0_Right==Binary1_Right&&Binary0_Left==Binary1_Left&&Binary0_Conversion==Binary1_Conversion)
        var a = Expression.Parameter(typeof(int), "a");
        this.共通コンパイル実行(
            Expression.Lambda<Action>(
                Expression.Block(
                    new[]{a},
                    Expression.AddAssign(a,Expression.Constant(0)),
                    Expression.Lambda<Func<decimal>>(
                        Expression.Add(
                            Expression.Constant(0m),
                            Expression.Constant(0m)
                        )
                    )
                )
            )
        );
        this.共通コンパイル実行(()=>"");
    }
    [Fact]public void Call(){
        var s = new Set<int>();
        //if(!ループ展開可能メソッドか(MethodCall0))
        this.共通コンパイル実行(() => s.Join(s,o => o,i => i,(o,i) =>o+i));
        //if(Reflection.ExtensionSet.Inline1==GetGenericMethodDefinition(MethodCall0.Method)){
        //    if(MethodCall0_Arguments_0 is LambdaExpression Lambda0)
        this.共通コンパイル実行(()=>LinqDB.Sets.ExtensionSet.Inline(()=>""));
        //    else
        this.共通コンパイル実行(()=>LinqDB.Sets.ExtensionSet.Inline((Func<string>)(()=>"")));
        //}else if(Reflection.ExtensionSet.Inline2==GetGenericMethodDefinition(MethodCall0.Method)) {
        //    if(MethodCall0_Arguments_1 is LambdaExpression Lambda0)
        this.共通コンパイル実行(()=>"".Inline(s=>""));
        //    else
        this.共通コンパイル実行(()=>"".Inline((Func<string,string>)(s=>"")));
        //}
        //for(var a = 1;a<MethodCall0_Arguments_Count;a++)
        //    if(MethodCall0_Argument is LambdaExpression Lambda0)
        this.共通コンパイル実行(()=>s.Select(p=>p+1));
        //    else
        this.共通コンパイル実行(()=>s.Select((Func<int,int>)(p=>p+1)));
    }
    [Fact]public void Block(){
        this.共通コンパイル実行(
            Expression.Lambda<Action>(
                Expression.Block(
                    Expression.Constant(0),
                    Expression.Constant(0)
                )
            )
        );
        //if(Block0_Variables.Count==0&&LinkedList.Count==1&&Block0.Type==LinkedList.Last.Value.Type) return LinkedList.Last.Value;
        this.共通コンパイル実行(
            Expression.Lambda<Action>(
                Expression.Block(
                    Expression.Constant(0)
                )
            )
        );
    }
    [Fact]public void 外だし(){
        var a = Expression.Parameter(typeof(int), "Pワーク変数a");
        var b = Expression.Parameter(typeof(int), "Pワーク変数b");
        var c = Expression.Parameter(typeof(int), "Pワーク変数c");
        var s = new Set<int>();
        //do {
        //    Debug.Assert(LinkedListNode!=null);
        //    var LinkedListNode_Value = LinkedListNode.Value;
        //    if(LinkedListNode_Value.NodeType==ExpressionType.Assign) {
        this.共通コンパイル実行(() => s.Join(s,o => o,i => i,(o,i) =>o+i));
        //    }
        this.共通コンパイル実行(() =>"");
        //    if(旧 is null) {
        this.共通コンパイル実行(() =>"");
        //    } else {
        //        if(分離Expressionの場所==場所.ラムダ跨ぎ){
        this.共通コンパイル実行(()=>"a".Let(o=>o.Let(i=>o)));
        //        } else{
        this.共通コンパイル実行(()=>"a".Inline(a=>a.Inline(b=>a.Inline(c=>a))));
        //        }
        //        do {
        this.共通コンパイル実行(() => s.Join(s,o => o,i => i,(o,i) => new { o,i }));
        //        } while(LinkedListNode0 is not null);
        this.共通コンパイル実行(()=>"a".Let(o=>o.Let(i=>o)));
        //        if(読み込みがあるか)LinkedListNode=LinkedList.AddBefore(LinkedListNode,Expression.Assign(新,旧));
        this.共通コンパイル実行(()=>"a".Let(o=>o.Let(i=>o)));
        //        if(書き込みがあるか)LinkedList.AddLast(Expression.Assign(旧,新));
        this.共通コンパイル実行(
            Expression.Lambda<Action>(
                Expression.Lambda<Func<int,int>>(
                    Expression.Block(
                        Expression.Lambda<Func<int>>(
                            Expression.Assign(a,Expression.Constant(0))
                        ),
                        a
                    ),
                    a
                )
            )
        );
        this.共通コンパイル実行(()=>"a".Let(o=>o.Let(i=>o)));
        //    }
        //} while(LinkedListNode is not null);
        //for(var Node = LinkedList.First;Node is not null;Node=Node.Next) Node.Value=this.Traverse(Node.Value);
        this.共通コンパイル実行(()=>s.SelectMany(o=>s.Let(i=>new{o,i}).i.Where(i=>i==3)));
    }
    //var st=new Set<TestLinqDB.Sets.Key,Sets.Value>{new(new(0)),new(new(1))};
    //this.共通コンパイル実行(()=>st.SelectMany(o=>st).Where(i=>i.Key.メンバー.value==0));
    //this.共通コンパイル実行(()=>st.SelectMany(o=>st.Where(i=>i.Key.メンバー.value==0&& o.Equals(i))));
    //this.共通コンパイル実行(()=>st.Where(p=>p.Key.Equals(new Sets.Key(new(0)))));
}
