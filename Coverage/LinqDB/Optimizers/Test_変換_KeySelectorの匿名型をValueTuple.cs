using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable SimilarAnonymousTypeNearby
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class Test_変換_KeySelectorの匿名型をValueTuple : ATest
{
    private static Func<int, T> Delegateを返す<T>(Func<int, T> t) => t;
    [TestMethod]
    public void Call00(){
        var SetInt32変数=new Set<int>{1,2};
        var ArrInt32変数=new[]{1,2};
        //   for(var a=0;a<MethodCall0_Arguments_Count;a++){
        //       if(MethodCall0_Argument!=MethodCall2_Argument){
        this.実行結果が一致するか確認(()=>
            SetInt32変数.Join(
                SetInt32変数,
                o=>o,
                i=>i,
                (o,i)=>o+i
            ).Join(
                SetInt32変数,
                o=>o,
                i=>i,
                (o,i)=>o+i
            )
        );
        this.実行結果が一致するか確認(()=>
            SetInt32変数.Join(
                SetInt32変数.Join(
                    SetInt32変数,
                    o=>o,
                    i=>i,
                    (o,i)=>o+i
                ),
                o=>o,
                i=>i,
                (o,i)=>o+i
            )
        );
        this.実行結果が一致するか確認(()=>
            SetInt32変数.Join(
                SetInt32変数.Join(
                    SetInt32変数,
                    o=>new{Key=o,Key1=o},
                    i=>new{Key=i,Key1=i},
                    (o,i)=>o+i
                ),
                o=>new{Key=o,Key1=o},
                i=>new{Key=i,Key1=i},
                (o,i)=>new{o,i}
            )
        );
        this.実行結果が一致するか確認(()=>
            ArrInt32変数.Join(
                ArrInt32変数,
                o=>new{Key=o},
                i=>new{Key=i},
                (o,i)=>new{o,i}
            )
        );
        this.実行結果が一致するか確認(()=>
            SetInt32変数.Join(
                SetInt32変数,
                o=>new{Key=o},
                i=>new{Key=i},
                (o,i)=>new{o,i}
            )
        );
    }
    [TestMethod]public void Call05(){
        var SetInt32変数=new Set<int>{1,2};
        var ArrInt32変数=new[]{1,2};
        this.実行結果が一致するか確認(() =>
            SetInt32変数.Join(
                SetInt32変数.Join(
                    SetInt32変数,
                    o => o,
                    i => i,
                    (o, i) => o 
                ),
                o=>new{ Key = new { Key = o },o=o},
                i=>new{ Key = new { Key = i },o=i},
                (o, i) => o
            )
        );
        this.実行結果が一致するか確認(() =>
            SetInt32変数.Join(
                SetInt32変数.Join(
                    SetInt32変数,
                    o => new { Key = new { Key = o }, Key1 = new { Key = o } },
                    i => new { Key = new { Key = i }, Key1 = new { Key = i } },
                    (o, i) => o + i
                ),
                o => new { Key = new { Key = o }, Key1 = new { Key = o } },
                i => new { Key = new { Key = i }, Key1 = new { Key = i } },
                (o, i) => new { o, i }
            )
        );
        //       }
        //   }
        //   if(Reflection.ExtendSet.Join==MethodCall0_GenericMethodDefinition||Reflection.ExtendEnumerable.Join==MethodCall0_GenericMethodDefinition) {
        //       if(KeyType.IsAnonymous()){
        //           var MethodCall3_Arguments=変化したか
        //               ?MethodCall1_Arguments
        //               :MethodCall0_Arguments.ToArray();
        //            if(MethodCall3_Arguments[2] is LambdaExpression outerKeySelector1&&MethodCall3_Arguments[3] is LambdaExpression innerKeySelector1) {
        this.実行結果が一致するか確認(() => ArrInt32変数.Join(ArrInt32変数, Delegateを返す(i => new { Key = i }), Delegateを返す(o => new { Key = o }), (o, i) => new { o, i }));
        this.実行結果が一致するか確認(() => SetInt32変数.Join(SetInt32変数, Delegateを返す(i => new { Key = i }), Delegateを返す(o => new { Key = o }), (o, i) => new { o, i }));
        this.実行結果が一致するか確認(() => ArrInt32変数.Join(ArrInt32変数, Delegateを返す(i => new { Key = i }), o => new { Key = o }, (o, i) => new { o, i }));
        this.実行結果が一致するか確認(() => SetInt32変数.Join(SetInt32変数, Delegateを返す(i => new { Key = i }), o => new { Key = o }, (o, i) => new { o, i }));
        this.実行結果が一致するか確認(() => ArrInt32変数.Join(ArrInt32変数, i => new { Key = i }, Delegateを返す(o => new { Key = o }), (o, i) => new { o, i }));
        this.実行結果が一致するか確認(() => SetInt32変数.Join(SetInt32変数, i => new { Key = i }, Delegateを返す(o => new { Key = o }), (o, i) => new { o, i }));
        //           if(outerKeySelector1_Body.NodeType==ExpressionType.New&&innerKeySelector1_Body.NodeType==ExpressionType.New){
        //               if(KeyType_GetGenericArguments.Length==1){
        //                }else{
        this.実行結果が一致するか確認(() => SetInt32変数.Join(SetInt32変数, o => new { Key1 = o, Key2 = o + 1 }, i => new { Key1 = i, Key2 = i + 1 }, (o, i) => new { o, i }));
        this.実行結果が一致するか確認(() => ArrInt32変数.Join(ArrInt32変数, o => new { Key1 = o, Key2 = o + 1 }, i => new { Key1 = i, Key2 = i + 1 }, (o, i) => new { o, i }));
        //               }
        //           }
        var Key値 = new { Key = 1 };
        this.実行結果が一致するか確認(() => SetInt32変数.Join(SetInt32変数, o => Key値, i => Key値, (o, i) => i));
        this.実行結果が一致するか確認(() => ArrInt32変数.Join(ArrInt32変数, o => Key値, i => Key値, (o, i) => new { o, i }));
        //        }
        this.実行結果が一致するか確認(() => SetInt32変数.Join(SetInt32変数, o => o + 1, i => i + 1, (o, i) => new { o, i }));
        this.実行結果が一致するか確認(() => ArrInt32変数.Join(ArrInt32変数, o => o + 1, i => i + 1, (o, i) => new { o, i }));
        //    } else if(Reflection.ExtendSet.GroupJoin==MethodCall0_GenericMethodDefinition||Reflection.ExtendEnumerable.GroupJoin0==MethodCall0_GenericMethodDefinition) {
        //       if(KeyType.IsAnonymous()){
        //           if(outerKeySelector1_Body.NodeType==ExpressionType.New&&innerKeySelector1_Body.NodeType==ExpressionType.New){
        //               if(KeyType_GetGenericArguments.Length==1){
        this.実行結果が一致するか確認(() => SetInt32変数.GroupJoin(SetInt32変数, o => new { Key = o }, i => new { Key = i }, (o, i) => new { o, i }));
        this.実行結果が一致するか確認(() => ArrInt32変数.GroupJoin(ArrInt32変数, o => new { Key = o }, i => new { Key = i }, (o, i) => new { o, i }));
        //                }else{
        this.実行結果が一致するか確認(() => SetInt32変数.GroupJoin(SetInt32変数, o => new { Key1 = o, Key2 = o + 1 }, i => new { Key1 = i, Key2 = i + 1 }, (o, i) => new { o, i }));
        this.実行結果が一致するか確認(() => SetInt32変数.GroupJoin(SetInt32変数, o => new
        {
            Key1 = o,
            Key2 = o + 1,
            Key3 = o + 1,
            Key4 = o + 1,
            Key5 = o + 1,
            Key6 = o + 1,
            Key7 = o + 1
        }, i => new
        {
            Key1 = i,
            Key2 = i + 1,
            Key3 = i + 1,
            Key4 = i + 1,
            Key5 = i + 1,
            Key6 = i + 1,
            Key7 = i + 1
        }, (o, i) => new
        {
            o,
            i
        }));
        this.実行結果が一致するか確認(() => SetInt32変数.GroupJoin(SetInt32変数, o => new
        {
            Key1 = o,
            Key2 = o + 1,
            Key3 = o + 1,
            Key4 = o + 1,
            Key5 = o + 1,
            Key6 = o + 1,
            Key7 = o + 1,
            Key8 = o + 2
        }, i => new
        {
            Key1 = i,
            Key2 = i + 1,
            Key3 = i + 1,
            Key4 = i + 1,
            Key5 = i + 1,
            Key6 = i + 1,
            Key7 = i + 1,
            Key8 = i + 2
        }, (o, i) => new
        {
            o,
            i
        }));
        this.実行結果が一致するか確認(() => ArrInt32変数.GroupJoin(ArrInt32変数, o => new { Key1 = o, Key2 = o + 1 }, i => new { Key1 = i, Key2 = i + 1 }, (o, i) => new { o, i }));
        //               }
        //           }
        this.実行結果が一致するか確認(() => SetInt32変数.GroupJoin(SetInt32変数,o => Key値,i => Key値,(o,i) => new { o,i }));
        this.実行結果が一致するか確認(() => ArrInt32変数.GroupJoin(ArrInt32変数,o => Key値,i => Key値,(o,i) => new { o,i }));
        //        }
        this.実行結果が一致するか確認(() => SetInt32変数.GroupJoin(SetInt32変数, o => o + 1, i => i + 1, (o, i) => new { o, i }));
        this.実行結果が一致するか確認(() => ArrInt32変数.GroupJoin(ArrInt32変数, o => o + 1, i => i + 1, (o, i) => new { o, i }));
        //    }
        //    if(MethodCall0_Object==MethodCall1_Object&&!変化したか) return MethodCall0;
        this.実行結果が一致するか確認(() =>
            SetInt32変数.Where(p => p % 2 == 0)
        );
        this.実行結果が一致するか確認(() =>
            SetInt32変数.Join(
                SetInt32変数,
                o => new { Key = new { Key = o }, Key1 = new { Key = o } },
                i => new { Key = new { Key = i }, Key1 = new { Key = i } },
                (o, i) => o + i
            ).Any()
        );
        this.実行結果が一致するか確認(() =>
            SetInt32変数.Join(
                SetInt32変数,
                o => new
                {
                    Key = new
                    {
                        Key = o
                    },
                    Key1 = new
                    {
                        Key = o
                    }
                },
                i => new
                {
                    Key = new
                    {
                        Key = i
                    },
                    Key1 = new
                    {
                        Key = i
                    }
                },
                (o, i) => o + i
            ).ToString()
        );
    }
}