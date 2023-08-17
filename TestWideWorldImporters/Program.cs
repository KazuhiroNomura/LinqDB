using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.SqlServer.Types;
using LinqDB.Sets;
using WideWorldImporters.Tables.Application;
using WideWorldImporters.Tables.Warehouse;
using WideWorldImporters.Tables.Purchasing;
using WideWorldImporters.Tables.Sales;
using WideWorldImporters;
using System.Data.SqlClient;

namespace TestWideWorldImporters;

abstract class Program:共通 {
    private static void Transaction(Func<Int32>Switchパターン) {
        const Int32 試行回数 = 10000;
        var Geography = new SqlGeography();
        using var Container = new Container();
        var Application = Container.Application;
        var Warehouse = Container.Warehouse;
        var Purchasing = Container.Purchasing;
        var Sales = Container.Sales;

        var People = Application.People;
        var Countries = Application.Countries;
        var DeliveryMethods = Application.DeliveryMethods;
        var PaymentMethods = Application.PaymentMethods;
        var TransactionTypes = Application.TransactionTypes;
        var SupplierCategories = Purchasing.SupplierCategories;
        var BuyingGroups = Sales.BuyingGroups;
        var CustomerCategories = Sales.CustomerCategories;
        var Colors = Warehouse.Colors;
        var PackageTypes = Warehouse.PackageTypes;
        var StockGroups = Warehouse.StockGroups;
        var StateProvinces = Application.StateProvinces;
        var Cities = Application.Cities;
        var SystemParameters = Application.SystemParameters;
        var Suppliers = Purchasing.Suppliers;
        var PurchaseOrders = Purchasing.PurchaseOrders;
        var PurchaseOrderLines = Purchasing.PurchaseOrderLines;
        var StockItems = Warehouse.StockItems;
        var StockItemHoldings = Warehouse.StockItemHoldings;
        var People情報 = new AddDel情報();
        var Countries情報 = new AddDel情報();
        var DeliveryMethods情報 = new AddDel情報();
        var PaymentMethods情報 = new AddDel情報();
        var TransactionTypes情報 = new AddDel情報();
        var SupplierCategories情報 = new AddDel情報();
        var BuyingGroups情報 = new AddDel情報();
        var CustomerCategories情報 = new AddDel情報();
        var Colors情報 = new AddDel情報();
        var PackageTypes情報 = new AddDel情報();
        var StockGroups情報 = new AddDel情報();
        var StateProvinces情報 = new AddDel情報();
        var Cities情報 = new AddDel情報();
        var SystemParameters情報 = new AddDel情報();
        var Suppliers情報 = new AddDel情報();
        var PurchaseOrders情報 = new AddDel情報();
        var PurchaseOrderLines情報 = new AddDel情報();
        var StockItems情報 = new AddDel情報();
        var StockItemHoldings情報 = new AddDel情報();
        var PeopleID = 0;
        var CountryID = 0;
        var DeliveryMethodID = 0;
        var PaymentMethodID = 0;
        var TransactionTypeID = 0;
        var SupplierCategoryID = 0;
        var BuyingGroupID = 0;
        var CustomerCategoryID = 0;
        var ColorID = 0;
        var PackageTypeID = 0;
        var StockGroupID = 0;
        var StateProvincesID = 0;
        var CitiesID = 0;
        var SystemParametersID = 0;
        var SuppliersID = 0;
        var StockItemID = 0;
        var PurchaseOrderID = 0;
        var PurchaseOrderLineID = 0;
        //var r = new Random(1);
        var s = Stopwatch.StartNew();
        for(var a = 0;a<試行回数;a++) {
            switch(Switchパターン()%2) {
                case 0:
                    switch(Switchパターン()%19) {
                        case 0:
                            if(People.Count==0) {
                                Add(ref People情報,People,new People(PeopleID,"","","",false,"",false,Array.Empty<Byte>(),false,false,false,"","","","",Array.Empty<Byte>(),"","",PeopleID,DateTime.Now,DateTime.Now));
                                PeopleID++;
                            } else {
                                Add(ref People情報,People,new People(PeopleID++,"","","",false,"",false,Array.Empty<Byte>(),false,false,false,"","","","",Array.Empty<Byte>(),"","",People.Sampling.PersonID,DateTime.Now,DateTime.Now));
                            }
                            break;
                        case 1:
                            if(People.Count==0) goto case 0;
                            Add(ref Countries情報,Countries,new Countries(CountryID++,"","","",0,"",0,"","","",Geography,People.Sampling.PersonID,DateTime.Now,DateTime.Now)); break;
                        case 2:
                            if(People.Count==0) goto case 0;
                            Add(ref DeliveryMethods情報,DeliveryMethods,new DeliveryMethods(DeliveryMethodID++,"",People.Sampling.PersonID,DateTime.Now,DateTime.Now)); break;
                        case 3:
                            if(People.Count==0) goto case 0;
                            Add(ref PaymentMethods情報,PaymentMethods,new PaymentMethods(PaymentMethodID++,"",People.Sampling.PersonID,DateTime.Now,DateTime.Now)); break;
                        case 4:
                            if(People.Count==0) goto case 0;
                            Add(ref TransactionTypes情報,TransactionTypes,new TransactionTypes(TransactionTypeID++,"",People.Sampling.PersonID,DateTime.Now,DateTime.Now)); break;
                        case 5:
                            if(People.Count==0) goto case 0;
                            Add(ref SupplierCategories情報,SupplierCategories,new SupplierCategories(SupplierCategoryID++,"",People.Sampling.PersonID,DateTime.Now,DateTime.Now)); break;
                        case 6:
                            if(People.Count==0) goto case 0;
                            Add(ref BuyingGroups情報,BuyingGroups,new BuyingGroups(BuyingGroupID++,"",People.Sampling.PersonID,DateTime.Now,DateTime.Now)); break;
                        case 7:
                            if(People.Count==0) goto case 0;
                            Add(ref CustomerCategories情報,CustomerCategories,new CustomerCategories(CustomerCategoryID++,"",People.Sampling.PersonID,DateTime.Now,DateTime.Now)); break;
                        case 8:
                            if(People.Count==0) goto case 0;
                            Add(ref Colors情報,Colors,new Colors(ColorID++,"",People.Sampling.PersonID,DateTime.Now,DateTime.Now)); break;
                        case 9:
                            if(People.Count==0) goto case 0;
                            Add(ref PackageTypes情報,PackageTypes,new PackageTypes(PackageTypeID++,"",People.Sampling.PersonID,DateTime.Now,DateTime.Now)); break;
                        case 10:
                            if(People.Count==0) goto case 0;
                            Add(ref StockGroups情報,StockGroups,new StockGroups(StockGroupID++,"",People.Sampling.PersonID,DateTime.Now,DateTime.Now)); break;
                        case 11:
                            if(Countries.Count==0) goto case 1;
                            Add(ref StateProvinces情報,StateProvinces,new StateProvinces(StateProvincesID++,"","",Countries.Sampling.CountryID,"",Geography,0,People.Sampling.PersonID,DateTime.Now,DateTime.Now)); break;
                        case 12:
                            if(StateProvinces.Count==0) goto case 11;
                            Add(ref Cities情報,Cities,new Cities(CitiesID++,"",StateProvinces.Sampling.StateProvinceID,Geography,0,People.Sampling.PersonID,DateTime.Now,DateTime.Now)); break;
                        case 13:
                            if(Cities.Count==0) goto case 12;
                            Add(ref SystemParameters情報,SystemParameters,new SystemParameters(SystemParametersID++,"","",Cities.Sampling.CityID,"",Geography,"","",Cities.Sampling.CityID,"","",People.Sampling.PersonID,DateTime.Now)); break;
                        case 14:
                            if(SupplierCategories.Count==0) goto case 5;
                            if(People.Count==0) goto case 0;
                            if(DeliveryMethods.Count==0) goto case 2;
                            if(Cities.Count==0) goto case 12;
                            Add(
                                ref Suppliers情報,
                                Suppliers,
                                new Suppliers(
                                    SuppliersID++,
                                    "",
                                    SupplierCategories.Sampling.SupplierCategoryID,
                                    People.Sampling.PersonID,
                                    People.Sampling.PersonID,
                                    DeliveryMethods.Sampling.DeliveryMethodID,
                                    Cities.Sampling.CityID,
                                    Cities.Sampling.CityID,
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    0,"",
                                    "",
                                    "","",
                                    "",
                                    "",
                                    "",
                                    Geography,
                                    "",
                                    "",
                                    "",
                                    People.Sampling.PersonID,
                                    DateTime.Now,
                                    DateTime.Now
                                )
                            );
                            break;
                        case 15:
                            if(SupplierCategories.Count==0) goto case 5;
                            if(People.Count==0) goto case 0;
                            if(Colors.Count==0) goto case 8;
                            Add(
                                ref StockItems情報,
                                StockItems,
                                new StockItems(
                                    StockItemID++,
                                    "",
                                    SupplierCategories.Sampling.SupplierCategoryID,
                                    Colors.Sampling.ColorID,
                                    0,0,
                                    "","",0,0,false,"",0,0,0,0,"","",Array.Empty<Byte>(),"","","",
                                    People.Sampling.PersonID,
                                    DateTime.Now,DateTime.Now
                                )
                            );
                            break;
                        case 16:
                            if(People.Count==0) goto case 0;
                            if(DeliveryMethods.Count==0) goto case 2;
                            Add(
                                ref PurchaseOrders情報,
                                PurchaseOrders,
                                new PurchaseOrders(
                                    PurchaseOrderID++,
                                    0,DateTime.Now,
                                    DeliveryMethods.Sampling.DeliveryMethodID,
                                    People.Sampling.PersonID,
                                    DateTime.Now,"",false,"","",
                                    People.Sampling.PersonID,
                                    DateTime.Now
                                )
                            );
                            break;
                        case 17:
                            if(People.Count==0) goto case 0;
                            Add(
                                ref StockItemHoldings情報,
                                StockItemHoldings,
                                new StockItemHoldings(
                                    StockItemID++,
                                    0,"",0,0,0,0,
                                    People.Sampling.PersonID,
                                    DateTime.Now
                                )
                            );
                            break;
                        case 18:
                            if(PurchaseOrders.Count==0) goto case 16;
                            if(StockItems.Count==0) goto case 15;
                            if(PackageTypes.Count==0) goto case 9;
                            if(People.Count==0) goto case 0;
                            if(a==569) {

                            }
                            Add(
                                ref PurchaseOrderLines情報,
                                PurchaseOrderLines,
                                new PurchaseOrderLines(
                                    PurchaseOrderLineID++,
                                    PurchaseOrders.Sampling.PurchaseOrderID,
                                    StockItems.Sampling.StockItemID,
                                    0,"",0,
                                    PackageTypes.Sampling.PackageTypeID,
                                    0,DateTime.Now,false,
                                    People.Sampling.PersonID,
                                    DateTime.Now
                                )
                            );
                            break;
                    }
                    break;
                case 1:
                    switch(Switchパターン()%19) {
                        case 0: 
                            Del(ref People情報,People);
                            break;
                        case 1:Del(ref Countries情報,Countries);break;
                        case 2:Del(ref DeliveryMethods情報,DeliveryMethods);break;
                        case 3:Del(ref PaymentMethods情報,PaymentMethods);break;
                        case 4:Del(ref TransactionTypes情報,TransactionTypes);break;
                        case 5:Del(ref SupplierCategories情報,SupplierCategories);break;
                        case 6:Del(ref BuyingGroups情報,BuyingGroups);break;
                        case 7:Del(ref CustomerCategories情報,CustomerCategories);break;
                        case 8:Del(ref Colors情報,Colors);break;
                        case 9:Del(ref PackageTypes情報,PackageTypes);break;
                        case 10:Del(ref StockGroups情報,StockGroups);break;
                        case 11:Del(ref StateProvinces情報,StateProvinces);break;
                        case 12:Del(ref Cities情報,Cities);break;
                        case 13:Del(ref SystemParameters情報,SystemParameters);break;
                        case 14:Del(ref Suppliers情報,Suppliers);break;
                        case 15:Del(ref StockItems情報,StockItems);break;
                        case 16:Del(ref PurchaseOrders情報,PurchaseOrders);break;
                        case 17:Del(ref StockItemHoldings情報,StockItemHoldings);break;
                        case 18:Del(ref PurchaseOrderLines情報,PurchaseOrderLines);break;
                    }
                    break;
            }
        }
        s.Stop();
        Trace.WriteLine($"Create {s.ElapsedMilliseconds,7}ms");
        Console.WriteLine($"Create {s.ElapsedMilliseconds,7}ms");
        AddWriteLine(People情報);
        AddWriteLine(Countries情報);
        AddWriteLine(DeliveryMethods情報);
        AddWriteLine(PaymentMethods情報);
        AddWriteLine(TransactionTypes情報);
        AddWriteLine(SupplierCategories情報);
        AddWriteLine(BuyingGroups情報);
        AddWriteLine(CustomerCategories情報);
        AddWriteLine(Colors情報);
        AddWriteLine(PackageTypes情報);
        AddWriteLine(StockGroups情報);
        AddWriteLine(StateProvinces情報);
        AddWriteLine(Cities情報);
        AddWriteLine(SystemParameters情報);
        AddWriteLine(Suppliers情報);
        AddWriteLine(PurchaseOrders情報);
        AddWriteLine(PurchaseOrderLines情報);
        AddWriteLine(StockItems情報);
        AddWriteLine(StockItemHoldings情報);

        DelWriteLine(People情報);
        DelWriteLine(Countries情報);
        DelWriteLine(DeliveryMethods情報);
        DelWriteLine(PaymentMethods情報);
        DelWriteLine(TransactionTypes情報);
        DelWriteLine(SupplierCategories情報);
        DelWriteLine(BuyingGroups情報);
        DelWriteLine(CustomerCategories情報);
        DelWriteLine(Colors情報);
        DelWriteLine(PackageTypes情報);
        DelWriteLine(StockGroups情報);
        DelWriteLine(StateProvinces情報);
        DelWriteLine(Cities情報);
        DelWriteLine(SystemParameters情報);
        DelWriteLine(Suppliers情報);
        DelWriteLine(PurchaseOrders情報);
        DelWriteLine(PurchaseOrderLines情報);
        DelWriteLine(StockItems情報);
        DelWriteLine(StockItemHoldings情報);
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization","CA1305:IFormatProvider を指定します",Justification = "<保留中>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization","CA1303:ローカライズされるパラメーターとしてリテラルを渡さない",Justification = "<保留中>")]
    private static void Create() {
        using var Container = new Container();
        var Application = Container.Application;
        var Warehouse = Container.Warehouse;
        var Purchasing = Container.Purchasing;
        var Sales = Container.Sales;
        var Geography = new SqlGeography();
        var People = Application.People;
        var Countries = Application.Countries;
        var DeliveryMethods = Application.DeliveryMethods;
        var PaymentMethods = Application.PaymentMethods;
        var TransactionTypes = Application.TransactionTypes;
        var SupplierCategories = Purchasing.SupplierCategories;
        var BuyingGroups = Sales.BuyingGroups;
        var CustomerCategories = Sales.CustomerCategories;
        var Colors = Warehouse.Colors;
        var PackageTypes = Warehouse.PackageTypes;
        var StockGroups = Warehouse.StockGroups;

        var StateProvinces = Application.StateProvinces;
        var Cities = Application.Cities;
        var SystemParameters = Application.SystemParameters;

        var Suppliers = Purchasing.Suppliers;
        var PurchaseOrders = Purchasing.PurchaseOrders;
        var PurchaseOrderLines = Purchasing.PurchaseOrderLines;
        var StockItems = Warehouse.StockItems;
        var StockItemHoldings = Warehouse.StockItemHoldings;

        var Customers = Sales.Customers;
        var Invoices = Sales.Invoices;
        var CustomerTransactions = Sales.CustomerTransactions;
        var Orders = Sales.Orders;
        var s = Stopwatch.StartNew();
        //LV0
        for(var PersonID=0;PersonID<2;PersonID++){
            People.AddOrThrow(
                new People(
                    PersonID,"","","",false,"",false,Array.Empty<Byte>(),false,false,false,"","","","",Array.Empty<Byte>(),"","",PersonID/2,DateTime.Now,DateTime.Now
                )
            );
        }
        {
            var Key=0;
            E(
                People,
                a=>{
                    for(var b=0;b<2;b++){
                        Countries.AddOrThrow(new Countries(Key,"","","",0,"",0,"","","",Geography,a.PersonID,DateTime.Now,DateTime.Now));
                        DeliveryMethods.AddOrThrow(new DeliveryMethods(Key,"",a.PersonID,DateTime.Now,DateTime.Now));
                        PaymentMethods.AddOrThrow(new PaymentMethods(Key,"",a.PersonID,DateTime.Now,DateTime.Now));
                        TransactionTypes.AddOrThrow(new TransactionTypes(Key,"",a.PersonID,DateTime.Now,DateTime.Now));
                        SupplierCategories.AddOrThrow(new SupplierCategories(Key,"",a.PersonID,DateTime.Now,DateTime.Now));
                        BuyingGroups.AddOrThrow(new BuyingGroups(Key,"",a.PersonID,DateTime.Now,DateTime.Now));
                        CustomerCategories.AddOrThrow(new CustomerCategories(Key,"",a.PersonID,DateTime.Now,DateTime.Now));
                        Colors.AddOrThrow(new Colors(Key,"",a.PersonID,DateTime.Now,DateTime.Now));
                        PackageTypes.AddOrThrow(new PackageTypes(Key,"",a.PersonID,DateTime.Now,DateTime.Now));
                        StockGroups.AddOrThrow(new StockGroups(Key,"",a.PersonID,DateTime.Now,DateTime.Now));
                        Key++;
                    }
                }
            );
        }
        {
            var StateProvincesID = 0;
            E(
                Countries,
                a =>{
                    for(var b=0;b<2;b++){
                        StateProvinces.AddOrThrow(new StateProvinces(StateProvincesID++,"","",a.CountryID,"",Geography,0,a.LastEditedBy,DateTime.Now,DateTime.Now));
                    }
                }
            );
        }
        {
            var CitiesID = 0;
            E(
                StateProvinces,
                a =>{
                    for(var b=0;b<2;b++){
                        Cities.AddOrThrow(new Cities(CitiesID++,"",a.StateProvinceID,Geography,0,a.LastEditedBy,DateTime.Now,DateTime.Now));
                    }
                }
            );
        }
        {
            var SystemParametersID = 0;
            E(
                Cities,
                a => E(
                    Cities,
                    b=>{
                        for(var c=0;c<2;c++){
                            SystemParameters.AddOrThrow(new SystemParameters(SystemParametersID++,"","",a.CityID,"",Geography,"","",b.CityID,"","",b.LastEditedBy,DateTime.Now));
                        }
                    }
                )
            );
        }
        {
            var SuppliersID = 0;
            E(
                SupplierCategories,
                a => E(
                    People,
                    b => E(
                        People,
                        c => E(
                            DeliveryMethods,
                            d => E(
                                Cities,
                                e => E(
                                    Cities,
                                    f => E(
                                        People,
                                        g => Suppliers.AddOrThrow(
                                            new Suppliers(
                                                SuppliersID++,
                                                "",
                                                a.SupplierCategoryID,
                                                b.PersonID,
                                                c.PersonID,
                                                d.DeliveryMethodID,
                                                e.CityID,
                                                f.CityID,
                                                "",
                                                "",
                                                "",
                                                "",
                                                "",
                                                "",
                                                0,"",
                                                "",
                                                "","",
                                                "",
                                                "",
                                                "",
                                                Geography,
                                                "",
                                                "",
                                                "",
                                                g.PersonID,
                                                DateTime.Now,
                                                DateTime.Now
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    )
                )
            );
        }
        {
            var StockItemID = 0;
            E(
                Suppliers,
                a => E(
                    Colors,
                    b => E(
                        People,
                        c => StockItems.AddOrThrow(
                            new StockItems(
                                StockItemID++,
                                "",
                                a.SupplierID,
                                b.ColorID,
                                0,0,
                                "","",0,0,false,"",0,0,0,0,"","",Array.Empty<Byte>(),"","","",
                                c.PersonID,
                                DateTime.Now,DateTime.Now
                            )
                        )
                    )
                )
            );
        }
        {
            var PurchaseOrderID = 0;
            E(
                DeliveryMethods,
                a=>E(
                    People,
                    b => E(
                        People,
                        c =>PurchaseOrders.AddOrThrow(
                            new PurchaseOrders(
                                PurchaseOrderID++,
                                0,DateTime.Now,
                                a.DeliveryMethodID,
                                b.PersonID,
                                DateTime.Now,"",false,"","",
                                c.PersonID,
                                DateTime.Now
                            )
                        )
                    )
                )
            );
        }
        {
            var StockItemID = 0;
            E(
                People,
                a => StockItemHoldings.AddOrThrow(
                    new StockItemHoldings(
                        StockItemID++,
                        0,"",0,0,0,0,
                        a.PersonID,
                        DateTime.Now
                    )
                )
            );
        }
        {
            var PurchaseOrderLineID = 0;
            E(
                PurchaseOrders,
                a => E(
                    StockItems.Take(65536),
                    b => E(
                        PackageTypes,
                        c => E(
                            People,
                            d => PurchaseOrderLines.AddOrThrow(
                                new PurchaseOrderLines(
                                    PurchaseOrderLineID++,
                                    a.PurchaseOrderID,
                                    b.StockItemID,
                                    0,"",0,
                                    c.PackageTypeID,
                                    0,DateTime.Now,false,
                                    d.PersonID,
                                    DateTime.Now
                                )
                            )
                        )
                    )
                )
            );
        }
        {
            var CustomerID = 0;
            E(
                CustomerCategories,
                a => E(
                    BuyingGroups,
                    b => E(
                        People,
                        c => E(
                            People,
                            _ => E(
                                DeliveryMethods,
                                e => E(
                                    People,
                                    f=>Customers.AddOrThrow(
                                        new Customers(
                                            CustomerID,
                                            "",
                                            (CustomerID++)/2,
                                            a.CustomerCategoryID,
                                            b.BuyingGroupID,
                                            c.PersonID,
                                            c.PersonID,
                                            e.DeliveryMethodID,
                                            0,0,null,DateTime.Now,0,false,false,0,"","",null,null,"","",null,"",null,"",null,"",
                                            f.PersonID,
                                            DateTime.Now,
                                            DateTime.Now
                                        )
                                    )
                                )
                            )
                        )
                    )
                )
            );
        }
        {
            var CustomerTransactionsID = 0;
            E(
                Customers, 
                a => E(
                    TransactionTypes,
                    b => E(
                        Invoices,
                        c=>E(
                            PaymentMethods,
                            d=>E(
                                People,
                                e=>CustomerTransactions.AddOrThrow(
                                    new CustomerTransactions(
                                        CustomerTransactionsID++,
                                        a.CustomerID,
                                        b.TransactionTypeID,
                                        c.InvoiceID,
                                        d.PaymentMethodID,
                                        DateTime.Now,
                                        0,0,0,0,null,null,
                                        e.PersonID,
                                        DateTime.Now
                                    )
                                )
                            )
                        )
                    )
                )
            );
        }
        {
            var OrderID = 0;
            E(
                Customers,
                a => E(
                    People,
                    b => E(
                        People,
                        c => E(
                            People,
                            d => E(
                                People,
                                f =>Orders.AddOrThrow(
                                    new Orders(
                                        OrderID,
                                        a.CustomerID,
                                        b.PersonID,
                                        c.PersonID,
                                        d.PersonID,
                                        (OrderID++)/2,
                                        DateTime.Now,
                                        DateTime.Now,
                                        null,
                                        false,
                                        null,
                                        null,
                                        null,
                                        null,
                                        f.PersonID,
                                        DateTime.Now
                                    )
                                )
                            )
                        )
                    )
                )
            );
        }
        {
            var InvoiceID = 0;
            E(
                Customers,
                a => E(
                    Customers,
                    b => E(
                        Customers,
                        _ => E(
                            Orders,
                            d => E(
                                DeliveryMethods,
                                e => E(
                                    People,
                                    f => E(
                                        People,
                                        _ => E(
                                            People,
                                            _ => E(
                                                People,
                                                _ => E(
                                                    People,
                                                    _ => E(
                                                        People,
                                                        k => {
                                                            if(InvoiceID==10000000) return;
                                                            Invoices.AddOrThrow(
                                                                new Invoices(
                                                                    InvoiceID++,
                                                                    a.CustomerID,
                                                                    b.CustomerID,
                                                                    d.OrderID,
                                                                    e.DeliveryMethodID,
                                                                    f.PersonID,
                                                                    f.PersonID,
                                                                    f.PersonID,
                                                                    f.PersonID,
                                                                    DateTime.Now,
                                                                    null,
                                                                    false,
                                                                    null,
                                                                    null,
                                                                    null,
                                                                    null,
                                                                    0,
                                                                    0,
                                                                    null,
                                                                    null,
                                                                    null,
                                                                    null,
                                                                    null,
                                                                    k.PersonID,
                                                                    DateTime.Now
                                                                )
                                                            );
                                                        }
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    )
                )
            );
        }
        Console.WriteLine($"Create {s.ElapsedMilliseconds,7}ms");
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization","CA1305:IFormatProvider を指定します",Justification = "<保留中>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization","CA1303:ローカライズされるパラメーターとしてリテラルを渡さない",Justification = "<保留中>")]
    private static void Load() {
        using var C = new SqlConnection(@"Data Source=localhost\MSSQLSERVER2019;Initial Catalog=WideWorldImporters;Integrated Security=True");
        C.Open();
        using var Command = new SqlCommand { Connection=C };
        using var e = new Container();
        var Application = e.Application;
        var Warehouse = e.Warehouse;
        var Purchasing = e.Purchasing;
        var Sales = e.Sales;
        var s = Stopwatch.StartNew();
        {
            Command.CommandText=@"
                SELECT
                 [PersonID]
                ,[FullName]
                ,[PreferredName]
                ,[SearchName]
                ,[IsPermittedToLogon]
                ,[LogonName]
                ,[IsExternalLogonProvider]
                ,[HashedPassword]
                ,[IsSystemUser]
                ,[IsEmployee]
                ,[IsSalesperson]
                ,[UserPreferences]
                ,[PhoneNumber]
                ,[FaxNumber]
                ,[EmailAddress]
                ,[Photo]
                ,[CustomFields]
                ,[OtherLanguages]
                ,[LastEditedBy]
                ,[ValidFrom]
                ,[ValidTo]
                FROM [Application].[People];
                SELECT 
                 [CountryID]
                ,[CountryName]
                ,[FormalName]
                ,[IsoAlpha3Code]
                ,[IsoNumericCode]
                ,[CountryType]
                ,[LatestRecordedPopulation]
                ,[Continent]
                ,[Region]
                ,[Subregion]
                ,[Border]
                ,[LastEditedBy]
                ,[ValidFrom]
                ,[ValidTo]
                FROM [Application].[Countries];
                SELECT 
                 [CountryID]
                ,[CountryName]
                ,[FormalName]
                ,[IsoAlpha3Code]
                ,[IsoNumericCode]
                ,[CountryType]
                ,[LatestRecordedPopulation]
                ,[Continent]
                ,[Region]
                ,[Subregion]
                ,[Border]
                ,[LastEditedBy]
                ,[ValidFrom]
                ,[ValidTo]
                FROM [Application].[Countries_Archive];
                SELECT 
                 [DeliveryMethodID]
                ,[DeliveryMethodName]
                ,[LastEditedBy]
                ,[ValidFrom]
                ,[ValidTo]
                FROM [Application].[DeliveryMethods];
                SELECT 
                 [PaymentMethodID]
                ,[PaymentMethodName]
                ,[LastEditedBy]
                ,[ValidFrom]
                ,[ValidTo]
                FROM [Application].[PaymentMethods];
                SELECT 
                 [TransactionTypeID]
                ,[TransactionTypeName]
                ,[LastEditedBy]
                ,[ValidFrom]
                ,[ValidTo]
                FROM [Application].[TransactionTypes];
                SELECT
                 [TransactionTypeID]
                ,[TransactionTypeName]
                ,[LastEditedBy]
                ,[ValidFrom]
                ,[ValidTo]
                FROM [WideWorldImporters].[Application].[TransactionTypes_Archive]
                SELECT 
                 [SupplierCategoryID]
                ,[SupplierCategoryName]
                ,[LastEditedBy]
                ,[ValidFrom]
                ,[ValidTo]
                FROM [Purchasing].[SupplierCategories];
                SELECT 
                 [BuyingGroupID]
                ,[BuyingGroupName]
                ,[LastEditedBy]
                ,[ValidFrom]
                ,[ValidTo]
                FROM [Sales].[BuyingGroups];
                SELECT 
                 [CustomerCategoryID]
                ,[CustomerCategoryName]
                ,[LastEditedBy]
                ,[ValidFrom]
                ,[ValidTo]
                FROM [Sales].[CustomerCategories];
                SELECT 
                 [ColorID]
                ,[ColorName]
                ,[LastEditedBy]
                ,[ValidFrom]
                ,[ValidTo]
                FROM [Warehouse].[Colors];
                SELECT 
                 [PackageTypeID]
                ,[PackageTypeName]
                ,[LastEditedBy]
                ,[ValidFrom]
                ,[ValidTo]
                FROM [Warehouse].[PackageTypes];
                SELECT 
                 [StockGroupID]
                ,[StockGroupName]
                ,[LastEditedBy]
                ,[ValidFrom]
                ,[ValidTo]
                FROM [Warehouse].[StockGroups];
                SELECT 
                [StateProvinceID]
                ,[StateProvinceCode]
                ,[StateProvinceName]
                ,[CountryID]
                ,[SalesTerritory]
                ,[Border]
                ,[LatestRecordedPopulation]
                ,[LastEditedBy]
                ,[ValidFrom]
                ,[ValidTo]
                FROM [Application].[StateProvinces]
                SELECT 
                 [CityID]
                ,[CityName]
                ,[StateProvinceID]
                ,[Location]
                ,[LatestRecordedPopulation]
                ,[LastEditedBy]
                ,[ValidFrom]
                ,[ValidTo]
                FROM [Application].[Cities];
                SELECT 
                 [CityID]
                ,[CityName]
                ,[StateProvinceID]
                ,[Location]
                ,[LatestRecordedPopulation]
                ,[LastEditedBy]
                ,[ValidFrom]
                ,[ValidTo]
                FROM [Application].[Cities_Archive];
                SELECT 
                 [SystemParameterID]
                ,[DeliveryAddressLine1]
                ,[DeliveryAddressLine2]
                ,[DeliveryCityID]
                ,[DeliveryPostalCode]
                ,[DeliveryLocation]
                ,[PostalAddressLine1]
                ,[PostalAddressLine2]
                ,[PostalCityID]
                ,[PostalPostalCode]
                ,[ApplicationSettings]
                ,[LastEditedBy]
                ,[LastEditedWhen]
                FROM [Application].[SystemParameters];
                SELECT
                 [CustomerID]
                ,[CustomerName]
                ,[BillToCustomerID]
                ,[CustomerCategoryID]
                ,[BuyingGroupID]
                ,[PrimaryContactPersonID]
                ,[AlternateContactPersonID]
                ,[DeliveryMethodID]
                ,[DeliveryCityID]
                ,[PostalCityID]
                ,[CreditLimit]
                ,[AccountOpenedDate]
                ,[StandardDiscountPercentage]
                ,[IsStatementSent]
                ,[IsOnCreditHold]
                ,[PaymentDays]
                ,[PhoneNumber]
                ,[FaxNumber]
                ,[DeliveryRun]
                ,[RunPosition]
                ,[WebsiteURL]
                ,[DeliveryAddressLine1]
                ,[DeliveryAddressLine2]
                ,[DeliveryPostalCode]
                ,[DeliveryLocation]
                ,[PostalAddressLine1]
                ,[PostalAddressLine2]
                ,[PostalPostalCode]
                ,[LastEditedBy]
                ,[ValidFrom]
                ,[ValidTo]
                FROM [Sales].[Customers]
                SELECT 
                [SupplierID]
                ,[SupplierName]
                ,[SupplierCategoryID]
                ,[PrimaryContactPersonID]
                ,[AlternateContactPersonID]
                ,[DeliveryMethodID]
                ,[DeliveryCityID]
                ,[PostalCityID]
                ,[SupplierReference]
                ,[BankAccountName]
                ,[BankAccountBranch]
                ,[BankAccountCode]
                ,[BankAccountNumber]
                ,[BankInternationalCode]
                ,[PaymentDays]
                ,[InternalComments]
                ,[PhoneNumber]
                ,[FaxNumber]
                ,[WebsiteURL]
                ,[DeliveryAddressLine1]
                ,[DeliveryAddressLine2]
                ,[DeliveryPostalCode]
                ,[DeliveryLocation]
                ,[PostalAddressLine1]
                ,[PostalAddressLine2]
                ,[PostalPostalCode]
                ,[LastEditedBy]
                ,[ValidFrom]
                ,[ValidTo]
                FROM [Purchasing].[Suppliers]
                SELECT 
                 [StockItemID]
                ,[StockItemName]
                ,[SupplierID]
                ,[ColorID]
                ,[UnitPackageID]
                ,[OuterPackageID]
                ,[Brand]
                ,[Size]
                ,[LeadTimeDays]
                ,[QuantityPerOuter]
                ,[IsChillerStock]
                ,[Barcode]
                ,[TaxRate]
                ,[UnitPrice]
                ,[RecommendedRetailPrice]
                ,[TypicalWeightPerUnit]
                ,[MarketingComments]
                ,[InternalComments]
                ,[Photo]
                ,[CustomFields]
                ,[Tags]
                ,[SearchDetails]
                ,[LastEditedBy]
                ,[ValidFrom]
                ,[ValidTo]
                FROM [Warehouse].[StockItems];
                WITH Children(OrderID,CustomerID,SalespersonPersonID,PickedByPersonID,ContactPersonID,BackorderOrderID,OrderDate,ExpectedDeliveryDate,CustomerPurchaseOrderNumber,IsUndersupplyBackordered,Comments,DeliveryInstructions,InternalComments,PickingCompletedWhen,LastEditedBy,LastEditedWhen,DEPTH
                ) AS (
                    SELECT    A.OrderID,A.CustomerID,A.SalespersonPersonID,A.PickedByPersonID,A.ContactPersonID,A.BackorderOrderID,A.OrderDate,A.ExpectedDeliveryDate,A.CustomerPurchaseOrderNumber,A.IsUndersupplyBackordered,A.Comments,A.DeliveryInstructions,A.InternalComments,A.PickingCompletedWhen,A.LastEditedBy,A.LastEditedWhen,0
                    FROM[Sales].[Orders]A
                    WHERE BackorderOrderID IS NULL
                    UNION ALL
                    SELECT    A.OrderID,A.CustomerID,A.SalespersonPersonID,A.PickedByPersonID,A.ContactPersonID,A.BackorderOrderID,A.OrderDate,A.ExpectedDeliveryDate,A.CustomerPurchaseOrderNumber,A.IsUndersupplyBackordered,A.Comments,A.DeliveryInstructions,A.InternalComments,A.PickingCompletedWhen,A.LastEditedBy,A.LastEditedWhen,B.DEPTH+1
                    FROM[Sales].[Orders]A,Children B
                    WHERE A.BackorderOrderID = B.OrderID
                )
                SELECT [OrderID],[CustomerID],[SalespersonPersonID],[PickedByPersonID],[ContactPersonID],[BackorderOrderID],[OrderDate],[ExpectedDeliveryDate],[CustomerPurchaseOrderNumber],[IsUndersupplyBackordered],[Comments],[DeliveryInstructions],[InternalComments],[PickingCompletedWhen],[LastEditedBy],[LastEditedWhen]
                FROM Children ORDER BY DEPTH;
                SELECT 
                 [OrderLineID]
                ,[OrderID]
                ,[StockItemID]
                ,[Description]
                ,[PackageTypeID]
                ,[Quantity]
                ,[UnitPrice]
                ,[TaxRate]
                ,[PickedQuantity]
                ,[PickingCompletedWhen]
                ,[LastEditedBy]
                ,[LastEditedWhen]
                FROM [WideWorldImporters].[Sales].[OrderLines]
                SELECT
                 [InvoiceID]
                ,[CustomerID]
                ,[BillToCustomerID]
                ,[OrderID]
                ,[DeliveryMethodID]
                ,[ContactPersonID]
                ,[AccountsPersonID]
                ,[SalespersonPersonID]
                ,[PackedByPersonID]
                ,[InvoiceDate]
                ,[CustomerPurchaseOrderNumber]
                ,[IsCreditNote]
                ,[CreditNoteReason]
                ,[Comments]
                ,[DeliveryInstructions]
                ,[InternalComments]
                ,[TotalDryItems]
                ,[TotalChillerItems]
                ,[DeliveryRun]
                ,[RunPosition]
                ,[ReturnedDeliveryData]
                ,[ConfirmedDeliveryTime]
                ,[ConfirmedReceivedBy]
                ,[LastEditedBy]
                ,[LastEditedWhen]
                FROM[WideWorldImporters].[Sales].[Invoices]
                SELECT
                 [InvoiceLineID]
                ,[InvoiceID]
                ,[StockItemID]
                ,[Description]
                ,[PackageTypeID]
                ,[Quantity]
                ,[UnitPrice]
                ,[TaxRate]
                ,[TaxAmount]
                ,[LineProfit]
                ,[ExtendedPrice]
                ,[LastEditedBy]
                ,[LastEditedWhen]
                FROM [WideWorldImporters].[Sales].[InvoiceLines]
                SELECT 
                 [SpecialDealID]
                ,[StockItemID]
                ,[CustomerID]
                ,[BuyingGroupID]
                ,[CustomerCategoryID]
                ,[StockGroupID]
                ,[DealDescription]
                ,[StartDate]
                ,[EndDate]
                ,[DiscountAmount]
                ,[DiscountPercentage]
                ,[UnitPrice]
                ,[LastEditedBy]
                ,[LastEditedWhen]
                FROM [WideWorldImporters].[Sales].[SpecialDeals]
                SELECT
                 [CustomerTransactionID]
                ,[CustomerID]
                ,[TransactionTypeID]
                ,[InvoiceID]
                ,[PaymentMethodID]
                ,[TransactionDate]
                ,[AmountExcludingTax]
                ,[TaxAmount]
                ,[TransactionAmount]
                ,[OutstandingBalance]
                ,[FinalizationDate]
                ,[IsFinalized]
                ,[LastEditedBy]
                ,[LastEditedWhen]
                FROM [WideWorldImporters].[Sales].[CustomerTransactions]
                SELECT
                 [StockItemID]
                ,[QuantityOnHand]
                ,[BinLocation]
                ,[LastStocktakeQuantity]
                ,[LastCostPrice]
                ,[ReorderLevel]
                ,[TargetStockLevel]
                ,[LastEditedBy]
                ,[LastEditedWhen]
                FROM [WideWorldImporters].[Warehouse].[StockItemHoldings]
                SELECT
                 [StockItemStockGroupID]
                ,[StockItemID]
                ,[StockGroupID]
                ,[LastEditedBy]
                ,[LastEditedWhen]
                FROM [WideWorldImporters].[Warehouse].[StockItemStockGroups]
                SELECT 
                 [PurchaseOrderID]
                ,[SupplierID]
                ,[OrderDate]
                ,[DeliveryMethodID]
                ,[ContactPersonID]
                ,[ExpectedDeliveryDate]
                ,[SupplierReference]
                ,[IsOrderFinalized]
                ,[Comments]
                ,[InternalComments]
                ,[LastEditedBy]
                ,[LastEditedWhen]
                FROM [WideWorldImporters].[Purchasing].[PurchaseOrders]
                SELECT
                 [PurchaseOrderLineID]
                ,[PurchaseOrderID]
                ,[StockItemID]
                ,[OrderedOuters]
                ,[Description]
                ,[ReceivedOuters]
                ,[PackageTypeID]
                ,[ExpectedUnitPricePerOuter]
                ,[LastReceiptDate]
                ,[IsOrderLineFinalized]
                ,[LastEditedBy]
                ,[LastEditedWhen]
                FROM [WideWorldImporters].[Purchasing].[PurchaseOrderLines]
                SELECT
                 [SupplierTransactionID]
                ,[SupplierID]
                ,[TransactionTypeID]
                ,[PurchaseOrderID]
                ,[PaymentMethodID]
                ,[SupplierInvoiceNumber]
                ,[TransactionDate]
                ,[AmountExcludingTax]
                ,[TaxAmount]
                ,[TransactionAmount]
                ,[OutstandingBalance]
                ,[FinalizationDate]
                ,[IsFinalized]
                ,[LastEditedBy]
                ,[LastEditedWhen]
                FROM [WideWorldImporters].[Purchasing].[SupplierTransactions]
                SELECT
                 [StockItemTransactionID]
                ,[StockItemID]
                ,[TransactionTypeID]
                ,[CustomerID]
                ,[InvoiceID]
                ,[SupplierID]
                ,[PurchaseOrderID]
                ,[TransactionOccurredWhen]
                ,[Quantity]
                ,[LastEditedBy]
                ,[LastEditedWhen]
                FROM [WideWorldImporters].[Warehouse].[StockItemTransactions]
                SELECT
                 [VehicleTemperatureID]
                ,[VehicleRegistration]
                ,[ChillerSensorNumber]
                ,[RecordedWhen]
                ,[Temperature]
                ,[FullSensorData]
                ,[IsCompressed]
                ,[CompressedSensorData]
                FROM [WideWorldImporters].[Warehouse].[VehicleTemperatures]
                ";
            //static SqlGeography? GetSqlGeography(SqlDataReader r,Int32 i) {
            //    if(r.IsDBNull(i)) {
            //        return null;
            //    } else {
            //        var SqlBytes = r.GetSqlBytes(i);
            //        using var BinaryReader = new System.IO.BinaryReader(SqlBytes.Stream,System.Text.Encoding.UTF8,true);
            //        var SqlGeography = new SqlGeography();
            //        SqlGeography.Read(BinaryReader);
            //        return SqlGeography;
            //    }
            //}
            //static String? GetString(SqlDataReader r,Int32 i) => r.IsDBNull(i) ? default : r.GetString(i);
            //static Int32? GetInt32(SqlDataReader r,Int32 i) => r.IsDBNull(i) ? default(Int32?) : r.GetInt32(i);
            //static Int64? GetInt64(SqlDataReader r,Int32 i) => r.IsDBNull(i) ? default(Int64?) : r.GetInt64(i);
            //static Decimal? GetDecimal(SqlDataReader r,Int32 i) => r.IsDBNull(i) ? default(Decimal?) : r.GetDecimal(i);
            //static DateTime? GetDateTime(SqlDataReader r,Int32 i) => r.IsDBNull(i) ? default(DateTime?) : r.GetDateTime(i);
            ////static DateTime? GetDateTimeOffset(SqlDataReader r,Int32 i) => r.IsDBNull(i) ? default(DateTime?) : r.GetDateTimeOffset(i);
            //static Boolean? GetBoolean(SqlDataReader r,Int32 i) => r.IsDBNull(i) ? default(Boolean?) : r.GetBoolean(i);
            ////static SqlGeography? GetSqlGeography(SqlDataReader r,Int32 i) => r.IsDBNull(i) ? default : (SqlGeography)r.GetValue(i);
            using var Reader = Command.ExecuteReader();
            var People = Application.People;
            while(Reader.Read()) {
                Byte[] HashedPassword7;
                if(Reader.IsDBNull(7)) {
                    HashedPassword7=Array.Empty<Byte>();
                } else {
                    HashedPassword7=new Byte[100];
                    Reader.GetBytes(7,0,HashedPassword7,0,HashedPassword7.Length);
                }

                Byte[] HashedPassword15;
                if(Reader.IsDBNull(15)) {
                    HashedPassword15=Array.Empty<Byte>();
                } else {
                    HashedPassword15=new Byte[100];
                    Reader.GetBytes(7,0,HashedPassword15,0,HashedPassword15.Length);
                }
                People.AddOrThrow(
                    new People(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetString(2),
                        Reader.GetString(3),
                        Reader.GetBoolean(4),
                        GetString(Reader,5),
                        Reader.GetBoolean(6),
                        HashedPassword7,
                        Reader.GetBoolean(8),
                        Reader.GetBoolean(9),
                        Reader.GetBoolean(10),
                        GetString(Reader,11),
                        GetString(Reader,12),
                        GetString(Reader,13),
                        GetString(Reader,14),
                        HashedPassword15,
                        GetString(Reader,16),
                        GetString(Reader,17),
                        Reader.GetInt32(18),
                        Reader.GetDateTime(19),
                        Reader.GetDateTime(20)
                    )
                );
            }
            var Countries = Application.Countries;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                Countries.AddOrThrow(
                    new Countries(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetString(2),
                        GetString(Reader,3),
                        GetInt32(Reader,4),
                        GetString(Reader,5),
                        GetInt64(Reader,6),
                        Reader.GetString(7),
                        Reader.GetString(8),
                        Reader.GetString(9),
                        GetSqlGeography(Reader,10),
                        Reader.GetInt32(11),
                        Reader.GetDateTime(12),
                        Reader.GetDateTime(13)
                    )
                );
            }
            var Countries_Archive = Application.Countries_Archive;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                Countries_Archive.AddOrThrow(
                    new Countries_Archive(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetString(2),
                        GetString(Reader,3),
                        GetInt32(Reader,4),
                        GetString(Reader,5),
                        GetInt64(Reader,6),
                        Reader.GetString(7),
                        Reader.GetString(8),
                        Reader.GetString(9),
                        GetSqlGeography(Reader,10),
                        Reader.GetInt32(11),
                        Reader.GetDateTime(12),
                        Reader.GetDateTime(13)
                    )
                );
            }
            var DeliveryMethods = Application.DeliveryMethods;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                DeliveryMethods.AddOrThrow(
                    new DeliveryMethods(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetInt32(2),
                        Reader.GetDateTime(3),
                        Reader.GetDateTime(4)
                    )
                );
            }
            var PaymentMethods = Application.PaymentMethods;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                PaymentMethods.AddOrThrow(
                    new PaymentMethods(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetInt32(2),
                        Reader.GetDateTime(3),
                        Reader.GetDateTime(4)
                    )
                );
            }
            var TransactionTypes = Application.TransactionTypes;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                TransactionTypes.AddOrThrow(
                    new TransactionTypes(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetInt32(2),
                        Reader.GetDateTime(3),
                        Reader.GetDateTime(4)
                    )
                );
            }
            var TransactionTypes_Archive = Application.TransactionTypes_Archive;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                TransactionTypes_Archive.AddOrThrow(
                    new TransactionTypes_Archive(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetInt32(2),
                        Reader.GetDateTime(3),
                        Reader.GetDateTime(4)
                    )
                );
            }
            var SupplierCategories = Purchasing.SupplierCategories;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                SupplierCategories.AddOrThrow(
                    new SupplierCategories(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetInt32(2),
                        Reader.GetDateTime(3),
                        Reader.GetDateTime(4)
                    )
                );
            }
            var BuyingGroups = Sales.BuyingGroups;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                BuyingGroups.AddOrThrow(
                    new BuyingGroups(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetInt32(2),
                        Reader.GetDateTime(3),
                        Reader.GetDateTime(4)
                    )
                );
            }
            var CustomerCategories = Sales.CustomerCategories;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                CustomerCategories.AddOrThrow(
                    new CustomerCategories(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetInt32(2),
                        Reader.GetDateTime(3),
                        Reader.GetDateTime(4)
                    )
                );
            }
            var Colors = Warehouse.Colors;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                Colors.AddOrThrow(
                    new Colors(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetInt32(2),
                        Reader.GetDateTime(3),
                        Reader.GetDateTime(4)
                    )
                );
            }
            var PackageTypes = Warehouse.PackageTypes;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                PackageTypes.AddOrThrow(
                    new PackageTypes(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetInt32(2),
                        Reader.GetDateTime(3),
                        Reader.GetDateTime(4)
                    )
                );
            }
            var StockGroups = Warehouse.StockGroups;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                StockGroups.AddOrThrow(
                    new StockGroups(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetInt32(2),
                        Reader.GetDateTime(3),
                        Reader.GetDateTime(4)
                    )
                );
            }
            var StateProvinces = Application.StateProvinces;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                StateProvinces.AddOrThrow(
                    new StateProvinces(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetString(2),
                        Reader.GetInt32(3),
                        Reader.GetString(4),
                        GetSqlGeography(Reader,5),
                        GetInt64(Reader,6),
                        Reader.GetInt32(7),
                        Reader.GetDateTime(8),
                        Reader.GetDateTime(9)
                    )
                );
            }
            var Cities = Application.Cities;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                Cities.AddOrThrow(
                    new Cities(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetInt32(2),
                        GetSqlGeography(Reader,3),
                        GetInt64(Reader,4),
                        Reader.GetInt32(5),
                        Reader.GetDateTime(6),
                        Reader.GetDateTime(7)
                    )
                );
            }
            var Cities_Archive = Application.Cities_Archive;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                Cities_Archive.AddOrThrow(
                    new Cities_Archive(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetInt32(2),
                        GetSqlGeography(Reader,3),
                        GetInt64(Reader,4),
                        Reader.GetInt32(5),
                        Reader.GetDateTime(6),
                        Reader.GetDateTime(7)
                    )
                );
            }
            var SystemParameters = Application.SystemParameters;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                SystemParameters.AddOrThrow(
                    new SystemParameters(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetString(2),
                        Reader.GetInt32(3),
                        Reader.GetString(4),
                        GetSqlGeography(Reader,5),
                        Reader.GetString(6),
                        GetString(Reader,7),
                        Reader.GetInt32(8),
                        Reader.GetString(9),
                        Reader.GetString(10),
                        Reader.GetInt32(11),
                        Reader.GetDateTime(12)
                    )
                );
            }
            var Customers = Sales.Customers;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                Customers.AddOrThrow(
                    new Customers(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetInt32(2),
                        Reader.GetInt32(3),
                        GetInt32(Reader,4),
                        Reader.GetInt32(5),
                        GetInt32(Reader,6),
                        Reader.GetInt32(7),
                        Reader.GetInt32(8),
                        Reader.GetInt32(9),
                        GetDecimal(Reader,10),
                        Reader.GetDateTime(11),
                        Reader.GetDecimal(12),
                        Reader.GetBoolean(13),
                        Reader.GetBoolean(14),
                        Reader.GetInt32(15),
                        Reader.GetString(16),
                        Reader.GetString(17),
                        GetString(Reader,18),
                        GetString(Reader,19),
                        Reader.GetString(20),
                        Reader.GetString(21),
                        GetString(Reader,22),
                        Reader.GetString(23),
                        GetSqlGeography(Reader,24),
                        Reader.GetString(25),
                        GetString(Reader,26),
                        Reader.GetString(27),
                        Reader.GetInt32(28),
                        Reader.GetDateTime(29),
                        Reader.GetDateTime(30)
                    )
                );
            }
            var Suppliers = Purchasing.Suppliers;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                Suppliers.AddOrThrow(
                    new Suppliers(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetInt32(2),
                        Reader.GetInt32(3),
                        Reader.GetInt32(4),
                        GetInt32(Reader,5),
                        Reader.GetInt32(6),
                        Reader.GetInt32(7),
                        GetString(Reader,8),
                        GetString(Reader,9),
                        GetString(Reader,10),
                        GetString(Reader,11),
                        GetString(Reader,12),
                        GetString(Reader,13),
                        Reader.GetInt32(14),
                        GetString(Reader,15),
                        Reader.GetString(16),
                        Reader.GetString(17),
                        Reader.GetString(18),
                        Reader.GetString(19),
                        GetString(Reader,20),
                        Reader.GetString(21),
                        GetSqlGeography(Reader,22),
                        Reader.GetString(23),
                        GetString(Reader,24),
                        Reader.GetString(25),
                        Reader.GetInt32(26),
                        Reader.GetDateTime(27),
                        Reader.GetDateTime(28)
                    )
                );
            }
            var StockItems = Warehouse.StockItems;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                Byte[]?Photo;
                if(Reader.IsDBNull(18)) {
                    Photo=null;
                } else {
                    var str=Reader.GetString(18);
                    Photo=System.Text.Encoding.Unicode.GetBytes(str);
                }
                StockItems.AddOrThrow(
                    new StockItems(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetInt32(2),
                        GetInt32(Reader,3),
                        Reader.GetInt32(4),
                        Reader.GetInt32(5),
                        GetString(Reader,6),
                        GetString(Reader,7),
                        Reader.GetInt32(8),
                        Reader.GetInt32(9),
                        Reader.GetBoolean(10),
                        GetString(Reader,11),
                        Reader.GetDecimal(12),
                        Reader.GetDecimal(13),
                        GetDecimal(Reader,14),
                        Reader.GetDecimal(15),
                        GetString(Reader,16),
                        GetString(Reader,17),
                        Photo,
                        GetString(Reader,19),
                        GetString(Reader,20),
                        Reader.GetString(21),
                        Reader.GetInt32(22),
                        Reader.GetDateTime(23),
                        Reader.GetDateTime(24)
                    )
                );
            }
            var Orders = Sales.Orders;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                Orders.AddOrThrow(
                    new Orders(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetInt32(2),
                        GetInt32(Reader,3),
                        Reader.GetInt32(4),
                        GetInt32(Reader,5),
                        Reader.GetDateTime(6),
                        Reader.GetDateTime(7),
                        GetString(Reader,8),
                        Reader.GetBoolean(9),
                        GetString(Reader,10),
                        GetString(Reader,11),
                        GetString(Reader,12),
                        GetDateTime(Reader,13),
                        Reader.GetInt32(14),
                        Reader.GetDateTime(15)
                    )
                );
            }
            var OrderLines = Sales.OrderLines;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                OrderLines.AddOrThrow(
                    new OrderLines(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetInt32(2),
                        Reader.GetString(3),
                        Reader.GetInt32(4),
                        Reader.GetInt32(5),
                        GetDecimal(Reader,6),
                        Reader.GetDecimal(7),
                        Reader.GetInt32(8),
                        GetDateTime(Reader,9),
                        Reader.GetInt32(10),
                        Reader.GetDateTime(11)
                    )
                );
            }
            var Invoices = Sales.Invoices;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                Invoices.AddOrThrow(
                    new Invoices(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetInt32(2),
                        GetInt32(Reader,3),
                        Reader.GetInt32(4),
                        Reader.GetInt32(5),
                        Reader.GetInt32(6),
                        Reader.GetInt32(7),
                        Reader.GetInt32(8),
                        Reader.GetDateTime(9),
                        GetString(Reader,10),
                        Reader.GetBoolean(11),
                        GetString(Reader,12),
                        GetString(Reader,13),
                        GetString(Reader,14),
                        GetString(Reader,15),
                        Reader.GetInt32(16),
                        Reader.GetInt32(17),
                        GetString(Reader,18),
                        GetString(Reader,19),
                        GetString(Reader,20),
                        GetDateTime(Reader,21),
                        GetString(Reader,22),
                        Reader.GetInt32(23),
                        Reader.GetDateTime(24)
                    )
                );
            }
            var InvoiceLines = Sales.InvoiceLines;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                InvoiceLines.AddOrThrow(
                    new InvoiceLines(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetInt32(2),
                        Reader.GetString(3),
                        Reader.GetInt32(4),
                        Reader.GetInt32(5),
                        GetDecimal(Reader,6),
                        Reader.GetDecimal(7),
                        Reader.GetDecimal(8),
                        Reader.GetDecimal(9),
                        Reader.GetDecimal(10),
                        Reader.GetInt32(11),
                        Reader.GetDateTime(12)
                    )
                );
            }
            var SpecialDeals = Sales.SpecialDeals;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                SpecialDeals.AddOrThrow(
                    new SpecialDeals(
                        Reader.GetInt32(0),
                        GetInt32(Reader,1),
                        GetInt32(Reader,2),
                        GetInt32(Reader,3),
                        GetInt32(Reader,4),
                        GetInt32(Reader,5),
                        Reader.GetString(6),
                        Reader.GetDateTime(7),
                        Reader.GetDateTime(8),
                        GetDecimal(Reader,9),
                        GetDecimal(Reader,10),
                        GetDecimal(Reader,11),
                        Reader.GetInt32(12),
                        Reader.GetDateTime(13)
                    )
                );
            }
            var CustomerTransactions = Sales.CustomerTransactions;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                CustomerTransactions.AddOrThrow(
                    new CustomerTransactions(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetInt32(2),
                        GetInt32(Reader,3),
                        GetInt32(Reader,4),
                        Reader.GetDateTime(5),
                        Reader.GetDecimal(6),
                        Reader.GetDecimal(7),
                        Reader.GetDecimal(8),
                        Reader.GetDecimal(9),
                        GetDateTime(Reader,10),
                        GetBoolean(Reader,11),
                        Reader.GetInt32(12),
                        Reader.GetDateTime(13)
                    )
                );
            }
            var StockItemHoldings = Warehouse.StockItemHoldings;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                StockItemHoldings.AddOrThrow(
                    new StockItemHoldings(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetString(2),
                        Reader.GetInt32(3),
                        Reader.GetDecimal(4),
                        Reader.GetInt32(5),
                        Reader.GetInt32(6),
                        Reader.GetInt32(7),
                        Reader.GetDateTime(8)
                    )
                );
            }
            var StockItemStockGroups = Warehouse.StockItemStockGroups;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                StockItemStockGroups.AddOrThrow(
                    new StockItemStockGroups(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetInt32(2),
                        Reader.GetInt32(3),
                        Reader.GetDateTime(4)
                    )
                );
            }
            var PurchaseOrders = Purchasing.PurchaseOrders;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                PurchaseOrders.AddOrThrow(
                    new PurchaseOrders(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetDateTime(2),
                        Reader.GetInt32(3),
                        Reader.GetInt32(4),
                        GetDateTime(Reader,5),
                        GetString(Reader,6),
                        Reader.GetBoolean(7),
                        GetString(Reader,8),
                        GetString(Reader,9),
                        Reader.GetInt32(10),
                        Reader.GetDateTime(11)
                    )
                );
            }
            var PurchaseOrderLines = Purchasing.PurchaseOrderLines;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                PurchaseOrderLines.AddOrThrow(
                    new PurchaseOrderLines(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetInt32(2),
                        Reader.GetInt32(3),
                        GetString(Reader,4),
                        Reader.GetInt32(5),
                        Reader.GetInt32(6),
                        GetDecimal(Reader,7),
                        GetDateTime(Reader,8),
                        Reader.GetBoolean(9),
                        Reader.GetInt32(10),
                        Reader.GetDateTime(11)
                    )
                );
            }
            var SupplierTransactions = Purchasing.SupplierTransactions;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                SupplierTransactions.AddOrThrow(
                    new SupplierTransactions(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetInt32(2),
                        GetInt32(Reader,3),
                        GetInt32(Reader,4),
                        GetString(Reader,5),
                        Reader.GetDateTime(6),
                        Reader.GetDecimal(7),
                        Reader.GetDecimal(8),
                        Reader.GetDecimal(9),
                        Reader.GetDecimal(10),
                        GetDateTime(Reader,11),
                        GetBoolean(Reader,12),
                        Reader.GetInt32(13),
                        Reader.GetDateTime(14)
                    )
                );
            }
            var StockItemTransactions = Warehouse.StockItemTransactions;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                StockItemTransactions.AddOrThrow(
                    new StockItemTransactions(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetInt32(2),
                        GetInt32(Reader,3),
                        GetInt32(Reader,4),
                        GetInt32(Reader,5),
                        GetInt32(Reader,6),
                        Reader.GetDateTime(7),
                        Reader.GetDecimal(8),
                        Reader.GetInt32(9),
                        Reader.GetDateTime(10)
                    )
                );
            }
            var VehicleTemperatures = Warehouse.VehicleTemperatures;
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                Byte[]? CompressedSensorData;
                if(Reader.IsDBNull(7)) {
                    CompressedSensorData=null;
                } else {
                    var str = Reader.GetString(7);
                    CompressedSensorData=System.Text.Encoding.Unicode.GetBytes(str);
                }
                VehicleTemperatures.AddOrThrow(
                    new VehicleTemperatures(
                        Reader.GetInt64(0),
                        Reader.GetString(1),
                        Reader.GetInt32(2),
                        Reader.GetDateTime(3),
                        Reader.GetDecimal(4),
                        GetString(Reader,5),
                        Reader.GetBoolean(6),
                        CompressedSensorData
                    )
                );
            }
        }
        Customers(e,Command);
        Suppliers(e,Command);
        VehicleTemperatures(e,Command);
        GetCityUpdates(e,Command);
        e.Clear();
        Console.WriteLine($"Load {s.ElapsedMilliseconds,7}ms");
    }
    private static void Customers(Container e,SqlCommand Command) {
        {
            var r = (
                from s in e.Sales.Customers
                join sc in e.Sales.CustomerCategories on s.CustomerCategoryID equals sc.CustomerCategoryID into sc0
                from sc in sc0.DefaultIfEmpty()
                join pp in e.Application.People on s.PrimaryContactPersonID equals pp.PersonID into pp0
                from pp in pp0.DefaultIfEmpty()
                join ap in e.Application.People on s.AlternateContactPersonID equals ap.PersonID into ap0
                from ap in ap0.DefaultIfEmpty()
                join bg in e.Sales.BuyingGroups on s.BuyingGroupID equals bg.BuyingGroupID into bg0
                from bg in bg0.DefaultIfEmpty()
                join dm in e.Application.DeliveryMethods on s.DeliveryMethodID equals dm.DeliveryMethodID into dm0
                from dm in dm0.DefaultIfEmpty()
                join c in e.Application.Cities on s.DeliveryCityID equals c.CityID into c0
                from c in c0.DefaultIfEmpty()
                select new {
                    s.CustomerID,
                    s.CustomerName,
                    CustomerCategoryName=sc?.CustomerCategoryName,
                    FullName= pp?.FullName,
                    AlternateContact = ap?.FullName,
                    PhoneNumber=s?.PhoneNumber,
                    FaxNumber = s?.FaxNumber,
                    BuyingGroupName = bg?.BuyingGroupName,
                    WebsiteURL=s?.WebsiteURL,
                    DeliveryMethod = dm?.DeliveryMethodName,
                    CityName=c?.CityName,
                    DeliveryLocation=s?.DeliveryLocation,
                    DeliveryRun=s?.DeliveryRun,
                    RunPosition=s?.RunPosition
                }
            ).ToArray();
        }
        比較(
            () =>
                from s in e.Sales.Customers
                join sc in e.Sales.CustomerCategories on s.CustomerCategoryID equals sc.CustomerCategoryID into sc0
                from sc in sc0.DefaultIfEmpty()
                join pp in e.Application.People on s.PrimaryContactPersonID equals pp.PersonID into pp0
                from pp in pp0.DefaultIfEmpty()
                join ap in e.Application.People on s.AlternateContactPersonID equals ap.PersonID into ap0
                from ap in ap0.DefaultIfEmpty()
                join bg in e.Sales.BuyingGroups on s.BuyingGroupID equals bg.BuyingGroupID into bg0
                from bg in bg0.DefaultIfEmpty()
                join dm in e.Application.DeliveryMethods on s.DeliveryMethodID equals dm.DeliveryMethodID into dm0
                from dm in dm0.DefaultIfEmpty()
                join c in e.Application.Cities on s.DeliveryCityID equals c.CityID into c0
                from c in c0.DefaultIfEmpty()
                select new {
                    s.CustomerID,
                    s.CustomerName,
                    CustomerCategoryName = sc==null ? null : sc.CustomerCategoryName,
                    PrimaryContact = pp==null ? null : pp.FullName,
                    AlternateContact = ap==null ? null : ap.FullName,
                    PhoneNumber = s==null ? null : s.PhoneNumber,
                    FaxNumber = s==null ? null : s.FaxNumber,
                    BuyingGroupName = bg==null ? null : bg.BuyingGroupName,
                    WebsiteURL = s==null ? null : s.WebsiteURL,
                    DeliveryMethod = dm==null ? null : dm.DeliveryMethodName,
                    CityName = c==null ? null : c.CityName,
                    DeliveryLocation = s==null ? null : s.DeliveryLocation,
                    DeliveryRun = s==null ? null : s.DeliveryRun,
                    RunPosition = s==null ? null : s.RunPosition
                },
            Command,
            "SELECT * FROM Website.Customers",
            Reader => new {
                CustomerID = Reader.GetInt32(0),
                CustomerName = Reader.GetString(1),
                CustomerCategoryName = GetString(Reader,2),
                PrimaryContact = GetString(Reader,3),
                AlternateContact = GetString(Reader,4),
                PhoneNumber = Reader.GetString(5),
                FaxNumber = Reader.GetString(6),
                BuyingGroupName = GetString(Reader,7),
                WebsiteURL = Reader.GetString(8),
                DeliveryMethod = GetString(Reader,9),
                CityName = GetString(Reader,10),
                DeliveryLocation = GetSqlGeography(Reader,11),
                DeliveryRun = GetString(Reader,12),
                RunPosition = GetString(Reader,13),
            });
    }
    private static void Suppliers(Container e,SqlCommand Command) {
        {
            var r = (
                from s in e.Purchasing.Suppliers
                join sc in e.Purchasing.SupplierCategories on s.SupplierCategoryID equals sc.SupplierCategoryID into sc0
                from sc in sc0.DefaultIfEmpty()
                join pp in e.Application.People on s.PrimaryContactPersonID equals pp.PersonID into pp0
                from pp in pp0.DefaultIfEmpty()
                join ap in e.Application.People on s.AlternateContactPersonID equals ap.PersonID into ap0
                from ap in ap0.DefaultIfEmpty()
                join dm in e.Application.DeliveryMethods on s.DeliveryMethodID equals dm.DeliveryMethodID into dm0
                from dm in dm0.DefaultIfEmpty()
                join c in e.Application.Cities on s.DeliveryCityID equals c.CityID into c0
                from c in c0.DefaultIfEmpty()
                select new {
                    s.SupplierID,
                    s.SupplierName,
                    SupplierCategoryName = sc?.SupplierCategoryName,
                    PrimaryContact = pp?.FullName,
                    AlternateContact = ap?.FullName,
                    PhoneNumber = s?.PhoneNumber,
                    FaxNumber = s?.FaxNumber,
                    WebsiteURL = s?.WebsiteURL,
                    DeliveryMethod = dm?.DeliveryMethodName,
                    CityName = c?.CityName,
                    DeliveryLocation = s?.DeliveryLocation,
                    SupplierReference = s?.SupplierReference
                }
            ).ToArray();
        }
        比較(
            () =>
                from s in e.Purchasing.Suppliers
                join sc in e.Purchasing.SupplierCategories on s.SupplierCategoryID equals sc.SupplierCategoryID into sc0
                from sc in sc0.DefaultIfEmpty()
                join pp in e.Application.People on s.PrimaryContactPersonID equals pp.PersonID into pp0
                from pp in pp0.DefaultIfEmpty()
                join ap in e.Application.People on s.AlternateContactPersonID equals ap.PersonID into ap0
                from ap in ap0.DefaultIfEmpty()
                join dm in e.Application.DeliveryMethods on s.DeliveryMethodID equals dm.DeliveryMethodID into dm0
                from dm in dm0.DefaultIfEmpty()
                join c in e.Application.Cities on s.DeliveryCityID equals c.CityID into c0
                from c in c0.DefaultIfEmpty()
                select new {
                    s.SupplierID,
                    s.SupplierName,
                    SupplierCategoryName = sc==null ? null : sc.SupplierCategoryName,
                    PrimaryContact = pp==null ? null : pp.FullName,
                    AlternateContact = ap==null ? null : ap.FullName,
                    PhoneNumber = s==null ? null : s.PhoneNumber,
                    FaxNumber = s==null ? null : s.FaxNumber,
                    WebsiteURL = s==null ? null : s.WebsiteURL,
                    DeliveryMethod = dm==null ? null : dm.DeliveryMethodName,
                    CityName = c==null ? null : c.CityName,
                    DeliveryLocation = s==null ? null : s.DeliveryLocation,
                    SupplierReference = s==null ? null : s.SupplierReference
                },
            Command,
            "SELECT * FROM Website.Suppliers",
            Reader => new {
                SupplierID = Reader.GetInt32(0),
                SupplierName = Reader.GetString(1),
                SupplierCategoryName = GetString(Reader,2),
                PrimaryContact = GetString(Reader,3),
                AlternateContact = GetString(Reader,4),
                PhoneNumber = Reader.GetString(5),
                FaxNumber = Reader.GetString(6),
                WebsiteURL = Reader.GetString(7),
                DeliveryMethod = GetString(Reader,8),
                CityName = GetString(Reader,9),
                DeliveryLocation = GetSqlGeography(Reader,10),
                SupplierReference= GetString(Reader,11),
            });
    }
    private static void VehicleTemperatures(Container e,SqlCommand Command) {
        {
            var r = (
                from vt in e.Warehouse.VehicleTemperatures
                select new {
                    vt.VehicleTemperatureID,
                    vt.VehicleRegistration,
                    vt.ChillerSensorNumber,
                    vt.RecordedWhen,
                    vt.Temperature,
                    FullSensorData = vt.IsCompressed
                        ? vt.CompressedSensorData.ToString()
                        : vt.FullSensorData,
                }
            ).ToArray();
        }
        比較(
            () =>
                from vt in e.Warehouse.VehicleTemperatures
                select new {
                    vt.VehicleTemperatureID,
                    vt.VehicleRegistration,
                    vt.ChillerSensorNumber,
                    vt.RecordedWhen,
                    vt.Temperature,
                    FullSensorData = vt.IsCompressed
                        ? vt.CompressedSensorData.ToString()
                        : vt.FullSensorData,
                },
            Command,
            "SELECT * FROM Website.VehicleTemperatures",
            Reader => new {
                VehicleTemperatureID = Reader.GetInt64(0),
                VehicleRegistration = Reader.GetString(1),
                ChillerSensorNumber = Reader.GetInt32(2),
                RecordedWhen = Reader.GetDateTime(3),
                Temperature = Reader.GetDecimal(4),
                FullSensorData = GetString(Reader,5),
            });
    }
    private static void GetCityUpdates(Container e,SqlCommand Command) {
        var CityChanges = new Set<(
            Int32 WWI_City_ID,
            String City,
            String State_Province,
            String Country,
            String Continent,
            String Sales_Territory,
            String Region,
            String Subregion,
            SqlGeography Location,
            Int64 Latest_Recorded_Population,
            DateTime Valid_From,
            DateTime? Valid_To
            )>();
        {
            DateTime LastCutoff=new DateTime(2012,1,1);
            DateTime NewCutoff = new DateTime(2020,1,1);
            DateTime EndOfTime=DateTime.MaxValue;
            DateTime InitialLoadDate = new DateTime(2013,1,1);
            var CountryChangeList=(
                from co in e.Application.Countries_Archive
                where co.ValidFrom>LastCutoff
                      &&co.ValidFrom<=NewCutoff
                      &&co.ValidFrom<InitialLoadDate
                select new { co.CountryID,co.ValidFrom }
            ).Union(
                from co in e.Application.Countries
                where co.ValidFrom>LastCutoff
                      &&co.ValidFrom<=NewCutoff
                      &&co.ValidFrom<InitialLoadDate
                select new { co.CountryID,co.ValidFrom }
            );
            foreach(var CountryChange in CountryChangeList) { 
                var(CountryID, ValidFrom)=(CountryChange.CountryID, CountryChange.ValidFrom);
                var source = (
                    from c in e.Application.Cities
                    join sp in e.Application.StateProvinces on c.StateProvinceID equals sp.StateProvinceID
                    join co in e.Application.Countries on sp.CountryID equals co.CountryID
                    where co.CountryID==CountryID
                    select (
                        WWI_City_ID : c.CityID,
                        City : c.CityName,
                        State_Province : sp.StateProvinceName,
                        Country : co.CountryName,
                        co.Continent,
                        Sales_Territory : sp.SalesTerritory,
                        co.Region,
                        co.Subregion,
                        c.Location,
                        Latest_Recorded_Population : c.LatestRecordedPopulation??0,
                        Valid_From : ValidFrom,
                        Valid_To : default(DateTime?)
                    )
                );
                CityChanges.AddRange(source);
            }
            var StateProvinceChangeList = (
                from sp in e.Application.StateProvinces_Archive
                where sp.ValidFrom>LastCutoff
                      &&sp.ValidFrom<=NewCutoff
                      &&sp.ValidFrom<InitialLoadDate
                select new { sp.StateProvinceID,sp.ValidFrom }
            ).Union(
                from sp in e.Application.StateProvinces_Archive
                where sp.ValidFrom>LastCutoff
                      &&sp.ValidFrom<=NewCutoff
                      &&sp.ValidFrom<InitialLoadDate
                select new { sp.StateProvinceID,sp.ValidFrom }
            );
            foreach(var StateProvinceChange in StateProvinceChangeList) {
                var (StateProvinceID, ValidFrom)=(StateProvinceChange.StateProvinceID, StateProvinceChange.ValidFrom);
                var source = (
                    from c in e.Application.Cities
                    join sp in e.Application.StateProvinces on c.StateProvinceID equals sp.StateProvinceID
                    join co in e.Application.Countries on sp.CountryID equals co.CountryID
                    where sp.StateProvinceID==StateProvinceID
                    select (
                        WWI_City_ID: c.CityID,
                        City: c.CityName,
                        State_Province: sp.StateProvinceName,
                        Country: co.CountryName,
                        co.Continent,
                        Sales_Territory: sp.SalesTerritory,
                        co.Region,
                        co.Subregion,
                        c.Location,
                        Latest_Recorded_Population: c.LatestRecordedPopulation??0,
                        Valid_From: ValidFrom,
                        Valid_To: default(DateTime?)
                    )
                );
                CityChanges.AddRange(source);
            }
            var CityChangeList = (
                from c in e.Application.Cities_Archive
                where c.ValidFrom>LastCutoff
                      &&c.ValidFrom<=NewCutoff
                select new { c.CityID,c.ValidFrom }
            ).Union(
                from c in e.Application.Cities_Archive
                where c.ValidFrom>LastCutoff
                      &&c.ValidFrom<=NewCutoff
                select new { c.CityID,c.ValidFrom }
            );
            foreach(var CityChange in CityChangeList) {
                var (CityID, ValidFrom)=(CityChange.CityID, CityChange.ValidFrom);
                var source = (
                    from c in e.Application.Cities
                    join sp in e.Application.StateProvinces on c.StateProvinceID equals sp.StateProvinceID
                    join co in e.Application.Countries on sp.CountryID equals co.CountryID
                    where c.CityID==CityID
                    select (
                        WWI_City_ID: c.CityID,
                        City: c.CityName,
                        State_Province: sp.StateProvinceName,
                        Country: co.CountryName,
                        co.Continent,
                        Sales_Territory: sp.SalesTerritory,
                        co.Region,
                        co.Subregion,
                        c.Location,
                        Latest_Recorded_Population: c.LatestRecordedPopulation??0,
                        Valid_From: ValidFrom,
                        Valid_To: default(DateTime?)
                    )
                );
                CityChanges.AddRange(source);
            }
            CityChanges.UpdateWith(
                cc => {
                    cc.Valid_To=(
                        from cc2 in CityChanges
                        where cc2.WWI_City_ID==cc.WWI_City_ID
                              &&cc2.Valid_From>cc.Valid_From
                        select cc2
                    ).Min(cc2 => (DateTime?)cc2.Valid_From)??EndOfTime;
                    return cc;
                },
                _ => true);
        }
        比較(
            () =>
                from vt in e.Warehouse.VehicleTemperatures
                select new {
                    vt.VehicleTemperatureID,
                    vt.VehicleRegistration,
                    vt.ChillerSensorNumber,
                    vt.RecordedWhen,
                    vt.Temperature,
                    FullSensorData = vt.IsCompressed
                        ? vt.CompressedSensorData.ToString()
                        : vt.FullSensorData,
                },
            Command,
            "SELECT * FROM Website.VehicleTemperatures",
            Reader => new {
                VehicleTemperatureID = Reader.GetInt64(0),
                VehicleRegistration = Reader.GetString(1),
                ChillerSensorNumber = Reader.GetInt32(2),
                RecordedWhen = Reader.GetDateTime(3),
                Temperature = Reader.GetDecimal(4),
                FullSensorData = GetString(Reader,5),
            });
    }
    private static void Main() {
        Load();
        Create();
        var r = new Random(2);
        Transaction(() => r.Next());
        var index = 0;
        Transaction(() => index++);
#pragma warning disable CA1303 // ローカライズされるパラメーターとしてリテラルを渡さない
        Console.Write("終了");
#pragma warning restore CA1303 // ローカライズされるパラメーターとしてリテラルを渡さない
        Console.ReadKey();
    }
}