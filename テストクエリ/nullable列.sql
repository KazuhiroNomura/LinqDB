
        SELECT T0.name,T0.is_nullable,T1.is_nullable
        from      sys.types   T0
        LEFT JOIN sys.types   T1 ON T0.system_type_id=T1.user_type_id
        --WHERE O.type IN('V','FN','TF','P')AND S.name='dbo' AND O.name='sysutility_ucp_dac_policies'
        --ORDER BY C.column_id