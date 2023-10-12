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
    [Fact]public void 外だし(){
        var a = Expression.Parameter(typeof(int), "Pワーク変数a");
        var b = Expression.Parameter(typeof(int), "Pワーク変数b");
        var c = Expression.Parameter(typeof(int), "Pワーク変数c");
        var s = new Set<int>();
        /*
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
        */
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
