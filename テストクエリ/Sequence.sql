declare @SCHEMA nvarchar(max)='Sequences'


SELECT name,start_value,increment,current_value,type
FROM sys.sequences S
WHERE SCHEMA_NAME(schema_id)=@SCHEMA

SELECT S.name,start_value,increment,current_value,type,T.name
FROM sys.sequences S
JOIN sys.types T ON S.user_type_id=T.user_type_id
--WHERE SCHEMA_NAME(S.schema_id)=@SCHEMA

