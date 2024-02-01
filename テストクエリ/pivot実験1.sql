use AdventureWorks2019
DECLARE @t TABLE(
	G0 INT,
	G1 NVARCHAR,
	F INT
) 
insert into @t values(0,'a',0)
insert into @t values(0,'a',1)
insert into @t values(1,'b',0)
insert into @t values(1,'b',1)
insert into @t values(1,'b',2)
insert into @t values(2,'c',0)
insert into @t values(2,'c',1)
insert into @t values(2,'c',2)
insert into @t values(2,'c',3)
SELECT 
pvt.[0],
pvt.[1],
pvt.[2]
FROM @t soh PIVOT(
    SUM(soh.F) 
    FOR soh.G0
    IN([0],[1],[2])
)pvt
