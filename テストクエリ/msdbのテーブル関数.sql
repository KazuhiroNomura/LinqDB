
        SELECT O.name,M.definition,O.type
        FROM sys.objects       O
        JOIN sys.schemas           S ON O.schema_id=S.schema_id
        JOIN sys.sql_modules M ON O.object_id=M.object_id
        WHERE O.type IN('TF','IF')AND S.name='sys'
        ORDER BY O.name
    