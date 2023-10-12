using System.Linq.Expressions;
using LinqDB.Sets;
using static LinqDB.Optimizers.Optimizer;
using TestLinqDB.Serializers.Formatters.Sets;
using TestLinqDB.Sets;
using テスト.PrimaryKeys.dbo;
using Expression = System.Linq.Expressions.Expression;
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
        //if(!ループ展開可能メソッドか(MethodCall0)) return base.Call(MethodCall0);
        //if(Reflection.ExtensionSet.Inline1==GetGenericMethodDefinition(MethodCall0.Method)) {
        //    var MethodCall1_Arguments_0 = MethodCall0_Arguments_0 is LambdaExpression Lambda0
        //        ? this.Lambda(Lambda0) : this.Traverse(MethodCall0_Arguments_0);
        this.共通コンパイル実行(()=>LinqDB.Sets.ExtensionSet.Inline(()=>""));
        this.共通コンパイル実行(()=>LinqDB.Sets.ExtensionSet.Inline((Func<string>)(()=>"")));
        //}
        var s = new Set<int>();
        //for(var a = 1;a<MethodCall0_Arguments_Count;a++) {
        //    MethodCall1_Arguments[a]=MethodCall0_Argument is LambdaExpression Lambda0
        //        ? this.Lambda(Lambda0) : this.Traverse(MethodCall0_Argument);
        this.共通コンパイル実行(()=>s.Select(p=>p+1));
        this.共通コンパイル実行(()=>s.Select((Func<int,int>)(p=>p+1)));
        //}
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
