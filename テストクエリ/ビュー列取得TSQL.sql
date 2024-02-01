SELECT B.name,D.name DATA_TYPE,A.name COLUMN_NAME,CASE WHEN A.is_nullable=1 THEN'YES'ELSE'NO'END IS_NULLABLE
FROM sys.all_columns A
JOIN sys.all_objects       B ON A.object_id   =B.object_id
JOIN sys.schemas     C ON B.schema_id   =C.schema_id
--"JOIN sys.types       D ON D.user_type_id=A.user_type_id OR D.user_type_id=A.system_type_id
JOIN sys.types   D ON D.user_type_id=A.system_type_id
--"JOIN sys.types       D ON D.system_type_id=A.system_type_id AND D.is_user_defined=0
--WHERE C.name=@SCHEMA AND B.name=@NAME
ORDER BY B.name,A.name
