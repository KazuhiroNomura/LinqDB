declare @SCHEMA nvarchar(max)='dbo'

    SELECT O.name,M.definition,T.name
    FROM sys.objects       O
    JOIN sys.schemas    S ON O.schema_id=S.schema_id
    JOIN sys.sql_modules M ON O.object_id=M.object_id
    JOIN sys.parameters P ON O.object_id     =P.object_id
    JOIN sys.types      T ON P.system_type_id=T.user_type_id
    WHERE S.name=@SCHEMA AND P.is_output=1 AND O.type='FN'
    ORDER BY O.name
    