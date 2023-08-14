DECLARE @SCHEMA NVARCHAR(MAX)
SELECT @SCHEMA='sys'
--USE [TPC-H] 
SELECT sys.all_views.name NAME,sys.all_sql_modules.definition
FROM sys.all_views
JOIN sys.all_sql_modules ON sys.all_views.object_id=sys.all_sql_modules.object_id
JOIN sys.schemas         ON sys.all_views.schema_id=schemas.schema_id
WHERE sys.schemas.name=@SCHEMA
ORDER BY sys.all_views.name
