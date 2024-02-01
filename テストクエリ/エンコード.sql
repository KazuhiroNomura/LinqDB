declare @nvarchar nvarchar(100)='‚ '
declare @binary varbinary(100)=convert(varbinary(100),@nvarchar)
declare @varbinary varbinary(100)=convert(varbinary(100),@nvarchar)
select @binary,@varbinary
declare @binary_int varbinary(100)=7777777777777777
declare @int        int           =1

--set @binary_int=@int
select @binary_int,@int
--declare @uniqueidentifier uniqueidentifier=0x82A0
--declare @int int=0x82A0
--set @owner_sid=convert(varbinary(100),@s)
--select case when @varbinary='‚ ' then 'true' else 'false' end

--select case when @varbinary=@int then 'true' else 'false' end




