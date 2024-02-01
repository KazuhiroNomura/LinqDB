SELECT
	DB_NAME()					AS SPECIFIC_CATALOG,
	SCHEMA_NAME(o.schema_id)	AS SPECIFIC_SCHEMA,
	o.name						AS SPECIFIC_NAME,
	DB_NAME()					AS ROUTINE_CATALOG,
	SCHEMA_NAME(o.schema_id)	AS ROUTINE_SCHEMA,
	o.name						AS ROUTINE_NAME,
	convert(nvarchar(20), CASE
		WHEN o.type IN ('P','PC')
		THEN 'PROCEDURE'
		ELSE 'FUNCTION' END)		AS ROUTINE_TYPE,
	convert(sysname, null)			AS MODULE_CATALOG,
	convert(sysname, null) collate catalog_default		AS MODULE_SCHEMA,
	convert(sysname, null) collate catalog_default		AS MODULE_NAME,
	convert(sysname, null)			AS UDT_CATALOG,
	convert(sysname, null) collate catalog_default		AS UDT_SCHEMA,
	convert(sysname, null) collate catalog_default		AS UDT_NAME,
	convert(sysname, CASE
		WHEN o.type IN ('TF', 'IF', 'FT') THEN N'TABLE'
		ELSE ISNULL(TYPE_NAME(c.system_type_id),
			TYPE_NAME(c.user_type_id)) END)		AS DATA_TYPE,
	COLUMNPROPERTY(c.object_id, c.name, 'charmaxlen')	AS CHARACTER_MAXIMUM_LENGTH,
	COLUMNPROPERTY(c.object_id, c.name, 'octetmaxlen')	AS CHARACTER_OCTET_LENGTH,
	convert(sysname, null)			AS COLLATION_CATALOG,
	convert(sysname, null) collate catalog_default		AS COLLATION_SCHEMA,
	convert(sysname, CASE
		WHEN c.system_type_id IN (35, 99, 167, 175, 231, 239)	-- [n]char/[n]varchar/[n]text
		THEN DATABASEPROPERTYEX(DB_NAME(), 'collation') END)	AS COLLATION_NAME,
	convert(sysname, null)			AS CHARACTER_SET_CATALOG,
	convert(sysname, null) collate catalog_default		AS CHARACTER_SET_SCHEMA,
	convert(sysname, CASE
		WHEN c.system_type_id IN (35, 167, 175)
		THEN SERVERPROPERTY('sqlcharsetname') -- char/varchar/text
		WHEN c.system_type_id IN (99, 231, 239)
		THEN N'UNICODE'	-- nchar/nvarchar/ntext
		END)				AS CHARACTER_SET_NAME,
	convert(tinyint, CASE -- int/decimal/numeric/real/float/money
		WHEN c.system_type_id IN (48, 52, 56, 59, 60, 62, 106, 108, 122, 127) THEN c.precision
		END)										AS NUMERIC_PRECISION,
	convert(smallint, CASE	-- int/money/decimal/numeric
		WHEN c.system_type_id IN (48, 52, 56, 60, 106, 108, 122, 127) THEN 10
		WHEN c.system_type_id IN (59, 62) THEN 2 END)	AS NUMERIC_PRECISION_RADIX, -- real/float
	convert(int, CASE	-- datetime/smalldatetime
		WHEN c.system_type_id IN (40, 41, 42, 43, 58, 61) THEN NULL
		ELSE ODBCSCALE(c.system_type_id, c.scale) END)	AS NUMERIC_SCALE,
	convert(smallint, CASE -- datetime/smalldatetime
		WHEN c.system_type_id IN (40, 41, 42, 43, 58, 61) THEN ODBCSCALE(c.system_type_id, c.scale) END)	AS DATETIME_PRECISION,
	convert(nvarchar(30), null)			AS INTERVAL_TYPE,
	convert(smallint, null)				AS INTERVAL_PRECISION,
	convert(sysname, null)				AS TYPE_UDT_CATALOG,
	convert(sysname, null) collate catalog_default	AS TYPE_UDT_SCHEMA,
	convert(sysname, null) collate catalog_default	AS TYPE_UDT_NAME,
	convert(sysname, null)				AS SCOPE_CATALOG,
	convert(sysname, null) collate catalog_default		AS SCOPE_SCHEMA,
	convert(sysname, null) collate catalog_default		AS SCOPE_NAME,
	convert(bigint, null)				AS MAXIMUM_CARDINALITY,
	convert(sysname, null)				AS DTD_IDENTIFIER,
	convert(nvarchar(30), CASE
		WHEN o.type IN ('P ', 'FN', 'TF', 'IF') THEN 'SQL'
		ELSE 'EXTERNAL' END)			AS ROUTINE_BODY,
	convert(nvarchar(4000),
		OBJECT_DEFINITION(o.object_id))	AS ROUTINE_DEFINITION,
	convert(sysname, null)				AS EXTERNAL_NAME,
	convert(nvarchar(30), null)			AS EXTERNAL_LANGUAGE,
	convert(nvarchar(30), null)			AS PARAMETER_STYLE,
	convert(nvarchar(10), CASE
		WHEN ObjectProperty(o.object_id, 'IsDeterministic') = 1
		THEN 'YES' ELSE 'NO' END)		AS IS_DETERMINISTIC,
	convert(nvarchar(30), CASE
		WHEN o.type IN ('P', 'PC') THEN 'MODIFIES'
		ELSE 'READS' END)				AS SQL_DATA_ACCESS,
	convert(nvarchar(10), CASE
		WHEN o.type in ('P', 'PC') THEN null
		WHEN o.null_on_null_input = 1 THEN 'YES'
		ELSE 'NO' END)				AS IS_NULL_CALL,
	convert(sysname, null)				AS SQL_PATH,
	convert(nvarchar(10), 'YES')	AS SCHEMA_LEVEL_ROUTINE,
	convert(smallint, CASE
		WHEN o.type IN ('P ', 'PC')
		THEN -1 ELSE 0 END)			AS MAX_DYNAMIC_RESULT_SETS,
	convert(nvarchar(10), 'NO')			AS IS_USER_DEFINED_CAST,
	convert(nvarchar(10), 'NO')			AS IS_IMPLICITLY_INVOCABLE,
	o.create_date						AS CREATED,
	o.modify_date						AS LAST_ALTERED
FROM (SELECT x.*,s.null_on_null_input from sys.all_objects x JOIN sys.system_sql_modules s on x.object_id=s.object_id)as o
LEFT JOIN sys.parameters c 
	ON (c.object_id = o.object_id AND c.parameter_id = 0)
WHERE
	o.type IN ('P', 'FN', 'TF', 'IF', 'AF', 'FT', 'IS', 'PC', 'FS')
