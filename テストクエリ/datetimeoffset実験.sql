declare @d datetimeoffset='2022-12-31 23:59:59.0000000 +14:00'
declare @e datetime=@d
select year(@d),year(@e)
select @e,@d

