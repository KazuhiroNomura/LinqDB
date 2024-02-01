use msdb
SELECT 実験.dbo.分割Database取得(base_object_name),実験.dbo.分割Schema取得(base_object_name),実験.dbo.分割Object取得(base_object_name)
FROM sys.synonyms


declare @文字数1 int=charindex('].[',base_object_name,2)-2
declare @開始2 int=2+@文字数1+3
declare @文字数2 int=charindex('].[',base_object_name,@開始2)-@開始2
declare @開始3 int=@開始2+@文字数2+3
declare @文字数3 int=charindex(']',base_object_name,@開始3)-@開始3
substring(base_object_name,2,@文字数1)
substring(base_object_name,@開始2,@文字数2)
substring(base_object_name,@開始3,@文字数3)
