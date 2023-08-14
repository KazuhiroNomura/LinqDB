using System;
namespace LinqDB.Reflection;

internal static class ValueTuple {
    public static readonly Type ValueTuple1 = typeof(ValueTuple<>);
    public static readonly Type ValueTuple2 = typeof(ValueTuple<,>);
    public static readonly Type ValueTuple3 = typeof(ValueTuple<,,>);
    public static readonly Type ValueTuple4 = typeof(ValueTuple<,,,>);
    public static readonly Type ValueTuple5 = typeof(ValueTuple<,,,,>);
    public static readonly Type ValueTuple6 = typeof(ValueTuple<,,,,,>);
    public static readonly Type ValueTuple7 = typeof(ValueTuple<,,,,,,>);
    public static readonly Type ValueTuple8 = typeof(ValueTuple<,,,,,,,>);
}