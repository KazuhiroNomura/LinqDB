use 実験
SELECT *
FROM dbo.Keyあり  PIVOT(
		SUM(dbo.Keyあり.C) 
		FOR dbo.Keyあり.B
		IN ([0], [1],[2],[3])
) AS pvt;



