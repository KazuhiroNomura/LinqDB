using System;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Xml.Linq;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using LinqDB.Sets;
using AdventureWorks2017.Tables.dbo;
using AdventureWorks2017.Tables.HumanResources;
using AdventureWorks2017.Tables.Person;
using AdventureWorks2017.Tables.Production;
using AdventureWorks2017.Tables.Purchasing;
using AdventureWorks2017.Tables.Sales;
using AdventureWorks2017;
using System.Linq;
using System.Xml.XPath;
using System.Xml;
using System.Globalization;
using System.IO;
using Microsoft.SqlServer.Types;
using System.Text;
namespace TestAdventureWorks2019 {
    abstract class Program:共通 {
        [SuppressMessage("Globalization","CA1305:IFormatProvider を指定します",Justification = "<保留中>")]
        [SuppressMessage("Globalization","CA1303:ローカライズされるパラメーターとしてリテラルを渡さない",Justification = "<保留中>")]
        [SuppressMessage("ReSharper","AccessToModifiedClosure")]
        private static void Create() {
            using var Container = new Container();
            var dbo = Container.dbo;
            var HumanResources = Container.HumanResources;
            var Person = Container.Person;
            var Production = Container.Production;
            var Purchasing = Container.Purchasing;
            var Sales = Container.Sales;
            var AWBuildVersion = dbo.AWBuildVersion;
            var DatabaseLog = dbo.DatabaseLog;
            var ErrorLog = dbo.ErrorLog;
            var Department = HumanResources.Department;
            var Shift = HumanResources.Shift;
            var AddressType = Person.AddressType;
            var BusinessEntity = Person.BusinessEntity;
            var ContactType = Person.ContactType;
            var CountryRegion = Person.CountryRegion;
            var PhoneNumberType = Person.PhoneNumberType;
            var Culture = Production.Culture;
            var Illustration = Production.Illustration;
            var Location = Production.Location;
            var ProductCategory = Production.ProductCategory;
            var ProductDescription = Production.ProductDescription;
            var ProductModel = Production.ProductModel;
            var ProductPhoto = Production.ProductPhoto;
            var ScrapReason = Production.ScrapReason;
            var TransactionHistoryArchive = Production.TransactionHistoryArchive;
            var UnitMeasure = Production.UnitMeasure;
            var ShipMethod = Purchasing.ShipMethod;
            var CreditCard = Sales.CreditCard;
            var Currency = Sales.Currency;
            var SalesReason = Sales.SalesReason;
            var SpecialOffer = Sales.SpecialOffer;
            var s = Stopwatch.StartNew();
            const int スケール = 2;
            //LV0
            for(var ID = 0;ID<2;ID++) {
                AWBuildVersion.AddOrThrow(
                    new AWBuildVersion(
                        (byte)ID,"",DateTime.Now,DateTime.Now
                    )
                );
                DatabaseLog.AddOrThrow(
                    new DatabaseLog(
                        ID,DateTime.Now,"","",null,null,"",XDocument.Parse("<ROOT />")
                    )
                );
                ErrorLog.AddOrThrow(
                    new ErrorLog(
                        ID,DateTime.Now,"",0,null,null,null,null,""
                    )
                );
                ProductPhoto.AddOrThrow(
                    new ProductPhoto(
                        ID,Array.Empty<byte>(),"",Array.Empty<byte>(),"",DateTime.Now
                    )
                );
                Location.AddOrThrow(
                    new Location((short)ID,"",0,0,DateTime.Now)
                );
                ProductCategory.AddOrThrow(
                    new ProductCategory(ID,"",Guid.NewGuid(),DateTime.Now)
                );
                Department.AddOrThrow(
                    new Department((short)ID,"","",DateTime.Now)
                );
                Shift.AddOrThrow(
                    new Shift((byte)ID,"",new TimeSpan(ID),new TimeSpan(ID+1),DateTime.Now)
                );
                CountryRegion.AddOrThrow(
                    new CountryRegion(ID.ToString(),"",DateTime.Now)
                );
                Currency.AddOrThrow(
                    new Currency(ID.ToString(),"",DateTime.Now)
                );
                ContactType.AddOrThrow(
                    new ContactType(ID,"",DateTime.Now)
                );
                BusinessEntity.AddOrThrow(
                    new BusinessEntity(ID,Guid.NewGuid(),DateTime.Now)
                );
                AddressType.AddOrThrow(
                    new AddressType(ID,"",Guid.NewGuid(),DateTime.Now)
                );
                PhoneNumberType.AddOrThrow(
                    new PhoneNumberType(ID,"",DateTime.Now)
                );
                Culture.AddOrThrow(
                    new Culture(ID.ToString(),"",DateTime.Now)
                );
                Illustration.AddOrThrow(
                    new Illustration(ID,XDocument.Parse("<ROOT />"),DateTime.Now)
                );
                ProductDescription.AddOrThrow(
                    new ProductDescription(ID,"",Guid.NewGuid(),DateTime.Now)
                );
                ProductModel.AddOrThrow(
                    new ProductModel(ID,"",null,null,Guid.NewGuid(),DateTime.Now)
                );
                ScrapReason.AddOrThrow(
                    new ScrapReason((short)ID,"",DateTime.Now)
                );
                TransactionHistoryArchive.AddOrThrow(
                    new TransactionHistoryArchive(ID,0,0,0,DateTime.Now,"",0,0,DateTime.Now)
                );
                UnitMeasure.AddOrThrow(
                    new UnitMeasure(ID.ToString(),"",DateTime.Now)
                );
                ShipMethod.AddOrThrow(
                    new ShipMethod(ID,"",0,0,Guid.NewGuid(),DateTime.Now)
                );
                CreditCard.AddOrThrow(
                    new CreditCard(ID,"","",0,0,DateTime.Now)
                );
                SalesReason.AddOrThrow(
                    new SalesReason(ID,"","",DateTime.Now)
                );
                SpecialOffer.AddOrThrow(
                    new SpecialOffer(ID,"",0,"","",DateTime.Now,DateTime.Now,0,null,Guid.NewGuid(),DateTime.Now)
                );
            }
            var Person0 = Person.Person;
            E(
                BusinessEntity,
                a => Person0.AddOrThrow(
                    new Person(
                        a.BusinessEntityID,
                        "",
                        false,
                        null,
                        "",
                        null,
                        "",
                        null,
                        0,
                        null,
                        null,
                        Guid.NewGuid(),
                        DateTime.Now
                    )
                )
            );
            var PersonPhone = Person.PersonPhone;
            var PhoneNumber = 0;
            E(
                Person.Person,
                a => E(
                    PhoneNumberType,
                    b => {
                        for(var c = 0;c<2;c++) {
                            PersonPhone.AddOrThrow(
                                new PersonPhone(
                                    a.BusinessEntityID,
                                    PhoneNumber++.ToString(),
                                    b.PhoneNumberTypeID,
                                    DateTime.Now
                                )
                            );
                        }
                    }
                )
            );
            var ProductModelIllustration = Production.ProductModelIllustration;
            E(
                ProductModel,
                a => E(
                    Illustration,
                    b => ProductModelIllustration.AddOrThrow(
                        new ProductModelIllustration(
                            a.ProductModelID,
                            b.IllustrationID,
                            DateTime.Now
                        )
                    )
                )
            );
            var ProductModelProductDescriptionCulture = Production.ProductModelProductDescriptionCulture;
            E(
                ProductModel,
                a => E(
                    ProductDescription,
                    b => E(
                        Culture,
                        c => ProductModelProductDescriptionCulture.AddOrThrow(
                            new ProductModelProductDescriptionCulture(
                                a.ProductModelID,
                                b.ProductDescriptionID,
                                c.CultureID,
                                DateTime.Now
                            )
                        )
                    )
                )
            );
            var ProductSubcategory = Production.ProductSubcategory;
            var ProductModelID = 0;
            E(
                ProductCategory,
                a => ProductSubcategory.AddOrThrow(
                    new ProductSubcategory(
                        ProductModelID++,
                        a.ProductCategoryID,
                        "",
                        Guid.NewGuid(),
                        DateTime.Now
                    )
                )
            );
            var Vendor = Purchasing.Vendor;
            E(
                BusinessEntity,
                a => Vendor.AddOrThrow(
                    new Vendor(
                        a.BusinessEntityID,
                        "",
                        "",
                        0,
                        false,
                        false,
                        null,
                        DateTime.Now
                    )
                )
            );
            var CountryRegionCurrency = Sales.CountryRegionCurrency;
            E(
                CountryRegion,
                a => E(
                    Currency,
                    b => CountryRegionCurrency.AddOrThrow(
                        new CountryRegionCurrency(
                            a.CountryRegionCode,
                            b.CurrencyCode,
                            DateTime.Now
                        )
                    )
                )
            );
            var CurrencyRate = Sales.CurrencyRate;
            var CurrencyRateID = 0;
            E(
                Currency,
                a => E(
                    Currency,
                    b => CurrencyRate.AddOrThrow(
                        new CurrencyRate(
                            CurrencyRateID++,
                            DateTime.Now,
                            a.CurrencyCode,
                            b.CurrencyCode,
                            0,
                            0,
                            DateTime.Now
                        )
                    )
                )
            );
            var PersonCreditCard = Sales.PersonCreditCard;
            E(
                Person0,
                a => E(
                    CreditCard,
                    b => PersonCreditCard.AddOrThrow(
                        new PersonCreditCard(
                            a.BusinessEntityID,
                            b.CreditCardID,
                            DateTime.Now
                        )
                    )
                )
            );
            var SalesTerritory = Sales.SalesTerritory;
            var TerritoryID = 0;
            E(
                CountryRegion,
                a => {
                    for(var b = 0;b<2;b++) {
                        SalesTerritory.AddOrThrow(
                            new SalesTerritory(
                                TerritoryID++,
                                "",
                                a.CountryRegionCode,
                                "",
                                0,
                                0,
                                0,
                                0,
                                Guid.NewGuid(),
                                DateTime.Now
                            )
                        );
                    }
                }
            );
            var Employee = HumanResources.Employee;
            E(
                Person0,
                a => Employee.AddOrThrow(
                    new Employee(
                        a.BusinessEntityID,
                        "",
                        "",
                        null,
                        null,
                        "",DateTime.Now,
                        "",
                        "",
                        DateTime.Now,
                        false,0,
                        0,
                        false,
                        Guid.NewGuid(),
                        DateTime.Now
                    )
                )
            );
            var EmployeeDepartmentHistory = HumanResources.EmployeeDepartmentHistory;
            E(
                Employee,
                a => E(
                    Department,
                    b => E(
                        Shift,
                        c => EmployeeDepartmentHistory.AddOrThrow(
                            new EmployeeDepartmentHistory(
                                a.BusinessEntityID,
                                b.DepartmentID,
                                c.ShiftID,
                                DateTime.Now,
                                DateTime.Now,
                                DateTime.Now
                            )
                        )
                    )
                )
            );
            var EmployeePayHistory = HumanResources.EmployeePayHistory;
            var RateChangeDate = DateTime.Now;
            E(
                Employee,
                a => EmployeePayHistory.AddOrThrow(
                    new EmployeePayHistory(
                        a.BusinessEntityID,
                        RateChangeDate.AddDays(a.BusinessEntityID),
                        0,
                        0,
                        DateTime.Now
                    )
                )
            );
            var JobCandidate = HumanResources.JobCandidate;
            var JobCandidateID = 0;
            E(
                Employee,
                a => {
                    for(var b = 0;b<2;b++) {
                        JobCandidate.AddOrThrow(
                            new JobCandidate(
                                JobCandidateID++,
                                a.BusinessEntityID,
                                null,
                                DateTime.Now
                            )
                        );
                    }
                }
            );
            var BusinessEntityContact = Person.BusinessEntityContact;
            E(
                BusinessEntity,
                a => E(
                    Person0,
                    b => E(
                        ContactType,
                        c => BusinessEntityContact.AddOrThrow(
                            new BusinessEntityContact(
                                a.BusinessEntityID,
                                b.BusinessEntityID,
                                c.ContactTypeID,
                                Guid.NewGuid(),
                                DateTime.Now
                            )
                        )
                    )
                )
            );
            var EmailAddress = Person.EmailAddress;
            var EmailAddressID = 0;
            E(
                Person0,
                a => EmailAddress.AddOrThrow(
                    new EmailAddress(
                        a.BusinessEntityID,
                        EmailAddressID++,
                        null,
                        Guid.NewGuid(),
                        DateTime.Now
                    )
                )
            );
            var Password = Person.Password;
            E(
                Person0,
                a => Password.AddOrThrow(
                    new Password(
                        a.BusinessEntityID,
                        "",
                        "",
                        Guid.NewGuid(),
                        DateTime.Now
                    )
                )
            );
            var StateProvince = Person.StateProvince;
            var StateProvinceID = 0;
            E(
                CountryRegion,
                a => E(
                    SalesTerritory,
                    b => {
                        for(var c = 0;c<2;c++) {
                            StateProvince.AddOrThrow(
                                new StateProvince(
                                    StateProvinceID++,
                                    "",
                                    a.CountryRegionCode,
                                    false,
                                    "",
                                    b.TerritoryID,
                                    Guid.NewGuid(),
                                    DateTime.Now
                                )
                            );
                        }
                    }
                )
            );
            var Document = Production.Document;
            var sb=new StringBuilder("/0/");
            short DocumentLevel=0;
            E(
                Employee,
                _=>{
                    for(var b=0;b<2;b++){
                        var DocumentNode=new SqlHierarchyId();
                        Document.AddOrThrow(
                            new Document(
                                DocumentNode,
                                DocumentLevel,
                                "",
                                b,
                                false,
                                "",
                                "",
                                "",
                                0,
                                0,
                                null,
                                null,
                                Guid.NewGuid(),
                                DateTime.Now
                            )
                        );
                        var c = sb.Length-2;
                        while(true) {
                            if(c==-1) {
                                sb[1]='1';
                                c=3;
                                while(c<sb.Length) {
                                    sb[c]='0';
                                    c+=2;
                                }
                                sb.Append("0/");
                                break;
                            }
                            if(sb[c]=='0') {
                                sb[c]='1';
                                while((c+=2)<sb.Length) {
                                    sb[c]='0';
                                }
                                break;
                            }
                            c-=2;
                        }
                        DocumentLevel++;
                    }
                }
            );
            var Product = Production.Product;
            var ProductID = 0;
            E(
                UnitMeasure,
                a => E(
                    UnitMeasure,
                    b => E(
                        ProductSubcategory,
                        c => E(
                            ProductModel,
                            d => {
                                for(var e = 0;e<2;e++) {
                                    Product.AddOrThrow(
                                        new Product(
                                            ProductID++,
                                            "",
                                            "",
                                            false,
                                            false,
                                            null,
                                            0,
                                            0,
                                            0,
                                            0,
                                            null,
                                            a.UnitMeasureCode,
                                            b.UnitMeasureCode,
                                            0,
                                            0,null,
                                            null,
                                            null,
                                            c.ProductSubcategoryID,
                                            d.ProductModelID,
                                            DateTime.Now,
                                            null,
                                            null,
                                            Guid.NewGuid(),
                                            DateTime.Now
                                        )
                                    );
                                }
                            }
                        )
                    )
                )
            );
            var ProductCostHistory = Production.ProductCostHistory;
            var ProductCostHistory_Key = 0;
            E(
                Product,
                a => {
                    for(var b = 0;b<2;b++) {
                        ProductCostHistory.AddOrThrow(
                            new ProductCostHistory(
                                a.ProductID,
                                new DateTime(2001,1,1).AddDays(ProductCostHistory_Key),
                                null,
                                0,
                                DateTime.Now
                            )
                        );
                        ProductCostHistory_Key++;
                    }
                }
            );
            var ProductDocument = Production.ProductDocument;
            ProductID=0;
            E(
                Document,
                a => {
                    for(var b = 0;b<2;b++) {
                        ProductDocument.AddOrThrow(
                            new ProductDocument(
                                ProductID,
                                a.DocumentNode,
                                DateTime.Now
                            )
                        );
                        ProductID++;
                    }
                }
            );
            var ProductInventory = Production.ProductInventory;
            E(
                Product,
                a => E(
                    Location,
                    b => ProductInventory.AddOrThrow(
                        new ProductInventory(
                            a.ProductID,
                            b.LocationID,
                            "",
                            0,
                            0,
                            Guid.NewGuid(),
                            DateTime.Now
                        )
                    )
                )
            );
            var ProductListPriceHistory = Production.ProductListPriceHistory;
            E(
                Product,
                a => {
                    for(var b = 0;b<2;b++) {
                        ProductListPriceHistory.AddOrThrow(
                            new ProductListPriceHistory(
                                a.ProductID,
                                new DateTime(2001,1,1).AddDays(b),
                                null,
                                0,
                                DateTime.Now
                            )
                        );
                    }
                }
            );
            var ProductProductPhoto = Production.ProductProductPhoto;
            E(
                Product,
                a => E(
                    ProductPhoto,
                    b => ProductProductPhoto.AddOrThrow(
                        new ProductProductPhoto(
                            a.ProductID,
                            b.ProductPhotoID,
                            false,
                            DateTime.Now
                        )
                    )
                )
            );
            var ProductReview = Production.ProductReview;
            var ProductReviewID = 0;
            E(
                Product,
                a => {
                    for(var b = 0;b<2;b++) {
                        ProductReview.AddOrThrow(
                            new ProductReview(
                                ProductReviewID++,
                                a.ProductID,
                                "",
                                DateTime.Now,
                                "",
                                0,
                                null,
                                DateTime.Now
                            )
                        );
                    }
                }
            );
            var TransactionHistory = Production.TransactionHistory;
            var TransactionHistoryID = 0;
            E(
                Product,
                a => {
                    for(var b = 0;b<2;b++) {
                        TransactionHistory.AddOrThrow(
                            new TransactionHistory(
                                TransactionHistoryID++,
                                a.ProductID,
                                0,
                                0,
                                DateTime.Now,
                                "",
                                0,
                                0,
                                DateTime.Now
                            )
                        );
                    }
                }
            );
            var WorkOrder = Production.WorkOrder;
            var WorkOrderID = 0;
            E(
                Product,
                a => E(
                    ScrapReason,
                    b => {
                        WorkOrder.AddOrThrow(
                            new WorkOrder(
                                WorkOrderID++,
                                a.ProductID,
                                0,
                                0,
                                0,
                                DateTime.Now,
                                null,
                                DateTime.Now,
                                b.ScrapReasonID,
                                DateTime.Now
                            )
                        );
                        WorkOrder.AddOrThrow(
                            new WorkOrder(
                                WorkOrderID++,
                                a.ProductID,
                                0,
                                0,
                                0,
                                DateTime.Now,
                                null,
                                DateTime.Now,
                                null,
                                DateTime.Now
                            )
                        );
                    }
                )
            );
            var WorkOrderRouting = Production.WorkOrderRouting;
            var OperationSequence = 0;
            E(
                WorkOrder,
                a => E(
                    Product,
                    b => E(
                        Location,
                        c => {
                            for(var d = 0;d<2;d++) {
                                WorkOrderRouting.AddOrThrow(
                                    new WorkOrderRouting(
                                        a.WorkOrderID,
                                        b.ProductID,
                                        (short)OperationSequence++,
                                        c.LocationID,
                                        DateTime.Now,
                                        DateTime.Now,
                                        null,
                                        null,
                                        null,
                                        0,
                                        null,
                                        DateTime.Now
                                    )
                                );
                            }
                        }
                    )
                )
            );
            var ProductVendor = Purchasing.ProductVendor;
            var ProductVendor_UnitMeasureCode = 0;
            E(
                Product,
                a => E(
                    Vendor,
                    b => ProductVendor.AddOrThrow(
                        new ProductVendor(
                            a.ProductID,
                            b.BusinessEntityID,
                            0,
                            0,
                            null,
                            null,
                            0,
                            0,
                            null,
                            (ProductVendor_UnitMeasureCode++%スケール).ToString(),
                            DateTime.Now
                        )
                    )
                )
            );
            var PurchaseOrderHeader = Purchasing.PurchaseOrderHeader;
            var PurchaseOrderID = 0;
            E(
                Employee,
                a => E(
                    Vendor,
                    b => E(
                        ShipMethod,
                        c => {
                            PurchaseOrderHeader.AddOrThrow(
                                new PurchaseOrderHeader(
                                    PurchaseOrderID++,
                                    0,
                                    0,
                                    a.BusinessEntityID,
                                    b.BusinessEntityID,
                                    c.ShipMethodID,
                                    DateTime.Now,
                                    null,
                                    0,
                                    0,
                                    0,
                                    0,
                                    DateTime.Now
                                )
                            );
                        }
                    )
                )
            );
            var SalesPerson = Sales.SalesPerson;
            var SalesPerson_TerritoryID = 0;
            E(
                Employee,
                a => {
                    SalesPerson.AddOrThrow(
                        new SalesPerson(
                            a.BusinessEntityID,
                            TerritoryID%2==0 ? (int)(SalesPerson_TerritoryID%SalesTerritory.Count) : (int?)null,
                            null,
                            0,
                            0,
                            0,
                            0,
                            Guid.NewGuid(),
                            DateTime.Now
                        )
                    );
                    SalesPerson_TerritoryID++;
                }
            );
            var SalesPersonQuotaHistory = Sales.SalesPersonQuotaHistory;
            E(
                SalesPerson,
                a => {
                    for(var b = 0;b<2;b++) {
                        SalesPersonQuotaHistory.AddOrThrow(
                            new SalesPersonQuotaHistory(
                                a.BusinessEntityID,
                                new DateTime(2001,1,1).AddDays(b),
                                0,Guid.NewGuid(),
                                DateTime.Now
                            )
                        );
                    }
                }
            );
            var SalesTaxRate = Sales.SalesTaxRate;
            var SalesTaxRateID = 0;
            E(
                StateProvince,
                a => {
                    for(var b = 0;b<2;b++) {
                        SalesTaxRate.AddOrThrow(
                            new SalesTaxRate(
                                SalesTaxRateID++,
                                a.StateProvinceID,
                                0,
                                0,
                                "",
                                Guid.NewGuid(),
                                DateTime.Now
                            )
                        );
                    }
                }
            );
            var SalesTerritoryHistory = Sales.SalesTerritoryHistory;
            E(
                Person0,
                a => E(
                    SalesTerritory,
                    b => {
                        for(var c = 0;c<2;c++) {
                            SalesTerritoryHistory.AddOrThrow(
                                new SalesTerritoryHistory(
                                    a.BusinessEntityID,
                                    b.TerritoryID,
                                    new DateTime(2001,1,1).AddDays(c),
                                    null,
                                    Guid.NewGuid(),
                                    DateTime.Now
                                )
                            );
                        }
                    }
                )
            );
            var ShoppingCartItem = Sales.ShoppingCartItem;
            var ShoppingCartItemID = 0;
            E(
                Product,
                a => {
                    for(var b = 0;b<2;b++) {
                        ShoppingCartItem.AddOrThrow(
                            new ShoppingCartItem(
                                ShoppingCartItemID++,
                                "",
                                0,
                                a.ProductID,
                                DateTime.Now,
                                DateTime.Now
                            )
                        );
                    }
                }
            );
            var SpecialOfferProduct = Sales.SpecialOfferProduct;
            E(
                SpecialOffer,
                a => E(
                    Product,
                    b => SpecialOfferProduct.AddOrThrow(
                        new SpecialOfferProduct(
                            a.SpecialOfferID,
                            b.ProductID,
                            Guid.NewGuid(),
                            DateTime.Now
                        )
                    )
                )
            );
            var Store = Sales.Store;
            E(
                SalesPerson,
                a => Store.AddOrThrow(
                    new Store(
                        a.BusinessEntityID,
                        "",
                        null,
                        null,
                        Guid.NewGuid(),
                        DateTime.Now
                    )
                )
            );
            var Address = Person.Address;
            var AddressID = 0;
            E(
                StateProvince,
                a => Address.AddOrThrow(
                    new Address(
                        AddressID++,
                        "",
                        null,
                        "",
                        a.StateProvinceID,
                        "",
                        null,
                        Guid.NewGuid(),
                        DateTime.Now
                    )
                )
            );
            var BusinessEntityAddress = Person.BusinessEntityAddress;
            E(
                BusinessEntity,
                a => E(
                    Address,
                    b => E(
                        AddressType,
                        c => BusinessEntityAddress.AddOrThrow(
                            new BusinessEntityAddress(
                                a.BusinessEntityID,
                                b.AddressID,
                                c.AddressTypeID,
                                Guid.NewGuid(),
                                DateTime.Now
                            )
                        )
                    )
                )
            );
            var BillOfMaterials = Production.BillOfMaterials;
            var BillOfMaterialsID = 0;
            E(
                UnitMeasure,
                a => BillOfMaterials.AddOrThrow(
                    new BillOfMaterials(
                        BillOfMaterialsID++,
                        null,
                        0,
                        DateTime.Now,
                        null,
                        a.UnitMeasureCode,
                        0,
                        0,
                        DateTime.Now
                    )
                )
            );
            var PurchaseOrderDetail = Purchasing.PurchaseOrderDetail;
            var PurchaseOrderDetailID = 0;
            E(
                PurchaseOrderHeader,
                a => E(
                    Product,
                    b => PurchaseOrderDetail.AddOrThrow(
                        new PurchaseOrderDetail(
                            a.PurchaseOrderID,
                            PurchaseOrderDetailID++,
                            DateTime.Now,
                            0,
                            b.ProductID,
                            0,
                            0,
                            0,
                            0,
                            0,
                            DateTime.Now
                        )
                    )
                )
            );
            var Customer = Sales.Customer;
            var CustomerID = 0;
            E(
                Person0,
                a => E(
                    Store,
                    b => E(
                        SalesTerritory,
                        c => {
                            for(var d = 0;d<2;d++) {
                                Customer.AddOrThrow(
                                    new Customer(
                                        CustomerID++,
                                        a.BusinessEntityID,
                                        b.BusinessEntityID,
                                        c.TerritoryID,
                                        "",
                                        Guid.NewGuid(),
                                        DateTime.Now
                                    )
                                );
                            }
                        }
                    )
                )
            );
            var SalesOrderHeader = Sales.SalesOrderHeader;
            //customer
            //SalesPerson
            //SalesTerritory.TerittoryID
            //Address.BillToAddress BillToAddress
            //Address.ShipToAddressID
            //ShipMethod.ShipMethodID
            //cREDITcARD
            //CurrencyRate.CurrencyRateID
            //
            var SalesOrderID = 0;
            E(
                Customer,
                a => E(
                    SalesPerson,
                    b => E(
                        SalesTerritory,
                        c => E(
                            Address,
                            d => E(
                                Address,
                                e => E(
                                    ShipMethod,
                                    f => E(
                                        CreditCard,
                                        g => E(
                                            CurrencyRate,
                                            h => {
                                                for(var i = 0;i<2;i++) {
                                                    SalesOrderHeader.AddOrThrow(
                                                        new SalesOrderHeader(
                                                            SalesOrderID++,
                                                            0,
                                                            DateTime.Now,
                                                            DateTime.Now,
                                                            null,
                                                            0,
                                                            false,
                                                            "",
                                                            null,
                                                            null,
                                                            a.CustomerID,
                                                            b.BusinessEntityID,
                                                            c.TerritoryID,
                                                            d.AddressID,
                                                            e.AddressID,
                                                            f.ShipMethodID,
                                                            g.CreditCardID,
                                                            null,
                                                            h.CurrencyRateID,
                                                            0,
                                                            0,
                                                            0,
                                                            0,
                                                            null,
                                                            Guid.NewGuid(),
                                                            DateTime.Now
                                                        )
                                                    );
                                                }
                                            }
                                        )
                                    )
                                )
                            )
                        )
                    )
                )
            );
            var SalesOrderHeaderSalesReason = Sales.SalesOrderHeaderSalesReason;
            E(
                SalesOrderHeader,
                a => E(
                    SalesReason,
                    b => SalesOrderHeaderSalesReason.AddOrThrow(
                        new SalesOrderHeaderSalesReason(
                            a.SalesOrderID,
                            b.SalesReasonID,
                            DateTime.Now
                        )
                    )
                )
            );
            var SalesOrderDetail = Sales.SalesOrderDetail;
            var SalesOrderDetailID = 0;
            E(
                SalesOrderHeader,
                a => E(
                    SpecialOfferProduct,
                    b => {
                        for(var c = 0;c<2;c++) {
                            SalesOrderDetail.AddOrThrow(
                                new SalesOrderDetail(
                                    a.SalesOrderID,
                                    SalesOrderDetailID++,
                                    null,
                                    0,
                                    b.ProductID,
                                    b.SpecialOfferID,
                                    0,
                                    0,
                                    0,
                                    Guid.NewGuid(),
                                    DateTime.Now
                                )
                            );
                        }
                    }
                )
            );
            Console.WriteLine($"Create {s.ElapsedMilliseconds,7}ms");
        }

