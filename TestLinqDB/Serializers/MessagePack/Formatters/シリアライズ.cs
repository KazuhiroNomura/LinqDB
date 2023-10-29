namespace TestLinqDB.Serializers.MessagePack.Formatters;
using global::MessagePack;
using E = Generic.Formatters;
[MessagePackObject(true)]
public record シリアライズ対象(
    int @int,
    string @string
);
public class シリアライズ : E.シリアライズ<シリアライズ対象>{public シリアライズ():base(C.O,new シリアライズ対象(1,"s")){}}

