use ����
SELECT *
FROM dbo.Key����  PIVOT(
		SUM(dbo.Key����.C) 
		FOR dbo.Key����.B
		IN ([0], [1],[2],[3])
) AS pvt;



