namespace LinqDB.Product.SQLServer;
public class information_schema:Product.information_schema{
    private readonly string all;
    public information_schema(bool allか){
        this.all = allか ? "all_" : "";
    }
    public override string Database =>@"
        SELECT name
        FROM master.sys.databases
        WHERE name=@CATALOG
        ORDER BY database_id
    ";
    public override string SQL_View =>this.SQL_View_ScalarFunction_TableFunction_Procedure("='V'");
    public override string SQL_ScalarFunction =>this.SQL_View_ScalarFunction_TableFunction_Procedure("='FN'");
    public override string SQL_TableFunction =>this.SQL_View_ScalarFunction_TableFunction_Procedure(" IN('TF','IF')");
    public override string SQL_Procedure =>this.SQL_View_ScalarFunction_TableFunction_Procedure("='P'");
    private string SQL_View_ScalarFunction_TableFunction_Procedure(string predicate)=>$@"
        SELECT O.name,M.definition,O.type
        FROM sys.{this.all}objects       O
        JOIN sys.schemas           S ON O.schema_id=S.schema_id
        JOIN sys.{this.all}sql_modules M ON O.object_id=M.object_id
        WHERE O.type{predicate}AND S.name=@SCHEMA
        ORDER BY O.name
    ";
    public override string SQL_Table_Column => $@"
        SELECT SCHEMA_NAME(types.schema_id),TYPE_NAME(C.user_type_id),C.name,C.is_nullable,CASE WHEN C.is_identity=1 THEN 1 ELSE 0 END,CASE WHEN EXISTS(
	        SELECT * FROM
	        sys.index_columns IC
	        JOIN sys.indexes I ON IC.object_id = I.object_id AND IC.index_id = I.index_id AND I.is_unique=1
	        WHERE T.object_id=IC.object_id AND C.column_id = IC.column_id AND I.is_unique=1
        )THEN 1 ELSE 0 END A
        FROM sys.all_columns C
		INNER JOIN sys.types types ON C.user_type_id=types.user_type_id
        INNER JOIN sys.tables T ON C.object_id = T.object_id
        INNER JOIN sys.schemas S ON T.schema_id = S.schema_id
        WHERE S.name=@SCHEMA AND T.name=@NAME
        order by C.column_id
    ";
    public override string SQL_View_ScalarFunction_TableFunction=>this.SQL_View_ScalarFunction_TableFunction_Procedure(" IN('V','FN','TF','IF','P')");
    //public override String SQLView =>
    //    "WITH 表(     name,SQL                     ,  object_id,  type) AS (\r\n"+
    //    "    SELECT B.name,CAST(''AS NVARCHAR(MAX)),B.object_id,B.type\r\n"+
    //    "    FROM sys.objects B \r\n"+
    //    "    WHERE B.type IN('U')\r\n"+
    //    "    UNION ALL\r\n"+
    //    "    SELECT B.name,SQL.definition          ,B.object_id,B.type\r\n"+
    //    "    FROM sys.sql_expression_dependencies A \r\n"+
    //    "    JOIN sys.objects   B ON A.referencing_id=B.object_id\r\n"+
    //    "    JOIN 表            C ON A.referenced_id=C.object_id\r\n"+
    //    "    JOIN sys.all_views V ON V.object_id=B.object_id\r\n"+
    //    "    JOIN sys.all_sql_modules SQL ON V.object_id=SQL.object_id\r\n"+
    //    "    JOIN sys.schemas S ON V.schema_id=S.schema_id\r\n"+
    //    "    WHERE S.name=@SCHEMA AND B.type IN('V')\r\n"+
    //    ")SELECT DISTINCT * FROM 表 WHERE type='V'\r\n";
    //private const String 共通=
    //    "SELECT O.name,SQL.definition\r\n"+
    //    "FROM sys.objects   O\r\n"+
    //    "JOIN sys.all_sql_modules SQL ON O.object_id=SQL.object_id\r\n"+
    //    "JOIN sys.schemas           S ON O.schema_id=S.schema_id\r\n"+
    //    "WHERE S.name=@SCHEMA AND O.type='";
    //public virtual String SQLView_ScalarFunction_TableFunction =>
    //    "SELECT O.name,SQL.definition,O.type\r\n"+
    //    "FROM sys.objects   O\r\n"+
    //    "JOIN sys.all_sql_modules SQL ON O.object_id=SQL.object_id\r\n"+
    //    "JOIN sys.schemas           S ON O.schema_id=S.schema_id\r\n"+
    //    "WHERE S.name=@SCHEMA AND O.type IN('V','FN','TF','P')\r\n";
    //public override String SQLView =>共通+"V'\r\nORDER BY O.name\r\n";
    //public override String SQLScalarFunction =>共通+"FN'\r\nORDER BY O.name\r\n";
    //public virtual String SQLTableFunction =>共通+"TF'\r\nORDER BY O.name\r\n";
    //public override String SQLView =>
    //    "SELECT O.name,SQL.definition\r\n"+
    //    "FROM sys.objects   O\r\n"+
    //    "JOIN sys.all_sql_modules SQL ON O.object_id=SQL.object_id\r\n"+
    //    "JOIN sys.schemas           S ON O.schema_id=S.schema_id\r\n"+
    //    "WHERE S.name=@SCHEMA AND O.type='V'\r\n"+
    //    "ORDER BY O.name\r\n";
    //public override String SQLScalarFunction =>
    //    "SELECT O.name,SQL.DEFINITION\r\n"+
    //    "FROM sys.objects O\r\n"+
    //    "JOIN sys.all_sql_modules SQL ON O.object_id=SQL.object_id\r\n"+
    //    "JOIN sys.schemas           S ON O.schema_id=S.schema_id\r\n"+
    //    "WHERE S.name=@SCHEMA AND O.type='FN'\r\n"+
    //    "ORDER BY O.name\r\n";
    //public virtual String SQLTableFunction =>
    //    "SELECT O.name,SQL.DEFINITION\r\n"+
    //    "FROM sys.objects O\r\n"+
    //    "JOIN sys.all_sql_modules SQL ON O.object_id=SQL.object_id\r\n"+
    //    "JOIN sys.schemas           S ON O.schema_id=S.schema_id\r\n"+
    //    "WHERE S.name=@SCHEMA AND O.type='TF'\r\n"+
    //    "ORDER BY O.name\r\n";
    public override string SQL_Function_Parameter =>@$"
        SELECT P.name,P.is_output,T.name type,P.has_default_value,P.default_value
        FROM sys.{this.all}objects    B
        JOIN sys.schemas        S ON B.schema_id     =S.schema_id
        JOIN sys.{this.all}parameters P ON B.object_id     =P.object_id
        JOIN sys.types          T ON P.system_type_id=T.user_type_id
        WHERE S.name=@SCHEMA AND B.name=@NAME
        ORDER BY B.name,P.parameter_id
    ";
    public virtual string SQL_Synonym =>@$"
        SELECT O.name synonym,A.base_object_name
        FROM sys.{this.all}objects  O
        JOIN sys.synonyms A ON O.object_id=A.object_id
        JOIN sys.schemas  S ON O.schema_id=S.schema_id
        WHERE S.name=@SCHEMA 
        ORDER BY O.name
    ";
    //public override String SQLProcedure =>
    //    "SELECT P.name SPECIFIC_NAME,SQL.definition ROUTINE_DEFINITION\r\n"+
    //    "FROM sys.procedures P\r\n"+
    //    "JOIN sys.all_sql_modules SQL ON P.object_id= SQL.object_id\r\n"+
    //    "JOIN sys.schemas S ON P.schema_id=S.schema_id\r\n"+
    //    "WHERE S.name=@SCHEMA\r\n"+
    //    "ORDER BY P.name\r\n";
    //    //"SELECT ''TRIGGER_NAME,''EVENT_MANIPLULATION,''EVENT_OBJECT_NAME,''ACTION_STATEMENT\r\n"+
    //public override String SQLTrigger =>
    //    "SELECT schemas.name EVENT_OBJECT_SCHEMA,tables.name EVENT_OBJECT_TABLE,triggers.name TRIGGER_NAME,CASE triggers.is_instead_of_trigger WHEN 1 THEN 'BEFORE'ELSE'AFTER'END ACTION_TIMING,trigger_events.type_desc EVENT_MANIPULATION,sql_modules.definition ACTION_STATEMENT\r\n"+
    //    "FROM sys.triggers\r\n"+
    //    "JOIN sys.trigger_events ON triggers.object_id= trigger_events.object_id\r\n"+
    //    "JOIN sys.tables ON triggers.parent_id= tables.object_id\r\n"+
    //    "JOIN sys.sql_modules ON sql_modules.object_id= triggers.object_id\r\n"+
    //    "JOIN sys.schemas ON tables.schema_id= schemas.schema_id\r\n"+
    //    "WHERE schemas.name=@SCHEMA AND tables.name=@NAME\r\n"+
    //    "ORDER BY triggers.name\r\n";
    //public override String SQLTableColumn =>
    //    "SELECT ISNULL(D.name,E.name)DATA_TYPE,A.name COLUMN_NAME,CASE WHEN A.is_nullable=1 THEN'YES'ELSE'NO'END IS_NULLABLE\r\n"+
    //    "FROM sys.columns A\r\n"+
    //    "JOIN sys.tables  B ON A.object_id   =B.object_id\r\n"+
    //    "JOIN sys.schemas C ON B.schema_id   =C.schema_id\r\n"+
    //    //"JOIN sys.types   D ON D.user_type_id=A.user_type_id OR D.user_type_id=A.system_type_id\r\n"+
    //    "LEFT JOIN sys.types D ON A.system_type_id=D.user_type_id\r\n"+
    //    "LEFT JOIN sys.types E ON D.user_type_id IS NULL AND A.user_type_id=E.user_type_id\r\n"+
    //    //"JOIN sys.types       D ON D.system_type_id=A.system_type_id AND D.is_user_defined=0\r\n"+
    //    "WHERE C.name=@SCHEMA AND B.name=@NAME\r\n"+
    //    "ORDER BY B.name,A.name\r\n";
    //public override String SQLViewColumn =>
    //    "SELECT D.name DATA_TYPE,A.name COLUMN_NAME,CASE WHEN A.is_nullable=1 THEN'YES'ELSE'NO'END IS_NULLABLE\r\n"+
    //    "FROM sys.columns A\r\n"+
    //    "JOIN sys.all_views       B ON A.object_id   =B.object_id\r\n"+
    //    "JOIN sys.schemas     C ON B.schema_id   =C.schema_id\r\n"+
    //    //"JOIN sys.types       D ON D.user_type_id=A.user_type_id OR D.user_type_id=A.system_type_id\r\n"+
    //    "JOIN sys.types   D ON D.user_type_id=A.system_type_id\r\n"+
    //    //"JOIN sys.types       D ON D.system_type_id=A.system_type_id AND D.is_user_defined=0\r\n"+
    //    "WHERE C.name=@SCHEMA AND B.name=@NAME\r\n"+
    //    "ORDER BY B.name,A.name\r\n";
    //sys.types
    public override string SQL_View_FunctionTable_Column => @$"
        SELECT C.name COLUMN_NAME,ISNULL(T1.name,T0.name)DATA_TYPE,C.is_nullable IS_NULLABLE
        FROM      sys.{this.all}objects O
        JOIN      sys.schemas     S  ON O.schema_id     =S.schema_id
        JOIN      sys.{this.all}columns C  ON O.object_id     =C.object_id
        JOIN      sys.types       T0 ON C.user_type_id=T0.user_type_id
        LEFT JOIN sys.types       T1 ON T0.system_type_id=T1.user_type_id
        WHERE O.type IN('V','TF','IF')AND S.name=@SCHEMA AND O.name=@NAME
        ORDER BY C.column_id";//column_idというのはコンストラクター順、列順として重要
    public string SQL_Types=>@$"
        SELECT types.name,baset.name SystemType,types.is_nullable
        FROM sys.types AS types
        JOIN sys.schemas AS sst ON sst.schema_id = types.schema_id
        JOIN sys.types AS baset ON (baset.user_type_id = types.system_type_id and baset.user_type_id = baset.system_type_id) or ((baset.system_type_id = types.system_type_id) and (baset.user_type_id = types.user_type_id) and (baset.is_user_defined = 0) and (baset.is_assembly_type = 1)) 
        WHERE types.is_user_defined=1 AND sst.name=@SCHEMA";
}
