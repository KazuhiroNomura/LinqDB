DECLARE @CATALOG NVARCHAR(MAX)
SELECT @CATALOG='TPC-H'

--USE [TPC-H]
SELECT SCHEMA_NAME
FROM INFORMATION_SCHEMA.SCHEMATA
--WHERE CATALOG_NAME=@CATALOG
ORDER BY SCHEMA_NAME

SELECT name SCHEMA_NAME
FROM sys.schemas
ORDER BY name

