use msdb
SELECT S.name �X�L�[�},B.name �֐�,P.is_output,T.name type,P.name �p�����[�^,P.has_default_value,P.default_value
FROM sys.all_objects    B
JOIN sys.schemas        S ON B.schema_id     =S.schema_id
JOIN sys.all_parameters P ON B.object_id     =P.object_id
JOIN sys.types          T ON P.system_type_id=T.user_type_id
WHERE S.name='smart_admin' AND B.name='fn_get_health_status'
ORDER BY 
P.has_default_value,
P.is_output,
--is_output,has_default_value,
default_value,B.name,P.parameter_id
