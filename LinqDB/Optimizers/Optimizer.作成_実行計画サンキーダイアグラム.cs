using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace LinqDB.Optimizers;

partial class Optimizer {
    private sealed class 作成_実行計画サンキーダイアグラム:VoidExpressionTraverser {
        private readonly StringBuilder sb=new();
        private string? 上位ノード;
        public void 実行(Expression e) {
            var sb=this.sb;
            sb.Clear();
            sb.AppendLine("var data=[");
            this.上位ノード="出力";
            this.Traverse(((LambdaExpression)e).Body);
            sb.AppendLine("];");
            File.WriteAllText(@"data.js", sb.ToString(), Encoding.Unicode);
        }
        protected override void Assign(BinaryExpression Binary) {
            var 旧上位ノード=this.上位ノード;
            //var 新上位ノード=this.上位ノード=Binary.Left.ToString();
            this.Traverse(Binary.Right);
            this.上位ノード=旧上位ノード;
            //this.sb.AppendLine("['"+新上位ノード+"','"+旧上位ノード+"',50],");
            //base.Assign(特定の形式はなし);
        }
        protected override void Constant(ConstantExpression Constant) {
            if(Constant.Value  is not null) {
                this.sb.Append("['" + Constant.Value + "','" + this.上位ノード + "',5],");
            } else {
                this.sb.Append("['null','" + this.上位ノード + "',5],");
            }
        }
        protected override void Call(MethodCallExpression MethodCall) {
            var 旧上位ノード=this.上位ノード;
            var 新上位ノード=this.上位ノード=MethodCall.Method.Name;
            if(MethodCall.Object  is not null) {
                this.Traverse(MethodCall.Object);
            }
            this.TraverseExpressions(MethodCall.Arguments);
            this.上位ノード=旧上位ノード;
            this.sb.AppendLine("['" + 新上位ノード + "','" + 旧上位ノード + "',5],");
        }
        protected override void Lambda(LambdaExpression Lambda) {
        }
        protected override void MemberAccess(MemberExpression Member) {
            var 旧上位ノード=this.上位ノード;
            string 新上位ノード;
            if(Member.Member is PropertyInfo Property) {
                if(Property.GetMethod!.IsStatic) {
                    //Debug.Assert(Property.DeclaringType is not null,"Property.DeclaringType  is not null");
                    //Debug.Assert(Property.DeclaringType.Name is not null,"Property.DeclaringType.Name  is not null");
                    新上位ノード=Property.DeclaringType!.Name + "." + Property.Name;
                } else {
                    新上位ノード="." + Property.Name;
                }
            } else {
                var Field=(FieldInfo)Member.Member;
                if(Field.IsStatic) {
                    //Debug.Assert(Field.DeclaringType  is not null, "Field.DeclaringType  is not null");
                    //Debug.Assert(Field.DeclaringType.Name is not null,"Field.DeclaringType.Name  is not null");
                    新上位ノード=Field.DeclaringType!.Name + "." + Field.Name;
                } else {
                    新上位ノード="." + Field.Name;
                }
            }
            this.上位ノード=新上位ノード;
            this.Traverse(Member.Expression);
            this.上位ノード=旧上位ノード;
            this.sb.AppendLine("['" + 新上位ノード + "','" + 旧上位ノード + "',5],");
        }
        protected override void Parameter(ParameterExpression Parameter) => this.sb.AppendLine("['"+Parameter.Name+"','"+this.上位ノード+"',5],");
    }
}