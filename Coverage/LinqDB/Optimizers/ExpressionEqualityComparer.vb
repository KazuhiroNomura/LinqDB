Imports System.Linq.Expressions
Imports System.Reflection

Namespace Lite.Optimizers
    <TestClass>
    Public Class ExpressionEqualityComparer
        Private ReadOnly Optimizer As Global.Lite.Optimizers.Optimizer = New Global.Lite.Optimizers.Optimizer()
        Private ReadOnly _Int32 As Int32 = 3
        Private Shared Function Lambda(ByVal l As Func(Of Double, Double)) As Double
            Return l(2)
        End Function
        <TestMethod>
        Public Sub Power()
            Me.Optimizer.Execute(Function() _Int32 ^ 2)
        End Sub
        ' ReSharper disable once UnusedMember.Local
        Private Shared _Double As Double
        <TestMethod>
        Public Sub PowerAssign()
            'if(Parameter!=null) {
            Dim p = Expression.Parameter(GetType(Double))
            Me.Optimizer.Execute(
                Expression.Lambda(Of Func(Of Double))(
                    Expression.Call(
                        GetType(ExpressionEqualityComparer).GetMethod(NameOf(Lambda), BindingFlags.Static Or BindingFlags.NonPublic),
                        Expression.Lambda(Of Func(Of Double, Double))(
                            Expression.PowerAssign(
                                p,
                                Expression.Constant(2.0)
                            ),
                            p
                        )
                    )
                )
            )
            '}else{
            Me.Optimizer.Execute(
                Expression.Lambda(Of Func(Of Double))(
                    Expression.PowerAssign(
                        Expression.Field(
                            Nothing,
                            GetType(ExpressionEqualityComparer).GetField(NameOf(_Double), BindingFlags.Static Or BindingFlags.NonPublic)
                        ),
                        Expression.Constant(2.0)
                    )
                )
            )
            '}
        End Sub
    End Class
End Namespace
