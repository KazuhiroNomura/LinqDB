use msdb
SELECT ����.dbo.����Database�擾(base_object_name),����.dbo.����Schema�擾(base_object_name),����.dbo.����Object�擾(base_object_name)
FROM sys.synonyms


declare @������1 int=charindex('].[',base_object_name,2)-2
declare @�J�n2 int=2+@������1+3
declare @������2 int=charindex('].[',base_object_name,@�J�n2)-@�J�n2
declare @�J�n3 int=@�J�n2+@������2+3
declare @������3 int=charindex(']',base_object_name,@�J�n3)-@�J�n3
substring(base_object_name,2,@������1)
substring(base_object_name,@�J�n2,@������2)
substring(base_object_name,@�J�n3,@������3)
