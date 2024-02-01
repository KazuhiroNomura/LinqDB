SELECT C.name,O.name
FROM      sys.all_objects O
JOIN      sys.schemas S  ON O.schema_id     =S.schema_id
JOIN      sys.all_columns C  ON O.object_id     =C.object_id
WHERE C.name='null_on_null_input'
--ORDER BY DATA_TYPE,C.column_id