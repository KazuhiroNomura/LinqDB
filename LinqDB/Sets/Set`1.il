/*
.class public BackendClient.AbstractSet`1<T>{
    .class public Enumerator<T>
    {
        .method public final hidebysig newslot virtual instance bool MoveNextCurrent ([out]!T& current) cil managed 
        {
            .locals(
                [0] valuetype BackendClient.AbstractSet`1/Slot<!T>[] Slots,
                [1] valuetype BackendClient.AbstractSet`1/Slot<!T>& a,
                [2] valuetype BackendClient.AbstractSet`1/Slot<!T>& '末尾',
                [3] int64 Length,
                [4] int64 index
            )
            ldarg.0
            call instance valuetype BackendClient.AbstractSet`1/Slot<!0, valuetype BackendClient.Set`1/Enumerator<!0>, !0, !0>[] valuetype BackendClient.AbstractSet`1/Enumerator<!T>::get_Slots()
            stloc.0
            ldarg.0
            ldfld int64 valuetype BackendClient.AbstractSet`1/Enumerator<!T>::index
            stloc.s index
            ldloc.0
            ldlen
            conv.i8
            stloc.3
            ldloc.s index
            ldloc.3
            blt.s '要素があった'
            ldc.i4.0
            ret
            '要素があった': 
            ldloc.0
            ldloc.s index
            ldelema valuetype BackendClient.AbstractSet`1/Slot<!T>
            stloc.1
            ldloc.0
            ldloc.3
            ldc.i8 1
            sub
            ldelema valuetype BackendClient.AbstractSet`1/Slot<!T>
            stloc.2
            br.s '開始'
            '再検索': 
            ldloc.1
            ldfld uint32 valuetype BackendClient.AbstractSet`1/Slot<!T>::hashCode
            ldc.i4.1
            bgt '発見'
            ldloc.1
            sizeof valuetype BackendClient.AbstractSet`1/Slot<!T>
            add
            stloc.1
            '開始': 
            ldloc.1
            ldloc.2
            ble '再検索'
            ldarg.0
            ldloc.1
            ldloc.0
            ldc.i4.0
            ldelema valuetype BackendClient.AbstractSet`1/Slot<!T>
            sub
            sizeof valuetype BackendClient.AbstractSet`1/Slot<!T>
            div
            ldc.i8 1
            add
            stfld int64 valuetype BackendClient.AbstractSet`1/Enumerator<!T>::index
            ldc.i4.0
            ret
            '発見':
            ldarg.1
            ldloc.1
            ldfld !0 valuetype BackendClient.AbstractSet`1/Slot<!T>::item
            stobj !T
            ldarg.0
            ldloc.1
            ldloc.0
            ldc.i4.0
            ldelema valuetype BackendClient.AbstractSet`1/Slot<!T>
            sub
            sizeof valuetype BackendClient.AbstractSet`1/Slot<!T>
            conv.i8
            div
            ldc.i8 1
            add
            stfld int64 valuetype BackendClient.AbstractSet`1/Enumerator<!T>::index

            ldc.i4.1
            ret
        }
        .method public final hidebysig newslot virtual instance bool MoveNextCurrentObject ([out] object& Current) cil managed 
        {
            .locals(
                [0] !T CurrentT,
                [1] bool result
            )
            ldarg.0
            ldloca.s CurrentT
            call instance bool valuetype BackendClient.AbstractSet`1/Enumerator<!T>::MoveNextCurrent(!0&)
            stloc.1
            ldarg.1
            ldloc.0
            box !T
            stind.ref
            ldloc.1
            ret
        } // end of method Enumerator::MoveNextCurrentObject
    } // end of class Enumerator
} // end of class BackendClient.Set`1
*/