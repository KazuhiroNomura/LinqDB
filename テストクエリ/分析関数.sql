use DWDiagnostics
declare @T table(
	f0 int,
	f1 int,
	f2 int,
	f3 int
)
declare @f0 int=1
declare @f2 int=1
while @f0<=5 begin
	declare @f1 int=1
	while @f1<=@f0 begin
		insert @T(f0,f1,f2)values(@f0,@f1,@f2)
		set @f1=@f1+1
		set @f2=@f2+1
	end
	set @f0=@f0+1
end
--select * from @T
--SSELECT T.f0,
--	avg(cast(T.f2 as float)) OVER (PARTITION BY T.f0 ORDER BY T.f1 DESC) [Rank] f0‚Íorder by
--FROM @T T
--group by T.f0
--ORDER BY T.f2--T.f0,T.f1
SELECT T.f0,T.f1,T.f2,
	avg(cast(T.f2 as float)) OVER (PARTITION BY T.f0 ORDER BY T.f0 DESC) avg,
	sum(cast(T.f2 as float)) OVER (PARTITION BY T.f0 ORDER BY T.f1 DESC) sum
FROM @T T
--group by T.f0
--ORDER BY T.f2--T.f0,T.f1


SELECT T.f0,T.f1,T.f2,
	avg(cast(T.f2 as float)) OVER (PARTITION BY T.f0 ORDER BY T.f1 DESC) [Rank]
FROM @T T
group by T.f0,T.f1,T.f2
ORDER BY T.f2--T.f0,T.f1
--insert @T
--select 0,0,0,0 union
--select 0,0,0,1 union
--select 0,0,0,2 union
--select 0,0,0,0 union
--select 0,0,0,0 union
--select 0,0,0,0 union

--SELECT
--T.TABLE_CATALOG,T.TABLE_SCHEMA,T.TABLE_NAME,
--	RANK() OVER (PARTITION BY T.TABLE_CATALOG,T.TABLE_SCHEMA ORDER BY T.TABLE_NAME DESC) [Rank]
--FROM INFORMATION_SCHEMA.TABLES T
