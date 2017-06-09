using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class SqlStatement
    {
        private string tablename;
        private List<string> columns = new List<string>();
        private string whereClause;
        private List<string> orderByClauses = new List<string>();

        public string ToDelete()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE ");
            sb.Append(" FROM [" + this.tablename + "]");
            if (whereClause != null)
            {
                sb.Append(" WHERE (");
                sb.Append(this.whereClause);
                sb.Append(") ");
            }
            return sb.ToString();
        }
        public string ToSelect()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if(this.columns.Count == 0)
            {
                sb.Append(" * ");
            } else
            {
                string columnList = "[" + string.Join("],[", this.columns) + "]";
                sb.Append(" " + columnList);
            }
            sb.Append(" FROM ["+this.tablename+"]");

            if(whereClause != null)
            {
                sb.Append(" WHERE (");
                sb.Append(this.whereClause);
                sb.Append(") ");
            }
            if(orderByClauses.Count != 0)
            {
                sb.Append(" ORDER BY ");
                foreach(string orderClause in this.orderByClauses)
                {
                    string orderclause = string.Join(",", this.orderByClauses);
                    sb.Append(orderclause);
                }

            }

            return sb.ToString();
        }
        public SqlStatement(string tablename, string[] columns, string whereClause, string[] orderColumns)
        {
            // table
            this.tablename = tablename;
            // column list
            if(columns != null)
            {
                foreach (string column in columns)
                    this.columns.Add(column);
            }
            // where clause
            this.whereClause = whereClause;
            // order by
            if(orderColumns != null)
            {
                foreach(string column in orderColumns)
                    this.orderByClauses.Add(column);
            }
        }
        public SqlStatement()
        {

        }
    }

}
