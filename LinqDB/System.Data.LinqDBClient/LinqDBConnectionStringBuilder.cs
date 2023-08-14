using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.ComponentModel;
using System.Globalization;
using System.ComponentModel.Design.Serialization;
// ReSharper disable once CheckNamespace
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
namespace System.Data.LinqDBClient;

public sealed class LinqDBConnectionStringBuilder:DbConnectionStringBuilder {
    private enum Keywords {
        DataSource = 0,
        FailoverPartner = 1,
        AttachDBFilename = 2,
        InitialCatalog = 3,
        IntegratedSecurity = 4,
        PersistSecurityInfo = 5,
        UserID = 6,
        Password = 7,
        //Enlist = 8,
        //Pooling = 9,
        //MinPoolSize = 10,
        //MaxPoolSize = 11,
        //PoolBlockingPeriod = 12,
        //AsynchronousProcessing = 13,
        //ConnectionReset = 14,
        //MultipleActiveResultSets = 0xF,
        //Replication = 0x10,
        //ConnectTimeout = 17,
        //Encrypt = 18,
        //TrustServerCertificate = 19,
        //LoadBalanceTimeout = 20,
        //NetworkLibrary = 21,
        //PacketSize = 22,
        //TypeSystemVersion = 23,
        //Authentication = 24,
        //ApplicationName = 25,
        //CurrentLanguage = 26,
        //WorkstationID = 27,
        //UserInstance = 28,
        //ContextConnection = 29,
        //TransactionBinding = 30,
        //ApplicationIntent = 0x1F,
        //MultiSubnetFailover = 0x20,
        //TransparentNetworkIPResolution = 33,
        //ConnectRetryCount = 34,
        //ConnectRetryInterval = 35,
        //ColumnEncryptionSetting = 36,
        //EnclaveAttestationUrl = 37,
        //KeywordsCount = 38
    }

    private sealed class NetworkLibraryConverter:TypeConverter {
        //private const String NamedPipes = "Named Pipes (DBNMPNTW)";

        //private const String SharedMemory = "Shared Memory (DBMSLPCN)";

        //private const String TCPIP = "TCP/IP (DBMSSOCN)";

        //private const String VIA = "VIA (DBMSGNET)";

        //private StandardValuesCollection _standardValues;

        public override bool CanConvertFrom(ITypeDescriptorContext? context,Type sourceType) {
            if(!(typeof(string)==sourceType)) {
                return base.CanConvertFrom(context,sourceType);
            }
            return true;
        }

        public override object? ConvertFrom(ITypeDescriptorContext? context,CultureInfo? culture,object value) {
            if(value is string text) {
                text=text.Trim();
                if(StringComparer.OrdinalIgnoreCase.Equals(text,"Named Pipes (DBNMPNTW)")) {
                    return "dbnmpntw";
                }
                if(StringComparer.OrdinalIgnoreCase.Equals(text,"Shared Memory (DBMSLPCN)")) {
                    return "dbmslpcn";
                }
                if(StringComparer.OrdinalIgnoreCase.Equals(text,"TCP/IP (DBMSSOCN)")) {
                    return "dbmssocn";
                }
                if(StringComparer.OrdinalIgnoreCase.Equals(text,"VIA (DBMSGNET)")) {
                    return "dbmsgnet";
                }
                return text;
            }
            return base.ConvertFrom(context,culture,value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext? context,Type? destinationType) {
            if(!(typeof(string)==destinationType)) {
                return base.CanConvertTo(context,destinationType);
            }
            return true;
        }

        public override object? ConvertTo(ITypeDescriptorContext? context,CultureInfo? culture,object? value,Type destinationType) {
            if(value is string text&&destinationType==typeof(string))
            {
                return text.Trim().ToUpperInvariant() switch
                {
                    "DBNMPNTW"=>"Named Pipes (DBNMPNTW)",
                    "DBMSLPCN"=>"Shared Memory (DBMSLPCN)",
                    "DBMSSOCN"=>"TCP/IP (DBMSSOCN)",
                    "DBMSGNET"=>"VIA (DBMSGNET)",
                    _=>text
                };
            }
            return base.ConvertTo(context,culture,value,destinationType);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext? context) {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext? context) {
            return false;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext? context) {
            var values = new string[4]
            {
                "Named Pipes (DBNMPNTW)",
                "Shared Memory (DBMSLPCN)",
                "TCP/IP (DBMSSOCN)",
                "VIA (DBMSGNET)"
            };
            return new StandardValuesCollection(values);
        }
    }


