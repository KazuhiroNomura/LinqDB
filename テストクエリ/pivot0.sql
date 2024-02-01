use ŽÀŒ±
SELECT *
FROM dbo.Key‚ ‚è  PIVOT(
		SUM(dbo.Key‚ ‚è.C) 
		FOR dbo.Key‚ ‚è.B
		IN ([0], [1],[2],[3])
) AS pvt;



