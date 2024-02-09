using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LinqDB.Databases.Attributes;
//public class Sequence<T>where T:struct,IIncrementOperators<T>{
public struct Sequence<T>where T:struct,IIncrementOperators<T>{
    public T start_value;
    public T increment;
    public T current_value;
    public Sequence(){}
    public T NextValue(){
        return this.current_value++;
    }
}