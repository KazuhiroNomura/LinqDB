DECLARE @SCHEMA NVARCHAR(MAX)
SELECT @SCHEMA='dbo'


SELECT schemas.name TABLE_SCHEMA,tables.name TABLE_NAME
FROM sys.tables
JOIN sys.schemas ON tables.schema_id=schemas.schema_id
ORDER BY tables.name

