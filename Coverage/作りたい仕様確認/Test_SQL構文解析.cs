using System;
using System.Text;
using CoverageCS.LinqDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.IO;
namespace CoverageCS.作りたい仕様確認;

[TestClass]
public class Test_SQL構文解析: ATest
{
    public class TSql110Parser2:TSql110Parser {
        public TSql110Parser2(bool initialQuotedIdentifiers) : base(initialQuotedIdentifiers) {
        }
        public override BooleanExpression ParseBooleanExpression(TextReader input,out IList<ParseError> errors,int startOffset,int startLine,int startColumn) => base.ParseBooleanExpression(input,out errors,startOffset,startLine,startColumn);
        public override ChildObjectName ParseChildObjectName(TextReader input,out IList<ParseError> errors,int startOffset,int startLine,int startColumn) => base.ParseChildObjectName(input,out errors,startOffset,startLine,startColumn);
        public override TSqlFragment ParseConstantOrIdentifier(TextReader input,out IList<ParseError> errors,int startOffset,int startLine,int startColumn) => base.ParseConstantOrIdentifier(input,out errors,startOffset,startLine,startColumn);
        public override TSqlFragment ParseConstantOrIdentifierWithDefault(TextReader input,out IList<ParseError> errors,int startOffset,int startLine,int startColumn) => base.ParseConstantOrIdentifierWithDefault(input,out errors,startOffset,startLine,startColumn);
        public override StatementList ParseStatementList(TextReader input,out IList<ParseError> errors,int startOffset,int startLine,int startColumn) => base.ParseStatementList(input,out errors,startOffset,startLine,startColumn);
        public override SelectStatement ParseSubQueryExpressionWithOptionalCTE(TextReader input,out IList<ParseError> errors,int startOffset,int startLine,int startColumn) => base.ParseSubQueryExpressionWithOptionalCTE(input,out errors,startOffset,startLine,startColumn);
        public override ScalarExpression ParseExpression(TextReader input,out IList<ParseError> errors,int startOffset,int startLine,int startColumn) => base.ParseExpression(input,out errors,startOffset,startLine,startColumn);
        public override DataTypeReference ParseScalarDataType(TextReader input,out IList<ParseError> errors,int startOffset,int startLine,int startColumn) => base.ParseScalarDataType(input,out errors,startOffset,startLine,startColumn);
        public override SchemaObjectName ParseSchemaObjectName(TextReader input,out IList<ParseError> errors,int startOffset,int startLine,int startColumn) => base.ParseSchemaObjectName(input,out errors,startOffset,startLine,startColumn);
    }
    public class SelectElementVisitor:TSqlConcreteFragmentVisitor {
        private readonly StringBuilder sb = new();
        //private Expression? Result;
        public void 初期化() => this.sb.Clear();
        public int Count { get; set; }
        //public override void Visit(SelectElement node) {
        //    Count++;
        //    base.Visit(node);
        //}
        public override void Visit(FromClause node) => base.Visit(node);
        public override void Visit(AddAlterFullTextIndexAction node) => base.Visit(node);
        public override void Visit(SelectStatement node) => base.Visit(node);
        public override void Visit(SelectScalarExpression node) => base.Visit(node);
    }
    public class IdentifierSquareQuote:TSqlConcreteFragmentVisitor {
        public override void Visit(Identifier node) {
            if(node.QuoteType==QuoteType.NotQuoted) {
                //node.QuoteType=QuoteType.SquareBracket;
            }
            base.Visit(node);
        }
    }
    [TestMethod]
    public void SQL()
    {
        var query = @"select [Id], [Name], [EntryDate] 
                from [Table_1] 
                where [EntryDate] >= @EntryDateFrom and [Name] like @Name 
                order by [Id] desc";
        var parser = new TSql110Parser2(false);
        var parsed = parser.Parse(new StringReader(query),out var errors);
        if(errors.Count!=0) { throw new Exception("パース失敗してるよ！"); }
        var SelectElementVisitor=new SelectElementVisitor();
        parsed.Accept(SelectElementVisitor);
        var options = new SqlScriptGeneratorOptions() {
                
            //  KeywordCasing=KeywordCasing.Uppercase,
            //IncludeSemicolons=true,
            //NewLineBeforeFromClause=true,
            //NewLineBeforeOrderByClause=true,
            //NewLineBeforeWhereClause=true
        };
        var generator = new Sql110ScriptGenerator(options);
        generator.GenerateScript(parsed,out var formated);
        Console.WriteLine(formated);
    }
}