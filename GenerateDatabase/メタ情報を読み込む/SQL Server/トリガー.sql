DECLARE @SCHEMA NVARCHAR(MAX),@TABLE_NAME NVARCHAR(MAX)
SELECT @SCHEMA='Person',@TABLE_NAME='Person'
USE [AdventureWorks2017] 
SELECT triggers.name NAME,sql_modules.definition
FROM sys.triggers
JOIN sys.tables ON triggers.parent_id=tables.object_id
JOIN sys.sql_modules ON sql_modules.object_id = triggers.object_id
JOIN sys.schemas         ON tables.schema_id=schemas.schema_id
WHERE schemas.name=@SCHEMA AND tables.name=@SCHEMA
