namespace TestLinqDB.Serializers.Utf8Json.Formatters;
using E = Generic.Formatters;
[Serializable]public record シリアライズ対象(
    int @int,
    string @string
);
public class シリアライズ : E.シリアライズ<シリアライズ対象>{public シリアライズ():base(C.O,new シリアライズ対象(3,"c")){}}

