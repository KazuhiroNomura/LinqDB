using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.CRC;

[TestClass]
public class Test_CRC32
{
    private const int 外回数 = 10000;
    private const int 内回数 = 1000;
    private static void Input(Action<global::LinqDB.CRC.CRC32, int> InputAction)
    {
        var r = new Random(1);
        var actual = new bool[内回数];
        var シノニム数 = 0;
        for (var a = 0; a < 外回数; a++)
        {
            var C = new global::LinqDB.CRC.CRC32();
            for (var b = 0; b < 内回数; b++)
            {
                InputAction(C, r.Next());
                var HashCode = (uint)C.GetHashCode() % 内回数;
                if (actual[HashCode])
                    シノニム数++;
                else
                    actual[HashCode] = true;
            }
            for (var b = 0; b < 内回数; b++)
                actual[b] = false;
        }
        Debug.WriteLine("シノニム" + シノニム数);
        Debug.WriteLine("シノニム/要素数" + (double)シノニム数 / 内回数);
    }
    [TestMethod] public void Input_Byte() => Input((CRC, 値) => CRC.Input((byte)値));
    [TestMethod] public void Input_UInt16() => Input((CRC, 値) => CRC.Input((ushort)値));
    [TestMethod] public void Input_UInt32() => Input((CRC, 値) => CRC.Input((uint)値));
    [TestMethod] public void Input_SByte() => Input((CRC, 値) => CRC.Input((sbyte)値));
    [TestMethod] public void Input_Int16() => Input((CRC, 値) => CRC.Input((short)値));
    [TestMethod] public void Input_Int32() => Input((CRC, 値) => CRC.Input(値));
    [TestMethod] public void Input_String() => Input((CRC, 値) => CRC.Input(値.ToString()));
}