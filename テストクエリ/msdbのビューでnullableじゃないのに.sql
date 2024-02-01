SELECT C.name COLUMN_NAME,ISNULL(T1.name,T0.name)DATA_TYPE,C.is_nullable, T0.is_nullable,T1.is_nullable,CASE WHEN T0.is_nullable=1 THEN'YES'ELSE'NO'END IS_NULLABLE
FROM      sys.objects O
JOIN      sys.schemas S  ON O.schema_id     =S.schema_id
JOIN      sys.columns C  ON O.object_id     =C.object_id
JOIN      sys.types   T0 ON C.user_type_id=T0.user_type_id
LEFT JOIN sys.types   T1 ON T0.system_type_id=T1.user_type_id
--WHERE O.type IN('V','FN','TF','P')AND S.name='dbo' AND O.name='sysutility_ucp_dac_policies'
ORDER BY DATA_TYPE,C.column_id