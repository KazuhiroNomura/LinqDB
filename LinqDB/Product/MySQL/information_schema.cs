namespace LinqDB.Product.MySQL;
public class information_schema:Product.information_schema{
    private readonly string all;
    public information_schema(bool allか){
        this.all = allか ? "all_" : "";
    }
    public override string Database =>"show databases";
    public override string SQL_ScalarFunction =>"SELECT * FROM performance_schema.user_defined_functions";
    public override string SQL_Function_Parameter =>@$"
        SELECT P.name,P.is_output,T.name type,P.has_default_value,P.default_value
        FROM sys.{this.all}objects    B
        JOIN sys.schemas        S ON B.schema_id     =S.schema_id
        JOIN sys.{this.all}parameters P ON B.object_id     =P.object_id
        JOIN sys.types          T ON P.system_type_id=T.user_type_id
        WHERE S.name=@SCHEMA AND B.name=@NAME
        ORDER BY B.name,P.parameter_id
    ";
    public override string SQL_ForeignKey =>@"
        SELECT DISTINCT
             TC_N.CONSTRAINT_NAME
            ,TC_1.TABLE_SCHEMA
            ,TC_1.TABLE_NAME
            ,TC_N.TABLE_SCHEMA
            ,TC_N.TABLE_NAME
        FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS       TC_N
        JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE        KCU_N ON TC_N .CONSTRAINT_CATALOG       =KCU_N.CONSTRAINT_CATALOG AND TC_N .CONSTRAINT_SCHEMA       =KCU_N.CONSTRAINT_SCHEMA AND TC_N .CONSTRAINT_NAME       =KCU_N.CONSTRAINT_NAME
        JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC_N  ON KCU_N.CONSTRAINT_CATALOG       =RC_N .CONSTRAINT_CATALOG AND KCU_N.CONSTRAINT_SCHEMA       =RC_N .CONSTRAINT_SCHEMA AND KCU_N.CONSTRAINT_NAME       =RC_N .CONSTRAINT_NAME
        JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS       TC_1  ON RC_N .UNIQUE_CONSTRAINT_CATALOG=TC_1 .CONSTRAINT_CATALOG AND RC_N .UNIQUE_CONSTRAINT_SCHEMA=TC_1 .CONSTRAINT_SCHEMA AND RC_N .UNIQUE_CONSTRAINT_NAME=TC_1 .CONSTRAINT_NAME
        JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE        KCU_1 ON TC_1.CONSTRAINT_CATALOG       =KCU_1.CONSTRAINT_CATALOG AND TC_1.CONSTRAINT_SCHEMA       =KCU_1.CONSTRAINT_SCHEMA AND TC_1.CONSTRAINT_NAME       =KCU_1.CONSTRAINT_NAME
        WHERE TC_N.CONSTRAINT_TYPE='FOREIGN KEY'
        ORDER BY
             TC_N.CONSTRAINT_NAME
            ,TC_1.TABLE_SCHEMA
            ,TC_1.TABLE_NAME
            ,TC_N.TABLE_SCHEMA
            ,TC_N.TABLE_NAME
    ";
}
