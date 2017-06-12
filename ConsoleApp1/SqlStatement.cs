using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class SqlStatement
    {
        public string Tablename { get; set; }
        public List<string> Columns = new List<string>();
        public string WhereClause { get; set; }
        public List<string> OrderByClauses = new List<string>();

        public string ToUpdate() {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE ");
            sb.Append(" [" + this.Tablename + "] ");
            sb.Append(" SET ");
            List<string> updateColumns = new List<string>();
            foreach(string column in this.Columns) {
                if (column.ToLower().Equals("Id".ToLower())) // skip updating ID
                    continue;
                updateColumns.Add($"{column} = @{column}");
            }
            sb.Append(string.Join(", ", updateColumns));
            sb.Append(" WHERE Id = @Id");
            return sb.ToString();
        }
        public string ToInsert() {
            StringBuilder sb = new StringBuilder();
            var columnsNoId = new List<string>();
            foreach(string column in this.Columns) {
                if (column.ToLower().Equals("Id".ToLower()))
                    continue;
                columnsNoId.Add(column);
            }
            sb.Append("INSERT ");
            sb.Append(" [" + this.Tablename + "] ");
            sb.Append("(");
            string columnList = "[" + string.Join("],[", columnsNoId) + "]";
            sb.Append(" " + columnList);
            sb.Append(") VALUES (");
            string parameterList = "@" + string.Join(", @", columnsNoId);
            sb.Append(parameterList);
            sb.Append(")");

            return sb.ToString();
        }
        public string ToDelete()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE ");
            sb.Append(" FROM [" + this.Tablename + "]");
            if (WhereClause != null)
            {
                sb.Append(" WHERE (");
                sb.Append(this.WhereClause);
                sb.Append(") ");
            }
            return sb.ToString();
        }
        public string ToSelect()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if(this.Columns.Count == 0)
            {
                sb.Append(" * ");
            } else
            {
                string columnList = "[" + string.Join("],[", this.Columns) + "]";
                sb.Append(" " + columnList);
            }
            sb.Append(" FROM ["+this.Tablename + "]");

            if(WhereClause != null)
            {
                sb.Append(" WHERE (");
                sb.Append(this.WhereClause);
                sb.Append(") ");
            }
            if(OrderByClauses.Count != 0)
            {
                sb.Append(" ORDER BY ");
                foreach(string orderClause in this.OrderByClauses)
                {
                    string orderclause = string.Join(",", this.OrderByClauses);
                    sb.Append(orderclause);
                }

            }

            return sb.ToString();
        }
        public SqlStatement(string tablename, string[] columns, string whereClause, string[] orderColumns)
        {
            // table
            this.Tablename = tablename;
            // column list
            if(columns != null)
            {
                foreach (string column in columns)
                    this.Columns.Add(column);
            }
            // where clause
            this.WhereClause = whereClause;
            // order by
            if(orderColumns != null)
            {
                foreach(string column in orderColumns)
                    this.OrderByClauses.Add(column);
            }
        }
        public SqlStatement(string tablename, string[] columns, string[] orderColumns) 
            : this(tablename, columns, null, orderColumns) {

        }
        public SqlStatement(string tablename, string[] columns, string whereClause) 
            : this(tablename, columns, whereClause, null) {

        }
        public SqlStatement(string tablename, string[] columns) 
            : this(tablename, columns, null, null) {

        }
        public SqlStatement(string tablename) 
            : this(tablename, null) {

        }
    }

}
