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
	--�o�͂����͂����Ō���
	SUM(Z.C)   --[0],[1]�����̒l
	FOR Z.A    --A
	IN([1],[2])--[0],[1]��3�̗���o��
)X

--SELECT X.�N,
--X.[1]��1�l����,
--X.[2]��2�l����,
--X.[3]��3�l����,
--X.[4]��4�l����
--FROM(
--	SELECT
--		�N,
--		[1]=(SELECT SUM(X.����)FROM @t X WHERE X.��/3=0 AND X.�N=�N),
--		[2]=(SELECT SUM(X.����)FROM @t X WHERE X.��/3=1 AND X.�N=�N),
--		[3]=(SELECT SUM(X.����)FROM @t X WHERE X.��/3=2 AND X.�N=�N),
--		[4]=(SELECT SUM(X.����)FROM @t X WHERE X.��/3=3 AND X.�N=�N)
--	FROM @t X GROUP BY X.�N
--)X
