use AdventureWorks2019
DECLARE @t TABLE(
	ID INT,
	���� INT,
	�N INT,
	�� INT
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
SELECT A.�N,
A.[1]��1,
A.[2]��2,
A.[3]��3,
A.[4]��4
FROM @t X PIVOT(
	SUM(X.����) 
	FOR X.��
	IN([1],[2],[3],[4])
)A

SELECT A.�N,
A.[1]��1�l����,
A.[2]��2�l����,
A.[3]��3�l����,
A.[4]��4�l����
FROM(
	SELECT Y.�N,(Y.��-1)/3+1 �l����,Y.����
	FROM @t Y
)X PIVOT(
	SUM(X.����) 
	FOR X.�l����
	IN([1],[2],[3],[4])
)A

SELECT A.�N,
A.[1]��1�l����,
A.[2]��2�l����,
A.[3]��3�l����,
A.[4]��4�l����
FROM(
	SELECT
		�N,
		[1]=(SELECT SUM(X.����)FROM @t X WHERE X.��/3=0 AND A.�N=�N),
		[2]=(SELECT SUM(X.����)FROM @t X WHERE X.��/3=1 AND A.�N=�N),
		[3]=(SELECT SUM(X.����)FROM @t X WHERE X.��/3=2 AND A.�N=�N),
		[4]=(SELECT SUM(X.����)FROM @t X WHERE X.��/3=3 AND A.�N=�N)
	FROM @t A GROUP BY A.�N
)A

SELECT X.�N,
��1�l����=SUM(A.����),
��2�l����=SUM(B.����),
��3�l����=SUM(C.����),
��4�l����=SUM(D.����)
FROM @t X
LEFT JOIN @t A ON X.ID=A.ID AND (A.��-1)/3=0
LEFT JOIN @t B ON X.ID=B.ID AND (B.��-1)/3=1
LEFT JOIN @t C ON X.ID=C.ID AND (C.��-1)/3=2
LEFT JOIN @t D ON X.ID=D.ID AND (D.��-1)/3=3
GROUP BY X.�N
--��pivot
SELECT X.�N,
��1�l����=SUM(X.����)
FROM @t X
GROUP BY X.�N
--select* from(
--	SELECT 
--	A.�N,
--	A.[1]��1�l����,
--	A.[2]��2�l����,
--	A.[3]��3�l����,
--	A.[4]��4�l����
--	--FROM @t X PIVOT(
--	--    SUM(X.����) 
--	--    FOR X.��
--	--    IN([1],[2],[3],[4])
--	--)A
--	FROM(
--		SELECT Y.�N,(Y.��-1)/3+1 �l����,Y.����
--		FROM @t Y
--	)X PIVOT(
--		SUM(X.����) 
--		FOR X.�l����
--		IN([1],[2],[3],[4])
--	)A
--	GROUP BY A.�N,A.[1],A.[2],A.[3],A.[4]
--)z
