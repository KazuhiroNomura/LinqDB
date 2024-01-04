using System.Reflection;
using TestLinqDB;
using Xunit;
var 無効数=0;
var 有効数=0;
foreach(var Type in typeof(Serializer).Assembly.GetTypes()){
    if(!Type.IsPublic){
        無効数++;
        continue;
    }
    if(!Type.IsClass){
        無効数++;
        continue;
    }
    var ctor=Type.GetConstructor(Type.EmptyTypes);
    if(ctor is null){
        無効数++;
        continue;
    }
    var o=Activator.CreateInstance(Type);
    foreach(var Method in Type.GetMethods(BindingFlags.Public|BindingFlags.Instance)){
        if(Method.GetCustomAttribute<FactAttribute>() is null) continue;
        Method.Invoke(o,Array.Empty<object>());
        有効数++;
    }
}
Console.WriteLine($"無効{無効数},有効{有効数}");
Console.ReadKey();
