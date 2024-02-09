declare @SCHEMA nvarchar(max)='dbo'
        SELECT P.name,P.is_output,T.name type,P.has_default_value,P.default_value
        FROM sys.objects    B
        JOIN sys.schemas              S ON B.schema_id     =S.schema_id
        JOIN sys.parameters P ON B.object_id     =P.object_id
        JOIN sys.types                T ON P.system_type_id=T.user_type_id
        WHERE S.name=@SCHEMA AND P.is_output=1
        ORDER BY B.name,P.parameter_id
