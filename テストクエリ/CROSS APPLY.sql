USE AdventureWorks
SELECT 
    [Resume].ref.value(N'declare default element namespace "http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/Resume"; 
        (/Resume/Name/Name.Prefix)[1]', 'nvarchar(30)') AS [Name.Prefix] 
FROM [HumanResources].[JobCandidate] jc 
CROSS APPLY jc.[Resume].nodes(
	N'declare default element namespace "http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/Resume"; 
    /Resume'
) AS Resume(ref);
