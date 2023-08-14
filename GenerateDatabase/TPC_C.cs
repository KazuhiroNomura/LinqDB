#pragma warning disable CS0109 // メンバーは継承されたメンバーを非表示にしません。new キーワードは不要です
namespace TPC_C
    public static class ChildExtensions{
        public static global::LinqDB.Sets.ASet<Tables.dbo.history>FK_history_customer(this Tables.dbo.customer customer)=>customer.FK_history_customer;
        public static global::LinqDB.Sets.ASet<Tables.dbo.orders>FK_orders_customer(this Tables.dbo.customer customer)=>customer.FK_orders_customer;
        public static global::LinqDB.Sets.ASet<Tables.dbo.customer>FK_customer_district(this Tables.dbo.district district)=>district.FK_customer_district;
        public static global::LinqDB.Sets.ASet<Tables.dbo.history>FK_history_district(this Tables.dbo.district district)=>district.FK_history_district;
        public static global::LinqDB.Sets.ASet<Tables.dbo.stock>FK_stock_item(this Tables.dbo.item item)=>item.FK_stock_item;
        public static global::LinqDB.Sets.ASet<Tables.dbo.new_orders>FK_new_orders_orders(this Tables.dbo.orders orders)=>orders.FK_new_orders_orders;
        public static global::LinqDB.Sets.ASet<Tables.dbo.order_line>FK_order_line_orders(this Tables.dbo.orders orders)=>orders.FK_order_line_orders;
        public static global::LinqDB.Sets.ASet<Tables.dbo.order_line>FK_order_line_stock(this Tables.dbo.stock stock)=>stock.FK_order_line_stock;
        public static global::LinqDB.Sets.ASet<Tables.dbo.district>FK_district_warehouse(this Tables.dbo.warehouse warehouse)=>warehouse.FK_district_warehouse;
        public static global::LinqDB.Sets.ASet<Tables.dbo.stock>FK_stock_warehouse(this Tables.dbo.warehouse warehouse)=>warehouse.FK_stock_warehouse;
    }
    public static class ParentExtensions{
        public static Tables.dbo.district FK_customer_district(this Tables.dbo.customer customer)=>customer.FK_customer_district;
        public static Tables.dbo.warehouse FK_district_warehouse(this Tables.dbo.district district)=>district.FK_district_warehouse;
        public static Tables.dbo.customer FK_history_customer(this Tables.dbo.history history)=>history.FK_history_customer;
        public static Tables.dbo.district FK_history_district(this Tables.dbo.history history)=>history.FK_history_district;
        public static Tables.dbo.orders FK_new_orders_orders(this Tables.dbo.new_orders new_orders)=>new_orders.FK_new_orders_orders;
        public static Tables.dbo.orders FK_order_line_orders(this Tables.dbo.order_line order_line)=>order_line.FK_order_line_orders;
        public static Tables.dbo.stock FK_order_line_stock(this Tables.dbo.order_line order_line)=>order_line.FK_order_line_stock;
        public static Tables.dbo.customer FK_orders_customer(this Tables.dbo.orders orders)=>orders.FK_orders_customer;
        public static Tables.dbo.item FK_stock_item(this Tables.dbo.stock stock)=>stock.FK_stock_item;
        public static Tables.dbo.warehouse FK_stock_warehouse(this Tables.dbo.stock stock)=>stock.FK_stock_warehouse;
    }
    namespace PrimaryKeys{
        namespace {db_accessadmin{
        }
        namespace {db_backupoperator{
        }
        namespace {db_datareader{
        }
        namespace {db_datawriter{
        }
        namespace {db_ddladmin{
        }
        namespace {db_denydatareader{
        }
        namespace {db_denydatawriter{
        }
        namespace {db_owner{
        }
        namespace {db_securityadmin{
        }
        namespace {dbo{
            [global::System.Serializable]
            public struct customer:global::System.IEquatable<customer>{
                public global::System.Int32 c_w_id;
                public global::System.Int32 c_d_id;
                public global::System.Int32 c_id;
                public customer(global::System.Int32 c_w_id,global::System.Int32 c_d_id,global::System.Int32 c_id){                    this.c_w_id=c_w_id;
                    this.c_d_id=c_d_id;
                    this.c_id=c_id;
                }
                internal void ToStringBuilder(global::System.Text.StringBuilder sb){
                    sb.Append(nameof(this.c_w_id),"="+this.c_w_id);
                    sb.Append(nameof(this.c_d_id),"="+this.c_d_id);
                    sb.Append(nameof(this.c_id),"="+this.c_id);
                }
                public override global::System.String ToString(){
                    var sb=new global::System.Text.StringBuilder();
                    this.ToStringBuilder(sb);
                    return sb.ToString();
                }
                internal void InputHashCode(ref global::LinqDB.CRC.CRC32 CRC){
                    CRC.Input(this.c_w_id);
                    CRC.Input(this.c_d_id);
                    CRC.Input(this.c_id);
                }
                public override global::System.Int32 GetHashCode(){
                    var CRC=new global::LinqDB.CRC.CRC32();
                    CRC.Input(this.c_w_id);
                    CRC.Input(this.c_d_id);
                    CRC.Input(this.c_id);
                    return CRC.GetHashCode();
                }
                public global::System.Boolean Equals(customer other){
                    if(!this.c_w_id.Equals(other.c_w_id))return false;
                    if(!this.c_d_id.Equals(other.c_d_id))return false;
                    if(!this.c_id.Equals(other.c_id))return false;
                    return true;
                }
                public override global::System.Boolean Equals(global::System.Object obj)=>this.Equals((customer)obj);
                public static global::System.Boolean operator==(customer x,customer y)=> x.Equals(y);
                public static global::System.Boolean operator!=(customer x,customer y)=>!x.Equals(y);
            }
            [global::System.Serializable]
            public struct district:global::System.IEquatable<district>{
                public global::System.Int32 d_w_id;
                public global::System.Int32 d_id;
                public district(global::System.Int32 d_w_id,global::System.Int32 d_id){                    this.d_w_id=d_w_id;
                    this.d_id=d_id;
                }
                internal void ToStringBuilder(global::System.Text.StringBuilder sb){
                    sb.Append(nameof(this.d_w_id),"="+this.d_w_id);
                    sb.Append(nameof(this.d_id),"="+this.d_id);
                }
                public override global::System.String ToString(){
                    var sb=new global::System.Text.StringBuilder();
                    this.ToStringBuilder(sb);
                    return sb.ToString();
                }
                internal void InputHashCode(ref global::LinqDB.CRC.CRC32 CRC){
                    CRC.Input(this.d_w_id);
                    CRC.Input(this.d_id);
                }
                public override global::System.Int32 GetHashCode(){
                    var CRC=new global::LinqDB.CRC.CRC32();
                    CRC.Input(this.d_w_id);
                    CRC.Input(this.d_id);
                    return CRC.GetHashCode();
                }
                public global::System.Boolean Equals(district other){
                    if(!this.d_w_id.Equals(other.d_w_id))return false;
                    if(!this.d_id.Equals(other.d_id))return false;
                    return true;
                }
                public override global::System.Boolean Equals(global::System.Object obj)=>this.Equals((district)obj);
                public static global::System.Boolean operator==(district x,district y)=> x.Equals(y);
                public static global::System.Boolean operator!=(district x,district y)=>!x.Equals(y);
            }
            [global::System.Serializable]
            public struct history:global::System.IEquatable<history>{
                public global::System.Int32 h_w_id;
                public global::System.Int32 h_d_id;
                public global::System.Int32 h_c_w_id;
                public global::System.Int32 h_c_d_id;
                public global::System.Int32 h_c_id;
                public global::System.DateTime h_date;
                public history(global::System.Int32 h_w_id,global::System.Int32 h_d_id,global::System.Int32 h_c_w_id,global::System.Int32 h_c_d_id,global::System.Int32 h_c_id,global::System.DateTime h_date){                    this.h_w_id=h_w_id;
                    this.h_d_id=h_d_id;
                    this.h_c_w_id=h_c_w_id;
                    this.h_c_d_id=h_c_d_id;
                    this.h_c_id=h_c_id;
                    this.h_date=h_date;
                }
                internal void ToStringBuilder(global::System.Text.StringBuilder sb){
                    sb.Append(nameof(this.h_w_id),"="+this.h_w_id);
                    sb.Append(nameof(this.h_d_id),"="+this.h_d_id);
                    sb.Append(nameof(this.h_c_w_id),"="+this.h_c_w_id);
                    sb.Append(nameof(this.h_c_d_id),"="+this.h_c_d_id);
                    sb.Append(nameof(this.h_c_id),"="+this.h_c_id);
                    sb.Append(nameof(this.h_date),"="+this.h_date);
                }
                public override global::System.String ToString(){
                    var sb=new global::System.Text.StringBuilder();
                    this.ToStringBuilder(sb);
                    return sb.ToString();
                }
                internal void InputHashCode(ref global::LinqDB.CRC.CRC32 CRC){
                    CRC.Input(this.h_w_id);
                    CRC.Input(this.h_d_id);
                    CRC.Input(this.h_c_w_id);
                    CRC.Input(this.h_c_d_id);
                    CRC.Input(this.h_c_id);
                    CRC.Input(this.h_date);
                }
                public override global::System.Int32 GetHashCode(){
                    var CRC=new global::LinqDB.CRC.CRC32();
                    CRC.Input(this.h_w_id);
                    CRC.Input(this.h_d_id);
                    CRC.Input(this.h_c_w_id);
                    CRC.Input(this.h_c_d_id);
                    CRC.Input(this.h_c_id);
                    CRC.Input(this.h_date);
                    return CRC.GetHashCode();
                }
                public global::System.Boolean Equals(history other){
                    if(!this.h_w_id.Equals(other.h_w_id))return false;
                    if(!this.h_d_id.Equals(other.h_d_id))return false;
                    if(!this.h_c_w_id.Equals(other.h_c_w_id))return false;
                    if(!this.h_c_d_id.Equals(other.h_c_d_id))return false;
                    if(!this.h_c_id.Equals(other.h_c_id))return false;
                    if(!this.h_date.Equals(other.h_date))return false;
                    return true;
                }
                public override global::System.Boolean Equals(global::System.Object obj)=>this.Equals((history)obj);
                public static global::System.Boolean operator==(history x,history y)=> x.Equals(y);
                public static global::System.Boolean operator!=(history x,history y)=>!x.Equals(y);
            }
            [global::System.Serializable]
            public struct item:global::System.IEquatable<item>{
                public global::System.Int32 i_id;
                public item(global::System.Int32 i_id){                    this.i_id=i_id;
                }
                internal void ToStringBuilder(global::System.Text.StringBuilder sb){
                    sb.Append(nameof(this.i_id),"="+this.i_id);
                }
                public override global::System.String ToString(){
                    var sb=new global::System.Text.StringBuilder();
                    this.ToStringBuilder(sb);
                    return sb.ToString();
                }
                internal void InputHashCode(ref global::LinqDB.CRC.CRC32 CRC){
                    CRC.Input(this.i_id);
                }
                public override global::System.Int32 GetHashCode()=>this.i_id;
                public global::System.Boolean Equals(item other){
                    if(!this.i_id.Equals(other.i_id))return false;
                    return true;
                }
                public override global::System.Boolean Equals(global::System.Object obj)=>this.Equals((item)obj);
                public static global::System.Boolean operator==(item x,item y)=> x.Equals(y);
                public static global::System.Boolean operator!=(item x,item y)=>!x.Equals(y);
            }
            [global::System.Serializable]
            public struct new_orders:global::System.IEquatable<new_orders>{
                public global::System.Int32 no_w_id;
                public global::System.Int32 no_d_id;
                public global::System.Int32 no_o_id;
                public new_orders(global::System.Int32 no_w_id,global::System.Int32 no_d_id,global::System.Int32 no_o_id){                    this.no_w_id=no_w_id;
                    this.no_d_id=no_d_id;
                    this.no_o_id=no_o_id;
                }
                internal void ToStringBuilder(global::System.Text.StringBuilder sb){
                    sb.Append(nameof(this.no_w_id),"="+this.no_w_id);
                    sb.Append(nameof(this.no_d_id),"="+this.no_d_id);
                    sb.Append(nameof(this.no_o_id),"="+this.no_o_id);
                }
                public override global::System.String ToString(){
                    var sb=new global::System.Text.StringBuilder();
                    this.ToStringBuilder(sb);
                    return sb.ToString();
                }
                internal void InputHashCode(ref global::LinqDB.CRC.CRC32 CRC){
                    CRC.Input(this.no_w_id);
                    CRC.Input(this.no_d_id);
                    CRC.Input(this.no_o_id);
                }
                public override global::System.Int32 GetHashCode(){
                    var CRC=new global::LinqDB.CRC.CRC32();
                    CRC.Input(this.no_w_id);
                    CRC.Input(this.no_d_id);
                    CRC.Input(this.no_o_id);
                    return CRC.GetHashCode();
                }
                public global::System.Boolean Equals(new_orders other){
                    if(!this.no_w_id.Equals(other.no_w_id))return false;
                    if(!this.no_d_id.Equals(other.no_d_id))return false;
                    if(!this.no_o_id.Equals(other.no_o_id))return false;
                    return true;
                }
                public override global::System.Boolean Equals(global::System.Object obj)=>this.Equals((new_orders)obj);
                public static global::System.Boolean operator==(new_orders x,new_orders y)=> x.Equals(y);
                public static global::System.Boolean operator!=(new_orders x,new_orders y)=>!x.Equals(y);
            }
            [global::System.Serializable]
            public struct order_line:global::System.IEquatable<order_line>{
                public global::System.Int32 ol_w_id;
                public global::System.Int32 ol_d_id;
                public global::System.Int32 ol_o_id;
                public global::System.Int32 ol_number;
                public order_line(global::System.Int32 ol_w_id,global::System.Int32 ol_d_id,global::System.Int32 ol_o_id,global::System.Int32 ol_number){                    this.ol_w_id=ol_w_id;
                    this.ol_d_id=ol_d_id;
                    this.ol_o_id=ol_o_id;
                    this.ol_number=ol_number;
                }
                internal void ToStringBuilder(global::System.Text.StringBuilder sb){
                    sb.Append(nameof(this.ol_w_id),"="+this.ol_w_id);
                    sb.Append(nameof(this.ol_d_id),"="+this.ol_d_id);
                    sb.Append(nameof(this.ol_o_id),"="+this.ol_o_id);
                    sb.Append(nameof(this.ol_number),"="+this.ol_number);
                }
                public override global::System.String ToString(){
                    var sb=new global::System.Text.StringBuilder();
                    this.ToStringBuilder(sb);
                    return sb.ToString();
                }
                internal void InputHashCode(ref global::LinqDB.CRC.CRC32 CRC){
                    CRC.Input(this.ol_w_id);
                    CRC.Input(this.ol_d_id);
                    CRC.Input(this.ol_o_id);
                    CRC.Input(this.ol_number);
                }
                public override global::System.Int32 GetHashCode(){
                    var CRC=new global::LinqDB.CRC.CRC32();
                    CRC.Input(this.ol_w_id);
                    CRC.Input(this.ol_d_id);
                    CRC.Input(this.ol_o_id);
                    CRC.Input(this.ol_number);
                    return CRC.GetHashCode();
                }
                public global::System.Boolean Equals(order_line other){
                    if(!this.ol_w_id.Equals(other.ol_w_id))return false;
                    if(!this.ol_d_id.Equals(other.ol_d_id))return false;
                    if(!this.ol_o_id.Equals(other.ol_o_id))return false;
                    if(!this.ol_number.Equals(other.ol_number))return false;
                    return true;
                }
                public override global::System.Boolean Equals(global::System.Object obj)=>this.Equals((order_line)obj);
                public static global::System.Boolean operator==(order_line x,order_line y)=> x.Equals(y);
                public static global::System.Boolean operator!=(order_line x,order_line y)=>!x.Equals(y);
            }
            [global::System.Serializable]
            public struct orders:global::System.IEquatable<orders>{
                public global::System.Int32 o_w_id;
                public global::System.Int32 o_d_id;
                public global::System.Int32 o_id;
                public orders(global::System.Int32 o_w_id,global::System.Int32 o_d_id,global::System.Int32 o_id){                    this.o_w_id=o_w_id;
                    this.o_d_id=o_d_id;
                    this.o_id=o_id;
                }
                internal void ToStringBuilder(global::System.Text.StringBuilder sb){
                    sb.Append(nameof(this.o_w_id),"="+this.o_w_id);
                    sb.Append(nameof(this.o_d_id),"="+this.o_d_id);
                    sb.Append(nameof(this.o_id),"="+this.o_id);
                }
                public override global::System.String ToString(){
                    var sb=new global::System.Text.StringBuilder();
                    this.ToStringBuilder(sb);
                    return sb.ToString();
                }
                internal void InputHashCode(ref global::LinqDB.CRC.CRC32 CRC){
                    CRC.Input(this.o_w_id);
                    CRC.Input(this.o_d_id);
                    CRC.Input(this.o_id);
                }
                public override global::System.Int32 GetHashCode(){
                    var CRC=new global::LinqDB.CRC.CRC32();
                    CRC.Input(this.o_w_id);
                    CRC.Input(this.o_d_id);
                    CRC.Input(this.o_id);
                    return CRC.GetHashCode();
                }
                public global::System.Boolean Equals(orders other){
                    if(!this.o_w_id.Equals(other.o_w_id))return false;
                    if(!this.o_d_id.Equals(other.o_d_id))return false;
                    if(!this.o_id.Equals(other.o_id))return false;
                    return true;
                }
                public override global::System.Boolean Equals(global::System.Object obj)=>this.Equals((orders)obj);
                public static global::System.Boolean operator==(orders x,orders y)=> x.Equals(y);
                public static global::System.Boolean operator!=(orders x,orders y)=>!x.Equals(y);
            }
            [global::System.Serializable]
            public struct stock:global::System.IEquatable<stock>{
                public global::System.Int32 s_w_id;
                public global::System.Int32 s_i_id;
                public stock(global::System.Int32 s_w_id,global::System.Int32 s_i_id){                    this.s_w_id=s_w_id;
                    this.s_i_id=s_i_id;
                }
                internal void ToStringBuilder(global::System.Text.StringBuilder sb){
                    sb.Append(nameof(this.s_w_id),"="+this.s_w_id);
                    sb.Append(nameof(this.s_i_id),"="+this.s_i_id);
                }
                public override global::System.String ToString(){
                    var sb=new global::System.Text.StringBuilder();
                    this.ToStringBuilder(sb);
                    return sb.ToString();
                }
                internal void InputHashCode(ref global::LinqDB.CRC.CRC32 CRC){
                    CRC.Input(this.s_w_id);
                    CRC.Input(this.s_i_id);
                }
                public override global::System.Int32 GetHashCode(){
                    var CRC=new global::LinqDB.CRC.CRC32();
                    CRC.Input(this.s_w_id);
                    CRC.Input(this.s_i_id);
                    return CRC.GetHashCode();
                }
                public global::System.Boolean Equals(stock other){
                    if(!this.s_w_id.Equals(other.s_w_id))return false;
                    if(!this.s_i_id.Equals(other.s_i_id))return false;
                    return true;
                }
                public override global::System.Boolean Equals(global::System.Object obj)=>this.Equals((stock)obj);
                public static global::System.Boolean operator==(stock x,stock y)=> x.Equals(y);
                public static global::System.Boolean operator!=(stock x,stock y)=>!x.Equals(y);
            }
            [global::System.Serializable]
            public struct sysdiagrams:global::System.IEquatable<sysdiagrams>{
                public global::System.Int32 diagram_id;
                public sysdiagrams(global::System.Int32 diagram_id){                    this.diagram_id=diagram_id;
                }
                internal void ToStringBuilder(global::System.Text.StringBuilder sb){
                    sb.Append(nameof(this.diagram_id),"="+this.diagram_id);
                }
                public override global::System.String ToString(){
                    var sb=new global::System.Text.StringBuilder();
                    this.ToStringBuilder(sb);
                    return sb.ToString();
                }
                internal void InputHashCode(ref global::LinqDB.CRC.CRC32 CRC){
                    CRC.Input(this.diagram_id);
                }
                public override global::System.Int32 GetHashCode()=>this.diagram_id;
                public global::System.Boolean Equals(sysdiagrams other){
                    if(!this.diagram_id.Equals(other.diagram_id))return false;
                    return true;
                }
                public override global::System.Boolean Equals(global::System.Object obj)=>this.Equals((sysdiagrams)obj);
                public static global::System.Boolean operator==(sysdiagrams x,sysdiagrams y)=> x.Equals(y);
                public static global::System.Boolean operator!=(sysdiagrams x,sysdiagrams y)=>!x.Equals(y);
            }
            [global::System.Serializable]
            public struct warehouse:global::System.IEquatable<warehouse>{
                public global::System.Int32 w_id;
                public warehouse(global::System.Int32 w_id){                    this.w_id=w_id;
                }
                internal void ToStringBuilder(global::System.Text.StringBuilder sb){
                    sb.Append(nameof(this.w_id),"="+this.w_id);
                }
                public override global::System.String ToString(){
                    var sb=new global::System.Text.StringBuilder();
                    this.ToStringBuilder(sb);
                    return sb.ToString();
                }
                internal void InputHashCode(ref global::LinqDB.CRC.CRC32 CRC){
                    CRC.Input(this.w_id);
                }
                public override global::System.Int32 GetHashCode()=>this.w_id;
                public global::System.Boolean Equals(warehouse other){
                    if(!this.w_id.Equals(other.w_id))return false;
                    return true;
                }
                public override global::System.Boolean Equals(global::System.Object obj)=>this.Equals((warehouse)obj);
                public static global::System.Boolean operator==(warehouse x,warehouse y)=> x.Equals(y);
                public static global::System.Boolean operator!=(warehouse x,warehouse y)=>!x.Equals(y);
            }
        }
        namespace {guest{
        }
        namespace {INFORMATION_SCHEMA{
        }
        namespace {sys{
        }
    }//end PrimaryKeys
    namespace BaseTables{
        namespace db_accessadmin{
        }
        namespace db_backupoperator{
        }
        namespace db_datareader{
        }
        namespace db_datawriter{
        }
        namespace db_ddladmin{
        }
        namespace db_denydatareader{
        }
        namespace db_denydatawriter{
        }
        namespace db_owner{
        }
        namespace db_securityadmin{
        }
        namespace dbo{
            [global::System.Serializable]
            public abstract class customer:global::LinqDB.Sets.AEntity<global::TPC_C.PrimaryKeys.dbo.customer,Container>,global::System.IEquatable<customer>{
                public global::System.Int32 c_w_id=>this.PrimaryKey.c_w_id;
                public global::System.Int32 c_d_id=>this.PrimaryKey.c_d_id;
                public global::System.Int32 c_id=>this.PrimaryKey.c_id;
                public readonly global::System.String c_first;
                public readonly global::System.String c_middle;
                public readonly global::System.String c_last;
                public readonly global::System.String c_street_1;
                public readonly global::System.String c_street_2;
                public readonly global::System.String c_city;
                public readonly global::System.String c_state;
                public readonly global::System.String c_zip;
                public readonly global::System.String c_phone;
                public readonly global::System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] c_since;
                public readonly global::System.String c_credit;
                public readonly global::System.Nullable`1[[System.Int64, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] c_credit_lim;
                public readonly global::System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] c_discount;
                public readonly global::System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] c_balance;
                public readonly global::System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] c_ytd_payment;
                public readonly global::System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] c_payment_cnt;
                public readonly global::System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] c_delivery_cnt;
                public readonly global::System.String c_data;
                public customer(System.Int32 c_w_id,System.Int32 c_d_id,System.Int32 c_id,System.String c_first,System.String c_middle,System.String c_last,System.String c_street_1,System.String c_street_2,System.String c_city,System.String c_state,System.String c_zip,System.String c_phone,System.Nullable`1[System.DateTime] c_since,System.String c_credit,System.Nullable`1[System.Int64] c_credit_lim,System.Nullable`1[System.Decimal] c_discount,System.Nullable`1[System.Decimal] c_balance,System.Nullable`1[System.Decimal] c_ytd_payment,System.Nullable`1[System.Int32] c_payment_cnt,System.Nullable`1[System.Int32] c_delivery_cnt,System.String c_data):base(new PrimaryKeys.dbo.customer($c_w_id,c_d_id,c_id)){
                    this.c_first=c_first;
                    this.c_middle=c_middle;
                    this.c_last=c_last;
                    this.c_street_1=c_street_1;
                    this.c_street_2=c_street_2;
                    this.c_city=c_city;
                    this.c_state=c_state;
                    this.c_zip=c_zip;
                    this.c_phone=c_phone;
                    this.c_since=c_since;
                    this.c_credit=c_credit;
                    this.c_credit_lim=c_credit_lim;
                    this.c_discount=c_discount;
                    this.c_balance=c_balance;
                    this.c_ytd_payment=c_ytd_payment;
                    this.c_payment_cnt=c_payment_cnt;
                    this.c_delivery_cnt=c_delivery_cnt;
                    this.c_data=c_data;
                }
                public global::System.Boolean Equals(customer other){
                    if(other==null)return false;
                    if(!this.PrimaryKey.Equals(other.PrimaryKey))return false;
                    if(!this.c_first.Equals(other.c_first))return false;
                    if(!this.c_middle.Equals(other.c_middle))return false;
                    if(!this.c_last.Equals(other.c_last))return false;
                    if(!this.c_street_1.Equals(other.c_street_1))return false;
                    if(!this.c_street_2.Equals(other.c_street_2))return false;
                    if(!this.c_city.Equals(other.c_city))return false;
                    if(!this.c_state.Equals(other.c_state))return false;
                    if(!this.c_zip.Equals(other.c_zip))return false;
                    if(!this.c_phone.Equals(other.c_phone))return false;
                    if(!this.c_since.Equals(other.c_since))return false;
                    if(!this.c_credit.Equals(other.c_credit))return false;
                    if(!this.c_credit_lim.Equals(other.c_credit_lim))return false;
                    if(!this.c_discount.Equals(other.c_discount))return false;
                    if(!this.c_balance.Equals(other.c_balance))return false;
                    if(!this.c_ytd_payment.Equals(other.c_ytd_payment))return false;
                    if(!this.c_payment_cnt.Equals(other.c_payment_cnt))return false;
                    if(!this.c_delivery_cnt.Equals(other.c_delivery_cnt))return false;
                    if(!this.c_data.Equals(other.c_data))return false;
                    return true;
                }
                public override global::System.Boolean Equals(global::System.Object other)=>this.Equals(other as customer);
                public static global::System.Boolean operator==(customer x,customer y)=> x.Equals(y);
                public static global::System.Boolean operator!=(customer x,customer y)=>!x.Equals(y);
                protected override void ToStringBuilder(global::System.Text.StringBuilder sb){
                    this.PrimaryKey.ToStringBuilder(sb);
                    sb.Append("+nameof(this.c_w_id)+"="+this.c_w_id);
                    sb.Append("+nameof(this.c_d_id)+"="+this.c_d_id);
                    sb.Append("+nameof(this.c_id)+"="+this.c_id);
                    sb.Append("+nameof(this.c_first)+"="+this.c_first);
                    sb.Append("+nameof(this.c_middle)+"="+this.c_middle);
                    sb.Append("+nameof(this.c_last)+"="+this.c_last);
                    sb.Append("+nameof(this.c_street_1)+"="+this.c_street_1);
                    sb.Append("+nameof(this.c_street_2)+"="+this.c_street_2);
                    sb.Append("+nameof(this.c_city)+"="+this.c_city);
                    sb.Append("+nameof(this.c_state)+"="+this.c_state);
                    sb.Append("+nameof(this.c_zip)+"="+this.c_zip);
                    sb.Append("+nameof(this.c_phone)+"="+this.c_phone);
                    sb.Append("+nameof(this.c_since)+"="+this.c_since);
                    sb.Append("+nameof(this.c_credit)+"="+this.c_credit);
                    sb.Append("+nameof(this.c_credit_lim)+"="+this.c_credit_lim);
                    sb.Append("+nameof(this.c_discount)+"="+this.c_discount);
                    sb.Append("+nameof(this.c_balance)+"="+this.c_balance);
                    sb.Append("+nameof(this.c_ytd_payment)+"="+this.c_ytd_payment);
                    sb.Append("+nameof(this.c_payment_cnt)+"="+this.c_payment_cnt);
                    sb.Append("+nameof(this.c_delivery_cnt)+"="+this.c_delivery_cnt);
                    sb.Append("+nameof(this.c_data)+"="+this.c_data);
                }
            }//end class
            [global::System.Serializable]
            public abstract class district:global::LinqDB.Sets.AEntity<global::TPC_C.PrimaryKeys.dbo.district,Container>,global::System.IEquatable<district>{
                public global::System.Int32 d_w_id=>this.PrimaryKey.d_w_id;
                public global::System.Int32 d_id=>this.PrimaryKey.d_id;
                public readonly global::System.String d_name;
                public readonly global::System.String d_street_1;
                public readonly global::System.String d_street_2;
                public readonly global::System.String d_city;
                public readonly global::System.String d_state;
                public readonly global::System.String d_zip;
                public readonly global::System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] d_tax;
                public readonly global::System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] d_ytd;
                public readonly global::System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] d_next_o_id;
                public district(System.Int32 d_w_id,System.Int32 d_id,System.String d_name,System.String d_street_1,System.String d_street_2,System.String d_city,System.String d_state,System.String d_zip,System.Nullable`1[System.Decimal] d_tax,System.Nullable`1[System.Decimal] d_ytd,System.Nullable`1[System.Int32] d_next_o_id):base(new PrimaryKeys.dbo.district($d_w_id,d_id)){
                    this.d_name=d_name;
                    this.d_street_1=d_street_1;
                    this.d_street_2=d_street_2;
                    this.d_city=d_city;
                    this.d_state=d_state;
                    this.d_zip=d_zip;
                    this.d_tax=d_tax;
                    this.d_ytd=d_ytd;
                    this.d_next_o_id=d_next_o_id;
                }
                public global::System.Boolean Equals(district other){
                    if(other==null)return false;
                    if(!this.PrimaryKey.Equals(other.PrimaryKey))return false;
                    if(!this.d_name.Equals(other.d_name))return false;
                    if(!this.d_street_1.Equals(other.d_street_1))return false;
                    if(!this.d_street_2.Equals(other.d_street_2))return false;
                    if(!this.d_city.Equals(other.d_city))return false;
                    if(!this.d_state.Equals(other.d_state))return false;
                    if(!this.d_zip.Equals(other.d_zip))return false;
                    if(!this.d_tax.Equals(other.d_tax))return false;
                    if(!this.d_ytd.Equals(other.d_ytd))return false;
                    if(!this.d_next_o_id.Equals(other.d_next_o_id))return false;
                    return true;
                }
                public override global::System.Boolean Equals(global::System.Object other)=>this.Equals(other as district);
                public static global::System.Boolean operator==(district x,district y)=> x.Equals(y);
                public static global::System.Boolean operator!=(district x,district y)=>!x.Equals(y);
                protected override void ToStringBuilder(global::System.Text.StringBuilder sb){
                    this.PrimaryKey.ToStringBuilder(sb);
                    sb.Append("+nameof(this.d_w_id)+"="+this.d_w_id);
                    sb.Append("+nameof(this.d_id)+"="+this.d_id);
                    sb.Append("+nameof(this.d_name)+"="+this.d_name);
                    sb.Append("+nameof(this.d_street_1)+"="+this.d_street_1);
                    sb.Append("+nameof(this.d_street_2)+"="+this.d_street_2);
                    sb.Append("+nameof(this.d_city)+"="+this.d_city);
                    sb.Append("+nameof(this.d_state)+"="+this.d_state);
                    sb.Append("+nameof(this.d_zip)+"="+this.d_zip);
                    sb.Append("+nameof(this.d_tax)+"="+this.d_tax);
                    sb.Append("+nameof(this.d_ytd)+"="+this.d_ytd);
                    sb.Append("+nameof(this.d_next_o_id)+"="+this.d_next_o_id);
                }
            }//end class
            [global::System.Serializable]
            public abstract class history:global::LinqDB.Sets.AEntity<global::TPC_C.PrimaryKeys.dbo.history,Container>,global::System.IEquatable<history>{
                public global::System.Int32 h_w_id=>this.PrimaryKey.h_w_id;
                public global::System.Int32 h_d_id=>this.PrimaryKey.h_d_id;
                public global::System.Int32 h_c_w_id=>this.PrimaryKey.h_c_w_id;
                public global::System.Int32 h_c_d_id=>this.PrimaryKey.h_c_d_id;
                public global::System.Int32 h_c_id=>this.PrimaryKey.h_c_id;
                public global::System.DateTime h_date=>this.PrimaryKey.h_date;
                public readonly global::System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] h_amount;
                public readonly global::System.String h_data;
                public history(System.Int32 h_w_id,System.Int32 h_d_id,System.Int32 h_c_w_id,System.Int32 h_c_d_id,System.Int32 h_c_id,System.DateTime h_date,System.Nullable`1[System.Decimal] h_amount,System.String h_data):base(new PrimaryKeys.dbo.history($h_w_id,h_d_id,h_c_w_id,h_c_d_id,h_c_id,h_date)){
                    this.h_amount=h_amount;
                    this.h_data=h_data;
                }
                public global::System.Boolean Equals(history other){
                    if(other==null)return false;
                    if(!this.PrimaryKey.Equals(other.PrimaryKey))return false;
                    if(!this.h_amount.Equals(other.h_amount))return false;
                    if(!this.h_data.Equals(other.h_data))return false;
                    return true;
                }
                public override global::System.Boolean Equals(global::System.Object other)=>this.Equals(other as history);
                public static global::System.Boolean operator==(history x,history y)=> x.Equals(y);
                public static global::System.Boolean operator!=(history x,history y)=>!x.Equals(y);
                protected override void ToStringBuilder(global::System.Text.StringBuilder sb){
                    this.PrimaryKey.ToStringBuilder(sb);
                    sb.Append("+nameof(this.h_w_id)+"="+this.h_w_id);
                    sb.Append("+nameof(this.h_d_id)+"="+this.h_d_id);
                    sb.Append("+nameof(this.h_c_w_id)+"="+this.h_c_w_id);
                    sb.Append("+nameof(this.h_c_d_id)+"="+this.h_c_d_id);
                    sb.Append("+nameof(this.h_c_id)+"="+this.h_c_id);
                    sb.Append("+nameof(this.h_date)+"="+this.h_date);
                    sb.Append("+nameof(this.h_amount)+"="+this.h_amount);
                    sb.Append("+nameof(this.h_data)+"="+this.h_data);
                }
            }//end class
            [global::System.Serializable]
            public abstract class item:global::LinqDB.Sets.AEntity<global::TPC_C.PrimaryKeys.dbo.item,Container>,global::System.IEquatable<item>{
                public global::System.Int32 i_id=>this.PrimaryKey.i_id;
                public readonly global::System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] i_im_id;
                public readonly global::System.String i_name;
                public readonly global::System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] i_price;
                public readonly global::System.String i_data;
                public item(System.Int32 i_id,System.Nullable`1[System.Int32] i_im_id,System.String i_name,System.Nullable`1[System.Decimal] i_price,System.String i_data):base(new PrimaryKeys.dbo.item($i_id)){
                    this.i_im_id=i_im_id;
                    this.i_name=i_name;
                    this.i_price=i_price;
                    this.i_data=i_data;
                }
                public global::System.Boolean Equals(item other){
                    if(other==null)return false;
                    if(!this.PrimaryKey.Equals(other.PrimaryKey))return false;
                    if(!this.i_im_id.Equals(other.i_im_id))return false;
                    if(!this.i_name.Equals(other.i_name))return false;
                    if(!this.i_price.Equals(other.i_price))return false;
                    if(!this.i_data.Equals(other.i_data))return false;
                    return true;
                }
                public override global::System.Boolean Equals(global::System.Object other)=>this.Equals(other as item);
                public static global::System.Boolean operator==(item x,item y)=> x.Equals(y);
                public static global::System.Boolean operator!=(item x,item y)=>!x.Equals(y);
                protected override void ToStringBuilder(global::System.Text.StringBuilder sb){
                    this.PrimaryKey.ToStringBuilder(sb);
                    sb.Append("+nameof(this.i_id)+"="+this.i_id);
                    sb.Append("+nameof(this.i_im_id)+"="+this.i_im_id);
                    sb.Append("+nameof(this.i_name)+"="+this.i_name);
                    sb.Append("+nameof(this.i_price)+"="+this.i_price);
                    sb.Append("+nameof(this.i_data)+"="+this.i_data);
                }
            }//end class
            [global::System.Serializable]
            public abstract class new_orders:global::LinqDB.Sets.AEntity<global::TPC_C.PrimaryKeys.dbo.new_orders,Container>,global::System.IEquatable<new_orders>{
                public global::System.Int32 no_w_id=>this.PrimaryKey.no_w_id;
                public global::System.Int32 no_d_id=>this.PrimaryKey.no_d_id;
                public global::System.Int32 no_o_id=>this.PrimaryKey.no_o_id;
                public new_orders(System.Int32 no_w_id,System.Int32 no_d_id,System.Int32 no_o_id):base(new PrimaryKeys.dbo.new_orders($no_w_id,no_d_id,no_o_id)){
                }
                public global::System.Boolean Equals(new_orders other){
                    if(other==null)return false;
                    if(!this.PrimaryKey.Equals(other.PrimaryKey))return false;
                    return true;
                }
                public override global::System.Boolean Equals(global::System.Object other)=>this.Equals(other as new_orders);
                public static global::System.Boolean operator==(new_orders x,new_orders y)=> x.Equals(y);
                public static global::System.Boolean operator!=(new_orders x,new_orders y)=>!x.Equals(y);
                protected override void ToStringBuilder(global::System.Text.StringBuilder sb){
                    this.PrimaryKey.ToStringBuilder(sb);
                    sb.Append("+nameof(this.no_w_id)+"="+this.no_w_id);
                    sb.Append("+nameof(this.no_d_id)+"="+this.no_d_id);
                    sb.Append("+nameof(this.no_o_id)+"="+this.no_o_id);
                }
            }//end class
            [global::System.Serializable]
            public abstract class order_line:global::LinqDB.Sets.AEntity<global::TPC_C.PrimaryKeys.dbo.order_line,Container>,global::System.IEquatable<order_line>{
                public global::System.Int32 ol_w_id=>this.PrimaryKey.ol_w_id;
                public global::System.Int32 ol_d_id=>this.PrimaryKey.ol_d_id;
                public global::System.Int32 ol_o_id=>this.PrimaryKey.ol_o_id;
                public global::System.Int32 ol_number=>this.PrimaryKey.ol_number;
                public readonly global::System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] ol_i_id;
                public readonly global::System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] ol_supply_w_id;
                public readonly global::System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] ol_delivery_d;
                public readonly global::System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] ol_quantity;
                public readonly global::System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] ol_amount;
                public readonly global::System.String ol_dist_info;
                public order_line(System.Int32 ol_w_id,System.Int32 ol_d_id,System.Int32 ol_o_id,System.Int32 ol_number,System.Nullable`1[System.Int32] ol_i_id,System.Nullable`1[System.Int32] ol_supply_w_id,System.Nullable`1[System.DateTime] ol_delivery_d,System.Nullable`1[System.Int32] ol_quantity,System.Nullable`1[System.Decimal] ol_amount,System.String ol_dist_info):base(new PrimaryKeys.dbo.order_line($ol_w_id,ol_d_id,ol_o_id,ol_number)){
                    this.ol_i_id=ol_i_id;
                    this.ol_supply_w_id=ol_supply_w_id;
                    this.ol_delivery_d=ol_delivery_d;
                    this.ol_quantity=ol_quantity;
                    this.ol_amount=ol_amount;
                    this.ol_dist_info=ol_dist_info;
                }
                public global::System.Boolean Equals(order_line other){
                    if(other==null)return false;
                    if(!this.PrimaryKey.Equals(other.PrimaryKey))return false;
                    if(!this.ol_i_id.Equals(other.ol_i_id))return false;
                    if(!this.ol_supply_w_id.Equals(other.ol_supply_w_id))return false;
                    if(!this.ol_delivery_d.Equals(other.ol_delivery_d))return false;
                    if(!this.ol_quantity.Equals(other.ol_quantity))return false;
                    if(!this.ol_amount.Equals(other.ol_amount))return false;
                    if(!this.ol_dist_info.Equals(other.ol_dist_info))return false;
                    return true;
                }
                public override global::System.Boolean Equals(global::System.Object other)=>this.Equals(other as order_line);
                public static global::System.Boolean operator==(order_line x,order_line y)=> x.Equals(y);
                public static global::System.Boolean operator!=(order_line x,order_line y)=>!x.Equals(y);
                protected override void ToStringBuilder(global::System.Text.StringBuilder sb){
                    this.PrimaryKey.ToStringBuilder(sb);
                    sb.Append("+nameof(this.ol_w_id)+"="+this.ol_w_id);
                    sb.Append("+nameof(this.ol_d_id)+"="+this.ol_d_id);
                    sb.Append("+nameof(this.ol_o_id)+"="+this.ol_o_id);
                    sb.Append("+nameof(this.ol_number)+"="+this.ol_number);
                    sb.Append("+nameof(this.ol_i_id)+"="+this.ol_i_id);
                    sb.Append("+nameof(this.ol_supply_w_id)+"="+this.ol_supply_w_id);
                    sb.Append("+nameof(this.ol_delivery_d)+"="+this.ol_delivery_d);
                    sb.Append("+nameof(this.ol_quantity)+"="+this.ol_quantity);
                    sb.Append("+nameof(this.ol_amount)+"="+this.ol_amount);
                    sb.Append("+nameof(this.ol_dist_info)+"="+this.ol_dist_info);
                }
            }//end class
            [global::System.Serializable]
            public abstract class orders:global::LinqDB.Sets.AEntity<global::TPC_C.PrimaryKeys.dbo.orders,Container>,global::System.IEquatable<orders>{
                public global::System.Int32 o_w_id=>this.PrimaryKey.o_w_id;
                public global::System.Int32 o_d_id=>this.PrimaryKey.o_d_id;
                public global::System.Int32 o_id=>this.PrimaryKey.o_id;
                public readonly global::System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] o_c_id;
                public readonly global::System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] o_entry_d;
                public readonly global::System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] o_carrier_id;
                public readonly global::System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] o_ol_cnt;
                public readonly global::System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] o_all_local;
                public orders(System.Int32 o_w_id,System.Int32 o_d_id,System.Int32 o_id,System.Nullable`1[System.Int32] o_c_id,System.Nullable`1[System.DateTime] o_entry_d,System.Nullable`1[System.Int32] o_carrier_id,System.Nullable`1[System.Int32] o_ol_cnt,System.Nullable`1[System.Int32] o_all_local):base(new PrimaryKeys.dbo.orders($o_w_id,o_d_id,o_id)){
                    this.o_c_id=o_c_id;
                    this.o_entry_d=o_entry_d;
                    this.o_carrier_id=o_carrier_id;
                    this.o_ol_cnt=o_ol_cnt;
                    this.o_all_local=o_all_local;
                }
                public global::System.Boolean Equals(orders other){
                    if(other==null)return false;
                    if(!this.PrimaryKey.Equals(other.PrimaryKey))return false;
                    if(!this.o_c_id.Equals(other.o_c_id))return false;
                    if(!this.o_entry_d.Equals(other.o_entry_d))return false;
                    if(!this.o_carrier_id.Equals(other.o_carrier_id))return false;
                    if(!this.o_ol_cnt.Equals(other.o_ol_cnt))return false;
                    if(!this.o_all_local.Equals(other.o_all_local))return false;
                    return true;
                }
                public override global::System.Boolean Equals(global::System.Object other)=>this.Equals(other as orders);
                public static global::System.Boolean operator==(orders x,orders y)=> x.Equals(y);
                public static global::System.Boolean operator!=(orders x,orders y)=>!x.Equals(y);
                protected override void ToStringBuilder(global::System.Text.StringBuilder sb){
                    this.PrimaryKey.ToStringBuilder(sb);
                    sb.Append("+nameof(this.o_w_id)+"="+this.o_w_id);
                    sb.Append("+nameof(this.o_d_id)+"="+this.o_d_id);
                    sb.Append("+nameof(this.o_id)+"="+this.o_id);
                    sb.Append("+nameof(this.o_c_id)+"="+this.o_c_id);
                    sb.Append("+nameof(this.o_entry_d)+"="+this.o_entry_d);
                    sb.Append("+nameof(this.o_carrier_id)+"="+this.o_carrier_id);
                    sb.Append("+nameof(this.o_ol_cnt)+"="+this.o_ol_cnt);
                    sb.Append("+nameof(this.o_all_local)+"="+this.o_all_local);
                }
            }//end class
            [global::System.Serializable]
            public abstract class stock:global::LinqDB.Sets.AEntity<global::TPC_C.PrimaryKeys.dbo.stock,Container>,global::System.IEquatable<stock>{
                public global::System.Int32 s_w_id=>this.PrimaryKey.s_w_id;
                public global::System.Int32 s_i_id=>this.PrimaryKey.s_i_id;
                public readonly global::System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] s_quantity;
                public readonly global::System.String s_dist_01;
                public readonly global::System.String s_dist_02;
                public readonly global::System.String s_dist_03;
                public readonly global::System.String s_dist_04;
                public readonly global::System.String s_dist_05;
                public readonly global::System.String s_dist_06;
                public readonly global::System.String s_dist_07;
                public readonly global::System.String s_dist_08;
                public readonly global::System.String s_dist_09;
                public readonly global::System.String s_dist_10;
                public readonly global::System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] s_ytd;
                public readonly global::System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] s_order_cnt;
                public readonly global::System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] s_remote_cnt;
                public readonly global::System.String s_data;
                public stock(System.Int32 s_w_id,System.Int32 s_i_id,System.Nullable`1[System.Int32] s_quantity,System.String s_dist_01,System.String s_dist_02,System.String s_dist_03,System.String s_dist_04,System.String s_dist_05,System.String s_dist_06,System.String s_dist_07,System.String s_dist_08,System.String s_dist_09,System.String s_dist_10,System.Nullable`1[System.Decimal] s_ytd,System.Nullable`1[System.Int32] s_order_cnt,System.Nullable`1[System.Int32] s_remote_cnt,System.String s_data):base(new PrimaryKeys.dbo.stock($s_w_id,s_i_id)){
                    this.s_quantity=s_quantity;
                    this.s_dist_01=s_dist_01;
                    this.s_dist_02=s_dist_02;
                    this.s_dist_03=s_dist_03;
                    this.s_dist_04=s_dist_04;
                    this.s_dist_05=s_dist_05;
                    this.s_dist_06=s_dist_06;
                    this.s_dist_07=s_dist_07;
                    this.s_dist_08=s_dist_08;
                    this.s_dist_09=s_dist_09;
                    this.s_dist_10=s_dist_10;
                    this.s_ytd=s_ytd;
                    this.s_order_cnt=s_order_cnt;
                    this.s_remote_cnt=s_remote_cnt;
                    this.s_data=s_data;
                }
                public global::System.Boolean Equals(stock other){
                    if(other==null)return false;
                    if(!this.PrimaryKey.Equals(other.PrimaryKey))return false;
                    if(!this.s_quantity.Equals(other.s_quantity))return false;
                    if(!this.s_dist_01.Equals(other.s_dist_01))return false;
                    if(!this.s_dist_02.Equals(other.s_dist_02))return false;
                    if(!this.s_dist_03.Equals(other.s_dist_03))return false;
                    if(!this.s_dist_04.Equals(other.s_dist_04))return false;
                    if(!this.s_dist_05.Equals(other.s_dist_05))return false;
                    if(!this.s_dist_06.Equals(other.s_dist_06))return false;
                    if(!this.s_dist_07.Equals(other.s_dist_07))return false;
                    if(!this.s_dist_08.Equals(other.s_dist_08))return false;
                    if(!this.s_dist_09.Equals(other.s_dist_09))return false;
                    if(!this.s_dist_10.Equals(other.s_dist_10))return false;
                    if(!this.s_ytd.Equals(other.s_ytd))return false;
                    if(!this.s_order_cnt.Equals(other.s_order_cnt))return false;
                    if(!this.s_remote_cnt.Equals(other.s_remote_cnt))return false;
                    if(!this.s_data.Equals(other.s_data))return false;
                    return true;
                }
                public override global::System.Boolean Equals(global::System.Object other)=>this.Equals(other as stock);
                public static global::System.Boolean operator==(stock x,stock y)=> x.Equals(y);
                public static global::System.Boolean operator!=(stock x,stock y)=>!x.Equals(y);
                protected override void ToStringBuilder(global::System.Text.StringBuilder sb){
                    this.PrimaryKey.ToStringBuilder(sb);
                    sb.Append("+nameof(this.s_w_id)+"="+this.s_w_id);
                    sb.Append("+nameof(this.s_i_id)+"="+this.s_i_id);
                    sb.Append("+nameof(this.s_quantity)+"="+this.s_quantity);
                    sb.Append("+nameof(this.s_dist_01)+"="+this.s_dist_01);
                    sb.Append("+nameof(this.s_dist_02)+"="+this.s_dist_02);
                    sb.Append("+nameof(this.s_dist_03)+"="+this.s_dist_03);
                    sb.Append("+nameof(this.s_dist_04)+"="+this.s_dist_04);
                    sb.Append("+nameof(this.s_dist_05)+"="+this.s_dist_05);
                    sb.Append("+nameof(this.s_dist_06)+"="+this.s_dist_06);
                    sb.Append("+nameof(this.s_dist_07)+"="+this.s_dist_07);
                    sb.Append("+nameof(this.s_dist_08)+"="+this.s_dist_08);
                    sb.Append("+nameof(this.s_dist_09)+"="+this.s_dist_09);
                    sb.Append("+nameof(this.s_dist_10)+"="+this.s_dist_10);
                    sb.Append("+nameof(this.s_ytd)+"="+this.s_ytd);
                    sb.Append("+nameof(this.s_order_cnt)+"="+this.s_order_cnt);
                    sb.Append("+nameof(this.s_remote_cnt)+"="+this.s_remote_cnt);
                    sb.Append("+nameof(this.s_data)+"="+this.s_data);
                }
            }//end class
            [global::System.Serializable]
            public abstract class sysdiagrams:global::LinqDB.Sets.AEntity<global::TPC_C.PrimaryKeys.dbo.sysdiagrams,Container>,global::System.IEquatable<sysdiagrams>{
                public readonly global::System.String name;
                public readonly global::System.Int32 principal_id;
                public global::System.Int32 diagram_id=>this.PrimaryKey.diagram_id;
                public readonly global::System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] version;
                public readonly global::System.Byte[] definition;
                public sysdiagrams(System.String name,System.Int32 principal_id,System.Int32 diagram_id,System.Nullable`1[System.Int32] version,System.Byte[] definition):base(new PrimaryKeys.dbo.sysdiagrams($diagram_id)){
                    this.name=name;
                    this.principal_id=principal_id;
                    this.version=version;
                    this.definition=definition;
                }
                public global::System.Boolean Equals(sysdiagrams other){
                    if(other==null)return false;
                    if(!this.PrimaryKey.Equals(other.PrimaryKey))return false;
                    if(!this.name.Equals(other.name))return false;
                    if(!this.principal_id.Equals(other.principal_id))return false;
                    if(!this.version.Equals(other.version))return false;
                    if(!this.definition.Equals(other.definition))return false;
                    return true;
                }
                public override global::System.Boolean Equals(global::System.Object other)=>this.Equals(other as sysdiagrams);
                public static global::System.Boolean operator==(sysdiagrams x,sysdiagrams y)=> x.Equals(y);
                public static global::System.Boolean operator!=(sysdiagrams x,sysdiagrams y)=>!x.Equals(y);
                protected override void ToStringBuilder(global::System.Text.StringBuilder sb){
                    this.PrimaryKey.ToStringBuilder(sb);
                    sb.Append("+nameof(this.name)+"="+this.name);
                    sb.Append("+nameof(this.principal_id)+"="+this.principal_id);
                    sb.Append("+nameof(this.diagram_id)+"="+this.diagram_id);
                    sb.Append("+nameof(this.version)+"="+this.version);
                    sb.Append("+nameof(this.definition)+"="+this.definition);
                }
            }//end class
            [global::System.Serializable]
            public abstract class warehouse:global::LinqDB.Sets.AEntity<global::TPC_C.PrimaryKeys.dbo.warehouse,Container>,global::System.IEquatable<warehouse>{
                public global::System.Int32 w_id=>this.PrimaryKey.w_id;
                public readonly global::System.String w_name;
                public readonly global::System.String w_street_1;
                public readonly global::System.String w_street_2;
                public readonly global::System.String w_city;
                public readonly global::System.String w_state;
                public readonly global::System.String w_zip;
                public readonly global::System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] w_tax;
                public readonly global::System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] w_ytd;
                public warehouse(System.Int32 w_id,System.String w_name,System.String w_street_1,System.String w_street_2,System.String w_city,System.String w_state,System.String w_zip,System.Nullable`1[System.Decimal] w_tax,System.Nullable`1[System.Decimal] w_ytd):base(new PrimaryKeys.dbo.warehouse($w_id)){
                    this.w_name=w_name;
                    this.w_street_1=w_street_1;
                    this.w_street_2=w_street_2;
                    this.w_city=w_city;
                    this.w_state=w_state;
                    this.w_zip=w_zip;
                    this.w_tax=w_tax;
                    this.w_ytd=w_ytd;
                }
                public global::System.Boolean Equals(warehouse other){
                    if(other==null)return false;
                    if(!this.PrimaryKey.Equals(other.PrimaryKey))return false;
                    if(!this.w_name.Equals(other.w_name))return false;
                    if(!this.w_street_1.Equals(other.w_street_1))return false;
                    if(!this.w_street_2.Equals(other.w_street_2))return false;
                    if(!this.w_city.Equals(other.w_city))return false;
                    if(!this.w_state.Equals(other.w_state))return false;
                    if(!this.w_zip.Equals(other.w_zip))return false;
                    if(!this.w_tax.Equals(other.w_tax))return false;
                    if(!this.w_ytd.Equals(other.w_ytd))return false;
                    return true;
                }
                public override global::System.Boolean Equals(global::System.Object other)=>this.Equals(other as warehouse);
                public static global::System.Boolean operator==(warehouse x,warehouse y)=> x.Equals(y);
                public static global::System.Boolean operator!=(warehouse x,warehouse y)=>!x.Equals(y);
                protected override void ToStringBuilder(global::System.Text.StringBuilder sb){
                    this.PrimaryKey.ToStringBuilder(sb);
                    sb.Append("+nameof(this.w_id)+"="+this.w_id);
                    sb.Append("+nameof(this.w_name)+"="+this.w_name);
                    sb.Append("+nameof(this.w_street_1)+"="+this.w_street_1);
                    sb.Append("+nameof(this.w_street_2)+"="+this.w_street_2);
                    sb.Append("+nameof(this.w_city)+"="+this.w_city);
                    sb.Append("+nameof(this.w_state)+"="+this.w_state);
                    sb.Append("+nameof(this.w_zip)+"="+this.w_zip);
                    sb.Append("+nameof(this.w_tax)+"="+this.w_tax);
                    sb.Append("+nameof(this.w_ytd)+"="+this.w_ytd);
                }
            }//end class
        }
        namespace guest{
        }
        namespace INFORMATION_SCHEMA{
        }
        namespace sys{
        }
    }//BaseTables
    namespace Tables{
        namespace {db_accessadmin{
        }
        namespace {db_backupoperator{
        }
        namespace {db_datareader{
        }
        namespace {db_datawriter{
        }
        namespace {db_ddladmin{
        }
        namespace {db_denydatareader{
        }
        namespace {db_denydatawriter{
        }
        namespace {db_owner{
        }
        namespace {db_securityadmin{
        }
        namespace {dbo{
            [global::System.Serializable]
            public sealed class customer:global::TPC_C.BaseTables.dbo.customer{
                public customer(System.Int32 c_w_id,System.Int32 c_d_id,System.Int32 c_id,System.String c_first,System.String c_middle,System.String c_last,System.String c_street_1,System.String c_street_2,System.String c_city,System.String c_state,System.String c_zip,System.String c_phone,System.Nullable`1[System.DateTime] c_since,System.String c_credit,System.Nullable`1[System.Int64] c_credit_lim,System.Nullable`1[System.Decimal] c_discount,System.Nullable`1[System.Decimal] c_balance,System.Nullable`1[System.Decimal] c_ytd_payment,System.Nullable`1[System.Int32] c_payment_cnt,System.Nullable`1[System.Int32] c_delivery_cnt,System.String c_data):base($c_w_id,c_d_id,c_id,c_first,c_middle,c_last,c_street_1,c_street_2,c_city,c_state,c_zip,c_phone,c_since,c_credit,c_credit_lim,c_discount,c_balance,c_ytd_payment,c_payment_cnt,c_delivery_cnt,c_data){}
                protected override void AddRelationship(Container Container){
                    (this.FK_customer_district=Container.dbo.district.GetReference(new PrimaryKeys.dbo.district(this.c_w_id,this.c_d_id))).FK_customer_district.VoidAdd(this);
                }
                protected override void RemoveRelationship(){
                    this.FK_customer_district.FK_customer_district.Remove(this);
                }
                //1→*
                [global::System.NonSerialized]
                internal readonly global::LinqDB.Sets.Set<dbo.history>FK_history_customer=new global::LinqDB.Sets.Set<dbo.history>();
                //1→*
                [global::System.NonSerialized]
                internal readonly global::LinqDB.Sets.Set<dbo.orders>FK_orders_customer=new global::LinqDB.Sets.Set<dbo.orders>();
                //*→1
                [global::System.NonSerialized]
                internal dbo.district FK_customer_district=default;
            }
            [global::System.Serializable]
            public sealed class district:global::TPC_C.BaseTables.dbo.district{
                public district(System.Int32 d_w_id,System.Int32 d_id,System.String d_name,System.String d_street_1,System.String d_street_2,System.String d_city,System.String d_state,System.String d_zip,System.Nullable`1[System.Decimal] d_tax,System.Nullable`1[System.Decimal] d_ytd,System.Nullable`1[System.Int32] d_next_o_id):base($d_w_id,d_id,d_name,d_street_1,d_street_2,d_city,d_state,d_zip,d_tax,d_ytd,d_next_o_id){}
                protected override void AddRelationship(Container Container){
                    (this.FK_district_warehouse=Container.dbo.warehouse.GetReference(new PrimaryKeys.dbo.warehouse(this.d_w_id))).FK_district_warehouse.VoidAdd(this);
                }
                protected override void RemoveRelationship(){
                    this.FK_district_warehouse.FK_district_warehouse.Remove(this);
                }
                //1→*
                [global::System.NonSerialized]
                internal readonly global::LinqDB.Sets.Set<dbo.customer>FK_customer_district=new global::LinqDB.Sets.Set<dbo.customer>();
                //1→*
                [global::System.NonSerialized]
                internal readonly global::LinqDB.Sets.Set<dbo.history>FK_history_district=new global::LinqDB.Sets.Set<dbo.history>();
                //*→1
                [global::System.NonSerialized]
                internal dbo.warehouse FK_district_warehouse=default;
            }
            [global::System.Serializable]
            public sealed class history:global::TPC_C.BaseTables.dbo.history{
                public history(System.Int32 h_w_id,System.Int32 h_d_id,System.Int32 h_c_w_id,System.Int32 h_c_d_id,System.Int32 h_c_id,System.DateTime h_date,System.Nullable`1[System.Decimal] h_amount,System.String h_data):base($h_w_id,h_d_id,h_c_w_id,h_c_d_id,h_c_id,h_date,h_amount,h_data){}
                protected override void AddRelationship(Container Container){
                    (this.FK_history_customer=Container.dbo.customer.GetReference(new PrimaryKeys.dbo.customer(this.h_c_w_id,this.h_c_d_id,this.h_c_id))).FK_history_customer.VoidAdd(this);
                    (this.FK_history_district=Container.dbo.district.GetReference(new PrimaryKeys.dbo.district(this.h_w_id,this.h_d_id))).FK_history_district.VoidAdd(this);
                }
                protected override void RemoveRelationship(){
                    this.FK_history_customer.FK_history_customer.Remove(this);
                    this.FK_history_district.FK_history_district.Remove(this);
                }
                //*→1
                [global::System.NonSerialized]
                internal dbo.customer FK_history_customer=default;
                //*→1
                [global::System.NonSerialized]
                internal dbo.district FK_history_district=default;
            }
            [global::System.Serializable]
            public sealed class item:global::TPC_C.BaseTables.dbo.item{
                public item(System.Int32 i_id,System.Nullable`1[System.Int32] i_im_id,System.String i_name,System.Nullable`1[System.Decimal] i_price,System.String i_data):base($i_id,i_im_id,i_name,i_price,i_data){}
                protected override void AddRelationship(Container Container){}
                protected override void RemoveRelationship(){}
                //1→*
                [global::System.NonSerialized]
                internal readonly global::LinqDB.Sets.Set<dbo.stock>FK_stock_item=new global::LinqDB.Sets.Set<dbo.stock>();
            }
            [global::System.Serializable]
            public sealed class new_orders:global::TPC_C.BaseTables.dbo.new_orders{
                public new_orders(System.Int32 no_w_id,System.Int32 no_d_id,System.Int32 no_o_id):base($no_w_id,no_d_id,no_o_id){}
                protected override void AddRelationship(Container Container){
                    (this.FK_new_orders_orders=Container.dbo.orders.GetReference(new PrimaryKeys.dbo.orders(this.no_w_id,this.no_d_id,this.no_o_id))).FK_new_orders_orders.VoidAdd(this);
                }
                protected override void RemoveRelationship(){
                    this.FK_new_orders_orders.FK_new_orders_orders.Remove(this);
                }
                //*→1
                [global::System.NonSerialized]
                internal dbo.orders FK_new_orders_orders=default;
            }
            [global::System.Serializable]
            public sealed class order_line:global::TPC_C.BaseTables.dbo.order_line{
                public order_line(System.Int32 ol_w_id,System.Int32 ol_d_id,System.Int32 ol_o_id,System.Int32 ol_number,System.Nullable`1[System.Int32] ol_i_id,System.Nullable`1[System.Int32] ol_supply_w_id,System.Nullable`1[System.DateTime] ol_delivery_d,System.Nullable`1[System.Int32] ol_quantity,System.Nullable`1[System.Decimal] ol_amount,System.String ol_dist_info):base($ol_w_id,ol_d_id,ol_o_id,ol_number,ol_i_id,ol_supply_w_id,ol_delivery_d,ol_quantity,ol_amount,ol_dist_info){}
                protected override void AddRelationship(Container Container){
                    (this.FK_order_line_orders=Container.dbo.orders.GetReference(new PrimaryKeys.dbo.orders(this.ol_w_id,this.ol_d_id,this.ol_o_id))).FK_order_line_orders.VoidAdd(this);
                    (this.FK_order_line_stock=Container.dbo.stock.GetReference(new PrimaryKeys.dbo.stock(this.ol_w_id,this.ol_i_id))).FK_order_line_stock.VoidAdd(this);
                }
                protected override void RemoveRelationship(){
                    this.FK_order_line_orders.FK_order_line_orders.Remove(this);
                    this.FK_order_line_stock.FK_order_line_stock.Remove(this);
                }
                //*→1
                [global::System.NonSerialized]
                internal dbo.orders FK_order_line_orders=default;
                //*→1
                [global::System.NonSerialized]
                internal dbo.stock FK_order_line_stock=default;
            }
            [global::System.Serializable]
            public sealed class orders:global::TPC_C.BaseTables.dbo.orders{
                public orders(System.Int32 o_w_id,System.Int32 o_d_id,System.Int32 o_id,System.Nullable`1[System.Int32] o_c_id,System.Nullable`1[System.DateTime] o_entry_d,System.Nullable`1[System.Int32] o_carrier_id,System.Nullable`1[System.Int32] o_ol_cnt,System.Nullable`1[System.Int32] o_all_local):base($o_w_id,o_d_id,o_id,o_c_id,o_entry_d,o_carrier_id,o_ol_cnt,o_all_local){}
                protected override void AddRelationship(Container Container){
                    (this.FK_orders_customer=Container.dbo.customer.GetReference(new PrimaryKeys.dbo.customer(this.o_w_id,this.o_d_id,this.o_c_id))).FK_orders_customer.VoidAdd(this);
                }
                protected override void RemoveRelationship(){
                    this.FK_orders_customer.FK_orders_customer.Remove(this);
                }
                //1→*
                [global::System.NonSerialized]
                internal readonly global::LinqDB.Sets.Set<dbo.new_orders>FK_new_orders_orders=new global::LinqDB.Sets.Set<dbo.new_orders>();
                //1→*
                [global::System.NonSerialized]
                internal readonly global::LinqDB.Sets.Set<dbo.order_line>FK_order_line_orders=new global::LinqDB.Sets.Set<dbo.order_line>();
                //*→1
                [global::System.NonSerialized]
                internal dbo.customer FK_orders_customer=default;
            }
            [global::System.Serializable]
            public sealed class stock:global::TPC_C.BaseTables.dbo.stock{
                public stock(System.Int32 s_w_id,System.Int32 s_i_id,System.Nullable`1[System.Int32] s_quantity,System.String s_dist_01,System.String s_dist_02,System.String s_dist_03,System.String s_dist_04,System.String s_dist_05,System.String s_dist_06,System.String s_dist_07,System.String s_dist_08,System.String s_dist_09,System.String s_dist_10,System.Nullable`1[System.Decimal] s_ytd,System.Nullable`1[System.Int32] s_order_cnt,System.Nullable`1[System.Int32] s_remote_cnt,System.String s_data):base($s_w_id,s_i_id,s_quantity,s_dist_01,s_dist_02,s_dist_03,s_dist_04,s_dist_05,s_dist_06,s_dist_07,s_dist_08,s_dist_09,s_dist_10,s_ytd,s_order_cnt,s_remote_cnt,s_data){}
                protected override void AddRelationship(Container Container){
                    (this.FK_stock_item=Container.dbo.item.GetReference(new PrimaryKeys.dbo.item(this.s_i_id))).FK_stock_item.VoidAdd(this);
                    (this.FK_stock_warehouse=Container.dbo.warehouse.GetReference(new PrimaryKeys.dbo.warehouse(this.s_w_id))).FK_stock_warehouse.VoidAdd(this);
                }
                protected override void RemoveRelationship(){
                    this.FK_stock_item.FK_stock_item.Remove(this);
                    this.FK_stock_warehouse.FK_stock_warehouse.Remove(this);
                }
                //1→*
                [global::System.NonSerialized]
                internal readonly global::LinqDB.Sets.Set<dbo.order_line>FK_order_line_stock=new global::LinqDB.Sets.Set<dbo.order_line>();
                //*→1
                [global::System.NonSerialized]
                internal dbo.item FK_stock_item=default;
                //*→1
                [global::System.NonSerialized]
                internal dbo.warehouse FK_stock_warehouse=default;
            }
            [global::System.Serializable]
            public sealed class sysdiagrams:global::TPC_C.BaseTables.dbo.sysdiagrams{
                public sysdiagrams(System.String name,System.Int32 principal_id,System.Int32 diagram_id,System.Nullable`1[System.Int32] version,System.Byte[] definition):base($name,principal_id,diagram_id,version,definition){}
                protected override void AddRelationship(Container Container){}
                protected override void RemoveRelationship(){}
            }
            [global::System.Serializable]
            public sealed class warehouse:global::TPC_C.BaseTables.dbo.warehouse{
                public warehouse(System.Int32 w_id,System.String w_name,System.String w_street_1,System.String w_street_2,System.String w_city,System.String w_state,System.String w_zip,System.Nullable`1[System.Decimal] w_tax,System.Nullable`1[System.Decimal] w_ytd):base($w_id,w_name,w_street_1,w_street_2,w_city,w_state,w_zip,w_tax,w_ytd){}
                protected override void AddRelationship(Container Container){}
                protected override void RemoveRelationship(){}
                //1→*
                [global::System.NonSerialized]
                internal readonly global::LinqDB.Sets.Set<dbo.district>FK_district_warehouse=new global::LinqDB.Sets.Set<dbo.district>();
                //1→*
                [global::System.NonSerialized]
                internal readonly global::LinqDB.Sets.Set<dbo.stock>FK_stock_warehouse=new global::LinqDB.Sets.Set<dbo.stock>();
            }
        }
        namespace {guest{
        }
        namespace {INFORMATION_SCHEMA{
        }
        namespace {sys{
        }
    }//end Tables
    namespace Schemas{
        [global::System.Serializable]
        public sealed class db_accessadmin{
            public db_accessadmin(Container Container){
            }
            internal void Read(global::System.Xml.XmlDictionaryReader Reader){
            }
            internal void Write(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void Assign(db_accessadmin To){
            }
            internal void Commit(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void UpdateRelationship(){
            }
        }
        [global::System.Serializable]
        public sealed class db_backupoperator{
            public db_backupoperator(Container Container){
            }
            internal void Read(global::System.Xml.XmlDictionaryReader Reader){
            }
            internal void Write(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void Assign(db_backupoperator To){
            }
            internal void Commit(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void UpdateRelationship(){
            }
        }
        [global::System.Serializable]
        public sealed class db_datareader{
            public db_datareader(Container Container){
            }
            internal void Read(global::System.Xml.XmlDictionaryReader Reader){
            }
            internal void Write(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void Assign(db_datareader To){
            }
            internal void Commit(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void UpdateRelationship(){
            }
        }
        [global::System.Serializable]
        public sealed class db_datawriter{
            public db_datawriter(Container Container){
            }
            internal void Read(global::System.Xml.XmlDictionaryReader Reader){
            }
            internal void Write(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void Assign(db_datawriter To){
            }
            internal void Commit(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void UpdateRelationship(){
            }
        }
        [global::System.Serializable]
        public sealed class db_ddladmin{
            public db_ddladmin(Container Container){
            }
            internal void Read(global::System.Xml.XmlDictionaryReader Reader){
            }
            internal void Write(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void Assign(db_ddladmin To){
            }
            internal void Commit(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void UpdateRelationship(){
            }
        }
        [global::System.Serializable]
        public sealed class db_denydatareader{
            public db_denydatareader(Container Container){
            }
            internal void Read(global::System.Xml.XmlDictionaryReader Reader){
            }
            internal void Write(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void Assign(db_denydatareader To){
            }
            internal void Commit(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void UpdateRelationship(){
            }
        }
        [global::System.Serializable]
        public sealed class db_denydatawriter{
            public db_denydatawriter(Container Container){
            }
            internal void Read(global::System.Xml.XmlDictionaryReader Reader){
            }
            internal void Write(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void Assign(db_denydatawriter To){
            }
            internal void Commit(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void UpdateRelationship(){
            }
        }
        [global::System.Serializable]
        public sealed class db_owner{
            public db_owner(Container Container){
            }
            internal void Read(global::System.Xml.XmlDictionaryReader Reader){
            }
            internal void Write(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void Assign(db_owner To){
            }
            internal void Commit(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void UpdateRelationship(){
            }
        }
        [global::System.Serializable]
        public sealed class db_securityadmin{
            public db_securityadmin(Container Container){
            }
            internal void Read(global::System.Xml.XmlDictionaryReader Reader){
            }
            internal void Write(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void Assign(db_securityadmin To){
            }
            internal void Commit(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void UpdateRelationship(){
            }
        }
        [global::System.Serializable]
        public sealed class dbo{
            public global::LinqDB.Sets.Set<Tables.dbo.customer,PrimaryKeys.dbo.customer,Container>customer{get;}
            public global::LinqDB.Sets.Set<Tables.dbo.district,PrimaryKeys.dbo.district,Container>district{get;}
            public global::LinqDB.Sets.Set<Tables.dbo.history,PrimaryKeys.dbo.history,Container>history{get;}
            public global::LinqDB.Sets.Set<Tables.dbo.item,PrimaryKeys.dbo.item,Container>item{get;}
            public global::LinqDB.Sets.Set<Tables.dbo.new_orders,PrimaryKeys.dbo.new_orders,Container>new_orders{get;}
            public global::LinqDB.Sets.Set<Tables.dbo.order_line,PrimaryKeys.dbo.order_line,Container>order_line{get;}
            public global::LinqDB.Sets.Set<Tables.dbo.orders,PrimaryKeys.dbo.orders,Container>orders{get;}
            public global::LinqDB.Sets.Set<Tables.dbo.stock,PrimaryKeys.dbo.stock,Container>stock{get;}
            public global::LinqDB.Sets.Set<Tables.dbo.sysdiagrams,PrimaryKeys.dbo.sysdiagrams,Container>sysdiagrams{get;}
            public global::LinqDB.Sets.Set<Tables.dbo.warehouse,PrimaryKeys.dbo.warehouse,Container>warehouse{get;}
            public dbo(Container Container){
                this.customer=new global::LinqDB.Sets.Set<Tables.dbo.customer,PrimaryKeys.dbo.customer,Container>(Container);
                this.district=new global::LinqDB.Sets.Set<Tables.dbo.district,PrimaryKeys.dbo.district,Container>(Container);
                this.history=new global::LinqDB.Sets.Set<Tables.dbo.history,PrimaryKeys.dbo.history,Container>(Container);
                this.item=new global::LinqDB.Sets.Set<Tables.dbo.item,PrimaryKeys.dbo.item,Container>(Container);
                this.new_orders=new global::LinqDB.Sets.Set<Tables.dbo.new_orders,PrimaryKeys.dbo.new_orders,Container>(Container);
                this.order_line=new global::LinqDB.Sets.Set<Tables.dbo.order_line,PrimaryKeys.dbo.order_line,Container>(Container);
                this.orders=new global::LinqDB.Sets.Set<Tables.dbo.orders,PrimaryKeys.dbo.orders,Container>(Container);
                this.stock=new global::LinqDB.Sets.Set<Tables.dbo.stock,PrimaryKeys.dbo.stock,Container>(Container);
                this.sysdiagrams=new global::LinqDB.Sets.Set<Tables.dbo.sysdiagrams,PrimaryKeys.dbo.sysdiagrams,Container>(Container);
                this.warehouse=new global::LinqDB.Sets.Set<Tables.dbo.warehouse,PrimaryKeys.dbo.warehouse,Container>(Container);
            }
            internal void Read(global::System.Xml.XmlDictionaryReader Reader){
                this.customer.Read(Reader);
                this.district.Read(Reader);
                this.history.Read(Reader);
                this.item.Read(Reader);
                this.new_orders.Read(Reader);
                this.order_line.Read(Reader);
                this.orders.Read(Reader);
                this.stock.Read(Reader);
                this.sysdiagrams.Read(Reader);
                this.warehouse.Read(Reader);
            }
            internal void Write(global::System.Xml.XmlDictionaryWriter Writer){
                this.customer.Write           (Writer);
                this.district.Write           (Writer);
                this.history.Write           (Writer);
                this.item.Write           (Writer);
                this.new_orders.Write           (Writer);
                this.order_line.Write           (Writer);
                this.orders.Write           (Writer);
                this.stock.Write           (Writer);
                this.sysdiagrams.Write           (Writer);
                this.warehouse.Write           (Writer);
            }
            internal void Assign(dbo To){
                To.customer.Assign(this.customer);
                To.district.Assign(this.district);
                To.history.Assign(this.history);
                To.item.Assign(this.item);
                To.new_orders.Assign(this.new_orders);
                To.order_line.Assign(this.order_line);
                To.orders.Assign(this.orders);
                To.stock.Assign(this.stock);
                To.sysdiagrams.Assign(this.sysdiagrams);
                To.warehouse.Assign(this.warehouse);
            }
            internal void Commit(global::System.Xml.XmlDictionaryWriter Writer){
                this.customer.Write           (Writer);
                this.district.Write           (Writer);
                this.history.Write           (Writer);
                this.item.Write           (Writer);
                this.new_orders.Write           (Writer);
                this.order_line.Write           (Writer);
                this.orders.Write           (Writer);
                this.stock.Write           (Writer);
                this.sysdiagrams.Write           (Writer);
                this.warehouse.Write           (Writer);
            }
            internal void UpdateRelationship(){
                this.customer.UpdateRelationship();
                this.district.UpdateRelationship();
                this.history.UpdateRelationship();
                this.item.UpdateRelationship();
                this.new_orders.UpdateRelationship();
                this.order_line.UpdateRelationship();
                this.orders.UpdateRelationship();
                this.stock.UpdateRelationship();
                this.sysdiagrams.UpdateRelationship();
                this.warehouse.UpdateRelationship();
            }
        }
        [global::System.Serializable]
        public sealed class guest{
            public guest(Container Container){
            }
            internal void Read(global::System.Xml.XmlDictionaryReader Reader){
            }
            internal void Write(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void Assign(guest To){
            }
            internal void Commit(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void UpdateRelationship(){
            }
        }
        [global::System.Serializable]
        public sealed class INFORMATION_SCHEMA{
            public INFORMATION_SCHEMA(Container Container){
            }
            internal void Read(global::System.Xml.XmlDictionaryReader Reader){
            }
            internal void Write(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void Assign(INFORMATION_SCHEMA To){
            }
            internal void Commit(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void UpdateRelationship(){
            }
        }
        [global::System.Serializable]
        public sealed class sys{
            public sys(Container Container){
            }
            internal void Read(global::System.Xml.XmlDictionaryReader Reader){
            }
            internal void Write(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void Assign(sys To){
            }
            internal void Commit(global::System.Xml.XmlDictionaryWriter Writer){
            }
            internal void UpdateRelationship(){
            }
        }
    }//end Schemas
    [global::System.Serializable]
    public sealed class Container:global::LinqDB.Databases.Container<Container>{
        public Schemas.db_accessadmin db_accessadmin{get;private set;}
        public Schemas.db_backupoperator db_backupoperator{get;private set;}
        public Schemas.db_datareader db_datareader{get;private set;}
        public Schemas.db_datawriter db_datawriter{get;private set;}
        public Schemas.db_ddladmin db_ddladmin{get;private set;}
        public Schemas.db_denydatareader db_denydatareader{get;private set;}
        public Schemas.db_denydatawriter db_denydatawriter{get;private set;}
        public Schemas.db_owner db_owner{get;private set;}
        public Schemas.db_securityadmin db_securityadmin{get;private set;}
        public Schemas.dbo dbo{get;private set;}
        public Schemas.guest guest{get;private set;}
        public Schemas.INFORMATION_SCHEMA INFORMATION_SCHEMA{get;private set;}
        public Schemas.sys sys{get;private set;}
        public Container():base(default)=>this.Init();
        public Container(Container Parent):base(Parent)=>this.Init();
        public Container(global::System.Xml.XmlDictionaryReader Reader,global::System.Xml.XmlDictionaryWriter Writer):base(Reader,Writer){}
        protected override void Init(){
            this.db_accessadmin=new Schemas.db_accessadmin(this);
            this.db_backupoperator=new Schemas.db_backupoperator(this);
            this.db_datareader=new Schemas.db_datareader(this);
            this.db_datawriter=new Schemas.db_datawriter(this);
            this.db_ddladmin=new Schemas.db_ddladmin(this);
            this.db_denydatareader=new Schemas.db_denydatareader(this);
            this.db_denydatawriter=new Schemas.db_denydatawriter(this);
            this.db_owner=new Schemas.db_owner(this);
            this.db_securityadmin=new Schemas.db_securityadmin(this);
            this.dbo=new Schemas.dbo(this);
            this.guest=new Schemas.guest(this);
            this.INFORMATION_SCHEMA=new Schemas.INFORMATION_SCHEMA(this);
            this.sys=new Schemas.sys(this);
        }
        public override Container Transaction(){
            var Container=new Container(this);
            this.Copy(Container);
            return Container;
        }
        protected override void Read(global::System.Xml.XmlDictionaryReader Reader){
            this.db_accessadmin.Read(Reader);
            this.db_backupoperator.Read(Reader);
            this.db_datareader.Read(Reader);
            this.db_datawriter.Read(Reader);
            this.db_ddladmin.Read(Reader);
            this.db_denydatareader.Read(Reader);
            this.db_denydatawriter.Read(Reader);
            this.db_owner.Read(Reader);
            this.db_securityadmin.Read(Reader);
            this.dbo.Read(Reader);
            this.guest.Read(Reader);
            this.INFORMATION_SCHEMA.Read(Reader);
            this.sys.Read(Reader);
        }
        protected override void Write(global::System.Xml.XmlDictionaryWriter Writer){
            this.db_accessadmin.Write           (Writer);
            this.db_backupoperator.Write           (Writer);
            this.db_datareader.Write           (Writer);
            this.db_datawriter.Write           (Writer);
            this.db_ddladmin.Write           (Writer);
            this.db_denydatareader.Write           (Writer);
            this.db_denydatawriter.Write           (Writer);
            this.db_owner.Write           (Writer);
            this.db_securityadmin.Write           (Writer);
            this.dbo.Write           (Writer);
            this.guest.Write           (Writer);
            this.INFORMATION_SCHEMA.Write           (Writer);
            this.sys.Write           (Writer);
        }
        protected override void Copy(Container To){
            To.db_accessadmin.Assign(this.db_accessadmin);
            To.db_backupoperator.Assign(this.db_backupoperator);
            To.db_datareader.Assign(this.db_datareader);
            To.db_datawriter.Assign(this.db_datawriter);
            To.db_ddladmin.Assign(this.db_ddladmin);
            To.db_denydatareader.Assign(this.db_denydatareader);
            To.db_denydatawriter.Assign(this.db_denydatawriter);
            To.db_owner.Assign(this.db_owner);
            To.db_securityadmin.Assign(this.db_securityadmin);
            To.dbo.Assign(this.dbo);
            To.guest.Assign(this.guest);
            To.INFORMATION_SCHEMA.Assign(this.INFORMATION_SCHEMA);
            To.sys.Assign(this.sys);
        }
        protected override void Commit(global::System.Xml.XmlDictionaryWriter Writer){
            this.db_accessadmin.Write           (Writer);
            this.db_backupoperator.Write           (Writer);
            this.db_datareader.Write           (Writer);
            this.db_datawriter.Write           (Writer);
            this.db_ddladmin.Write           (Writer);
            this.db_denydatareader.Write           (Writer);
            this.db_denydatawriter.Write           (Writer);
            this.db_owner.Write           (Writer);
            this.db_securityadmin.Write           (Writer);
            this.dbo.Write           (Writer);
            this.guest.Write           (Writer);
            this.INFORMATION_SCHEMA.Write           (Writer);
            this.sys.Write           (Writer);
        }
        protected override void UpdateRelationship(){
            this.db_accessadmin.UpdateRelationship();
            this.db_backupoperator.UpdateRelationship();
            this.db_datareader.UpdateRelationship();
            this.db_datawriter.UpdateRelationship();
            this.db_ddladmin.UpdateRelationship();
            this.db_denydatareader.UpdateRelationship();
            this.db_denydatawriter.UpdateRelationship();
            this.db_owner.UpdateRelationship();
            this.db_securityadmin.UpdateRelationship();
            this.dbo.UpdateRelationship();
            this.guest.UpdateRelationship();
            this.INFORMATION_SCHEMA.UpdateRelationship();
            this.sys.UpdateRelationship();
        }
        public override void RelationValidate(){
            var db_accessadmin=this.db_accessadmin;
            var db_backupoperator=this.db_backupoperator;
            var db_datareader=this.db_datareader;
            var db_datawriter=this.db_datawriter;
            var db_ddladmin=this.db_ddladmin;
            var db_denydatareader=this.db_denydatareader;
            var db_denydatawriter=this.db_denydatawriter;
            var db_owner=this.db_owner;
            var db_securityadmin=this.db_securityadmin;
            var dbo=this.dbo;
            var guest=this.guest;
            var INFORMATION_SCHEMA=this.INFORMATION_SCHEMA;
            var sys=this.sys;
            //多対１
            foreach(var a in dbo.customer){
                if(!dbo.district.ContainsKey(a.FK_customer_district.PrimaryKey))throw new global::LinqDB.Databases.RelationshipException("dbo.customer"に対応するFK_customer_district.PrimaryKeyがなかった。");
            }
            foreach(var a in dbo.district){
                if(!dbo.warehouse.ContainsKey(a.FK_district_warehouse.PrimaryKey))throw new global::LinqDB.Databases.RelationshipException("dbo.district"に対応するFK_district_warehouse.PrimaryKeyがなかった。");
            }
            foreach(var a in dbo.history){
                if(!dbo.customer.ContainsKey(a.FK_history_customer.PrimaryKey))throw new global::LinqDB.Databases.RelationshipException("dbo.history"に対応するFK_history_customer.PrimaryKeyがなかった。");
                if(!dbo.district.ContainsKey(a.FK_history_district.PrimaryKey))throw new global::LinqDB.Databases.RelationshipException("dbo.history"に対応するFK_history_district.PrimaryKeyがなかった。");
            }
            foreach(var a in dbo.new_orders){
                if(!dbo.orders.ContainsKey(a.FK_new_orders_orders.PrimaryKey))throw new global::LinqDB.Databases.RelationshipException("dbo.new_orders"に対応するFK_new_orders_orders.PrimaryKeyがなかった。");
            }
            foreach(var a in dbo.order_line){
                if(!dbo.orders.ContainsKey(a.FK_order_line_orders.PrimaryKey))throw new global::LinqDB.Databases.RelationshipException("dbo.order_line"に対応するFK_order_line_orders.PrimaryKeyがなかった。");
                if(!dbo.stock.ContainsKey(a.FK_order_line_stock.PrimaryKey))throw new global::LinqDB.Databases.RelationshipException("dbo.order_line"に対応するFK_order_line_stock.PrimaryKeyがなかった。");
            }
            foreach(var a in dbo.orders){
                if(!dbo.customer.ContainsKey(a.FK_orders_customer.PrimaryKey))throw new global::LinqDB.Databases.RelationshipException("dbo.orders"に対応するFK_orders_customer.PrimaryKeyがなかった。");
            }
            foreach(var a in dbo.stock){
                if(!dbo.item.ContainsKey(a.FK_stock_item.PrimaryKey))throw new global::LinqDB.Databases.RelationshipException("dbo.stock"に対応するFK_stock_item.PrimaryKeyがなかった。");
                if(!dbo.warehouse.ContainsKey(a.FK_stock_warehouse.PrimaryKey))throw new global::LinqDB.Databases.RelationshipException("dbo.stock"に対応するFK_stock_warehouse.PrimaryKeyがなかった。");
            }
        }
    }
}
