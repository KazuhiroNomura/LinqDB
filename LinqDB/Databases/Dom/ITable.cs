using System.Reflection.Emit;
using System.Collections.Generic;
using LinqDB.Helpers;

namespace LinqDB.Databases.Dom;
public interface IColumns{
    IEnumerable<IColumn> Columns { get; }
}
public interface ITable:IName,IColumns{
    ISchema Schema { get; }
    double Left { get; }
    double Top { get; }
    internal class Information {
        internal readonly TypeBuilder Key_TypeBuilder;
        internal readonly MethodBuilder Key_IEquatable_Equals;
        internal readonly ConstructorBuilder Key_ctor;
        internal readonly ILGenerator ctor_I;
        internal readonly FieldBuilder Tables_FieldBuilder;
        internal readonly MethodBuilder GetMethod;
        internal readonly ILGenerator AddRelationship_I;
        internal readonly ILGenerator RemoveRelationship_I;
        internal TypeBuilder TypeBuilder { get; private set; }
        public Information(TypeBuilder Key_TypeBuilder,MethodBuilder Key_IEquatable_Equals,ConstructorBuilder Key_ctor,TypeBuilder TypeBuilder,ILGenerator ctor_I,FieldBuilder Tables_FieldBuilder,MethodBuilder GetMethod,ILGenerator AddRelationship_I,ILGenerator RemoveRelationship_I) {
            this.Key_TypeBuilder= Key_TypeBuilder;
            this.Key_IEquatable_Equals= Key_IEquatable_Equals;
            this.Key_ctor= Key_ctor;
            this.TypeBuilder= TypeBuilder;
            this.ctor_I= ctor_I;
            this.Tables_FieldBuilder=Tables_FieldBuilder;
            this.GetMethod=GetMethod;
            this.AddRelationship_I= AddRelationship_I;
            this.RemoveRelationship_I=RemoveRelationship_I;
        }
        //internal Type? Type;
        public void Create() {
            this.AddRelationship_I.Ret();
            this.RemoveRelationship_I.Ret();
            this.ctor_I.Ret();
            this.TypeBuilder!.CreateType();
            this.TypeBuilder=default!;
        }
    }
}
