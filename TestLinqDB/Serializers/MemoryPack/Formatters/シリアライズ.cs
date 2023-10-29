namespace TestLinqDB.Serializers.MemoryPack.Formatters;
using global::MemoryPack;
using E = Generic.Formatters;

[MemoryPackable]
public partial record シリアライズ対象(
    int @int,
    string @string
);
public class シリアライズ : E.シリアライズ<シリアライズ対象>{public シリアライズ():base(C.O,new シリアライズ対象(1,"s")){}}

