use AdventureWorks2019
SELECT FirstName,PersonType,NameStyle,grouping(NameStyle),Title,middlename,lastname,suffix,emailpromotion,rowguid,modifieddate, count(*)
FROM Person.Person a
GROUP BY ROLLUP(FirstName,PersonType,NameStyle,Title,firstname,middlename,lastname,suffix,emailpromotion,rowguid,modifieddate)
SELECT FirstName,PersonType,NameStyle,Title,grouping(NameStyle)
FROM Person.Person a
GROUP BY ROLLUP(FirstName,PersonType,NameStyle,Title)
