use AdventureWorks2019
SELECT 
    soh.[SalesPersonID]
    ,p.[FirstName] + ' ' + COALESCE(p.[MiddleName], '') + ' ' + p.[LastName] AS [FullName]
    ,e.[JobTitle]
    ,st.[Name] AS [SalesTerritory]
    ,soh.[SubTotal]
    ,YEAR(DATEADD(m, 6, soh.[OrderDate])) AS [FiscalYear] 
FROM [Sales].[SalesPerson] sp 
    INNER JOIN [Sales].[SalesOrderHeader] soh 
    ON sp.[BusinessEntityID] = soh.[SalesPersonID]
    INNER JOIN [Sales].[SalesTerritory] st 
    ON sp.[TerritoryID] = st.[TerritoryID] 
    INNER JOIN [HumanResources].[Employee] e 
    ON soh.[SalesPersonID] = e.[BusinessEntityID] 
	INNER JOIN [Person].[Person] p
	ON p.[BusinessEntityID] = sp.[BusinessEntityID]

