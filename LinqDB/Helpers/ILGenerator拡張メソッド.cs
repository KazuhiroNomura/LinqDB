using System;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
namespace LinqDB.Helpers;

/// <summary>
/// ILGeneratorメソッドの便利なシグネチャ
/// </summary>
public static class ILGenerator拡張メソッド {
    private static readonly Func<ILGenerator,Label,int> GetLabelPos= InitGetLabelPos();
    private static Func<ILGenerator,Label,int> InitGetLabelPos() {
        var D = new DynamicMethod("GetLabelValue",typeof(int),new[]{
            typeof(ILGenerator),typeof(Label)
        },typeof(ILGenerator拡張メソッド),true) {
            InitLocals=false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        I.Ldfld(typeof(ILGenerator).GetField("m_labelList",BindingFlags.Instance|BindingFlags.NonPublic));
        I.Ldarg_1();
        I.Ldfld(typeof(Label).GetField("m_label",BindingFlags.Instance|BindingFlags.NonPublic));
        I.Ldelem_I4();
        I.Ret();
        return(Func<ILGenerator,Label,int>)D.CreateDelegate(typeof(Func<ILGenerator,Label,int>));
    }
    //static ILGenerator拡張メソッド(){
    //    var D=new DynamicMethod("GetLabelValue",typeof(Int32),new[]{
    //        typeof(ILGenerator),typeof(Label)
    //    },typeof(ILGenerator拡張メソッド),true){
    //        InitLocals=false
    //    };
    //    var I=D.GetILGenerator();
    //    I.Ldarg_0();
    //    I.Ldfld(typeof(ILGenerator).GetField("m_labelList",BindingFlags.Instance|BindingFlags.NonPublic));
    //    I.Ldarg_1();
    //    I.Ldfld(typeof(Label).GetField("m_label",BindingFlags.Instance|BindingFlags.NonPublic));
    //    I.Ldelem_I4();
    //    I.Ret();
    //    GetLabelPos=(Func<ILGenerator,Label,Int32>)D.CreateDelegate(typeof(Func<ILGenerator,Label,Int32>));
    //}
    private static void B_S命令かB命令か(ILGenerator I,Label Label,OpCode 短縮形,OpCode 通常形){
        var LabelPos=GetLabelPos(I,Label);
        if(LabelPos==-1)
            I.Emit(通常形,Label);
        //上にジャンプ
        else if(-128<=LabelPos-I.ILOffset-2)//-2はBr_S,オフセット(Byte)
            I.Emit(短縮形,Label);
        else
            I.Emit(通常形,Label);
    }
    /// <summary>
    /// 2 つの値を加算し、結果を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Add           (this ILGenerator I                  )=>I.Emit(OpCodes.Add                );
    /// <summary>
    /// 2 つの整数値を加算し、オーバーフロー チェックを実行し、結果を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Add_Ovf       (this ILGenerator I                  )=>I.Emit(OpCodes.Add_Ovf            );
    /// <summary>
    /// 2 つの符号なし整数値を加算し、オーバーフロー チェックを実行し、結果を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Add_Ovf_Un    (this ILGenerator I                  )=>I.Emit(OpCodes.Add_Ovf_Un         );
    /// <summary>
    /// 2 つの値のビットごとの AND を計算し、結果を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void And           (this ILGenerator I                  )=>I.Emit(OpCodes.And                );
    /// <summary>
    /// 現在のメソッドの引数リストへのアンマネージ ポインターを返します。
    /// </summary>
    /// <param name="I"></param>
    public static void Arglist       (this ILGenerator I                  )=>I.Emit(OpCodes.Arglist            );
    /// <summary>
    /// 2 つの値が等しい場合は、ターゲット命令に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Beq           (this ILGenerator I,Label Label      )=>B_S命令かB命令か(I,Label,OpCodes.Beq_S,OpCodes.Beq);
    /// <summary>
    /// 2 つの値が等しい場合は、ターゲット命令 (短い形式) に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Beq_S         (this ILGenerator I,Label Label      )=>I.Emit(OpCodes.Beq_S        ,Label);
    /// <summary>
    /// 最初の値が 2 番目の値以上の場合は、ターゲット命令に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Bge           (this ILGenerator I,Label Label      )=>B_S命令かB命令か(I,Label,OpCodes.Bge_S,OpCodes.Bge);
    /// <summary>
    /// 最初の値が 2 番目の値以上の場合は、ターゲット命令 (短い形式) に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Bge_S         (this ILGenerator I,Label Label      )=>I.Emit(OpCodes.Bge_S        ,Label);
    /// <summary>
    /// 符号なし整数値または順序なし float 値を比較したとき、最初の値が 2 番目の値を超える場合は、ターゲット命令に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Bge_Un        (this ILGenerator I,Label Label      )=>B_S命令かB命令か(I,Label,OpCodes.Bge_Un_S,OpCodes.Bge_Un);
    /// <summary>
    /// 符号なし整数値または順序なし float 値を比較したとき、最初の値が 2 番目の値を超える場合は、ターゲット命令 (短い形式) に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Bge_Un_S      (this ILGenerator I,Label Label      )=>I.Emit(OpCodes.Bge_Un_S     ,Label);
    /// <summary>
    /// 最初の値が 2 番目の値を超える場合は、ターゲット命令に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Bgt           (this ILGenerator I,Label Label      )=>B_S命令かB命令か(I,Label,OpCodes.Bgt_S,OpCodes.Bgt);
    /// <summary>
    /// 最初の値が 2 番目の値を超える場合は、ターゲット命令 (短い形式) に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Bgt_S         (this ILGenerator I,Label Label      )=>I.Emit(OpCodes.Bgt_S        ,Label);
    /// <summary>
    /// 符号なし整数値または順序なし float 値を比較したとき、最初の値が 2 番目の値を超える場合は、ターゲット命令に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Bgt_Un        (this ILGenerator I,Label Label      )=>B_S命令かB命令か(I,Label,OpCodes.Bgt_Un_S,OpCodes.Bgt_Un);
    /// <summary>
    /// 符号なし整数値または順序なし float 値を比較したとき、最初の値が 2 番目の値を超える場合は、ターゲット命令 (短い形式) に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Bgt_Un_S      (this ILGenerator I,Label Label      )=>I.Emit(OpCodes.Bgt_Un_S     ,Label);
    /// <summary>
    /// 最初の値が 2 番目の値以下の場合は、ターゲット命令に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Ble           (this ILGenerator I,Label Label      )=>B_S命令かB命令か(I,Label,OpCodes.Ble_S,OpCodes.Ble);
    /// <summary>
    /// 最初の値が 2 番目の値以下の場合は、ターゲット命令 (短い形式) に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Ble_S         (this ILGenerator I,Label Label      )=>I.Emit(OpCodes.Ble_S        ,Label);
    /// <summary>
    /// 符号なし整数値または順序なし float 値を比較したとき、最初の値が 2 番目の値以下の場合は、ターゲット命令に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Ble_Un        (this ILGenerator I,Label Label      )=>B_S命令かB命令か(I,Label,OpCodes.Ble_Un_S,OpCodes.Ble_Un);
    /// <summary>
    /// 符号なし整数値または順序なし float 値を比較したとき、最初の値が 2 番目の値以下の場合は、ターゲット命令 (短い形式) に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Ble_Un_S      (this ILGenerator I,Label Label      )=>I.Emit(OpCodes.Ble_Un_S     ,Label);
    /// <summary>
    /// 最初の値が 2 番目の値より小さい場合は、ターゲット命令に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Blt           (this ILGenerator I,Label Label      )=>B_S命令かB命令か(I,Label,OpCodes.Blt_S,OpCodes.Blt);
    /// <summary>
    /// 最初の値が 2 番目の値より小さい場合は、ターゲット命令 (短い形式) に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Blt_S         (this ILGenerator I,Label Label      )=>I.Emit(OpCodes.Blt_S        ,Label);
    /// <summary>
    /// 符号なし整数値または順序なし float 値を比較したとき、最初の値が 2 番目の値より小さい場合は、ターゲット命令に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Blt_Un        (this ILGenerator I,Label Label      )=>B_S命令かB命令か(I,Label,OpCodes.Blt_Un_S,OpCodes.Blt_Un);
    /// <summary>
    /// 符号なし整数値または順序なし float 値を比較したとき、最初の値が 2 番目の値より小さい場合は、ターゲット命令 (短い形式) に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Blt_Un_S      (this ILGenerator I,Label Label      )=>I.Emit(OpCodes.Blt_Un_S     ,Label);
    /// <summary>
    /// 2 つの符号なし整数値または順序なし float 値が等しくない場合は、ターゲット命令に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Bne_Un        (this ILGenerator I,Label Label      )=>B_S命令かB命令か(I,Label,OpCodes.Bne_Un_S,OpCodes.Bne_Un);
    /// <summary>
    /// 2 つの符号なし整数値または順序なし float 値が等しくない場合は、ターゲット命令 (短い形式) に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Bne_Un_S      (this ILGenerator I,Label Label      )=>I.Emit(OpCodes.Bne_Un_S     ,Label);
    /// <summary>
    /// 値型をオブジェクト参照 (O 型) に変換します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="T"></param>
    public static void Box           (this ILGenerator I,Type T           )=>I.Emit(OpCodes.Box          ,T    );
    /// <summary>
    /// 無条件でターゲット命令に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Br(this ILGenerator I,Label Label                  )=>B_S命令かB命令か(I,Label,OpCodes.Br_S,OpCodes.Br);
    /// <summary>
    /// 無条件でターゲット命令に制御を転送します (短い形式)。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Br_S          (this ILGenerator I,Label Label      )=>I.Emit(OpCodes.Br_S         ,Label);
    /// <summary>
    /// ブレークポイントがトリップしたことをデバッガーに通知するように、共通言語基盤 (CLI) に通知します。
    /// </summary>
    /// <param name="I"></param>
    public static void Break         (this ILGenerator I                  )=>I.Emit(OpCodes.Break              );
    /// <summary>
    /// value が false、null 参照 (Visual Basic の場合は Nothing)、または 0 の場合は、ターゲット命令に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Brfalse       (this ILGenerator I,Label Label      )=>B_S命令かB命令か(I,Label,OpCodes.Brfalse_S,OpCodes.Brfalse);
    /// <summary>
    /// 0 MarkLabel(Label)
    /// 0 Nop
    /// 1 Br.s Label
    /// 3 Nop
    /// ↑にジャンプするときはBr.sの下の命令からMarkLabelまでのオフセットである。Offset=-3である。これは-128以上であること
    /// 0 Br.s Label
    /// 2 Nop
    /// 3 MarkLabel(Label)
    /// 3 Nop
    /// ↓にジャンプするときはOffset=3である。これは127以下であること
    /// Br時点ではMarkLabelまでのOffsetが不明なのでBrかBr.sのどちらを使うか不明。
    /// だから安全のためBrを使う
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Brfalse_S     (this ILGenerator I,Label Label      )=>I.Emit(OpCodes.Brfalse_S    ,Label);
    /// <summary>
    /// value が true、null 以外、または 0 以外の場合は、ターゲット命令に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Brtrue        (this ILGenerator I,Label Label      )=>B_S命令かB命令か(I,Label,OpCodes.Brtrue_S,OpCodes.Brtrue);
    /// <summary>
    /// value が true、null 以外、または 0 以外の場合は、ターゲット命令 (短い形式) に制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Brtrue_S      (this ILGenerator I,Label Label      )=>I.Emit(OpCodes.Brtrue_S     ,Label);
    /// <summary>
    /// 渡されたメソッド記述子によって示されているメソッドを呼び出します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="M"></param>
    public static void Call          (this ILGenerator I,MethodInfo M     ){
        Debug.Assert(!M.IsAbstract,"AbstractメソッドはCallしてはいけない。");
        I.Emit(OpCodes.Call         ,M    );
    }
    /// <summary>
    /// コンストラクタで現在のオブジェクトを書き換える。基底コンストラクタを呼び出すときに使う。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="C"></param>
    public static void Call          (this ILGenerator I,ConstructorInfo C)=>I.Emit(OpCodes.Call         ,C    );
    /// <summary>
    /// オブジェクト上で遅延バインディング メソッドを呼び出し、戻り値を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="M"></param>
    public static void Callvirt      (this ILGenerator I,MethodInfo M     ){
        Debug.Assert(M.IsAbstract||M.IsVirtual,"Abstract,Virtualメソッド以外はCallvirtしてはいけない。ポインタチェックしたいならCallvirtしてもよい。");
        I.Emit(OpCodes.Callvirt     ,M    );
    }
    /// <summary>
    /// 指定したクラスへの参照により渡されたオブジェクトをキャストしようとします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="T"></param>
    public static void Castclass     (this ILGenerator I,Type T           )=>I.Emit(OpCodes.Castclass    ,T    );
    /// <summary>
    /// 2 つの値を比較します。2 つの値が等しい場合は、整数 1 (int32) が評価スタックにプッシュされます。それ以外の場合は、0 (int32) が評価スタックにプッシュされます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ceq           (this ILGenerator I                  )=>I.Emit(OpCodes.Ceq                );
    /// <summary>
    /// 2 つの値を比較します。最初の値が 2 番目の値を超える場合は、整数 1 (int32) が評価スタックにプッシュされます。それ以外の場合は、0 (int32)が評価スタックにプッシュされます。
    /// </summary>
    /// <param name="I"></param>
    public static void Cgt           (this ILGenerator I                  )=>I.Emit(OpCodes.Cgt                );
    /// <summary>
    /// 2 つの符号なしの値または順序なしの値を比較します。最初の値が 2 番目の値を超える場合は、整数 1 (int32) が評価スタックにプッシュされます。それ以外の場合は、0(int32) が評価スタックにプッシュされます。
    /// </summary>
    /// <param name="I"></param>
    public static void Cgt_Un        (this ILGenerator I                  )=>I.Emit(OpCodes.Cgt_Un             );
    /// <summary>
    /// 値が有限数ではない場合は、System.ArithmeticException をスローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Ckfinite      (this ILGenerator I                  )=>I.Emit(OpCodes.Ckfinite           );
    /// <summary>
    /// 2 つの値を比較します。最初の値が 2 番目の値より小さい場合は、整数 1 (int32) が評価スタックにプッシュされます。それ以外の場合は、0 (int32)が評価スタックにプッシュされます。
    /// </summary>
    /// <param name="I"></param>
    public static void Clt           (this ILGenerator I                  )=>I.Emit(OpCodes.Clt                );
    /// <summary>
    /// 符号なしの値または順序なしの値である value1 と value2 を比較します。value1 が value2 より小さい場合は、整数値 1 (int32)が評価スタックにプッシュされます。それ以外の場合は、0 (int32) が評価スタックにプッシュされます。
    /// </summary>
    /// <param name="I"></param>
    public static void Clt_Un        (this ILGenerator I                  )=>I.Emit(OpCodes.Clt_Un             );
    /// <summary>
    /// 仮想メソッド呼び出しをする対象の型を制約します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="T"></param>
    public static void Constrained   (this ILGenerator I,Type T           )=>I.Emit(OpCodes.Constrained  ,T    );
    /// <summary>
    /// 評価スタックの一番上の値を native Int32 に変換します。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_I        (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_I             );
    /// <summary>
    /// 評価スタックの一番上の値を int8 に変換し、int32 への拡張 (埋め込み) を行います。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_I1       (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_I1            );
    /// <summary>
    /// 評価スタックの一番上の値を int16 に変換し、int32 への拡張 (埋め込み) を行います。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_I2       (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_I2            );
    /// <summary>
    /// 評価スタックの一番上の値を int32 に変換します。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_I4       (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_I4            );
    /// <summary>
    /// 評価スタックの一番上の値を int64 に変換します。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_I8       (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_I8            );
    /// <summary>
    /// 評価スタックの一番上にある符号付きの値を符号付き native Int32 に変換し、オーバーフローについては System.OverflowExceptionをスローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_Ovf_I    (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_Ovf_I         );
    /// <summary>
    /// 評価スタックの一番上にある符号なしの値を符号付き native Int32 に変換し、オーバーフローについては System.OverflowExceptionをスローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_Ovf_I_Un (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_Ovf_I_Un      );
    /// <summary>
    /// 評価スタックの一番上にある符号付きの値を符号付き int8 に変換し、その値を int32 に拡張し、オーバーフローについては System.OverflowExceptionをスローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_Ovf_I1   (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_Ovf_I1        );
    /// <summary>
    /// 評価スタックの一番上にある符号なしの値を符号付き int8 に変換し、その値を int32 に拡張し、オーバーフローについては System.OverflowExceptionをスローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_Ovf_I1_Un(this ILGenerator I                  )=>I.Emit(OpCodes.Conv_Ovf_I1_Un     );
    /// <summary>
    /// 評価スタックの一番上にある符号付きの値を符号付き int16 に変換し、その値を int32 に拡張し、オーバーフローについては System.OverflowExceptionをスローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_Ovf_I2   (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_Ovf_I2        );
    /// <summary>
    /// 評価スタックの一番上にある符号なしの値を符号付き int16 に変換し、その値を int32 に拡張し、オーバーフローについては System.OverflowExceptionをスローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_Ovf_I2_Un(this ILGenerator I                  )=>I.Emit(OpCodes.Conv_Ovf_I2_Un     );
    /// <summary>
    /// 評価スタックの一番上にある符号付きの値を符号付き int32 に変換し、オーバーフローについては System.OverflowException をスローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_Ovf_I4   (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_Ovf_I4        );
    /// <summary>
    /// 評価スタックの一番上にある符号なしの値を符号付き int32 に変換し、オーバーフローについては System.OverflowException をスローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_Ovf_I4_Un(this ILGenerator I                  )=>I.Emit(OpCodes.Conv_Ovf_I4_Un     );
    /// <summary>
    /// 評価スタックの一番上にある符号付きの値を符号付き int64 に変換し、オーバーフローについては System.OverflowException をスローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_Ovf_I8   (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_Ovf_I8        );
    /// <summary>
    /// 評価スタックの一番上にある符号なしの値を符号付き int64 に変換し、オーバーフローについては System.OverflowException をスローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_Ovf_I8_Un(this ILGenerator I                  )=>I.Emit(OpCodes.Conv_Ovf_I8_Un     );
    /// <summary>
    /// 評価スタックの一番上にある符号付きの値を unsigned native Int32 に変換し、オーバーフローについては System.OverflowExceptionをスローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_Ovf_U    (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_Ovf_U         );
    /// <summary>
    /// 評価スタックの一番上にある符号なしの値を unsigned native Int32 に変換し、オーバーフローについては System.OverflowExceptionをスローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_Ovf_U_Un (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_Ovf_U_Un      );
    /// <summary>
    /// 評価スタックの一番上にある符号付きの値を unsigned int8 に変換し、その値を int32 に拡張し、オーバーフローについては System.OverflowExceptionをスローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_Ovf_U1   (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_Ovf_U1        );
    /// <summary>
    /// 評価スタックの一番上にある符号なしの値を unsigned int8 に変換し、その値を int32 に拡張し、オーバーフローについては System.OverflowExceptionをスローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_Ovf_U1_Un(this ILGenerator I                  )=>I.Emit(OpCodes.Conv_Ovf_U1_Un     );
    /// <summary>
    /// 評価スタックの一番上にある符号付きの値を unsigned int16 に変換し、その値を int32 に拡張し、オーバーフローについては System.OverflowExceptionをスローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_Ovf_U2   (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_Ovf_U2        );
    /// <summary>
    /// 評価スタックの一番上にある符号なしの値を unsigned int16 に変換し、その値を int32 に拡張し、オーバーフローについては System.OverflowExceptionをスローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_Ovf_U2_Un(this ILGenerator I                  )=>I.Emit(OpCodes.Conv_Ovf_U2_Un     );
    /// <summary>
    /// 評価スタックの一番上にある符号付きの値を unsigned int32 に変換し、オーバーフローについては System.OverflowExceptionをスローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_Ovf_U4   (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_Ovf_U4        );
    /// <summary>
    /// 評価スタックの一番上にある符号なしの値を unsigned int32 に変換し、オーバーフローについては System.OverflowExceptionをスローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_Ovf_U4_Un(this ILGenerator I                  )=>I.Emit(OpCodes.Conv_Ovf_U4_Un     );
    /// <summary>
    /// 評価スタックの一番上にある符号付きの値を unsigned int64 に変換し、オーバーフローについては System.OverflowExceptionをスローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_Ovf_U8   (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_Ovf_U8        );
    /// <summary>
    /// 評価スタックの一番上にある符号なしの値を unsigned int64 に変換し、オーバーフローについては System.OverflowExceptionをスローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_Ovf_U8_Un(this ILGenerator I                  )=>I.Emit(OpCodes.Conv_Ovf_U8_Un     );
    /// <summary>
    /// 評価スタックの一番上の符号なし整数値を float32 に変換します。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_R_Un     (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_R_Un          );
    /// <summary>
    /// 評価スタックの一番上の値を float32 に変換します。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_R4       (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_R4            );
    /// <summary>
    /// 評価スタックの一番上の値を float64 に変換します。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_R8       (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_R8            );
    /// <summary>
    /// 評価スタックの一番上の値を unsigned native Int32 に変換し、その値を native Int32 に拡張します。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_U        (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_U             );
    /// <summary>
    /// 評価スタックの一番上の値を unsigned int8 に変換し、その値を int32 に拡張します。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_U1       (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_U1            );
    /// <summary>
    /// 評価スタックの一番上の値を unsigned int16 に変換し、その値を int32 に拡張します。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_U2       (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_U2            );
    /// <summary>
    /// 評価スタックの一番上の値を unsigned int32 に変換し、その値を int32 に拡張します。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_U4       (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_U4            );
    /// <summary>
    /// 評価スタックの一番上の値を unsigned int64 に変換し、その値を int64 に拡張します。
    /// </summary>
    /// <param name="I"></param>
    public static void Conv_U8       (this ILGenerator I                  )=>I.Emit(OpCodes.Conv_U8            );
    /// <summary>
    /// ソース アドレスから指定した数のバイトを宛先アドレスにコピーします。
    /// </summary>
    /// <param name="I"></param>
    public static void Cpblk         (this ILGenerator I                  )=>I.Emit(OpCodes.Cpblk              );
    /// <summary>
    /// オブジェクトのアドレス (&amp;、*、または native Int32 の各型) にある値型をコピー先のオブジェクトのアドレス (&amp;、*、または native Int32の各型) にコピーします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="T"></param>
    public static void Cpobj         (this ILGenerator I,Type T           )=>I.Emit(OpCodes.Cpobj        ,T    );
    /// <summary>
    /// 2 つの値の除算を実行し、結果を浮動小数点値 (F 型) または商 (int32 型) として評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Div           (this ILGenerator I                  )=>I.Emit(OpCodes.Div                );
    /// <summary>
    /// 2 つの符号なし整数値を除算し、結果 (int32) を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Div_Un        (this ILGenerator I                  )=>I.Emit(OpCodes.Div_Un             );
    /// <summary>
    /// 現在評価スタックの一番上にある値をコピーし、そのコピーを評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Dup           (this ILGenerator I                  )=>I.Emit(OpCodes.Dup                );
    /// <summary>
    /// 例外の filter 句から共通言語基盤 (CLI) 例外ハンドラーに制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    public static void Endfilter     (this ILGenerator I                  )=>I.Emit(OpCodes.Endfilter          );
    /// <summary>
    /// 例外ブロックの fault 句または finally 句から共通言語基盤 (CLI) 例外ハンドラーに制御を転送します。
    /// </summary>
    /// <param name="I"></param>
    public static void Endfinally    (this ILGenerator I                  )=>I.Emit(OpCodes.Endfinally         );
    /// <summary>
    /// 特定のアドレスの指定したメモリ ブロックを、指定のサイズと初期値に初期化します。
    /// </summary>
    /// <param name="I"></param>
    public static void Initblk       (this ILGenerator I                  )=>I.Emit(OpCodes.Initblk            );
    /// <summary>
    /// 指定したアドレスにある値型の各フィールドを null 参照または適切なプリミティブ型の 0 に初期化します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="T"></param>
    public static void Initobj       (this ILGenerator I,Type T           )=>I.Emit(OpCodes.Initobj,T          );
    /// <summary>
    /// オブジェクト参照 (O 型) が特定のクラスのインスタンスかどうかをテストします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="T"></param>
    public static void Isinst        (this ILGenerator I,Type T           )=>I.Emit(OpCodes.Isinst,T           );
    /// <summary>
    /// 現在のメソッドを終了し、指定したメソッドにジャンプします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="M"></param>
    public static void Jmp           (this ILGenerator I,MethodInfo M     )=>I.Emit(OpCodes.Jmp   ,M           );
    /// <summary>
    /// 指定したインデックス値によって参照される引数をスタックに読み込みます。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="arg"></param>
    public static void Ldarg(this ILGenerator I,ushort arg){
        switch(arg){
            case 0:
                I.Emit(OpCodes.Ldarg_0);
                break;
            case 1:
                I.Emit(OpCodes.Ldarg_1);
                break;
            case 2:
                I.Emit(OpCodes.Ldarg_2);
                break;
            case 3:
                I.Emit(OpCodes.Ldarg_3);
                break;
            default:
                I.Emit(arg<=255?OpCodes.Ldarg_S:OpCodes.Ldarg,arg);
                break;
        }
    }
    /// <summary>
    /// インデックス 0 の引数を評価スタックに読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldarg_0       (this ILGenerator I                  )=>I.Emit(OpCodes.Ldarg_0            );
    /// <summary>
    /// インデックス 1 の引数を評価スタックに読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldarg_1       (this ILGenerator I                  )=>I.Emit(OpCodes.Ldarg_1            );
    /// <summary>
    /// インデックス 2 の引数を評価スタックに読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldarg_2       (this ILGenerator I                  )=>I.Emit(OpCodes.Ldarg_2            );
    /// <summary>
    /// インデックス 3 の引数を評価スタックに読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldarg_3       (this ILGenerator I                  )=>I.Emit(OpCodes.Ldarg_3            );
    /// <summary>
    /// 指定した短い形式のインデックスによって参照される引数を評価スタックに読み込みます。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="arg"></param>
    public static void Ldarg_S       (this ILGenerator I,byte arg         )=>I.Emit(OpCodes.Ldarg_S      ,arg  );
    /// <summary>
    /// 引数アドレスを評価スタックに読み込みます。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="arg"></param>

    public static void Ldarga        (this ILGenerator I,ushort arg       )=>I.Emit(
        arg<=255
            ?OpCodes.Ldarga_S
            :OpCodes.Ldarga,arg
    );
    /// <summary>
    /// 引数アドレス (短い形式) を評価スタックに読み込みます。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="arg"></param>
    public static void Ldarga_S      (this ILGenerator I,byte arg         )=>I.Emit(OpCodes.Ldarga_S     ,arg  );
    /// <summary>
    /// 提供された int32 型の値を int32 として評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="値"></param>
    public static void Ldc_I4        (this ILGenerator I,int 値         ){
        switch(値){
            case -1:
                I.Emit(OpCodes.Ldc_I4_M1);
                break;
            case 0:
                I.Emit(OpCodes.Ldc_I4_0);
                break;
            case 1:
                I.Emit(OpCodes.Ldc_I4_1);
                break;
            case 2:
                I.Emit(OpCodes.Ldc_I4_2);
                break;
            case 3:
                I.Emit(OpCodes.Ldc_I4_3);
                break;
            case 4:
                I.Emit(OpCodes.Ldc_I4_4);
                break;
            case 5:
                I.Emit(OpCodes.Ldc_I4_5);
                break;
            case 6:
                I.Emit(OpCodes.Ldc_I4_6);
                break;
            case 7:
                I.Emit(OpCodes.Ldc_I4_7);
                break;
            case 8:
                I.Emit(OpCodes.Ldc_I4_8);
                break;
            default:
                if(-128<=値&& 値<=127)
                    I.Emit(OpCodes.Ldc_I4_S,(sbyte)値);
                else
                    I.Emit(OpCodes.Ldc_I4,値);
                break;
        }
    }
    /// <summary>
    /// 整数値 0 を int32 として評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldc_I4_0      (this ILGenerator I                  )=>I.Emit(OpCodes.Ldc_I4_0           );
    /// <summary>
    /// 整数値 1 を int32 として評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldc_I4_1      (this ILGenerator I                  )=>I.Emit(OpCodes.Ldc_I4_1           );
    /// <summary>
    /// 整数値 2 を int32 として評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldc_I4_2      (this ILGenerator I                  )=>I.Emit(OpCodes.Ldc_I4_2           );
    /// <summary>
    /// 整数値 3 を int32 として評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldc_I4_3      (this ILGenerator I                  )=>I.Emit(OpCodes.Ldc_I4_3           );
    /// <summary>
    /// 整数値 4 を int32 として評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldc_I4_4      (this ILGenerator I                  )=>I.Emit(OpCodes.Ldc_I4_4           );
    /// <summary>
    /// 整数値 5 を int32 として評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldc_I4_5      (this ILGenerator I                  )=>I.Emit(OpCodes.Ldc_I4_5           );
    /// <summary>
    /// 整数値 6 を int32 として評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldc_I4_6      (this ILGenerator I                  )=>I.Emit(OpCodes.Ldc_I4_6           );
    /// <summary>
    /// 整数値 7 を int32 として評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldc_I4_7      (this ILGenerator I                  )=>I.Emit(OpCodes.Ldc_I4_7           );
    /// <summary>
    /// 整数値 8 を int32 として評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldc_I4_8      (this ILGenerator I                  )=>I.Emit(OpCodes.Ldc_I4_8           );
    /// <summary>
    /// 整数値 -1 を int32 として評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldc_I4_M1     (this ILGenerator I                  )=>I.Emit(OpCodes.Ldc_I4_M1          );
    /// <summary>
    ///  提供された int8 値を int32 として評価スタックにプッシュします (短い形式)。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="値"></param>
    public static void Ldc_I4_S      (this ILGenerator I,sbyte 値         )=>I.Emit(OpCodes.Ldc_I4_S      ,値  );
    /// <summary>
    /// 提供された int64 型の値を int64 として評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="値"></param>
    public static void Ldc_I8        (this ILGenerator I,long 値         )=>I.Emit(OpCodes.Ldc_I8,値          );
    /// <summary>
    /// 提供された float32 型の値を F (float) 型として評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="値"></param>
    public static void Ldc_R4        (this ILGenerator I,float 値        )=>I.Emit(OpCodes.Ldc_R4,値          );
    /// <summary>
    /// 提供された float64 型の値を F (float) 型として評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="値"></param>
    public static void Ldc_R8        (this ILGenerator I,double 値        )=>I.Emit(OpCodes.Ldc_R8,値          );
    /// <summary>
    /// 指定した配列インデックスの要素を命令で指定された型として評価スタックの一番上に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="T"></param>
    public static void Ldelem        (this ILGenerator I,Type T           ){
        if(T==typeof(IntPtr))
            I.Emit(OpCodes.Ldelem_I);
        else if(T==typeof(sbyte))
            I.Emit(OpCodes.Ldelem_I1);
        else if(T==typeof(short))
            I.Emit(OpCodes.Ldelem_I2);
        else if(T==typeof(int))
            I.Emit(OpCodes.Ldelem_I4);
        else if(T==typeof(long))
            I.Emit(OpCodes.Ldelem_I8);
        else if(T==typeof(float))
            I.Emit(OpCodes.Ldelem_R4);
        else if(T==typeof(double))
            I.Emit(OpCodes.Ldelem_R8);
        else if(T.IsByRef)
            I.Emit(OpCodes.Ldelem_Ref);
        else if(T==typeof(byte))
            I.Emit(OpCodes.Ldelem_U1);
        else if(T==typeof(ushort))
            I.Emit(OpCodes.Ldelem_U2);
        else if(T==typeof(uint))
            I.Emit(OpCodes.Ldelem_U4);
        else
            I.Emit(OpCodes.Ldelem,T);
    }
    /// <summary>
    /// 指定した配列インデックスの native Int32 型の要素を native Int32 として評価スタックの一番上に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldelem_I      (this ILGenerator I                  )=>I.Emit(OpCodes.Ldelem_I           );
    /// <summary>
    /// 指定した配列インデックスの int8 型の要素を int32 として評価スタックの一番上に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldelem_I1     (this ILGenerator I                  )=>I.Emit(OpCodes.Ldelem_I1          );
    /// <summary>
    /// 指定した配列インデックスの int16 型の要素を int32 として評価スタックの一番上に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldelem_I2     (this ILGenerator I                  )=>I.Emit(OpCodes.Ldelem_I2          );
    /// <summary>
    /// 指定した配列インデックスの int32 型の要素を int32 として評価スタックの一番上に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldelem_I4     (this ILGenerator I                  )=>I.Emit(OpCodes.Ldelem_I4          );
    /// <summary>
    /// 指定した配列インデックスの int64 型の要素を int64 として評価スタックの一番上に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldelem_I8     (this ILGenerator I                  )=>I.Emit(OpCodes.Ldelem_I8          );
    /// <summary>
    /// 指定した配列インデックスの float32 型の要素を F (float) 型として評価スタックの一番上に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldelem_R4     (this ILGenerator I                  )=>I.Emit(OpCodes.Ldelem_R4          );
    /// <summary>
    /// 指定した配列インデックスの float64 型の要素を F (float) 型として評価スタックの一番上に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldelem_R8     (this ILGenerator I                  )=>I.Emit(OpCodes.Ldelem_R8          );
    /// <summary>
    /// 指定した配列インデックスのオブジェクト参照を格納している要素を O 型 (オブジェクト参照) として評価スタックの一番上に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldelem_Ref    (this ILGenerator I                  )=>I.Emit(OpCodes.Ldelem_Ref         );
    /// <summary>
    /// 指定した配列インデックスの unsigned int8 型の要素を int32 として評価スタックの一番上に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldelem_U1     (this ILGenerator I                  )=>I.Emit(OpCodes.Ldelem_U1          );
    /// <summary>
    /// 指定した配列インデックスの unsigned int16 型の要素を int32 として評価スタックの一番上に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldelem_U2     (this ILGenerator I                  )=>I.Emit(OpCodes.Ldelem_U2          );
    /// <summary>
    /// 指定した配列インデックスの unsigned int32 型の要素を int32 として評価スタックの一番上に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldelem_U4     (this ILGenerator I                  )=>I.Emit(OpCodes.Ldelem_U4          );
    /// <summary>
    /// 指定した配列インデックスにある配列要素のアドレスを &amp; 型 (マネージ ポインター) として評価スタックの一番上に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="T"></param>
    public static void Ldelema       (this ILGenerator I,Type T           )=>I.Emit(OpCodes.Ldelema      ,T    );
    /// <summary>
    /// 参照が現在評価スタック上にあるオブジェクト内のフィールドの値を検索します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="F"></param>
    public static void Ldfld         (this ILGenerator I,FieldInfo F      )=>I.Emit(OpCodes.Ldfld        ,F    );
    /// <summary>
    /// 参照が現在評価スタック上にあるオブジェクト内のフィールドのアドレスを検索します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="F"></param>
    public static void Ldflda        (this ILGenerator I,FieldInfo F      )=>I.Emit(OpCodes.Ldflda       ,F    );
    /// <summary>
    /// 特定のメソッドを実装しているネイティブ コードへのアンマネージ ポインター (native Int32 型) を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="M"></param>
    public static void Ldftn         (this ILGenerator I,MethodInfo M     )=>I.Emit(OpCodes.Ldftn        ,M    );
    /// <summary>
    /// native Int32 型の値を native Int32 として評価スタックに間接的に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldind_I       (this ILGenerator I                  )=>I.Emit(OpCodes.Ldind_I            );
    /// <summary>
    /// int8 型の値を int32 として評価スタックに間接的に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldind_I1      (this ILGenerator I                  )=>I.Emit(OpCodes.Ldind_I1           );
    /// <summary>
    /// int16 型の値を int32 として評価スタックに間接的に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldind_I2      (this ILGenerator I                  )=>I.Emit(OpCodes.Ldind_I2           );
    /// <summary>
    /// int32 型の値を int32 として評価スタックに間接的に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldind_I4      (this ILGenerator I                  )=>I.Emit(OpCodes.Ldind_I4           );
    /// <summary>
    /// int64 型の値を int64 として評価スタックに間接的に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldind_I8      (this ILGenerator I                  )=>I.Emit(OpCodes.Ldind_I8           );
    /// <summary>
    /// float32 型の値を F (float) 型として評価スタックに間接的に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldind_R4      (this ILGenerator I                  )=>I.Emit(OpCodes.Ldind_R4           );
    /// <summary>
    /// float64 型の値を F (float) 型として評価スタックに間接的に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldind_R8      (this ILGenerator I                  )=>I.Emit(OpCodes.Ldind_R8           );
    /// <summary>
    /// オブジェクト参照を O 型 (オブジェクト参照) として評価スタックに間接的に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldind_Ref     (this ILGenerator I                  )=>I.Emit(OpCodes.Ldind_Ref          );
    /// <summary>
    /// unsigned int8 型の値を int32 として評価スタックに間接的に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldind_U1      (this ILGenerator I                  )=>I.Emit(OpCodes.Ldind_U1           );
    /// <summary>
    /// unsigned int16 型の値を int32 として評価スタックに間接的に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldind_U2      (this ILGenerator I                  )=>I.Emit(OpCodes.Ldind_U2           );
    /// <summary>
    /// unsigned int32 型の値を int32 として評価スタックに間接的に読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldind_U4      (this ILGenerator I                  )=>I.Emit(OpCodes.Ldind_U4           );
    /// <summary>
    /// インデックス番号が 0 から始まる 1 次元配列の要素数を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldlen         (this ILGenerator I                  )=>I.Emit(OpCodes.Ldlen              );
    /// <summary>
    /// 特定のインデックスのローカル変数を評価スタックに読み込みます。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="L"></param>
    public static void Ldloc         (this ILGenerator I,LocalBuilder L   ){
        switch(L.LocalIndex){
            case 0:
                I.Emit(OpCodes.Ldloc_0);
                break;
            case 1:
                I.Emit(OpCodes.Ldloc_1);
                break;
            case 2:
                I.Emit(OpCodes.Ldloc_2);
                break;
            case 3:
                I.Emit(OpCodes.Ldloc_3);
                break;
            default:
                I.Emit(L.LocalIndex<=255?OpCodes.Ldloc_S:OpCodes.Ldloc,L);
                break;
        }
    }
    /// <summary>
    /// インデックス 0 のローカル変数を評価スタックに読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldloc_0       (this ILGenerator I                  )=>I.Emit(OpCodes.Ldloc_0            );
    /// <summary>
    /// インデックス 1 のローカル変数を評価スタックに読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldloc_1       (this ILGenerator I                  )=>I.Emit(OpCodes.Ldloc_1            );
    /// <summary>
    /// インデックス 2 のローカル変数を評価スタックに読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldloc_2       (this ILGenerator I                  )=>I.Emit(OpCodes.Ldloc_2            );
    /// <summary>
    /// インデックス 3 のローカル変数を評価スタックに読み込みます。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldloc_3       (this ILGenerator I                  )=>I.Emit(OpCodes.Ldloc_3            );
    /// <summary>
    /// 特定のインデックスのローカル変数を評価スタックに読み込みます (短い形式)。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="L"></param>
    public static void Ldloc_S       (this ILGenerator I,LocalBuilder L   )=>I.Emit(OpCodes.Ldloc_S      ,L    );
    /// <summary>
    /// 特定のインデックスのローカル変数のアドレスを評価スタックに読み込みます。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="L"></param>
    public static void Ldloca        (this ILGenerator I,LocalBuilder L   )=>I.Emit(
        L.LocalIndex<=255
            ?OpCodes.Ldloca_S
            :OpCodes.Ldloca,
        L
    );
    /// <summary>
    /// 特定のインデックスのローカル変数のアドレスを評価スタックに読み込みます (短い形式)。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="L"></param>
    public static void Ldloca_S      (this ILGenerator I,LocalBuilder L  )=> I.Emit(OpCodes.Ldloca_S,L         );
    /// <summary>
    /// null 参照 (O 型) を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldnull        (this ILGenerator I                  )=>I.Emit(OpCodes.Ldnull             );

    /// <summary>
    /// アドレスが指す値型オブジェクトを評価スタックの一番上にコピーします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="T"></param>
    public static void Ldobj(this ILGenerator I,Type T){
        if(T==typeof(IntPtr))
            I.Emit(OpCodes.Ldind_I);
        else if(T==typeof(sbyte))
            I.Emit(OpCodes.Ldind_I1);
        else if(T==typeof(short))
            I.Emit(OpCodes.Ldind_I2);
        else if(T==typeof(int))
            I.Emit(OpCodes.Ldind_I4);
        else if(T==typeof(long))
            I.Emit(OpCodes.Ldind_I8);
        else if(T==typeof(float))
            I.Emit(OpCodes.Ldind_R4);
        else if(T==typeof(double))
            I.Emit(OpCodes.Ldind_R8);
        else if(T.IsByRef)
            I.Emit(OpCodes.Ldind_Ref);
        else if(T==typeof(byte))
            I.Emit(OpCodes.Ldind_U1);
        else if(T==typeof(ushort))
            I.Emit(OpCodes.Ldind_U2);
        else if(T==typeof(uint))
            I.Emit(OpCodes.Ldind_U4);
        else
            I.Emit(OpCodes.Ldobj,T);
    }

    /// <summary>
    /// 静的フィールドの値を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="F"></param>
    public static void Ldsfld        (this ILGenerator I,FieldInfo F      )=>I.Emit(OpCodes.Ldsfld       ,F    );
    /// <summary>
    /// 静的フィールドのアドレスを評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="F"></param>
    public static void Ldsflda       (this ILGenerator I,FieldInfo F      )=>I.Emit(OpCodes.Ldsflda      ,F    );
    /// <summary>
    /// メタデータに格納されているリテラル文字列への新しいオブジェクト参照をプッシュします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="S"></param>
    public static void Ldstr         (this ILGenerator I,string S         )=>I.Emit(OpCodes.Ldstr        ,S    );
    /// <summary>
    /// ConstructorInfoメタデータ トークンをそのランタイム表現に変換し、評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="C"></param>
    public static void Ldtoken       (this ILGenerator I,ConstructorInfo C)=>I.Emit(OpCodes.Ldtoken      ,C    );
    /// <summary>
    /// MethodInfoメタデータ トークンをそのランタイム表現に変換し、評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="M"></param>
    public static void Ldtoken       (this ILGenerator I,MethodInfo M     )=>I.Emit(OpCodes.Ldtoken      ,M    );
    /// <summary>
    /// FieldInfoメタデータ トークンをそのランタイム表現に変換し、評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="F"></param>
    public static void Ldtoken       (this ILGenerator I,FieldInfo F      )=>I.Emit(OpCodes.Ldtoken      ,F    );
    /// <summary>
    /// Typeメタデータ トークンをそのランタイム表現に変換し、評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="T"></param>
    public static void Ldtoken       (this ILGenerator I,Type T           )=>I.Emit(OpCodes.Ldtoken      ,T    );
    /// <summary>
    /// 指定したオブジェクトに関連付けられた特定の仮想メソッドを実装しているネイティブ コードへのアンマネージ ポインター (native Int32 型) を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Ldvirtftn     (this ILGenerator I                  )=>I.Emit(OpCodes.Ldvirtftn          );
    /// <summary>
    /// コードの保護領域を終了し、制御を特定のターゲット命令に無条件で転送します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Leave         (this ILGenerator I,Label Label      )=>B_S命令かB命令か(I,Label,OpCodes.Leave_S,OpCodes.Leave);
    /// <summary>
    /// コードの保護領域を終了し、制御をターゲット命令に無条件で転送します (短い形式)。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Leave_S       (this ILGenerator I,Label Label      )=>I.Emit(OpCodes.Leave_S      ,Label);
    /// <summary>
    /// ローカル動的メモリ プールから特定のバイト数を割り当て、最初に割り当てたバイトのアドレス (一時ポインター、* 型) を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Localloc      (this ILGenerator I                  )=>I.Emit(OpCodes.Localloc           );
    /// <summary>
    /// 特定の型のインスタンスへの型指定された参照を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Type"></param>
    public static void Mkrefany      (this ILGenerator I,Type Type        )=>I.Emit(OpCodes.Mkrefany,Type      );
    /// <summary>
    /// 2 つの値を乗算し、結果を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Mul           (this ILGenerator I                  )=>I.Emit(OpCodes.Mul                );
    /// <summary>
    /// 2 つの整数値を乗算し、オーバーフロー チェックを実行し、結果を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Mul_Ovf       (this ILGenerator I                  )=>I.Emit(OpCodes.Mul_Ovf            );
    /// <summary>
    /// 2 つの符号なし整数値を乗算し、オーバーフロー チェックを実行し、結果を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Mul_Ovf_Un    (this ILGenerator I                  )=>I.Emit(OpCodes.Mul_Ovf_Un         );
    /// <summary>
    /// 値を無効にし、結果を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Neg           (this ILGenerator I                  )=>I.Emit(OpCodes.Neg                );
    /// <summary>
    /// 特定の型の要素を持つ、インデックス番号が 0 から始まる新しい 1 次元配列へのオブジェクト参照を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="T"></param>
    public static void Newarr        (this ILGenerator I,Type T           )=>I.Emit(OpCodes.Newarr       ,T    );
    /// <summary>
    /// 新しいオブジェクトまたは値型の新しいインスタンスを作成し、オブジェクト参照 (O 型) を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="C"></param>
    public static void Newobj        (this ILGenerator I,ConstructorInfo C)=>I.Emit(OpCodes.Newobj       ,C    );
    /// <summary>
    /// オペコードがパッチされている場合は、領域を補完します。循環参照の処理を利用することはできますが、意味のある演算は実行されません。
    /// </summary>
    /// <param name="I"></param>
    public static void Nop           (this ILGenerator I                  )=>I.Emit(OpCodes.Nop                );
    /// <summary>
    /// スタックの一番上にある整数値のビットごとの補数を計算し、結果を同じ型として評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Not           (this ILGenerator I                  )=>I.Emit(OpCodes.Not                );
    /// <summary>
    /// スタックの一番上にある 2 つの整数値のビットごとの補数を計算し、結果を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Or            (this ILGenerator I                  )=>I.Emit(OpCodes.Or                 );
    /// <summary>
    /// 現在評価スタックの一番上にある値を削除します。
    /// </summary>
    /// <param name="I"></param>
    public static void Pop           (this ILGenerator I                  )=>I.Emit(OpCodes.Pop                );
    /// <summary>
    /// 以降の配列アドレス演算で、実行時に型チェックを実行しないこと、および変更可能性が制限されたマネージ ポインターを返すことを指定します。
    /// </summary>
    /// <param name="I"></param>
    public static void Readonly      (this ILGenerator I                  )=>I.Emit(OpCodes.Readonly           );
    /// <summary>
    /// 型指定された参照に埋め込まれている型トークンを取得します。
    /// </summary>
    /// <param name="I"></param>
    public static void Refanytype    (this ILGenerator I                  )=>I.Emit(OpCodes.Refanytype         );
    /// <summary>
    /// 型指定された参照に埋め込まれているアドレス (&amp; 型) を取得します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="T"></param>
    public static void Refanyval     (this ILGenerator I,Type T           )=>I.Emit(OpCodes.Refanyval,T        );
    /// <summary>
    /// 2 つの値を除算し、剰余を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Rem           (this ILGenerator I                  )=>I.Emit(OpCodes.Rem                );
    /// <summary>
    /// 2 つの符号なしの値を除算し、剰余を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Rem_Un        (this ILGenerator I                  )=>I.Emit(OpCodes.Rem_Un             );
    /// <summary>
    /// 現在のメソッドから戻り、呼び出し先の評価スタックから呼び出し元の評価スタックに戻り値 (存在する場合) をプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Ret           (this ILGenerator I                  )=>I.Emit(OpCodes.Ret                );
    /// <summary>
    /// 現在の例外を再スローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Rethrow       (this ILGenerator I                  )=>I.Emit(OpCodes.Rethrow            );
    /// <summary>
    /// 整数値を指定したビット数だけ、0 を使用して左にシフトし、結果を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Shl           (this ILGenerator I                  )=>I.Emit(OpCodes.Shl                );
    /// <summary>
    /// 整数値を指定したビット数だけ、符号を付けて右にシフトし、結果を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Shr           (this ILGenerator I                  )=>I.Emit(OpCodes.Shr                );
    /// <summary>
    /// 符号なし整数値を指定したビット数だけ、0 を使用して右にシフトし、結果を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Shr_Un        (this ILGenerator I                  )=>I.Emit(OpCodes.Shr_Un             );
    /// <summary>
    /// 提供された値型のサイズ (バイト単位) を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="T"></param>
    public static void Sizeof        (this ILGenerator I,Type T           )=>I.Emit(OpCodes.Sizeof       ,T    );
    /// <summary>
    /// 評価スタックの一番上にある値を指定したインデックスの引数スロットに格納します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="arg"></param>
    public static void Starg         (this ILGenerator I,ushort arg       )=>I.Emit(
        arg<=255
            ?OpCodes.Starg_S
            :OpCodes.Starg
        ,arg
    );
    /// <summary>
    /// 評価スタックの一番上にある値を指定したインデックスの引数スロットに格納します (短い形式)。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="arg"></param>
    public static void Starg_S       (this ILGenerator I,byte arg         )=>I.Emit(OpCodes.Starg_S      ,arg  );

    /// <summary>
    /// 指定のインデックス位置にある配列要素を評価スタックの、命令で指定された型の値に置き換えます。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="T"></param>
    public static void Stelem(this ILGenerator I,Type T){
        if(T==typeof(IntPtr)||T==typeof(UIntPtr))
            I.Emit(OpCodes.Stelem_I);
        else if(T==typeof(sbyte)||T==typeof(byte))
            I.Emit(OpCodes.Stelem_I1);
        else if(T==typeof(short)||T==typeof(ushort))
            I.Emit(OpCodes.Stelem_I2);
        else if(T==typeof(int)||T==typeof(uint))
            I.Emit(OpCodes.Stelem_I4);
        else if(T==typeof(long)||T==typeof(ulong))
            I.Emit(OpCodes.Stelem_I8);
        else if(T==typeof(float))
            I.Emit(OpCodes.Stelem_R4);
        else if(T==typeof(double))
            I.Emit(OpCodes.Stelem_R8);
        else if(T.IsByRef)
            I.Emit(OpCodes.Stelem_Ref);
        else
            I.Emit(OpCodes.Stelem,T);
    }

    /// <summary>
    /// 指定のインデックス位置にある配列要素を評価スタックの native Int32 値に置き換えます。
    /// </summary>
    /// <param name="I"></param>
    public static void Stelem_I      (this ILGenerator I                  )=>I.Emit(OpCodes.Stelem_I           );
    /// <summary>
    /// 指定のインデックス位置にある配列要素を評価スタックの int8 値に置き換えます。
    /// </summary>
    /// <param name="I"></param>
    public static void Stelem_I1     (this ILGenerator I                  )=>I.Emit(OpCodes.Stelem_I1          );
    /// <summary>
    /// 指定のインデックス位置にある配列要素を評価スタックの int16 値に置き換えます。
    /// </summary>
    /// <param name="I"></param>
    public static void Stelem_I2     (this ILGenerator I                  )=>I.Emit(OpCodes.Stelem_I2          );
    /// <summary>
    /// 指定のインデックス位置にある配列要素を評価スタックの int32 値に置き換えます。
    /// </summary>
    /// <param name="I"></param>
    public static void Stelem_I4     (this ILGenerator I                  )=>I.Emit(OpCodes.Stelem_I4          );
    /// <summary>
    /// 指定のインデックス位置にある配列要素を評価スタックの int64 値に置き換えます。
    /// </summary>
    /// <param name="I"></param>
    public static void Stelem_I8     (this ILGenerator I                  )=>I.Emit(OpCodes.Stelem_I8          );
    /// <summary>
    /// 指定のインデックス位置にある配列要素を評価スタックの float32 値に置き換えます。
    /// </summary>
    /// <param name="I"></param>
    public static void Stelem_R4     (this ILGenerator I                  )=>I.Emit(OpCodes.Stelem_R4          );
    /// <summary>
    /// 指定のインデックス位置にある配列要素を評価スタックの float64 値に置き換えます。
    /// </summary>
    /// <param name="I"></param>
    public static void Stelem_R8     (this ILGenerator I                  )=>I.Emit(OpCodes.Stelem_R8          );
    /// <summary>
    /// 指定のインデックス位置にある配列要素をオブジェクト参照値 (O 型) に置き換えます。
    /// </summary>
    /// <param name="I"></param>
    public static void Stelem_Ref    (this ILGenerator I                  )=>I.Emit(OpCodes.Stelem_Ref         );
    /// <summary>
    /// オブジェクト参照またはポインターのフィールドに格納された値を新しい値に置き換えます。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="F"></param>
    public static void Stfld         (this ILGenerator I,FieldInfo F      )=>I.Emit(OpCodes.Stfld,F            );
    /// <summary>
    /// 提供されたアドレスに native Int32 型の値を格納します。
    /// </summary>
    /// <param name="I"></param>
    public static void Stind_I       (this ILGenerator I                  )=>I.Emit(OpCodes.Stind_I            );
    /// <summary>
    /// 提供されたアドレスに int8 型の値を格納します。
    /// </summary>
    /// <param name="I"></param>
    public static void Stind_I1      (this ILGenerator I                  )=>I.Emit(OpCodes.Stind_I1           );
    /// <summary>
    /// 提供されたアドレスに int16 型の値を格納します。
    /// </summary>
    /// <param name="I"></param>
    public static void Stind_I2      (this ILGenerator I                  )=>I.Emit(OpCodes.Stind_I2           );
    /// <summary>
    /// 提供されたアドレスに int32 型の値を格納します。
    /// </summary>
    /// <param name="I"></param>
    public static void Stind_I4      (this ILGenerator I                  )=>I.Emit(OpCodes.Stind_I4           );
    /// <summary>
    /// 提供されたアドレスに int64 型の値を格納します。
    /// </summary>
    /// <param name="I"></param>
    public static void Stind_I8      (this ILGenerator I                  )=>I.Emit(OpCodes.Stind_I8           );
    /// <summary>
    /// 提供されたアドレスに float32 型の値を格納します。
    /// </summary>
    /// <param name="I"></param>
    public static void Stind_R4      (this ILGenerator I                  )=>I.Emit(OpCodes.Stind_R4           );
    /// <summary>
    /// 提供されたアドレスに float64 型の値を格納します。
    /// </summary>
    /// <param name="I"></param>
    public static void Stind_R8      (this ILGenerator I                  )=>I.Emit(OpCodes.Stind_R8           );
    /// <summary>
    /// 提供されたアドレスにオブジェクト参照値を格納します。
    /// </summary>
    /// <param name="I"></param>
    public static void Stind_Ref     (this ILGenerator I                  )=>I.Emit(OpCodes.Stind_Ref          );
    /// <summary>
    /// 評価スタックの一番上から現在の値をポップし、指定したインデックスのローカル変数リストに格納します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="L"></param>
    public static void Stloc         (this ILGenerator I,LocalBuilder L   ){
        switch(L.LocalIndex){
            case 0:
                I.Emit(OpCodes.Stloc_0);
                break;
            case 1:
                I.Emit(OpCodes.Stloc_1);
                break;
            case 2:
                I.Emit(OpCodes.Stloc_2);
                break;
            case 3:
                I.Emit(OpCodes.Stloc_3);
                break;
            default:
                I.Emit(L.LocalIndex<=255?OpCodes.Stloc_S:OpCodes.Stloc,L);
                break;
        }
    }
    /// <summary>
    /// 評価スタックの一番上から現在の値をポップし、インデックス 0 のローカル変数リストに格納します。
    /// </summary>
    /// <param name="I"></param>
    public static void Stloc_0       (this ILGenerator I                  )=>I.Emit(OpCodes.Stloc_0            );
    /// <summary>
    /// 評価スタックの一番上から現在の値をポップし、インデックス 1 のローカル変数リストに格納します。
    /// </summary>
    /// <param name="I"></param>
    public static void Stloc_1       (this ILGenerator I                  )=>I.Emit(OpCodes.Stloc_1            );
    /// <summary>
    /// 評価スタックの一番上から現在の値をポップし、インデックス 2 のローカル変数リストに格納します。
    /// </summary>
    /// <param name="I"></param>
    public static void Stloc_2       (this ILGenerator I                  )=>I.Emit(OpCodes.Stloc_2            );
    /// <summary>
    /// 評価スタックの一番上から現在の値をポップし、インデックス 3 のローカル変数リストに格納します。
    /// </summary>
    /// <param name="I"></param>
    public static void Stloc_3       (this ILGenerator I                  )=>I.Emit(OpCodes.Stloc_3            );
    /// <summary>
    /// 評価スタックの一番上から現在の値をポップし、index のローカル変数リストに格納します (短い形式)。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="L"></param>
    public static void Stloc_S       (this ILGenerator I,LocalBuilder L   )=>I.Emit(OpCodes.Stloc        ,L    );

    /// <summary>
    /// 評価スタックから提供されたメモリ アドレスに、指定した型の値をコピーします。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="T"></param>
    public static void Stobj(this ILGenerator I,Type T){
        if(T==typeof(IntPtr)||T==typeof(UIntPtr))
            I.Emit(OpCodes.Stind_I);
        else if(T==typeof(sbyte)||T==typeof(byte))
            I.Emit(OpCodes.Stind_I1);
        else if(T==typeof(short)||T==typeof(ushort))
            I.Emit(OpCodes.Stind_I2);
        else if(T==typeof(int)||T==typeof(uint))
            I.Emit(OpCodes.Stind_I4);
        else if(T==typeof(long)||T==typeof(ulong))
            I.Emit(OpCodes.Stind_I8);
        else if(T==typeof(float))
            I.Emit(OpCodes.Stind_R4);
        else if(T==typeof(double))
            I.Emit(OpCodes.Stind_R8);
        else if(T.IsByRef)
            I.Emit(OpCodes.Stind_Ref);
        else
            I.Emit(OpCodes.Stobj,T);
    }

    /// <summary>
    /// 静的フィールドの値を評価スタックの値に置き換えます。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="F"></param>
    public static void Stsfld        (this ILGenerator I,FieldInfo F      )=>I.Emit(OpCodes.Stsfld       ,F    );
    /// <summary>
    /// ある値から別の値を減算し、結果を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Sub           (this ILGenerator I                  )=>I.Emit(OpCodes.Sub                );
    /// <summary>
    /// ある整数値を別の整数値から減算し、オーバーフロー チェックを実行し、結果を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Sub_Ovf       (this ILGenerator I                  )=>I.Emit(OpCodes.Sub_Ovf            );
    /// <summary>
    /// ある符号なし整数値を別の符号なし整数値から減算し、オーバーフロー チェックを実行し、結果を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Sub_Ovf_Un    (this ILGenerator I                  )=>I.Emit(OpCodes.Sub_Ovf_Un         );
    /// <summary>
    /// ジャンプ テーブルを実装します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Labels"></param>
    public static void Switch        (this ILGenerator I,Label[] Labels   )=>I.Emit(OpCodes.Switch       ,Labels);
    /// <summary>
    /// 実際の呼び出し命令が実行される前に、現在のメソッドのスタック フレームが削除されるように、後置のメソッド呼び出し命令を実行します。
    /// </summary>
    /// <param name="I"></param>
    public static void Tailcall      (this ILGenerator I                  )=>I.Emit(OpCodes.Tailcall           );
    /// <summary>
    /// 現在評価スタックにある例外オブジェクトをスローします。
    /// </summary>
    /// <param name="I"></param>
    public static void Throw         (this ILGenerator I                  )=>I.Emit(OpCodes.Throw              );
    /// <summary>
    /// 現在評価スタックの一番上にあるアドレスが、直後の ldind、stind、ldfld、stfld、ldobj、stobj、initblk または cpblkの各命令の通常サイズに合わせて配置されていない可能性があることを示します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="Label"></param>
    public static void Unaligned     (this ILGenerator I,Label Label      )=>I.Emit(OpCodes.Unaligned    ,Label);
    /// <summary>
    /// 現在評価スタックの一番上にあるアドレスが、直後の ldind、stind、ldfld、stfld、ldobj、stobj、initblk または cpblkの各命令の通常サイズに合わせて配置されていない可能性があることを示します。
    /// </summary>
    /// <param name="I"></param>
    /// <param name="値"></param>
    public static void Unaligned     (this ILGenerator I,byte 値          )=>I.Emit(OpCodes.Unaligned    ,値   );
    /// <summary>
    /// 値型のボックス化変換された形式をボックス化が解除された形式に変換します。Object→Int32&amp; 
    /// </summary>
    /// <param name="I"></param>
    /// <param name="T"></param>
    public static void Unbox         (this ILGenerator I,Type T           )=>I.Emit(OpCodes.Unbox        ,T    );
    /// <summary>
    /// 命令で指定された型のボックス化変換された形式を、ボックス化が解除された形式に変換します。Object→Int32
    /// </summary>
    /// <param name="I"></param>
    /// <param name="T"></param>
    public static void Unbox_Any     (this ILGenerator I,Type T           )=>I.Emit(OpCodes.Unbox_Any    ,T    );
    /// <summary>
    /// 現在評価スタックの一番上にあるアドレスが揮発性である可能性があるため、この位置の読み取り結果をキャッシュできないこと、またはこの位置への複数の格納を中止できないことを指定します。
    /// </summary>
    /// <param name="I"></param>
    public static void Volatile      (this ILGenerator I                  )=>I.Emit(OpCodes.Volatile           );
    /// <summary>
    /// 2 つの値のビットごとの XOR を計算し、結果を評価スタックにプッシュします。
    /// </summary>
    /// <param name="I"></param>
    public static void Xor           (this ILGenerator I                  )=>I.Emit(OpCodes.Xor                );
}