        [SuppressMessage("Globalization","CA1305:IFormatProvider を指定します",Justification = "<保留中>")]
        private static void Transaction(Func<int> Switchパターン) {
            const int 試行回数 = 10000;
            using var Container = new Container();
            var dbo = Container.dbo;
            var HumanResources = Container.HumanResources;
            var Person = Container.Person;
            var Production = Container.Production;
            var Purchasing = Container.Purchasing;
            var Sales = Container.Sales;
            var AWBuildVersion = dbo.AWBuildVersion;
            var DatabaseLog = dbo.DatabaseLog;
            var ErrorLog = dbo.ErrorLog;
            var Department = HumanResources.Department;
            var Shift = HumanResources.Shift;
            var AddressType = Person.AddressType;
            var BusinessEntity = Person.BusinessEntity;
            var ContactType = Person.ContactType;
            var CountryRegion = Person.CountryRegion;
            var PhoneNumberType = Person.PhoneNumberType;
            var Culture = Production.Culture;
            var Illustration = Production.Illustration;
            var Location = Production.Location;
            var ProductCategory = Production.ProductCategory;
            var ProductDescription = Production.ProductDescription;
            var ProductModel = Production.ProductModel;
            var ProductPhoto = Production.ProductPhoto;
            var ScrapReason = Production.ScrapReason;
            var TransactionHistoryArchive = Production.TransactionHistoryArchive;
            var UnitMeasure = Production.UnitMeasure;
            var ShipMethod = Purchasing.ShipMethod;
            var CreditCard = Sales.CreditCard;
            var Currency = Sales.Currency;
            var SalesReason = Sales.SalesReason;
            var SpecialOffer = Sales.SpecialOffer;
            var Person_Person = Person.Person;
            var PersonPhone = Person.PersonPhone;
            var ProductModelIllustration = Production.ProductModelIllustration;
            var ProductModelProductDescriptionCulture = Production.ProductModelProductDescriptionCulture;
            var ProductSubcategory = Production.ProductSubcategory;
            var Vendor = Purchasing.Vendor;
            var CountryRegionCurrency = Sales.CountryRegionCurrency;
            var CurrencyRate = Sales.CurrencyRate;
            var PersonCreditCard = Sales.PersonCreditCard;
            var SalesTerritory = Sales.SalesTerritory;
            var Employee = HumanResources.Employee;
            var EmployeeDepartmentHistory = HumanResources.EmployeeDepartmentHistory;
            var EmployeePayHistory = HumanResources.EmployeePayHistory;
            var JobCandidate = HumanResources.JobCandidate;
            var BusinessEntityContact = Person.BusinessEntityContact;
            var EmailAddress = Person.EmailAddress;
            var Password = Person.Password;
            var StateProvince = Person.StateProvince;
            var Document = Production.Document;
            var Product = Production.Product;
            var ProductCostHistory = Production.ProductCostHistory;
            var ProductDocument = Production.ProductDocument;
            var ProductInventory = Production.ProductInventory;
            var ProductListPriceHistory = Production.ProductListPriceHistory;
            var ProductProductPhoto = Production.ProductProductPhoto;
            var ProductReview = Production.ProductReview;
            var TransactionHistory = Production.TransactionHistory;
            var WorkOrder = Production.WorkOrder;
            var WorkOrderRouting = Production.WorkOrderRouting;
            var ProductVendor = Purchasing.ProductVendor;
            var PurchaseOrderHeader = Purchasing.PurchaseOrderHeader;
            var SalesPerson = Sales.SalesPerson;
            var SalesPersonQuotaHistory = Sales.SalesPersonQuotaHistory;
            var SalesTaxRate = Sales.SalesTaxRate;
            var SalesTerritoryHistory = Sales.SalesTerritoryHistory;
            var ShoppingCartItem = Sales.ShoppingCartItem;
            var SpecialOfferProduct = Sales.SpecialOfferProduct;
            var Store = Sales.Store;
            var Address = Person.Address;
            var BusinessEntityAddress = Person.BusinessEntityAddress;
            var BillOfMaterials = Production.BillOfMaterials;
            var PurchaseOrderDetail = Purchasing.PurchaseOrderDetail;
            var Customer = Sales.Customer;
            var SalesOrderHeader = Sales.SalesOrderHeader;
            var SalesOrderHeaderSalesReason = Sales.SalesOrderHeaderSalesReason;
            var SalesOrderDetail = Sales.SalesOrderDetail;

            var s = Stopwatch.StartNew();
            var AWBuildVersion情報 = new AddDel情報();
            var DatabaseLog情報 = new AddDel情報();
            var ErrorLog情報 = new AddDel情報();
            var Department情報 = new AddDel情報();
            var Shift情報 = new AddDel情報();
            var AddressType情報 = new AddDel情報();
            var BusinessEntity情報 = new AddDel情報();
            var ContactType情報 = new AddDel情報();
            var CountryRegion情報 = new AddDel情報();
            var PhoneNumberType情報 = new AddDel情報();
            var Culture情報 = new AddDel情報();
            var Illustration情報 = new AddDel情報();
            var Location情報 = new AddDel情報();
            var ProductCategory情報 = new AddDel情報();
            var ProductDescription情報 = new AddDel情報();
            var ProductModel情報 = new AddDel情報();
            var ProductPhoto情報 = new AddDel情報();
            var ScrapReason情報 = new AddDel情報();
            var TransactionHistoryArchive情報 = new AddDel情報();
            var UnitMeasure情報 = new AddDel情報();
            var ShipMethod情報 = new AddDel情報();
            var CreditCard情報 = new AddDel情報();
            var Currency情報 = new AddDel情報();
            var SalesReason情報 = new AddDel情報();
            var SpecialOffer情報 = new AddDel情報();
            var Person情報 = new AddDel情報();
            var PersonPhone情報 = new AddDel情報();
            var ProductModelIllustration情報 = new AddDel情報();
            var ProductModelProductDescriptionCulture情報 = new AddDel情報();
            var ProductSubcategory情報 = new AddDel情報();
            var Vendor情報 = new AddDel情報();
            var CountryRegionCurrency情報 = new AddDel情報();
            var CurrencyRate情報 = new AddDel情報();
            var PersonCreditCard情報 = new AddDel情報();
            var SalesTerritory情報 = new AddDel情報();
            var Employee情報 = new AddDel情報();
            var EmployeeDepartmentHistory情報 = new AddDel情報();
            var EmployeePayHistory情報 = new AddDel情報();
            var JobCandidate情報 = new AddDel情報();
            var BusinessEntityContact情報 = new AddDel情報();
            var EmailAddress情報 = new AddDel情報();
            var Password情報 = new AddDel情報();
            var StateProvince情報 = new AddDel情報();
            var Document情報 = new AddDel情報();
            var Product情報 = new AddDel情報();
            var ProductCostHistory情報 = new AddDel情報();
            var ProductDocument情報 = new AddDel情報();
            var ProductInventory情報 = new AddDel情報();
            var ProductListPriceHistory情報 = new AddDel情報();
            var ProductProductPhoto情報 = new AddDel情報();
            var ProductReview情報 = new AddDel情報();
            var TransactionHistory情報 = new AddDel情報();
            var WorkOrder情報 = new AddDel情報();
            var WorkOrderRouting情報 = new AddDel情報();
            var ProductVendor情報 = new AddDel情報();
            var PurchaseOrderHeader情報 = new AddDel情報();
            var SalesPerson情報 = new AddDel情報();
            var SalesPersonQuotaHistory情報 = new AddDel情報();
            var SalesTaxRate情報 = new AddDel情報();
            var SalesTerritoryHistory情報 = new AddDel情報();
            var ShoppingCartItem情報 = new AddDel情報();
            var SpecialOfferProduct情報 = new AddDel情報();
            var Store情報 = new AddDel情報();
            var Address情報 = new AddDel情報();
            var BusinessEntityAddress情報 = new AddDel情報();
            var BillOfMaterials情報 = new AddDel情報();
            var PurchaseOrderDetail情報 = new AddDel情報();
            var Customer情報 = new AddDel情報();
            var SalesOrderHeader情報 = new AddDel情報();
            var SalesOrderHeaderSalesReason情報 = new AddDel情報();
            var SalesOrderDetail情報 = new AddDel情報();
            var Document_DocumentNode = new StringBuilder("/0/");
            var ProductDocument_DocumentNode = new StringBuilder("/0/");
            for(var a=0;a<試行回数;a++){
                switch(Switchパターン()%71) {
                    case 0: Add(ref AWBuildVersion情報,AWBuildVersion,new AWBuildVersion((byte)ID(AWBuildVersion),"",DateTime.Now,DateTime.Now)); break;
                    case 1: Add(ref DatabaseLog情報,DatabaseLog,new DatabaseLog(ID(DatabaseLog),DateTime.Now,"","",null,null,"",XDocument.Parse("<ROOT />"))); break;
                    case 2: Add(ref ErrorLog情報,ErrorLog,new ErrorLog(ID(ErrorLog),DateTime.Now,"",0,null,null,null,null,"")); break;
                    case 3: Add(ref Department情報,Department,new Department((short)ID(Department),"","",DateTime.Now)); break;
                    case 4: Add(ref Shift情報,Shift,new Shift((byte)ID(Shift),"",TimeSpan.Zero,TimeSpan.Zero,DateTime.Now)); break;
                    case 5: Add(ref AddressType情報,AddressType,new AddressType(ID(AddressType),"",Guid.NewGuid(),DateTime.Now)); break;
                    case 6: Add(ref BusinessEntity情報,BusinessEntity,new BusinessEntity(ID(BusinessEntity),Guid.NewGuid(),DateTime.Now)); break;
                    case 7: Add(ref ContactType情報,ContactType,new ContactType(ID(ContactType),"",DateTime.Now)); break;
                    case 8: Add(ref CountryRegion情報,CountryRegion,new CountryRegion(ID(CountryRegion).ToString(),"",DateTime.Now)); break;
                    case 9: Add(ref PhoneNumberType情報,PhoneNumberType,new PhoneNumberType(ID(PhoneNumberType),"",DateTime.Now)); break;
                    case 10: Add(ref Culture情報,Culture,new Culture(ID(Culture).ToString(),"",DateTime.Now)); break;
                    case 11: Add(ref Illustration情報,Illustration,new Illustration(ID(Illustration),null,DateTime.Now)); break;
                    case 12: Add(ref Location情報,Location,new Location((short)ID(Location),"",0,0,DateTime.Now)); break;
                    case 13: Add(ref ProductCategory情報,ProductCategory,new ProductCategory(ID(ProductCategory),"",Guid.NewGuid(),DateTime.Now)); break;
                    case 14: Add(ref ProductDescription情報,ProductDescription,new ProductDescription(ID(ProductDescription),"",Guid.NewGuid(),DateTime.Now)); break;
                    case 15: Add(ref ProductModel情報,ProductModel,new ProductModel(ID(ProductModel),"",null,null,Guid.NewGuid(),DateTime.Now)); break;
                    case 16: Add(ref ProductPhoto情報,ProductPhoto,new ProductPhoto(ID(ProductPhoto),null,null,null,null,DateTime.Now)); break;
                    case 17: Add(ref ScrapReason情報,ScrapReason,new ScrapReason((short)ID(ScrapReason),"",DateTime.Now)); break;
                    case 18: Add(ref TransactionHistoryArchive情報,TransactionHistoryArchive,new TransactionHistoryArchive(ID(ProductCategory),0,0,0,DateTime.Now,"",0,0,DateTime.Now)); break;
                    case 19: Add(ref UnitMeasure情報,UnitMeasure,new UnitMeasure(ID(UnitMeasure).ToString(),"",DateTime.Now)); break;
                    case 20: Add(ref ShipMethod情報,ShipMethod,new ShipMethod(ID(ShipMethod),"",0,0,Guid.NewGuid(),DateTime.Now)); break;
                    case 21: Add(ref CreditCard情報,CreditCard,new CreditCard(ID(CreditCard),"","",0,0,DateTime.Now)); break;
                    case 22: Add(ref Currency情報,Currency,new Currency(ID(Currency).ToString(),"",DateTime.Now)); break;
                    case 23: Add(ref SalesReason情報,SalesReason,new SalesReason(ID(SalesReason),"","",DateTime.Now)); break;
                    case 24: Add(ref SpecialOffer情報,SpecialOffer,new SpecialOffer(ID(SpecialOffer),"",0,"","",DateTime.Now,DateTime.Now,0,null,Guid.NewGuid(),DateTime.Now)); break;
                    case 25:
                        if(BusinessEntity.Count==0) goto case 6;
                        Add(
                            ref Person情報,
                            Person_Person,
                            new Person(
                                BusinessEntity.Sampling.BusinessEntityID,
                                ID(Person_Person).ToString(),false,null,"",null,"",null,0,null,null,Guid.NewGuid(),DateTime.Now
                            )
                        );
                        break;
                    case 26:
                        if(Person_Person.Count==0) goto case 25;
                        if(PhoneNumberType.Count==0) goto case 9;
                        Add(
                            ref PersonPhone情報,
                            PersonPhone,
                            new PersonPhone(
                                Person_Person.Sampling.BusinessEntityID,
                                "",
                                PhoneNumberType.Sampling.PhoneNumberTypeID,
                                DateTime.Now
                            )
                        );
                        break;
                    case 27:
                        if(ProductModel.Count==0) goto case 15;
                        if(Illustration.Count==0) goto case 11;
                        Add(
                            ref ProductModelIllustration情報,
                            ProductModelIllustration,
                            new ProductModelIllustration(
                                ProductModel.Sampling.ProductModelID,
                                Illustration.Sampling.IllustrationID,
                                DateTime.Now
                            )
                        );
                        break;
                    case 28:
                        if(ProductModel.Count==0) goto case 15;
                        if(ProductDescription.Count==0) goto case 14;
                        if(Culture.Count==0) goto case 10;
                        Add(
                            ref ProductModelProductDescriptionCulture情報,
                            ProductModelProductDescriptionCulture,
                            new ProductModelProductDescriptionCulture(
                                ProductModel.Sampling.ProductModelID,
                                ProductDescription.Sampling.ProductDescriptionID,
                                Culture.Sampling.CultureID,DateTime.Now
                            )
                        );
                        break;
                    case 29:
                        if(ProductCategory.Count==0) goto case 13;
                        Add(
                            ref ProductSubcategory情報,
                            ProductSubcategory,
                            new ProductSubcategory(
                                ID(ProductSubcategory),
                                ProductCategory.Sampling.ProductCategoryID,
                                "",Guid.NewGuid(),DateTime.Now
                            )
                        );
                        break;
                    case 30:
                        if(BusinessEntity.Count==0) goto case 6;
                        Add(
                            ref Vendor情報,
                            Vendor,
                            new Vendor(
                                BusinessEntity.Sampling.BusinessEntityID,
                                "","",0,false,false,null,DateTime.Now
                            )
                        );
                        break;
                    case 31:
                        if(CountryRegion.Count==0) goto case 8;
                        if(Currency.Count==0) goto case 22;
                        Add(
                            ref CountryRegionCurrency情報,
                            CountryRegionCurrency,
                            new CountryRegionCurrency(
                                CountryRegion.Sampling.CountryRegionCode,
                                Currency.Sampling.CurrencyCode,
                                DateTime.Now
                            )
                        );
                        break;
                    case 32:
                        if(Currency.Count==0) goto case 22;
                        Add(
                            ref CurrencyRate情報,
                            CurrencyRate,
                            new CurrencyRate(
                                ID(CurrencyRate),DateTime.Now,
                                Currency.Sampling.CurrencyCode,
                                Currency.Sampling.CurrencyCode,
                                0,0,DateTime.Now
                            )
                        );
                        break;
                    case 33:
                        if(Person_Person.Count==0) goto case 25;
                        if(CreditCard.Count==0) goto case 21;
                        Add(
                            ref PersonCreditCard情報,
                            PersonCreditCard,
                            new PersonCreditCard(
                                Person_Person.Sampling.BusinessEntityID,
                                CreditCard.Sampling.CreditCardID,DateTime.Now
                            )
                        );
                        break;
                    case 34:
                        if(CountryRegion.Count==0) goto case 8;
                        Add(
                            ref SalesTerritory情報,
                            SalesTerritory,
                            new SalesTerritory(
                                ID(SalesTerritory),"",
                                CountryRegion.Sampling.CountryRegionCode,
                                "",0,0,0,0,Guid.NewGuid(),DateTime.Now
                            )
                        );
                        break;
                    case 35:
                        if(Person_Person.Count==0) goto case 25;
                        Add(
                            ref Employee情報,
                            Employee,
                            new Employee(
                                Person_Person.Sampling.BusinessEntityID,
                                "","",null,null,"",DateTime.Now,"","",DateTime.Now,false,0,0,false,Guid.NewGuid(),DateTime.Now
                            )
                        );
                        break;
                    case 36:
                        if(Person_Person.Count==0) goto case 25;
                        if(Department.Count==0) goto case 3;
                        if(Shift.Count==0) goto case 4;
                        Add(
                            ref EmployeeDepartmentHistory情報,
                            EmployeeDepartmentHistory,
                            new EmployeeDepartmentHistory(
                                Person_Person.Sampling.BusinessEntityID,
                                Department.Sampling.DepartmentID,
                                Shift.Sampling.ShiftID,
                                DateTime.Now,null,DateTime.Now
                            )
                        );
                        break;
                    case 37:
                        if(Person_Person.Count==0) goto case 25;
                        Add(
                            ref EmployeePayHistory情報,
                            EmployeePayHistory,
                            new EmployeePayHistory(
                                Person_Person.Sampling.BusinessEntityID,
                                DateTime.Now,0,0,DateTime.Now
                            )
                        );
                        break;
                    case 38:
                        if(Employee.Count==0) goto case 35;
                        Add(
                            ref JobCandidate情報,
                            JobCandidate,
                            new JobCandidate(
                                ID(JobCandidate),
                                Employee.Sampling.BusinessEntityID,
                                null,DateTime.Now
                            )
                        );
                        break;
                    case 39:
                        if(BusinessEntity.Count==0) goto case 6;
                        if(Person_Person.Count==0) goto case 25;
                        if(ContactType.Count==0) goto case 7;
                        Add(
                            ref BusinessEntityContact情報,
                            BusinessEntityContact,
                            new BusinessEntityContact(
                                BusinessEntity.Sampling.BusinessEntityID,
                                Person_Person.Sampling.BusinessEntityID,
                                ContactType.Sampling.ContactTypeID,
                                Guid.NewGuid(),DateTime.Now
                            )
                        );
                        break;
                    case 40:
                        if(BusinessEntity.Count==0) goto case 6;
                        Add(
                            ref EmailAddress情報,
                            EmailAddress,
                            new EmailAddress(
                                BusinessEntity.Sampling.BusinessEntityID,
                                ID(EmailAddress),null,Guid.NewGuid(),DateTime.Now
                            )
                        );
                        break;
                    case 41:
                        if(Person_Person.Count==0) goto case 25;
                        Add(
                            ref Password情報,
                            Password,
                            new Password(
                                Person_Person.Sampling.BusinessEntityID,
                                "","",Guid.NewGuid(),DateTime.Now
                            )
                        );
                        break;
                    case 42:
                        if(CountryRegion.Count==0) goto case 8;
                        if(SalesTerritory.Count==0) goto case 34;
                        Add(
                            ref StateProvince情報,
                            StateProvince,
                            new StateProvince(
                                ID(StateProvince),"",
                                CountryRegion.Sampling.CountryRegionCode,
                                false,"",
                                SalesTerritory.Sampling.TerritoryID,
                                Guid.NewGuid(),DateTime.Now
                            )
                        );
                        break;
                    case 43:{
                        if(Employee.Count==0) goto case 35;
                        var MemoryStream=new MemoryStream(Encoding.UTF8.GetBytes(Document_DocumentNode.ToString()));
                        var BinaryReader=new BinaryReader(MemoryStream);
                        var DocumentNode=new SqlHierarchyId();
                        DocumentNode.Read(BinaryReader);
                        Add(
                            ref Document情報,
                            Document,
                            new Document(
                                DocumentNode,
                                0,
                                "",
                                0,
                                false,
                                "",
                                "",
                                "",
                                0,
                                0,
                                null,
                                null,
                                Guid.NewGuid(),
                                DateTime.Now
                            )
                        );
                        break;
                    }
                    case 44:
                        if(UnitMeasure.Count==0) goto case 19;
                        Add(
                            ref Product情報,
                            Product,
                            new Product(
                                ID(Product),"","",false,false,null,0,0,0,0,null,
                                UnitMeasure.Sampling.UnitMeasureCode,
                                null,null,0,null,null,null,null,null,DateTime.Now,null,null,Guid.NewGuid(),DateTime.Now
                            )
                        );
                        break;
                    case 45:
                        if(Product.Count==0) goto case 44;
                        Add(
                            ref ProductCostHistory情報,
                            ProductCostHistory,
                            new ProductCostHistory(
                                Product.Sampling.ProductID,
                                DateTime.Now,null,0,DateTime.Now
                            )
                        );
                        break;
                    case 46:{
                        if(Document.Count==0) goto case 43;
                        var MemoryStream=new MemoryStream(Encoding.UTF8.GetBytes(ProductDocument_DocumentNode.ToString()));
                        var BinaryReader=new BinaryReader(MemoryStream);
                        var SqlHierarchyId=new SqlHierarchyId();
                        SqlHierarchyId.Read(BinaryReader);
                        Add(
                            ref ProductDocument情報,
                            ProductDocument,
                            new ProductDocument(
                                Product.Sampling.ProductID,
                                SqlHierarchyId,
                                DateTime.Now
                            )
                        );
                        break;
                    }
                    case 47:
                        if(Product.Count==0) goto case 44;
                        if(Location.Count==0) goto case 12;
                        Add(
                            ref ProductInventory情報,
                            ProductInventory,
                            new ProductInventory(
                                Product.Sampling.ProductID,
                                Location.Sampling.LocationID,
                                "",0,0,Guid.NewGuid(),DateTime.Now
                            )
                        );
                        break;
                    case 48:
                        if(Product.Count==0) goto case 44;
                        Add(
                            ref ProductListPriceHistory情報,
                            ProductListPriceHistory,
                            new ProductListPriceHistory(
                                Product.Sampling.ProductID,
                                DateTime.Now,null,0,DateTime.Now
                            )
                        );
                        break;
                    case 49:
                        if(Product.Count==0) goto case 44;
                        if(ProductPhoto.Count==0) goto case 16;
                        Add(
                            ref ProductProductPhoto情報,
                            ProductProductPhoto,
                            new ProductProductPhoto(
                                Product.Sampling.ProductID,
                                ProductPhoto.Sampling.ProductPhotoID,
                                false,DateTime.Now
                            )
                        );
                        break;
                    case 50:
                        if(Product.Count==0) goto case 44;
                        Add(
                            ref ProductReview情報,
                            ProductReview,
                            new ProductReview(
                                ID(ProductReview),
                                Product.Sampling.ProductID,
                                "",DateTime.Now,"",0,null,DateTime.Now
                            )
                        );
                        break;
                    case 51:
                        if(Product.Count==0) goto case 44;
                        Add(
                            ref TransactionHistory情報,
                            TransactionHistory,
                            new TransactionHistory(
                                ID(TransactionHistory),
                                Product.Sampling.ProductID,
                                0,0,DateTime.Now,"",0,0,DateTime.Now
                            )
                        );
                        break;
                    case 52:
                        if(Product.Count==0) goto case 44;
                        Add(
                            ref WorkOrder情報,
                            WorkOrder,
                            new WorkOrder(
                                ID(WorkOrder),
                                Product.Sampling.ProductID,
                                0,0,0,DateTime.Now,null,DateTime.Now,null,DateTime.Now
                            )
                        );
                        break;
                    case 53:
                        if(WorkOrder.Count==0) goto case 52;
                        if(Product.Count==0) goto case 44;
                        if(Location.Count==0) goto case 12;
                        Add(
                            ref WorkOrderRouting情報,WorkOrderRouting,
                            new WorkOrderRouting(
                                WorkOrder.Sampling.WorkOrderID,
                                Product.Sampling.ProductID,
                                0,
                                Location.Sampling.LocationID,
                                DateTime.Now,DateTime.Now,null,null,null,0,null,DateTime.Now
                            )
                        );
                        break;
                    case 54:
                        if(Product.Count==0) goto case 44;
                        if(Vendor.Count==0) goto case 30;
                        if(UnitMeasure.Count==0) goto case 19;
                        Add(
                            ref ProductVendor情報,
                            ProductVendor,
                            new ProductVendor(
                                Product.Sampling.ProductID,
                                Vendor.Sampling.BusinessEntityID,
                                0,0,null,null,0,0,null,
                                UnitMeasure.Sampling.UnitMeasureCode,
                                DateTime.Now
                            )
                        );
                        break;
                    case 55:
                        if(Employee.Count==0) goto case 35;
                        if(Vendor.Count==0) goto case 30;
                        if(ShipMethod.Count==0) goto case 20;
                        Add(
                            ref PurchaseOrderHeader情報,
                            PurchaseOrderHeader,
                            new PurchaseOrderHeader(
                                ID(PurchaseOrderHeader),0,0,
                                Employee.Sampling.BusinessEntityID,
                                Vendor.Sampling.BusinessEntityID,
                                ShipMethod.Sampling.ShipMethodID,
                                DateTime.Now,null,0,0,0,0,DateTime.Now
                            )
                        );
                        break;
                    case 56:
                        if(Employee.Count==0) goto case 35;
                        if(SalesTerritory.Count==0) goto case 34;
                        Add(
                            ref SalesPerson情報,
                            SalesPerson,
                            new SalesPerson(
                                Employee.Sampling.BusinessEntityID,
                                SalesTerritory.SamplingNullable?.TerritoryID,
                                null,0,0,0,0,Guid.NewGuid(),DateTime.Now
                            )
                        );
                        break;
                    case 57:
                        if(SalesPerson.Count==0) goto case 56;
                        Add(
                            ref SalesPersonQuotaHistory情報,
                            SalesPersonQuotaHistory,
                            new SalesPersonQuotaHistory(
                                SalesPerson.Sampling.BusinessEntityID,
                                DateTime.Now,0,Guid.NewGuid(),DateTime.Now
                            )
                        );
                        break;
                    case 58:
                        if(StateProvince.Count==0) goto case 42;
                        Add(
                            ref SalesTaxRate情報,
                            SalesTaxRate,
                            new SalesTaxRate(
                                ID(SalesTaxRate),
                                StateProvince.Sampling.StateProvinceID,
                                0,0,"",Guid.NewGuid(),DateTime.Now
                            )
                        );
                        break;
                    case 59:
                        if(SalesPerson.Count==0) goto case 56;
                        if(SalesTerritory.Count==0) goto case 34;
                        Add(
                            ref SalesTerritoryHistory情報,SalesTerritoryHistory,
                            new SalesTerritoryHistory(
                                SalesPerson.Sampling.BusinessEntityID,
                                SalesTerritory.Sampling.TerritoryID,
                                DateTime.Now,null,Guid.NewGuid(),DateTime.Now
                            )
                        );
                        break;
                    case 60:
                        if(Product.Count==0) goto case 44;
                        Add(
                            ref ShoppingCartItem情報,
                            ShoppingCartItem,
                            new ShoppingCartItem(
                                ID(ShoppingCartItem),"",0,
                                Product.Sampling.ProductID,
                                DateTime.Now,DateTime.Now
                            )
                        );
                        break;
                    case 61:
                        if(Product.Count==0) goto case 44;
                        if(SpecialOffer.Count==0) goto case 24;
                        Add(
                            ref SpecialOfferProduct情報,
                            SpecialOfferProduct,
                            new SpecialOfferProduct(
                                SpecialOffer.Sampling.SpecialOfferID,
                                Product.Sampling.ProductID,
                                Guid.NewGuid(),DateTime.Now
                            )
                        );
                        break;
                    case 62:
                        if(SalesPerson.Count==0) goto case 56;
                        Add(
                            ref Store情報,
                            Store,
                            new Store(
                                SalesPerson.Sampling.BusinessEntityID,
                                "",
                                SalesPerson.SamplingNullable?.BusinessEntityID,
                                null,Guid.NewGuid(),DateTime.Now
                            )
                        );
                        break;
                    case 63:
                        if(StateProvince.Count==0) goto case 42;
                        Add(
                            ref Address情報,Address,
                            new Address(
                                ID(Address),"",null,"",
                                StateProvince.Sampling.StateProvinceID,
                                "",null,Guid.NewGuid(),DateTime.Now
                            )
                        );
                        break;
                    case 64:
                        if(SalesPerson.Count==0) goto case 56;
                        if(Address.Count==0) goto case 63;
                        if(AddressType.Count==0) goto case 5;
                        Add(
                            ref BusinessEntityAddress情報,
                            BusinessEntityAddress,
                            new BusinessEntityAddress(
                                SalesPerson.Sampling.BusinessEntityID,
                                Address.Sampling.AddressID,
                                AddressType.Sampling.AddressTypeID,
                                Guid.NewGuid(),DateTime.Now
                            )
                        );
                        break;
                    case 65:
                        if(UnitMeasure.Count==0) goto case 19;
                        Add(
                            ref BillOfMaterials情報,
                            BillOfMaterials,
                            new BillOfMaterials(
                                ID(BillOfMaterials),null,0,DateTime.Now,null,
                                UnitMeasure.Sampling.UnitMeasureCode,
                                0,0,DateTime.Now
                            )
                        );
                        break;
                    case 66:
                        if(PurchaseOrderHeader.Count==0) goto case 55;
                        if(Product.Count==0) goto case 44;
                        Add(
                            ref PurchaseOrderDetail情報,
                            PurchaseOrderDetail,
                            new PurchaseOrderDetail(
                                PurchaseOrderHeader.Sampling.PurchaseOrderID,
                                ID(PurchaseOrderDetail),
                                DateTime.Now,0,
                                Product.Sampling.ProductID,
                                0,0,0,0,0,DateTime.Now
                            )
                        );
                        break;
                    case 67:
                        Add(
                            ref Customer情報,
                            Customer,
                            new Customer(
                                ID(Customer),
                                Person_Person.SamplingNullable?.BusinessEntityID,
                                Store.SamplingNullable?.BusinessEntityID,
                                SalesTerritory.SamplingNullable?.TerritoryID,
                                "",Guid.NewGuid(),DateTime.Now
                            )
                        );
                        break;
                    case 68:
                        if(Customer.Count==0) goto case 67;
                        if(BillOfMaterials.Count==0) goto case 65;
                        if(Address.Count==0) goto case 63;
                        if(ShipMethod.Count==0) goto case 20;
                        if(CreditCard.Count==0) goto case 21;
                        Add(
                            ref SalesOrderHeader情報,
                            SalesOrderHeader,
                            new SalesOrderHeader(
                                ID(SalesOrderHeader),
                                0,DateTime.Now,DateTime.Now,null,0,false,"",null,null,
                                Customer.Sampling.CustomerID,
                                SalesPerson.SamplingNullable?.BusinessEntityID,
                                null,
                                BillOfMaterials.Sampling.BillOfMaterialsID,
                                Address.Sampling.AddressID,
                                ShipMethod.Sampling.ShipMethodID,
                                CreditCard.Sampling.CreditCardID,
                                null,null,0,0,0,0,null,Guid.NewGuid(),DateTime.Now
                            )
                        );
                        break;
                    case 69:
                        if(SalesOrderHeader.Count==0) goto case 68;
                        if(SalesReason.Count==0) goto case 23;
                        Add(
                            ref SalesOrderHeaderSalesReason情報,
                            SalesOrderHeaderSalesReason,
                            new SalesOrderHeaderSalesReason(
                                SalesOrderHeader.Sampling.SalesOrderID,
                                SalesReason.Sampling.SalesReasonID,
                                DateTime.Now
                            )
                        );
                        break;
                    case 70: {
                        if(SalesOrderHeader.Count==0) goto case 68;
                        if(SpecialOfferProduct.Count==0) goto case 61;
                        var Sampling = SpecialOfferProduct.Sampling;
                        Add(
                            ref SalesOrderDetail情報,
                            SalesOrderDetail,
                            new SalesOrderDetail(
                                SalesOrderHeader.Sampling.SalesOrderID,
                                ID(SalesOrderDetail),
                                null,0,
                                Sampling.ProductID,
                                Sampling.SpecialOfferID,
                                0,0,0,Guid.NewGuid(),DateTime.Now
                            )
                        );
                        break;
                    }
                }
            }
            AddWriteLine(AWBuildVersion情報);
            AddWriteLine(DatabaseLog情報);
            AddWriteLine(ErrorLog情報);
            AddWriteLine(Department情報);
            AddWriteLine(Shift情報);
            AddWriteLine(AddressType情報);
            AddWriteLine(BusinessEntity情報);
            AddWriteLine(ContactType情報);
            AddWriteLine(CountryRegion情報);
            AddWriteLine(PhoneNumberType情報);
            AddWriteLine(Culture情報);
            AddWriteLine(Illustration情報);
            AddWriteLine(Location情報);
            AddWriteLine(ProductCategory情報);
            AddWriteLine(ProductDescription情報);
            AddWriteLine(ProductModel情報);
            AddWriteLine(ProductPhoto情報);
            AddWriteLine(ScrapReason情報);
            AddWriteLine(TransactionHistoryArchive情報);
            AddWriteLine(UnitMeasure情報);
            AddWriteLine(ShipMethod情報);
            AddWriteLine(CreditCard情報);
            AddWriteLine(Currency情報);
            AddWriteLine(SalesReason情報);
            AddWriteLine(SpecialOffer情報);
            AddWriteLine(Person情報);
            AddWriteLine(PersonPhone情報);
            AddWriteLine(ProductModelIllustration情報);
            AddWriteLine(ProductModelProductDescriptionCulture情報);
            AddWriteLine(ProductSubcategory情報);
            AddWriteLine(Vendor情報);
            AddWriteLine(CountryRegionCurrency情報);
            AddWriteLine(CurrencyRate情報);
            AddWriteLine(PersonCreditCard情報);
            AddWriteLine(SalesTerritory情報);
            AddWriteLine(Employee情報);
            AddWriteLine(EmployeeDepartmentHistory情報);
            AddWriteLine(EmployeePayHistory情報);
            AddWriteLine(JobCandidate情報);
            AddWriteLine(BusinessEntityContact情報);
            AddWriteLine(EmailAddress情報);
            AddWriteLine(Password情報);
            AddWriteLine(StateProvince情報);
            AddWriteLine(Document情報);
            AddWriteLine(Product情報);
            AddWriteLine(ProductCostHistory情報);
            AddWriteLine(ProductDocument情報);
            AddWriteLine(ProductInventory情報);
            AddWriteLine(ProductListPriceHistory情報);
            AddWriteLine(ProductProductPhoto情報);
            AddWriteLine(ProductReview情報);
            AddWriteLine(TransactionHistory情報);
            AddWriteLine(WorkOrder情報);
            AddWriteLine(WorkOrderRouting情報);
            AddWriteLine(ProductVendor情報);
            AddWriteLine(PurchaseOrderHeader情報);
            AddWriteLine(SalesPerson情報);
            AddWriteLine(SalesPersonQuotaHistory情報);
            AddWriteLine(SalesTaxRate情報);
            AddWriteLine(SalesTerritoryHistory情報);
            AddWriteLine(ShoppingCartItem情報);
            AddWriteLine(SpecialOfferProduct情報);
            AddWriteLine(Store情報);
            AddWriteLine(Address情報);
            AddWriteLine(BusinessEntityAddress情報);
            AddWriteLine(BillOfMaterials情報);
            AddWriteLine(PurchaseOrderDetail情報);
            AddWriteLine(Customer情報);
            AddWriteLine(SalesOrderHeader情報);
            AddWriteLine(SalesOrderHeaderSalesReason情報);
            AddWriteLine(SalesOrderDetail情報);
            for(var a = 0;a<試行回数;a++) {
                switch(Switchパターン()%71) {
                    case 0: Del(ref AWBuildVersion情報,AWBuildVersion); break;
                    case 1: Del(ref DatabaseLog情報,DatabaseLog); break;
                    case 2: Del(ref ErrorLog情報,ErrorLog); break;
                    case 3: Del(ref Department情報,Department); break;
                    case 4: Del(ref Shift情報,Shift); break;
                    case 5: Del(ref AddressType情報,AddressType); break;
                    case 6: Del(ref BusinessEntity情報,BusinessEntity); break;
                    case 7: Del(ref ContactType情報,ContactType); break;
                    case 8: Del(ref CountryRegion情報,CountryRegion); break;
                    case 9: Del(ref PhoneNumberType情報,PhoneNumberType); break;
                    case 10: Del(ref Culture情報,Culture); break;
                    case 11: Del(ref Illustration情報,Illustration); break;
                    case 12: Del(ref Location情報,Location); break;
                    case 13: Del(ref ProductCategory情報,ProductCategory); break;
                    case 14: Del(ref ProductDescription情報,ProductDescription); break;
                    case 15: Del(ref ProductModel情報,ProductModel); break;
                    case 16: Del(ref ProductPhoto情報,ProductPhoto); break;
                    case 17: Del(ref ScrapReason情報,ScrapReason); break;
                    case 18: Del(ref TransactionHistoryArchive情報,TransactionHistoryArchive); break;
                    case 19: Del(ref UnitMeasure情報,UnitMeasure); break;
                    case 20: Del(ref ShipMethod情報,ShipMethod); break;
                    case 21: Del(ref CreditCard情報,CreditCard); break;
                    case 22: Del(ref Currency情報,Currency); break;
                    case 23: Del(ref SalesReason情報,SalesReason); break;
                    case 24: Del(ref SpecialOffer情報,SpecialOffer); break;
                    case 25: Del(ref Person情報,Person_Person); break;
                    case 26: Del(ref PersonPhone情報,PersonPhone); break;
                    case 27: Del(ref ProductModelIllustration情報,ProductModelIllustration); break;
                    case 28: Del(ref ProductModelProductDescriptionCulture情報,ProductModelProductDescriptionCulture); break;
                    case 29: Del(ref ProductSubcategory情報,ProductSubcategory); break;
                    case 30: Del(ref Vendor情報,Vendor); break;
                    case 31: Del(ref CountryRegionCurrency情報,CountryRegionCurrency); break;
                    case 32: Del(ref CurrencyRate情報,CurrencyRate); break;
                    case 33: Del(ref PersonCreditCard情報,PersonCreditCard); break;
                    case 34: Del(ref SalesTerritory情報,SalesTerritory); break;
                    case 35: Del(ref Employee情報,Employee); break;
                    case 36: Del(ref EmployeeDepartmentHistory情報,EmployeeDepartmentHistory); break;
                    case 37: Del(ref EmployeePayHistory情報,EmployeePayHistory); break;
                    case 38: Del(ref JobCandidate情報,JobCandidate); break;
                    case 39: Del(ref BusinessEntityContact情報,BusinessEntityContact); break;
                    case 40: Del(ref EmailAddress情報,EmailAddress); break;
                    case 41: Del(ref Password情報,Password); break;
                    case 42: Del(ref StateProvince情報,StateProvince); break;
                    case 43: Del(ref Document情報,Document); break;
                    case 44: Del(ref Product情報,Product); break;
                    case 45: Del(ref ProductCostHistory情報,ProductCostHistory); break;
                    case 46: Del(ref ProductDocument情報,ProductDocument); break;
                    case 47: Del(ref ProductInventory情報,ProductInventory); break;
                    case 48: Del(ref ProductListPriceHistory情報,ProductListPriceHistory); break;
                    case 49: Del(ref ProductProductPhoto情報,ProductProductPhoto); break;
                    case 50: Del(ref ProductReview情報,ProductReview); break;
                    case 51: Del(ref TransactionHistory情報,TransactionHistory); break;
                    case 52: Del(ref WorkOrder情報,WorkOrder); break;
                    case 53: Del(ref WorkOrderRouting情報,WorkOrderRouting); break;
                    case 54: Del(ref ProductVendor情報,ProductVendor); break;
                    case 55: Del(ref PurchaseOrderHeader情報,PurchaseOrderHeader); break;
                    case 56: Del(ref SalesPerson情報,SalesPerson); break;
                    case 57: Del(ref SalesPersonQuotaHistory情報,SalesPersonQuotaHistory); break;
                    case 58: Del(ref SalesTaxRate情報,SalesTaxRate); break;
                    case 59: Del(ref SalesTerritoryHistory情報,SalesTerritoryHistory); break;
                    case 60: Del(ref ShoppingCartItem情報,ShoppingCartItem); break;
                    case 61: Del(ref SpecialOfferProduct情報,SpecialOfferProduct); break;
                    case 62: Del(ref Store情報,Store); break;
                    case 63: Del(ref Address情報,Address); break;
                    case 64: Del(ref BusinessEntityAddress情報,BusinessEntityAddress); break;
                    case 65: Del(ref BillOfMaterials情報,BillOfMaterials); break;
                    case 66: Del(ref PurchaseOrderDetail情報,PurchaseOrderDetail); break;
                    case 67: Del(ref Customer情報,Customer); break;
                    case 68: Del(ref SalesOrderHeader情報,SalesOrderHeader); break;
                    case 69: Del(ref SalesOrderHeaderSalesReason情報,SalesOrderHeaderSalesReason); break;
                    case 70: Del(ref SalesOrderDetail情報,SalesOrderDetail); break;
                }
            }
            Console.WriteLine($"Transaction {s.ElapsedMilliseconds,7}ms");
            DelWriteLine(AWBuildVersion情報);
            DelWriteLine(DatabaseLog情報);
            DelWriteLine(ErrorLog情報);
            DelWriteLine(Department情報);
            DelWriteLine(Shift情報);
            DelWriteLine(AddressType情報);
            DelWriteLine(BusinessEntity情報);
            DelWriteLine(ContactType情報);
            DelWriteLine(CountryRegion情報);
            DelWriteLine(PhoneNumberType情報);
            DelWriteLine(Culture情報);
            DelWriteLine(Illustration情報);
            DelWriteLine(Location情報);
            DelWriteLine(ProductCategory情報);
            DelWriteLine(ProductDescription情報);
            DelWriteLine(ProductModel情報);
            DelWriteLine(ProductPhoto情報);
            DelWriteLine(ScrapReason情報);
            DelWriteLine(TransactionHistoryArchive情報);
            DelWriteLine(UnitMeasure情報);
            DelWriteLine(ShipMethod情報);
            DelWriteLine(CreditCard情報);
            DelWriteLine(Currency情報);
            DelWriteLine(SalesReason情報);
            DelWriteLine(SpecialOffer情報);
            DelWriteLine(Person情報);
            DelWriteLine(PersonPhone情報);
            DelWriteLine(ProductModelIllustration情報);
            DelWriteLine(ProductModelProductDescriptionCulture情報);
            DelWriteLine(ProductSubcategory情報);
            DelWriteLine(Vendor情報);
            DelWriteLine(CountryRegionCurrency情報);
            DelWriteLine(CurrencyRate情報);
            DelWriteLine(PersonCreditCard情報);
            DelWriteLine(SalesTerritory情報);
            DelWriteLine(Employee情報);
            DelWriteLine(EmployeeDepartmentHistory情報);
            DelWriteLine(EmployeePayHistory情報);
            DelWriteLine(JobCandidate情報);
            DelWriteLine(BusinessEntityContact情報);
            DelWriteLine(EmailAddress情報);
            DelWriteLine(Password情報);
            DelWriteLine(StateProvince情報);
            DelWriteLine(Document情報);
            DelWriteLine(Product情報);
            DelWriteLine(ProductCostHistory情報);
            DelWriteLine(ProductDocument情報);
            DelWriteLine(ProductInventory情報);
            DelWriteLine(ProductListPriceHistory情報);
            DelWriteLine(ProductProductPhoto情報);
            DelWriteLine(ProductReview情報);
            DelWriteLine(TransactionHistory情報);
            DelWriteLine(WorkOrder情報);
            DelWriteLine(WorkOrderRouting情報);
            DelWriteLine(ProductVendor情報);
            DelWriteLine(PurchaseOrderHeader情報);
            DelWriteLine(SalesPerson情報);
            DelWriteLine(SalesPersonQuotaHistory情報);
            DelWriteLine(SalesTaxRate情報);
            DelWriteLine(SalesTerritoryHistory情報);
            DelWriteLine(ShoppingCartItem情報);
            DelWriteLine(SpecialOfferProduct情報);
            DelWriteLine(Store情報);
            DelWriteLine(Address情報);
            DelWriteLine(BusinessEntityAddress情報);
            DelWriteLine(BillOfMaterials情報);
            DelWriteLine(PurchaseOrderDetail情報);
            DelWriteLine(Customer情報);
            DelWriteLine(SalesOrderHeader情報);
            DelWriteLine(SalesOrderHeaderSalesReason情報);
            DelWriteLine(SalesOrderDetail情報);
        }
        [SuppressMessage("Globalization","CA1303:ローカライズされるパラメーターとしてリテラルを渡さない",Justification = "<保留中>")]
        private static void 列挙(Container Container) {
            var dbo = Container.dbo;
            var HumanResources = Container.HumanResources;
            var Person = Container.Person;
            var Production = Container.Production;
            var Purchasing = Container.Purchasing;
            var Sales = Container.Sales;
            Console.WriteLine("列挙開始");
            列挙(() => dbo.AWBuildVersion);
            列挙(() => dbo.DatabaseLog);
            列挙(() => dbo.ErrorLog);
            列挙(() => HumanResources.Department);
            列挙(() => HumanResources.Shift);
            列挙(() => Person.AddressType);
            列挙(() => Person.BusinessEntity);
            列挙(() => Person.ContactType);
            列挙(() => Person.CountryRegion);
            列挙(() => Person.PhoneNumberType);
            列挙(() => Production.Culture);
            列挙(() => Production.Illustration);
            列挙(() => Production.Location);
            列挙(() => Production.ProductCategory);
            列挙(() => Production.ProductDescription);
            列挙(() => Production.ProductModel);
            列挙(() => Production.ProductPhoto);
            列挙(() => Production.ScrapReason);
            列挙(() => Production.TransactionHistoryArchive);
            列挙(() => Production.UnitMeasure);
            列挙(() => Purchasing.ShipMethod);
            列挙(() => Sales.CreditCard);
            列挙(() => Sales.Currency);
            列挙(() => Sales.SalesReason);
            列挙(() => Sales.SpecialOffer);
            列挙(() => Person.Person);
            列挙(() => Person.PersonPhone);
            列挙(() => Production.ProductModelIllustration);
            列挙(() => Production.ProductModelProductDescriptionCulture);
            列挙(() => Production.ProductSubcategory);
            列挙(() => Purchasing.Vendor);
            列挙(() => Sales.CountryRegionCurrency);
            列挙(() => Sales.CurrencyRate);
            列挙(() => Sales.PersonCreditCard);
            列挙(() => Sales.SalesTerritory);
            列挙(() => HumanResources.Employee);
            列挙(() => HumanResources.EmployeeDepartmentHistory);
            列挙(() => HumanResources.EmployeePayHistory);
            列挙(() => HumanResources.JobCandidate);
            列挙(() => Person.BusinessEntityContact);
            列挙(() => Person.EmailAddress);
            列挙(() => Person.Password);
            列挙(() => Person.StateProvince);
            列挙(() => Production.Document);
            列挙(() => Production.Product);
            列挙(() => Production.ProductCostHistory);
            列挙(() => Production.ProductDocument);
            Console.WriteLine("列挙終了");
        }
        private static void Load() {
            var s = Stopwatch.StartNew();
            using var e = new Container();
            var dbo = e.dbo;
            var HumanResources = e.HumanResources;
            var Person = e.Person;
            var Production = e.Production;
            var Purchasing = e.Purchasing;
            var Sales = e.Sales;
            using var C = new SqlConnection(@"Data Source=localhost\MSSQLSERVER2019;Initial Catalog=AdventureWorks2019;Integrated Security=True");
            C.Open();
            using var Command = new SqlCommand { Connection=C };
            Command.CommandText=
                "SELECT[SystemInformationID]"
                +",[Database Version]"
                +",[VersionDate]"
                +",[ModifiedDate]"
                +"FROM[dbo].[AWBuildVersion]"
                +"SELECT[DatabaseLogID]"
                +",[PostTime]"
                +",[DatabaseUser]"
                +",[Event]"
                +",[Schema]"
                +",[Object]"
                +",[TSQL]"
                +",[XmlEvent]"
                +"FROM[dbo].[DatabaseLog]"
                +"SELECT[ErrorLogID]"
                +",[ErrorTime]"
                +",[UserName]"
                +",[ErrorNumber]"
                +",[ErrorSeverity]"
                +",[ErrorState]"
                +",[ErrorProcedure]"
                +",[ErrorLine]"
                +",[ErrorMessage]"
                +"FROM[dbo].[ErrorLog]"
                +"SELECT[DepartmentID]"
                +",[Name]"
                +",[GroupName]"
                +",[ModifiedDate]"
                +"FROM[HumanResources].[Department]"
                +"SELECT[ShiftID]"
                +",[Name]"
                +",[StartTime]"
                +",[EndTime]"
                +",[ModifiedDate]"
                +"FROM[HumanResources].[Shift]"
                +"SELECT[AddressTypeID]"
                +",[Name]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[Person].[AddressType]"
                +"SELECT[BusinessEntityID]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[Person].[BusinessEntity]"
                +"SELECT[ContactTypeID]"
                +",[Name]"
                +",[ModifiedDate]"
                +"FROM[Person].[ContactType]"
                +"SELECT[CountryRegionCode]"
                +",[Name]"
                +",[ModifiedDate]"
                +"FROM[Person].[CountryRegion]"
                +"SELECT[PhoneNumberTypeID]"
                +",[Name]"
                +",[ModifiedDate]"
                +"FROM[Person].[PhoneNumberType]"
                +"SELECT[CultureID]"
                +",[Name]"
                +",[ModifiedDate]"
                +"FROM[Production].[Culture]"
                +"SELECT[IllustrationID]"
                +",[Diagram]"
                +",[ModifiedDate]"
                +"FROM[Production].[Illustration]"
                +"SELECT[LocationID]"
                +",[Name]"
                +",[CostRate]"
                +",[Availability]"
                +",[ModifiedDate]"
                +"FROM[Production].[Location]"
                +"SELECT[ProductCategoryID]"
                +",[Name]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[Production].[ProductCategory]"
                +"SELECT[ProductDescriptionID]"
                +",[Description]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[Production].[ProductDescription]"
                +"SELECT[ProductModelID]"
                +",[Name]"
                +",[CatalogDescription]"
                +",[Instructions]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[Production].[ProductModel]"
                +"SELECT[ProductPhotoID]"
                +",[ThumbNailPhoto]"
                +",[ThumbnailPhotoFileName]"
                +",[LargePhoto]"
                +",[LargePhotoFileName]"
                +",[ModifiedDate]"
                +"FROM[Production].[ProductPhoto]"
                +"SELECT[ScrapReasonID]"
                +",[Name]"
                +",[ModifiedDate]"
                +"FROM[Production].[ScrapReason]"
                +"SELECT[TransactionID]"
                +",[ProductID]"
                +",[ReferenceOrderID]"
                +",[ReferenceOrderLineID]"
                +",[TransactionDate]"
                +",[TransactionType]"
                +",[Quantity]"
                +",[ActualCost]"
                +",[ModifiedDate]"
                +"FROM[Production].[TransactionHistoryArchive]"
                +"SELECT[UnitMeasureCode]"
                +",[Name]"
                +",[ModifiedDate]"
                +"FROM[Production].[UnitMeasure]"
                +"SELECT[ShipMethodID]"
                +",[Name]"
                +",[ShipBase]"
                +",[ShipRate]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[Purchasing].[ShipMethod]"
                +"SELECT[CreditCardID]"
                +",[CardType]"
                +",[CardNumber]"
                +",[ExpMonth]"
                +",[ExpYear]"
                +",[ModifiedDate]"
                +"FROM[Sales].[CreditCard]"
                +"SELECT[CurrencyCode]"
                +",[Name]"
                +",[ModifiedDate]"
                +"FROM[Sales].[Currency]"
                +"SELECT[SalesReasonID]"
                +",[Name]"
                +",[ReasonType]"
                +",[ModifiedDate]"
                +"FROM[Sales].[SalesReason]"
                +"SELECT[SpecialOfferID]"
                +",[Description]"
                +",[DiscountPct]"
                +",[Type]"
                +",[Category]"
                +",[StartDate]"
                +",[EndDate]"
                +",[MinQty]"
                +",[MaxQty]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[Sales].[SpecialOffer]"
                +"SELECT[BusinessEntityID]"
                +",[PersonType]"
                +",[NameStyle]"
                +",[Title]"
                +",[FirstName]"
                +",[MiddleName]"
                +",[LastName]"
                +",[Suffix]"
                +",[EmailPromotion]"
                +",[AdditionalContactInfo]"
                +",[Demographics]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Person].[Person]"
                +"SELECT[BusinessEntityID]"
                +",[PhoneNumber]"
                +",[PhoneNumberTypeID]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Person].[PersonPhone]"
                +"SELECT[ProductModelID]"
                +",[IllustrationID]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Production].[ProductModelIllustration]"
                +"SELECT[ProductModelID]"
                +",[ProductDescriptionID]"
                +",[CultureID]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Production].[ProductModelProductDescriptionCulture]"
                +"SELECT[ProductSubcategoryID]"
                +",[ProductCategoryID]"
                +",[Name]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Production].[ProductSubcategory]"
                +"SELECT[BusinessEntityID]"
                +",[AccountNumber]"
                +",[Name]"
                +",[CreditRating]"
                +",[PreferredVendorStatus]"
                +",[ActiveFlag]"
                +",[PurchasingWebServiceURL]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Purchasing].[Vendor]"
                +"SELECT[CountryRegionCode]"
                +",[CurrencyCode]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Sales].[CountryRegionCurrency]"
                +"SELECT[CurrencyRateID]"
                +",[CurrencyRateDate]"
                +",[FromCurrencyCode]"
                +",[ToCurrencyCode]"
                +",[AverageRate]"
                +",[EndOfDayRate]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Sales].[CurrencyRate]"
                +"SELECT[BusinessEntityID]"
                +",[CreditCardID]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Sales].[PersonCreditCard]"
                +"SELECT[TerritoryID]"
                +",[Name]"
                +",[CountryRegionCode]"
                +",[Group]"
                +",[SalesYTD]"
                +",[SalesLastYear]"
                +",[CostYTD]"
                +",[CostLastYear]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Sales].[SalesTerritory]"
                +"SELECT[BusinessEntityID]"
                +",[NationalIDNumber]"
                +",[LoginID]"
                +",[OrganizationNode]"
                +",[OrganizationLevel]"
                +",[JobTitle]"
                +",[BirthDate]"
                +",[MaritalStatus]"
                +",[Gender]"
                +",[HireDate]"
                +",[SalariedFlag]"
                +",[VacationHours]"
                +",[SickLeaveHours]"
                +",[CurrentFlag]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[HumanResources].[Employee]"
                +"SELECT[BusinessEntityID]"
                +",[DepartmentID]"
                +",[ShiftID]"
                +",[StartDate]"
                +",[EndDate]"
                +",[ModifiedDate]"
                +"FROM[HumanResources].[EmployeeDepartmentHistory]"
                +"SELECT[BusinessEntityID]"
                +",[RateChangeDate]"
                +",[Rate]"
                +",[PayFrequency]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[HumanResources].[EmployeePayHistory]"
                +"SELECT[JobCandidateID]"
                +",[BusinessEntityID]"
                +",[Resume]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[HumanResources].[JobCandidate]"
                +"SELECT[BusinessEntityID]"
                +",[PersonID]"
                +",[ContactTypeID]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Person].[BusinessEntityContact]"
                +"SELECT[BusinessEntityID]"
                +",[EmailAddressID]"
                +",[EmailAddress]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Person].[EmailAddress]"
                +"SELECT[BusinessEntityID]"
                +",[PasswordHash]"
                +",[PasswordSalt]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Person].[Password]"
                +"SELECT[StateProvinceID]"
                +",[StateProvinceCode]"
                +",[CountryRegionCode]"
                +",[IsOnlyStateProvinceFlag]"
                +",[Name]"
                +",[TerritoryID]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Person].[StateProvince]"
                +"SELECT[DocumentNode]"
                +",[DocumentLevel]"
                +",[Title]"
                +",[Owner]"
                +",[FolderFlag]"
                +",[FileName]"
                +",[FileExtension]"
                +",[Revision]"
                +",[ChangeNumber]"
                +",[Status]"
                +",[DocumentSummary]"
                +",[Document]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Production].[Document]"
                +"SELECT[ProductID]"
                +",[Name]"
                +",[ProductNumber]"
                +",[MakeFlag]"
                +",[FinishedGoodsFlag]"
                +",[Color]"
                +",[SafetyStockLevel]"
                +",[ReorderPoint]"
                +",[StandardCost]"
                +",[ListPrice]"
                +",[Size]"
                +",[SizeUnitMeasureCode]"
                +",[WeightUnitMeasureCode]"
                +",[Weight]"
                +",[DaysToManufacture]"
                +",[ProductLine]"
                +",[Class]"
                +",[Style]"
                +",[ProductSubcategoryID]"
                +",[ProductModelID]"
                +",[SellStartDate]"
                +",[SellEndDate]"
                +",[DiscontinuedDate]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Production].[Product]"
                +"SELECT[ProductID]"
                +",[StartDate]"
                +",[EndDate]"
                +",[StandardCost]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Production].[ProductCostHistory]"
                +"SELECT[ProductID]"
                +",[DocumentNode]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Production].[ProductDocument]"
                +"SELECT[ProductID]"
                +",[LocationID]"
                +",[Shelf]"
                +",[Bin]"
                +",[Quantity]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Production].[ProductInventory]"
                +"SELECT[ProductID]"
                +",[StartDate]"
                +",[EndDate]"
                +",[ListPrice]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Production].[ProductListPriceHistory]"
                +"SELECT[ProductID]"
                +",[ProductPhotoID]"
                +",[Primary]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Production].[ProductProductPhoto]"
                +"SELECT[ProductReviewID]"
                +",[ProductID]"
                +",[ReviewerName]"
                +",[ReviewDate]"
                +",[EmailAddress]"
                +",[Rating]"
                +",[Comments]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Production].[ProductReview]"
                +"SELECT[TransactionID]"
                +",[ProductID]"
                +",[ReferenceOrderID]"
                +",[ReferenceOrderLineID]"
                +",[TransactionDate]"
                +",[TransactionType]"
                +",[Quantity]"
                +",[ActualCost]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Production].[TransactionHistory]"
                +"SELECT[WorkOrderID]"
                +",[ProductID]"
                +",[OrderQty]"
                +",[StockedQty]"
                +",[ScrappedQty]"
                +",[StartDate]"
                +",[EndDate]"
                +",[DueDate]"
                +",[ScrapReasonID]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Production].[WorkOrder]"
                +"SELECT[WorkOrderID]"
                +",[ProductID]"
                +",[OperationSequence]"
                +",[LocationID]"
                +",[ScheduledStartDate]"
                +",[ScheduledEndDate]"
                +",[ActualStartDate]"
                +",[ActualEndDate]"
                +",[ActualResourceHrs]"
                +",[PlannedCost]"
                +",[ActualCost]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Production].[WorkOrderRouting]"
                +"SELECT[ProductID]"
                +",[BusinessEntityID]"
                +",[AverageLeadTime]"
                +",[StandardPrice]"
                +",[LastReceiptCost]"
                +",[LastReceiptDate]"
                +",[MinOrderQty]"
                +",[MaxOrderQty]"
                +",[OnOrderQty]"
                +",[UnitMeasureCode]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Purchasing].[ProductVendor]"
                +"SELECT[PurchaseOrderID]"
                +",[RevisionNumber]"
                +",[Status]"
                +",[EmployeeID]"
                +",[VendorID]"
                +",[ShipMethodID]"
                +",[OrderDate]"
                +",[ShipDate]"
                +",[SubTotal]"
                +",[TaxAmt]"
                +",[Freight]"
                +",[TotalDue]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Purchasing].[PurchaseOrderHeader]"
                +"SELECT[BusinessEntityID]"
                +",[TerritoryID]"
                +",[SalesQuota]"
                +",[Bonus]"
                +",[CommissionPct]"
                +",[SalesYTD]"
                +",[SalesLastYear]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Sales].[SalesPerson]"
                +"SELECT[BusinessEntityID]"
                +",[QuotaDate]"
                +",[SalesQuota]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Sales].[SalesPersonQuotaHistory]"
                +"SELECT[SalesTaxRateID]"
                +",[StateProvinceID]"
                +",[TaxType]"
                +",[TaxRate]"
                +",[Name]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Sales].[SalesTaxRate]"
                +"SELECT[BusinessEntityID]"
                +",[TerritoryID]"
                +",[StartDate]"
                +",[EndDate]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Sales].[SalesTerritoryHistory]"
                +"SELECT[ShoppingCartItemID]"
                +",[ShoppingCartID]"
                +",[Quantity]"
                +",[ProductID]"
                +",[DateCreated]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Sales].[ShoppingCartItem]"
                +"SELECT[SpecialOfferID]"
                +",[ProductID]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Sales].[SpecialOfferProduct]"
                +"SELECT[BusinessEntityID]"
                +",[Name]"
                +",[SalesPersonID]"
                +",[Demographics]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Sales].[Store]"
                +"SELECT[AddressID]"
                +",[AddressLine1]"
                +",[AddressLine2]"
                +",[City]"
                +",[StateProvinceID]"
                +",[PostalCode]"
                +",[SpatialLocation]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Person].[Address]"
                +"SELECT[BusinessEntityID]"
                +",[AddressID]"
                +",[AddressTypeID]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[Person].[BusinessEntityAddress]"
                +"SELECT[BillOfMaterialsID]"
                +",[ProductAssemblyID]"
                +",[ComponentID]"
                +",[StartDate]"
                +",[EndDate]"
                +",[UnitMeasureCode]"
                +",[BOMLevel]"
                +",[PerAssemblyQty]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Production].[BillOfMaterials]"
                +"SELECT[PurchaseOrderID]"
                +",[PurchaseOrderDetailID]"
                +",[DueDate]"
                +",[OrderQty]"
                +",[ProductID]"
                +",[UnitPrice]"
                +",[LineTotal]"
                +",[ReceivedQty]"
                +",[RejectedQty]"
                +",[StockedQty]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Purchasing].[PurchaseOrderDetail]"
                +"SELECT[CustomerID]"
                +",[PersonID]"
                +",[StoreID]"
                +",[TerritoryID]"
                +",[AccountNumber]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Sales].[Customer]"
                +"SELECT[SalesOrderID]"
                +",[RevisionNumber]"
                +",[OrderDate]"
                +",[DueDate]"
                +",[ShipDate]"
                +",[Status]"
                +",[OnlineOrderFlag]"
                +",[SalesOrderNumber]"
                +",[PurchaseOrderNumber]"
                +",[AccountNumber]"
                +",[CustomerID]"
                +",[SalesPersonID]"
                +",[TerritoryID]"
                +",[BillToAddressID]"
                +",[ShipToAddressID]"
                +",[ShipMethodID]"
                +",[CreditCardID]"
                +",[CreditCardApprovalCode]"
                +",[CurrencyRateID]"
                +",[SubTotal]"
                +",[TaxAmt]"
                +",[Freight]"
                +",[TotalDue]"
                +",[Comment]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Sales].[SalesOrderHeader]"
                +"SELECT[SalesOrderID]"
                +",[SalesReasonID]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Sales].[SalesOrderHeaderSalesReason]"
                +"SELECT[SalesOrderID]"
                +",[SalesOrderDetailID]"
                +",[CarrierTrackingNumber]"
                +",[OrderQty]"
                +",[ProductID]"
                +",[SpecialOfferID]"
                +",[UnitPrice]"
                +",[UnitPriceDiscount]"
                +",[LineTotal]"
                +",[rowguid]"
                +",[ModifiedDate]"
                +"FROM[AdventureWorks2017].[Sales].[SalesOrderDetail]";
            using var Reader = Command.ExecuteReader();
            while(Reader.Read())
                dbo.AWBuildVersion.AddOrThrow(
                    new AWBuildVersion(
                        Reader.GetByte(0),
                        Reader.GetString(1),
                        Reader.GetDateTime(2),
                        Reader.GetDateTime(3)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                dbo.DatabaseLog.AddOrThrow(
                    new DatabaseLog(
                        Reader.GetInt32(0),
                        Reader.GetDateTime(1),
                        Reader.GetString(2),
                        Reader.GetString(3),
                        GetString(Reader,4),
                        GetString(Reader,5),
                        Reader.GetString(6),
                        XDocument.Load(Reader.GetXmlReader(7))
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                dbo.ErrorLog.AddOrThrow(
                    new ErrorLog(
                        Reader.GetInt32(0),
                        Reader.GetDateTime(1),
                        Reader.GetString(2),
                        Reader.GetInt32(3),
                        GetInt32(Reader,4),
                        GetInt32(Reader,5),
                        GetString(Reader,6),
                        GetInt32(Reader,7),
                        Reader.GetString(8)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                HumanResources.Department.AddOrThrow(
                    new Department(
                        Reader.GetInt16(0),
                        Reader.GetString(1),
                        Reader.GetString(2),
                        Reader.GetDateTime(3)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                HumanResources.Shift.AddOrThrow(
                    new Shift(
                        Reader.GetByte(0),
                        Reader.GetString(1),
                        Reader.GetTimeSpan(2),
                        Reader.GetTimeSpan(3),
                        Reader.GetDateTime(4)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                Person.AddressType.AddOrThrow(
                    new AddressType(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetGuid(2),
                        Reader.GetDateTime(3)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                Person.BusinessEntity.AddOrThrow(
                    new BusinessEntity(
                        Reader.GetInt32(0),
                        Reader.GetGuid(1),
                        Reader.GetDateTime(2)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                Person.ContactType.AddOrThrow(
                    new ContactType(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetDateTime(2)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                Person.CountryRegion.AddOrThrow(
                    new CountryRegion(
                        Reader.GetString(0),
                        Reader.GetString(1),
                        Reader.GetDateTime(2)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                Person.PhoneNumberType.AddOrThrow(
                    new PhoneNumberType(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetDateTime(2)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                Production.Culture.AddOrThrow(
                    new Culture(
                        Reader.GetString(0),
                        Reader.GetString(1),
                        Reader.GetDateTime(2)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                Production.Illustration.AddOrThrow(
                    new Illustration(
                        Reader.GetInt32(0),
                        GetXDocument(Reader,1),
                        Reader.GetDateTime(2)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                Production.Location.AddOrThrow(
                    new Location(
                        Reader.GetInt16(0),
                        Reader.GetString(1),
                        Reader.GetDecimal(2),
                        Reader.GetDecimal(3),
                        Reader.GetDateTime(4)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                Production.ProductCategory.AddOrThrow(
                    new ProductCategory(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetGuid(2),
                        Reader.GetDateTime(3)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                Production.ProductDescription.AddOrThrow(
                    new ProductDescription(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetGuid(2),
                        Reader.GetDateTime(3)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                Production.ProductModel.AddOrThrow(
                    new ProductModel(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        GetXDocument(Reader,2),
                        GetXDocument(Reader,3),
                        Reader.GetGuid(4),
                        Reader.GetDateTime(5)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                Production.ProductPhoto.AddOrThrow(
                    new ProductPhoto(
                        Reader.GetInt32(0),
                        GetBytes(Reader,1),
                        GetString(Reader,2),
                        GetBytes(Reader,3),
                        GetString(Reader,4),
                        Reader.GetDateTime(5)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                Production.ScrapReason.AddOrThrow(
                    new ScrapReason(
                        Reader.GetInt16(0),
                        Reader.GetString(1),
                        Reader.GetDateTime(2)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                Production.TransactionHistoryArchive.AddOrThrow(
                    new TransactionHistoryArchive(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetInt32(2),
                        Reader.GetInt32(3),
                        Reader.GetDateTime(4),
                        Reader.GetString(5),
                        Reader.GetInt32(6),
                        Reader.GetDecimal(7),
                        Reader.GetDateTime(8)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                Production.UnitMeasure.AddOrThrow(
                    new UnitMeasure(
                        Reader.GetString(0),
                        Reader.GetString(1),
                        Reader.GetDateTime(2)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                Purchasing.ShipMethod.AddOrThrow(
                    new ShipMethod(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetDecimal(2),
                        Reader.GetDecimal(3),
                        Reader.GetGuid(4),
                        Reader.GetDateTime(5)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                Sales.CreditCard.AddOrThrow(
                    new CreditCard(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetString(2),
                        Reader.GetByte(3),
                        Reader.GetInt16(4),
                        Reader.GetDateTime(5)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                Sales.Currency.AddOrThrow(
                    new Currency(
                        Reader.GetString(0),
                        Reader.GetString(1),
                        Reader.GetDateTime(2)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                Sales.SalesReason.AddOrThrow(
                    new SalesReason(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetString(2),
                        Reader.GetDateTime(3)
                    )
                );
            Reader.NextResult();
            while(Reader.Read())
                Sales.SpecialOffer.AddOrThrow(
                    new SpecialOffer(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetDecimal(2),
                        Reader.GetString(3),
                        Reader.GetString(4),
                        Reader.GetDateTime(5),
                        Reader.GetDateTime(6),
                        Reader.GetInt32(7),
                        GetInt32(Reader,8),
                        Reader.GetGuid(9),
                        Reader.GetDateTime(10)
                    )
                );
            //1
            Reader.NextResult();
            while(Reader.Read())
                Person.Person.AddOrThrow(
                    new Person(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetBoolean(2),
                        GetString(Reader,3),
                        Reader.GetString(4),
                        GetString(Reader,5),
                        Reader.GetString(6),
                        GetString(Reader,7),
                        Reader.GetInt32(8),
                        GetXDocument(Reader,9),
                        GetXDocument(Reader,10),
                        Reader.GetGuid(11),
                        Reader.GetDateTime(12)
                    )
                );
            //2
            Reader.NextResult();
            while(Reader.Read())
                Person.PersonPhone.AddOrThrow(
                    new PersonPhone(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetInt32(2),
                        Reader.GetDateTime(3)
                    )
                );
            //3
            Reader.NextResult();
            while(Reader.Read())
                Production.ProductModelIllustration.AddOrThrow(
                    new ProductModelIllustration(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetDateTime(2)
                    )
                );
            //4
            Reader.NextResult();
            while(Reader.Read())
                Production.ProductModelProductDescriptionCulture.AddOrThrow(
                    new ProductModelProductDescriptionCulture(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetString(2),
                        Reader.GetDateTime(3)
                    )
                );
            //5
            Reader.NextResult();
            while(Reader.Read())
                Production.ProductSubcategory.AddOrThrow(
                    new ProductSubcategory(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetString(2),
                        Reader.GetGuid(3),
                        Reader.GetDateTime(4)
                    )
                );
            //6
            Reader.NextResult();
            while(Reader.Read())
                Purchasing.Vendor.AddOrThrow(
                    new Vendor(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetString(2),
                        Reader.GetByte(3),
                        Reader.GetBoolean(4),
                        Reader.GetBoolean(5),
                        GetString(Reader,6),
                        Reader.GetDateTime(7)
                    )
                );
            //7
            Reader.NextResult();
            while(Reader.Read())
                Sales.CountryRegionCurrency.AddOrThrow(
                    new CountryRegionCurrency(
                        Reader.GetString(0),
                        Reader.GetString(1),
                        Reader.GetDateTime(2)
                    )
                );
            //8
            Reader.NextResult();
            while(Reader.Read())
                Sales.CurrencyRate.AddOrThrow(
                    new CurrencyRate(
                        Reader.GetInt32(0),
                        Reader.GetDateTime(1),
                        Reader.GetString(2),
                        Reader.GetString(3),
                        Reader.GetDecimal(4),
                        Reader.GetDecimal(5),
                        Reader.GetDateTime(6)
                    )
                );
            //9
            Reader.NextResult();
            while(Reader.Read())
                Sales.PersonCreditCard.AddOrThrow(
                    new PersonCreditCard(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetDateTime(2)
                    )
                );
            //10
            Reader.NextResult();
            while(Reader.Read())
                Sales.SalesTerritory.AddOrThrow(
                    new SalesTerritory(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetString(2),
                        Reader.GetString(3),
                        Reader.GetDecimal(4),
                        Reader.GetDecimal(5),
                        Reader.GetDecimal(6),
                        Reader.GetDecimal(7),
                        Reader.GetGuid(8),
                        Reader.GetDateTime(9)
                    )
                );
            //11
            Reader.NextResult();
            while(Reader.Read())
                HumanResources.Employee.AddOrThrow(
                    new Employee(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetString(2),
                        GetHierarchyidNullable(Reader,3),
                        GetInt16(Reader,4),
                        Reader.GetString(5),
                        Reader.GetDateTime(6),
                        Reader.GetString(7),
                        Reader.GetString(8),
                        Reader.GetDateTime(9),
                        Reader.GetBoolean(10),
                        Reader.GetInt16(11),
                        Reader.GetInt16(12),
                        Reader.GetBoolean(13),
                        Reader.GetGuid(14),
                        Reader.GetDateTime(15)
                    )
                );
            //12
            Reader.NextResult();
            while(Reader.Read())
                HumanResources.EmployeeDepartmentHistory.AddOrThrow(
                    new EmployeeDepartmentHistory(
                        Reader.GetInt32(0),
                        Reader.GetInt16(1),
                        Reader.GetByte(2),
                        Reader.GetDateTime(3),
                        GetDateTime(Reader,4),
                        Reader.GetDateTime(5)
                    )
                );
            //13
            Reader.NextResult();
            while(Reader.Read())
                HumanResources.EmployeePayHistory.AddOrThrow(
                    new EmployeePayHistory(
                        Reader.GetInt32(0),
                        Reader.GetDateTime(1),
                        Reader.GetDecimal(2),
                        Reader.GetByte(3),
                        Reader.GetDateTime(4)
                    )
                );
            //14
            Reader.NextResult();
            while(Reader.Read())
                HumanResources.JobCandidate.AddOrThrow(
                    new JobCandidate(
                        Reader.GetInt32(0),
                        GetInt32(Reader,1),
                        GetXDocument(Reader,2),
                        Reader.GetDateTime(3)
                    )
                );
            //15
            Reader.NextResult();
            while(Reader.Read())
                Person.BusinessEntityContact.AddOrThrow(
                    new BusinessEntityContact(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetInt32(2),
                        Reader.GetGuid(3),
                        Reader.GetDateTime(4)
                    )
                );
            //16
            Reader.NextResult();
            while(Reader.Read())
                Person.EmailAddress.AddOrThrow(
                    new EmailAddress(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        GetString(Reader,2),
                        Reader.GetGuid(3),
                        Reader.GetDateTime(4)
                    )
                );
            //17
            Reader.NextResult();
            while(Reader.Read())
                Person.Password.AddOrThrow(
                    new Password(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetString(2),
                        Reader.GetGuid(3),
                        Reader.GetDateTime(4)
                    )
                );
            //18
            Reader.NextResult();
            while(Reader.Read())
                Person.StateProvince.AddOrThrow(
                    new StateProvince(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetString(2),
                        Reader.GetBoolean(3),
                        Reader.GetString(4),
                        Reader.GetInt32(5),
                        Reader.GetGuid(6),
                        Reader.GetDateTime(7)
                    )
                );
            //19
            Reader.NextResult();
            while(Reader.Read())
                Production.Document.AddOrThrow(
                    new Document(
                        GetHierarchyid(Reader,0),
                        GetInt16(Reader,1),
                        Reader.GetString(2),
                        Reader.GetInt32(3),
                        Reader.GetBoolean(4),
                        Reader.GetString(5),
                        Reader.GetString(6),
                        Reader.GetString(7),
                        Reader.GetInt32(8),
                        Reader.GetByte(9),
                        GetString(Reader,10),
                        GetBytes(Reader,11),
                        Reader.GetGuid(12),
                        Reader.GetDateTime(13)
                    )
                );
            //20
            Reader.NextResult();
            while(Reader.Read())
                Production.Product.AddOrThrow(
                    new Product(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetString(2),
                        Reader.GetBoolean(3),
                        Reader.GetBoolean(4),
                        GetString(Reader,5),
                        Reader.GetInt16(6),
                        Reader.GetInt16(7),
                        Reader.GetDecimal(8),
                        Reader.GetDecimal(9),
                        GetString(Reader,10),
                        GetString(Reader,11),
                        GetString(Reader,12),
                        GetDecimal(Reader,13),
                        Reader.GetInt32(14),
                        GetString(Reader,15),
                        GetString(Reader,16),
                        GetString(Reader,17),
                        GetInt32(Reader,18),
                        GetInt32(Reader,19),
                        Reader.GetDateTime(20),
                        GetDateTime(Reader,21),
                        GetDateTime(Reader,22),
                        Reader.GetGuid(23),
                        Reader.GetDateTime(24)
                    )
                );
            //21
            Reader.NextResult();
            while(Reader.Read())
                Production.ProductCostHistory.AddOrThrow(
                    new ProductCostHistory(
                        Reader.GetInt32(0),
                        Reader.GetDateTime(1),
                        GetDateTime(Reader,2),
                        Reader.GetDecimal(3),
                        Reader.GetDateTime(4)
                    )
                );
            //22
            Reader.NextResult();
            while(Reader.Read()){
                //[ProductDocument].[DocumentNode]に対応するタプルが[Document]に存在しなかった。
                //SqlHierarchyId.Equalsは実装されていない
                Production.ProductDocument.AddOrThrow(
                    new ProductDocument(
                        Reader.GetInt32(0),
                        GetHierarchyid(Reader,1),
                        Reader.GetDateTime(2)
                    )
                );
            }
            //23
            Reader.NextResult();
            while(Reader.Read())
                Production.ProductInventory.AddOrThrow(
                    new ProductInventory(
                        Reader.GetInt32(0),
                        Reader.GetInt16(1),
                        Reader.GetString(2),
                        Reader.GetByte(3),
                        Reader.GetInt16(4),
                        Reader.GetGuid(5),
                        Reader.GetDateTime(6)
                    )
                );
            //24
            Reader.NextResult();
            while(Reader.Read())
                Production.ProductListPriceHistory.AddOrThrow(
                    new ProductListPriceHistory(
                        Reader.GetInt32(0),
                        Reader.GetDateTime(1),
                        GetDateTime(Reader,2),
                        Reader.GetDecimal(3),
                        Reader.GetDateTime(4)
                    )
                );
            //25
            Reader.NextResult();
            while(Reader.Read())
                Production.ProductProductPhoto.AddOrThrow(
                    new ProductProductPhoto(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetBoolean(2),
                        Reader.GetDateTime(3)
                    )
                );
            //26
            Reader.NextResult();
            while(Reader.Read())
                Production.ProductReview.AddOrThrow(
                    new ProductReview(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetString(2),
                        Reader.GetDateTime(3),
                        Reader.GetString(4),
                        Reader.GetInt32(5),
                        GetString(Reader,6),
                        Reader.GetDateTime(7)
                    )
                );
            //27
            Reader.NextResult();
            while(Reader.Read())
                Production.TransactionHistory.AddOrThrow(
                    new TransactionHistory(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetInt32(2),
                        Reader.GetInt32(3),
                        Reader.GetDateTime(4),
                        Reader.GetString(5),
                        Reader.GetInt32(6),
                        Reader.GetDecimal(7),
                        Reader.GetDateTime(8)
                    )
                );
            //28
            Reader.NextResult();
            while(Reader.Read())
                Production.WorkOrder.AddOrThrow(
                    new WorkOrder(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetInt32(2),
                        Reader.GetInt32(3),
                        Reader.GetInt16(4),
                        Reader.GetDateTime(5),
                        GetDateTime(Reader,6),
                        Reader.GetDateTime(7),
                        GetInt16(Reader,8),
                        Reader.GetDateTime(9)
                    )
                );
            //29
            Reader.NextResult();
            while(Reader.Read())
                Production.WorkOrderRouting.AddOrThrow(
                    new WorkOrderRouting(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetInt16(2),
                        Reader.GetInt16(3),
                        Reader.GetDateTime(4),
                        Reader.GetDateTime(5),
                        GetDateTime(Reader,6),
                        GetDateTime(Reader,7),
                        GetDecimal(Reader,8),
                        Reader.GetDecimal(9),
                        GetDecimal(Reader,10),
                        Reader.GetDateTime(11)
                    )
                );
            //30
            Reader.NextResult();
            while(Reader.Read())
                Purchasing.ProductVendor.AddOrThrow(
                    new ProductVendor(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetInt32(2),
                        Reader.GetDecimal(3),
                        GetDecimal(Reader,4),
                        GetDateTime(Reader,5),
                        Reader.GetInt32(6),
                        Reader.GetInt32(7),
                        GetInt32(Reader,8),
                        Reader.GetString(9),
                        Reader.GetDateTime(10)
                    )
                );
            //31
            Reader.NextResult();
            while(Reader.Read())
                Purchasing.PurchaseOrderHeader.AddOrThrow(
                    new PurchaseOrderHeader(
                        Reader.GetInt32(0),
                        Reader.GetByte(1),
                        Reader.GetByte(2),
                        Reader.GetInt32(3),
                        Reader.GetInt32(4),
                        Reader.GetInt32(5),
                        Reader.GetDateTime(6),
                        Reader.GetDateTime(7),
                        Reader.GetDecimal(8),
                        Reader.GetDecimal(9),
                        Reader.GetDecimal(10),
                        Reader.GetDecimal(11),
                        Reader.GetDateTime(12)
                    )
                );
            //32
            Reader.NextResult();
            while(Reader.Read())
                Sales.SalesPerson.AddOrThrow(
                    new SalesPerson(
                        Reader.GetInt32(0),
                        GetInt32(Reader,1),
                        GetDecimal(Reader,2),
                        Reader.GetDecimal(3),
                        Reader.GetDecimal(4),
                        Reader.GetDecimal(5),
                        Reader.GetDecimal(6),
                        Reader.GetGuid(7),
                        Reader.GetDateTime(8)
                    )
                );
            //33
            Reader.NextResult();
            while(Reader.Read())
                Sales.SalesPersonQuotaHistory.AddOrThrow(
                    new SalesPersonQuotaHistory(
                        Reader.GetInt32(0),
                        Reader.GetDateTime(1),
                        Reader.GetDecimal(2),
                        Reader.GetGuid(3),
                        Reader.GetDateTime(4)
                    )
                );
            //34
            Reader.NextResult();
            while(Reader.Read())
                Sales.SalesTaxRate.AddOrThrow(
                    new SalesTaxRate(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetByte(2),
                        Reader.GetDecimal(3),
                        Reader.GetString(4),
                        Reader.GetGuid(5),
                        Reader.GetDateTime(6)
                    )
                );
            //35
            Reader.NextResult();
            while(Reader.Read())
                Sales.SalesTerritoryHistory.AddOrThrow(
                    new SalesTerritoryHistory(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetDateTime(2),
                        GetDateTime(Reader,3),
                        Reader.GetGuid(4),
                        Reader.GetDateTime(5)
                    )
                );
            //36
            Reader.NextResult();
            while(Reader.Read())
                Sales.ShoppingCartItem.AddOrThrow(
                    new ShoppingCartItem(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetInt32(2),
                        Reader.GetInt32(3),
                        Reader.GetDateTime(4),
                        Reader.GetDateTime(5)
                    )
                );
            //37
            Reader.NextResult();
            while(Reader.Read())
                Sales.SpecialOfferProduct.AddOrThrow(
                    new SpecialOfferProduct(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetGuid(2),
                        Reader.GetDateTime(3)
                    )
                );
            //38
            Reader.NextResult();
            while(Reader.Read())
                Sales.Store.AddOrThrow(
                    new Store(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        GetInt32(Reader,2),
                        GetXDocument(Reader,3),
                        Reader.GetGuid(4),
                        Reader.GetDateTime(5)
                    )
                );
            //39
            Reader.NextResult();
            while(Reader.Read())
                Person.Address.AddOrThrow(
                    new Address(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        GetString(Reader,2),
                        Reader.GetString(3),
                        Reader.GetInt32(4),
                        Reader.GetString(5),
                        GetSqlGeography(Reader,6),
                        Reader.GetGuid(7),
                        Reader.GetDateTime(8)
                    )
                );
            //40
            Reader.NextResult();
            while(Reader.Read())
                Person.BusinessEntityAddress.AddOrThrow(
                    new BusinessEntityAddress(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetInt32(2),
                        Reader.GetGuid(3),
                        Reader.GetDateTime(4)
                    )
                );
            //41
            Reader.NextResult();
            while(Reader.Read())
                Production.BillOfMaterials.AddOrThrow(
                    new BillOfMaterials(
                        Reader.GetInt32(0),
                        GetInt32(Reader,1),
                        Reader.GetInt32(2),
                        Reader.GetDateTime(3),
                        GetDateTime(Reader,4),
                        Reader.GetString(5),
                        Reader.GetInt16(6),
                        Reader.GetDecimal(7),
                        Reader.GetDateTime(8)
                    )
                );
            //42
            Reader.NextResult();
            while(Reader.Read())
                Purchasing.PurchaseOrderDetail.AddOrThrow(
                    new PurchaseOrderDetail(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetDateTime(2),
                        Reader.GetInt16(3),
                        Reader.GetInt32(4),
                        Reader.GetDecimal(5),
                        Reader.GetDecimal(6),
                        Reader.GetDecimal(7),
                        Reader.GetDecimal(8),
                        Reader.GetDecimal(9),
                        Reader.GetDateTime(10)
                    )
                );
            //43
            Reader.NextResult();
            while(Reader.Read())
                Sales.Customer.AddOrThrow(
                    new Customer(
                        Reader.GetInt32(0),
                        GetInt32(Reader,1),
                        GetInt32(Reader,2),
                        GetInt32(Reader,3),
                        GetString(Reader,4),
                        Reader.GetGuid(5),
                        Reader.GetDateTime(6)
                    )
                );
            //44
            Reader.NextResult();
            while(Reader.Read())
                Sales.SalesOrderHeader.AddOrThrow(
                    new SalesOrderHeader(
                        Reader.GetInt32(0),
                        Reader.GetByte(1),
                        Reader.GetDateTime(2),
                        Reader.GetDateTime(3),
                        GetDateTime(Reader,4),
                        Reader.GetByte(5),
                        Reader.GetBoolean(6),
                        Reader.GetString(7),
                        GetString(Reader,8),
                        GetString(Reader,9),
                        Reader.GetInt32(10),
                        GetInt32(Reader,11),
                        GetInt32(Reader,12),
                        Reader.GetInt32(13),
                        Reader.GetInt32(14),
                        Reader.GetInt32(15),
                        GetInt32(Reader,16),
                        GetString(Reader,17),
                        GetInt32(Reader,18),
                        Reader.GetDecimal(19),
                        Reader.GetDecimal(20),
                        Reader.GetDecimal(21),
                        Reader.GetDecimal(22),
                        GetString(Reader,23),
                        Reader.GetGuid(24),
                        Reader.GetDateTime(25)
                    )
                );
            //45
            Reader.NextResult();
            while(Reader.Read())
                Sales.SalesOrderHeaderSalesReason.AddOrThrow(
                    new SalesOrderHeaderSalesReason(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetDateTime(2)
                    )
                );
            //46
            Reader.NextResult();
            while(Reader.Read())
                Sales.SalesOrderDetail.AddOrThrow(
                    new SalesOrderDetail(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        GetString(Reader,2),
                        Reader.GetInt16(3),
                        Reader.GetInt32(4),
                        Reader.GetInt32(5),
                        Reader.GetDecimal(6),
                        Reader.GetDecimal(7),
                        Reader.GetDecimal(8),
                        Reader.GetGuid(9),
                        Reader.GetDateTime(10)
                    )
                );
            Console.WriteLine($"Add {s.ElapsedMilliseconds,7}ms");
            Reader.Close();
            //列挙(e);
            uspGetBillOfMaterials(e,3,new DateTime(2010,8,18));
            vEmployee(e,Command);
            vEmployeeDepartment(e,Command);
            vEmployeeDepartmentHistory(e,Command);
            vJobCandidate(e,Command);
            vJobCandidateEducation(e,Command);
            vJobCandidateEmployment(e,Command);
            vAdditionalContactInfo(e,Command);
            vStateProvinceCountryRegion(e,Command);
            vProductAndDescription(e,Command);
            vProductModelCatalogDescription(e,Command);
            vProductModelInstructions(e,Command);
            vVendorWithAddresses(e,Command);
            vVendorWithContacts(e,Command);
            vIndividualCustomer(e,Command);
            vPersonDemographics(e,Command);
            vSalesPerson(e,Command);
            vSalesPersonSalesByFiscalYears(e,Command);
            vStoreWithAddresses(e,Command);
            vStoreWithContacts(e,Command);
            vStoreWithDemographics(e,Command);
            e.Clear();
        }
        private static void vEmployee(Container e,SqlCommand Command) => 比較(
            () =>
                from e in e.HumanResources.Employee
                join p in e.Person.Person on e.BusinessEntityID equals p.BusinessEntityID
                join bea in e.Person.BusinessEntityAddress on e.BusinessEntityID equals bea.BusinessEntityID
                join a in e.Person.Address on bea.AddressID equals a.AddressID
                join sp in e.Person.StateProvince on a.StateProvinceID equals sp.StateProvinceID
                join cr in e.Person.CountryRegion on sp.CountryRegionCode equals cr.CountryRegionCode
                join pp in e.Person.PersonPhone on p.BusinessEntityID equals pp.BusinessEntityID into pp1
                from pp in pp1
                join pnt in e.Person.PhoneNumberType on pp.PhoneNumberTypeID equals pnt.PhoneNumberTypeID into pnt1
                from pnt in pnt1
                join ea in e.Person.EmailAddress on p.BusinessEntityID equals ea.BusinessEntityID into ea1
                from ea in ea1
                select new {
                    e.BusinessEntityID,
                    p.Title,
                    p.FirstName,
                    p.MiddleName,
                    p.LastName,
                    p.Suffix,
                    e.JobTitle,
                    pp.PhoneNumber,
                    PhoneNumberType = pnt.Name,
                    ea.EmailAddress,
                    p.EmailPromotion,
                    a.AddressLine1,
                    a.AddressLine2,
                    a.City,
                    StateProvinceName = sp.Name,
                    a.PostalCode,
                    CountryRegionName = cr.Name,
                    p.AdditionalContactInfo
                },
            Command,
            "SELECT * FROM HumanResources.vEmployee",
            Reader => new {
                BusinessEntityID = Reader.GetInt32(0),
                Title = GetString(Reader,1),
                FirstName = Reader.GetString(2),
                MiddleName = GetString(Reader,3),
                LastName = Reader.GetString(4),
                Suffix = GetString(Reader,5),
                JobTitle = Reader.GetString(6),
                PhoneNumber = GetString(Reader,7),
                PhoneNumberType = GetString(Reader,8),
                EmailAddress = GetString(Reader,9),
                EmailPromotion = Reader.GetInt32(10),
                AddressLine1 = Reader.GetString(11),
                AddressLine2 = GetString(Reader,12),
                City = Reader.GetString(13),
                StateProvinceName = Reader.GetString(14),
                PostalCode = Reader.GetString(15),
                CountryRegionName = Reader.GetString(16),
                AdditionalContactInfo = GetXDocument(Reader,17),
            });
        private static void vEmployeeDepartment(Container Container,SqlCommand Command) {
            比較(
                () =>
                    from e in Container.HumanResources.Employee
                    join p in Container.Person.Person on e.BusinessEntityID equals p.BusinessEntityID
                    join edh in Container.HumanResources.EmployeeDepartmentHistory on e.BusinessEntityID equals edh.BusinessEntityID
                    join d in Container.HumanResources.Department on edh.DepartmentID equals d.DepartmentID
                    where edh.EndDate==null
                    select new {
                        e.BusinessEntityID,
                        p.Title,
                        p.FirstName,
                        p.MiddleName,
                        p.LastName,
                        p.Suffix,
                        e.JobTitle,
                        Department = d.Name,
                        d.GroupName,
                        edh.StartDate,
                    },
                Command,
                "SELECT * FROM HumanResources.vEmployeeDepartment",
                Reader => new {
                    BusinessEntityID = Reader.GetInt32(0),
                    Title = Reader.IsDBNull(1) ? null : Reader.GetString(1),
                    FirstName = Reader.GetString(2),
                    MiddleName = Reader.IsDBNull(3) ? null : Reader.GetString(3),
                    LastName = Reader.GetString(4),
                    Suffix = Reader.IsDBNull(5) ? null : Reader.GetString(5),
                    JobTitle = Reader.GetString(6),
                    Department = Reader.GetString(7),
                    GroupName = Reader.GetString(8),
                    StartDate = Reader.GetDateTime(9)
                });
        }
        private static void vEmployeeDepartmentHistory(Container e,SqlCommand Command) => 比較(
            () =>
                from e in e.HumanResources.Employee
                join p in e.Person.Person on e.BusinessEntityID equals p.BusinessEntityID
                join edh in e.HumanResources.EmployeeDepartmentHistory on e.BusinessEntityID equals edh.BusinessEntityID
                join d in e.HumanResources.Department on edh.DepartmentID equals d.DepartmentID
                join s in e.HumanResources.Shift on edh.ShiftID equals s.ShiftID
                select new {
                    e.BusinessEntityID,
                    p.Title,
                    p.FirstName,
                    p.MiddleName,
                    p.LastName,
                    p.Suffix,
                    Shift = s.Name,
                    Department = d.Name,
                    d.GroupName,
                    edh.StartDate,
                    edh.EndDate,
                },
            Command,
            "SELECT * FROM HumanResources.vEmployeeDepartmentHistory",
            Reader => {
                var r = new {
                    BusinessEntityID = Reader.GetInt32(0),
                    Title = Reader.IsDBNull(1) ? null : Reader.GetString(1),
                    FirstName = Reader.GetString(2),
                    MiddleName = Reader.IsDBNull(3) ? null : Reader.GetString(3),
                    LastName = Reader.GetString(4),
                    Suffix = Reader.IsDBNull(5) ? null : Reader.GetString(5),
                    Shift = Reader.GetString(6),
                    Department = Reader.GetString(7),
                    GroupName = Reader.GetString(8),
                    StartDate =Reader.GetDateTime(9),
                    EndDate = GetDateTime(Reader,10)
                };
                //r.EndDate;
                return r;
            });
        private static readonly XmlNamespaceManager ci = new(new NameTable());
        private static readonly XmlNamespaceManager crm = new(new NameTable());
        private static readonly XmlNamespaceManager act = new(new NameTable());
        private static readonly XmlNamespaceManager ContactInfo = new(new NameTable());
        static Program() {
            ci.AddNamespace(nameof(ci),"http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactInfo");
            crm.AddNamespace(nameof(crm),"http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactRecord");
            act.AddNamespace(nameof(act),"http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes");
           
            ContactInfo.AddNamespace(nameof(ContactInfo),"http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactInfo");
        }
        //private static String? EvaluateString(XNode Resume_ref,String 式,XmlNamespaceManager XmlNamespaceManager) => Resume_ref.XPathEvaluate(式,XmlNamespaceManager)?.ToString();
        private static string? InnerString(XNode? XNode,string 式,XmlNamespaceManager XmlNamespaceManager) {
            var x = XNode?.XPathEvaluate(式,XmlNamespaceManager);
            if(x==null) return null;
            if(x is System.Collections.Generic.IEnumerable<object> Enumerable1) {
                foreach(var a in Enumerable1) {
                    var XText = (XText)a;
                    return XText.Value;
                }
            }
            if(x is string s) {
                return s;
            }
            return null;
        }
        private static int? TagInt32(XNode XNode,string 式,XmlNamespaceManager XmlNamespaceManager) {
            var XElement = XNode.XPathSelectElement(式,XmlNamespaceManager);
            if(XElement==null) return null;
            return int.Parse(XElement.Value,CultureInfo.InvariantCulture);
        }
        private static bool? TagBoolean(XNode XNode,string 式,XmlNamespaceManager XmlNamespaceManager) {
            var XElement = XNode.XPathSelectElement(式,XmlNamespaceManager);
            if(XElement==null) return null;
            return XElement.Value=="1";
        }
        //private static Int32? AttributeInt32(XNode XNode,String 式,String Attribute,XmlNamespaceManager XmlNamespaceManager) {
        //    var XElement = XNode.XPathSelectElement(式,XmlNamespaceManager);
        //    if(XElement==null) return null;
        //    return Int32.Parse(XElement.Attribute(Attribute).Value,CultureInfo.InvariantCulture);
        //}
        private static string? AttributeString(XElement XNode,string Attribute) {
            return XNode.Attribute(Attribute)?.Value;
        }
        private static int? AttributeInt32(XElement XNode,string Attribute) {
            var r = AttributeString(XNode,Attribute);
            if(r==null) return null;
            return int.Parse(r,CultureInfo.InvariantCulture);
        }
        private static decimal? AttributeDecimal(XElement XNode,string Attribute) {
            var r = AttributeString(XNode,Attribute);
            if(r==null) return null;
            return decimal.Round(decimal.Parse(r,CultureInfo.InvariantCulture),4,MidpointRounding.AwayFromZero);
        }
        private static decimal? TagDecimal(XNode XNode,string 式,XmlNamespaceManager XmlNamespaceManager) {
            var XElement = XNode.XPathSelectElement(式,XmlNamespaceManager);
            if(XElement==null) return null;
            return decimal.Round(decimal.Parse(XElement.Value,CultureInfo.InvariantCulture),4,MidpointRounding.AwayFromZero);
        }
        private static string? AttributeString(XNode XNode,string 式,string Attribute,XmlNamespaceManager XmlNamespaceManager) {
            var XElement = XNode.XPathSelectElement(式,XmlNamespaceManager);
            return XElement?.Attribute(Attribute)?.Value;
        }
        private static string? TagString(XNode XNode,string 式,XmlNamespaceManager XmlNamespaceManager) {
            var XElement = XNode.XPathSelectElement(式,XmlNamespaceManager);
            return XElement?.Value;
        }
        private static string? TagString(XNode XNode,string 式,XmlNamespaceManager XmlNamespaceManager,int Length) {
            var XElement = XNode.XPathSelectElement(式,XmlNamespaceManager);
            if(XElement==null) return null;
            var s=XElement.Value;
            if(s.Length<Length) return s;
            return s.Substring(0,Length);
        }
        private static DateTime? TagDateTime(XNode XNode,string 式,XmlNamespaceManager XmlNamespaceManager) {
            var XElement = XNode.XPathSelectElement(式,XmlNamespaceManager);
            if(XElement==null) return null;
            return DateTime.Parse(XElement.Value,CultureInfo.CurrentCulture,DateTimeStyles.RoundtripKind);
        }
        private static DateTimeOffset? TagDateTimeOffset(XNode XNode,string 式,XmlNamespaceManager XmlNamespaceManager) {
            var XElement = XNode.XPathSelectElement(式,XmlNamespaceManager);
            if(XElement==null) return null;
            return DateTimeOffset.Parse(XElement.Value,CultureInfo.CurrentCulture,DateTimeStyles.RoundtripKind);
        }
        private static void vJobCandidate(Container e,SqlCommand Command) {
            var ns = new XmlNamespaceManager(new NameTable());
            ns.AddNamespace("ns","http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/Resume");
            比較(
                () =>
                    from jc in e.HumanResources.JobCandidate
                    from Resume_ref in jc.Resume.XPathSelectElements("/ns:Resume",ns)
                    select new {
                        jc.JobCandidateID,
                        jc.BusinessEntityID,
                        Prefix = TagString(Resume_ref,"ns:Name/ns:Name.Prefix",ns,30),
                        First = TagString(Resume_ref,"ns:Name/ns:Name.First",ns,30),
                        Middle = TagString(Resume_ref,"ns:Name/ns:Name.Middle",ns,30),
                        Last = TagString(Resume_ref,"ns:Name/ns:Name.Last",ns,30),
                        Suffix = TagString(Resume_ref,"ns:Name/ns:Name.Suffix",ns,30),
                        Skills = TagString(Resume_ref,"ns:Skills",ns),
                        Type = TagString(Resume_ref,"ns:Address/ns:Addr.Type",ns,30),
                        CountryRegion = TagString(Resume_ref,"ns:Address/ns:Addr.Location/ns:Location/ns:Loc.CountryRegion",ns,100),
                        State = TagString(Resume_ref,"ns:Address/ns:Addr.Location/ns:Location/ns:Loc.State",ns,100),
                        City = TagString(Resume_ref,"ns:Address/ns:Addr.Location/ns:Location/ns:Loc.City",ns,100),
                        PostalCode = TagString(Resume_ref,"ns:Address/ns:Addr.PostalCode",ns,20),
                        EMail = TagString(Resume_ref,"ns:EMail",ns),
                        WebSite = TagString(Resume_ref,"ns:WebSite",ns),
                        jc.ModifiedDate
                    },
                Command,
                "SELECT * FROM HumanResources.vJobCandidate",
                Reader => new {
                    JobCandidateID = Reader.GetInt32(0),
                    BusinessEntityID = Reader.IsDBNull(1) ? default(int?) : Reader.GetInt32(1),
                    Prefix        = GetString(Reader,2),
                    First         = GetString(Reader,3),
                    Middle        = GetString(Reader,4),
                    Last          = GetString(Reader,5),
                    Suffix        = GetString(Reader,6),
                    Skills        = GetString(Reader,7),
                    Type          = GetString(Reader,8),
                    CountryRegion = GetString(Reader,9),
                    State         = GetString(Reader,10),
                    City          = GetString(Reader,11),
                    PostalCode    = GetString(Reader,12),
                    EMail         = GetString(Reader,13),
                    WebSite       = GetString(Reader,14),
                    ModifiedDate  = Reader.GetDateTime(15)
                });
        }
        private static void vJobCandidateEducation(Container e,SqlCommand Command) {
            var ns = new XmlNamespaceManager(new NameTable());
            ns.AddNamespace("ns","http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/Resume");
            比較(
                () =>
                    from jc in e.HumanResources.JobCandidate
                    from Resume_ref in jc.Resume.XPathSelectElements("/ns:Resume/ns:Education",ns)
                    select new {
                        jc.JobCandidateID,
                        Level = TagString(Resume_ref,"ns:Edu.Level",ns),
                        StartDate = TagDateTime(Resume_ref,"ns:Edu.StartDate",ns),
                        EndDate = TagDateTime(Resume_ref,"ns:Edu.EndDate",ns),
                        Degree = TagString(Resume_ref,"ns:Edu.Degree",ns,50),
                        Major = TagString(Resume_ref,"ns:Edu.Major",ns,50),
                        Minor = TagString(Resume_ref,"ns:Edu.Minor",ns,50),
                        GPA = TagString(Resume_ref,"ns:Edu.GPA",ns,5),
                        GPAScale = TagString(Resume_ref,"ns:Edu.GPAScale",ns,5),
                        School = TagString(Resume_ref,"ns:Edu.School",ns,100),
                        CountryRegion = TagString(Resume_ref,"ns:Edu.Location/ns:Location/ns:Loc.CountryRegion",ns,100),
                        State = TagString(Resume_ref,"ns:Edu.Location/ns:Location/ns:Loc.State",ns,100),
                        City = TagString(Resume_ref,"ns:Edu.Location/ns:Location/ns:Loc.City",ns,100),
                    },
                Command,
                "SELECT * FROM HumanResources.vJobCandidateEducation",
                Reader => new {
                    JobCandidateID = Reader.GetInt32(0),
                    Level          = GetString(Reader,1),
                    StartDate      = GetDateTime(Reader,2),
                    EndDate        = GetDateTime(Reader,3),
                    Degree         = GetString(Reader,4),
                    Major          = GetString(Reader,5),
                    Minor          = GetString(Reader,6),
                    GPA            = GetString(Reader,7),
                    GPAScale       = GetString(Reader,8),
                    School         = GetString(Reader,9),
                    CountryRegion  = GetString(Reader,10),
                    State          = GetString(Reader,11),
                    City           = GetString(Reader,12),
                });
        }
        private static void vJobCandidateEmployment(Container e,SqlCommand Command) {
            var ns = new XmlNamespaceManager(new NameTable());
            ns.AddNamespace("ns","http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/Resume");
            比較(
                () =>
                    from jc in e.HumanResources.JobCandidate
                    from Resume_ref in jc.Resume.XPathSelectElements("/ns:Resume/ns:Employment",ns)
                    select new {
                        jc.JobCandidateID,
                        StartDate = TagDateTime(Resume_ref,"ns:Emp.StartDate",ns),
                        EndDate = TagDateTime(Resume_ref,"ns:Emp.EndDate",ns),
                        OrgName = TagString(Resume_ref,"ns:Emp.OrgName",ns,100),
                        JobTitle= TagString(Resume_ref,"ns:Emp.JobTitle",ns,100),
                        Responsibility = TagString(Resume_ref,"ns:Emp.Responsibility",ns),
                        FunctionCategory = TagString(Resume_ref,"ns:Emp.FunctionCategory",ns),
                        IndustryCategory = TagString(Resume_ref,"ns:Emp.IndustryCategory",ns),
                        CountryRegion = TagString(Resume_ref,"ns:Emp.Location/ns:Location/ns:Loc.CountryRegion",ns),
                        State = TagString(Resume_ref,"ns:Emp.Location/ns:Location/ns:Loc.State",ns),
                        City = TagString(Resume_ref,"ns:Emp.Location/ns:Location/ns:Loc.City",ns),
                    },
                Command,
                "SELECT * FROM HumanResources.vJobCandidateEmployment",
                Reader => new {
                    JobCandidateID   = Reader.GetInt32(0),
                    StartDate        = GetDateTime(Reader,1),
                    EndDate          = GetDateTime(Reader,2),
                    OrgName          = GetString(Reader,3),
                    JobTitle         = GetString(Reader,4),
                    Responsibility   = GetString(Reader,5),
                    FunctionCategory = GetString(Reader,6),
                    IndustryCategory = GetString(Reader,7),
                    CountryRegion    = GetString(Reader,8),
                    State            = GetString(Reader,9),
                    City             = GetString(Reader,10),
                });
        }
        private static void vAdditionalContactInfo(Container e,SqlCommand Command) {
            //{
            //    var x = (from Person in e.Person.Person
            //             let AdditionalContactInfo = Person.AdditionalContactInfo
            //             where AdditionalContactInfo!=null
            //             from ContactInfo_ref in AdditionalContactInfo.XPathSelectElements("/ContactInfo:AdditionalContactInfo",ContactInfo)
            //             select new {
            //                 Person.BusinessEntityID,
            //                 Person.FirstName,
            //                 Person.MiddleName,
            //                 Person.LastName,
            //                 TelephoneNumber = SelectString(ContactInfo_ref,"(act:telephoneNumber)[1]/act:number",act,50),
            //                 TelephoneSpecialInstructions = EvaluateString(ContactInfo_ref,"(act:telephoneNumber/act:SpecialInstructions/text())[1]",act),
            //                 Street = SelectString(ContactInfo_ref,"(act:homePostalAddress/act:Street)[1]",act),
            //                 City = SelectString(ContactInfo_ref,"(act:homePostalAddress/act:City)[1]",act,50),
            //                 StateProvince = SelectString(ContactInfo_ref,"(act:homePostalAddress/act:StateProvince)[1]",act,50),
            //                 PostalCode = SelectString(ContactInfo_ref,"(act:homePostalAddress/act:PostalCode)[1]",act,50),
            //                 CountryRegion = SelectString(ContactInfo_ref," (act:homePostalAddress/act:CountryRegion)[1]",act,50),
            //                 HomeAddressSpecialInstructions = EvaluateString(ContactInfo_ref,"(act:homePostalAddress/act:SpecialInstructions/text())[1]",act),
            //                 EMailAddress = SelectString(ContactInfo_ref,"(act:eMail/act:eMailAddress)[1]",act,128),
            //                 EMailSpecialInstructions = EvaluateString(ContactInfo_ref,"(act:eMail/act:SpecialInstructions/text())[1]",act),
            //                 EMailTelephoneNumber = SelectString(ContactInfo_ref,"(act:eMail/act:SpecialInstructions/act:telephoneNumber/act:number)[1]",act,50),
            //                 Person.rowguid,
            //                 Person.ModifiedDate,
            //             }).ToArray();
            //    var y = (from Person in e.Person.Person
            //             let AdditionalContactInfo = Person.AdditionalContactInfo
            //             where AdditionalContactInfo!=null
            //             from ContactInfo_ref in AdditionalContactInfo.XPathSelectElements("/ContactInfo:AdditionalContactInfo",ContactInfo)
            //             select Person
            //    ).Select(p => {
            //        var r=p.AdditionalContactInfo.XPathSelectElements("/act:AdditionalContactInfo",act);
            //        return r;
            //    });
            //    var z = (from Person in e.Person.Person
            //             let AdditionalContactInfo = Person.AdditionalContactInfo
            //             where AdditionalContactInfo!=null
            //             from ContactInfo_ref in AdditionalContactInfo.XPathSelectElements("/AdditionalContactInfo")
            //             select Person
            //    ).Select(p => {
            //        var r = p.AdditionalContactInfo.XPathSelectElements("/act:AdditionalContactInfo",act);
            //        return r;
            //    });
            //}
            比較(
                () =>
                    from Person in e.Person.Person
                    let AdditionalContactInfo = Person.AdditionalContactInfo
                    where AdditionalContactInfo!=null
                    from ContactInfo_ref in AdditionalContactInfo.XPathSelectElements("/ContactInfo:AdditionalContactInfo",ContactInfo)
                    select new {
                        Person.BusinessEntityID,
                        Person.FirstName,
                        Person.MiddleName,
                        Person.LastName,
                        TelephoneNumber = TagString(ContactInfo_ref,"(act:telephoneNumber)[1]/act:number",act,50),
                        TelephoneSpecialInstructions = InnerString(ContactInfo_ref,"(act:telephoneNumber/act:SpecialInstructions/text())[1]",act),
                        Street = TagString(ContactInfo_ref,"(act:homePostalAddress/act:Street)[1]",act),
                        City = TagString(ContactInfo_ref,"(act:homePostalAddress/act:City)[1]",act,50),
                        StateProvince = TagString(ContactInfo_ref,"(act:homePostalAddress/act:StateProvince)[1]",act,50),
                        PostalCode = TagString(ContactInfo_ref,"(act:homePostalAddress/act:PostalCode)[1]",act,50),
                        CountryRegion = TagString(ContactInfo_ref," (act:homePostalAddress/act:CountryRegion)[1]",act,50),
                        HomeAddressSpecialInstructions = InnerString(ContactInfo_ref,"(act:homePostalAddress/act:SpecialInstructions/text())[1]",act),
                        EMailAddress = TagString(ContactInfo_ref,"(act:eMail/act:eMailAddress)[1]",act,128),
                        EMailSpecialInstructions = InnerString(ContactInfo_ref,"(act:eMail/act:SpecialInstructions/text())[1]",act),
                        EMailTelephoneNumber = TagString(ContactInfo_ref,"(act:eMail/act:SpecialInstructions/act:telephoneNumber/act:number)[1]",act,50),
                        Person.rowguid,
                        Person.ModifiedDate,
                    },
                Command,
                "SELECT * FROM Person.vAdditionalContactInfo",
                Reader => new {
                    BusinessEntityID = Reader.GetInt32(0),
                    FirstName = Reader.GetString(1),
                    MiddleName = GetString(Reader,2),
                    LastName = Reader.GetString(3),
                    TelephoneNumber = GetString(Reader,4),
                    TelephoneSpecialInstructions = GetString(Reader,5),
                    Street = GetString(Reader,6),
                    City = GetString(Reader,7),
                    StateProvince = GetString(Reader,8),
                    PostalCode = GetString(Reader,9),
                    CountryRegion = GetString(Reader,10),
                    HomeAddressSpecialInstructions = GetString(Reader,11),
                    EMailAddress = GetString(Reader,12),
                    EMailSpecialInstructions = GetString(Reader,13),
                    EMailTelephoneNumber = GetString(Reader,14),
                    rowguid                        = Reader.GetGuid(15),
                    ModifiedDate                   = Reader.GetDateTime(16),
                });
        }
        private static void vStateProvinceCountryRegion(Container e,SqlCommand Command) {
            比較(
                () =>
                    from sp in e.Person.StateProvince
                    join cr in e.Person.CountryRegion on sp.CountryRegionCode equals cr.CountryRegionCode
                    select new {
                        sp.StateProvinceID,
                        sp.StateProvinceCode,
                        sp.IsOnlyStateProvinceFlag,
                        StateProvinceName=sp.Name,
                        sp.TerritoryID,
                        cr.CountryRegionCode,
                        CountryRegionName=cr.Name
                    },
                Command,
                "SELECT * FROM Person.vStateProvinceCountryRegion",
                Reader => new {
                    StateProvinceID = Reader.GetInt32(0),
                    StateProvinceCode = Reader.GetString(1),
                    IsOnlyStateProvinceFlag = Reader.GetBoolean(2),
                    StateProvinceName = Reader.GetString(3),
                    TerritoryID = Reader.GetInt32(4),
                    CountryRegionCode = Reader.GetString(5),
                    CountryRegionName = Reader.GetString(6),
                });
        }
        private static void vProductAndDescription(Container e,SqlCommand Command) {
            比較(
                () =>
                    from p in e.Production.Product
                    join pm in e.Production.ProductModel on p.ProductModelID equals pm.ProductModelID
                    join pmx in e.Production.ProductModelProductDescriptionCulture on pm.ProductModelID equals pmx.ProductModelID
                    join pd in e.Production.ProductDescription on pmx.ProductDescriptionID equals pd.ProductDescriptionID
                    select new {
                        p.ProductID,
                        p.Name,
                        ProductModel=pm.Name,
                        pmx.CultureID,
                        pd.Description,
                    },
                Command,
                "SELECT * FROM Production.vProductAndDescription",
                Reader => new {
                    ProductID = Reader.GetInt32(0),
                    Name = Reader.GetString(1),
                    ProductModel = Reader.GetString(2),
                    CultureID = Reader.GetString(3),
                    Description = Reader.GetString(4),
                });
        }
        private static void vProductModelCatalogDescription(Container e,SqlCommand Command) {
            var ns = new XmlNamespaceManager(new NameTable());
            ns.AddNamespace("p1","http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription");
            ns.AddNamespace("wm","http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain");
            ns.AddNamespace("wf","http://www.adventure-works.com/schemas/OtherFeatures");
            ns.AddNamespace("html","http://www.w3.org/1999/xhtml");
            比較(
                () =>
                    from pm in e.Production.ProductModel
                    where pm.CatalogDescription!=null
                    select new {
                        pm.ProductModelID,
                        pm.Name,
                        Summary = TagString(pm.CatalogDescription,"(/p1:ProductDescription/p1:Summary/html:p)[1]",ns),
                        Manufacturer = TagString(pm.CatalogDescription,"(/p1:ProductDescription/p1:Manufacturer/p1:Name)[1]",ns),
                        Copyright = TagString(pm.CatalogDescription,"(/p1:ProductDescription/p1:Manufacturer/p1:Copyright)[1]",ns,30),
                        ProductURL=TagString(pm.CatalogDescription,"(/p1:ProductDescription/p1:Manufacturer/p1:ProductURL)[1]",ns,256),
                        WarrantyPeriod = TagString(pm.CatalogDescription,"(/p1:ProductDescription/p1:Features/wm:Warranty/wm:WarrantyPeriod)[1]",ns,256),
                        WarrantyDescription = TagString(pm.CatalogDescription,"(/p1:ProductDescription/p1:Features/wm:Warranty/wm:Description)[1]",ns,256),
                        NoOfYears = TagString(pm.CatalogDescription,"(/p1:ProductDescription/p1:Features/wm:Maintenance/wm:NoOfYears)[1]",ns,256),
                        MaintenanceDescription=TagString(pm.CatalogDescription,"(/p1:ProductDescription/p1:Features/wm:Maintenance/wm:Description)[1]",ns,256),
                        Wheel=TagString(pm.CatalogDescription,"(/p1:ProductDescription/p1:Features/wf:wheel)[1]",ns,256),
                        Saddle=TagString(pm.CatalogDescription,"(/p1:ProductDescription/p1:Features/wf:saddle)[1]",ns,256),
                        Pedal =TagString(pm.CatalogDescription,"(/p1:ProductDescription/p1:Features/wf:pedal)[1]",ns,256),
                        BikeFrame =TagString(pm.CatalogDescription,"(/p1:ProductDescription/p1:Features/wf:BikeFrame)[1]",ns),
                        Crankset =TagString(pm.CatalogDescription,"(/p1:ProductDescription/p1:Features/wf:crankset)[1]",ns,256),
                        PictureAngle =TagString(pm.CatalogDescription,"(/p1:ProductDescription/p1:Picture/p1:Angle)[1]",ns,256),
                        PictureSize =TagString(pm.CatalogDescription,"(/p1:ProductDescription/p1:Picture/p1:Size)[1]",ns,256),
                        ProductPhotoID =TagString(pm.CatalogDescription,"(/p1:ProductDescription/p1:Picture/p1:ProductPhotoID)[1]",ns,256),
                        Material =TagString(pm.CatalogDescription,"(/p1:ProductDescription/p1:Specifications/Material)[1]",ns,256),
                        Color =TagString(pm.CatalogDescription,"(/p1:ProductDescription/p1:Specifications/Color)[1]",ns,256),
                        ProductLine =TagString(pm.CatalogDescription,"(/p1:ProductDescription/p1:Specifications/ProductLine)[1]",ns,256),
                        Style =TagString(pm.CatalogDescription,"(/p1:ProductDescription/p1:Specifications/Style)[1]",ns,256),
                        RiderExperience =TagString(pm.CatalogDescription,"(/p1:ProductDescription/p1:Specifications/RiderExperience)[1]",ns,1024),
                        pm.rowguid,
                        pm.ModifiedDate,
                    },
                Command,
                "SELECT * FROM Production.vProductModelCatalogDescription",
                Reader => new {
                    ProductModelID = Reader.GetInt32(0),
                    Name = Reader.GetString(1),
                    Summary = GetString(Reader,2),
                    Manufacturer = GetString(Reader,3),
                    Copyright= GetString(Reader,4),
                    ProductURL= GetString(Reader,5),
                    WarrantyPeriod= GetString(Reader,6),
                    WarrantyDescription= GetString(Reader,7),
                    NoOfYears= GetString(Reader,8),
                    MaintenanceDescription= GetString(Reader,9),
                    Wheel= GetString(Reader,10),
                    Saddle= GetString(Reader,11),
                    Pedal= GetString(Reader,12),
                    BikeFrame= GetString(Reader,13),
                    Crankset= GetString(Reader,14),
                    PictureAngle= GetString(Reader,15),
                    PictureSize = GetString(Reader,16),
                    ProductPhotoID= GetString(Reader,17),
                    Material = GetString(Reader,18),
                    Color= GetString(Reader,19),
                    ProductLine = GetString(Reader,20),
                    Style= GetString(Reader,21),
                    RiderExperience= GetString(Reader,22),
                    rowguid = Reader.GetGuid(23),
                    ModifiedDate = Reader.GetDateTime(24),
                });
        }
        private static void vProductModelInstructions(Container e,SqlCommand Command) {
            var ns = new XmlNamespaceManager(new NameTable());
            ns.AddNamespace("ns","http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions");
            //ns.AddNamespace(String.Empty,"http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions");
            {
                var s = (
                    e.Production.ProductModel.Where(
                        pm=> pm.Instructions!=null
                    ).SelectMany(
                        pm=> pm.Instructions.XPathSelectElements("/ns:root/ns:Location",ns),
                        (pm,MfgInstructions_ref)=>new { pm,MfgInstructions_ref }
                    ).SelectMany(
                        X=> X.MfgInstructions_ref.XPathSelectElements("ns:step",ns),
                        (pm_MfgInstructions_ref,Steps_ref) => new { pm_MfgInstructions_ref,Steps_ref }
                    )
                ).ToArray();
            }
            {
                var s=(
                    from pm in e.Production.ProductModel
                    where pm.Instructions!=null
                    from MfgInstructions_ref in pm.Instructions.XPathSelectElements("/ns:root/ns:Location",ns)
                    from Steps_ref in MfgInstructions_ref.XPathSelectElements("ns:step",ns)
                    select new {
                        pm.ProductModelID,
                        pm.Name,
                        Instructions = InnerString(pm.Instructions,"(/ns:root/text())[1]",ns),
                        LocationID = AttributeInt32(MfgInstructions_ref,"LocationID"),
                        SetupHours = AttributeDecimal(MfgInstructions_ref,"SetupHours"),
                        MachineHours = AttributeDecimal(MfgInstructions_ref,"MachineHours"),
                        LaborHours = AttributeDecimal(MfgInstructions_ref,"LaborHours"),
                        LotSize = AttributeInt32(MfgInstructions_ref,"LotSize"),
                        Steps = InnerString(Steps_ref,"string(.)",ns),
                        pm.rowguid,
                        pm.ModifiedDate,
                    }
                ).ToArray();
            }
            比較(
                () =>
                    from pm in e.Production.ProductModel
                    where pm.Instructions!=null
                    from MfgInstructions_ref in pm.Instructions.XPathSelectElements("/ns:root/ns:Location",ns)
                    from Steps_ref in MfgInstructions_ref.XPathSelectElements("ns:step",ns)
                    select new {
                        pm.ProductModelID,
                        pm.Name,
                        Instructions = InnerString(pm.Instructions,"(/ns:root/text())[1]",ns),
                        LocationID = AttributeInt32(MfgInstructions_ref,"LocationID"),
                        SetupHours = AttributeDecimal(MfgInstructions_ref,"SetupHours"),
                        MachineHours = AttributeDecimal(MfgInstructions_ref,"MachineHours"),
                        LaborHours = AttributeDecimal(MfgInstructions_ref,"LaborHours"),
                        LotSize = AttributeInt32(MfgInstructions_ref,"LotSize"),
                        Steps = InnerString(Steps_ref,"string(.)",ns),
                        pm.rowguid,
                        pm.ModifiedDate,
                    },
                Command,
                "SELECT * FROM Production.vProductModelInstructions",
                Reader => new {
                    ProductModelID = Reader.GetInt32(0),
                    Name = Reader.GetString(1),
                    Instructions = GetString(Reader,2),
                    LocationID = GetInt32(Reader,3),
                    SetupHours = GetDecimal(Reader,4),
                    MachineHours = GetDecimal(Reader,5),
                    LaborHours = GetDecimal(Reader,6),
                    LotSize = GetInt32(Reader,7),
                    Steps = GetString(Reader,8),
                    rowguid = Reader.GetGuid(9),
                    ModifiedDate = Reader.GetDateTime(10),
                });
        }
        private static void vVendorWithAddresses(Container e,SqlCommand Command) {
            比較(
                () =>
                    from v in e.Purchasing.Vendor
                    join bea in e.Person.BusinessEntityAddress on v.BusinessEntityID equals bea.BusinessEntityID
                    join a in e.Person.Address on bea.AddressID equals a.AddressID
                    join sp in e.Person.StateProvince on a.StateProvinceID equals sp.StateProvinceID
                    join cr in e.Person.CountryRegion on sp.CountryRegionCode equals cr.CountryRegionCode
                    join at in e.Person.AddressType on bea.AddressTypeID equals at.AddressTypeID
                    select new {
                        v.BusinessEntityID,
                        v.Name,
                        AddressType=at.Name,
                        a.AddressLine1,
                        a.AddressLine2,
                        a.City,
                        StateProvinceName=sp.Name,
                        a.PostalCode,
                        CountryRegionName=cr.Name,
                    },
                Command,
                "SELECT * FROM Purchasing.vVendorWithAddresses",
                Reader => new {
                    BusinessEntityID = Reader.GetInt32(0),
                    Name = Reader.GetString(1),
                    AddressType = Reader.GetString(2),
                    AddressLine1 = Reader.GetString(3),
                    AddressLine2 = GetString(Reader,4),
                    City = Reader.GetString(5),
                    StateProvinceName = Reader.GetString(6),
                    PostalCode = Reader.GetString(7),
                    CountryRegionName = Reader.GetString(8),
                });
        }
        private static void vVendorWithContacts(Container e,SqlCommand Command) {
            {
                var s=(
                    from v in e.Purchasing.Vendor
                    join bec in e.Person.BusinessEntityContact on v.BusinessEntityID equals bec.BusinessEntityID
                    join ct in e.Person.ContactType on bec.ContactTypeID equals ct.ContactTypeID
                    join p in e.Person.Person on bec.PersonID equals p.BusinessEntityID
                    join ea in e.Person.EmailAddress on p.BusinessEntityID equals ea.BusinessEntityID into ea0
                    from ea in ea0
                    join pp in e.Person.PersonPhone on p.BusinessEntityID equals pp.BusinessEntityID into pp0
                    from pp in pp0
                    join pnt in e.Person.PhoneNumberType on pp.PhoneNumberTypeID equals pnt.PhoneNumberTypeID into pnt0
                    from pnt in pnt0
                    select new {
                        v.BusinessEntityID,
                        v.Name,
                        ContactType = ct.Name,
                        p.Title,
                        p.FirstName,
                        p.MiddleName,
                        p.LastName,
                        p.Suffix,
                        pp.PhoneNumber,
                        PhoneNumberType = pnt.Name,
                        ea.EmailAddress,
                        p.EmailPromotion,
                    }
                ).ToArray();
            }
            比較(
                () =>
                    from v in e.Purchasing.Vendor
                    join bec in e.Person.BusinessEntityContact on v.BusinessEntityID equals bec.BusinessEntityID
                    join ct in e.Person.ContactType on bec.ContactTypeID equals ct.ContactTypeID
                    join p in e.Person.Person on bec.PersonID equals p.BusinessEntityID
                    join ea in e.Person.EmailAddress on p.BusinessEntityID equals ea.BusinessEntityID into ea0
                    from ea in ea0
                    join pp in e.Person.PersonPhone on p.BusinessEntityID equals pp.BusinessEntityID into pp0
                    from pp in pp0
                    join pnt in e.Person.PhoneNumberType on pp.PhoneNumberTypeID equals pnt.PhoneNumberTypeID into pnt0
                    from pnt in pnt0
                    select new {
                        v.BusinessEntityID,
                        v.Name,
                        ContactType = ct.Name,
                        p.Title,
                        p.FirstName,
                        p.MiddleName,
                        p.LastName,
                        p.Suffix,
                        pp.PhoneNumber,
                        PhoneNumberType=pnt.Name,
                        ea.EmailAddress,
                        p.EmailPromotion,
                    },
                Command,
                "SELECT * FROM Purchasing.vVendorWithContacts",
                Reader => new {
                    BusinessEntityID = Reader.GetInt32(0),
                    Name = Reader.GetString(1),
                    ContactType = Reader.GetString(2),
                    Title= GetString(Reader,3),
                    FirstName = Reader.GetString(4),
                    MiddleName= GetString(Reader,5),
                    LastName = Reader.GetString(6),
                    Suffix = GetString(Reader,7),
                    PhoneNumber = GetString(Reader,8),
                    PhoneNumberType = GetString(Reader,9),
                    EmailAddress = GetString(Reader,10),
                    EmailPromotion = Reader.GetInt32(11),
                });
        }
        private static void vIndividualCustomer(Container e,SqlCommand Command) {
            {
                var s = (
                    from p in e.Person.Person
                    join bea in e.Person.BusinessEntityAddress on p.BusinessEntityID equals bea.BusinessEntityID
                    join a in e.Person.Address on bea.AddressID equals a.AddressID
                    join sp in e.Person.StateProvince on a.StateProvinceID equals sp.StateProvinceID
                    join cr in e.Person.CountryRegion on sp.CountryRegionCode equals cr.CountryRegionCode
                    join at in e.Person.AddressType on bea.AddressTypeID equals at.AddressTypeID
                    join c in e.Sales.Customer on p.BusinessEntityID equals c.PersonID
                    join ea in e.Person.EmailAddress on p.BusinessEntityID equals ea.BusinessEntityID
                    join pp in e.Person.PersonPhone on p.BusinessEntityID equals pp.BusinessEntityID
                    join pnt in e.Person.PhoneNumberType on pp.PhoneNumberTypeID equals pnt.PhoneNumberTypeID
                    where c.StoreID==null
                    select new {
                        p.BusinessEntityID,
                        p.Title,
                        p.FirstName,
                        p.MiddleName,
                        p.LastName,
                        p.Suffix,
                        pp.PhoneNumber,
                        PhoneNumberType = pnt.Name,
                        ea.EmailAddress,
                        p.EmailPromotion,
                        AddressType = at.Name,
                        a.AddressLine1,
                        a.AddressLine2,
                        a.City,
                        StateProvinceName = sp.Name,
                        a.PostalCode,
                        CountryRegionName = cr.Name,
                        p.Demographics,
                    }
                ).ToArray();
            }
            比較(
                () =>
                    from p in e.Person.Person
                    join bea in e.Person.BusinessEntityAddress on p.BusinessEntityID equals bea.BusinessEntityID
                    join a in e.Person.Address on bea.AddressID equals a.AddressID
                    join sp in e.Person.StateProvince on a.StateProvinceID equals sp.StateProvinceID
                    join cr in e.Person.CountryRegion on sp.CountryRegionCode equals cr.CountryRegionCode
                    join at in e.Person.AddressType on bea.AddressTypeID equals at.AddressTypeID
                    join c in e.Sales.Customer on p.BusinessEntityID equals c.PersonID
                    join ea in e.Person.EmailAddress on p.BusinessEntityID equals ea.BusinessEntityID
                    join pp in e.Person.PersonPhone on p.BusinessEntityID equals pp.BusinessEntityID
                    join pnt in e.Person.PhoneNumberType on pp.PhoneNumberTypeID equals pnt.PhoneNumberTypeID
                    where c.StoreID==null
                    select new {
                        p.BusinessEntityID,
                        p.Title,
                        p.FirstName,
                        p.MiddleName,
                        p.LastName,
                        p.Suffix,
                        pp.PhoneNumber,
                        PhoneNumberType = pnt.Name,
                        ea.EmailAddress,
                        p.EmailPromotion,
                        AddressType = at.Name,
                        a.AddressLine1,
                        a.AddressLine2,
                        a.City,
                        StateProvinceName = sp.Name,
                        a.PostalCode,
                        CountryRegionName = cr.Name,
                        p.Demographics,
                    },
                Command,
                "SELECT * FROM Sales.vIndividualCustomer",
                Reader => new {
                    BusinessEntityID = Reader.GetInt32(0),
                    Title = GetString(Reader,1),
                    FirstName = Reader.GetString(2),
                    MiddleName= GetString(Reader,3),
                    LastName = Reader.GetString(4),
                    Suffix = GetString(Reader,5),
                    PhoneNumber = GetString(Reader,6),
                    PhoneNumberType = GetString(Reader,7),
                    EmailAddress = GetString(Reader,8),
                    EmailPromotion = Reader.GetInt32(9),
                    AddressType= Reader.GetString(10),
                    AddressLine1 = Reader.GetString(11),
                    AddressLine2 = GetString(Reader,12),
                    City = Reader.GetString(13),
                    StateProvinceName= Reader.GetString(14),
                    PostalCode = Reader.GetString(15),
                    CountryRegionName = Reader.GetString(16),
                    Demographics = GetXDocument(Reader,17),
                    //Demographics = Reader.GetSqlXml(17).IsNull ? null : XDocument.Parse(Reader.GetSqlXml(17).Value),
                });
        }
        private static void vPersonDemographics(Container C,SqlCommand Command) {
            var ns = new XmlNamespaceManager(new NameTable());
            ns.AddNamespace("ns","http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/IndividualSurvey");
            {
                var s=(
                    C.Person.Person.SelectMany(
                        p=> p.Demographics.XPathSelectElements("/ns:IndividualSurvey",ns),
                        (p,IndividualSurvey_ref) => new { p,IndividualSurvey_ref }
                    ).Select(
                        oi => {
                            if(oi.p.BusinessEntityID==1003) {
                                var g = new NumberFormatInfo();
                                var gg=g.NumberDecimalDigits;
                            }
                            return new {
                                oi.p.BusinessEntityID,
                                TotalPurchaseYTD = TagDecimal(oi.IndividualSurvey_ref,"ns:TotalPurchaseYTD",ns)
                            };
                        }
                    )
                ).ToArray();
            }
            比較(
                () =>
                    from p in C.Person.Person
                    from IndividualSurvey_ref in p.Demographics.XPathSelectElements("/ns:IndividualSurvey",ns)
                    select new {
                        p.BusinessEntityID,
                        TotalPurchaseYTD= TagDecimal(IndividualSurvey_ref,"ns:TotalPurchaseYTD",ns),
                        DateFirstPurchase = TagDateTime(IndividualSurvey_ref,"ns:DateFirstPurchase",ns),
                        BirthDate = TagDateTime(IndividualSurvey_ref,"ns:BirthDate",ns),
                        MaritalStatus = TagString(IndividualSurvey_ref,"ns:MaritalStatus",ns,1),
                        YearlyIncome= TagString(IndividualSurvey_ref,"ns:YearlyIncome",ns,30),
                        Gender= TagString(IndividualSurvey_ref,"ns:Gender",ns,1),
                        TotalChildren= TagInt32(IndividualSurvey_ref,"ns:TotalChildren",ns),
                        NumberChildrenAtHome= TagInt32(IndividualSurvey_ref,"ns:NumberChildrenAtHome",ns),
                        Education= TagString(IndividualSurvey_ref,"ns:Education",ns,30),
                        Occupation= TagString(IndividualSurvey_ref,"ns:Occupation",ns,30),
                        HomeOwnerFlag = TagBoolean(IndividualSurvey_ref,"ns:HomeOwnerFlag",ns),
                        NumberCarsOwned = TagInt32(IndividualSurvey_ref,"ns:NumberCarsOwned",ns),
                    },
                Command,
                "SELECT * FROM Sales.vPersonDemographics",
                Reader => new {
                    BusinessEntityID = Reader.GetInt32(0),
                    TotalPurchaseYTD= GetDecimal(Reader,1),
                    DateFirstPurchase = GetDateTime(Reader,2),
                    BirthDate= GetDateTime(Reader,3),
                    MaritalStatus= GetString(Reader,4),
                    YearlyIncome= GetString(Reader,5),
                    Gender= GetString(Reader,6),
                    TotalChildren= GetInt32(Reader,7),
                    NumberChildrenAtHome= GetInt32(Reader,8),
                    Education= GetString(Reader,9),
                    Occupation= GetString(Reader,10),
                    HomeOwnerFlag= GetBoolean(Reader,11),
                    NumberCarsOwned= GetInt32(Reader,12),
                });
        }
        private static void vSalesPerson(Container C,SqlCommand Command) {
            //{
            //    Expression<Func<IOutImmutableSet<Int32>>> LINQ = () =>
            //        from s in C.Sales.SalesPerson
            //        select s.BusinessEntityID;
            //    var LINQ結果 = Optimizer.Execute(LINQ,"LINQ");
            //}
            {
                var SalesPerson = new Set<int>();
                Expression<Func<object>> LINQ = () =>
                     from a in SalesPerson
                     join b in SalesPerson on a equals b
                     select new { a,b };
                var LINQ結果 = o.Execute(LINQ);
            }
            {
                var SalesPerson = new Set<int>();
                Expression<Func<object>> LINQ = () =>
                     from a in SalesPerson
                     join b in SalesPerson on a equals b
                     join c in SalesPerson on b equals c
                     select new { a,b,c };
                var LINQ結果 = o.Execute(LINQ);
            }
            {
                var SalesPerson = new Set<int>();
                Expression<Func<object>> LINQ = () =>
                     from a in SalesPerson
                     join b in SalesPerson on a equals b
                     join c in SalesPerson on b equals c
                     join d in SalesPerson on c equals d
                     select new { a,b,c,d };
                var LINQ結果 = o.Execute(LINQ);
            }
            {
                var SalesPerson = new Set<int>();
                Expression<Func<object>> LINQ = () =>
                     from a in SalesPerson
                     join b in SalesPerson on a equals b
                     join c in SalesPerson on b equals c
                     join d in SalesPerson on c equals d
                     select new { a,b,c,d,SalesPerson };
                var LINQ結果 = o.Execute(LINQ);
            }
            {
                var SalesPerson = new Set<int>();
                var Employee = new Set<int>();
                Expression<Func<IEnumerable<int>>> LINQ = () =>
                     from s in SalesPerson
                     join e in Employee on s equals e
                     select s+e;
                var LINQ結果 = o.Execute(LINQ);
            }
            {
                Expression<Func<IEnumerable<Guid>>> LINQ = () =>
                     from s in C.Sales.SalesPerson
                     join e in C.HumanResources.Employee on s.BusinessEntityID equals e.BusinessEntityID
                     select s.rowguid;
                var LINQ結果 = o.Execute(LINQ);
            }
            {
                Expression<Func<IEnumerable<Guid>>> LINQ = () =>
                     from s in C.Sales.SalesPerson
                     join e in C.HumanResources.Employee on s.BusinessEntityID equals e.BusinessEntityID
                     join p in C.Person.Person on s.BusinessEntityID equals p.BusinessEntityID
                     select s.rowguid;
                var LINQ結果 = o.Execute(LINQ);
            }
            {
                //14,17検討中
                Expression<Func<object>> LINQ = () =>
                     from s in C.Sales.SalesPerson
                     where s.BusinessEntityID==274
                     join st in C.Sales.SalesTerritory on s.TerritoryID equals st.TerritoryID into st0
                     from st in st0.DefaultIfEmpty()
                     select new { s,st };
                var LINQ結果 = o.Execute(LINQ);
                var 直接結果 = (
                     from s in C.Sales.SalesPerson
                     where s.BusinessEntityID==274
                     join st in C.Sales.SalesTerritory on s.TerritoryID equals st.TerritoryID into st0
                     from st in st0.DefaultIfEmpty()
                     select new { s,st }
                ).ToArray();
            }
            {
                //14,17検討中
                Expression<Func<IEnumerable<int>>> LINQ = () =>
                     from s in C.Sales.SalesPerson
                     where s.BusinessEntityID==274||s.BusinessEntityID==275||s.BusinessEntityID==277
                     join st in C.Sales.SalesTerritory on s.TerritoryID equals st.TerritoryID into st0
                     from st in st0.DefaultIfEmpty()
                     select s.BusinessEntityID;
                var LINQ結果 = o.Execute(LINQ);
                var 直接結果 = (
                     from s in C.Sales.SalesPerson
                     where s.BusinessEntityID==274||s.BusinessEntityID==275||s.BusinessEntityID==277
                     join st in C.Sales.SalesTerritory on s.TerritoryID equals st.TerritoryID into st0
                     from st in st0.DefaultIfEmpty()
                     select s.BusinessEntityID
                ).ToArray();
            }
            {
                //14,17検討中
                Expression<Func<IEnumerable<int>>> LINQ = () =>
                     from s in C.Sales.SalesPerson
                     join st in C.Sales.SalesTerritory on s.TerritoryID equals st.TerritoryID into st0
                     from st in st0.DefaultIfEmpty()
                     select s.BusinessEntityID;
                var LINQ結果 = o.Execute(LINQ);
                var 直接結果 = (
                     from s in C.Sales.SalesPerson
                     join st in C.Sales.SalesTerritory on s.TerritoryID equals st.TerritoryID into st0
                     from st in st0.DefaultIfEmpty()
                     select s.BusinessEntityID
                ).ToArray();
            }
            {
                //17,17
                Expression<Func<IEnumerable<int>>> LINQ = () =>
                     from s in C.Sales.SalesPerson
                     join e in C.HumanResources.Employee on s.BusinessEntityID equals e.BusinessEntityID
                     join p in C.Person.Person on s.BusinessEntityID equals p.BusinessEntityID
                     join bea in C.Person.BusinessEntityAddress on s.BusinessEntityID equals bea.BusinessEntityID
                     join a in C.Person.Address on bea.AddressID equals a.AddressID
                     join sp in C.Person.StateProvince on a.StateProvinceID equals sp.StateProvinceID
                     join cr in C.Person.CountryRegion on sp.CountryRegionCode equals cr.CountryRegionCode
                     select s.BusinessEntityID;
                var LINQ結果 = o.Execute(LINQ);
                var 直接結果 = (
                     from s in C.Sales.SalesPerson
                     join e in C.HumanResources.Employee on s.BusinessEntityID equals e.BusinessEntityID
                     join p in C.Person.Person on s.BusinessEntityID equals p.BusinessEntityID
                     join bea in C.Person.BusinessEntityAddress on s.BusinessEntityID equals bea.BusinessEntityID
                     join a in C.Person.Address on bea.AddressID equals a.AddressID
                     join sp in C.Person.StateProvince on a.StateProvinceID equals sp.StateProvinceID
                     join cr in C.Person.CountryRegion on sp.CountryRegionCode equals cr.CountryRegionCode
                     join st in C.Sales.SalesTerritory on s.TerritoryID equals st.TerritoryID into st0
                     select s.BusinessEntityID
                ).ToArray();
            }
            {
                //14,17
                Expression<Func<IEnumerable<int>>> LINQ = () =>
                     from s in C.Sales.SalesPerson
                     join e in C.HumanResources.Employee on s.BusinessEntityID equals e.BusinessEntityID
                     join p in C.Person.Person on s.BusinessEntityID equals p.BusinessEntityID
                     join bea in C.Person.BusinessEntityAddress on s.BusinessEntityID equals bea.BusinessEntityID
                     join a in C.Person.Address on bea.AddressID equals a.AddressID
                     join sp in C.Person.StateProvince on a.StateProvinceID equals sp.StateProvinceID
                     join cr in C.Person.CountryRegion on sp.CountryRegionCode equals cr.CountryRegionCode
                     join st in C.Sales.SalesTerritory on s.TerritoryID equals st.TerritoryID into st0
                     from st in st0.DefaultIfEmpty()
                     select s.BusinessEntityID;
                var LINQ結果 = o.Execute(LINQ);
                var 直接結果 = (
                     from s in C.Sales.SalesPerson
                     join e in C.HumanResources.Employee on s.BusinessEntityID equals e.BusinessEntityID
                     join p in C.Person.Person on s.BusinessEntityID equals p.BusinessEntityID
                     join bea in C.Person.BusinessEntityAddress on s.BusinessEntityID equals bea.BusinessEntityID
                     join a in C.Person.Address on bea.AddressID equals a.AddressID
                     join sp in C.Person.StateProvince on a.StateProvinceID equals sp.StateProvinceID
                     join cr in C.Person.CountryRegion on sp.CountryRegionCode equals cr.CountryRegionCode
                     join st in C.Sales.SalesTerritory on s.TerritoryID equals st.TerritoryID into st0
                     from st in st0.DefaultIfEmpty()
                     select s.BusinessEntityID
                ).ToArray();
            }
            {
                //17行入るべき
                //BusinnessEntityID274,285,287入るべき
                Expression<Func<IEnumerable<int>>> LINQ = () =>
                     from s in C.Sales.SalesPerson
                     join e in C.HumanResources.Employee on s.BusinessEntityID equals e.BusinessEntityID
                     join p in C.Person.Person on s.BusinessEntityID equals p.BusinessEntityID
                     join bea in C.Person.BusinessEntityAddress on s.BusinessEntityID equals bea.BusinessEntityID
                     join a in C.Person.Address on bea.AddressID equals a.AddressID
                     join sp in C.Person.StateProvince on a.StateProvinceID equals sp.StateProvinceID
                     join cr in C.Person.CountryRegion on sp.CountryRegionCode equals cr.CountryRegionCode
                     join st in C.Sales.SalesTerritory on s.TerritoryID equals st.TerritoryID into st0
                     from st in st0.DefaultIfEmpty()
                     join ea in C.Person.EmailAddress on p.BusinessEntityID equals ea.BusinessEntityID into ea0
                     from ea in ea0.DefaultIfEmpty()
                     join pp in C.Person.PersonPhone on p.BusinessEntityID equals pp.BusinessEntityID into pp0
                     from pp in pp0.DefaultIfEmpty()
                     join pnt in C.Person.PhoneNumberType on pp.PhoneNumberTypeID equals pnt.PhoneNumberTypeID into pnt0
                     from pnt in pnt0.DefaultIfEmpty()
                     select s.BusinessEntityID;
                var LINQ結果 = o.Execute(LINQ);
                var 直接結果=(
                     from s in C.Sales.SalesPerson
                     join e in C.HumanResources.Employee on s.BusinessEntityID equals e.BusinessEntityID
                     join p in C.Person.Person on s.BusinessEntityID equals p.BusinessEntityID
                     join bea in C.Person.BusinessEntityAddress on s.BusinessEntityID equals bea.BusinessEntityID
                     join a in C.Person.Address on bea.AddressID equals a.AddressID
                     join sp in C.Person.StateProvince on a.StateProvinceID equals sp.StateProvinceID
                     join cr in C.Person.CountryRegion on sp.CountryRegionCode equals cr.CountryRegionCode
                     join st in C.Sales.SalesTerritory on s.TerritoryID equals st.TerritoryID into st0
                     from st in st0.DefaultIfEmpty()
                     join ea in C.Person.EmailAddress on p.BusinessEntityID equals ea.BusinessEntityID into ea0
                     from ea in ea0.DefaultIfEmpty()
                     join pp in C.Person.PersonPhone on p.BusinessEntityID equals pp.BusinessEntityID into pp0
                     from pp in pp0.DefaultIfEmpty()
                     join pnt in C.Person.PhoneNumberType on pp.PhoneNumberTypeID equals pnt.PhoneNumberTypeID into pnt0
                     from pnt in pnt0.DefaultIfEmpty()
                     select s.BusinessEntityID
                ).ToArray();
            }
            var A = (
                from s in C.Sales.SalesPerson
                join e in C.HumanResources.Employee on s.BusinessEntityID equals e.BusinessEntityID
                join p in C.Person.Person on s.BusinessEntityID equals p.BusinessEntityID
                join bea in C.Person.BusinessEntityAddress on s.BusinessEntityID equals bea.BusinessEntityID
                join a in C.Person.Address on bea.AddressID equals a.AddressID
                join sp in C.Person.StateProvince on a.StateProvinceID equals sp.StateProvinceID
                join cr in C.Person.CountryRegion on sp.CountryRegionCode equals cr.CountryRegionCode
                join st in C.Sales.SalesTerritory on s.TerritoryID equals st.TerritoryID into st0
                from st in st0.DefaultIfEmpty()
                //join ea in C.Person.EmailAddress on p.BusinessEntityID equals ea.BusinessEntityID into ea0
                //from ea in ea0.DefaultIfEmpty()
                //join pp in C.Person.PersonPhone on p.BusinessEntityID equals pp.BusinessEntityID into pp0
                //from pp in pp0.DefaultIfEmpty()
                //join pnt in C.Person.PhoneNumberType on pp.PhoneNumberTypeID equals pnt.PhoneNumberTypeID into pnt0
                //from pnt in pnt0.DefaultIfEmpty()
                select e.rowguid
            ).ToArray();
            //Array.Sort(A,(a,b) =>a-b);
            var B = (
                from s in C.Sales.SalesPerson
                join e in C.HumanResources.Employee on s.BusinessEntityID equals e.BusinessEntityID
                join p in C.Person.Person on s.BusinessEntityID equals p.BusinessEntityID
                join bea in C.Person.BusinessEntityAddress on s.BusinessEntityID equals bea.BusinessEntityID
                join a in C.Person.Address on bea.AddressID equals a.AddressID
                join sp in C.Person.StateProvince on a.StateProvinceID equals sp.StateProvinceID
                join cr in C.Person.CountryRegion on sp.CountryRegionCode equals cr.CountryRegionCode
                join st in C.Sales.SalesTerritory on s.TerritoryID equals st.TerritoryID into st0
                from st in st0.DefaultIfEmpty()
                join ea in C.Person.EmailAddress on p.BusinessEntityID equals ea.BusinessEntityID into ea0
                from ea in ea0.DefaultIfEmpty()
                join pp in C.Person.PersonPhone on p.BusinessEntityID equals pp.BusinessEntityID into pp0
                from pp in pp0.DefaultIfEmpty()
                join pnt in C.Person.PhoneNumberType on pp.PhoneNumberTypeID equals pnt.PhoneNumberTypeID into pnt0
                from pnt in pnt0.DefaultIfEmpty()
                select new {
                    s.BusinessEntityID,
                    p.Title,
                    p.FirstName,
                    p.MiddleName,
                    p.LastName,
                    p.Suffix,
                    e.JobTitle,
                    PhoneNumber = pp==null ? null : pp.PhoneNumber,
                    PhoneNumberType = pnt==null ? null : pnt.Name,
                    EmailAddress = ea==null ? null : ea.EmailAddress,
                    p.EmailPromotion,
                    a.AddressLine1,
                    a.AddressLine2,
                    a.City,
                    StateProvinceName = sp.Name,
                    a.PostalCode,
                    CountryRegionName = cr.Name,
                    TerritoryName = st==null ? null : st.Name,
                    TerritoryGroup = st==null ? null : st.Group,
                    s.SalesQuota,
                    s.SalesYTD,
                    s.SalesLastYear,
                }
            ).ToArray();
            比較(
                () =>
                    from s in C.Sales.SalesPerson
                    join e in C.HumanResources.Employee on s.BusinessEntityID equals e.BusinessEntityID 
                    join p in C.Person.Person on s.BusinessEntityID equals p.BusinessEntityID
                    join bea in C.Person.BusinessEntityAddress on s.BusinessEntityID  equals bea.BusinessEntityID
                    join a in C.Person.Address on bea.AddressID equals a.AddressID
                    join sp in C.Person.StateProvince on a.StateProvinceID equals sp.StateProvinceID
                    join cr in C.Person.CountryRegion on sp.CountryRegionCode equals cr.CountryRegionCode
                    join st in C.Sales.SalesTerritory on s.TerritoryID equals st.TerritoryID into st0
                    from st in st0.DefaultIfEmpty()
                    join ea in C.Person.EmailAddress on p.BusinessEntityID equals ea.BusinessEntityID into ea0
                    from ea in ea0.DefaultIfEmpty()
                    join pp in C.Person.PersonPhone on p.BusinessEntityID equals pp.BusinessEntityID into pp0
                    from pp in pp0.DefaultIfEmpty()
                    join pnt in C.Person.PhoneNumberType on pp.PhoneNumberTypeID equals pnt.PhoneNumberTypeID into pnt0
                    from pnt in pnt0.DefaultIfEmpty()
                    select new {
                        s.BusinessEntityID,
                        p.Title,
                        p.FirstName,
                        p.MiddleName,
                        p.LastName,
                        p.Suffix,
                        e.JobTitle,
                        PhoneNumber=pp==null?null:pp.PhoneNumber,
                        PhoneNumberType = pnt==null?null:pnt.Name,
                        EmailAddress=ea==null?null:ea.EmailAddress,
                        p.EmailPromotion,
                        a.AddressLine1,
                        a.AddressLine2,
                        a.City,
                        StateProvinceName = sp.Name,
                        a.PostalCode,
                        CountryRegionName = cr.Name,
                        TerritoryName = st==null?null:st.Name,
                        TerritoryGroup = st==null?null:st.Group,
                        s.SalesQuota,
                        s.SalesYTD,
                        s.SalesLastYear,
                    },
                Command,
                "SELECT * FROM Sales.vSalesPerson",
                Reader => new {
                    BusinessEntityID = Reader.GetInt32(0),
                    Title = GetString(Reader,1),
                    FirstName = Reader.GetString(2),
                    MiddleName = GetString(Reader,3),
                    LastName = Reader.GetString(4),
                    Suffix = GetString(Reader,5),
                    JobTitle = Reader.GetString(6),
                    PhoneNumber = GetString(Reader,7),
                    PhoneNumberType = GetString(Reader,8),
                    EmailAddress = GetString(Reader,9),
                    EmailPromotion = Reader.GetInt32(10),
                    AddressLine1 = Reader.GetString(11),
                    AddressLine2 = GetString(Reader,12),
                    City = Reader.GetString(13),
                    StateProvinceName = Reader.GetString(14),
                    PostalCode = Reader.GetString(15),
                    CountryRegionName = Reader.GetString(16),
                    TerritoryName = GetString(Reader,17),
                    TerritoryGroup = GetString(Reader,18),
                    SalesQuota = GetDecimal(Reader,19),
                    SalesYTD = Reader.GetDecimal(20),
                    SalesLastYear = Reader.GetDecimal(21),
                });
        }

        [SuppressMessage("Globalization","CA1305:IFormatProvider を指定します",Justification = "<保留中>")]
        private static void vSalesPersonSalesByFiscalYears(Container C,SqlCommand Command) {
            //SELECT
            //    soh.SalesPersonID,
            //    FullName=p.FirstName+' '+COALESCE(p.MiddleName,'')+' '+p.LastName,
            //    e.JobTitle,
            //    SalesTerritory=st.Name,
            //    soh.SubTotal,
            //    YEAR(DATEADD(m, 6, soh.OrderDate))FiscalYear 
            //    FROM Sales.SalesPerson sp 
            //    JOIN Sales.SalesOrderHeader  soh ON sp.BusinessEntityID=soh.SalesPersonID
            //    JOIN Sales.SalesTerritory    st  ON sp.TerritoryID     =st.TerritoryID 
            //    JOIN HumanResources.Employee e   ON soh.SalesPersonID  =e.BusinessEntityID 
            //    JOIN Person.Person           p   ON p.BusinessEntityID =sp.BusinessEntityID
            //SELECT
            //    pvt.SalesPersonID
            //    ,pvt.FullName
            //    ,pvt.JobTitle
            //    ,pvt.SalesTerritory
            //    ,pvt.[2002],pvt.[2003],pvt.[2004]
            //FROM(
            //    SELECT
            //        soh.SalesPersonID,
            //        FullName=p.FirstName+' '+COALESCE(p.MiddleName,'')+' '+p.LastName,
            //        e.JobTitle,
            //        SalesTerritory=st.Name,
            //        soh.SubTotal,
            //        YEAR(DATEADD(m, 6, soh.OrderDate))FiscalYear 
            //    FROM Sales.SalesPerson sp
            //    JOIN Sales.SalesOrderHeader  soh ON sp.BusinessEntityID=soh.SalesPersonID
            //    JOIN Sales.SalesTerritory    st  ON sp.TerritoryID     =st.TerritoryID 
            //    JOIN HumanResources.Employee e   ON soh.SalesPersonID  =e.BusinessEntityID 
            //    JOIN Person.Person           p   ON p.BusinessEntityID =sp.BusinessEntityID
            //)soh 
            //PIVOT(
            //    SUM(SubTotal) 
            //    FOR FiscalYear 
            //    IN([2002],[2003],[2004])
            //)pvt
            {
                Expression<Func<object>> LINQ = () =>
                    from sp  in C.Sales.SalesPerson
                    join soh in C.Sales.SalesOrderHeader  on sp.BusinessEntityID equals soh.SalesPersonID
                    join st  in C.Sales.SalesTerritory    on sp.TerritoryID      equals st.TerritoryID
                    join e   in C.HumanResources.Employee on soh.SalesPersonID   equals e.BusinessEntityID
                    join p   in C.Person.Person           on sp.BusinessEntityID equals p.BusinessEntityID
                    where soh.SalesPersonID==275
                    select new {
                        soh.SalesPersonID,
                        FullName = p.FirstName+' '+(p.MiddleName??"")+' '+p.LastName,
                        e.JobTitle,
                        SalesTerritory = st.Name,
                        soh.SubTotal,
                        FiscalYear = soh.OrderDate.AddMonths(6)
                    };
                var LINQ結果 = o.Execute(LINQ);
                var 直接結果 = (
                    from sp in C.Sales.SalesPerson
                    join soh in C.Sales.SalesOrderHeader on sp.BusinessEntityID equals soh.SalesPersonID
                    join st in C.Sales.SalesTerritory on sp.TerritoryID equals st.TerritoryID
                    join e in C.HumanResources.Employee on soh.SalesPersonID equals e.BusinessEntityID
                    join p in C.Person.Person on sp.BusinessEntityID equals p.BusinessEntityID
                    where soh.SalesPersonID==275
                    select new {
                        soh.SalesPersonID,
                        FullName = p.FirstName+' '+(p.MiddleName??"")+' '+p.LastName,
                        e.JobTitle,
                        SalesTerritory = st.Name,
                        soh.SubTotal,
                        FiscalYear = soh.OrderDate.AddMonths(6)
                    }
                ).ToArray();
            }
            比較(
                () =>
                    from sp in C.Sales.SalesPerson
                    join soh in C.Sales.SalesOrderHeader on sp.BusinessEntityID equals soh.SalesPersonID
                    join st in C.Sales.SalesTerritory on sp.TerritoryID equals st.TerritoryID
                    join e in C.HumanResources.Employee on soh.SalesPersonID equals e.BusinessEntityID
                    join p in C.Person.Person on sp.BusinessEntityID equals p.BusinessEntityID
                    select new {
                        soh.SalesPersonID,
                        FullName = p.FirstName+' '+(p.MiddleName??"")+' '+p.LastName,
                        e.JobTitle,
                        SalesTerritory = st.Name,
                        soh.SubTotal,
                        FiscalYear = soh.OrderDate.AddMonths(6).Year
                    },
                Command,
                "    SELECT DISTINCT\r\n"+
                "        soh.SalesPersonID,\r\n"+
                "        FullName=p.FirstName+' '+COALESCE(p.MiddleName,'')+' '+p.LastName,\r\n"+
                "        e.JobTitle,\r\n"+
                "        SalesTerritory=st.Name,\r\n"+
                "        soh.SubTotal,\r\n"+
                "        FiscalYear=YEAR(DATEADD(m,6,soh.OrderDate))\r\n"+
                "    FROM Sales.SalesPerson sp\r\n"+
                "    JOIN Sales.SalesOrderHeader  soh ON sp.BusinessEntityID=soh.SalesPersonID\r\n"+
                "    JOIN Sales.SalesTerritory    st  ON sp.TerritoryID     =st.TerritoryID\r\n"+
                "    JOIN HumanResources.Employee e   ON soh.SalesPersonID  =e.BusinessEntityID\r\n"+
                "    JOIN Person.Person           p   ON p.BusinessEntityID =sp.BusinessEntityID\r\n",
                Reader => new {
                    SalesPersonID = GetInt32(Reader,0),
                    FullName = Reader.GetString(1),
                    JobTitle = Reader.GetString(2),
                    SalesTerritory = Reader.GetString(3),
                    SubTotal = Reader.GetDecimal(4),
                    FiscalYear = Reader.GetInt32(5)
                }
            );
            //{
            //    Expression<Func<Object>> LINQ = () =>
            //        Pivot(
            //            from sp in C.Sales.SalesPerson
            //            join soh in C.Sales.SalesOrderHeader on sp.BusinessEntityID equals soh.SalesPersonID
            //            join st in C.Sales.SalesTerritory on sp.TerritoryID equals st.TerritoryID
            //            join e in C.HumanResources.Employee on soh.SalesPersonID equals e.BusinessEntityID
            //            join p in C.Person.Person on sp.BusinessEntityID equals p.BusinessEntityID
            //            select new {
            //                soh.SalesPersonID,
            //                FullName = p.FirstName+' '+(p.MiddleName??"")+' '+p.LastName,
            //                e.JobTitle,
            //                SalesTerritory = st.Name,
            //                soh.SubTotal,
            //                FiscalYear = soh.OrderDate.AddMonths(6).Year
            //            },
            //            p => $"Y{p.FiscalYear}",
            //            new[] { "2002","2003","2004","2011" }
            //            //p => new {
            //            //    Y2002 = p.Sum(q => q.SubTotal),
            //            //    Y2003 = p.Sum(q => q.SubTotal),
            //            //    Y2004 = p.Sum(q => q.SubTotal),
            //            //    Y2011 = p.Sum(q => q.SubTotal),
            //            //}
            //        ).Select(
            //            p => new {
            //                p.Key.SalesPersonID,
            //                p.Key.FullName,
            //                p.Key.JobTitle,
            //                p.Key.SalesTerritory,
            //                Total = p.Sum(q => q.SubTotal)
            //            }
            //        );
            //    var LINQ結果 = Optimizer.Execute(LINQ,"LINQ");
            //    var 直接結果 = (
            //        from sp in C.Sales.SalesPerson
            //        join soh in C.Sales.SalesOrderHeader on sp.BusinessEntityID equals soh.SalesPersonID
            //        join st in C.Sales.SalesTerritory on sp.TerritoryID equals st.TerritoryID
            //        join e in C.HumanResources.Employee on soh.SalesPersonID equals e.BusinessEntityID
            //        join p in C.Person.Person on sp.BusinessEntityID equals p.BusinessEntityID
            //        where soh.SalesPersonID==275
            //        select new {
            //            soh.SalesPersonID,
            //            FullName = p.FirstName+' '+(p.MiddleName??"")+' '+p.LastName,
            //            e.JobTitle,
            //            SalesTerritory = st.Name,
            //            soh.SubTotal,
            //            FiscalYear = soh.OrderDate.AddMonths(6)
            //        }
            //    ).ToArray();
            //}
            //比較(
            //    () =>
            //        Pivot(
            //            from sp in C.Sales.SalesPerson
            //            join soh in C.Sales.SalesOrderHeader on sp.BusinessEntityID equals soh.SalesPersonID
            //            join st in C.Sales.SalesTerritory on sp.TerritoryID equals st.TerritoryID
            //            join e in C.HumanResources.Employee on soh.SalesPersonID equals e.BusinessEntityID
            //            join p in C.Person.Person on sp.BusinessEntityID equals p.BusinessEntityID
            //            select new {
            //                soh.SalesPersonID,
            //                FullName = p.FirstName+' '+(p.MiddleName??"")+' '+p.LastName,
            //                e.JobTitle,
            //                SalesTerritory = st.Name,
            //                soh.SubTotal,
            //                FiscalYear = soh.OrderDate.AddMonths(6).Year
            //            },
            //            p => $"Y{p.FiscalYear}",
            //            p=>p,
            //            new[] { "2002","2003","2004","2011" }
            //            //p => new {
            //            //    Y2002 = p.Sum(q => q.SubTotal),
            //            //    Y2003 = p.Sum(q => q.SubTotal),
            //            //    Y2004 = p.Sum(q => q.SubTotal),
            //            //    Y2011 = p.Sum(q => q.SubTotal),
            //            //}
            //        ).Select(
            //            p => new {
            //                p.
            //                p.Key.SalesPersonID,
            //                p.Key.FullName,
            //                p.Key.JobTitle,
            //                p.Key.SalesTerritory,
            //                Total = p.Sum(q => q.SubTotal)
            //            }
            //        )
            //    ,
            //    Command,
            //    "    SELECT DISTINCT\r\n"+
            //    "        soh.SalesPersonID,\r\n"+
            //    "        FullName=p.FirstName+' '+COALESCE(p.MiddleName,'')+' '+p.LastName,\r\n"+
            //    "        e.JobTitle,\r\n"+
            //    "        SalesTerritory=st.Name,\r\n"+
            //    "        soh.SubTotal,\r\n"+
            //    "        FiscalYear=YEAR(DATEADD(m,6,soh.OrderDate))\r\n"+
            //    "    FROM Sales.SalesPerson sp\r\n"+
            //    "    JOIN Sales.SalesOrderHeader  soh ON sp.BusinessEntityID=soh.SalesPersonID\r\n"+
            //    "    JOIN Sales.SalesTerritory    st  ON sp.TerritoryID     =st.TerritoryID\r\n"+
            //    "    JOIN HumanResources.Employee e   ON soh.SalesPersonID  =e.BusinessEntityID\r\n"+
            //    "    JOIN Person.Person           p   ON p.BusinessEntityID =sp.BusinessEntityID\r\n",
            //    Reader => new {
            //        SalesPersonID = GetInt32(Reader,0),
            //        FullName = Reader.GetString(1),
            //        JobTitle = Reader.GetString(2),
            //        SalesTerritory = Reader.GetString(3),
            //        SubTotal = Reader.GetDecimal(4),
            //        FiscalYear = Reader.GetInt32(5)
            //    }
            //);
        }
        //interface IPivot<T> {
        //    T PivotColumns { get; }
        //}
        //class ResultPivot<T>{
        //    public T PivotColumns { get; set; }
        //    public ResultPivot(T PivotColumns) => this.PivotColumns=PivotColumns;
        //}
        //private static IEnumerable<IGrouping<String,T>> Pivot<T>(IEnumerable<T> source,Func<T,String> pivotedColumnSelector,String[] pivotedColumns) {
        //    return source.GroupBy(pivotedColumnSelector).Where(p => Enumerable.Contains(pivotedColumns,p.Key));
        //    //return source.GroupBy(pivotedColumnSelector).Where(p => pivotedColumns.Cotains(p.Key));
        //}
        /*
            select pvt.F
            from(
                select s.F
                from source s
            )f
            pivot(
                sum(f.number)
                for 横にしたい列
                in(横にしたい列に含まれる値1,横にしたい列に含まれる値2)
        */
        //[SuppressMessage("Performance","CA1820:文字列の長さを使用して空の文字列をテストします",Justification="<保留中>")]
        //private static IOutImmutableSet<TResult> Pivot<T, TKey,TResult>(
        //    IOutImmutableSet<T> source,
        //    Func<T,String> pivotedColumnSelector,
        //    Func<T,TResult> resultSelectorSelector
        //) {
        //    SetGroupingSet<TResult,T> g = source.GroupBy(keySelector);
        //    SetGrouping<TResult,T,GroupingSet<TResult,T>> g0 = g;
        //    Set<GroupingSet<TResult,T>> g1 = g;
        //    IEnumerable<GroupingSet<TResult,T>> g2 = g;
        //    IOutImmutableSet<GroupingSet<TResult,T>> g3 = g;
        //    IOutImmutableSet<IGrouping<TResult,T>> g4 = g2;

        //    //IGrouping<TKey,T> g = source.GroupBy(keySelector);
        //    return source.Where(
        //        p => Enumerable.Contains(pivotedColumns,pivotedColumnSelector(p))
        //    ).GroupBy(
        //        p => keySelector(p)
        //    );
        //    //return source.GroupBy(
        //    //    pivotedColumnSelector
        //    //).Where(
        //    //    p => Enumerable.Contains(pivotedColumns,p.Key)
        //    //);
        //    //return source.GroupBy(pivotedColumnSelector).Where(p => pivotedColumns.Cotains(p.Key));
        //}

        //[SuppressMessage("Performance","CA1820:文字列の長さを使用して空の文字列をテストします",Justification = "<保留中>")]
        //private static IOutImmutableSet<IGrouping<TKey,T>> Pivot<T,  TKey>(
        //    IOutImmutableSet<T> source,
        //    Func<T,String> pivotedColumnSelector,
        //    //Func<T,TKey> keySelector,
        //    String[] pivotedColumns
        //) {
        //    SetGroupingSet<TKey,T> g = source.GroupBy(keySelector);
        //    SetGrouping<TKey,T,GroupingSet<TKey,T>> g0 = g;
        //    Set<GroupingSet<TKey,T>> g1 = g;
        //    IEnumerable<GroupingSet<TKey,T>> g2 = g;
        //    IOutImmutableSet<GroupingSet<TKey,T>> g3 = g;
        //    IOutImmutableSet<IGrouping<TKey,T>> g4 = g2;

        //    //IGrouping<TKey,T> g = source.GroupBy(keySelector);
        //    return source.Where(
        //        p => Enumerable.Contains(pivotedColumns,pivotedColumnSelector(p))
        //    ).GroupBy(
        //        p => keySelector(p)
        //    );
        //    //return source.GroupBy(
        //    //    pivotedColumnSelector
        //    //).Where(
        //    //    p => Enumerable.Contains(pivotedColumns,p.Key)
        //    //);
        //    //return source.GroupBy(pivotedColumnSelector).Where(p => pivotedColumns.Cotains(p.Key));
        //}
        private static void vStoreWithAddresses(Container e,SqlCommand Command) {
            {
                Expression<Func<object>> LINQ = () =>
                    from s in e.Sales.Store
                    join bea in e.Person.BusinessEntityAddress on s.BusinessEntityID equals bea.BusinessEntityID
                    join a in e.Person.Address on bea.AddressID equals a.AddressID
                    join sp in e.Person.StateProvince on a.StateProvinceID equals sp.StateProvinceID
                    join cr in e.Person.CountryRegion on sp.CountryRegionCode equals cr.CountryRegionCode
                    join at in e.Person.AddressType on bea.AddressTypeID equals at.AddressTypeID
                    select new {
                        s.BusinessEntityID,
                        s.Name,
                        AddressType = at.Name,
                        a.AddressLine1,
                        a.AddressLine2,
                        a.City,
                        StateProvinceName = sp.Name,
                        a.PostalCode,
                        CountryRegionName = cr.Name
                    };
                var LINQ結果 = o.Execute(LINQ);
                var 直接結果 = (
                    from s in e.Sales.Store
                    join bea in e.Person.BusinessEntityAddress on s.BusinessEntityID equals bea.BusinessEntityID
                    join a in e.Person.Address on bea.AddressID equals a.AddressID
                    join sp in e.Person.StateProvince on a.StateProvinceID equals sp.StateProvinceID
                    join cr in e.Person.CountryRegion on sp.CountryRegionCode equals cr.CountryRegionCode
                    join at in e.Person.AddressType on bea.AddressTypeID equals at.AddressTypeID
                    select new {
                        s.BusinessEntityID,
                        s.Name,
                        AddressType = at.Name,
                        a.AddressLine1,
                        a.AddressLine2,
                        a.City,
                        StateProvinceName = sp.Name,
                        a.PostalCode,
                        CountryRegionName = cr.Name
                    }
                ).ToArray();
            }
            比較(
                () =>
                    from s in e.Sales.Store
                    join bea in e.Person.BusinessEntityAddress on s.BusinessEntityID equals bea.BusinessEntityID
                    join a   in e.Person.Address               on bea.AddressID equals a.AddressID
                    join sp  in e.Person.StateProvince         on a.StateProvinceID equals sp.StateProvinceID
                    join cr  in e.Person.CountryRegion         on sp.CountryRegionCode equals cr.CountryRegionCode
                    join at  in e.Person.AddressType           on bea.AddressTypeID equals at.AddressTypeID
                    select new {
                        s.BusinessEntityID,
                        s.Name,
                        AddressType = at.Name,
                        a.AddressLine1,
                        a.AddressLine2,
                        a.City,
                        StateProvinceName = sp.Name,
                        a.PostalCode,
                        CountryRegionName = cr.Name
                    },
                Command,
                "    SELECT * FROM Sales.vStoreWithAddresses",
                Reader => new {
                    BusinessEntityID = Reader.GetInt32(0),
                    Name = Reader.GetString(1),
                    AddressType= Reader.GetString(2),
                    AddressLine1= Reader.GetString(3),
                    AddressLine2= GetString(Reader,4),
                    City= Reader.GetString(5),
                    StateProvinceName= Reader.GetString(6),
                    PostalCode= Reader.GetString(7),
                    CountryRegionName= Reader.GetString(8),
                }
            );
        }
        private static void vStoreWithContacts(Container e,SqlCommand Command) {
            {
                Expression<Func<object>> LINQ = () =>
                    from s in e.Sales.Store
                    join bec in e.Person.BusinessEntityContact on s.BusinessEntityID equals bec.BusinessEntityID
                    join ct in e.Person.ContactType on bec.ContactTypeID equals ct.ContactTypeID
                    join p in e.Person.Person on bec.PersonID equals p.BusinessEntityID
                    join ea in e.Person.EmailAddress on p.BusinessEntityID equals ea.BusinessEntityID  into ea0
                    from ea in ea0.DefaultIfEmpty()
                    join pp in e.Person.PersonPhone on p.BusinessEntityID equals pp.BusinessEntityID into pp0
                    from pp in pp0.DefaultIfEmpty()
                    join pnt in e.Person.PhoneNumberType on pp.PhoneNumberTypeID equals pnt.PhoneNumberTypeID into pnt0
                    from pnt in pnt0.DefaultIfEmpty()
                    select new {
                        s.BusinessEntityID ,
                        s.Name ,
                        ContactType=ct.Name,
                        p.Title ,
                        p.FirstName ,
                        p.MiddleName ,
                        p.LastName, 
                        p.Suffix ,
                        pp.PhoneNumber ,
	                    PhoneNumberType=pnt.Name,
                        ea.EmailAddress,
                        p.EmailPromotion
                    };
                var LINQ結果 = o.Execute(LINQ);
                var 直接結果 = (
                    from s in e.Sales.Store
                    join bec in e.Person.BusinessEntityContact on s.BusinessEntityID equals bec.BusinessEntityID
                    join ct in e.Person.ContactType on bec.ContactTypeID equals ct.ContactTypeID
                    join p in e.Person.Person on bec.PersonID equals p.BusinessEntityID
                    join ea in e.Person.EmailAddress on p.BusinessEntityID equals ea.BusinessEntityID into ea0
                    from ea in ea0.DefaultIfEmpty()
                    join pp in e.Person.PersonPhone on p.BusinessEntityID equals pp.BusinessEntityID into pp0
                    from pp in pp0.DefaultIfEmpty()
                    join pnt in e.Person.PhoneNumberType on pp.PhoneNumberTypeID equals pnt.PhoneNumberTypeID into pnt0
                    from pnt in pnt0.DefaultIfEmpty()
                    select new {
                        s.BusinessEntityID,
                        s.Name,
                        ContactType = ct.Name,
                        p.Title,
                        p.FirstName,
                        p.MiddleName,
                        p.LastName,
                        p.Suffix,
                        pp.PhoneNumber,
                        PhoneNumberType = pnt.Name,
                        ea.EmailAddress,
                        p.EmailPromotion
                    }
                ).ToArray();
            }
            比較(
                () =>
                    from s in e.Sales.Store
                    join bec in e.Person.BusinessEntityContact on s.BusinessEntityID equals bec.BusinessEntityID
                    join ct in e.Person.ContactType on bec.ContactTypeID equals ct.ContactTypeID
                    join p in e.Person.Person on bec.PersonID equals p.BusinessEntityID
                    join ea in e.Person.EmailAddress on p.BusinessEntityID equals ea.BusinessEntityID into ea0
                    from ea in ea0.DefaultIfEmpty()
                    join pp in e.Person.PersonPhone on p.BusinessEntityID equals pp.BusinessEntityID into pp0
                    from pp in pp0.DefaultIfEmpty()
                    join pnt in e.Person.PhoneNumberType on pp.PhoneNumberTypeID equals pnt.PhoneNumberTypeID into pnt0
                    from pnt in pnt0.DefaultIfEmpty()
                    select new {
                        s.BusinessEntityID,
                        s.Name,
                        ContactType = ct.Name,
                        p.Title,
                        p.FirstName,
                        p.MiddleName,
                        p.LastName,
                        p.Suffix,
                        pp.PhoneNumber,
                        PhoneNumberType = pnt.Name,
                        ea.EmailAddress,
                        p.EmailPromotion
                    },
                Command,
                "    SELECT * FROM Sales.vStoreWithContacts",
                Reader => new {
                    BusinessEntityID = Reader.GetInt32(0),
                    Name = Reader.GetString(1),
                    ContactType= Reader.GetString(2),
                    Title= GetString(Reader,3),
                    FirstName = Reader.GetString(4),
                    MiddleName= GetString(Reader,5),
                    LastName= Reader.GetString(6),
                    Suffix= GetString(Reader,7),
                    PhoneNumber = GetString(Reader,8),
                    PhoneNumberType = GetString(Reader,9),
                    EmailAddress= GetString(Reader,10),
                    EmailPromotion= Reader.GetInt32(11),
                }
            );
        }
        private static void vStoreWithDemographics(Container e,SqlCommand Command) {
            var ns = new XmlNamespaceManager(new NameTable());
            ns.AddNamespace("ns","http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/StoreSurvey");
            {
                Expression<Func<object>> LINQ = () =>
                    from s in e.Sales.Store
                    select new {
                        s.BusinessEntityID,
                        s.Name,
                        AnnualSales = TagDecimal(s.Demographics,"(/ns:StoreSurvey/ns:AnnualSales)[1]",ns),
                        AnnualRevenue = TagDecimal(s.Demographics,"(/ns:StoreSurvey/ns:AnnualRevenue)[1]",ns),
                        BankName = TagString(s.Demographics,"(/ns:StoreSurvey/ns:BankName)[1]",ns),
                        BusinessType = TagString(s.Demographics,"(/ns:StoreSurvey/ns:BusinessType)[1]",ns),
                        YearOpened = TagInt32(s.Demographics,"(/ns:StoreSurvey/ns:YearOpened)[1]",ns),
                        Specialty = TagString(s.Demographics,"(/ns:StoreSurvey/ns:Specialty)[1]",ns),
                        SquareFeet = TagInt32(s.Demographics,"(/ns:StoreSurvey/ns:SquareFeet)[1]",ns),
                        Brands = TagString(s.Demographics,"(/ns:StoreSurvey/ns:Brands)[1]",ns),
                        Internet = TagString(s.Demographics,"(/ns:StoreSurvey/ns:Internet)[1]",ns),
                        NumberEmployees = TagInt32(s.Demographics,"(/ns:StoreSurvey/ns:NumberEmployees)[1]",ns),
                    };
                var LINQ結果 = o.Execute(LINQ);
                var 直接結果 = (
                    from s in e.Sales.Store
                    select new {
                        s.BusinessEntityID,
                        s.Name,
                        AnnualSales = TagDecimal(s.Demographics,"(/ns:StoreSurvey/ns:AnnualSales)[1]",ns),
                        AnnualRevenue = TagDecimal(s.Demographics,"(/ns:StoreSurvey/ns:AnnualRevenue)[1]",ns),
                        BankName = TagString(s.Demographics,"(/ns:StoreSurvey/ns:BankName)[1]",ns),
                        BusinessType = TagString(s.Demographics,"(/ns:StoreSurvey/ns:BusinessType)[1]",ns),
                        YearOpened = TagInt32(s.Demographics,"(/ns:StoreSurvey/ns:YearOpened)[1]",ns),
                        Specialty = TagString(s.Demographics,"(/ns:StoreSurvey/ns:Specialty)[1]",ns),
                        SquareFeet = TagInt32(s.Demographics,"(/ns:StoreSurvey/ns:SquareFeet)[1]",ns),
                        Brands = TagString(s.Demographics,"(/ns:StoreSurvey/ns:Brands)[1]",ns),
                        Internet = TagString(s.Demographics,"(/ns:StoreSurvey/ns:Internet)[1]",ns),
                        NumberEmployees = TagInt32(s.Demographics,"(/ns:StoreSurvey/ns:NumberEmployees)[1]",ns),
                    }
                ).ToArray();
            }
            比較(
                () =>
                    from s in e.Sales.Store
                    select new {
                        s.BusinessEntityID,
                        s.Name,
                        AnnualSales = TagDecimal(s.Demographics,"(/ns:StoreSurvey/ns:AnnualSales)[1]",ns),
                        AnnualRevenue = TagDecimal(s.Demographics,"(/ns:StoreSurvey/ns:AnnualRevenue)[1]",ns),
                        BankName = TagString(s.Demographics,"(/ns:StoreSurvey/ns:BankName)[1]",ns),
                        BusinessType = TagString(s.Demographics,"(/ns:StoreSurvey/ns:BusinessType)[1]",ns),
                        YearOpened = TagInt32(s.Demographics,"(/ns:StoreSurvey/ns:YearOpened)[1]",ns),
                        Specialty = TagString(s.Demographics,"(/ns:StoreSurvey/ns:Specialty)[1]",ns),
                        SquareFeet = TagInt32(s.Demographics,"(/ns:StoreSurvey/ns:SquareFeet)[1]",ns),
                        Brands = TagString(s.Demographics,"(/ns:StoreSurvey/ns:Brands)[1]",ns),
                        Internet = TagString(s.Demographics,"(/ns:StoreSurvey/ns:Internet)[1]",ns),
                        NumberEmployees = TagInt32(s.Demographics,"(/ns:StoreSurvey/ns:NumberEmployees)[1]",ns),
                    },
                Command,
                "    SELECT * FROM Sales.vStoreWithDemographics",
                Reader => new {
                    BusinessEntityID = Reader.GetInt32(0),
                    Name = Reader.GetString(1),
                    AnnualSales= GetDecimal(Reader,2),
                    AnnualRevenue= GetDecimal(Reader,3),
                    BankName= GetString(Reader,4),
                    BusinessType= GetString(Reader,5),
                    YearOpened= GetInt32(Reader,6),
                    Specialty= GetString(Reader,7),
                    SquareFeet= GetInt32(Reader,8),
                    Brands= GetString(Reader,9),
                    Internet= GetString(Reader,10),
                    NumberEmployees= GetInt32(Reader,11),
                }
            );
        }
        //private static 
        //    IEnumerable<(Int32? ProductAssemblyID,Int32 ComponentID,String ComponentDesc, Decimal PerAssemblyQty, Decimal StandardCost,Decimal ListPrice, Int16 BOMLevel)> uspGetBillOfMaterials(Container e,Int32 StartProductID,DateTime CheckDate) 
        //{
        //    var result = from b in e.Production.BillOfMaterials
        //                 join p in e.Production.Product on b.ComponentID equals p.ProductID
        //                 where b.ProductAssemblyID==StartProductID
        //                     &&CheckDate>=b.StartDate
        //                     &&CheckDate<=(b.EndDate??CheckDate)
        //                 select (
        //                     b.ProductAssemblyID,
        //                     b.ComponentID,
        //                     ComponentDesc: p.Name,
        //                     b.PerAssemblyQty,
        //                     p.StandardCost,
        //                     p.ListPrice,
        //                     b.BOMLevel
        //                 );
        //    while(true) {
        //        var result2 = from cte in result
        //                      join b in e.Production.BillOfMaterials on cte.ComponentID equals b.ProductAssemblyID
        //                      join p in e.Production.Product on b.ComponentID equals p.ProductID
        //                      where CheckDate>=b.StartDate
        //                      &&CheckDate<=(b.EndDate??CheckDate)
        //                      select (
        //                          b.ProductAssemblyID,
        //                          b.ComponentID,
        //                          ComponentDesc: p.Name,
        //                          b.PerAssemblyQty,
        //                          p.StandardCost,
        //                          p.ListPrice,
        //                          b.BOMLevel
        //                      );
        //        if(result2.Count==result.Count) break;
        //        //if(result2.Count==result.Count) return result;
        //        result=result2;
        //    }
        //    var r = from b in BOM_cte(
        //        from b in e.Production.BillOfMaterials
        //        join p in e.Production.Product on b.ComponentID equals p.ProductID
        //        where b.ProductAssemblyID==StartProductID
        //            &&CheckDate>=b.StartDate
        //            &&CheckDate<=(b.EndDate??CheckDate)
        //        select (
        //            b.ProductAssemblyID,
        //            b.ComponentID,
        //            ComponentDesc: p.Name,
        //            b.PerAssemblyQty,
        //            p.StandardCost,
        //            p.ListPrice,
        //            b.BOMLevel,
        //            RecursionLevel:0
        //        ),
        //        0
        //    )
        //            group b by new { b.ComponentID,b.ComponentDesc,b.ProductAssemblyID,b.BOMLevel,b.RecursionLevel,b.StandardCost,b.ListPrice }into g
        //            select new { g.Key.ProductAssemblyID,g.Key.ComponentID,TotalQuantity = g.Sum(p => p.PerAssemblyQty),g.Key.StandardCost,g.Key.ListPrice,g.Key.BOMLevel,g.Key.RecursionLevel };
        //    return r;
        //    IEnumerable<(Int32? ProductAssemblyID, Int32 ComponentID, String ComponentDesc, Decimal PerAssemblyQty, Decimal StandardCost, Decimal ListPrice, Int16 BOMLevel, Int32 RecursionLevel)> BOM_cte(IEnumerable<(Int32? ProductAssemblyID, Int32 ComponentID, String ComponentDesc, Decimal PerAssemblyQty, Decimal StandardCost, Decimal ListPrice, Int16 BOMLevel,Int32 RecursionLevel)> source,Int32 RecursionLevel) {
        //        if(source.Count==0) return source;
        //        return source.Union(
        //            BOM_cte(
        //                from cte in source
        //                join b in e.Production.BillOfMaterials on cte.ComponentID equals b.ProductAssemblyID
        //                join p in e.Production.Product on b.ComponentID equals p.ProductID
        //                where CheckDate>=b.StartDate
        //                &&CheckDate<=(b.EndDate??CheckDate)
        //                select (
        //                    b.ProductAssemblyID,
        //                    b.ComponentID,
        //                    ComponentDesc: p.Name,
        //                    b.PerAssemblyQty,
        //                    p.StandardCost,
        //                    p.ListPrice,
        //                    b.BOMLevel,
        //                    RecursionLevel+1
        //                )
        //            )
        //        );
        //    }
        //}
        private static void uspGetBillOfMaterials(Container e,int StartProductID,DateTime CheckDate) {
            {
                var r = from b in BOM_cte(
                    from b in e.Production.BillOfMaterials
                    join p in e.Production.Product on b.ComponentID equals p.ProductID
                    where b.ProductAssemblyID==StartProductID
                        &&CheckDate>=b.StartDate
                        &&CheckDate<=(b.EndDate??CheckDate)
                    select (
                        b.ProductAssemblyID,
                        b.ComponentID,
                        ComponentDesc: p.Name,
                        b.PerAssemblyQty,
                        p.StandardCost,
                        p.ListPrice,
                        b.BOMLevel,
                        RecursionLevel: 0
                    ),
                    0
                )
                        group b by new { b.ComponentID,b.ComponentDesc,b.ProductAssemblyID,b.BOMLevel,b.RecursionLevel,b.StandardCost,b.ListPrice } into g
                        select new { g.Key.ProductAssemblyID,g.Key.ComponentID,g.Key.ComponentDesc, TotalQuantity = g.Sum(p => p.PerAssemblyQty),g.Key.StandardCost,g.Key.ListPrice,g.Key.BOMLevel,g.Key.RecursionLevel };
                var array = r.ToArray();
                IEnumerable<(int? ProductAssemblyID, int ComponentID, string ComponentDesc, decimal PerAssemblyQty, decimal StandardCost, decimal ListPrice, short BOMLevel, int RecursionLevel)> BOM_cte(IEnumerable<(int? ProductAssemblyID, int ComponentID, string ComponentDesc, decimal PerAssemblyQty, decimal StandardCost, decimal ListPrice, short BOMLevel, int RecursionLevel)> source,int RecursionLevel) {
                    if(source.Count==0) return source;
                    return source.Union(
                        BOM_cte(
                            from cte in source
                            join b in e.Production.BillOfMaterials on cte.ComponentID equals b.ProductAssemblyID
                            join p in e.Production.Product on b.ComponentID equals p.ProductID
                            where CheckDate>=b.StartDate
                            &&CheckDate<=(b.EndDate??CheckDate)
                            select (
                                b.ProductAssemblyID,
                                b.ComponentID,
                                ComponentDesc: p.Name,
                                b.PerAssemblyQty,
                                p.StandardCost,
                                p.ListPrice,
                                b.BOMLevel,
                                RecursionLevel+1
                            ),
                            RecursionLevel+1
                        )
                    );
                }
            }
            //比較(
            //    () =>
            //        from s in vDMPrep(e)
            //        where s.FiscalYear==2013
            //        select new {
            //            s.OrderNumber,
            //            s.LineNumber,
            //            s.Model
            //        },
            //    Command,
            //    "SELECT * FROM dbo.vAssocSeqLineItems",
            //    Reader => new {
            //        OrderNumber = Reader.GetString(0),
            //        LineNumber = Reader.GetByte(1),
            //        Model = GetString(Reader,2)
            //    }
            //);
        }
        class G:IEquatable<G> {
            private readonly int v;
            public G(int v) => this.v=v;
            public override bool Equals(object? obj) => obj is G other&&this.v==other.v;
            public bool Equals(G? other) => !(other is null)&&this.v==other.v;
            public static bool operator ==(G a,G b) => a.v==b.v;
            public static bool operator !=(G a,G b) => a.v!=b.v;
            public override int GetHashCode() => 0;
        }
        private static void GroupJoin0() {
            var G1 = new G(1);
            var S0 = new Set<G> { G1 };
            var S1 = new Set<G>();
            var S2 = new Set<G>();
            //var S0 = new G[] { new G(1) };
            //var S1 = Array.Empty<G>();
            //var S2 = Array.Empty<G>();
            var x =
                S0.GroupJoin(
                    S1,
                    (G s0) => s0,
                    (G s1) => s1,
                    (G s0,IEnumerable<G> g1) => (s0, g1)
                ).SelectMany(
                    ((G s0, IEnumerable<G> g1)h__TransparentIdentifier0) => h__TransparentIdentifier0.g1.DefaultIfEmpty(),
                    ((G s0, IEnumerable<G> g1)h__TransparentIdentifier0,G s1) => (
                        h__TransparentIdentifier0,
                        s1
                    )
                ).GroupJoin(
                    S2,
                    (((G s0, IEnumerable<G> g1) h__TransparentIdentifier0, G s1)h__TransparentIdentifier1) => h__TransparentIdentifier1.s1,
                    (G s2) => s2,
                    (((G s0, IEnumerable<G> g1) h__TransparentIdentifier0, G s1) h__TransparentIdentifier1,IEnumerable<G> g2) => (
                        h__TransparentIdentifier1,
                        g2
                    )
                ).SelectMany(
                    ((((G s0, IEnumerable<G> g1) h__TransparentIdentifier0, G s1) h__TransparentIdentifier1, IEnumerable<G> g2)h__TransparentIdentifier2) => h__TransparentIdentifier2.g2.DefaultIfEmpty(),
                    ((((G s0, IEnumerable<G> g1) h__TransparentIdentifier0, G s1) h__TransparentIdentifier1, IEnumerable<G> g2)h__TransparentIdentifier2,G s2) => (
                        h__TransparentIdentifier2.h__TransparentIdentifier1.h__TransparentIdentifier0.s0,
                        h__TransparentIdentifier2.h__TransparentIdentifier1.s1,
                        s2
                    )
                ).ToArray();
        }
        private static void GroupJoin1() {
            var G1 = new G(1);
            var S0 = new G[] { new G(1) };
            var S1 = Array.Empty<G>();
            var S2 = Array.Empty<G>();
            /*
            var x =
                S0.GroupJoin(
                    S1,
                    (G s0) => s0,
                    (G s1) => s1,
                    (G s0,IEnumerable<G> g1) => (s0, g1)
                ).SelectMany(
                    ((G s0, IEnumerable<G> g1)h__TransparentIdentifier0) => h__TransparentIdentifier0.g1.DefaultIfEmpty(),
                    ((G s0, IEnumerable<G> g1)h__TransparentIdentifier0,G? s1) => (
                        h__TransparentIdentifier0,
                        ss1:s1!
                    )
                ).GroupJoin(
                    S2,
                    (((G s0, IEnumerable<G> g1) h__TransparentIdentifier0, G s1) h__TransparentIdentifier1) => h__TransparentIdentifier1.s1,
                    (G s2) => s2,
                    (((G s0, IEnumerable<G> g1) h__TransparentIdentifier0, G s1) h__TransparentIdentifier1,IEnumerable<G> g2) => (
                        h__TransparentIdentifier1,
                        g2
                    )
                ).SelectMany(
                    ((((G s0, IEnumerable<G> g1) h__TransparentIdentifier0, G s1) h__TransparentIdentifier1, IEnumerable<G> g2) h__TransparentIdentifier2) => h__TransparentIdentifier2.g2.DefaultIfEmpty(),
                    ((((G s0, IEnumerable<G> g1) h__TransparentIdentifier0, G s1) h__TransparentIdentifier1, IEnumerable<G> g2) h__TransparentIdentifier2,G s2) => (
                        h__TransparentIdentifier2.h__TransparentIdentifier1.h__TransparentIdentifier0.s0,
                        h__TransparentIdentifier2.h__TransparentIdentifier1.s1,
                        s2!
                    )
                ).ToArray();
            */
        }
        private static void GroupJoin2() {
            var G1 = new G(1);
            var S0 = new G[] { new G(1) };
            var S1 = Array.Empty<G>();
            var S2 = Array.Empty<G>();
            var x = (
                from s0 in S0
                join s1 in S1 on s0 equals s1 into g1
                from s1 in g1.DefaultIfEmpty()
                join s2 in S2 on s1 equals s2 into g2
                from s2 in g2.DefaultIfEmpty()
                select new {
                    s0,s1,s2
                }
            ).ToArray();
        }
        private static void Main() {
            var sb = new StringBuilder("/0/");
            Console.WriteLine(sb.ToString());
            for(var a=0;a<16;a++){
                //Debug.Assert(sb[sb.Length-2]=='1');
                var c=sb.Length-2;
                while(true){
                    if(c==-1){
                        sb[1]='1';
                        c=3;
                        while(c<sb.Length) {
                            sb[c]='0';
                            c+=2;
                        }
                        sb.Append("0/");
                        break;
                    }
                    if(sb[c]=='0'){
                        sb[c]='1';
                        while((c+=2)<sb.Length){
                            sb[c]='0';
                        }
                        break;
                    }
                    c-=2;
                }
                Console.WriteLine(sb.ToString());
            }
            //GroupJoin2();
            //var n = new NumberFormatInfo();
            //n.CurrencyDecimalDigits=2;
            //Console.WriteLine(Decimal.Parse("1.123456",NumberStyles.AllowDecimalPoint, n));
            //n.CurrencyDecimalDigits=3;
            //Console.WriteLine(Decimal.Parse("1.123456",NumberStyles.AllowDecimalPoint,n));
            //n.NumberDecimalDigits=2;
            //Console.WriteLine(Decimal.Parse("1.123456",NumberStyles.AllowDecimalPoint,n));
            //n.NumberDecimalDigits=3;
            //Console.WriteLine(Decimal.Parse("1.123456",NumberStyles.AllowDecimalPoint,n));
            //{
            //    var A_Difinition = typeof(A<>);
            //    var A = A_Difinition.MakeGenericType(typeof(Int32));
            //    var A_Difinition_M_Difinition = A_Difinition.GetMethod("M");
            //    var A_Difinition_N_Difinition = A_Difinition.GetMethod("N");
            //    var A_Difinition_M = A_Difinition_M_Difinition.MakeGenericMethod(typeof(String));
            //    var A_M = A_Difinition.GetMethod(
            //        "M",
            //        A_Difinition_M_Difinition.GetParameters().Select(p=>p.ParameterType).ToArray()
            //    );
            //}
            //XmlNamespaceManager manager = new XmlNamespaceManager(new NameTable());
            //manager.AddNamespace("books","http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactInfo");
            //var d=XDocument.Parse(
            //    "<AdditionalContactInfo xmlns=\"http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactInfo\" "+
            //    "xmlns:crm=\"http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactRecord\" "+
            //    "xmlns:act=\"http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes\" "+
            //    ">"+
            //    "    <crm:ContactRecord date=\"2002-09-02Z\">"+
            //    "        The customer is interested in the Road-450 and Mountain-500 series bicycles. Customer called us. We need to follow up. Pager "+
            //    "        <act:pager>"+
            //    "            <act:number>206-555-1234</act:number>"+
            //    "        </act:pager> is the best way to reach."+
            //    "    </crm:ContactRecord>"+
            //    "</AdditionalContactInfo>"
            //);
            //var re = d.XPathSelectElements("/books:AdditionalContactInfo",manager);
            Load();
            var index = 0;
            Transaction(() => index++);
            var r = new Random(2);
            Transaction(() => r.Next());
            Create();
#pragma warning disable CA1303 // ローカライズされるパラメーターとしてリテラルを渡さない
            Console.WriteLine("終了");
#pragma warning restore CA1303 // ローカライズされるパラメーターとしてリテラルを渡さない
            Console.ReadKey();
        }
    }
    public class A<T0> {
        public (T0 t0, T1 t1) M<T1>(T0 t0,T1 t1) => (t0, t1);
        public (T0 t0, T1 t1) N<T1>(T0 t0,T1 t1) => (t0, t1);
    }
}