    internal sealed class SqlConnectionStringBuilderConverter:ExpandableObjectConverter {
        public override bool CanConvertTo(ITypeDescriptorContext? context,Type? destinationType) {
            if(typeof(InstanceDescriptor)==destinationType) {
                return true;
            }
            return base.CanConvertTo(context,destinationType);
        }

        public override object? ConvertTo(ITypeDescriptorContext? context,CultureInfo? culture,object? value,Type destinationType) {
            if(destinationType is null) {
                throw new ArgumentNullException(nameof(destinationType));
            }
            if(typeof(InstanceDescriptor)==destinationType&&value is LinqDBConnectionStringBuilder sqlConnectionStringBuilder) {
                return this.ConvertToInstanceDescriptor(sqlConnectionStringBuilder);
            }
            return base.ConvertTo(context,culture,value,destinationType);
        }

        private InstanceDescriptor ConvertToInstanceDescriptor(LinqDBConnectionStringBuilder options) {
            var types = new Type[1]
            {
                typeof(string)
            };
            var arguments = new object[1]
            {
                options.ConnectionString
            };
            var constructor = typeof(LinqDBConnectionStringBuilder).GetConstructor(types);
            return new InstanceDescriptor(constructor,arguments);
        }
    }
    private readonly Dictionary<string,object> DictionaryStringObject = new();
    public override object this[string keyword] {
        get => this.DictionaryStringObject[keyword];
#pragma warning disable CS8765 // パラメーターの型の NULL 値の許容が、オーバーライドされたメンバーと一致しません。おそらく、NULL 値の許容の属性が原因です。
        set => this.DictionaryStringObject.Add(keyword,value);
#pragma warning restore CS8765 // パラメーターの型の NULL 値の許容が、オーバーライドされたメンバーと一致しません。おそらく、NULL 値の許容の属性が原因です。
    }
    public LinqDBConnectionStringBuilder()
        : this("") {
    }
    public LinqDBConnectionStringBuilder(string ConnectionString){
        base.ConnectionString=ConnectionString;
    }
    public override bool ContainsKey(string keyword)=>this.DictionaryStringObject.ContainsKey(keyword);
    public override bool Remove(string keyword) => this.DictionaryStringObject.Remove(keyword);
    public override bool ShouldSerialize(string keyword) {
        if(this.DictionaryStringObject.TryGetValue(keyword,out var value)) {
            return base.ShouldSerialize(value.ToString());
        }
        return false;
    }
    public override bool TryGetValue(string keyword,out object value) {
        if(this.DictionaryStringObject.TryGetValue(keyword,out value!)) {
            return true;
        }
        return false;
    }
    //public DnsEndPoint? DnsEndPoint { get; set; }
    public string? Host { get; set; }
    public int Port { get; set; }
    public string? User { get; set; }
    private string? _Password;
#pragma warning disable CA1044 // プロパティを書き込み専用にすることはできません
    public string? Password{ set=>this._Password=value; }
#pragma warning restore CA1044 // プロパティを書き込み専用にすることはできません
    public string? Database { get; set; }
    private readonly StringBuilder sb = new();
    private void UpdateConnectionString() {
        var sb = this.sb;
        sb.Clear();
        if(this.Host is not null) {
            sb.Append($"Host=\"{this.Host.Replace("\"","\"\"")}:{this.Port}\";");
        }
        if(this.User is not null) {
            sb.Append($"User=\"{this.User.Replace("\"","\"\"")}\";");
        }
        if(this.Database is not null) {
            sb.Append($"Database=\"{this.Database.Replace("\"","\"\"")}\";");
        }
        this.ConnectionString=sb.ToString();
    }
}