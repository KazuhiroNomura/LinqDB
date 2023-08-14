using System;
using System.Diagnostics;
using System.Spatial;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Microsoft.SqlServer.Types;
//using LinqDB.Databases;
using LinqDB.Sets;
using WideWorldImportersDW.Schemas;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using WideWorldImportersDW;
using WideWorldImportersDW.Tables.Fact;
using WideWorldImportersDW.Tables.Dimension;
using WideWorldImportersDW.Tables.Integration;

namespace WideWorldImportersDW {
    public abstract class Sequence<T> where T : struct {
        private readonly T Minimum, Maximum;
        private readonly Boolean Cycle;
        private protected T ProtectedCurrent;
        protected Sequence(T Minimum,T Maximum,Boolean Cycle) {
            this.Minimum=Minimum;
            this.Maximum=Maximum;
            this.Cycle=Cycle;
            this.ProtectedCurrent=Minimum;
        }
        public abstract T Current { get; }
    }
    namespace Sequences {
        public sealed class SequenceInt32:Sequence<Int32> {
            public SequenceInt32(Int32 Minimum,Int32 Maximum,Boolean Cycle) : base(Minimum,Maximum,Cycle) {
            }
            public override Int32 Current => this.ProtectedCurrent++;
        }
    }
    namespace Schemas {
        class Sequences2 {
            public readonly WideWorldImportersDW.Sequences.SequenceInt32 CityKey = new(Int32.MinValue,Int32.MaxValue,false);
            public readonly WideWorldImportersDW.Sequences.SequenceInt32 CustomerKey = new(Int32.MinValue,Int32.MaxValue,false);
            public readonly WideWorldImportersDW.Sequences.SequenceInt32 EmployeeKey = new(Int32.MinValue,Int32.MaxValue,false);
            public readonly WideWorldImportersDW.Sequences.SequenceInt32 LineageKey = new(Int32.MinValue,Int32.MaxValue,false);
            public readonly WideWorldImportersDW.Sequences.SequenceInt32 PaymentMethodKey = new(Int32.MinValue,Int32.MaxValue,false);
            public readonly WideWorldImportersDW.Sequences.SequenceInt32 StockItemKey = new(Int32.MinValue,Int32.MaxValue,false);
            public readonly WideWorldImportersDW.Sequences.SequenceInt32 SupplierKey = new(Int32.MinValue,Int32.MaxValue,false);
            public readonly WideWorldImportersDW.Sequences.SequenceInt32 TransactionTypeKey = new(Int32.MinValue,Int32.MaxValue,false);
        }
    }
}
namespace TestWideWorldImportersDW{
    class Container2:Container{
        public readonly Sequences2 Sequences2 = new();
    }
    [SuppressMessage("Style","IDE0060:未使用のパラメーターを削除します",Justification="<保留中>")]
    abstract class Program:共通{
        [SuppressMessage("Globalization","CA1305:IFormatProvider を指定します",Justification="<保留中>")]
        [SuppressMessage("Globalization","CA1303:ローカライズされるパラメーターとしてリテラルを渡さない",Justification="<保留中>")]
        [SuppressMessage("ReSharper","VariableHidesOuterVariable")]
        private static void Create(){
            const Int32 City数=4;
            const Int32 Customer数=8;
            const Int32 Date数=16;
            using var e=new Container();
            var Fact=e.Fact;
            var Dimension=e.Dimension;
            var Integration=e.Integration;
            var City=Dimension.City;
            var Customer=Dimension.Customer;
            var Date=Dimension.Date;
            var Payment_Method=Dimension.Payment_Method;
            var Transaction_Type=Dimension.Transaction_Type;
            var Supplier=Dimension.Supplier;
            var Stock_Item=Dimension.Stock_Item;
            var Employee=Dimension.Employee;
            var Movement=Fact.Movement;
            var Order=Fact.Order;
            var Purchase=Fact.Purchase;
            var Sale=Fact.Sale;
            var Stock_Holding=Fact.Stock_Holding;
            var Transaction=Fact.Transaction;
            var Customer_Staging=Integration.Customer_Staging;
            var City_Staging=Integration.City_Staging;
            var ETL_Cutoff=Integration.ETL_Cutoff;
            var Employee_Staging=Integration.Employee_Staging;
            var Lineage=Integration.Lineage;
            var Movement_Staging=Integration.Movement_Staging;
            var Order_Staging=Integration.Order_Staging;
            var PaymentMethod_Staging=Integration.PaymentMethod_Staging;
            var Purchase_Staging=Integration.Purchase_Staging;
            var Sale_Staging=Integration.Sale_Staging;
            var StockHolding_Staging=Integration.StockHolding_Staging;
            var StockItem_Staging=Integration.StockItem_Staging;
            var Supplier_Staging=Integration.Supplier_Staging;
            var TransactionType_Staging=Integration.TransactionType_Staging;
            var Transaction_Staging=Integration.Transaction_Staging;
            var DateTime起点=new DateTime(2001,1,1);
            //var Geography = System.Spatial.GeographyPoint.Create(0,0);
            //Geography Geography=GeographyPoint.Create(90,35,4326);
            var Geography=new SqlGeography();
            var s=Stopwatch.StartNew();
            //LV0
            for(var Customer_Starting_Key=0;Customer_Starting_Key<2;Customer_Starting_Key++){
                Customer_Staging.AddOrThrow(
                    new Customer_Staging(
                        Customer_Starting_Key,
                        0,
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        DateTime起点,
                        DateTime起点
                    )
                );
            }

            for(var City_Starting_Key=0;City_Starting_Key<2;City_Starting_Key++){
                City_Staging.AddOrThrow(
                    new City_Staging(
                        City_Starting_Key,
                        0,
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        Geography,
                        0,
                        DateTime起点,
                        DateTime起点
                    )
                );
            }

            for(var Table_Name=0;Table_Name<2;Table_Name++){
                ETL_Cutoff.AddOrThrow(
                    new ETL_Cutoff(
                        Table_Name.ToString(),
                        DateTime起点
                    )
                );
            }

            for(var Employee_Starting_Key=0;Employee_Starting_Key<2;Employee_Starting_Key++){
                Employee_Staging.AddOrThrow(
                    new Employee_Staging(
                        Employee_Starting_Key,
                        0,
                        "",
                        "",
                        false,
                        null,
                        DateTime起点,
                        DateTime起点
                    )
                );
            }

            for(var Lineage_Key=0;Lineage_Key<2;Lineage_Key++){
                Lineage.AddOrThrow(
                    new Lineage(
                        Lineage_Key,
                        DateTime起点,
                        "",
                        null,
                        false,
                        DateTime起点
                    )
                );
            }

            for(var Movement_Staging_Key=0;Movement_Staging_Key<2;Movement_Staging_Key++){
                Movement_Staging.AddOrThrow(
                    new Movement_Staging(
                        Movement_Staging_Key,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null
                    )
                );
            }

            for(var Order_Staging_Key=0;Order_Staging_Key<2;Order_Staging_Key++){
                Order_Staging.AddOrThrow(
                    new Order_Staging(
                        Order_Staging_Key,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null
                    )
                );
            }

            for(var PaymentMethod_Staging_Key=0;PaymentMethod_Staging_Key<2;PaymentMethod_Staging_Key++){
                PaymentMethod_Staging.AddOrThrow(
                    new PaymentMethod_Staging(
                        PaymentMethod_Staging_Key,
                        0,
                        "",
                        DateTime起点,
                        DateTime起点
                    )
                );
            }

            for(var Purchase_Staging_Key=0;Purchase_Staging_Key<2;Purchase_Staging_Key++){
                Purchase_Staging.AddOrThrow(
                    new Purchase_Staging(
                        Purchase_Staging_Key,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null
                    )
                );
            }

            for(var Sale_Staging_Key=0;Sale_Staging_Key<2;Sale_Staging_Key++){
                Sale_Staging.AddOrThrow(
                    new Sale_Staging(
                        Sale_Staging_Key,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null
                    )
                );
            }

            for(var StockHolding_Staging_Key=0;StockHolding_Staging_Key<2;StockHolding_Staging_Key++){
                StockHolding_Staging.AddOrThrow(
                    new StockHolding_Staging(
                        StockHolding_Staging_Key,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null
                    )
                );
            }

            for(var StockItem_Staging_Key=0;StockItem_Staging_Key<2;StockItem_Staging_Key++){
                StockItem_Staging.AddOrThrow(
                    new StockItem_Staging(
                        StockItem_Staging_Key,
                        0,"",
                        "","",
                        "",
                        "",
                        "",
                        0,
                        0,
                        false,
                        null,
                        0,
                        0,
                        null,
                        0,
                        null,
                        DateTime起点,
                        DateTime起点
                    )
                );
            }

            for(var Supplier_Staging_Key=0;Supplier_Staging_Key<2;Supplier_Staging_Key++){
                Supplier_Staging.AddOrThrow(
                    new Supplier_Staging(
                        Supplier_Staging_Key,
                        0,
                        "",
                        "",
                        "",
                        null,
                        0,
                        "",
                        DateTime起点,
                        DateTime起点
                    )
                );
            }

            for(var TransactionType_Staging_Key=0;TransactionType_Staging_Key<2;TransactionType_Staging_Key++){
                TransactionType_Staging.AddOrThrow(
                    new TransactionType_Staging(
                        TransactionType_Staging_Key,
                        0,
                        "",
                        DateTime起点,
                        DateTime起点
                    )
                );
            }

            for(var Transaction_Staging_Key=0;Transaction_Staging_Key<2;Transaction_Staging_Key++){
                Transaction_Staging.AddOrThrow(
                    new Transaction_Staging(
                        Transaction_Staging_Key,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null
                    )
                );
            }

            for(var City_Key=0;City_Key<City数;City_Key++){
                City.AddOrThrow(
                    new City(
                        City_Key,
                        0,
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        Geography,
                        0,
                        DateTime起点,
                        DateTime起点,
                        0
                    )
                );
            }

            for(var Customer_Key=0;Customer_Key<Customer数;Customer_Key++){
                Customer.AddOrThrow(
                    new Customer(
                        Customer_Key,
                        0,
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        DateTime起点,
                        DateTime起点,
                        0
                    )
                );
            }

            for(var Key=0;Key<Date数;Key++){
                Date.AddOrThrow(
                    new Date(
                        new DateTime(2001,1,1).AddDays(Key),
                        0,
                        "",
                        "",
                        "",
                        0,
                        "",
                        0,
                        "",
                        0,"",
                        0,
                        "",
                        0

                    )
                );
            }

            for(var Payment_Method_Key=0;Payment_Method_Key<2;Payment_Method_Key++){
                Payment_Method.AddOrThrow(
                    new Payment_Method(
                        Payment_Method_Key,
                        0,
                        "",
                        DateTime起点,
                        DateTime起点,
                        0
                    )
                );
            }

            for(var Key=0;Key<2;Key++){
                Transaction_Type.AddOrThrow(
                    new Transaction_Type(
                        Key,
                        0,
                        "",
                        DateTime起点,
                        DateTime起点,
                        0
                    )
                );
            }

            for(var SupplierKey=0;SupplierKey<2;SupplierKey++){
                Supplier.AddOrThrow(
                    new Supplier(
                        SupplierKey,
                        0,
                        "",
                        "",
                        "",
                        "",
                        0,
                        "",
                        DateTime起点,
                        DateTime起点,
                        0
                    )
                );
            }

            for(var Stock_Item_Key=0;Stock_Item_Key<2;Stock_Item_Key++){
                Stock_Item.AddOrThrow(
                    new Stock_Item(
                        Stock_Item_Key,
                        0,
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        0,
                        0,
                        false,
                        "",
                        0,
                        0,
                        0,
                        0,
                        Array.Empty<Byte>(),
                        DateTime起点,
                        DateTime起点,
                        0
                    )
                );
            }

            for(var EmployeeKey=0;EmployeeKey<2;EmployeeKey++){
                Employee.AddOrThrow(
                    new Employee(
                        EmployeeKey,
                        0,
                        "",
                        "",
                        false,
                        Array.Empty<Byte>(),
                        DateTime起点,
                        DateTime起点,
                        0
                    )
                );
            }

            var Movement_Key=0L;
            E(
                Date,
                Date=>E(
                    Stock_Item,
                    Stock_Item=>E(
                        Customer,
                        Customer=>E(
                            Supplier,
                            Supplier=>E(
                                Transaction_Type,
                                Transaction_Type=>Movement.AddOrThrow(
                                    new Movement(
                                        Movement_Key++,
                                        Date.Date,
                                        Stock_Item.Stock_Item_Key,
                                        Customer.Customer_Key,
                                        Supplier.Supplier_Key,
                                        Transaction_Type.Transaction_Type_Key,
                                        0,
                                        0,
                                        0,
                                        0,
                                        0
                                    )
                                )
                            )
                        )
                    )
                )
            );
            var Order_Key=0;
            E(
                City,
                City=>E(
                    Customer,
                    Customer=>E(
                        Stock_Item,
                        Stock_Item=>E(
                            Date,
                            Order_Date_Key=>E(
                                Date,
                                Picked_Date_Key=>E(
                                    Employee,
                                    Salesperson_Key=>E(
                                        Employee,
                                        Picker_Key=>Order.AddOrThrow(
                                            new Order(
                                                Order_Key++,
                                                City.City_Key,
                                                Customer.Customer_Key,
                                                Stock_Item.Stock_Item_Key,
                                                Order_Date_Key.Date,
                                                Picked_Date_Key.Date,
                                                Salesperson_Key.Employee_Key,
                                                Picker_Key.Employee_Key,
                                                0,
                                                0,
                                                "",
                                                "",
                                                0,
                                                0,
                                                0,
                                                0,
                                                0,
                                                0,
                                                0
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    )
                )
            );
            var Purchase_Key=0L;
            E(
                Date,
                Date=>E(
                    Supplier,
                    Supplier=>E(
                        Stock_Item,
                        Stock_Item=>Purchase.AddOrThrow(
                            new Purchase(
                                Purchase_Key++,
                                Date.Date,
                                Supplier.Supplier_Key,
                                Stock_Item.Stock_Item_Key,
                                0,
                                0,
                                0,
                                0,
                                "",
                                false,
                                0
                            )
                        )
                    )
                )
            );
            var Sale_Key=0L;
            E(
                City,
                City=>E(
                    Customer,
                    Customer_Key=>E(
                        Customer,
                        Bill_To_Customer_Key=>E(
                            Stock_Item,
                            Stock_Item=>E(
                                Date,
                                Invoice_Date=>E(
                                    Date,
                                    Delivery_Date=>E(
                                        Employee,
                                        Salesperson_Key=>Sale.AddOrThrow(
                                            new Sale(
                                                Sale_Key++,
                                                City.City_Key,
                                                Customer_Key.Customer_Key,
                                                Bill_To_Customer_Key.Customer_Key,
                                                Stock_Item.Stock_Item_Key,
                                                Invoice_Date.Date,
                                                Delivery_Date.Date,
                                                Salesperson_Key.Employee_Key,
                                                0,
                                                "",
                                                "",
                                                0,
                                                0,
                                                0,
                                                0,
                                                0,
                                                0,
                                                0,
                                                0,
                                                0,
                                                0
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    )
                )
            );
            var Stock_Holding_Key=0L;
            E(
                Stock_Item,
                Stock_Item=>Stock_Holding.AddOrThrow(
                    new Stock_Holding(
                        Stock_Holding_Key++,
                        Stock_Item.Stock_Item_Key,
                        0,
                        "",
                        0,
                        0,
                        0,
                        0,
                        0
                    )
                )
            );
            var Transaction_Key=0L;
            E(
                Date,
                Date=>E(
                    Customer,
                    Customer_Key=>E(
                        Customer,
                        Bill_To_Customer_Key=>E(
                            Supplier,
                            Supplier=>E(
                                Transaction_Type,
                                Transaction_Type=>E(
                                    Payment_Method,
                                    Payment_Method=>Transaction.AddOrThrow(
                                        new Transaction(
                                            Transaction_Key++,
                                            Date.Date,
                                            Customer_Key.Customer_Key,
                                            Bill_To_Customer_Key.Customer_Key,
                                            Supplier.Supplier_Key,
                                            Transaction_Type.Transaction_Type_Key,
                                            Payment_Method.Payment_Method_Key,
                                            0,
                                            0,
                                            0,
                                            0,
                                            "",
                                            0,
                                            0,
                                            0,
                                            0,
                                            false,
                                            0
                                        )
                                    )
                                )
                            )
                        )
                    )
                )
            );
            Console.WriteLine($"Create {s.ElapsedMilliseconds,7}ms");
        }

        [SuppressMessage("Globalization","CA1305:IFormatProvider を指定します",Justification="<保留中>")]
        private static void Transaction(Func<Int32> Switchパターン){
            const Int32 試行回数=10000;
            using var e=new Container();
            var Fact=e.Fact;
            var Dimension=e.Dimension;
            var Integration=e.Integration;
            var City=Dimension.City;
            var Customer=Dimension.Customer;
            var Date=Dimension.Date;
            var Payment_Method=Dimension.Payment_Method;
            var Transaction_Type=Dimension.Transaction_Type;
            var Supplier=Dimension.Supplier;
            var Stock_Item=Dimension.Stock_Item;
            var Employee=Dimension.Employee;
            var Movement=Fact.Movement;
            var Order=Fact.Order;
            var Purchase=Fact.Purchase;
            var Sale=Fact.Sale;
            var Stock_Holding=Fact.Stock_Holding;
            var Transaction=Fact.Transaction;
            var Customer_Staging=Integration.Customer_Staging;
            var City_Staging=Integration.City_Staging;
            var ETL_Cutoff=Integration.ETL_Cutoff;
            var Employee_Staging=Integration.Employee_Staging;
            var Lineage=Integration.Lineage;
            var Movement_Staging=Integration.Movement_Staging;
            var Order_Staging=Integration.Order_Staging;
            var PaymentMethod_Staging=Integration.PaymentMethod_Staging;
            var Purchase_Staging=Integration.Purchase_Staging;
            var Sale_Staging=Integration.Sale_Staging;
            var StockHolding_Staging=Integration.StockHolding_Staging;
            var StockItem_Staging=Integration.StockItem_Staging;
            var Supplier_Staging=Integration.Supplier_Staging;
            var TransactionType_Staging=Integration.TransactionType_Staging;
            var Transaction_Staging=Integration.Transaction_Staging;
            var City情報=new AddDel情報();
            var Customer情報=new AddDel情報();
            var Date情報=new AddDel情報();
            var Payment_Method情報=new AddDel情報();
            var Transaction_Type情報=new AddDel情報();
            var Supplier情報=new AddDel情報();
            var Stock_Item情報=new AddDel情報();
            var Employee情報=new AddDel情報();
            var Movement情報=new AddDel情報();
            var Order情報=new AddDel情報();
            var Purchase情報=new AddDel情報();
            var Sale情報=new AddDel情報();
            var Stock_Holding情報=new AddDel情報();
            var Transaction情報=new AddDel情報();
            var Customer_Staging情報=new AddDel情報();
            var City_Staging情報=new AddDel情報();
            var ETL_Cutoff情報=new AddDel情報();
            var Employee_Staging情報=new AddDel情報();
            var Lineage情報=new AddDel情報();
            var Movement_Staging情報=new AddDel情報();
            var Order_Staging情報=new AddDel情報();
            var PaymentMethod_Staging情報=new AddDel情報();
            var Purchase_Staging情報=new AddDel情報();
            var Sale_Staging情報=new AddDel情報();
            var StockHolding_Staging情報=new AddDel情報();
            var StockItem_Staging情報=new AddDel情報();
            var Supplier_Staging情報=new AddDel情報();
            var TransactionType_Staging情報=new AddDel情報();
            var Transaction_Staging情報=new AddDel情報();
            var s=Stopwatch.StartNew();
            for(var a=0;a<試行回数;a++){
                switch(Switchパターン()%2){
                    case 0:
                        switch(Switchパターン()%32){
                            case 1:
                                Add(ref City情報,City,
                                    new City(a,0,"","","","","","","",null,0,DateTime.Now,DateTime.Now,0));
                                break;
                            case 2:
                                Add(ref Customer情報,Customer,
                                    new Customer(a,0,"","","","","","",DateTime.Now,DateTime.Now,0));
                                break;
                            case 3:
                                Add(ref City情報,City,
                                    new City(a,0,"","","","","","","",null,0,DateTime.Now,DateTime.Now,0));
                                break;
                            case 4:
                                Add(ref Date情報,Date,
                                    new Date(DateTime.Now,0,DateTime.Now.Day.ToString(),
                                        DateTime.Now.Month.ToString("MMMM"),DateTime.Now.ToString("MM"),
                                        DateTime.Now.Month,DateTime.Now.ToString("M"),DateTime.Now.Year,
                                        DateTime.Now.Year.ToString(),12,"",0,"",
                                        new JapaneseCalendar().GetWeekOfYear(DateTime.Now,CalendarWeekRule.FirstDay,
                                            DayOfWeek.Monday)));
                                break;
                            case 5:
                                Add(ref Payment_Method情報,Payment_Method,
                                    new Payment_Method(a,0,"",DateTime.Now,DateTime.Now,0));
                                break;
                            case 6:
                                Add(ref Transaction_Type情報,Transaction_Type,
                                    new Transaction_Type(a,0,"",DateTime.Now,DateTime.Now,0));
                                break;
                            case 7:
                                Add(ref Supplier情報,Supplier,
                                    new Supplier(a,0,"","","",null,0,"",DateTime.Now,DateTime.Now,0));
                                break;
                            case 8:
                                Add(ref Stock_Item情報,Stock_Item,
                                    new Stock_Item(a,0,"","","","","","",0,0,false,null,0,0,null,0,null,DateTime.Now,
                                        DateTime.Now,0));
                                break;
                            case 9:
                                Add(ref Employee情報,Employee,
                                    new Employee(a,0,"","",false,null,DateTime.Now,DateTime.Now,0));
                                break;
                            case 10:
                                Add(ref Movement情報,Movement,new Movement(a,DateTime.Now,0,null,null,0,0,null,null,0,0));
                                break;
                            case 11:
                                Add(ref Order情報,Order,
                                    new Order(a,0,0,0,DateTime.Now,null,0,null,0,null,"","",0,0,0,0,0,0,0));
                                break;
                            case 12:
                                Add(ref Purchase情報,Purchase,new Purchase(a,DateTime.Now,0,0,null,0,0,0,"",false,0));
                                break;
                            case 13:
                                Add(ref Sale情報,Sale,
                                    new Sale(a,0,0,0,0,DateTime.Now,null,0,0,"","",0,0,0,0,0,0,0,0,0,0));
                                break;
                            case 14:
                                Add(ref Stock_Holding情報,Stock_Holding,new Stock_Holding(a,0,0,"",0,0,0,0,0));
                                break;
                            case 15:
                                Add(ref Transaction情報,Transaction,
                                    new Transaction(a,DateTime.Now,null,null,null,0,null,null,null,null,null,null,0,0,0,
                                        0,false,0));
                                break;
                            case 16:
                                Add(ref Customer_Staging情報,Customer_Staging,
                                    new Customer_Staging(a,0,"","","","","","",DateTime.Now,DateTime.Now));
                                break;
                            case 17:
                                Add(ref City_Staging情報,City_Staging,
                                    new City_Staging(a,0,"","","","","","","",null,0,DateTime.Now,DateTime.Now));
                                break;
                            case 18:
                                Add(ref ETL_Cutoff情報,ETL_Cutoff,new ETL_Cutoff(a.ToString(),DateTime.Now));
                                break;
                            case 19:
                                Add(ref Employee_Staging情報,Employee_Staging,
                                    new Employee_Staging(a,0,"","",false,null,DateTime.Now,DateTime.Now));
                                break;
                            case 20:
                                Add(ref Lineage情報,Lineage,new Lineage(a,DateTime.Now,"",null,false,DateTime.Now));
                                break;
                            case 21:
                                Add(ref Movement_Staging情報,Movement_Staging,
                                    new Movement_Staging(a,null,null,null,null,null,null,null,null,null,null,null,null,
                                        null,null));
                                break;
                            case 22:
                                Add(ref Order_Staging情報,Order_Staging,
                                    new Order_Staging(a,null,null,null,null,null,null,null,null,null,null,null,null,
                                        null,null,null,null,null,null,null,null,null,null,null,null));
                                break;
                            case 23:
                                Add(ref PaymentMethod_Staging情報,PaymentMethod_Staging,
                                    new PaymentMethod_Staging(a,0,"",DateTime.Now,DateTime.Now));
                                break;
                            case 24:
                                Add(ref Purchase_Staging情報,Purchase_Staging,
                                    new Purchase_Staging(a,null,null,null,null,null,null,null,null,null,null,null,
                                        null));
                                break;
                            case 25:
                                Add(ref Sale_Staging情報,Sale_Staging,
                                    new Sale_Staging(a,null,null,null,null,null,null,null,null,null,null,null,null,null,
                                        null,null,null,null,null,null,null,null,null,null,null,null));
                                break;
                            case 26:
                                Add(ref StockHolding_Staging情報,StockHolding_Staging,
                                    new StockHolding_Staging(a,null,null,null,null,null,null,null,null));
                                break;
                            case 27:
                                Add(ref StockItem_Staging情報,StockItem_Staging,
                                    new StockItem_Staging(a,0,"","","","","","",0,0,false,null,0,0,null,0,null,
                                        DateTime.Now,DateTime.Now));
                                break;
                            case 28:
                                Add(ref Supplier_Staging情報,Supplier_Staging,
                                    new Supplier_Staging(a,0,"","","",null,0,"",DateTime.Now,DateTime.Now));
                                break;
                            case 29:
                                Add(ref Transaction_Type情報,Transaction_Type,
                                    new Transaction_Type(a,0,"",DateTime.Now,DateTime.Now,0));
                                break;
                            case 30:
                                Add(ref TransactionType_Staging情報,TransactionType_Staging,
                                    new TransactionType_Staging(a,0,"",DateTime.Now,DateTime.Now));
                                break;
                            case 31:
                                Add(ref Transaction_Staging情報,Transaction_Staging,
                                    new Transaction_Staging(a,null,null,null,null,null,null,null,null,null,null,null,
                                        null,null,null,null,null,null,null,null,null,null,null));
                                break;
                        }

                        break;
                    case 1:
                        switch(Switchパターン()%32){
                            case 1:
                                Del(ref City情報,City);
                                break;
                            case 2:
                                Del(ref Customer情報,Customer);
                                break;
                            case 3:
                                Del(ref City情報,City);
                                break;
                            case 4:
                                Del(ref Date情報,Date);
                                break;
                            case 5:
                                Del(ref Payment_Method情報,Payment_Method);
                                break;
                            case 6:
                                Del(ref Transaction_Type情報,Transaction_Type);
                                break;
                            case 7:
                                Del(ref Supplier情報,Supplier);
                                break;
                            case 8:
                                Del(ref Stock_Item情報,Stock_Item);
                                break;
                            case 9:
                                Del(ref Employee情報,Employee);
                                break;
                            case 10:
                                Del(ref Movement情報,Movement);
                                break;
                            case 11:
                                Del(ref Order情報,Order);
                                break;
                            case 12:
                                Del(ref Purchase情報,Purchase);
                                break;
                            case 13:
                                Del(ref Sale情報,Sale);
                                break;
                            case 14:
                                Del(ref Stock_Holding情報,Stock_Holding);
                                break;
                            case 15:
                                Del(ref Transaction情報,Transaction);
                                break;
                            case 16:
                                Del(ref Customer_Staging情報,Customer_Staging);
                                break;
                            case 17:
                                Del(ref City_Staging情報,City_Staging);
                                break;
                            case 18:
                                Del(ref ETL_Cutoff情報,ETL_Cutoff);
                                break;
                            case 19:
                                Del(ref Employee_Staging情報,Employee_Staging);
                                break;
                            case 20:
                                Del(ref Lineage情報,Lineage);
                                break;
                            case 21:
                                Del(ref Movement_Staging情報,Movement_Staging);
                                break;
                            case 22:
                                Del(ref Order_Staging情報,Order_Staging);
                                break;
                            case 23:
                                Del(ref PaymentMethod_Staging情報,PaymentMethod_Staging);
                                break;
                            case 24:
                                Del(ref Purchase_Staging情報,Purchase_Staging);
                                break;
                            case 25:
                                Del(ref Sale_Staging情報,Sale_Staging);
                                break;
                            case 26:
                                Del(ref StockHolding_Staging情報,StockHolding_Staging);
                                break;
                            case 27:
                                Del(ref StockItem_Staging情報,StockItem_Staging);
                                break;
                            case 28:
                                Del(ref Supplier_Staging情報,Supplier_Staging);
                                break;
                            case 29:
                                Del(ref Transaction_Type情報,Transaction_Type);
                                break;
                            case 30:
                                Del(ref TransactionType_Staging情報,TransactionType_Staging);
                                break;
                            case 31:
                                Del(ref Transaction_Staging情報,Transaction_Staging);
                                break;
                        }

                        break;
                }
            }

            s.Stop();
            Trace.WriteLine($"Create {s.ElapsedMilliseconds,7}ms");
            Console.WriteLine($"Create {s.ElapsedMilliseconds,7}ms");
            AddWriteLine(City情報);
            AddWriteLine(Customer情報);
            AddWriteLine(City情報);
            AddWriteLine(Date情報);
            AddWriteLine(Payment_Method情報);
            AddWriteLine(Transaction_Type情報);
            AddWriteLine(Supplier情報);
            AddWriteLine(Stock_Item情報);
            AddWriteLine(Employee情報);
            AddWriteLine(Movement情報);
            AddWriteLine(Order情報);
            AddWriteLine(Purchase情報);
            AddWriteLine(Sale情報);
            AddWriteLine(Stock_Holding情報);
            AddWriteLine(Transaction情報);
            AddWriteLine(Customer_Staging情報);
            AddWriteLine(City_Staging情報);
            AddWriteLine(ETL_Cutoff情報);
            AddWriteLine(Employee_Staging情報);
            AddWriteLine(Lineage情報);
            AddWriteLine(Movement_Staging情報);
            AddWriteLine(Order_Staging情報);
            AddWriteLine(PaymentMethod_Staging情報);
            AddWriteLine(Purchase_Staging情報);
            AddWriteLine(Sale_Staging情報);
            AddWriteLine(StockHolding_Staging情報);
            AddWriteLine(StockItem_Staging情報);
            AddWriteLine(Supplier_Staging情報);
            AddWriteLine(Transaction_Type情報);
            AddWriteLine(TransactionType_Staging情報);
            AddWriteLine(Transaction_Staging情報);

            DelWriteLine(City情報);
            DelWriteLine(Customer情報);
            DelWriteLine(City情報);
            DelWriteLine(Date情報);
            DelWriteLine(Payment_Method情報);
            DelWriteLine(Transaction_Type情報);
            DelWriteLine(Supplier情報);
            DelWriteLine(Stock_Item情報);
            DelWriteLine(Employee情報);
            DelWriteLine(Movement情報);
            DelWriteLine(Order情報);
            DelWriteLine(Purchase情報);
            DelWriteLine(Sale情報);
            DelWriteLine(Stock_Holding情報);
            DelWriteLine(Transaction情報);
            DelWriteLine(Customer_Staging情報);
            DelWriteLine(City_Staging情報);
            DelWriteLine(ETL_Cutoff情報);
            DelWriteLine(Employee_Staging情報);
            DelWriteLine(Lineage情報);
            DelWriteLine(Movement_Staging情報);
            DelWriteLine(Order_Staging情報);
            DelWriteLine(PaymentMethod_Staging情報);
            DelWriteLine(Purchase_Staging情報);
            DelWriteLine(Sale_Staging情報);
            DelWriteLine(StockHolding_Staging情報);
            DelWriteLine(StockItem_Staging情報);
            DelWriteLine(Supplier_Staging情報);
            DelWriteLine(Transaction_Type情報);
            DelWriteLine(TransactionType_Staging情報);
            DelWriteLine(Transaction_Staging情報);
        }

        [SuppressMessage("Globalization","CA1305:IFormatProvider を指定します",Justification="<保留中>")]
        [SuppressMessage("Globalization","CA1303:ローカライズされるパラメーターとしてリテラルを渡さない",Justification="<保留中>")]
        private static void Load(){
            using var Connection=
                new SqlConnection(
                    @"Data Source=localhost\MSSQLSERVER2019;Initial Catalog=WideWorldImportersDW;Integrated Security=True");
            Connection.Open();
            using var Command=new SqlCommand{Connection=Connection};
            using var e=new Container2();
            var dbo=e.dbo;
            var Dimension=e.Dimension;
            var s=Stopwatch.StartNew();
            {
                Command.CommandText=
                    "SELECT"
                    +" [City Key]"
                    +",[WWI City ID]"
                    +",[City]"
                    +",[State Province]"
                    +",[Country]"
                    +",[Continent]"
                    +",[Sales Territory]"
                    +",[Region]"
                    +",[Subregion]"
                    +",[Location]"
                    +",[Latest Recorded Population]"
                    +",[Valid From]"
                    +",[Valid To]"
                    +",[Lineage Key]"
                    +"FROM[WideWorldImportersDW].[Dimension].[City]"
                    +"SELECT"
                    +" [Customer Key]"
                    +",[WWI Customer ID]"
                    +",[Customer]"
                    +",[Bill To Customer]"
                    +",[Category]"
                    +",[Buying Group]"
                    +",[Primary Contact]"
                    +",[Postal Code]"
                    +",[Valid From]"
                    +",[Valid To]"
                    +",[Lineage Key]"
                    +"FROM[WideWorldImportersDW].[Dimension].[Customer]"
                    +"SELECT"
                    +" [Date]"
                    +",[Day Number]"
                    +",[Day]"
                    +",[Month]"
                    +",[Short Month]"
                    +",[Calendar Month Number]"
                    +",[Calendar Month Label]"
                    +",[Calendar Year]"
                    +",[Calendar Year Label]"
                    +",[Fiscal Month Number]"
                    +",[Fiscal Month Label]"
                    +",[Fiscal Year]"
                    +",[Fiscal Year Label]"
                    +",[ISO Week Number]"
                    +"FROM[WideWorldImportersDW].[Dimension].[Date]"
                    +"SELECT"
                    +" [Payment Method Key]"
                    +",[WWI Payment Method ID]"
                    +",[Payment Method]"
                    +",[Valid From]"
                    +",[Valid To]"
                    +",[Lineage Key]"
                    +"FROM[WideWorldImportersDW].[Dimension].[Payment Method]"
                    +"SELECT"
                    +" [Transaction Type Key]"
                    +",[WWI Transaction Type ID]"
                    +",[Transaction Type]"
                    +",[Valid From]"
                    +",[Valid To]"
                    +",[Lineage Key]"
                    +"FROM[WideWorldImportersDW].[Dimension].[Transaction Type]"
                    +"SELECT"
                    +" [Supplier Key]"
                    +",[WWI Supplier ID]"
                    +",[Supplier]"
                    +",[Category]"
                    +",[Primary Contact]"
                    +",[Supplier Reference]"
                    +",[Payment Days]"
                    +",[Postal Code]"
                    +",[Valid From]"
                    +",[Valid To]"
                    +",[Lineage Key]"
                    +"FROM[WideWorldImportersDW].[Dimension].[Supplier]"
                    +"SELECT"
                    +" [Stock Item Key]"
                    +",[WWI Stock Item ID]"
                    +",[Stock Item]"
                    +",[Color]"
                    +",[Selling Package]"
                    +",[Buying Package]"
                    +",[Brand]"
                    +",[Size]"
                    +",[Lead Time Days]"
                    +",[Quantity Per Outer]"
                    +",[Is Chiller Stock]"
                    +",[Barcode]"
                    +",[Tax Rate]"
                    +",[Unit Price]"
                    +",[Recommended Retail Price]"
                    +",[Typical Weight Per Unit]"
                    +",[Photo]"
                    +",[Valid From]"
                    +",[Valid To]"
                    +",[Lineage Key]"
                    +"FROM[WideWorldImportersDW].[Dimension].[Stock Item]"
                    +"SELECT"
                    +" [Employee Key]"
                    +",[WWI Employee ID]"
                    +",[Employee]"
                    +",[Preferred Name]"
                    +",[Is Salesperson]"
                    +",[Photo]"
                    +",[Valid From]"
                    +",[Valid To]"
                    +",[Lineage Key]"
                    +"FROM[WideWorldImportersDW].[Dimension].[Employee]"
                    +"SELECT"
                    +" [Movement Key]"
                    +",[Date Key]"
                    +",[Stock Item Key]"
                    +",[Customer Key]"
                    +",[Supplier Key]"
                    +",[Transaction Type Key]"
                    +",[WWI Stock Item Transaction ID]"
                    +",[WWI Invoice ID]"
                    +",[WWI Purchase Order ID]"
                    +",[Quantity]"
                    +",[Lineage Key]"
                    +"FROM [WideWorldImportersDW].[Fact].[Movement]"
                    +"SELECT"
                    +" [Order Key]"
                    +",[City Key]"
                    +",[Customer Key]"
                    +",[Stock Item Key]"
                    +",[Order Date Key]"
                    +",[Picked Date Key]"
                    +",[Salesperson Key]"
                    +",[Picker Key]"
                    +",[WWI Order ID]"
                    +",[WWI Backorder ID]"
                    +",[Description]"
                    +",[Package]"
                    +",[Quantity]"
                    +",[Unit Price]"
                    +",[Tax Rate]"
                    +",[Total Excluding Tax]"
                    +",[Tax Amount]"
                    +",[Total Including Tax]"
                    +",[Lineage Key]"
                    +"FROM [WideWorldImportersDW].[Fact].[Order]"
                    +"SELECT"
                    +" [Purchase Key]"
                    +",[Date Key]"
                    +",[Supplier Key]"
                    +",[Stock Item Key]"
                    +",[WWI Purchase Order ID]"
                    +",[Ordered Outers]"
                    +",[Ordered Quantity]"
                    +",[Received Outers]"
                    +",[Package]"
                    +",[Is Order Finalized]"
                    +",[Lineage Key]"
                    +"FROM [WideWorldImportersDW].[Fact].[Purchase]"
                    +"SELECT"
                    +" [Sale Key]"
                    +",[City Key]"
                    +",[Customer Key]"
                    +",[Bill To Customer Key]"
                    +",[Stock Item Key]"
                    +",[Invoice Date Key]"
                    +",[Delivery Date Key]"
                    +",[Salesperson Key]"
                    +",[WWI Invoice ID]"
                    +",[Description]"
                    +",[Package]"
                    +",[Quantity]"
                    +",[Unit Price]"
                    +",[Tax Rate]"
                    +",[Total Excluding Tax]"
                    +",[Tax Amount]"
                    +",[Profit]"
                    +",[Total Including Tax]"
                    +",[Total Dry Items]"
                    +",[Total Chiller Items]"
                    +",[Lineage Key]"
                    +"FROM [WideWorldImportersDW].[Fact].[Sale]"
                    +"SELECT"
                    +" [Stock Holding Key]"
                    +",[Stock Item Key]"
                    +",[Quantity On Hand]"
                    +",[Bin Location]"
                    +",[Last Stocktake Quantity]"
                    +",[Last Cost Price]"
                    +",[Reorder Level]"
                    +",[Target Stock Level]"
                    +",[Lineage Key]"
                    +"FROM [WideWorldImportersDW].[Fact].[Stock Holding]"
                    +"SELECT"
                    +" [Transaction Key]"
                    +",[Date Key]"
                    +",[Customer Key]"
                    +",[Bill To Customer Key]"
                    +",[Supplier Key]"
                    +",[Transaction Type Key]"
                    +",[Payment Method Key]"
                    +",[WWI Customer Transaction ID]"
                    +",[WWI Supplier Transaction ID]"
                    +",[WWI Invoice ID]"
                    +",[WWI Purchase Order ID]"
                    +",[Supplier Invoice Number]"
                    +",[Total Excluding Tax]"
                    +",[Tax Amount]"
                    +",[Total Including Tax]"
                    +",[Outstanding Balance]"
                    +",[Is Finalized]"
                    +",[Lineage Key]"
                    +"FROM [WideWorldImportersDW].[Fact].[Transaction]";

                //static String? GetString(SqlDataReader r,Int32 i) => r.IsDBNull(i) ? default : r.GetString(i);
                //static Int32? GetInt32(SqlDataReader r,Int32 i) => r.IsDBNull(i) ? default(Int32?) : r.GetInt32(i);
                //static DateTime? GetDateTime(SqlDataReader r,Int32 i) => r.IsDBNull(i) ? default(DateTime?) : r.GetDateTime(i);
                //static SqlGeography? GetSqlGeography(SqlDataReader r,Int32 i) => r.IsDBNull(i) ? default : (SqlGeography)r.GetValue(i);
                using var Reader=Command.ExecuteReader();
                var City=Dimension.City;
                while(Reader.Read()){
                    City.AddOrThrow(
                        new City(
                            Reader.GetInt32(0),
                            Reader.GetInt32(1),
                            Reader.GetString(2),
                            Reader.GetString(3),
                            Reader.GetString(4),
                            Reader.GetString(5),
                            Reader.GetString(6),
                            Reader.GetString(7),
                            Reader.GetString(8),
                            GetSqlGeography(Reader,9),
                            Reader.GetInt64(10),
                            Reader.GetDateTime(11),
                            Reader.GetDateTime(12),
                            Reader.GetInt32(13)
                        )
                    );
                }

                if(!Reader.NextResult()) throw new Exception();
                var Customer=Dimension.Customer;
                while(Reader.Read()){
                    Customer.AddOrThrow(
                        new Customer(
                            Reader.GetInt32(0),
                            Reader.GetInt32(1),
                            Reader.GetString(2),
                            Reader.GetString(3),
                            Reader.GetString(4),
                            Reader.GetString(5),
                            Reader.GetString(6),
                            Reader.GetString(7),
                            Reader.GetDateTime(8),
                            Reader.GetDateTime(9),
                            Reader.GetInt32(10)
                        )
                    );
                }

                if(!Reader.NextResult()) throw new Exception();
                var Date=Dimension.Date;
                while(Reader.Read()){
                    Date.AddOrThrow(
                        new Date(
                            Reader.GetDateTime(0),
                            Reader.GetInt32(1),
                            Reader.GetString(2),
                            Reader.GetString(3),
                            Reader.GetString(4),
                            Reader.GetInt32(5),
                            Reader.GetString(6),
                            Reader.GetInt32(7),
                            Reader.GetString(8),
                            Reader.GetInt32(9),
                            Reader.GetString(10),
                            Reader.GetInt32(11),
                            Reader.GetString(12),
                            Reader.GetInt32(13)
                        )
                    );
                }

                if(!Reader.NextResult()) throw new Exception();
                var Payment_Method=Dimension.Payment_Method;
                while(Reader.Read()){
                    Payment_Method.AddOrThrow(
                        new Payment_Method(
                            Reader.GetInt32(0),
                            Reader.GetInt32(1),
                            Reader.GetString(2),
                            Reader.GetDateTime(3),
                            Reader.GetDateTime(4),
                            Reader.GetInt32(5)
                        )
                    );
                }

                if(!Reader.NextResult()) throw new Exception();
                var Transaction_Type=Dimension.Transaction_Type;
                while(Reader.Read()){
                    Transaction_Type.AddOrThrow(
                        new Transaction_Type(
                            Reader.GetInt32(0),
                            Reader.GetInt32(1),
                            Reader.GetString(2),
                            Reader.GetDateTime(3),
                            Reader.GetDateTime(4),
                            Reader.GetInt32(5)
                        )
                    );
                }

                if(!Reader.NextResult()) throw new Exception();
                var Supplier=Dimension.Supplier;
                while(Reader.Read()){
                    Supplier.AddOrThrow(
                        new Supplier(
                            Reader.GetInt32(0),
                            Reader.GetInt32(1),
                            Reader.GetString(2),
                            Reader.GetString(3),
                            Reader.GetString(4),
                            Reader.GetString(5),
                            Reader.GetInt32(6),
                            Reader.GetString(7),
                            Reader.GetDateTime(8),
                            Reader.GetDateTime(9),
                            Reader.GetInt32(10)
                        )
                    );
                }

                if(!Reader.NextResult()) throw new Exception();
                var Stock_Item=Dimension.Stock_Item;
                while(Reader.Read()){
                    Stock_Item.AddOrThrow(
                        new Stock_Item(
                            Reader.GetInt32(0),
                            Reader.GetInt32(1),
                            Reader.GetString(2),
                            Reader.GetString(3),
                            Reader.GetString(4),
                            Reader.GetString(5),
                            Reader.GetString(6),
                            Reader.GetString(7),
                            Reader.GetInt32(8),
                            Reader.GetInt32(9),
                            Reader.GetBoolean(10),
                            Reader.GetString(11),
                            Reader.GetDecimal(12),
                            Reader.GetDecimal(13),
                            Reader.GetDecimal(14),
                            Reader.GetDecimal(15),
                            Reader.GetValue(16) as Byte[],
                            Reader.GetDateTime(17),
                            Reader.GetDateTime(18),
                            Reader.GetInt32(19)
                        )
                    );
                }

                if(!Reader.NextResult()) throw new Exception();
                var Employee=Dimension.Employee;
                while(Reader.Read()){
                    Employee.AddOrThrow(
                        new Employee(
                            Reader.GetInt32(0),
                            Reader.GetInt32(1),
                            Reader.GetString(2),
                            Reader.GetString(3),
                            Reader.GetBoolean(4),
                            Reader.GetValue(5) as Byte[],
                            Reader.GetDateTime(6),
                            Reader.GetDateTime(7),
                            Reader.GetInt32(8)
                        )
                    );
                }

                if(!Reader.NextResult()) throw new Exception();
                var Fact=e.Fact;
                var Movement=Fact.Movement;
                while(Reader.Read()){
                    Movement.AddOrThrow(
                        new Movement(
                            Reader.GetInt64(0),
                            Reader.GetDateTime(1),
                            Reader.GetInt32(2),
                            GetInt32(Reader,3),
                            GetInt32(Reader,4),
                            Reader.GetInt32(5),
                            Reader.GetInt32(6),
                            GetInt32(Reader,7),
                            GetInt32(Reader,8),
                            Reader.GetInt32(9),
                            Reader.GetInt32(10)
                        )
                    );
                }

                if(!Reader.NextResult()) throw new Exception();
                var Order=Fact.Order;
                while(Reader.Read()){
                    Order.AddOrThrow(
                        new Order(
                            Reader.GetInt64(0),
                            Reader.GetInt32(1),
                            Reader.GetInt32(2),
                            Reader.GetInt32(3),
                            Reader.GetDateTime(4),
                            GetDateTime(Reader,5),
                            Reader.GetInt32(6),
                            GetInt32(Reader,7),
                            Reader.GetInt32(8),
                            GetInt32(Reader,9),
                            Reader.GetString(10),
                            Reader.GetString(11),
                            Reader.GetInt32(12),
                            Reader.GetDecimal(13),
                            Reader.GetDecimal(14),
                            Reader.GetDecimal(15),
                            Reader.GetDecimal(16),
                            Reader.GetDecimal(17),
                            Reader.GetInt32(18)
                        )
                    );
                }

                if(!Reader.NextResult()) throw new Exception();
                var Purchase=Fact.Purchase;
                while(Reader.Read()){
                    Purchase.AddOrThrow(
                        new Purchase(
                            Reader.GetInt64(0),
                            Reader.GetDateTime(1),
                            Reader.GetInt32(2),
                            Reader.GetInt32(3),
                            GetInt32(Reader,4),
                            Reader.GetInt32(5),
                            Reader.GetInt32(6),
                            Reader.GetInt32(7),
                            Reader.GetString(8),
                            Reader.GetBoolean(9),
                            Reader.GetInt32(10)
                        )
                    );
                }

                if(!Reader.NextResult()) throw new Exception();
                var Sale=Fact.Sale;
                while(Reader.Read()){
                    Sale.AddOrThrow(
                        new Sale(
                            Reader.GetInt64(0),
                            Reader.GetInt32(1),
                            Reader.GetInt32(2),
                            Reader.GetInt32(3),
                            Reader.GetInt32(4),
                            Reader.GetDateTime(5),
                            GetDateTime(Reader,6),
                            Reader.GetInt32(7),
                            Reader.GetInt32(8),
                            Reader.GetString(9),
                            Reader.GetString(10),
                            Reader.GetInt32(11),
                            Reader.GetDecimal(12),
                            Reader.GetDecimal(13),
                            Reader.GetDecimal(14),
                            Reader.GetDecimal(15),
                            Reader.GetDecimal(16),
                            Reader.GetDecimal(17),
                            Reader.GetInt32(18),
                            Reader.GetInt32(19),
                            Reader.GetInt32(20)
                        )
                    );
                }

                if(!Reader.NextResult()) throw new Exception();
                var Stock_Holding=Fact.Stock_Holding;
                while(Reader.Read()){
                    Stock_Holding.AddOrThrow(
                        new Stock_Holding(
                            Reader.GetInt64(0),
                            Reader.GetInt32(1),
                            Reader.GetInt32(2),
                            Reader.GetString(3),
                            Reader.GetInt32(4),
                            Reader.GetDecimal(5),
                            Reader.GetInt32(6),
                            Reader.GetInt32(7),
                            Reader.GetInt32(8)
                        )
                    );
                }

                if(!Reader.NextResult()) throw new Exception();
                var Transaction=Fact.Transaction;
                while(Reader.Read()){
                    Transaction.AddOrThrow(
                        new Transaction(
                            Reader.GetInt64(0),
                            Reader.GetDateTime(1),
                            GetInt32(Reader,2),
                            GetInt32(Reader,3),
                            GetInt32(Reader,4),
                            Reader.GetInt32(5),
                            GetInt32(Reader,6),
                            GetInt32(Reader,7),
                            GetInt32(Reader,8),
                            GetInt32(Reader,9),
                            GetInt32(Reader,10),
                            GetString(Reader,11),
                            Reader.GetDecimal(12),
                            Reader.GetDecimal(13),
                            Reader.GetDecimal(14),
                            Reader.GetDecimal(15),
                            Reader.GetBoolean(16),
                            Reader.GetInt32(17)
                        )
                    );
                }
            }
            //Application.Configuration_ApplyPartitionedColumnstoreIndexing(e);
            //Application.Configuration_ApplyPolybase(e);
            Application.Configuration_ConfigureForEnterpriseEdition(e);
            Application.Configuration_PopulateLargeSaleTable(e);
            Application.Configuration_ReseedETL(e);
            Integration.GetLastETLCutoffTime(e,"City");
            e.Clear();
            Console.WriteLine($"Load {s.ElapsedMilliseconds,7}ms");
        }

        private static class Application{
            public static Int32 Configuration_ApplyPartitionedColumnstoreIndexing(Container2 e){
                Console.WriteLine(e.ToString());
                return 1;
            }
            public static void Configuration_ApplyPolybase(Container2 e) {
                Console.WriteLine(e.ToString());
            }
            public static void Configuration_ConfigureForEnterpriseEdition(Container2 e) {
                Application.Configuration_ApplyPartitionedColumnstoreIndexing(e);
                Application.Configuration_EnableInMemory(e);
                Application.Configuration_ApplyPolybase(e);
            }
            public static void Configuration_EnableInMemory(Container2 e){
                Console.WriteLine(e.ToString());
            }
            public static void Configuration_PopulateLargeSaleTable(Container2 e,Int64 EstimatedRowsFor2012= 12000000) {
                Integration.PopulateDateDimensionForYear(e,2012);
                var ReturnValue= Configuration_ApplyPartitionedColumnstoreIndexing(e);
                var LineageKey = e.Sequences2.LineageKey.Current;
                e.Integration.Lineage.Add(
                    new Lineage(
                        LineageKey,
                        DateTime.Now,
                        "Sale",
                        null,
                        false,
                        new DateTime(2012,12,31)
                    )
                );
                var NumberOfSalesPerDay = EstimatedRowsFor2012/365;
                var DateCounter = new DateTime(2012,1,1);
                var MaximumSaleKey = (from Sale in e.Fact.Sale select Sale.Sale_Key).Max();
                Console.WriteLine($"Targeting {NumberOfSalesPerDay} sales per day.");
                if(NumberOfSalesPerDay>50000) {
                    Console.WriteLine($"WARNING :Limiting sales to 40000 per day");
                    NumberOfSalesPerDay=50000;
                }
                var r = new Random(1);
                String OutputCounter;
                while(DateCounter<new DateTime(2012,12,31)) {
                    OutputCounter=DateCounter.ToString("yyyymmdd");
                    var StartingSaleKey = MaximumSaleKey-NumberOfSalesPerDay-Math.Floor(r.NextDouble());
                    var OrderCounter = 0;
                    e.Fact.Sale.AddRange(
                        (
                            from Sale in e.Fact.Sale
                            where Sale.Sale_Key>StartingSaleKey&&Sale.Invoice_Date_Key>=new DateTime(2013,1,1)
                            select new Sale(
                                e.Fact.Sale.Count,
                                Sale.City_Key,
                                Sale.Customer_Key,
                                Sale.Bill_To_Customer_Key,
                                Sale.Stock_Item_Key,
                                DateCounter,
                                DateCounter.AddDays(1),
                                Sale.Salesperson_Key,
                                Sale.WWI_Invoice_ID,
                                Sale.Description,
                                Sale.Package,
                                Sale.Quantity,
                                Sale.Unit_Price,
                                Sale.Tax_Rate,
                                Sale.Total_Excluding_Tax,
                                Sale.Tax_Amount,
                                Sale.Profit,
                                Sale.Total_Including_Tax,
                                Sale.Total_Dry_Items,
                                Sale.Total_Chiller_Items,
                                LineageKey
                            )
                        ).Take(OrderCounter)
                    );
                    DateCounter=DateCounter.AddDays(1);
                }
                e.Integration.Lineage.UpdateWith(
                    p => new Lineage(
                        p.Lineage_Key,
                        DateTime.Now,
                        p.Table_Name,
                        p.Data_Load_Completed,
                        true,
                        p.Source_System_Cutoff_Time
                    )
                );
            }
            public static void Configuration_ReseedETL(Container2 e){
                var @StartingETLCutoffTime=new DateTime(2012,12,31);
                var @StartOfTime=new DateTime(2013,1,1);
                var @EndOfTime=DateTime.MaxValue;
                e.Integration.ETL_Cutoff.UpdateWith(p=>new ETL_Cutoff(p.Table_Name,StartingETLCutoffTime));
                e.Fact.Movement.Clear();
                e.Fact.Order.Clear();
                e.Fact.Purchase.Clear();
                e.Fact.Sale.Clear();
                e.Fact.Stock_Holding.Clear();
                e.Fact.Transaction.Clear();
                e.Dimension.City.Clear();
                e.Dimension.Customer.Clear();
                e.Dimension.Employee.Clear();
                e.Dimension.Payment_Method.Clear();
                e.Dimension.Stock_Item.Clear();
                e.Dimension.Supplier.Clear();
                e.Dimension.Transaction_Type.Clear();
                e.Dimension.City.Add(new City(0,0,"Unknown","N/A","N/A","N/A","N/A","N/A","N/A",null,0,StartOfTime,EndOfTime,0));
                e.Dimension.Customer.Add(new Customer(0,0,"Unknown","N/A","N/A","N/A","N/A","N/A",StartOfTime,EndOfTime,0));
                e.Dimension.Employee.Add(new Employee(0,0,"Unknown","N/A",false,null,StartOfTime,EndOfTime,0));
                e.Dimension.Payment_Method.Add(new Payment_Method(0,0,"Unknown",StartOfTime,EndOfTime,0));
                e.Dimension.Stock_Item.Add(new Stock_Item(0,0,"Unknown","N/A","N/A","N/A","N/A","N/A",0,0,false,"N/A",0,0,0,0,null,StartOfTime,EndOfTime,0));
                e.Dimension.Supplier.Add(new Supplier(0,0,"Unknown","N/A","N/A","N/A",0,"N/A",StartOfTime,EndOfTime,0));
                e.Dimension.Transaction_Type.Add(new Transaction_Type(0,0,"Unknown",StartOfTime,EndOfTime,0));
            }
        }
        private static class Integration{
            public static void GetLastETLCutoffTime(Container2 e,String TableName){
                var x=from ETL_Cutoff in e.Integration.ETL_Cutoff
                    where ETL_Cutoff.Table_Name==TableName
                    select ETL_Cutoff;
                if(x.Count==0){
                    Debug.Print("Invalid ETL table name");
                    throw new ApplicationException("Invalid ETL table name");
                }
            }
            public static void GetLineageKey(Container2 e,String TableName,DateTime NewCutoffTime){
                var DataLoadStartedWhen=DateTime.Now;
                var Transaction=e.Transaction();
                Transaction.Integration.Lineage.Add(
                    new Lineage(
                        (Int32)e.Integration.Lineage.Count,
                        DataLoadStartedWhen,
                        TableName,
                        null,
                        false,
                        NewCutoffTime
                    )
                );
                Console.WriteLine(
                    (
                        from Lineage in Transaction.Integration.Lineage
                        where Lineage.Table_Name==TableName&&Lineage.Data_Load_Started==DataLoadStartedWhen
                        orderby Lineage.Lineage_Key
                        select Lineage.Lineage_Key
                    ).Single()
                );
            }
            private static String ToMonthName(DateTime dateTime) {
                return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
            }

            private static String ToShortMonthName(DateTime dateTime) {
                return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(dateTime.Month);
            }
            private static readonly DateTime EndOfTime = new(9999,12,31,23,59,59,999);
            public static void MigrateStagedCityData(Container2 e) {
                const String Table_Name = "City";
                var t = e.Transaction();
                var LineageKey=(
                    from Lineage in t.Integration.Lineage
                    where Lineage.Table_Name==Table_Name&&Lineage.Data_Load_Completed is null
                    orderby Lineage.Lineage_Key
                    select Lineage.Lineage_Key
                ).Single();
                var RowsToCloseOff=
                    from c in t.Integration.City_Staging
                    group c by c.WWI_City_ID
                    into g
                    select (WWI_City_ID:g.Key,Valid_From:g.Min(c=>c.Valid_From));
                t.Dimension.City.Assign(
                    from c in t.Dimension.City
                    join rtco in RowsToCloseOff on c.WWI_City_ID equals rtco.WWI_City_ID
                    where c.Valid_To==EndOfTime
                    select new City(c.City_Key,rtco.WWI_City_ID,
                    c.City,c.State_Province,c.Country,c.Continent,c.Sales_Territory,c.Region,c.Subregion,c.Location,c.Latest_Recorded_Population,c.Valid_From,c.Valid_To,c.Lineage_Key)
                );
                t.Dimension.City.Insert(
                    from c in t.Integration.City_Staging
                    select new City((Int32)t.Dimension.City.Count,c.WWI_City_ID,
                    c.City,c.State_Province,c.Country,c.Continent,c.Sales_Territory,c.Region,c.Subregion,c.Location,c.Latest_Recorded_Population,c.Valid_From,c.Valid_To,LineageKey)
                );
                t.Integration.Lineage.UpdateWith(
                    p=>new Lineage(p.Lineage_Key,p.Data_Load_Started,p.Table_Name,DateTime.Now,true,p.Source_System_Cutoff_Time),
                    p=>p.Lineage_Key==LineageKey
                );
                t.Integration.ETL_Cutoff.UpdateWith(
                    p => new ETL_Cutoff(
                        p.Table_Name,
                        (
                            from Lineage in t.Integration.Lineage
                            where Lineage.Lineage_Key==LineageKey
                            select Lineage.Source_System_Cutoff_Time
                        ).Single()
                    ),
                    p => p.Table_Name==Table_Name
                );
                t.Commit();
            }
            public static void MigrateStagedCustomerData(Container2 e) {
                const String Table_Name = "Customer";
                var t = e.Transaction();
                var LineageKey = (
                    from Lineage in t.Integration.Lineage
                    where Lineage.Table_Name==Table_Name&&Lineage.Data_Load_Completed is null
                    orderby Lineage.Lineage_Key
                    select Lineage.Lineage_Key
                ).Single();
                var RowsToCloseOff =
                    from c in t.Integration.Customer_Staging
                    group c by c.WWI_Customer_ID
                    into g
                    select (WWI_Customer_ID: g.Key, Valid_From: g.Min(c => c.Valid_From));
                t.Dimension.Customer.Assign(
                    from c in t.Dimension.Customer
                    join rtco in RowsToCloseOff on c.WWI_Customer_ID equals rtco.WWI_Customer_ID
                    where c.Valid_To==EndOfTime
                    select new Customer(c.Customer_Key,rtco.WWI_Customer_ID,
                    c.Customer,c.Bill_To_Customer,c.Category,c.Buying_Group,c.Primary_Contact,c.Postal_Code,c.Valid_From,c.Valid_To,c.Lineage_Key)
                );
                t.Dimension.Customer.Insert(
                    from c in t.Integration.Customer_Staging
                    select new Customer((Int32)t.Dimension.Customer.Count,c.WWI_Customer_ID,
                    c.Customer,c.Bill_To_Customer,c.Category,c.Buying_Group,c.Primary_Contact,c.Postal_Code,c.Valid_From,c.Valid_To,LineageKey)
                );
                t.Integration.Lineage.UpdateWith(
                    p => new Lineage(p.Lineage_Key,p.Data_Load_Started,p.Table_Name,DateTime.Now,true,p.Source_System_Cutoff_Time),
                    p => p.Lineage_Key==LineageKey
                );
                t.Integration.ETL_Cutoff.UpdateWith(
                    p => new ETL_Cutoff(
                        p.Table_Name,
                        (
                            from Lineage in t.Integration.Lineage
                            where Lineage.Lineage_Key==LineageKey
                            select Lineage.Source_System_Cutoff_Time
                        ).Single()
                    ),
                    p => p.Table_Name==Table_Name
                );
                t.Commit();
            }
            public static void MigrateStagedEmployeeData(Container2 e) {
                const String Table_Name = "Employee";
                var t = e.Transaction();
                var LineageKey = (
                    from Lineage in t.Integration.Lineage
                    where Lineage.Table_Name==Table_Name&&Lineage.Data_Load_Completed is null
                    orderby Lineage.Lineage_Key
                    select Lineage.Lineage_Key
                ).Single();
                var RowsToCloseOff =
                    from c in t.Integration.Employee_Staging
                    group c by c.WWI_Employee_ID
                    into g
                    select (WWI_Employee_ID: g.Key, Valid_From: g.Min(c => c.Valid_From));
                t.Dimension.Employee.Assign(
                    from c in t.Dimension.Employee
                    join rtco in RowsToCloseOff on c.WWI_Employee_ID equals rtco.WWI_Employee_ID
                    where c.Valid_To==EndOfTime
                    select new Employee(c.Employee_Key,rtco.WWI_Employee_ID,
                    c.Employee,c.Preferred_Name,c.Is_Salesperson,c.Photo,c.Valid_From,c.Valid_To,LineageKey)
                );
                t.Dimension.Employee.Insert(
                    from c in t.Integration.Employee_Staging
                    select new Employee((Int32)t.Dimension.Employee.Count,c.WWI_Employee_ID,
                    c.Employee,c.Preferred_Name,c.Is_Salesperson,c.Photo,c.Valid_From,c.Valid_To,LineageKey)
                );
                t.Integration.Lineage.UpdateWith(
                    p => new Lineage(p.Lineage_Key,p.Data_Load_Started,p.Table_Name,DateTime.Now,true,p.Source_System_Cutoff_Time),
                    p => p.Lineage_Key==LineageKey
                );
                t.Integration.ETL_Cutoff.UpdateWith(
                    p => new ETL_Cutoff(
                        p.Table_Name,
                        (
                            from Lineage in t.Integration.Lineage
                            where Lineage.Lineage_Key==LineageKey
                            select Lineage.Source_System_Cutoff_Time
                        ).Single()
                    ),
                    p => p.Table_Name==Table_Name
                );
                t.Commit();
            }
            public static void MigrateStagedMovementData(Container2 e) {
                const String Table_Name = "Movement";
                var t = e.Transaction();
                var LineageKey = (
                    from Lineage in t.Integration.Lineage
                    where Lineage.Table_Name==Table_Name&&Lineage.Data_Load_Completed is null
                    orderby Lineage.Lineage_Key
                    select Lineage.Lineage_Key
                ).Single();
                t.Integration.Movement_Staging.UpdateWith(
                    m=>new Movement_Staging(
                        m.Movement_Staging_Key,
                        m.Date_Key,
                        (
                            from si in t.Dimension.Stock_Item
                            where m.WWI_Stock_Item_ID==si.WWI_Stock_Item_ID&&
                            m.Last_Modifed_When>si.Valid_From&&m.Last_Modifed_When<=si.Valid_To
                            select si.Stock_Item_Key
                        ).SingleOrDefault(0),
                        (
                            from c in t.Dimension.Customer
                            where m.WWI_Stock_Item_ID==c.WWI_Customer_ID&&
                            m.Last_Modifed_When>c.Valid_From&&m.Last_Modifed_When<=c.Valid_To
                            select c.Customer_Key
                        ).SingleOrDefault(0),
                        (
                            from si in t.Dimension.Supplier
                            where m.WWI_Stock_Item_ID==si.WWI_Supplier_ID&&
                            m.Last_Modifed_When>si.Valid_From&&m.Last_Modifed_When<=si.Valid_To
                            select si.Supplier_Key
                        ).SingleOrDefault(0),
                        (
                            from tt in t.Dimension.Transaction_Type
                            where m.WWI_Stock_Item_ID==tt.WWI_Transaction_Type_ID&&
                            m.Last_Modifed_When>tt.Valid_From&&m.Last_Modifed_When<=tt.Valid_To
                            select tt.Transaction_Type_Key
                        ).SingleOrDefault(0),
                        m.WWI_Stock_Item_Transaction_ID,
                        m.WWI_Invoice_ID,
                        m.WWI_Purchase_Order_ID,
                        m.Quantity,
                        m.WWI_Stock_Item_ID,
                        m.WWI_Customer_ID,
                        m.WWI_Supplier_ID,
                        m.WWI_Transaction_Type_ID,
                        m.Last_Modifed_When
                    )
                );
                var remove = from m in e.Fact.Movement
                             join ms in e.Integration.Movement_Staging on m.WWI_Stock_Item_Transaction_ID equals ms.WWI_Stock_Item_Transaction_ID
                             select (m,ms);
                var insert = e.Fact.Movement.Except(remove.Select(p => p.m));
                e.Fact.Movement.Assign(
                    (
                        from p in remove
                        select new Movement(
                            p.m.Movement_Key,
                            p.ms.Date_Key!.Value,
                            p.ms.Stock_Item_Key!.Value,
                            p.ms.Customer_Key,
                            p.ms.Supplier_Key,
                            p.ms.Transaction_Type_Key!.Value,
                            p.ms.WWI_Stock_Item_Transaction_ID!.Value,
                            p.ms.WWI_Invoice_ID,
                            p.ms.WWI_Purchase_Order_ID,
                            p.ms.Quantity!.Value,
                            LineageKey
                        )
                    ).Union(insert)
                );
                t.Integration.Lineage.UpdateWith(
                    p => new Lineage(
                        p.Lineage_Key,
                        p.Data_Load_Started,
                        p.Table_Name,
                        DateTime.Now,
                        true,
                        p.Source_System_Cutoff_Time
                    ),
                    p=>p.Lineage_Key==LineageKey
                );
                t.Integration.ETL_Cutoff.UpdateWith(
                    p => new ETL_Cutoff(
                        p.Table_Name,
                        (
                            from Lineage in e.Integration.Lineage
                            where Lineage.Lineage_Key==LineageKey
                            select Lineage.Source_System_Cutoff_Time
                        ).Single()
                    ),
                    p => p.Table_Name==Table_Name
                );
                t.Commit();
            }
            public static void MigrateStagedOrderData(Container2 e) {
                const String Table_Name = "Movement";
                var t = e.Transaction();
                var LineageKey = (
                    from Lineage in t.Integration.Lineage
                    where Lineage.Table_Name==Table_Name&&Lineage.Data_Load_Completed is null
                    orderby Lineage.Lineage_Key
                    select Lineage.Lineage_Key
                ).Single();
                t.Integration.Order_Staging.UpdateWith(
                    o => new Order_Staging(
                        o.Order_Staging_Key,
                        (
                            from c in t.Dimension.City
                            where o.WWI_City_ID==c.WWI_City_ID&&
                            o.Last_Modified_When>c.Valid_From&&o.Last_Modified_When<=c.Valid_To
                            select c.City_Key
                        ).SingleOrDefault(0),
                        (
                            from c in t.Dimension.Customer
                            where o.WWI_Stock_Item_ID==c.WWI_Customer_ID&&
                            o.Last_Modified_When>c.Valid_From&&o.Last_Modified_When<=c.Valid_To
                            select c.Customer_Key
                        ).SingleOrDefault(0),
                        (
                            from si in t.Dimension.Stock_Item
                            where o.WWI_Stock_Item_ID==si.WWI_Stock_Item_ID&&
                            o.Last_Modified_When>si.Valid_From&&o.Last_Modified_When<=si.Valid_To
                            select si.Stock_Item_Key
                        ).SingleOrDefault(0),
                        o.Order_Date_Key,
                        o.Picked_Date_Key,
                        (
                            from e in t.Dimension.Employee
                            where o.WWI_Salesperson_ID==e.WWI_Employee_ID&&
                            o.Last_Modified_When>e.Valid_From&&o.Last_Modified_When<=e.Valid_To
                            select e.Employee_Key
                        ).SingleOrDefault(0),
                        (
                            from e in t.Dimension.Employee
                            where o.WWI_Picker_ID==e.WWI_Employee_ID&&
                            o.Last_Modified_When>e.Valid_From&&o.Last_Modified_When<=e.Valid_To
                            select e.Employee_Key
                        ).SingleOrDefault(0),
                        o.WWI_Order_ID,
                        o.WWI_Backorder_ID,
                        o.Description,
                        o.Package,
                        o.Quantity,
                        o.Unit_Price,
                        o.Tax_Rate,
                        o.Total_Excluding_Tax,
                        o.Tax_Amount,
                        o.Total_Including_Tax,
                        o.Lineage_Key,
                        o.WWI_City_ID,
                        o.WWI_Customer_ID,
                        o.WWI_Stock_Item_ID,
                        o.WWI_Salesperson_ID,
                        o.WWI_Picker_ID,
                        o.Last_Modified_When
                    )
                );
                var remove = from m in e.Fact.Movement
                             join ms in e.Integration.Movement_Staging on m.WWI_Stock_Item_Transaction_ID equals ms.WWI_Stock_Item_Transaction_ID
                             select (m, ms);
                var insert = e.Fact.Movement.Except(remove.Select(p => p.m));
                e.Fact.Movement.Assign(
                    (
                        from p in remove
                        select new Movement(
                            p.m.Movement_Key,
                            p.ms.Date_Key!.Value,
                            p.ms.Stock_Item_Key!.Value,
                            p.ms.Customer_Key,
                            p.ms.Supplier_Key,
                            p.ms.Transaction_Type_Key!.Value,
                            p.ms.WWI_Stock_Item_Transaction_ID!.Value,
                            p.ms.WWI_Invoice_ID,
                            p.ms.WWI_Purchase_Order_ID,
                            p.ms.Quantity!.Value,
                            LineageKey
                        )
                    ).Union(insert)
                );
                t.Integration.Lineage.UpdateWith(
                    p => new Lineage(
                        p.Lineage_Key,
                        p.Data_Load_Started,
                        p.Table_Name,
                        DateTime.Now,
                        true,
                        p.Source_System_Cutoff_Time
                    ),
                    p => p.Lineage_Key==LineageKey
                );
                t.Integration.ETL_Cutoff.UpdateWith(
                    p => new ETL_Cutoff(
                        p.Table_Name,
                        (
                            from Lineage in e.Integration.Lineage
                            where Lineage.Lineage_Key==LineageKey
                            select Lineage.Source_System_Cutoff_Time
                        ).Single()
                    ),
                    p => p.Table_Name==Table_Name
                );
                t.Commit();
            }
            public static void MigrateStagedPaymentMethodData(Container2 e) {
                const String Table_Name = "Payment Method";
                var t = e.Transaction();
                var LineageKey = (
                    from Lineage in t.Integration.Lineage
                    where Lineage.Table_Name==Table_Name&&Lineage.Data_Load_Completed is null
                    orderby Lineage.Lineage_Key
                    select Lineage.Lineage_Key
                ).Single();
                var RowsToCloseOff =
                    from pm in t.Integration.PaymentMethod_Staging
                    group pm by pm.WWI_Payment_Method_ID
                    into g
                    select (WWI_Payment_Method_ID: g.Key, Valid_From: g.Min(pm => pm.Valid_From));
                t.Dimension.Payment_Method.Assign(
                    from c in t.Dimension.Payment_Method
                    join rtco in RowsToCloseOff on c.WWI_Payment_Method_ID equals rtco.WWI_Payment_Method_ID
                    where c.Valid_To==EndOfTime
                    select new Payment_Method(
                        c.Payment_Method_Key,
                        c.WWI_Payment_Method_ID,
                        c.Payment_Method,
                        c.Valid_From,
                        rtco.Valid_From,
                        c.Lineage_Key
                    )
                );
                t.Dimension.Payment_Method.AddRangeOrThrow(
                    from p in e.Integration.PaymentMethod_Staging
                    select new Payment_Method(
                        (Int32)t.Dimension.Payment_Method.Count,
                        p.WWI_Payment_Method_ID,
                        p.Payment_Method,
                        p.Valid_From,
                        p.Valid_To,
                        LineageKey
                    )
                );
                t.Integration.Lineage.UpdateWith(
                    p => new Lineage(
                        p.Lineage_Key,
                        p.Data_Load_Started,
                        p.Table_Name,
                        DateTime.Now,
                        true,
                        p.Source_System_Cutoff_Time
                    ),
                    p => p.Lineage_Key==LineageKey
                );
                t.Integration.ETL_Cutoff.UpdateWith(
                    p => new ETL_Cutoff(
                        p.Table_Name,
                        (
                            from Lineage in t.Integration.Lineage
                            where Lineage.Lineage_Key==LineageKey
                            select Lineage.Source_System_Cutoff_Time
                        ).Single()
                    ),
                    p => p.Table_Name==Table_Name
                );
                t.Commit();
            }
            public static void MigrateStagedPurchaseData(Container2 e) {
                const String Table_Name = "Purchase";
                var t = e.Transaction();
                var LineageKey = (
                    from Lineage in t.Integration.Lineage
                    where Lineage.Table_Name==Table_Name&&Lineage.Data_Load_Completed is null
                    orderby Lineage.Lineage_Key
                    select Lineage.Lineage_Key
                ).Single();
                t.Integration.Purchase_Staging.UpdateWith(
                    p => new Purchase_Staging(
                        p.Purchase_Staging_Key,
                        p.Date_Key,
                        (
                            from s in t.Dimension.Supplier
                            where p.WWI_Supplier_ID==s.WWI_Supplier_ID&&
                            p.Last_Modified_When>s.Valid_From&&p.Last_Modified_When<=s.Valid_To
                            select s.Supplier_Key
                        ).SingleOrDefault(0),
                        (
                            from si in t.Dimension.Stock_Item
                            where p.WWI_Stock_Item_ID==si.WWI_Stock_Item_ID&&
                            p.Last_Modified_When>si.Valid_From&&p.Last_Modified_When<=si.Valid_To
                            select si.Stock_Item_Key
                        ).SingleOrDefault(0),
                        p.WWI_Purchase_Order_ID,
                        p.Ordered_Outers,
                        p.Ordered_Quantity,
                        p.Received_Outers,
                        p.Package,
                        p.Is_Order_Finalized,
                        p.WWI_Supplier_ID,
                        p.WWI_Stock_Item_ID,
                        p.Last_Modified_When
                    )
                );
                e.Fact.Purchase.DeleteWith(
                    p=>(
                        from q in e.Integration.Purchase_Staging
                        select q.WWI_Purchase_Order_ID
                    ).Contains(p.WWI_Purchase_Order_ID)
                );
                e.Fact.Purchase.AddRangeOrThrow(
                    from p in e.Integration.Purchase_Staging
                    select new Purchase(
                        e.Fact.Purchase.Count,
                        p.Date_Key!.Value,
                        p.Supplier_Key!.Value,
                        p.Stock_Item_Key!.Value,
                        p.WWI_Purchase_Order_ID,
                        p.Ordered_Outers!.Value,
                        p.Ordered_Quantity!.Value,
                        p.Received_Outers!.Value,
                        p.Package,
                        p.Is_Order_Finalized!.Value,
                        LineageKey
                    )
                );
                t.Integration.Lineage.UpdateWith(
                    p => new Lineage(
                        p.Lineage_Key,
                        p.Data_Load_Started,
                        p.Table_Name,
                        DateTime.Now,
                        true,
                        p.Source_System_Cutoff_Time
                    ),
                    p => p.Lineage_Key==LineageKey
                );
                t.Integration.ETL_Cutoff.UpdateWith(
                    p => new ETL_Cutoff(
                        p.Table_Name,
                        (
                            from Lineage in e.Integration.Lineage
                            where Lineage.Lineage_Key==LineageKey
                            select Lineage.Source_System_Cutoff_Time
                        ).Single()
                    ),
                    p => p.Table_Name==Table_Name
                );
                t.Commit();
            }
            public static void MigrateStagedSaleData(Container2 e) {
                const String Table_Name = "Sale";
                var t = e.Transaction();
                var LineageKey = (
                    from Lineage in t.Integration.Lineage
                    where Lineage.Table_Name==Table_Name&&Lineage.Data_Load_Completed is null
                    orderby Lineage.Lineage_Key
                    select Lineage.Lineage_Key
                ).Single();
                t.Integration.Sale_Staging.UpdateWith(
                    s => new Sale_Staging(
                        s.Sale_Staging_Key,
                        (
                            from c in t.Dimension.City
                            where c.WWI_City_ID==s.WWI_City_ID&&
                            s.Last_Modified_When>c.Valid_From&&s.Last_Modified_When<=c.Valid_To
                            select c.City_Key
                        ).SingleOrDefault(0),
                        (
                            from c in t.Dimension.Customer
                            where c.WWI_Customer_ID==s.WWI_Customer_ID&&
                            s.Last_Modified_When>c.Valid_From&&s.Last_Modified_When<=c.Valid_To
                            select c.Customer_Key
                        ).SingleOrDefault(0),
                        (
                            from c in t.Dimension.Customer
                            where c.WWI_Customer_ID==s.WWI_Bill_To_Customer_ID&&
                            s.Last_Modified_When>c.Valid_From&&s.Last_Modified_When<=c.Valid_To
                            select c.Customer_Key
                        ).SingleOrDefault(0),
                        (
                            from si in t.Dimension.Stock_Item
                            where si.WWI_Stock_Item_ID==s.WWI_Stock_Item_ID&&
                            s.Last_Modified_When>si.Valid_From&&s.Last_Modified_When<=si.Valid_To
                            select si.Stock_Item_Key
                        ).SingleOrDefault(0),
                        s.Invoice_Date_Key,
                        s.Delivery_Date_Key,
                        (
                            from e in t.Dimension.Employee
                            where e.WWI_Employee_ID==s.WWI_Salesperson_ID&&
                            s.Last_Modified_When>e.Valid_From&&s.Last_Modified_When<=e.Valid_To
                            select e.Employee_Key
                        ).SingleOrDefault(0),
                        s.WWI_Invoice_ID,
                        s.Description,
                        s.Package,
                        s.Quantity,
                        s.Unit_Price,
                        s.Tax_Rate,
                        s.Total_Excluding_Tax,
                        s.Tax_Amount,
                        s.Profit,
                        s.Total_Including_Tax,
                        s.Total_Dry_Items,
                        s.Total_Chiller_Items,
                        s.WWI_City_ID,
                        s.WWI_Customer_ID,
                        s.WWI_Bill_To_Customer_ID,
                        s.WWI_Stock_Item_ID,
                        s.WWI_Salesperson_ID,
                        s.Last_Modified_When
                    )
                );
                t.Fact.Sale.DeleteWith(
                    p=>(
                        from a in e.Integration.Sale_Staging
                        select a.WWI_Invoice_ID
                    ).Contains(p.WWI_Invoice_ID)
                );
                t.Fact.Sale.AddRangeOrThrow(
                    from p in e.Integration.Sale_Staging
                    select new Sale(
                        t.Fact.Sale.Count,
                        p.City_Key!.Value,
                        p.Customer_Key!.Value,
                        p.Bill_To_Customer_Key!.Value,
                        p.Stock_Item_Key!.Value,
                        p.Invoice_Date_Key!.Value,
                        p.Delivery_Date_Key,
                        p.Salesperson_Key!.Value,
                        p.WWI_Invoice_ID!.Value,
                        p.Description,
                        p.Package,
                        p.Quantity!.Value,
                        p.Unit_Price!.Value,
                        p.Tax_Rate!.Value,
                        p.Total_Excluding_Tax!.Value,
                        p.Tax_Amount!.Value,
                        p.Profit!.Value,
                        p.Total_Including_Tax!.Value,
                        p.Total_Dry_Items!.Value,
                        p.Total_Chiller_Items!.Value,
                        LineageKey
                    )
                );
                t.Integration.Lineage.UpdateWith(
                    p => new Lineage(
                        p.Lineage_Key,
                        p.Data_Load_Started,
                        p.Table_Name,
                        DateTime.Now,
                        true,
                        p.Source_System_Cutoff_Time
                    ),
                    p => p.Lineage_Key==LineageKey
                );
                t.Integration.ETL_Cutoff.UpdateWith(
                    p => new ETL_Cutoff(
                        p.Table_Name,
                        (
                            from Lineage in t.Integration.Lineage
                            where Lineage.Lineage_Key==LineageKey
                            select Lineage.Source_System_Cutoff_Time
                        ).Single()
                    ),
                    p => p.Table_Name==Table_Name
                );
                t.Commit();
            }
            public static void MigrateStagedStockHoldingData(Container2 e) {
                const String Table_Name = "Stock Holding";
                var t = e.Transaction();
                var LineageKey = (
                    from Lineage in t.Integration.Lineage
                    where Lineage.Table_Name==Table_Name&&Lineage.Data_Load_Completed is null
                    orderby Lineage.Lineage_Key
                    select Lineage.Lineage_Key
                ).Single();
                t.Integration.StockHolding_Staging.UpdateWith(
                    s => new StockHolding_Staging(
                        s.Stock_Holding_Staging_Key,
                        (
                            from si in t.Dimension.Stock_Item
                            where si.WWI_Stock_Item_ID==si.WWI_Stock_Item_ID
                            select si.Stock_Item_Key
                        ).SingleOrDefault(0),
                        s.Quantity_On_Hand,
                        s.Bin_Location,
                        s.Last_Stocktake_Quantity,
                        s.Last_Cost_Price,
                        s.Reorder_Level,
                        s.Target_Stock_Level,
                        s.WWI_Stock_Item_ID
                    )
                );
                t.Fact.Stock_Holding.Clear();
                t.Fact.Stock_Holding.AddRangeOrThrow(
                    from p in e.Integration.StockHolding_Staging
                    select new Stock_Holding(
                        t.Fact.Stock_Holding.Count,
                        p.Stock_Item_Key!.Value,
                        p.Quantity_On_Hand!.Value,
                        p.Bin_Location,
                        p.Last_Stocktake_Quantity!.Value,
                        p.Last_Cost_Price!.Value,
                        p.Reorder_Level!.Value,
                        p.Target_Stock_Level!.Value,
                        LineageKey
                    )
                );
                t.Integration.Lineage.UpdateWith(
                    p => new Lineage(
                        p.Lineage_Key,
                        p.Data_Load_Started,
                        p.Table_Name,
                        DateTime.Now,
                        true,
                        p.Source_System_Cutoff_Time
                    ),
                    p => p.Lineage_Key==LineageKey
                );
                t.Integration.ETL_Cutoff.UpdateWith(
                    p => new ETL_Cutoff(
                        p.Table_Name,
                        (
                            from Lineage in t.Integration.Lineage
                            where Lineage.Lineage_Key==LineageKey
                            select Lineage.Source_System_Cutoff_Time
                        ).Single()
                    ),
                    p => p.Table_Name==Table_Name
                );
                t.Commit();
            }
            public static void MigrateStagedStockItemData(Container2 e) {
                const String Table_Name = "Stock Item";
                var t = e.Transaction();
                var LineageKey = (
                    from Lineage in t.Integration.Lineage
                    where Lineage.Table_Name==Table_Name&&Lineage.Data_Load_Completed is null
                    orderby Lineage.Lineage_Key
                    select Lineage.Lineage_Key
                ).Single();
                var RowsToCloseOff =
                    from c in t.Integration.StockItem_Staging
                    group c by c.WWI_Stock_Item_ID
                    into g
                    select (WWI_Stock_Item_ID: g.Key, Valid_From: g.Min(c => c.Valid_From));
                t.Dimension.Stock_Item.Assign(
                    from s in t.Dimension.Stock_Item
                    join rtco in RowsToCloseOff on s.WWI_Stock_Item_ID equals rtco.WWI_Stock_Item_ID
                    where s.Valid_To==EndOfTime
                    select new Stock_Item(
                        s.Stock_Item_Key,
                        s.WWI_Stock_Item_ID,
                        s.Stock_Item,
                        s.Color,
                        s.Selling_Package,
                        s.Buying_Package,
                        s.Brand,
                        s.Size,
                        s.Lead_Time_Days,
                        s.Quantity_Per_Outer,
                        s.Is_Chiller_Stock,
                        s.Barcode,
                        s.Tax_Rate,
                        s.Unit_Price,
                        s.Recommended_Retail_Price,
                        s.Typical_Weight_Per_Unit,
                        s.Photo,
                        s.Valid_From,
                        rtco.Valid_From,
                        LineageKey
                    )
                );
                t.Dimension.Stock_Item.AddRangeOrThrow(
                    from c in t.Integration.StockItem_Staging
                    select new Stock_Item(
                        (Int32)t.Dimension.Stock_Item.Count,
                        c.WWI_Stock_Item_ID,
                        c.Stock_Item,
                        c.Color,
                        c.Selling_Package,
                        c.Buying_Package,
                        c.Brand,
                        c.Size,
                        c.Lead_Time_Days,
                        c.Quantity_Per_Outer,
                        c.Is_Chiller_Stock,
                        c.Barcode,
                        c.Tax_Rate,
                        c.Unit_Price,
                        c.Recommended_Retail_Price,
                        c.Typical_Weight_Per_Unit,
                        c.Photo,
                        c.Valid_From,
                        c.Valid_To,
                        LineageKey
                    )
                );
                t.Integration.Lineage.UpdateWith(
                    p => new Lineage(
                        p.Lineage_Key,
                        p.Data_Load_Started,
                        p.Table_Name,
                        DateTime.Now,
                        true,
                        p.Source_System_Cutoff_Time
                    ),
                    p => p.Lineage_Key==LineageKey
                );
                t.Integration.ETL_Cutoff.UpdateWith(
                    p => new ETL_Cutoff(
                        p.Table_Name,
                        (
                            from Lineage in t.Integration.Lineage
                            where Lineage.Lineage_Key==LineageKey
                            select Lineage.Source_System_Cutoff_Time
                        ).Single()
                    ),
                    p => p.Table_Name==Table_Name
                );
                t.Commit();
            }
            public static void MigrateStagedSupplierData(Container2 e) {
                const String Table_Name = "Supplier";
                var t = e.Transaction();
                var LineageKey = (
                    from Lineage in t.Integration.Lineage
                    where Lineage.Table_Name==Table_Name&&Lineage.Data_Load_Completed is null
                    orderby Lineage.Lineage_Key
                    select Lineage.Lineage_Key
                ).Single();
                var RowsToCloseOff =
                    from c in t.Integration.Supplier_Staging
                    group c by c.WWI_Supplier_ID
                    into g
                    select (WWI_Supplier_ID: g.Key, Valid_From: g.Min(c => c.Valid_From));
                t.Dimension.Supplier.Assign(
                    from s in t.Dimension.Supplier
                    join rtco in RowsToCloseOff on s.WWI_Supplier_ID equals rtco.WWI_Supplier_ID
                    where s.Valid_To==EndOfTime
                    select new Supplier(
                        s.Supplier_Key,
                        s.WWI_Supplier_ID,
                        s.Supplier,
                        s.Category,
                        s.Primary_Contact,
                        s.Supplier_Reference,
                        s.Payment_Days,
                        s.Postal_Code,
                        s.Valid_From,
                        s.Valid_To,
                        LineageKey
                    )
                );
                t.Integration.Lineage.UpdateWith(
                    p => new Lineage(
                        p.Lineage_Key,
                        p.Data_Load_Started,
                        p.Table_Name,
                        DateTime.Now,
                        true,
                        p.Source_System_Cutoff_Time
                    ),
                    p => p.Lineage_Key==LineageKey
                );
                t.Integration.ETL_Cutoff.UpdateWith(
                    p => new ETL_Cutoff(
                        p.Table_Name,
                        (
                            from Lineage in t.Integration.Lineage
                            where Lineage.Lineage_Key==LineageKey
                            select Lineage.Source_System_Cutoff_Time
                        ).Single()
                    ),
                    p => p.Table_Name==Table_Name
                );
                t.Commit();
            }
            public static void MigrateStagedTransactionData(Container2 e) {
                const String Table_Name = "Transaction";
                var t = e.Transaction();
                var LineageKey = (
                    from Lineage in t.Integration.Lineage
                    where Lineage.Table_Name==Table_Name&&Lineage.Data_Load_Completed is null
                    orderby Lineage.Lineage_Key
                    select Lineage.Lineage_Key
                ).Single();
                t.Integration.Transaction_Staging.UpdateWith(
                    t=>new Transaction_Staging(
                        t.Transaction_Staging_Key,
                        t.Date_Key,
                        (
                            from c in e.Dimension.Customer
                            where t.WWI_Customer_ID==c.WWI_Customer_ID&&
                            t.Last_Modified_When>c.Valid_From&&t.Last_Modified_When<=c.Valid_To
                            select c.Customer_Key
                        ).SingleOrDefault(0),
                        t.Bill_To_Customer_Key,
                        (
                            from c in e.Dimension.Supplier
                            where t.WWI_Supplier_ID==c.WWI_Supplier_ID&&
                            t.Last_Modified_When>c.Valid_From&&t.Last_Modified_When<=c.Valid_To
                            select c.Supplier_Key
                        ).SingleOrDefault(0),
                        (
                            from tt in e.Dimension.Transaction_Type
                            where t.WWI_Transaction_Type_ID==tt.WWI_Transaction_Type_ID&&
                            t.Last_Modified_When>tt.Valid_From&&t.Last_Modified_When<=tt.Valid_To
                            select tt.Transaction_Type_Key
                        ).SingleOrDefault(0),
                        (
                            from pm in e.Dimension.Payment_Method
                            where t.WWI_Payment_Method_ID==pm.WWI_Payment_Method_ID&&
                            t.Last_Modified_When>pm.Valid_From&&t.Last_Modified_When<=pm.Valid_To
                            select pm.Payment_Method_Key
                        ).SingleOrDefault(0),
                        t.WWI_Customer_Transaction_ID,
                        t.WWI_Supplier_Transaction_ID,
                        t.WWI_Invoice_ID,
                        t.WWI_Purchase_Order_ID,
                        t.Supplier_Invoice_Number,
                        t.Total_Excluding_Tax,
                        t.Tax_Amount,
                        t.Total_Including_Tax,
                        t.Outstanding_Balance,
                        t.Is_Finalized,
                        t.WWI_Customer_ID,
                        t.WWI_Bill_To_Customer_ID,
                        t.WWI_Supplier_ID,
                        t.WWI_Transaction_Type_ID,
                        t.WWI_Payment_Method_ID,
                        t.Last_Modified_When
                    )
                );
                t.Integration.Lineage.UpdateWith(
                    p => new Lineage(
                        p.Lineage_Key,
                        p.Data_Load_Started,
                        p.Table_Name,
                        DateTime.Now,
                        true,
                        p.Source_System_Cutoff_Time
                    ),
                    p => p.Lineage_Key==LineageKey
                );
                t.Integration.ETL_Cutoff.UpdateWith(
                    p => new ETL_Cutoff(
                        p.Table_Name,
                        (
                            from Lineage in t.Integration.Lineage
                            where Lineage.Lineage_Key==LineageKey
                            select Lineage.Source_System_Cutoff_Time
                        ).Single()
                    ),
                    p => p.Table_Name==Table_Name
                );
                t.Commit();
            }
            public static void MigrateStagedTransactionTypeData(Container2 e) {
                const String Table_Name = "Transaction Type";
                var t = e.Transaction();
                var LineageKey = (
                    from Lineage in t.Integration.Lineage
                    where Lineage.Table_Name==Table_Name&&Lineage.Data_Load_Completed is null
                    orderby Lineage.Lineage_Key
                    select Lineage.Lineage_Key
                ).Single();
                var RowsToCloseOff =
                    from pm in t.Integration.TransactionType_Staging
                    group pm by pm.WWI_Transaction_Type_ID
                    into g
                    select (WWI_Transaction_Type_ID: g.Key, Valid_From: g.Min(c => c.Valid_From));
                t.Dimension.Transaction_Type.Assign(
                    from s in t.Dimension.Transaction_Type
                    join rtco in RowsToCloseOff on s.WWI_Transaction_Type_ID equals rtco.WWI_Transaction_Type_ID
                    where s.Valid_To==EndOfTime
                    select new Transaction_Type(
                        s.Transaction_Type_Key,
                        s.WWI_Transaction_Type_ID,
                        s.Transaction_Type,
                        s.Valid_From,
                        s.Valid_To,
                        LineageKey
                    )
                );
                t.Dimension.Transaction_Type.AddRange(
                    from c in t.Integration.TransactionType_Staging
                    select new Transaction_Type(
                        (Int32)t.Dimension.Transaction_Type.Count,
                        c.WWI_Transaction_Type_ID,
                        c.Transaction_Type,
                        c.Valid_From,
                        c.Valid_To,
                        LineageKey
                    )
                );
                t.Integration.Lineage.UpdateWith(
                    p => new Lineage(
                        p.Lineage_Key,
                        p.Data_Load_Started,
                        p.Table_Name,
                        DateTime.Now,
                        true,
                        p.Source_System_Cutoff_Time
                    ),
                    p => p.Lineage_Key==LineageKey
                );
                t.Integration.ETL_Cutoff.UpdateWith(
                    p => new ETL_Cutoff(
                        p.Table_Name,
                        (
                            from Lineage in t.Integration.Lineage
                            where Lineage.Lineage_Key==LineageKey
                            select Lineage.Source_System_Cutoff_Time
                        ).Single()
                    ),
                    p => p.Table_Name==Table_Name
                );
                t.Commit();
            }
            private static ImmutableSet<Date> GenerateDateDimensionColumns(DateTime Date)=>
                new Set<Date>{
                    new Date(
                        Date,
                        Date.Day,
                        Date.Day.ToString(),
                        Date.Month.ToString(),
                        ToShortMonthName(Date),
                        Date.Month,
                        "CY"+Date.Year+'-'+ToShortMonthName(Date),
                        Date.Year,
                        "CY"+Date.Year,
                        Date.Month==11||Date.Month==12 ? Date.Month-10 : Date.Month+2,
                        "FY"+(Date.Month==11||Date.Month==12 ? Date.Month+1 : Date.Month)+'-'+ToShortMonthName(Date),
                        Date.Month==11||Date.Month==12 ? Date.Year-1 : Date.Year,
                        "FY"+(Date.Month==11||Date.Month==12 ? Date.Year-1 : Date.Year),
                        (Int32)Date.DayOfWeek
                    )
                };
            public static void PopulateDateDimensionForYear(Container2 e,Int32 YearNumber){
                var DateCounter=new DateTime(YearNumber,1,1);
                try{
                    //Transactrion
                    while(DateCounter.Year==YearNumber){
                        if(!(from Date in e.Dimension.Date where Date.Date==DateCounter select 1).Any()){
                            e.Dimension.Date.Insert(GenerateDateDimensionColumns(DateCounter));
                        }
                        DateCounter=DateCounter.AddDays(1);
                    }
                } catch{
                    Trace.WriteLine("Unable to populate dates for the year");
                    throw;
                }
            }
        }
        private static class Sequences {
            public static void ReseedAllSequences(Container2 e) {
                ReseedSequenceBeyondTableValues(e,"CityKey","Dimension","City","City Key");
                ReseedSequenceBeyondTableValues(e,"CustomerKey","Dimension","Customer","Customer Key");
                ReseedSequenceBeyondTableValues(e,"EmployeeKey","Dimension","Employee","Employee Key");
                ReseedSequenceBeyondTableValues(e,"LineageKey","Integration","Lineage","Lineage Key");
                ReseedSequenceBeyondTableValues(e,"PaymentMethodKey","Dimension","Payment Method","Payment Method Key");
                ReseedSequenceBeyondTableValues(e,"StockItemKey","Dimension","Stock Item","Stock Item Key");
                ReseedSequenceBeyondTableValues(e,"SupplierKey","Dimension","Supplier","Supplier Key");
                ReseedSequenceBeyondTableValues(e,"TransactionTypeKey","Dimension","Transaction Type","Transaction Type Key");
            }
            //private class 引数<T> {
            //    public readonly T @this;
            //    public 引数(T @this) => this.@this=@this;
            //}
            static Object 共通(String ステートメント,Object 引数) {
                var Script = CSharpScript.Create(
                    ステートメント,
                    ScriptOptions.Default.AddReferences(
                        typeof(System.Object).Assembly,
                        typeof(System.Linq.Enumerable).Assembly,
                        typeof(Program).Assembly,
                        引数.GetType().Assembly)
                    .AddImports("System","System.Linq","文字列コード実行"),
                    引数.GetType()
                );
                var ScriptRunner = Script.CreateDelegate();
                var result = ScriptRunner(引数);
                //var result = script.RunAsync(引数);
                result.Wait();
                Console.WriteLine(result.Result);
                return result.Result;
            }
            public static void ReseedSequenceBeyondTableValues(Container2 e,String SequenceName,String SchemaName,String TableName,String ColumnName) {
                //var CurrentSequenceMaximumValue = 3;// (from g in e.information_schema.s)
                var CurrentTableMaximumValue = (Int64)共通(
                    $"AddRange(\r\n"+
                    $"    (from a in {SchemaName}.{TableName} select a).Max(a=>a.{ColumnName})\r\n"+
                    $")\r\n",
                    e
                );
                Console.WriteLine(SequenceName);
                //if(@CurrentTableMaximumValue>=CurrentSequenceMaximumValue) {
                //    var NewSequenceValue = CurrentTableMaximumValue+1;
                //    SET @SQL = N'ALTER SEQUENCE Sequences.'+QUOTENAME(@SequenceName)+N' RESTART WITH '+CAST(@NewSequenceValue AS nvarchar(20))+N';';
                //    EXECUTE(@SQL);
                //}

            }
        }

        private static void Main() {
            var r = new Random(2);
            Transaction(() => r.Next());
            var index = 0;
            Transaction(() => index++);
            Create();
            Load();
#pragma warning disable CA1303 // ローカライズされるパラメーターとしてリテラルを渡さない
            Console.Write("終了");
#pragma warning restore CA1303 // ローカライズされるパラメーターとしてリテラルを渡さない
            Console.ReadKey();
        }
    }
}