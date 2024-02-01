use AdventureWorks2019
DECLARE @t TABLE(
	ID INT,
	売上 INT,
	年 INT,
	月 INT
) 
insert into @t values(0, 2500,2000,1)
insert into @t values(1,10000,2001,2)
insert into @t values(2, 9800,2002,3)
insert into @t values(3, 4800,2000,4)
insert into @t values(4, 3480,2001,5)
insert into @t values(5, 2480,2003,6)
insert into @t values(6,12800,2002,7)
insert into @t values(7,25000,2000,8)
insert into @t values(8,  980,2004,9)
insert into @t values(9,  480,2003,10)
insert into @t values(10,1180,2003,11)
insert into @t values(11,1530,2004,12)
SELECT A.年,
A.[1]月1,
A.[2]月2,
A.[3]月3,
A.[4]月4
FROM @t X PIVOT(
	SUM(X.売上) 
	FOR X.月
	IN([1],[2],[3],[4])
)A

SELECT A.年,
A.[1]第1四半期,
A.[2]第2四半期,
A.[3]第3四半期,
A.[4]第4四半期
FROM(
	SELECT Y.年,(Y.月-1)/3+1 四半期,Y.売上
	FROM @t Y
)X PIVOT(
	SUM(X.売上) 
	FOR X.四半期
	IN([1],[2],[3],[4])
)A

SELECT A.年,
A.[1]第1四半期,
A.[2]第2四半期,
A.[3]第3四半期,
A.[4]第4四半期
FROM(
	SELECT
		年,
		[1]=(SELECT SUM(X.売上)FROM @t X WHERE X.月/3=0 AND A.年=年),
		[2]=(SELECT SUM(X.売上)FROM @t X WHERE X.月/3=1 AND A.年=年),
		[3]=(SELECT SUM(X.売上)FROM @t X WHERE X.月/3=2 AND A.年=年),
		[4]=(SELECT SUM(X.売上)FROM @t X WHERE X.月/3=3 AND A.年=年)
	FROM @t A GROUP BY A.年
)A

SELECT X.年,
第1四半期=SUM(A.売上),
第2四半期=SUM(B.売上),
第3四半期=SUM(C.売上),
第4四半期=SUM(D.売上)
FROM @t X
LEFT JOIN @t A ON X.ID=A.ID AND (A.月-1)/3=0
LEFT JOIN @t B ON X.ID=B.ID AND (B.月-1)/3=1
LEFT JOIN @t C ON X.ID=C.ID AND (C.月-1)/3=2
LEFT JOIN @t D ON X.ID=D.ID AND (D.月-1)/3=3
GROUP BY X.年
--非pivot
SELECT X.年,
第1四半期=SUM(X.売上)
FROM @t X
GROUP BY X.年
--select* from(
--	SELECT 
--	A.年,
--	A.[1]第1四半期,
--	A.[2]第2四半期,
--	A.[3]第3四半期,
--	A.[4]第4四半期
--	--FROM @t X PIVOT(
--	--    SUM(X.売上) 
--	--    FOR X.月
--	--    IN([1],[2],[3],[4])
--	--)A
--	FROM(
--		SELECT Y.年,(Y.月-1)/3+1 四半期,Y.売上
--		FROM @t Y
--	)X PIVOT(
--		SUM(X.売上) 
--		FOR X.四半期
--		IN([1],[2],[3],[4])
--	)A
--	GROUP BY A.年,A.[1],A.[2],A.[3],A.[4]
--)z
