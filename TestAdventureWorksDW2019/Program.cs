using System;
using System.Data.SqlClient;
using System.Linq;
using LinqDB.Sets;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using AdventureWorksDW2019;
using AdventureWorksDW2019.Tables.dbo;
using System.Collections.Generic;

namespace TestAdventureWorksDW2019;
abstract class Program :共通{
    ///// <summary>アンマネージ リソースの解放またはリセットに関連付けられているアプリケーション定義のタスクを実行します。</summary>
    ///// <filterpriority>2</filterpriority>
    //public void Dispose() {
    //    this.Connection.Dispose();
    //    this.Command.Dispose();
    //    GC.SuppressFinalize(this);
    //}
    private static readonly CultureInfo en_GB= new("en-GB");
    private static readonly CultureInfo es_ES = new("es-ES");
    private static readonly CultureInfo fr_FR = new("fr-FR");
    [SuppressMessage("Globalization","CA1305:IFormatProvider を指定します",Justification = "<保留中>")]
    [SuppressMessage("Globalization","CA1303:ローカライズされるパラメーターとしてリテラルを渡さない",Justification = "<保留中>")]
    [SuppressMessage("ReSharper","StringLiteralTypo")]
    [SuppressMessage("ReSharper","VariableHidesOuterVariable")]
    [SuppressMessage("ReSharper","AccessToModifiedClosure")]
    private static void Create() {
        using var e =new Container();
        var dbo = e.dbo;
        var DimAccount = dbo.DimAccount;
        var DimCurrency = dbo.DimCurrency;
        var DimDate = dbo.DimDate;
        var DimProductCategory = dbo.DimProductCategory;
        var DimProductSubcategory = dbo.DimProductSubcategory;
        var DimProduct = dbo.DimProduct;
        var DimPromotion = dbo.DimPromotion;
        var DimSalesReason = dbo.DimSalesReason;
        var DimSalesTerritory = dbo.DimSalesTerritory;
        var DimEmployee = dbo.DimEmployee;
        var DimGeography = dbo.DimGeography;
        var DimCustomer = dbo.DimCustomer;
        var DimReseller = dbo.DimReseller;
        var DimScenario = dbo.DimScenario;
        var FactSurveyResponse = dbo.FactSurveyResponse;
        var FactCallCenter = dbo.FactCallCenter;
        var DimOrganization = dbo.DimOrganization;
        var DimDepartmentGroup = dbo.DimDepartmentGroup;
        var FactFinance = dbo.FactFinance;
        var FactCurrencyRate = dbo.FactCurrencyRate;
        var FactInternetSales = dbo.FactInternetSales;
        var FactInternetSalesReason = dbo.FactInternetSalesReason;
        var FactProductInventory = dbo.FactProductInventory;
        var FactResellerSales = dbo.FactResellerSales;
        var FactSalesQuota = dbo.FactSalesQuota;
        var s = Stopwatch.StartNew();
        //LV0
        DimAccount.AddOrThrow(
            new DimAccount(
                1,
                null,
                1,
                null,
                "",
                "",
                "",
                "",
                "",
                ""
            )
        );
        for(var AccountKey = 2;AccountKey<=2;AccountKey++)
            DimAccount.AddOrThrow(
                new DimAccount(
                    AccountKey,
                    AccountKey-1,
                    AccountKey*2,
                    (AccountKey-1)*2,
                    "",
                    "",
                    "",
                    "",
                    "",
                    ""
                )
            );
        for(var CurrencyKey = 1;CurrencyKey<=2;CurrencyKey++)
            DimCurrency.AddOrThrow(
                new DimCurrency(
                    CurrencyKey,
                    "",
                    ""
                )
            );
        for(var DateKey=1;DateKey<=2;DateKey++){
            var Date=new DateTime(2001,1,1).AddDays(DateKey);
            DimDate.AddOrThrow(
                new DimDate(
                    DateKey,
                    Date,
                    (byte)Date.DayOfWeek,
                    Date.ToString("dddd",en_GB),
                    Date.ToString("dddd",es_ES),
                    Date.ToString("dddd",fr_FR),
                    (byte)Date.Day,
                    (short)Date.DayOfYear,
                    (byte)en_GB.Calendar.GetWeekOfYear(Date,CalendarWeekRule.FirstDay,DayOfWeek.Sunday),
                    Date.ToString("MMMM",en_GB),
                    Date.ToString("MMMM",es_ES),
                    Date.ToString("MMMM",fr_FR),
                    (byte)Date.Month,
                    (byte)((Date.Month+3)/4),
                    (short)Date.Year,
                    0,
                    0,
                    0,
                    0
                )
            );
        }
        var ProductSubcategoryKey =1;
        var ProductKey=1;
        for(var ProductCategoryKey=1;ProductCategoryKey<=2;ProductCategoryKey++){
            DimProductCategory.AddOrThrow(
                new DimProductCategory(
                    ProductCategoryKey,
                    null,
                    "",
                    "",
                    ""
                )
            );
            for(var a= 1;a<=2;a++){
                DimProductSubcategory.AddOrThrow(
                    new DimProductSubcategory(
                        ProductSubcategoryKey,
                        null,
                        "",
                        "",
                        "",
                        ProductCategoryKey
                    )
                );
                for(var b=1;b<=2;b++){
                    DimProduct.AddOrThrow(
                        new DimProduct(
                            ProductKey++,
                            null,
                            ProductSubcategoryKey,
                            null,
                            null,
                            "",
                            "",
                            "",
                            null,
                            false,
                            "",null,
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
                ProductSubcategoryKey++;
            }
        }
        for(var PromotionKey=1;PromotionKey<=2;PromotionKey++){
            DimPromotion.AddOrThrow(
                new DimPromotion(
                    PromotionKey,
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
                    new DateTime(2001,1,1).AddDays(PromotionKey),
                    new DateTime(2001,1,1).AddDays(PromotionKey+1),
                    null,
                    null
                )
            );
        }
        for(var SalesReasonKey = 1;SalesReasonKey<=2;SalesReasonKey++)
            DimSalesReason.AddOrThrow(
                new DimSalesReason(
                    SalesReasonKey,
                    SalesReasonKey,
                    "",
                    ""
                )
            );
        var GeographyKey =1;
        var EmployeeKey=1;
        var CustomerKey=1;
        var ResellerKey=1;
        for(var SalesTerritoryKey = 1;SalesTerritoryKey<=2;SalesTerritoryKey++){
            DimSalesTerritory.AddOrThrow(
                new DimSalesTerritory(
                    SalesTerritoryKey,
                    SalesTerritoryKey,
                    "",
                    "",
                    "",
                    null
                )
            );
            for(var a=1;a<=2;a++){
                DimEmployee.AddOrThrow(
                    new DimEmployee(
                        EmployeeKey,
                        EmployeeKey==1?null:(int?)EmployeeKey-1,
                        null,
                        null,
                        SalesTerritoryKey,
                        null,
                        null,
                        null,
                        false,
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
                        false,
                        false,
                        null,
                        null,
                        null,
                        null,
                        null
                    )
                );
                EmployeeKey++;
            }
            for(var a=1;a<=2;a++){
                DimGeography.AddOrThrow(
                    new DimGeography(
                        GeographyKey,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        SalesTerritoryKey,
                        null
                    )
                );
                for(var b=1;b<=2;b++){
                    DimCustomer.AddOrThrow(
                        new DimCustomer(
                            CustomerKey++,
                            GeographyKey,
                            "",
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
                            null,
                            null
                        )
                    );
                }
                for(var b = 1;b<=2;b++) {
                    DimReseller.AddOrThrow(
                        new DimReseller(
                            ResellerKey++,
                            GeographyKey,
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
                GeographyKey++;
            }
        }
        for(var ScenarioKey= 1;ScenarioKey<=2;ScenarioKey++)
            DimScenario.AddOrThrow(
                new DimScenario(
                    ScenarioKey,
                    null
                )
            );
        var SurveyResponseKey=1;
        E(
            DimDate,
            Date=>E(
                DimCustomer,
                Customer=>E(
                    DimProductCategory,
                    ProductCategory=>E(
                        DimProductCategory,
                        ProductCategory=>E(
                            DimProductSubcategory,
                            ProductSubcategory=>FactSurveyResponse.AddOrThrow(
                                new FactSurveyResponse(
                                    SurveyResponseKey++,
                                    Date.DateKey,
                                    Customer.CustomerKey,
                                    ProductCategory.ProductCategoryKey,
                                    ProductCategory.EnglishProductCategoryName,
                                    ProductSubcategory.ProductSubcategoryKey,
                                    ProductSubcategory.EnglishProductSubcategoryName,
                                    null
                                )
                            )
                        )
                    )
                )
            )
        );
        var FactCallCenterID= 1;
        E(
            DimDate,
            Date => FactCallCenter.AddOrThrow(
                new FactCallCenter(
                    FactCallCenterID++,
                    Date.DateKey,
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
                    null
                )
            )
        );
        var OrganaizationKey=1;
        E(
            DimCurrency,
            Currency =>{
                DimOrganization.AddOrThrow(
                    new DimOrganization(
                        OrganaizationKey,
                        OrganaizationKey==1?null:(int?)OrganaizationKey-1,
                        "",
                        "",
                        Currency.CurrencyKey
                    )
                );
                OrganaizationKey++;
            });
        for(var DepartmentGroupKey=1;DepartmentGroupKey<=2;DepartmentGroupKey++)
            DimDepartmentGroup.AddOrThrow(
                new DimDepartmentGroup(
                    DepartmentGroupKey,
                    DepartmentGroupKey==1?null:(int?)DepartmentGroupKey-1,
                    ""
                )
            );
        var FinanceKey=1;
        E(
            DimDate,
            Date => E(
                DimDate,
                Date => E(
                    DimOrganization,
                    Organaization => E(
                        DimDepartmentGroup,
                        DepartmentGroup=>E(
                            DimScenario,
                            Scenario=>E(
                                DimAccount,
                                Account=>FactFinance.AddOrThrow(
                                    new FactFinance(
                                        FinanceKey++,
                                        Date.DateKey,
                                        Organaization.OrganizationKey,
                                        DepartmentGroup.DepartmentGroupKey,
                                        Scenario.ScenarioKey,
                                        Account.AccountKey,
                                        0,
                                        null
                                    )
                                )
                            )
                        )
                    )
                )
            )
        );
        E(
            DimCurrency,
            Currency => E(
                DimDate,
                Date => FactCurrencyRate.AddOrThrow(
                    new FactCurrencyRate(
                        Currency.CurrencyKey,
                        Date.DateKey,
                        0,
                        0,
                        Date.FullDateAlternateKey
                    )
                )
            )
        );
        var SalesOrderNumber =1;
        E(
            DimProduct,
            Product=>E(
                DimDate,
                OrderDate=>E(
                    DimDate,
                    DueDate=>E(
                        DimDate,
                        ShipDate=>E(
                            DimCustomer,
                            Customer=>E(
                                DimPromotion,
                                Promotion=>E(
                                    DimCurrency,
                                    Currency=>E(
                                        DimSalesTerritory,
                                        SalesTerritory=>{
                                            for(var a=1;a<=2;a++){
                                                for(var SalesOrderLineNumber= 1;SalesOrderLineNumber<=2;SalesOrderLineNumber++){
                                                    FactInternetSales.AddOrThrow(
                                                        new FactInternetSales(
                                                            Product.ProductKey,
                                                            OrderDate.DateKey,
                                                            DueDate.DateKey,
                                                            ShipDate.DateKey,
                                                            Customer.CustomerKey,
                                                            Promotion.PromotionKey,
                                                            Currency.CurrencyKey,
                                                            SalesTerritory.SalesTerritoryKey,
                                                            SalesOrderNumber.ToString(),
                                                            (byte)SalesOrderLineNumber,
                                                            0,
                                                            0,
                                                            0,
                                                            0,
                                                            0,
                                                            0,
                                                            0,
                                                            0,
                                                            0,
                                                            0,
                                                            0,
                                                            null,
                                                            null,
                                                            null,
                                                            null,
                                                            null
                                                        )
                                                    );
                                                    E(
                                                        DimSalesReason,
                                                        SalesReason=>FactInternetSalesReason.AddOrThrow(
                                                            new FactInternetSalesReason(
                                                                SalesOrderNumber.ToString(),
                                                                (byte)SalesOrderLineNumber,
                                                                SalesReason.SalesReasonKey
                                                            )
                                                        )
                                                    );
                                                }
                                                SalesOrderNumber++;
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
        E(
            DimProduct,
            Product=>E(
                DimDate,
                Date=>FactProductInventory.AddOrThrow(
                    new FactProductInventory(
                        Product.ProductKey,
                        Date.DateKey,
                        Date.FullDateAlternateKey,
                        0,
                        0,
                        0,
                        0
                    )
                )
            )
        );
        SalesOrderNumber=1;
        E(
            DimProduct,
            Product => E(
                DimDate,
                OrderDate => E(
                    DimDate,
                    DueDate => E(
                        DimDate,
                        ShipDate => E(
                            DimReseller,
                            Reseller => E(
                                DimEmployee,
                                Employee=>E(
                                    DimPromotion,
                                    Promotion => E(
                                        DimCurrency,
                                        Currency => E(
                                            DimSalesTerritory,
                                            SalesTerritory=>{
                                                for(var SalesOrderLineNumber=1;SalesOrderLineNumber<=2;SalesOrderLineNumber++){
                                                    FactResellerSales.Add(
                                                        new FactResellerSales(
                                                            Product.ProductKey,
                                                            OrderDate.DateKey,
                                                            DueDate.DateKey,
                                                            ShipDate.DateKey,
                                                            Reseller.ResellerKey,
                                                            Employee.EmployeeKey,
                                                            Promotion.PromotionKey,
                                                            Currency.CurrencyKey,
                                                            SalesTerritory.SalesTerritoryKey,
                                                            SalesOrderNumber++.ToString(),
                                                            (byte)SalesOrderLineNumber,
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
                                            }
                                        )
                                    )
                                )
                            )
                        )
                    )
                )
            )
        );
        var SalesQuotaKey=1;
        E(
            DimEmployee,
            Employee=>E(
                DimDate,
                Date=>FactSalesQuota.Add(
                    new FactSalesQuota(
                        SalesQuotaKey++,
                        Employee.EmployeeKey,
                        Date.DateKey,
                        2001,
                        1,
                        0,
                        null
                    )
                )
            )
        );
        Console.WriteLine($"Add {s.ElapsedMilliseconds,7}ms");
    }

    [SuppressMessage("Globalization","CA1305:IFormatProvider を指定します",Justification = "<保留中>")]
    private static void Transaction(Func<int> Switchパターン) {
        const int 試行回数 = 10000;
        using var e = new Container();
        var dbo = e.dbo;
        var DimAccount = dbo.DimAccount;
        var DimCurrency = dbo.DimCurrency;
        var DimDate = dbo.DimDate;
        var DimProductCategory = dbo.DimProductCategory;
        var DimProductSubcategory = dbo.DimProductSubcategory;
        var DimProduct = dbo.DimProduct;
        var DimPromotion = dbo.DimPromotion;
        var DimSalesReason = dbo.DimSalesReason;
        var DimSalesTerritory = dbo.DimSalesTerritory;
        var DimEmployee = dbo.DimEmployee;
        var DimGeography = dbo.DimGeography;
        var DimCustomer = dbo.DimCustomer;
        var DimReseller = dbo.DimReseller;
        var DimScenario = dbo.DimScenario;
        var FactSurveyResponse = dbo.FactSurveyResponse;
        var FactCallCenter = dbo.FactCallCenter;
        var DimOrganization = dbo.DimOrganization;
        var DimDepartmentGroup = dbo.DimDepartmentGroup;
        var FactFinance = dbo.FactFinance;
        var FactCurrencyRate = dbo.FactCurrencyRate;
        var FactInternetSales = dbo.FactInternetSales;
        var FactInternetSalesReason = dbo.FactInternetSalesReason;
        var FactProductInventory = dbo.FactProductInventory;
        var FactResellerSales = dbo.FactResellerSales;
        var FactSalesQuota = dbo.FactSalesQuota;

        var DimAccount情報 = new AddDel情報();
        var DimCurrency情報 = new AddDel情報();
        var DimDate情報 = new AddDel情報();
        var DimProductCategory情報 = new AddDel情報();
        var DimProductSubcategory情報 = new AddDel情報();
        var DimProduct情報 = new AddDel情報();
        var DimPromotion情報 = new AddDel情報();
        var DimSalesReason情報 = new AddDel情報();
        var DimSalesTerritory情報 = new AddDel情報();
        var DimEmployee情報 = new AddDel情報();
        var DimGeography情報 = new AddDel情報();
        var DimCustomer情報 = new AddDel情報();
        var DimReseller情報 = new AddDel情報();
        var DimScenario情報 = new AddDel情報();
        var FactSurveyResponse情報 = new AddDel情報();
        var FactCallCenter情報 = new AddDel情報();
        var DimOrganization情報 = new AddDel情報();
        var DimDepartmentGroup情報 = new AddDel情報();
        var FactFinance情報 = new AddDel情報();
        var FactCurrencyRate情報 = new AddDel情報();
        var FactInternetSales情報 = new AddDel情報();
        var FactInternetSalesReason情報 = new AddDel情報();
        var FactProductInventory情報 = new AddDel情報();
        var FactResellerSales情報 = new AddDel情報();
        var FactSalesQuota情報 = new AddDel情報();
        var s = Stopwatch.StartNew();
        for(var a=0;a<試行回数;a++){
            switch(Switchパターン()%25) {
                case 0: Add(ref DimAccount情報,DimAccount,new DimAccount(ID(DimAccount),null,null,null,null,null,null,null,null,null)); break;
                case 1: Add(ref DimCurrency情報,DimCurrency,new DimCurrency(ID(DimCurrency),"","")); break;
                case 2: Add(ref DimDate情報,DimDate,new DimDate(ID(DimDate),DateTime.Now,0,"","","",0,0,0,"","","",0,0,0,0,0,0,0)); break;
                case 3: Add(ref DimProductCategory情報,DimProductCategory,new DimProductCategory(ID(DimProductCategory),null,"","","")); break;
                case 4: Add(ref DimProductSubcategory情報,DimProductSubcategory,new DimProductSubcategory(ID(DimProductSubcategory),null,"","","",null)); break;
                case 5: Add(ref DimProduct情報,DimProduct,new DimProduct(ID(DimProduct),null,null,null,null,"","","",null,false,"",null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null)); break;
                case 6: Add(ref DimPromotion情報,DimPromotion,new DimPromotion(ID(DimPromotion),null,null,null,null,null,null,null,null,null,null,null,DateTime.Now,null,null,null)); break;
                case 7: Add(ref DimSalesReason情報,DimSalesReason,new DimSalesReason(ID(DimSalesReason),0,"","")); break;
                case 8: Add(ref DimSalesTerritory情報,DimSalesTerritory,new DimSalesTerritory(ID(DimSalesTerritory),null,"","",null,null)); break;
                case 9: Add(ref DimEmployee情報,DimEmployee,new DimEmployee(ID(DimEmployee),null,null,null,null,null,null,null,false,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,false,false,null,null,null,null,null)); break;
                case 10: Add(ref DimGeography情報,DimGeography,new DimGeography(ID(DimGeography),null,null,null,null,null,null,null,null,null,null)); break;
                case 11: Add(ref DimCustomer情報,DimCustomer,new DimCustomer(ID(DimCustomer),null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null)); break;
                case 12: Add(ref DimReseller情報,DimReseller,new DimReseller(ID(DimReseller),null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null)); break;
                case 13: Add(ref DimScenario情報,DimScenario,new DimScenario(ID(DimScenario),"")); break;
                case 14:
                    if(DimDate.Count==0) goto case 2;
                    if(DimCustomer.Count==0) goto case 11;
                    if(DimProductCategory.Count==0) goto case 3;
                    Add(
                        ref FactSurveyResponse情報,
                        FactSurveyResponse,
                        new FactSurveyResponse(
                            ID(FactSurveyResponse),
                            DimDate.Sampling.DateKey,
                            DimCustomer.Sampling.CustomerKey,
                            DimProductCategory.Sampling.ProductCategoryKey,
                            "",0,"",null
                        )
                    );
                    break;
                case 15:
                    if(DimDate.Count==0) goto case 2;
                    Add(
                        ref FactCallCenter情報,
                        FactCallCenter,
                        new FactCallCenter(
                            ID(FactCallCenter),
                            DimDate.Sampling.DateKey,
                            "","",0,0,0,0,0,0,0,0,0,null
                        )
                    );
                    break;
                case 16: {
                    int? OrganizationKey;
                    if(DimOrganization.Count==0) {
                        OrganizationKey=null;
                    } else {
                        OrganizationKey=DimOrganization.Sampling.OrganizationKey;
                    }
                    Add(
                        ref DimOrganization情報,
                        DimOrganization,
                        new DimOrganization(
                            ID(DimOrganization),
                            OrganizationKey,
                            null,null,null
                        )
                    );
                    break;
                }
                case 17: {
                    int? DepartmentGroupKey;
                    if(DimDepartmentGroup.Count==0) {
                        DepartmentGroupKey=null;
                    } else {
                        DepartmentGroupKey=DimDepartmentGroup.Sampling.DepartmentGroupKey;
                    }
                    Add(
                        ref DimDepartmentGroup情報,
                        DimDepartmentGroup,
                        new DimDepartmentGroup(
                            ID(DimDepartmentGroup),
                            DepartmentGroupKey,
                            null
                        )
                    );
                    break;
                }
                case 18:
                    if(DimDate.Count==0) goto case 2;
                    if(DimOrganization.Count==0) goto case 16;
                    if(DimDepartmentGroup.Count==0) goto case 17;
                    if(DimScenario.Count==0) goto case 13;
                    if(DimAccount.Count==0) goto case 0;
                    Add(ref FactFinance情報,FactFinance,new FactFinance(ID(FactFinance),DimDate.Sampling.DateKey,DimOrganization.Sampling.OrganizationKey,DimDepartmentGroup.Sampling.DepartmentGroupKey,DimScenario.Sampling.ScenarioKey,DimAccount.Sampling.AccountKey,0,null));
                    break;
                case 19:
                    if(DimDate.Count==0) goto case 2;
                    if(DimCurrency.Count==0) goto case 1;
                    Add(ref FactCurrencyRate情報,FactCurrencyRate,new FactCurrencyRate(ID(FactCurrencyRate),0,0,0,null));
                    break;
                case 20:
                    if(DimDate.Count==0) goto case 2;
                    if(DimCurrency.Count==0) goto case 1;
                    if(DimCustomer.Count==0) goto case 11;
                    if(DimProduct.Count==0) goto case 5;
                    if(DimPromotion.Count==0) goto case 6;
                    if(DimSalesTerritory.Count==0) goto case 8;
                    Add(
                        ref FactInternetSales情報,FactInternetSales,
                        new FactInternetSales(
                            ID(FactInternetSales),
                            DimDate.Sampling.DateKey,
                            DimDate.Sampling.DateKey,
                            DimDate.Sampling.DateKey,
                            DimCustomer.Sampling.CustomerKey,
                            DimPromotion.Sampling.PromotionKey,
                            DimCurrency.Sampling.CurrencyKey,
                            DimSalesTerritory.Sampling.SalesTerritoryKey,
                            "",0,0,0,0,0,0,0,0,0,0,0,0,null,null,null,null,null
                        )
                    );
                    break;
                case 21: {
                    if(FactInternetSales.Count==0) goto case 20;
                    if(DimSalesReason.Count==0) goto case 7;
                    var Sampling = FactInternetSales.Sampling;
                    Add(
                        ref FactInternetSalesReason情報,FactInternetSalesReason,
                        new FactInternetSalesReason(
                            Sampling.SalesOrderNumber,
                            Sampling.SalesOrderLineNumber,
                            DimSalesReason.Sampling.SalesReasonKey
                        )
                    );
                    break;
                }
                case 22:
                    if(DimProduct.Count==0) goto case 5;
                    if(DimDate.Count==0) goto case 2;
                    Add(ref FactProductInventory情報,FactProductInventory,
                        new FactProductInventory(
                            DimProduct.Sampling.ProductKey,
                            DimDate.Sampling.DateKey,
                            DateTime.Now,0,0,0,0
                        )
                    );
                    break;
                case 23: {
                    if(DimDate.Count==0) goto case 2;
                    if(DimReseller.Count==0) goto case 12;
                    if(DimEmployee.Count==0) goto case 9;
                    if(DimPromotion.Count==0) goto case 6;
                    if(DimCurrency.Count==0) goto case 1;
                    if(DimSalesTerritory.Count==0) goto case 8;
                    if(FactInternetSales.Count==0) goto case 20;
                    var Sampling = FactInternetSales.Sampling;
                    Add(
                        ref FactResellerSales情報,
                        FactResellerSales,
                        new FactResellerSales(
                            ID(FactResellerSales),
                            DimDate.Sampling.DateKey,
                            DimDate.Sampling.DateKey,
                            DimDate.Sampling.DateKey,
                            DimReseller.Sampling.ResellerKey,
                            DimEmployee.Sampling.EmployeeKey,
                            DimPromotion.Sampling.PromotionKey,
                            DimCurrency.Sampling.CurrencyKey,
                            DimSalesTerritory.Sampling.SalesTerritoryKey,
                            Sampling.SalesOrderNumber,
                            Sampling.SalesOrderLineNumber,
                            null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null
                        )
                    );
                    break;
                }
                case 24:
                    if(DimEmployee.Count==0) goto case 9;
                    if(DimDate.Count==0) goto case 2;
                    Add(
                        ref FactSalesQuota情報,
                        FactSalesQuota,
                        new FactSalesQuota(
                            ID(FactSalesQuota),
                            DimEmployee.Sampling.EmployeeKey,
                            DimDate.Sampling.DateKey,
                            0,0,0,null
                        )
                    );
                    break;
            }
        }
        AddWriteLine(DimAccount情報);
        AddWriteLine(DimCurrency情報);
        AddWriteLine(DimDate情報);
        AddWriteLine(DimProductCategory情報);
        AddWriteLine(DimProductSubcategory情報);
        AddWriteLine(DimProduct情報);
        AddWriteLine(DimPromotion情報);
        AddWriteLine(DimSalesReason情報);
        AddWriteLine(DimSalesTerritory情報);
        AddWriteLine(DimEmployee情報);
        AddWriteLine(DimGeography情報);
        AddWriteLine(DimCustomer情報);
        AddWriteLine(DimReseller情報);
        AddWriteLine(DimScenario情報);
        AddWriteLine(FactSurveyResponse情報);
        AddWriteLine(FactCallCenter情報);
        AddWriteLine(DimOrganization情報);
        AddWriteLine(DimDepartmentGroup情報);
        AddWriteLine(FactFinance情報);
        AddWriteLine(FactCurrencyRate情報);
        AddWriteLine(FactInternetSales情報);
        AddWriteLine(FactInternetSalesReason情報);
        AddWriteLine(FactProductInventory情報);
        AddWriteLine(FactResellerSales情報);
        AddWriteLine(FactSalesQuota情報);
        for(var a = 0;a<試行回数;a++) {
            switch(Switchパターン()%25) {
                case 0: Del(ref DimAccount情報,DimAccount); break;
                case 1: Del(ref DimCurrency情報,DimCurrency); break;
                case 2: Del(ref DimDate情報,DimDate); break;
                case 3: Del(ref DimProductCategory情報,DimProductCategory); break;
                case 4: Del(ref DimProductSubcategory情報,DimProductSubcategory); break;
                case 5: Del(ref DimProduct情報,DimProduct); break;
                case 6: Del(ref DimPromotion情報,DimPromotion); break;
                case 7: Del(ref DimSalesReason情報,DimSalesReason); break;
                case 8: Del(ref DimSalesTerritory情報,DimSalesTerritory); break;
                case 9: Del(ref DimEmployee情報,DimEmployee); break;
                case 10: Del(ref DimGeography情報,DimGeography); break;
                case 11: Del(ref DimCustomer情報,DimCustomer); break;
                case 12: Del(ref DimReseller情報,DimReseller); break;
                case 13: Del(ref DimScenario情報,DimScenario); break;
                case 14: Del(ref FactSurveyResponse情報,FactSurveyResponse); break;
                case 15: Del(ref FactCallCenter情報,FactCallCenter); break;
                case 16: Del(ref DimOrganization情報,DimOrganization); break;
                case 17: Del(ref DimDepartmentGroup情報,DimDepartmentGroup); break;
                case 18: Del(ref FactFinance情報,FactFinance); break;
                case 19: Del(ref FactCurrencyRate情報,FactCurrencyRate); break;
                case 20: Del(ref FactInternetSales情報,FactInternetSales); break;
                case 21: Del(ref FactInternetSalesReason情報,FactInternetSalesReason); break;
                case 22: Del(ref FactProductInventory情報,FactProductInventory); break;
                case 23: Del(ref FactResellerSales情報,FactResellerSales); break;
                case 24: Del(ref FactSalesQuota情報,FactSalesQuota); break;
            }
        }
        Trace.WriteLine($"Transaction {s.ElapsedMilliseconds,7}ms");
        Console.WriteLine($"Transaction {s.ElapsedMilliseconds,7}ms");
        DelWriteLine(DimAccount情報);
        DelWriteLine(DimCurrency情報);
        DelWriteLine(DimDate情報);
        DelWriteLine(DimProductCategory情報);
        DelWriteLine(DimProductSubcategory情報);
        DelWriteLine(DimProduct情報);
        DelWriteLine(DimPromotion情報);
        DelWriteLine(DimSalesReason情報);
        DelWriteLine(DimSalesTerritory情報);
        DelWriteLine(DimEmployee情報);
        DelWriteLine(DimGeography情報);
        DelWriteLine(DimCustomer情報);
        DelWriteLine(DimReseller情報);
        DelWriteLine(DimScenario情報);
        DelWriteLine(FactSurveyResponse情報);
        DelWriteLine(FactCallCenter情報);
        DelWriteLine(DimOrganization情報);
        DelWriteLine(DimDepartmentGroup情報);
        DelWriteLine(FactFinance情報);
        DelWriteLine(FactCurrencyRate情報);
        DelWriteLine(FactInternetSales情報);
        DelWriteLine(FactInternetSalesReason情報);
        DelWriteLine(FactProductInventory情報);
        DelWriteLine(FactResellerSales情報);
        DelWriteLine(FactSalesQuota情報);
    }
    private static void Load() {
        using var C = new SqlConnection(@"Data Source=localhost\MSSQLSERVER2019;Initial Catalog=AdventureWorksDW2019;Integrated Security=True");
        C.Open();
        using var Command = new SqlCommand { Connection=C };
        using var e = new Container();
        var dbo = e.dbo;
        var s = Stopwatch.StartNew();
        {
            Command.CommandText=
                "SELECT "
                +"[AccountKey]"
                +",[ParentAccountKey]"
                +",[AccountCodeAlternateKey]"
                +",[ParentAccountCodeAlternateKey]"
                +",[AccountDescription]"
                +",[AccountType]"
                +",[Operator]"
                +",[CustomMembers]"
                +",[ValueType]"
                +",[CustomMemberOptions]"
                +"FROM[dbo].[DimAccount];"
                +"SELECT "
                +"[CurrencyKey]"
                +",[CurrencyAlternateKey]"
                +",[CurrencyName]"
                +"FROM[dbo].[DimCurrency];"
                +"SELECT "
                +"[DateKey]"
                +",[FullDateAlternateKey]"
                +",[DayNumberOfWeek]"
                +",[EnglishDayNameOfWeek]"
                +",[SpanishDayNameOfWeek]"
                +",[FrenchDayNameOfWeek]"
                +",[DayNumberOfMonth]"
                +",[DayNumberOfYear]"
                +",[WeekNumberOfYear]"
                +",[EnglishMonthName]"
                +",[SpanishMonthName]"
                +",[FrenchMonthName]"
                +",[MonthNumberOfYear]"
                +",[CalendarQuarter]"
                +",[CalendarYear]"
                +",[CalendarSemester]"
                +",[FiscalQuarter]"
                +",[FiscalYear]"
                +",[FiscalSemester]"
                +"FROM[dbo].[DimDate];"
                +"SELECT "
                +"[ProductCategoryKey]"
                +",[ProductCategoryAlternateKey]"
                +",[EnglishProductCategoryName]"
                +",[SpanishProductCategoryName]"
                +",[FrenchProductCategoryName]"
                +"FROM [AdventureWorksDW2019].[dbo].[DimProductCategory]"
                +"SELECT "
                +"[ProductSubcategoryKey]"
                +",[ProductSubcategoryAlternateKey]"
                +",[EnglishProductSubcategoryName]"
                +",[SpanishProductSubcategoryName]"
                +",[FrenchProductSubcategoryName]"
                +",[ProductCategoryKey]"
                +"FROM [AdventureWorksDW2019].[dbo].[DimProductSubcategory]"
                +"SELECT "
                +"[ProductKey]"
                +",[ProductAlternateKey]"
                +",[ProductSubcategoryKey]"
                +",[WeightUnitMeasureCode]"
                +",[SizeUnitMeasureCode]"
                +",[EnglishProductName]"
                +",[SpanishProductName]"
                +",[FrenchProductName]"
                +",[StandardCost]"
                +",[FinishedGoodsFlag]"
                +",[Color]"
                +",[SafetyStockLevel]"
                +",[ReorderPoint]"
                +",[ListPrice]"
                +",[Size]"
                +",[SizeRange]"
                +",[Weight]"
                +",[DaysToManufacture]"
                +",[ProductLine]"
                +",[DealerPrice]"
                +",[Class]"
                +",[Style]"
                +",[ModelName]"
                +",[LargePhoto]"
                +",[EnglishDescription]"
                +",[FrenchDescription]"
                +",[ChineseDescription]"
                +",[ArabicDescription]"
                +",[HebrewDescription]"
                +",[ThaiDescription]"
                +",[GermanDescription]"
                +",[JapaneseDescription]"
                +",[TurkishDescription]"
                +",[StartDate]"
                +",[EndDate]"
                +",[Status]"
                +"FROM [AdventureWorksDW2019].[dbo].[DimProduct]"
                +"SELECT "
                +"[PromotionKey]"
                +",[PromotionAlternateKey]"
                +",[EnglishPromotionName]"
                +",[SpanishPromotionName]"
                +",[FrenchPromotionName]"
                +",[DiscountPct]"
                +",[EnglishPromotionType]"
                +",[SpanishPromotionType]"
                +",[FrenchPromotionType]"
                +",[EnglishPromotionCategory]"
                +",[SpanishPromotionCategory]"
                +",[FrenchPromotionCategory]"
                +",[StartDate]"
                +",[EndDate]"
                +",[MinQty]"
                +",[MaxQty]"
                +"FROM [AdventureWorksDW2019].[dbo].[DimPromotion]"
                +"SELECT "
                +"[SalesReasonKey]"
                +",[SalesReasonAlternateKey]"
                +",[SalesReasonName]"
                +",[SalesReasonReasonType]"
                +"FROM [AdventureWorksDW2019].[dbo].[DimSalesReason]"
                +"SELECT "
                +"[SalesTerritoryKey]"
                +",[SalesTerritoryAlternateKey]"
                +",[SalesTerritoryRegion]"
                +",[SalesTerritoryCountry]"
                +",[SalesTerritoryGroup]"
                +",[SalesTerritoryImage]"
                +"FROM [AdventureWorksDW2019].[dbo].[DimSalesTerritory];"
                +"WITH 再帰("
                +"    [EmployeeKey]"
                +"    ,[ParentEmployeeKey]"
                +"    ,[EmployeeNationalIDAlternateKey]"
                +"    ,[ParentEmployeeNationalIDAlternateKey]"
                +"    ,[SalesTerritoryKey]"
                +"    ,[FirstName]"
                +"    ,[LastName]"
                +"    ,[MiddleName]"
                +"    ,[NameStyle]"
                +"    ,[Title]"
                +"    ,[HireDate]"
                +"    ,[BirthDate]"
                +"    ,[LoginID]"
                +"    ,[EmailAddress]"
                +"    ,[Phone]"
                +"    ,[MaritalStatus]"
                +"    ,[EmergencyContactName]"
                +"    ,[EmergencyContactPhone]"
                +"    ,[SalariedFlag]"
                +"    ,[Gender]"
                +"    ,[PayFrequency]"
                +"    ,[BaseRate]"
                +"    ,[VacationHours]"
                +"    ,[SickLeaveHours]"
                +"    ,[CurrentFlag]"
                +"    ,[SalesPersonFlag]"
                +"    ,[DepartmentName]"
                +"    ,[StartDate]"
                +"    ,[EndDate]"
                +"    ,[Status]"
                +"    ,[EmployeePhoto]"
                +")AS("
                +"    SELECT"
                +"        [EmployeeKey]"
                +"        ,[ParentEmployeeKey]"
                +"        ,[EmployeeNationalIDAlternateKey]"
                +"        ,[ParentEmployeeNationalIDAlternateKey]"
                +"        ,[SalesTerritoryKey]"
                +"        ,[FirstName]"
                +"        ,[LastName]"
                +"        ,[MiddleName]"
                +"        ,[NameStyle]"
                +"        ,[Title]"
                +"        ,[HireDate]"
                +"        ,[BirthDate]"
                +"        ,[LoginID]"
                +"        ,[EmailAddress]"
                +"        ,[Phone]"
                +"        ,[MaritalStatus]"
                +"        ,[EmergencyContactName]"
                +"        ,[EmergencyContactPhone]"
                +"        ,[SalariedFlag]"
                +"        ,[Gender]"
                +"        ,[PayFrequency]"
                +"        ,[BaseRate]"
                +"        ,[VacationHours]"
                +"        ,[SickLeaveHours]"
                +"        ,[CurrentFlag]"
                +"        ,[SalesPersonFlag]"
                +"        ,[DepartmentName]"
                +"        ,[StartDate]"
                +"        ,[EndDate]"
                +"        ,[Status]"
                +"        ,[EmployeePhoto]"
                +"    FROM [AdventureWorksDW2019].[dbo].[DimEmployee]"
                +"    WHERE [ParentEmployeeKey]IS NULL"
                +"    UNION ALL"
                +"    SELECT"
                +"        A.[EmployeeKey]"
                +"        ,A.[ParentEmployeeKey]"
                +"        ,A.[EmployeeNationalIDAlternateKey]"
                +"        ,A.[ParentEmployeeNationalIDAlternateKey]"
                +"        ,A.[SalesTerritoryKey]"
                +"        ,A.[FirstName]"
                +"        ,A.[LastName]"
                +"        ,A.[MiddleName]"
                +"        ,A.[NameStyle]"
                +"        ,A.[Title]"
                +"        ,A.[HireDate]"
                +"        ,A.[BirthDate]"
                +"        ,A.[LoginID]"
                +"        ,A.[EmailAddress]"
                +"        ,A.[Phone]"
                +"        ,A.[MaritalStatus]"
                +"        ,A.[EmergencyContactName]"
                +"        ,A.[EmergencyContactPhone]"
                +"        ,A.[SalariedFlag]"
                +"        ,A.[Gender]"
                +"        ,A.[PayFrequency]"
                +"        ,A.[BaseRate]"
                +"        ,A.[VacationHours]"
                +"        ,A.[SickLeaveHours]"
                +"        ,A.[CurrentFlag]"
                +"        ,A.[SalesPersonFlag]"
                +"        ,A.[DepartmentName]"
                +"        ,A.[StartDate]"
                +"        ,A.[EndDate]"
                +"        ,A.[Status]"
                +"        ,A.[EmployeePhoto]"
                +"    FROM 再帰"
                +"    JOIN[AdventureWorksDW2019].[dbo].[DimEmployee]A ON 再帰.EmployeeKey=A.ParentEmployeeKey"
                +")"
                +"SELECT * FROM 再帰 "
                +"SELECT "
                +"[GeographyKey]"
                +",[City]"
                +",[StateProvinceCode]"
                +",[StateProvinceName]"
                +",[CountryRegionCode]"
                +",[EnglishCountryRegionName]"
                +",[SpanishCountryRegionName]"
                +",[FrenchCountryRegionName]"
                +",[PostalCode]"
                +",[SalesTerritoryKey]"
                +",[IpAddressLocator]"
                +"FROM [AdventureWorksDW2019].[dbo].[DimGeography]"
                +"SELECT "
                +"[CustomerKey]"
                +",[GeographyKey]"
                +",[CustomerAlternateKey]"
                +",[Title]"
                +",[FirstName]"
                +",[MiddleName]"
                +",[LastName]"
                +",[NameStyle]"
                +",[BirthDate]"
                +",[MaritalStatus]"
                +",[Suffix]"
                +",[Gender]"
                +",[EmailAddress]"
                +",[YearlyIncome]"
                +",[TotalChildren]"
                +",[NumberChildrenAtHome]"
                +",[EnglishEducation]"
                +",[SpanishEducation]"
                +",[FrenchEducation]"
                +",[EnglishOccupation]"
                +",[SpanishOccupation]"
                +",[FrenchOccupation]"
                +",[HouseOwnerFlag]"
                +",[NumberCarsOwned]"
                +",[AddressLine1]"
                +",[AddressLine2]"
                +",[Phone]"
                +",[DateFirstPurchase]"
                +",[CommuteDistance]"
                +"FROM [AdventureWorksDW2019].[dbo].[DimCustomer]"
                +"SELECT "
                +"[ResellerKey]"
                +",[GeographyKey]"
                +",[ResellerAlternateKey]"
                +",[Phone]"
                +",[BusinessType]"
                +",[ResellerName]"
                +",[NumberEmployees]"
                +",[OrderFrequency]"
                +",[OrderMonth]"
                +",[FirstOrderYear]"
                +",[LastOrderYear]"
                +",[ProductLine]"
                +",[AddressLine1]"
                +",[AddressLine2]"
                +",[AnnualSales]"
                +",[BankName]"
                +",[MinPaymentType]"
                +",[MinPaymentAmount]"
                +",[AnnualRevenue]"
                +",[YearOpened]"
                +"FROM [AdventureWorksDW2019].[dbo].[DimReseller]"
                +"SELECT "
                +"[ScenarioKey]"
                +",[ScenarioName]"
                +"FROM [AdventureWorksDW2019].[dbo].[DimScenario]"
                +"SELECT "
                +"[SurveyResponseKey]"
                +",[DateKey]"
                +",[CustomerKey]"
                +",[ProductCategoryKey]"
                +",[EnglishProductCategoryName]"
                +",[ProductSubcategoryKey]"
                +",[EnglishProductSubcategoryName]"
                +",[Date]"
                +"FROM [AdventureWorksDW2019].[dbo].[FactSurveyResponse]"
                +"SELECT "
                +"[FactCallCenterID]"
                +",[DateKey]"
                +",[WageType]"
                +",[Shift]"
                +",[LevelOneOperators]"
                +",[LevelTwoOperators]"
                +",[TotalOperators]"
                +",[Calls]"
                +",[AutomaticResponses]"
                +",[Orders]"
                +",[IssuesRaised]"
                +",[AverageTimePerIssue]"
                +",[ServiceGrade]"
                +",[Date]"
                +"FROM [AdventureWorksDW2019].[dbo].[FactCallCenter];"
                +"WITH 再帰("
                +"    [OrganizationKey]"
                +"    ,[ParentOrganizationKey]"
                +"    ,[PercentageOfOwnership]"
                +"    ,[OrganizationName]"
                +"    ,[CurrencyKey]"
                +")AS("
                +"    SELECT"
                +"        [OrganizationKey]"
                +"        ,[ParentOrganizationKey]"
                +"        ,[PercentageOfOwnership]"
                +"        ,[OrganizationName]"
                +"        ,[CurrencyKey]"
                +"    FROM [AdventureWorksDW2019].[dbo].[DimOrganization]"
                +"    WHERE [ParentOrganizationKey]IS NULL"
                +"    UNION ALL"
                +"    SELECT"
                +"        A.[OrganizationKey]"
                +"        ,A.[ParentOrganizationKey]"
                +"        ,A.[PercentageOfOwnership]"
                +"        ,A.[OrganizationName]"
                +"        ,A.[CurrencyKey]"
                +"    FROM 再帰"
                +"    JOIN[AdventureWorksDW2019].[dbo].[DimOrganization]A ON 再帰.[OrganizationKey]=A.[ParentOrganizationKey]"
                +")"
                +"SELECT * FROM 再帰 "
                +"SELECT "
                +"[DepartmentGroupKey]"
                +",[ParentDepartmentGroupKey]"
                +",[DepartmentGroupName]"
                +"FROM [AdventureWorksDW2019].[dbo].[DimDepartmentGroup]"
                +"SELECT "
                +"[FinanceKey]"
                +",[DateKey]"
                +",[OrganizationKey]"
                +",[DepartmentGroupKey]"
                +",[ScenarioKey]"
                +",[AccountKey]"
                +",[Amount]"
                +",[Date]"
                +"FROM [AdventureWorksDW2019].[dbo].[FactFinance]"
                +"SELECT "
                +"[CurrencyKey]"
                +",[DateKey]"
                +",[AverageRate]"
                +",[EndOfDayRate]"
                +",[Date]"
                +"FROM [AdventureWorksDW2019].[dbo].[FactCurrencyRate]"
                +"SELECT "
                +"[ProductKey]"
                +",[OrderDateKey]"
                +",[DueDateKey]"
                +",[ShipDateKey]"
                +",[CustomerKey]"
                +",[PromotionKey]"
                +",[CurrencyKey]"
                +",[SalesTerritoryKey]"
                +",[SalesOrderNumber]"
                +",[SalesOrderLineNumber]"
                +",[RevisionNumber]"
                +",[OrderQuantity]"
                +",[UnitPrice]"
                +",[ExtendedAmount]"
                +",[UnitPriceDiscountPct]"
                +",[DiscountAmount]"
                +",[ProductStandardCost]"
                +",[TotalProductCost]"
                +",[SalesAmount]"
                +",[TaxAmt]"
                +",[Freight]"
                +",[CarrierTrackingNumber]"
                +",[CustomerPONumber]"
                +",[OrderDate]"
                +",[DueDate]"
                +",[ShipDate]"
                +"FROM [AdventureWorksDW2019].[dbo].[FactInternetSales]"
                +"SELECT "
                +"[SalesOrderNumber]"
                +",[SalesOrderLineNumber]"
                +",[SalesReasonKey]"
                +"FROM [AdventureWorksDW2019].[dbo].[FactInternetSalesReason]"
                +"SELECT "
                +"[ProductKey]"
                +",[DateKey]"
                +",[MovementDate]"
                +",[UnitCost]"
                +",[UnitsIn]"
                +",[UnitsOut]"
                +",[UnitsBalance]"
                +"FROM [AdventureWorksDW2019].[dbo].[FactProductInventory]"
                +"SELECT "
                +"[ProductKey]"
                +",[OrderDateKey]"
                +",[DueDateKey]"
                +",[ShipDateKey]"
                +",[ResellerKey]"
                +",[EmployeeKey]"
                +",[PromotionKey]"
                +",[CurrencyKey]"
                +",[SalesTerritoryKey]"
                +",[SalesOrderNumber]"
                +",[SalesOrderLineNumber]"
                +",[RevisionNumber]"
                +",[OrderQuantity]"
                +",[UnitPrice]"
                +",[ExtendedAmount]"
                +",[UnitPriceDiscountPct]"
                +",[DiscountAmount]"
                +",[ProductStandardCost]"
                +",[TotalProductCost]"
                +",[SalesAmount]"
                +",[TaxAmt]"
                +",[Freight]"
                +",[CarrierTrackingNumber]"
                +",[CustomerPONumber]"
                +",[OrderDate]"
                +",[DueDate]"
                +",[ShipDate]"
                +"FROM [AdventureWorksDW2019].[dbo].[FactResellerSales]"
                +"SELECT "
                +"[SalesQuotaKey]"
                +",[EmployeeKey]"
                +",[DateKey]"
                +",[CalendarYear]"
                +",[CalendarQuarter]"
                +",[SalesAmountQuota]"
                +",[Date]"
                +"FROM [AdventureWorksDW2019].[dbo].[FactSalesQuota]"
                +"SELECT "
                +"[ProductKey]"
                +",[CultureName]"
                +",[ProductDescription]"
                +"FROM [AdventureWorksDW2019].[dbo].[FactAdditionalInternationalProductDescription]"
                +"SELECT DISTINCT"
                +"[AverageRate]"
                +",[CurrencyID]"
                +",[CurrencyDate]"
                +",[EndOfDayRate]"
                +",[CurrencyKey]"
                +",[DateKey]"
                +"FROM [AdventureWorksDW2019].[dbo].[NewFactCurrencyRate]"
                +"SELECT "
                +"[ProspectiveBuyerKey]"
                +",[ProspectAlternateKey]"
                +",[FirstName]"
                +",[MiddleName]"
                +",[LastName]"
                +",[BirthDate]"
                +",[MaritalStatus]"
                +",[Gender]"
                +",[EmailAddress]"
                +",[YearlyIncome]"
                +",[TotalChildren]"
                +",[NumberChildrenAtHome]"
                +",[Education]"
                +",[Occupation]"
                +",[HouseOwnerFlag]"
                +",[NumberCarsOwned]"
                +",[AddressLine1]"
                +",[AddressLine2]"
                +",[City]"
                +",[StateProvinceCode]"
                +",[PostalCode]"
                +",[Phone]"
                +",[Salutation]"
                +",[Unknown]"
                +"FROM [AdventureWorksDW2019].[dbo].[ProspectiveBuyer]"
                +"";
            using var Reader = Command.ExecuteReader();
            var DimAccount = dbo.DimAccount;
            while(Reader.Read()) {
                DimAccount.AddOrThrow(
                    new DimAccount(
                        Reader.GetInt32(0),
                        GetInt32(Reader,1),
                        GetInt32(Reader,2),
                        GetInt32(Reader,3),
                        GetString(Reader,4),
                        GetString(Reader,5),
                        GetString(Reader,6),
                        GetString(Reader,7),
                        GetString(Reader,8),
                        GetString(Reader,9)
                    )
                );
            }
            Reader.NextResult();
            var DimCurrency = dbo.DimCurrency;
            while(Reader.Read()) {
                DimCurrency.AddOrThrow(
                    new DimCurrency(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetString(2)
                    )
                );
            }
            Reader.NextResult();
            var DimDate = dbo.DimDate;
            while(Reader.Read()) {
                DimDate.AddOrThrow(
                    new DimDate(
                        Reader.GetInt32(0),
                        Reader.GetDateTime(1),
                        Reader.GetByte(2),
                        Reader.GetString(3),
                        Reader.GetString(4),
                        Reader.GetString(5),
                        Reader.GetByte(6),
                        Reader.GetInt16(7),
                        Reader.GetByte(8),
                        Reader.GetString(9),
                        Reader.GetString(10),
                        Reader.GetString(11),
                        Reader.GetByte(12),
                        Reader.GetByte(13),
                        Reader.GetInt16(14),
                        Reader.GetByte(15),
                        Reader.GetByte(16),
                        Reader.GetInt16(17),
                        Reader.GetByte(18)
                    )
                );
            }
            Reader.NextResult();
            var DimProductCategory = dbo.DimProductCategory;
            while(Reader.Read()) {
                DimProductCategory.AddOrThrow(
                    new DimProductCategory(
                        Reader.GetInt32(0),
                        GetInt32(Reader,1),
                        Reader.GetString(2),
                        Reader.GetString(3),
                        Reader.GetString(4)
                    )
                );
            }
            Reader.NextResult();
            var DimProductSubcategory = dbo.DimProductSubcategory;
            while(Reader.Read()) {
                DimProductSubcategory.AddOrThrow(
                    new DimProductSubcategory(
                        Reader.GetInt32(0),
                        GetInt32(Reader,1),
                        Reader.GetString(2),
                        Reader.GetString(3),
                        Reader.GetString(4),
                        Reader.GetInt32(5)
                    )
                );
            }
            Reader.NextResult();
            var DimProduct = dbo.DimProduct;
            while(Reader.Read()) {
                DimProduct.AddOrThrow(
                    new DimProduct(
                        Reader.GetInt32(0),
                        GetString(Reader,1),
                        GetInt32(Reader,2),
                        GetString(Reader,3),
                        GetString(Reader,4),
                        Reader.GetString(5),
                        Reader.GetString(6),
                        Reader.GetString(7),
                        GetDecimal(Reader,8),
                        Reader.GetBoolean(9),
                        Reader.GetString(10),
                        GetInt16(Reader,11),
                        GetInt16(Reader,12),
                        GetDecimal(Reader,13),
                        GetString(Reader,14),
                        GetString(Reader,15),
                        GetDouble(Reader,16),
                        GetInt32(Reader,17),
                        GetString(Reader,18),
                        GetDecimal(Reader,19),
                        GetString(Reader,20),
                        GetString(Reader,21),
                        GetString(Reader,22),
                        GetBytes(Reader,23),
                        GetString(Reader,24),
                        GetString(Reader,25),
                        GetString(Reader,26),
                        GetString(Reader,27),
                        GetString(Reader,28),
                        GetString(Reader,29),
                        GetString(Reader,30),
                        GetString(Reader,31),
                        GetString(Reader,32),
                        GetDateTime(Reader,33),
                        GetDateTime(Reader,34),
                        GetString(Reader,35)
                    )
                );
            }
            Reader.NextResult();
            var DimPromotion = dbo.DimPromotion;
            while(Reader.Read()) {
                DimPromotion.AddOrThrow(
                    new DimPromotion(
                        Reader.GetInt32(0),
                        GetInt32(Reader,1),
                        GetString(Reader,2),
                        GetString(Reader,3),
                        GetString(Reader,4),
                        GetDouble(Reader,5),
                        GetString(Reader,6),
                        GetString(Reader,7),
                        GetString(Reader,8),
                        GetString(Reader,9),
                        GetString(Reader,10),
                        GetString(Reader,11),
                        Reader.GetDateTime(12),
                        GetDateTime(Reader,13),
                        GetInt32(Reader,14),
                        GetInt32(Reader,15)
                    )
                );
            }
            Reader.NextResult();
            var DimSalesReason = dbo.DimSalesReason;
            while(Reader.Read()) {
                DimSalesReason.AddOrThrow(
                    new DimSalesReason(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetString(2),
                        Reader.GetString(3)
                    )
                );
            }
            Reader.NextResult();
            var DimSalesTerritory = dbo.DimSalesTerritory;
            while(Reader.Read()) {
                DimSalesTerritory.AddOrThrow(
                    new DimSalesTerritory(
                        Reader.GetInt32(0),
                        GetInt32(Reader,1),
                        Reader.GetString(2),
                        Reader.GetString(3),
                        GetString(Reader,4),
                        GetBytes(Reader,5)
                    )
                );
            }
            Reader.NextResult();
            var DimEmployee = dbo.DimEmployee;
            while(Reader.Read()) {
                DimEmployee.AddOrThrow(
                    new DimEmployee(
                        Reader.GetInt32(0),
                        GetInt32(Reader,1),
                        GetString(Reader,2),
                        GetString(Reader,3),
                        GetInt32(Reader,4),
                        Reader.GetString(5),
                        Reader.GetString(6),
                        GetString(Reader,7),
                        Reader.GetBoolean(8),
                        GetString(Reader,9),
                        GetDateTime(Reader,10),
                        GetDateTime(Reader,11),
                        GetString(Reader,12),
                        GetString(Reader,13),
                        GetString(Reader,14),
                        GetString(Reader,15),
                        GetString(Reader,16),
                        GetString(Reader,17),
                        GetBoolean(Reader,18),
                        GetString(Reader,19),
                        GetByte(Reader,20),
                        GetDecimal(Reader,21),
                        GetInt16(Reader,22),
                        GetInt16(Reader,23),
                        Reader.GetBoolean(24),
                        Reader.GetBoolean(25),
                        GetString(Reader,26),
                        GetDateTime(Reader,27),
                        GetDateTime(Reader,28),
                        GetString(Reader,29),
                        GetBytes(Reader,30)
                    )
                );
            }
            Reader.NextResult();
            var DimGeography = dbo.DimGeography;
            while(Reader.Read()) {
                DimGeography.AddOrThrow(
                    new DimGeography(
                        Reader.GetInt32(0),
                        GetString(Reader,1),
                        GetString(Reader,2),
                        GetString(Reader,3),
                        GetString(Reader,4),
                        GetString(Reader,5),
                        GetString(Reader,6),
                        GetString(Reader,7),
                        GetString(Reader,8),
                        GetInt32(Reader,9),
                        GetString(Reader,10)
                    )
                );
            }
            Reader.NextResult();
            var DimCustomer = dbo.DimCustomer;
            while(Reader.Read()) {
                DimCustomer.AddOrThrow(
                    new DimCustomer(
                        Reader.GetInt32(0),
                        GetInt32(Reader,1),
                        Reader.GetString(2),
                        GetString(Reader,3),
                        GetString(Reader,4),
                        GetString(Reader,5),
                        GetString(Reader,6),
                        GetBoolean(Reader,7),
                        GetDateTime(Reader,8),
                        GetString(Reader,9),
                        GetString(Reader,10),
                        GetString(Reader,11),
                        GetString(Reader,12),
                        GetDecimal(Reader,13),
                        GetByte(Reader,14),
                        GetByte(Reader,15),
                        GetString(Reader,16),
                        GetString(Reader,17),
                        GetString(Reader,18),
                        GetString(Reader,19),
                        GetString(Reader,20),
                        GetString(Reader,21),
                        GetString(Reader,22),
                        GetByte(Reader,23),
                        GetString(Reader,24),
                        GetString(Reader,25),
                        GetString(Reader,26),
                        GetDateTime(Reader,27),
                        GetString(Reader,28)
                    )
                );
            }
            Reader.NextResult();
            var DimReseller = dbo.DimReseller;
            while(Reader.Read()) {
                DimReseller.AddOrThrow(
                    new DimReseller(
                        Reader.GetInt32(0),
                        GetInt32(Reader,1),
                        GetString(Reader,2),
                        GetString(Reader,3),
                        Reader.GetString(4),
                        Reader.GetString(5),
                        GetInt32(Reader,6),
                        GetString(Reader,7),
                        GetByte(Reader,8),
                        GetInt32(Reader,9),
                        GetInt32(Reader,10),
                        GetString(Reader,11),
                        GetString(Reader,12),
                        GetString(Reader,13),
                        GetDecimal(Reader,14),
                        GetString(Reader,15),
                        GetByte(Reader,16),
                        GetDecimal(Reader,17),
                        GetDecimal(Reader,18),
                        GetInt32(Reader,19)
                    )
                );
            }
            Reader.NextResult();
            var DimScenario = dbo.DimScenario;
            while(Reader.Read()) {
                DimScenario.AddOrThrow(
                    new DimScenario(
                        Reader.GetInt32(0),
                        GetString(Reader,1)
                    )
                );
            }
            Reader.NextResult();
            var FactSurveyResponse = dbo.FactSurveyResponse;
            while(Reader.Read()) {
                FactSurveyResponse.AddOrThrow(
                    new FactSurveyResponse(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetInt32(2),
                        Reader.GetInt32(3),
                        Reader.GetString(4),
                        Reader.GetInt32(5),
                        Reader.GetString(6),
                        GetDateTime(Reader,7)
                    )
                );
            }
            Reader.NextResult();
            var FactCallCenter = dbo.FactCallCenter;
            while(Reader.Read()) {
                FactCallCenter.AddOrThrow(
                    new FactCallCenter(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetString(2),
                        Reader.GetString(3),
                        Reader.GetInt16(4),
                        Reader.GetInt16(5),
                        Reader.GetInt16(6),
                        Reader.GetInt32(7),
                        Reader.GetInt32(8),
                        Reader.GetInt32(9),
                        Reader.GetInt16(10),
                        Reader.GetInt16(11),
                        Reader.GetDouble(12),
                        GetDateTime(Reader,13)
                    )
                );
            }
            Reader.NextResult();
            var DimOrganization = dbo.DimOrganization;
            while(Reader.Read()) {
                DimOrganization.AddOrThrow(
                    new DimOrganization(
                        Reader.GetInt32(0),
                        GetInt32(Reader,1),
                        GetString(Reader,2),
                        GetString(Reader,3),
                        GetInt32(Reader,4)
                    )
                );
            }
            Reader.NextResult();
            var DimDepartmentGroup = dbo.DimDepartmentGroup;
            while(Reader.Read()) {
                DimDepartmentGroup.AddOrThrow(
                    new DimDepartmentGroup(
                        Reader.GetInt32(0),
                        GetInt32(Reader,1),
                        GetString(Reader,2)
                    )
                );
            }
            Reader.NextResult();
            var FactFinance = dbo.FactFinance;
            while(Reader.Read()) {
                FactFinance.AddOrThrow(
                    new FactFinance(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetInt32(2),
                        Reader.GetInt32(3),
                        Reader.GetInt32(4),
                        Reader.GetInt32(5),
                        Reader.GetDouble(6),
                        GetDateTime(Reader,7)
                    )
                );
            }
            Reader.NextResult();
            var FactCurrencyRate = dbo.FactCurrencyRate;
            while(Reader.Read()) {
                FactCurrencyRate.AddOrThrow(
                    new FactCurrencyRate(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetDouble(2),
                        Reader.GetDouble(3),
                        GetDateTime(Reader,4)
                    )
                );
            }
            Reader.NextResult();
            var FactInternetSales = dbo.FactInternetSales;
            while(Reader.Read()) {
                FactInternetSales.AddOrThrow(
                    new FactInternetSales(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetInt32(2),
                        Reader.GetInt32(3),
                        Reader.GetInt32(4),
                        Reader.GetInt32(5),
                        Reader.GetInt32(6),
                        Reader.GetInt32(7),
                        Reader.GetString(8),
                        Reader.GetByte(9),
                        Reader.GetByte(10),
                        Reader.GetInt16(11),
                        Reader.GetDecimal(12),
                        Reader.GetDecimal(13),
                        Reader.GetDouble(14),
                        Reader.GetDouble(15),
                        Reader.GetDecimal(16),
                        Reader.GetDecimal(17),
                        Reader.GetDecimal(18),
                        Reader.GetDecimal(19),
                        Reader.GetDecimal(20),
                        GetString(Reader,21),
                        GetString(Reader,22),
                        GetDateTime(Reader,23),
                        GetDateTime(Reader,24),
                        GetDateTime(Reader,25)
                    )
                );
            }
            Reader.NextResult();
            var FactInternetSalesReason = dbo.FactInternetSalesReason;
            while(Reader.Read()) {
                FactInternetSalesReason.AddOrThrow(
                    new FactInternetSalesReason(
                        Reader.GetString(0),
                        Reader.GetByte(1),
                        Reader.GetInt32(2)
                    )
                );
            }
            Reader.NextResult();
            var FactProductInventory = dbo.FactProductInventory;
            while(Reader.Read()) {
                FactProductInventory.AddOrThrow(
                    new FactProductInventory(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetDateTime(2),
                        Reader.GetDecimal(3),
                        Reader.GetInt32(4),
                        Reader.GetInt32(5),
                        Reader.GetInt32(6)
                    )
                );
            }
            Reader.NextResult();
            var FactResellerSales = dbo.FactResellerSales;
            while(Reader.Read()) {
                FactResellerSales.AddOrThrow(
                    new FactResellerSales(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetInt32(2),
                        Reader.GetInt32(3),
                        Reader.GetInt32(4),
                        Reader.GetInt32(5),
                        Reader.GetInt32(6),
                        Reader.GetInt32(7),
                        Reader.GetInt32(8),
                        Reader.GetString(9),
                        Reader.GetByte(10),
                        GetByte(Reader,11),
                        GetInt16(Reader,12),
                        GetDecimal(Reader,13),
                        GetDecimal(Reader,14),
                        GetDouble(Reader,15),
                        GetDouble(Reader,16),
                        GetDecimal(Reader,17),
                        GetDecimal(Reader,18),
                        GetDecimal(Reader,19),
                        GetDecimal(Reader,20),
                        GetDecimal(Reader,21),
                        GetString(Reader,22),
                        GetString(Reader,23),
                        GetDateTime(Reader,24),
                        GetDateTime(Reader,25),
                        GetDateTime(Reader,26)
                    )
                );
            }
            Reader.NextResult();
            var FactSalesQuota = dbo.FactSalesQuota;
            while(Reader.Read()) {
                FactSalesQuota.AddOrThrow(
                    new FactSalesQuota(
                        Reader.GetInt32(0),
                        Reader.GetInt32(1),
                        Reader.GetInt32(2),
                        Reader.GetInt16(3),
                        Reader.GetByte(4),
                        Reader.GetDecimal(5),
                        GetDateTime(Reader,6)
                    )
                );
            }
            Reader.NextResult();
            var FactAdditionalInternationalProductDescription = dbo.FactAdditionalInternationalProductDescription;
            while(Reader.Read()) {
                FactAdditionalInternationalProductDescription.AddOrThrow(
                    new FactAdditionalInternationalProductDescription(
                        Reader.GetInt32(0),
                        Reader.GetString(1),
                        Reader.GetString(2)
                    )
                );
            }
            Reader.NextResult();
            var NewFactCurrencyRate = dbo.NewFactCurrencyRate;
            while(Reader.Read()) {
                NewFactCurrencyRate.AddOrThrow(
                    new NewFactCurrencyRate(
                        GetSingle(Reader,0),
                        GetString(Reader,1),
                        GetDateTime(Reader,2),
                        GetSingle(Reader,3),
                        GetInt32(Reader,4),
                        GetInt32(Reader,5)
                    )
                );
            }
            Reader.NextResult();
            var ProspectiveBuyer = dbo.ProspectiveBuyer;
            while(Reader.Read()) {
                ProspectiveBuyer.AddOrThrow(
                    new ProspectiveBuyer(
                        Reader.GetInt32(0),
                        GetString(Reader,1),
                        GetString(Reader,2),
                        GetString(Reader,3),
                        GetString(Reader,4),
                        GetDateTime(Reader,5),
                        GetString(Reader,6),
                        GetString(Reader,7),
                        GetString(Reader,8),
                        GetDecimal(Reader,9),
                        GetByte(Reader,10),
                        GetByte(Reader,11),
                        GetString(Reader,12),
                        GetString(Reader,13),
                        GetString(Reader,14),
                        GetByte(Reader,15),
                        GetString(Reader,16),
                        GetString(Reader,17),
                        GetString(Reader,18),
                        GetString(Reader,19),
                        GetString(Reader,20),
                        GetString(Reader,21),
                        GetString(Reader,22),
                        Reader.GetInt32(23)
                    )
                );
            }
        }
        vAssocSeqLineItems(e,Command);
        vAssocSeqOrders(e,Command);
        vDMPrep(e,Command);
        vTargetMail(e,Command);
        vTimeSeries(e,Command);
        e.Clear();
        Console.WriteLine($"Add {s.ElapsedMilliseconds,7}ms");
    }
    private static void vAssocSeqLineItems(Container e,SqlCommand Command) {
        {
            var r = (
                from s in vDMPrep(e)
                where s.FiscalYear==2013
                select new {
                    s.OrderNumber,
                    s.LineNumber,
                    s.Model
                }
            ).ToArray();
        }
        比較(
            () =>
                from s in vDMPrep(e)
                where s.FiscalYear==2013
                select new {
                    s.OrderNumber,
                    s.LineNumber,
                    s.Model
                },
            Command,
            "SELECT * FROM dbo.vAssocSeqLineItems",
            Reader => new {
                OrderNumber = Reader.GetString(0),
                LineNumber = Reader.GetByte(1),
                Model = GetString(Reader,2)
            }
        );
    }
    private static void vAssocSeqOrders(Container e,SqlCommand Command) {
        {
            var r = (
                from s in vDMPrep(e)
                where s.FiscalYear==2013
                select new {
                    s.OrderNumber,
                    s.CustomerKey,
                    s.Region,
                    s.IncomeGroup
                }
            ).ToArray();
        }
        var x=from s in vDMPrep(e)
            where s.FiscalYear==2013
            select new {
                s.OrderNumber,
                s.CustomerKey,
                s.Region,
                s.IncomeGroup
            };
        比較(
            () =>
                from s in vDMPrep(e)
                where s.FiscalYear==2013
                select new {
                    s.OrderNumber,
                    s.CustomerKey,
                    s.Region,
                    s.IncomeGroup
                },
            Command,
            "SELECT * FROM dbo.vAssocSeqOrders",
            (SqlDataReader Reader) => new {
                OrderNumber = Reader.GetString(0),
                CustomerKey = Reader.GetInt32(1),
                Region =GetString(Reader,2),
                IncomeGroup = GetString(Reader,3)
            });
    }
    private static ImmutableSet<AdventureWorksDW2019.Views.dbo.vDMPrep> vDMPrep(Container e)=>
        from f in e.dbo.FactInternetSales
        join d in e.dbo.DimDate on f.OrderDateKey equals d.DateKey
        join p in e.dbo.DimProduct on f.ProductKey equals p.ProductKey
        join psc in e.dbo.DimProductSubcategory on p.ProductSubcategoryKey equals psc.ProductSubcategoryKey
        join pc in e.dbo.DimProductCategory on psc.ProductCategoryKey equals pc.ProductCategoryKey
        join c in e.dbo.DimCustomer on f.CustomerKey equals c.CustomerKey
        join g in e.dbo.DimGeography on c.GeographyKey equals g.GeographyKey
        join s in e.dbo.DimSalesTerritory on g.SalesTerritoryKey equals s.SalesTerritoryKey
        select new AdventureWorksDW2019.Views.dbo.vDMPrep(
            pc.EnglishProductCategoryName,
            p.ModelName??p.EnglishProductName
            ,c.CustomerKey,
            s.SalesTerritoryGroup,
            c.BirthDate.HasValue&&DateTime.Now.Month<c.BirthDate.Value.Month ? DateTime.Now.Year-c.BirthDate.Value.Year-1
            : c.BirthDate.HasValue&&DateTime.Now.Month==c.BirthDate.Value.Month&&DateTime.Now.Day<c.BirthDate.Value.Day ? DateTime.Now.Year-c.BirthDate.Value.Year-1
            : c.BirthDate.HasValue ? (int?)DateTime.Now.Year-c.BirthDate.Value.Year
            : null,
            c.YearlyIncome<40000 ? "Low" :
            c.YearlyIncome>60000 ? "High" :
            "Moderate",
            d.CalendarYear,
            d.FiscalYear,
            d.MonthNumberOfYear,
            f.SalesOrderNumber,
            f.SalesOrderLineNumber,
            f.OrderQuantity,
            f.ExtendedAmount
        );
    private static void 実行<T>(System.Linq.Expressions.Expression<Func<IEnumerable<T>>> LINQ){
        var LINQ結果=o.Execute(LINQ);
    }
    private static void vDMPrep(Container e,SqlCommand Command) {
        //実行(
        //    () =>
        //        from f in e.dbo.FactInternetSales
        //        join d in e.dbo.DimDate on f.OrderDateKey equals d.DateKey
        //        join p in e.dbo.DimProduct on f.ProductKey equals p.ProductKey
        //        join psc in e.dbo.DimProductSubcategory on p.ProductSubcategoryKey equals psc.ProductSubcategoryKey
        //        join pc in e.dbo.DimProductCategory on psc.ProductCategoryKey equals pc.ProductCategoryKey
        //        join c in e.dbo.DimCustomer on f.CustomerKey equals c.CustomerKey
        //        join g in e.dbo.DimGeography on c.GeographyKey equals g.GeographyKey
        //        join s in e.dbo.DimSalesTerritory on g.SalesTerritoryKey equals s.SalesTerritoryKey
        //        select new {
        //            Region = s.SalesTerritoryGroup,
        //            Age =
        //                c.BirthDate.HasValue&&DateTime.Now.Month<c.BirthDate.Value.Month ? DateTime.Now.Year-c.BirthDate.Value.Year-1
        //                : c.BirthDate.HasValue&&DateTime.Now.Month==c.BirthDate.Value.Month&&DateTime.Now.Day<c.BirthDate.Value.Day ? DateTime.Now.Year-c.BirthDate.Value.Year-1
        //                : c.BirthDate.HasValue ? (Int32?)DateTime.Now.Year-c.BirthDate.Value.Year
        //                : null,
        //            IncomeGroup =
        //                c.YearlyIncome<40000 ? "Low" :
        //                c.YearlyIncome>60000 ? "High" :
        //                "Moderate",
        //            d.CalendarYear,
        //            d.FiscalYear,
        //            Month = d.MonthNumberOfYear,
        //            OrderNumber = f.SalesOrderNumber,
        //            LineNumber = f.SalesOrderLineNumber,
        //            Quantity = f.OrderQuantity,
        //            Amount = f.ExtendedAmount
        //        }
        //    );
        //実行(
        //    () =>
        //        from f in e.dbo.FactInternetSales
        //        join d in e.dbo.DimDate on f.OrderDateKey equals d.DateKey
        //        join p in e.dbo.DimProduct on f.ProductKey equals p.ProductKey
        //        join c in e.dbo.DimCustomer on f.CustomerKey equals c.CustomerKey
        //        join g in e.dbo.DimGeography on c.GeographyKey equals g.GeographyKey
        //        select new {
        //            Age =
        //                c.BirthDate.HasValue&&DateTime.Now.Day<c.BirthDate.Value.Day ? DateTime.Now.Year
        //                : f.OrderDate.HasValue ? (Int32?)DateTime.Now.Year
        //                : null,
        //            IncomeGroup =
        //                p.ProductKey<40000 ? "Low" :
        //                p.ProductKey>60000 ? "High" :
        //                "Moderate",
        //            //d,
        //            OrderNumber = f.SalesOrderNumber,
        //            LineNumber = f.SalesOrderLineNumber,
        //            Quantity = f.OrderQuantity,
        //            Amount = f.ExtendedAmount,
        //        }
        //);
        実行(
            () =>
                from f in e.dbo.FactInternetSales
                join d in e.dbo.DimDate on f.OrderDateKey equals d.DateKey
                join p in e.dbo.DimProduct on f.ProductKey equals p.ProductKey
                join c in e.dbo.DimCustomer on f.CustomerKey equals c.CustomerKey
                join g in e.dbo.DimGeography on c.GeographyKey equals g.GeographyKey
                select new {
                    Age =
                        c.BirthDate.HasValue&&DateTime.Now.Day<c.BirthDate.Value.Day ? DateTime.Now.Year
                        : f.OrderDate.HasValue ? (int?)DateTime.Now.Year
                        : null,
                    IncomeGroup =
                        p.ProductKey<40000 ? "Low" :
                        p.ProductKey>60000 ? "High" :
                        "Moderate",
                    d,
                    OrderNumber = f.SalesOrderNumber,
                    LineNumber = f.SalesOrderLineNumber,
                    Quantity = f.OrderQuantity,
                    Amount = f.ExtendedAmount,
                }
        );
        実行(
            () =>
                from f in e.dbo.FactInternetSales
                join d in e.dbo.DimDate on f.OrderDateKey equals d.DateKey
                join p in e.dbo.DimProduct on f.ProductKey equals p.ProductKey
                join psc in e.dbo.DimProductSubcategory on p.ProductSubcategoryKey equals psc.ProductSubcategoryKey
                join pc in e.dbo.DimProductCategory on psc.ProductCategoryKey equals pc.ProductCategoryKey
                join c in e.dbo.DimCustomer on f.CustomerKey equals c.CustomerKey
                join g in e.dbo.DimGeography on c.GeographyKey equals g.GeographyKey
                select new {
                    Age =
                        c.BirthDate.HasValue&&DateTime.Now.Day<c.BirthDate.Value.Day ? DateTime.Now.Year
                        : f.OrderDate.HasValue ? (int?)DateTime.Now.Year
                        : null,
                    IncomeGroup =
                        p.ProductKey<40000 ? "Low" :
                        p.ProductKey>60000 ? "High" :
                        "Moderate",
                    c,
                    d,
                    OrderNumber = f.SalesOrderNumber,
                    LineNumber = f.SalesOrderLineNumber,
                    Quantity = f.OrderQuantity,
                    Amount = f.ExtendedAmount,
                }
        );
        実行(
            () =>
                from f in e.dbo.FactInternetSales
                join d in e.dbo.DimDate on f.OrderDateKey equals d.DateKey
                join p in e.dbo.DimProduct on f.ProductKey equals p.ProductKey
                join psc in e.dbo.DimProductSubcategory on p.ProductSubcategoryKey equals psc.ProductSubcategoryKey
                join pc in e.dbo.DimProductCategory on psc.ProductCategoryKey equals pc.ProductCategoryKey
                join c in e.dbo.DimCustomer on f.CustomerKey equals c.CustomerKey
                join g in e.dbo.DimGeography on c.GeographyKey equals g.GeographyKey
                select new {
                    c,
                    Age =
                        c.BirthDate.HasValue&&DateTime.Now.Day<c.BirthDate.Value.Day ? DateTime.Now.Year
                        : f.OrderDate.HasValue ? (int?)DateTime.Now.Year
                        : null,
                    IncomeGroup =
                        p.ProductKey<40000 ? "Low" :
                        p.ProductKey>60000 ? "High" :
                        "Moderate",
                    d,
                    OrderNumber = f.SalesOrderNumber,
                    LineNumber = f.SalesOrderLineNumber,
                    Quantity = f.OrderQuantity,
                    Amount = f.ExtendedAmount,
                }
        );
        実行(
            () =>
                from f in e.dbo.FactInternetSales
                join d in e.dbo.DimDate on f.OrderDateKey equals d.DateKey
                join p in e.dbo.DimProduct on f.ProductKey equals p.ProductKey
                join psc in e.dbo.DimProductSubcategory on p.ProductSubcategoryKey equals psc.ProductSubcategoryKey
                join pc in e.dbo.DimProductCategory on psc.ProductCategoryKey equals pc.ProductCategoryKey
                join c in e.dbo.DimCustomer on f.CustomerKey equals c.CustomerKey
                join g in e.dbo.DimGeography on c.GeographyKey equals g.GeographyKey
                select new {
                    c,
                    Age =
                        c.BirthDate.HasValue&&DateTime.Now.Day<c.BirthDate.Value.Day ? DateTime.Now.Year
                        : f.OrderDate.HasValue ? (int?)DateTime.Now.Year
                        : null,
                    IncomeGroup =
                        c.YearlyIncome<40000 ? "Low" :
                        c.YearlyIncome>60000 ? "High" :
                        "Moderate",
                    d,
                    OrderNumber = f.SalesOrderNumber,
                    LineNumber = f.SalesOrderLineNumber,
                    Quantity = f.OrderQuantity,
                    Amount = f.ExtendedAmount
                }
        );
        実行(
            () =>
                from f in e.dbo.FactInternetSales
                join d in e.dbo.DimDate on f.OrderDateKey equals d.DateKey
                join p in e.dbo.DimProduct on f.ProductKey equals p.ProductKey
                join psc in e.dbo.DimProductSubcategory on p.ProductSubcategoryKey equals psc.ProductSubcategoryKey
                join pc in e.dbo.DimProductCategory on psc.ProductCategoryKey equals pc.ProductCategoryKey
                join c in e.dbo.DimCustomer on f.CustomerKey equals c.CustomerKey
                join g in e.dbo.DimGeography on c.GeographyKey equals g.GeographyKey
                select new {
                    c,
                    Age =
                        c.BirthDate.HasValue&&DateTime.Now.Day<c.BirthDate.Value.Day ? DateTime.Now.Year
                        : c.BirthDate.HasValue ? (int?)DateTime.Now.Year
                        : null,
                    IncomeGroup =
                        c.YearlyIncome<40000 ? "Low" :
                        c.YearlyIncome>60000 ? "High" :
                        "Moderate",
                    d,
                    OrderNumber = f.SalesOrderNumber,
                    LineNumber = f.SalesOrderLineNumber,
                    Quantity = f.OrderQuantity,
                    Amount = f.ExtendedAmount
                }
        );
        実行(
            () =>
                from f in e.dbo.FactInternetSales
                join d in e.dbo.DimDate on f.OrderDateKey equals d.DateKey
                join p in e.dbo.DimProduct on f.ProductKey equals p.ProductKey
                join psc in e.dbo.DimProductSubcategory on p.ProductSubcategoryKey equals psc.ProductSubcategoryKey
                join pc in e.dbo.DimProductCategory on psc.ProductCategoryKey equals pc.ProductCategoryKey
                join c in e.dbo.DimCustomer on f.CustomerKey equals c.CustomerKey
                join g in e.dbo.DimGeography on c.GeographyKey equals g.GeographyKey
                join s in e.dbo.DimSalesTerritory on g.SalesTerritoryKey equals s.SalesTerritoryKey
                select new {
                    g,
                    Age =
                        c.BirthDate.HasValue&&DateTime.Now.Day<c.BirthDate.Value.Day ? DateTime.Now.Year
                        : c.BirthDate.HasValue ? (int?)DateTime.Now.Year
                        : null,
                    IncomeGroup =
                        c.YearlyIncome<40000 ? "Low" :
                        c.YearlyIncome>60000 ? "High" :
                        "Moderate",
                    d,
                    OrderNumber = f.SalesOrderNumber,
                    LineNumber = f.SalesOrderLineNumber,
                    Quantity = f.OrderQuantity,
                    Amount = f.ExtendedAmount
                }
        );
        実行(
            ()=>
                from f in e.dbo.FactInternetSales
                join d in e.dbo.DimDate on f.OrderDateKey equals d.DateKey
                join p in e.dbo.DimProduct on f.ProductKey equals p.ProductKey
                join psc in e.dbo.DimProductSubcategory on p.ProductSubcategoryKey equals psc.ProductSubcategoryKey
                join pc in e.dbo.DimProductCategory on psc.ProductCategoryKey equals pc.ProductCategoryKey
                join c in e.dbo.DimCustomer on f.CustomerKey equals c.CustomerKey
                join g in e.dbo.DimGeography on c.GeographyKey equals g.GeographyKey
                join s in e.dbo.DimSalesTerritory on g.SalesTerritoryKey equals s.SalesTerritoryKey
                select new{
                    s,
                    Age=
                        c.BirthDate.HasValue&&DateTime.Now.Month==c.BirthDate.Value.Month&&DateTime.Now.Day<c.BirthDate.Value.Day?DateTime.Now.Year-c.BirthDate.Value.Year-1
                        :c.BirthDate.HasValue?(int?)DateTime.Now.Year
                        :null,
                    IncomeGroup=
                        c.YearlyIncome<40000?"Low":
                        c.YearlyIncome>60000?"High":
                        "Moderate",
                    d,
                    OrderNumber=f.SalesOrderNumber,
                    LineNumber=f.SalesOrderLineNumber,
                    Quantity=f.OrderQuantity,
                    Amount=f.ExtendedAmount
                }
        );
        実行(
            () =>
                from f in e.dbo.FactInternetSales
                join d in e.dbo.DimDate on f.OrderDateKey equals d.DateKey
                join p in e.dbo.DimProduct on f.ProductKey equals p.ProductKey
                join psc in e.dbo.DimProductSubcategory on p.ProductSubcategoryKey equals psc.ProductSubcategoryKey
                join pc in e.dbo.DimProductCategory on psc.ProductCategoryKey equals pc.ProductCategoryKey
                join c in e.dbo.DimCustomer on f.CustomerKey equals c.CustomerKey
                join g in e.dbo.DimGeography on c.GeographyKey equals g.GeographyKey
                join s in e.dbo.DimSalesTerritory on g.SalesTerritoryKey equals s.SalesTerritoryKey
                select new {
                    Region = s.SalesTerritoryGroup,
                    Age =
                        c.BirthDate.HasValue&&DateTime.Now.Month==c.BirthDate.Value.Month&&DateTime.Now.Day<c.BirthDate.Value.Day ? DateTime.Now.Year-c.BirthDate.Value.Year-1
                        :c.BirthDate.HasValue ? (int?)DateTime.Now.Year
                        :null,
                    IncomeGroup =
                        c.YearlyIncome<40000 ? "Low" :
                        c.YearlyIncome>60000 ? "High" :
                        "Moderate",
                    d.CalendarYear,
                    d.FiscalYear,
                    Month = d.MonthNumberOfYear,
                    OrderNumber = f.SalesOrderNumber,
                    LineNumber = f.SalesOrderLineNumber,
                    Quantity = f.OrderQuantity,
                    Amount = f.ExtendedAmount
                }
        );
        実行(
            () =>
                from f in e.dbo.FactInternetSales
                join d in e.dbo.DimDate on f.OrderDateKey equals d.DateKey
                join p in e.dbo.DimProduct on f.ProductKey equals p.ProductKey
                join psc in e.dbo.DimProductSubcategory on p.ProductSubcategoryKey equals psc.ProductSubcategoryKey
                join pc in e.dbo.DimProductCategory on psc.ProductCategoryKey equals pc.ProductCategoryKey
                join c in e.dbo.DimCustomer on f.CustomerKey equals c.CustomerKey
                join g in e.dbo.DimGeography on c.GeographyKey equals g.GeographyKey
                join s in e.dbo.DimSalesTerritory on g.SalesTerritoryKey equals s.SalesTerritoryKey
                select new {
                    //pc.EnglishProductCategoryName,
                    //Model = p.ModelName??p.EnglishProductName,
                    //c.CustomerKey,
                    Region = s.SalesTerritoryGroup,
                    Age =
                        c.BirthDate.HasValue&&DateTime.Now.Month<c.BirthDate.Value.Month ? DateTime.Now.Year-c.BirthDate.Value.Year-1
                        : c.BirthDate.HasValue&&DateTime.Now.Month==c.BirthDate.Value.Month&&DateTime.Now.Day<c.BirthDate.Value.Day ? DateTime.Now.Year-c.BirthDate.Value.Year-1
                        : c.BirthDate.HasValue ? (int?)DateTime.Now.Year-c.BirthDate.Value.Year
                        : null,
                    IncomeGroup =
                        c.YearlyIncome<40000 ? "Low" :
                        c.YearlyIncome>60000 ? "High" :
                        "Moderate",
                    d.CalendarYear,
                    d.FiscalYear,
                    Month = d.MonthNumberOfYear,
                    OrderNumber = f.SalesOrderNumber,
                    LineNumber = f.SalesOrderLineNumber,
                    Quantity = f.OrderQuantity,
                    Amount = f.ExtendedAmount
                }
        );
        {
            var r = (
                from f in e.dbo.FactInternetSales
                join d in e.dbo.DimDate on f.OrderDateKey equals d.DateKey
                join p in e.dbo.DimProduct on f.ProductKey equals p.ProductKey
                join psc in e.dbo.DimProductSubcategory on p.ProductSubcategoryKey equals psc.ProductSubcategoryKey
                join pc in e.dbo.DimProductCategory on psc.ProductCategoryKey equals pc.ProductCategoryKey
                join c in e.dbo.DimCustomer on f.CustomerKey equals c.CustomerKey
                join g in e.dbo.DimGeography on c.GeographyKey equals g.GeographyKey
                join s in e.dbo.DimSalesTerritory on g.SalesTerritoryKey equals s.SalesTerritoryKey
                select new {
                    pc.EnglishProductCategoryName,
                    Model = p.ModelName??p.EnglishProductName
                    ,c.CustomerKey,
                    Region = s.SalesTerritoryGroup,
                    Age =
                        c.BirthDate.HasValue&&DateTime.Now.Month<c.BirthDate.Value.Month ? DateTime.Now.Year-c.BirthDate.Value.Year-1
                        : c.BirthDate.HasValue&&DateTime.Now.Month==c.BirthDate.Value.Month&&DateTime.Now.Day<c.BirthDate.Value.Day ? DateTime.Now.Year-c.BirthDate.Value.Year-1
                        : c.BirthDate.HasValue ? (int?)DateTime.Now.Year-c.BirthDate.Value.Year
                        : null,
                    IncomeGroup =
                        c.YearlyIncome<40000 ? "Low" :
                        c.YearlyIncome>60000 ? "High" :
                        "Moderate",
                    d.CalendarYear,
                    d.FiscalYear,
                    Month = d.MonthNumberOfYear,
                    OrderNumber = f.SalesOrderNumber,
                    LineNumber = f.SalesOrderLineNumber,
                    Quantity = f.OrderQuantity,
                    Amount = f.ExtendedAmount
                }
            ).ToArray();
        }
        比較(
            () =>
                from f in e.dbo.FactInternetSales
                join d in e.dbo.DimDate on f.OrderDateKey equals d.DateKey
                join p in e.dbo.DimProduct on f.ProductKey equals p.ProductKey
                join psc in e.dbo.DimProductSubcategory on p.ProductSubcategoryKey equals psc.ProductSubcategoryKey
                join pc in e.dbo.DimProductCategory on psc.ProductCategoryKey equals pc.ProductCategoryKey
                join c in e.dbo.DimCustomer on f.CustomerKey equals c.CustomerKey
                join g in e.dbo.DimGeography on c.GeographyKey equals g.GeographyKey
                join s in e.dbo.DimSalesTerritory on g.SalesTerritoryKey equals s.SalesTerritoryKey
                select new {
                    pc.EnglishProductCategoryName,
                    Model = p.ModelName??p.EnglishProductName
                    ,c.CustomerKey,
                    Region = s.SalesTerritoryGroup,
                    Age =
                        c.BirthDate.HasValue&&DateTime.Now.Month<c.BirthDate.Value.Month ? DateTime.Now.Year-c.BirthDate.Value.Year-1
                        : c.BirthDate.HasValue&&DateTime.Now.Month==c.BirthDate.Value.Month&&DateTime.Now.Day<c.BirthDate.Value.Day ? DateTime.Now.Year-c.BirthDate.Value.Year-1
                        : c.BirthDate.HasValue ? (int?)DateTime.Now.Year-c.BirthDate.Value.Year
                        : null,
                    IncomeGroup =
                        c.YearlyIncome<40000 ? "Low" :
                        c.YearlyIncome>60000 ? "High" :
                        "Moderate",
                    d.CalendarYear,
                    d.FiscalYear,
                    Month = d.MonthNumberOfYear,
                    OrderNumber = f.SalesOrderNumber,
                    LineNumber = f.SalesOrderLineNumber,
                    Quantity = f.OrderQuantity,
                    Amount = f.ExtendedAmount
                },
            Command,
            "SELECT * FROM dbo.vDMPrep",
            Reader => new {
                EnglishProductCategoryName = Reader.GetString(0),
                Model = GetString(Reader,1),
                CustomerKey = Reader.GetInt32(2),
                Region = GetString(Reader,3),
                Age = GetInt32(Reader,4),
                IncomeGroup = Reader.GetString(5),
                CalendarYear = Reader.GetInt16(6),
                FiscalYear = Reader.GetInt16(7),
                Month = Reader.GetByte(8),
                OrderNumber = Reader.GetString(9),
                LineNumber = Reader.GetByte(10),
                Quantity = Reader.GetInt16(11),
                Amount = Reader.GetDecimal(12),
            });
    }
    private static void vTargetMail(Container e,SqlCommand Command) {
        {
            var r = (
                from c in e.dbo.DimCustomer
                join x in (
                        from v in vDMPrep(e)
                        group v by new {
                            v.CustomerKey
                            ,v.Region
                            ,v.Age
                        }into g
                        select new {
                            g.Key.CustomerKey
                            ,g.Key.Region
                            ,g.Key.Age
                            ,Bikes=g.Sum(
                                p => p.EnglishProductCategoryName=="Bikes"
                                    ?1:0
                            )
                        }
                    )
                    on c.CustomerKey equals x.CustomerKey
                select new {
                    c.CustomerKey,
                    c.GeographyKey,
                    c.CustomerAlternateKey,
                    c.Title,
                    c.FirstName,
                    c.MiddleName,
                    c.LastName,
                    c.NameStyle,
                    c.BirthDate,
                    c.MaritalStatus,
                    c.Suffix,
                    c.Gender,
                    c.EmailAddress,
                    c.YearlyIncome,
                    c.TotalChildren,
                    c.NumberChildrenAtHome,
                    c.EnglishEducation,
                    c.SpanishEducation,
                    c.FrenchEducation,
                    c.EnglishOccupation,
                    c.SpanishOccupation,
                    c.FrenchOccupation,
                    c.HouseOwnerFlag,
                    c.NumberCarsOwned,
                    c.AddressLine1,
                    c.AddressLine2,
                    c.Phone,
                    c.DateFirstPurchase,
                    c.CommuteDistance,
                    x.Region,
                    x.Age,
                    BikeBuyer = x.Bikes==0
                        ? 0 : 1
                }
            ).ToArray();
        }
        比較(
            () =>
                from c in e.dbo.DimCustomer
                join x in (
                        from v in vDMPrep(e)
                        group v by new {
                            v.CustomerKey
                            ,v.Region
                            ,v.Age
                        } into g
                        select new {
                            g.Key.CustomerKey
                            ,g.Key.Region
                            ,g.Key.Age
                            ,Bikes = g.Sum(
                                p => p.EnglishProductCategoryName=="Bikes"
                                    ? 1 : 0
                            )
                        }
                    )
                    on c.CustomerKey equals x.CustomerKey
                select new {
                    c.CustomerKey,
                    c.GeographyKey,
                    c.CustomerAlternateKey,
                    c.Title,
                    c.FirstName,
                    c.MiddleName,
                    c.LastName,
                    c.NameStyle,
                    c.BirthDate,
                    c.MaritalStatus,
                    c.Suffix,
                    c.Gender,
                    c.EmailAddress,
                    c.YearlyIncome,
                    c.TotalChildren,
                    c.NumberChildrenAtHome,
                    c.EnglishEducation,
                    c.SpanishEducation,
                    c.FrenchEducation,
                    c.EnglishOccupation,
                    c.SpanishOccupation,
                    c.FrenchOccupation,
                    c.HouseOwnerFlag,
                    c.NumberCarsOwned,
                    c.AddressLine1,
                    c.AddressLine2,
                    c.Phone,
                    c.DateFirstPurchase,
                    c.CommuteDistance,
                    x.Region,
                    x.Age,
                    BikeBuyer = x.Bikes==0
                        ? 0 : 1
                },
            Command,
            "SELECT * FROM dbo.vTargetMail",
            Reader => new {
                CustomerKey = Reader.GetInt32(0),
                GeographyKey = GetInt32(Reader,1),
                CustomerAlternateKey = Reader.GetString(2),
                Title = GetString(Reader,3),
                FirstName = GetString(Reader,4),
                MiddleName = GetString(Reader,5),
                LastName = GetString(Reader,6),
                NameStyle = GetBoolean(Reader,7),
                BirthDate = GetDateTime(Reader,8),
                MaritalStatus = GetString(Reader,9),
                Suffix = GetString(Reader,10),
                Gender = GetString(Reader,11),
                EmailAddress = GetString(Reader,12),
                YearlyIncome = GetDecimal(Reader,13),
                TotalChildren = GetByte(Reader,14),
                NumberChildrenAtHome = GetByte(Reader,15),
                EnglishEducation = GetString(Reader,16),
                SpanishEducation = GetString(Reader,17),
                FrenchEducation =GetString(Reader,18),
                EnglishOccupation =GetString(Reader,19),
                SpanishOccupation = GetString(Reader,20),
                FrenchOccupation = GetString(Reader,21),
                HouseOwnerFlag = GetString(Reader,22),
                NumberCarsOwned = GetByte(Reader,23),
                AddressLine1 = GetString(Reader,24),
                AddressLine2 = GetString(Reader,25),
                Phone = GetString(Reader,26),
                DateFirstPurchase = GetDateTime(Reader,27),
                CommuteDistance = GetString(Reader,28),
                Region = GetString(Reader,29),
                Age = GetInt32(Reader,30),
                BikeBuyer = Reader.GetInt32(31),
            }
        );
    }
    private static DateTime udfBuildISO8601Date(int year,int month,int day)=>new DateTime(year,month,day);
    private static void vTimeSeries(Container e,SqlCommand Command) {
        {
            var r = (
                from c in vDMPrep(e)
                where new[] { "Mountain-100","Mountain-200","Road-150","Road-250","Road-650","Road-750","Touring-1000" }.Contains(c.Model)
                group c by new {
                    ModelRegion = c.Model switch
                    {
                        "Mountain-100" => "M200",
                        "Road-150" => "R250",
                        "Road-650" => "R750",
                        "Touring-1000" => "T1000",
                        _=>c.Model.Substring(0,1)+c.Model.Substring(c.Model.Length-3,3)
                    }+" "+c.Region,
                    TimeIndex = c.CalendarYear*100+c.Month,
                    c.CalendarYear,
                    c.Month,
                    ReportingDate = udfBuildISO8601Date(c.CalendarYear,c.Month,25)
                } into g
                select new {
                    g.Key.ModelRegion,
                    g.Key.TimeIndex,
                    Quantity = g.Sum(p => p.Quantity),
                    Amount = g.Sum(p => p.Amount),
                    g.Key.CalendarYear,
                    g.Key.Month,
                    g.Key.ReportingDate
                }
            ).ToArray();
        }
        比較(
            () =>
                from c in vDMPrep(e)
                where new[] { "Mountain-100","Mountain-200","Road-150","Road-250","Road-650","Road-750","Touring-1000" }.Contains(c.Model)
                group c by new {
                    ModelRegion=(c.Model=="Mountain-100"
                            ?"M200" :c.Model=="Road-150"
                                ?"R250" :c.Model=="Road-650"
                                    ?"R750" :c.Model=="Touring-1000"
                                        ?"T1000":c.Model.Substring(0,1)+c.Model.Substring(c.Model.Length-3,3)
                        )+" "+c.Region,
                    TimeIndex = c.CalendarYear*100+c.Month,
                    c.CalendarYear,
                    c.Month,
                    ReportingDate = udfBuildISO8601Date(c.CalendarYear,c.Month,25)
                } into g
                select new {
                    g.Key.ModelRegion,
                    TimeIndex = (int?)g.Key.TimeIndex,
                    Quantity = g.Sum(p => (int?)p.Quantity),
                    Amount = g.Sum(p => (decimal?)p.Amount),
                    g.Key.CalendarYear,
                    g.Key.Month,
                    ReportingDate=(DateTime?)g.Key.ReportingDate
                },
            Command,
            "SELECT * FROM dbo.vTimeSeries",
            Reader => new {
                ModelRegion = GetString(Reader,0),
                TimeIndex = GetInt32(Reader,1),
                Quantity = GetInt32(Reader,2),
                Amount = GetDecimal(Reader,3),
                CalendarYear = Reader.GetInt16(4),
                Month = Reader.GetByte(5),
                ReportingDate = GetDateTime(Reader,6),
            }
        );
    }
    private static void Main() {
        Load();
        Create();
        var r = new Random(2);
        Transaction(() => r.Next());
        var index = 0;
        Transaction(() => index++);
#pragma warning disable CA1303 // ローカライズされるパラメーターとしてリテラルを渡さない
        Console.WriteLine("終了");
#pragma warning restore CA1303 // ローカライズされるパラメーターとしてリテラルを渡さない
        Console.ReadKey();
    }
}