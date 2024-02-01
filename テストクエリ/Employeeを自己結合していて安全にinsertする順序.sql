use Northwind;
WITH W as(
    SELECT e.EmployeeID,e.LastName,e.FirstName,e.Title,e.TitleOfCourtesy,e.BirthDate,e.HireDate,e.Address,e.City,e.Region,e.PostalCode,e.Country,e.HomePhone,e.Extension,e.Photo,e.Notes,e.ReportsTo,e.PhotoPath FROM Employees e where ReportsTo is null
    UNION ALL
    SELECT e.EmployeeID,e.LastName,e.FirstName,e.Title,e.TitleOfCourtesy,e.BirthDate,e.HireDate,e.Address,e.City,e.Region,e.PostalCode,e.Country,e.HomePhone,e.Extension,e.Photo,e.Notes,e.ReportsTo,e.PhotoPath FROM Employees e join W on e.ReportsTo=W.EmployeeID
)select * from W
