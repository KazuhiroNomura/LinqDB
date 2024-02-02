using System.Reflection.Emit;
using System.Collections.Generic;
using LinqDB.Helpers;

namespace LinqDB.Databases.Dom;
public interface ITable:IName,IColumns{
    ISchema Schema { get; }
    internal class Information(TypeBuilder Key_TypeBuilder,MethodBuilder Key_IEquatable_Equals,ConstructorBuilder Key_ctor,TypeBuilder TypeBuilder,ILGenerator ctor_I,FieldBuilder Tables_FieldBuilder,MethodBuilder GetMethod,ILGenerator AddRelationship_I,ILGenerator RemoveRelationship_I){
        internal readonly TypeBuilder Key_TypeBuilder=Key_TypeBuilder;
        internal readonly MethodBuilder Key_IEquatable_Equals=Key_IEquatable_Equals;
        internal readonly ConstructorBuilder Key_ctor=Key_ctor;
        internal readonly ILGenerator ctor_I=ctor_I;
        internal readonly FieldBuilder Tables_FieldBuilder=Tables_FieldBuilder;
        internal readonly MethodBuilder GetMethod=GetMethod;
        internal readonly ILGenerator AddRelationship_I=AddRelationship_I;
        internal readonly ILGenerator RemoveRelationship_I=RemoveRelationship_I;
        internal TypeBuilder TypeBuilder { get; private set; }=TypeBuilder;
        //internal Type? Type;
        public void Create() {
            this.AddRelationship_I.Ret();
            this.RemoveRelationship_I.Ret();
            this.ctor_I.Ret();
            this.TypeBuilder.CreateType();
            this.TypeBuilder=default!;
        }
    }
}
