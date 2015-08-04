using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace ScaffoldFilter
{
    public class ScaffoldConst
    {
        public const string s_textFilter = "Text";
        public const string s_booleanFilter = "Boolean";
        public const string s_foreignKeyFilter = "ForeignKey";
        public const string s_enumerationFilter = "Enumeration";
        public const string s_datatimeFilter = "DateTime";

        public const string ForeignKey_TextField = "TextField";
        public const string ForeignKey_RequireSorting = "RequireSorting";
        public const string ForeignKey_SortDesending = "SortDesending";
    }

    public class FilterFields
    {
        public string FieldName { get; set; }
        public string ForeignField { get; set; }
        public string FilterUIHint { get; set; }
        public string ModelName { get; set; }
        public string DisplayName { get; set; }
        public PropertyInfo FieldProp { get; set; }
        public string ForeignText { get; set; }
        public string SortField { get; set; }
        public bool SortDescending { get; set; }

        public FilterFields()
        {
            FilterUIHint = String.Empty;
        }
    }

    public class ColumnFilter
    {
        public string FieldName { get; set; }
        public string ForeignField { get; set; }
    }
}