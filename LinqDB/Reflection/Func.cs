using System;
namespace LinqDB.Reflection;
/// <summary>
/// ループ展開するEnumerable拡張クラスのstaticメソッド
/// </summary>
public static class Func {
    public static Type Get(int 引数の数)=>引数の数 switch {
        0=>typeof(Func<>),
        1=>typeof(Func<,>),
        2=>typeof(Func<,,>),
        3=>typeof(Func<,,,>),
        4=>typeof(Func<,,,,>),
        5=>typeof(Func<,,,,,>),
        6=>typeof(Func<,,,,,,>),
        7=>typeof(Func<,,,,,,,>),
        8=>typeof(Func<,,,,,,,,>),
        9=>typeof(Func<,,,,,,,,,>),
        10=>typeof(Func<,,,,,,,,,,>),
        11=>typeof(Func<,,,,,,,,,,,>),
        12=>typeof(Func<,,,,,,,,,,,,>),
        13=>typeof(Func<,,,,,,,,,,,,,>),
        14=>typeof(Func<,,,,,,,,,,,,,,>),
        15=>typeof(Func<,,,,,,,,,,,,,,,>),
        16=>typeof(Func<,,,,,,,,,,,,,,,,>),
        _=>throw new NotSupportedException(引数の数.ToString())
    };
}
