use AdventureWorks2019
DECLARE @t TABLE(
	A INT,
	B INT,
	C INT
) 
insert into @t values(1,10,100)
insert into @t values(1,10,101)
insert into @t values(1,11,102)
insert into @t values(1,11,103)
insert into @t values(2,20,104)
insert into @t values(2,20,105)
insert into @t values(2,21,106)
insert into @t values(2,21,107)
SELECT X.*
FROM(
	SELECT Y.A,Y.B,Y.C
	FROM @t Y
)Z PIVOT(
	--出力する列はここで決定
	SUM(Z.C)   --[0],[1]内部の値
	FOR Z.A    --A
	IN([1],[2])--[0],[1]で3つの列を出力
)X

--SELECT X.年,
--X.[1]第1四半期,
--X.[2]第2四半期,
--X.[3]第3四半期,
--X.[4]第4四半期
--FROM(
--	SELECT
--		年,
--		[1]=(SELECT SUM(X.売上)FROM @t X WHERE X.月/3=0 AND X.年=年),
--		[2]=(SELECT SUM(X.売上)FROM @t X WHERE X.月/3=1 AND X.年=年),
--		[3]=(SELECT SUM(X.売上)FROM @t X WHERE X.月/3=2 AND X.年=年),
--		[4]=(SELECT SUM(X.売上)FROM @t X WHERE X.月/3=3 AND X.年=年)
--	FROM @t X GROUP BY X.年
--)X
