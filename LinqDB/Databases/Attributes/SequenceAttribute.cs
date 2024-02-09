using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinqDB.Databases.Attributes;
public class SequenceAttribute:Attribute{
    internal static class Reflection{
        public static readonly ConstructorInfo ctor=typeof(SequenceAttribute).GetConstructor(Type.EmptyTypes)!;
    }
}
