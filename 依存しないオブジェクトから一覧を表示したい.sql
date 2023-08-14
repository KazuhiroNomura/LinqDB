USE AdventureWorks2019;  
GO
SELECT V.name TABLE_NAME,SQL.definition VIEW_DEFINITION
FROM sys.all_views V
JOIN sys.all_sql_modules SQL ON V.object_id=SQL.object_id
JOIN sys.schemas S ON V.schema_id=S.schema_id
ORDER BY V.name
SELECT B.*,SQL.definition
FROM sys.sql_expression_dependencies A 
JOIN sys.objects   B ON A.referencing_id=B.object_id
JOIN sys.all_views V ON V.object_id=B.object_id
JOIN sys.all_sql_modules SQL ON V.object_id=SQL.object_id


WITH 表(     name,  object_id,  principal_id,  schema_id,  parent_object_id,  type,  type_desc,  create_date,  modify_date,  is_ms_shipped,  is_published,  is_schema_published,  SQL) AS (
    SELECT B.name,B.object_id,B.principal_id,B.schema_id,B.parent_object_id,B.type,B.type_desc,B.create_date,B.modify_date,B.is_ms_shipped,B.is_published,B.is_schema_published,CAST(''AS NVARCHAR(MAX))
	FROM sys.objects B 
	WHERE B.type IN('U')
    UNION ALL
    SELECT B.name,B.object_id,B.principal_id,B.schema_id,B.parent_object_id,B.type,B.type_desc,B.create_date,B.modify_date,B.is_ms_shipped,B.is_published,B.is_schema_published,SQL.definition
	FROM sys.sql_expression_dependencies A 
	JOIN sys.objects   B ON A.referencing_id=B.object_id
	JOIN 表            C ON A.referenced_id=C.object_id
	JOIN sys.all_views V ON V.object_id=B.object_id
	JOIN sys.all_sql_modules SQL ON V.object_id=SQL.object_id
	WHERE B.type IN('V')
)SELECT DISTINCT * FROM 表 WHERE type='V'
--REFERENCING_IDはID
--REFERENCED_IDはPARENT_ID
WITH 表(name,object_id,principal_id,schema_id,parent_object_id,type,type_desc,create_date,modifiy_date,is_ms_shipped,is_published,is_schema_published) AS (
    SELECT A.*
	FROM sys.objects A 
	WHERE A.type IN('U')
    UNION ALL
    SELECT B.*
	FROM sys.sql_expression_dependencies A 
	JOIN sys.objects B ON A.referencing_id=B.object_id
	JOIN 表          C ON A.referenced_id=C.object_id
	WHERE B.type IN('V')
)SELECT DISTINCT * FROM 表 WHERE type='V'
--referencing_id	int	参照元エンティティの ID。 NULL 値は許可されません。
SELECT 
--OBJECT_NAME(referencing_id) AS referencing_entity_name,   
--    o.type_desc AS referencing_desciption,   
--    --COALESCE(COL_NAME(referencing_id, referencing_minor_id), '(n/a)') AS referencing_minor_id,   
--    referencing_class_desc, referenced_class_desc,  
--    referenced_server_name, referenced_database_name, referenced_schema_name,  
--    referenced_entity_name,
	s0.*
    --COALESCE(COL_NAME(referenced_id, referenced_minor_id), '(n/a)') AS referenced_column_name,  
    --is_caller_dependent, is_ambiguous  
FROM sys.sql_expression_dependencies AS s0
LEFT JOIN sys.sql_expression_dependencies AS s1 ON s0.referenced_id
INNER JOIN sys.objects AS o ON s0.referencing_id = o.object_id  
--WHERE referencing_id = OBJECT_ID(N'Production.vProductAndDescription');  
SELECT 
*,
	sed.referenced_server_name,
	sed.referenced_database_name,
	sed.referenced_schema_name,
	sed.referenced_entity_name
FROM sys.sql_expression_dependencies AS sed
ORDER BY
	sed.referenced_server_name,
	sed.referenced_database_name,
	sed.referenced_schema_name,
	sed.referenced_entity_name
