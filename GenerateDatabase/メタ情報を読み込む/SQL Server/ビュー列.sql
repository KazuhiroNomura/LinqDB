SELECT D.name DATA_TYPE,A.name COLUMN_NAME,CASE WHEN A.is_nullable=1 THEN'YES'ELSE'NO'END IS_NULLABLE
FROM sys.all_columns A
JOIN sys.views       B ON A.object_id   =B.object_id
JOIN sys.schemas     C ON B.schema_id   =C.schema_id
JOIN sys.types       D ON D.user_type_id=A.user_type_id
ORDER BY B.name,A.name

