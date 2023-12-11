using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq.Expressions;
using LinqDB.Optimizers.Comparison;
using LinqDB.Optimizers.VoidExpressionTraverser;
using LinqDB.Serializers.MemoryPack.Formatters;
using Expression=System.Linq.Expressions.Expression;
namespace LinqDB.Optimizers.ReturnExpressionTraverser;
using static Common;
/// <summary>
/// Conditional,Loopを全てジャンプ命令に変換
/// </summary>
internal sealed class 変換_ジャンプ命令のみ:ReturnExpressionTraverser_Quoteを処理しない {
    internal 変換_ジャンプ命令のみ(作業配列 作業配列) : base(作業配列){
    }
    public Expression 実行(Expression Expression0) {
        var Expression1 = this.Traverse(Expression0);
        return Expression1;
    }
    protected override Expression Loop(LoopExpression Loop0){
        var ContinueLabel=Loop0.ContinueLabel??Expression.Label("Continue");
        if(Loop0.BreakLabel is not null){
            return Expression.Block(
                Expression.Label(ContinueLabel),
                this.Traverse(Loop0.Body),
                Expression.Continue(ContinueLabel),
                Expression.Label(Loop0.BreakLabel)
            );
        } else{
             return Expression.Block(
                Expression.Label(ContinueLabel),
                this.Traverse(Loop0.Body),
                Expression.Continue(ContinueLabel)
            );
        }
    }
    protected override Expression Conditional(ConditionalExpression Conditional0){
        var ifTrue=Expression.Label("ifTrue");
        var ifFalse=Expression.Label("ifFalse");
        var endif=Expression.Label("endif");
        return Expression.Block(
            Expression.IfThenElse(
                this.Traverse(Conditional0.Test),
                Expression.Goto(ifTrue),
                Expression.Goto(ifFalse)
            ),
            Expression.Label(ifTrue),
            Expression.Goto(
                endif,
                this.Traverse(Conditional0.IfTrue)
            ),
            Expression.Label(ifFalse),
            Expression.Label(
                endif,
                this.Traverse(Conditional0.IfFalse)
            )
        );
    }
    protected override Expression AndAlso(BinaryExpression Binary0){
        var Binary1_Left=this.Traverse(Binary0.Left);
        var endif=Expression.Label("endif");
        return Expression.Block(
            Expression.IfThenElse(
                Binary1_Left,
                Default_void,
                Expression.Goto(
                    endif,
                    Binary1_Left
                )
            ),
            Expression.Label(
                endif,
                Expression.And(
                    Binary1_Left,
                    this.Traverse(Binary0.Right),
                    Binary0.Method
                )
            )
        );
    }
    protected override Expression OrElse(BinaryExpression Binary0){
        var Binary1_Left=this.Traverse(Binary0.Left);
        var endif=Expression.Label("endif");
        return Expression.Block(
            Expression.IfThenElse(
                Binary1_Left,
                Expression.Goto(
                    endif,
                    Binary1_Left
                ),
                Default_void
            ),
            Expression.Label(
                endif,
                Expression.Or(
                    Binary1_Left,
                    this.Traverse(Binary0.Right),
                    Binary0.Method
                )
            )
        );
    }
    protected override Expression Block(BlockExpression Block0){
        var Block0_Expressions=Block0.Expressions;
        var Expressions=new List<Expression>(Block0_Expressions.Count);
        foreach(var Block0_Expression in Block0_Expressions) {
            var Block1_Expression=this.Traverse(Block0_Expression);
            if(Block1_Expression is BlockExpression{Variables.Count: 0} Block1_Block1){
                Expressions.AddRange(Block1_Block1.Expressions);
                continue;
            }
            Expressions.Add(Block1_Expression);
        }
        return Expression.Block(Block0.Variables,Expressions);
    }
}
