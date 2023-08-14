using System.Reflection;
namespace LinqDB.Reflection;

internal static class Container{
    public static readonly MethodInfo TABLE_DEE = typeof(Databases.Container).GetProperty(nameof(Databases.Container.TABLE_DEE),BindingFlags.Static|BindingFlags.Public)!.GetMethod!;
    public static readonly MethodInfo TABLE_DUM = typeof(Databases.Container).GetProperty(nameof(Databases.Container.TABLE_DUM),BindingFlags.Static|BindingFlags.Public)!.GetMethod!;
}