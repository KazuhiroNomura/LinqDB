using System;
using System.Diagnostics;
using Northwind.Tables.dbo;
using Northwind;
using System.Data.SqlClient;

namespace TestNorthwind;

abstract class Program:共通 {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization","CA1305:IFormatProvider を指定します",Justification = "<保留中>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization","CA1303:ローカライズされるパラメーターとしてリテラルを渡さない",Justification = "<保留中>")]
    private static void Create() {
        using var Container = new Container();
        var dbo=Container.dbo;
        var Region = dbo.Region;
        var Territories= dbo.Territories;
        var Employees= dbo.Employees;
        var EmployeeTerritories= dbo.EmployeeTerritories;
        var s = Stopwatch.StartNew();
        //LV0
        for(var EmployeeID=0;EmployeeID<8;EmployeeID++){
            Employees.AddOrThrow(new(EmployeeID,"","",null,null,null,null,null,null,null,null,null,null,null,null,null,null,null));
        }
        for(var EmployeeID=8;EmployeeID<64;EmployeeID++){
            Employees.AddOrThrow(new(EmployeeID,"","",null,null,null,null,null,null,null,null,null,null,null,null,null,EmployeeID/8,null));
        }
        for(var RegionID=0;RegionID<2;RegionID++){
            Region.AddOrThrow(new Region(RegionID,""));
            for(var TerritoryID=0;TerritoryID<2;TerritoryID++){
                Territories.AddOrThrow(new(TerritoryID.ToString(),"",RegionID));
            }
        }

        foreach(var Employee in Employees){
            foreach(var Territory in Territories){
                EmployeeTerritories.AddOrThrow(new(Employee.EmployeeID,Territory.TerritoryID));
            }
        }
        Console.WriteLine($"Create {s.ElapsedMilliseconds,7}ms");
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization","CA1305:IFormatProvider を指定します",Justification = "<保留中>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization","CA1303:ローカライズされるパラメーターとしてリテラルを渡さない",Justification = "<保留中>")]
    private static void Load() {
        using var C = new SqlConnection(@"Data Source=localhost\MSSQLSERVER2019;Initial Catalog=Northwind;Integrated Security=True");
        C.Open();
        using var Command = new SqlCommand { Connection=C };
        using var e = new Container();
        var dbo= e.dbo;
        var Employees= dbo.Employees;
        var Region= dbo.Region;
        var Territories= dbo.Territories;
        var EmployeeTerritories= dbo.EmployeeTerritories;
        var Shippers= dbo.Shippers;
        var Customers= dbo.Customers;
        var CustomerDemographics= dbo.CustomerDemographics;
        var CustomerCustomerDemo= dbo.CustomerCustomerDemo;
        var Categories= dbo.Categories;
        var Suppliers= dbo.Suppliers;
        var Products= dbo.Products;
        var Order_Details= dbo.Order_Details;
        var Orders= dbo.Orders;
        var s = Stopwatch.StartNew();
        {
            Command.CommandText=@"
                WITH W as(
                    SELECT e.EmployeeID,e.LastName,e.FirstName,e.Title,e.TitleOfCourtesy,e.BirthDate,e.HireDate,e.Address,e.City,e.Region,e.PostalCode,e.Country,e.HomePhone,e.Extension,e.Photo,e.Notes,e.ReportsTo,e.PhotoPath FROM Employees e where ReportsTo is null
                    UNION ALL
                    SELECT e.EmployeeID,e.LastName,e.FirstName,e.Title,e.TitleOfCourtesy,e.BirthDate,e.HireDate,e.Address,e.City,e.Region,e.PostalCode,e.Country,e.HomePhone,e.Extension,e.Photo,e.Notes,e.ReportsTo,e.PhotoPath FROM Employees e join W on e.ReportsTo=W.EmployeeID
                )select * from W
                SELECT 
                 RegionID
                ,RegionDescription
                FROM Region;
			    SELECT 
			     TerritoryID
			    ,TerritoryDescription
			    ,RegionID
			    FROM Territories;
                SELECT 
                 EmployeeID
                ,TerritoryID
                FROM EmployeeTerritories;
                SELECT 
                ShipperID
                ,CompanyName
                ,Phone
                FROM Shippers;
                SELECT 
                CustomerID
                ,CompanyName
                ,ContactName
                ,ContactTitle
                ,Address
                ,City
                ,Region
                ,PostalCode
                ,Country
                ,Phone
                ,Fax
                FROM Customers;
                SELECT 
                CustomerTypeID
                ,CustomerDesc
                FROM CustomerDemographics;
                SELECT 
                CustomerID
                ,CustomerTypeID
                FROM CustomerCustomerDemo;
                SELECT 
                CategoryID
                ,CategoryName
                ,Description
                ,Picture
                FROM Categories;
                SELECT 
                SupplierID
                ,CompanyName
                ,ContactName
                ,ContactTitle
                ,Address
                ,City
                ,Region
                ,PostalCode
                ,Country
                ,Phone
                ,Fax
                ,HomePage
                FROM Suppliers;
                SELECT 
                ProductID
                ,ProductName
                ,SupplierID
                ,CategoryID
                ,QuantityPerUnit
                ,UnitPrice
                ,UnitsInStock
                ,UnitsOnOrder
                ,ReorderLevel
                ,Discontinued
                FROM Products;
                SELECT 
                 OrderID
                ,CustomerID
                ,EmployeeID
                ,OrderDate
                ,RequiredDate
                ,ShippedDate
                ,ShipVia
                ,Freight
                ,ShipName
                ,ShipAddress
                ,ShipCity
                ,ShipRegion
                ,ShipPostalCode
                ,ShipCountry
                FROM Orders;
                SELECT 
                OrderID
                ,ProductID
                ,UnitPrice
                ,Quantity
                ,Discount
                FROM [Order Details];
                ";
            using var Reader = Command.ExecuteReader();
            while(Reader.Read()) {
                Employees.AddOrThrow(
                    new Employees(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetString(2),
                        GetString(Reader,3),
                        GetString(Reader,4),
                        GetDateTime(Reader,5),
                        GetDateTime(Reader,6),
                        GetString(Reader,7),
                        GetString(Reader,8),
                        GetString(Reader,9),
                        GetString(Reader,10),
                        GetString(Reader,11),
                        GetString(Reader,12),
                        GetString(Reader,13),
                        GetBytes(Reader,14),
                        GetString(Reader,15),
                        GetInt32(Reader,16),
                        GetString(Reader,17)
                    )
                );
            }
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                Region.AddOrThrow(
                    new Region(
                        Reader.GetInt32(0),
                        Reader.GetString(1)
                    )
                );
            }
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                Territories.AddOrThrow(
                    new Territories(
                        Reader.GetString(0),
                        Reader.GetString(1),
                        Reader.GetInt32(2)
                    )
                );
            }
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                EmployeeTerritories.AddOrThrow(
                    new EmployeeTerritories(
                        Reader.GetInt32(0),
                        Reader.GetString(1)
                    )
                );
            }
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                Shippers.AddOrThrow(
                    new Shippers(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        GetString(Reader,2)
                    )
                );
            }
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                Customers.AddOrThrow(
                    new Customers(
                        Reader.GetString(0),
                        Reader.GetString(1),
                        GetString(Reader,2),
                        GetString(Reader,3),
                        GetString(Reader,4),
                        GetString(Reader,5),
                        GetString(Reader,6),
                        GetString(Reader,7),
                        GetString(Reader,8),
                        GetString(Reader,9),
                        GetString(Reader,10)
                    )
                );
            }

            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                CustomerDemographics.AddOrThrow(
                    new CustomerDemographics(
                        Reader.GetString(0),
                        GetString(Reader,1)
                    )
                );
            }
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                CustomerCustomerDemo.AddOrThrow(
                    new CustomerCustomerDemo(
                        Reader.GetString(0),
                        Reader.GetString(1)
                    )
                );
            }
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                Categories.AddOrThrow(
                    new Categories(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        GetString(Reader,2),
                        GetBytes(Reader,3)
                    )
                );
            }
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                Suppliers.AddOrThrow(
                    new Suppliers(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        GetString(Reader,2),
                        GetString(Reader,3),
                        GetString(Reader,4),
                        GetString(Reader,5),
                        GetString(Reader,6),
                        GetString(Reader,7),
                        GetString(Reader,8),
                        GetString(Reader,9),
                        GetString(Reader,10),
                        GetString(Reader,11)
                    )
                );
            }
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                Products.AddOrThrow(
                    new Products(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        GetInt32(Reader,2),
                        GetInt32(Reader,3),
                        GetString(Reader,4),
                        GetDecimal(Reader,5),
                        GetInt16(Reader,6),
                        GetInt16(Reader,7),
                        GetInt16(Reader,8),
                        Reader.GetBoolean(9)
                    )
                );
            }
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                Orders.AddOrThrow(
                    new Orders(
                        Reader.GetInt32(0),
                        GetString(Reader,1),
                        GetInt32(Reader,2),
                        GetDateTime(Reader,3),
                        GetDateTime(Reader,4),
                        GetDateTime(Reader,5),
                        GetInt32(Reader,6),
                        GetDecimal(Reader,7),
                        GetString(Reader,8),
                        GetString(Reader,9),
                        GetString(Reader,10),
                        GetString(Reader,11),
                        GetString(Reader,12),
                        GetString(Reader,13)
                    )
                );
            }
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                Order_Details.AddOrThrow(
                    new Order_Details(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetDecimal(2),
                        Reader.GetInt16(3),
                        Reader.GetFloat(4)
                    )
                );
            }
        }
        e.Clear();
        Console.WriteLine($"Load {s.ElapsedMilliseconds,7}ms");
    }
    private static void Main() {
        Load();
        //Create();
#pragma warning disable CA1303 // ローカライズされるパラメーターとしてリテラルを渡さない
        Console.Write("終了");
#pragma warning restore CA1303 // ローカライズされるパラメーターとしてリテラルを渡さない
        Console.ReadKey();
    }
}