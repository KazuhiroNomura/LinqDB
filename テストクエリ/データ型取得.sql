SELECT A.name îhê∂DATA_TYPE,B.name ÉxÅ[ÉXDATA_TYPE
FROM sys.types A
--"JOIN sys.types       D ON D.user_type_id=A.user_type_id OR D.user_type_id=A.system_type_id
JOIN sys.types B ON B.user_type_id=A.system_type_id
--"JOIN sys.types       D ON D.system_type_id=A.system_type_id AND D.is_user_defined=0
--WHERE C.name=@SCHEMA AND B.name=@NAME
ORDER BY A.name,B.name
