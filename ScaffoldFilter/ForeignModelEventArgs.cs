using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScaffoldFilter
{
    public delegate void ForeignKeyEventHandler(ForeignModelEventArgs e);
	public delegate Task AsyncForeignKeyEventHandler(ForeignModelEventArgs e);
    public delegate void FilterChangeEventHandler(Object s, FilterChangeEventArgs e);
    public delegate void FilterLoadEventHandler(FilterLoadEventArgs e);

    public class ForeignModelEventArgs : EventArgs
    {
        public const int LoadForeignTableByModel = 1;
        public const int LoadForeignTableByKey = 2;

        public IQueryable returnResults { get; set; }
        public string foreignKeyModel { get; set; }
        public int keyType { get; set; }
    }

    public class FilterChangeEventArgs : EventArgs
    {
        public string SelectedValue { get; set; }
        public string FieldName { get; set; }
    }

    public class FilterLoadEventArgs : EventArgs
    {
        public Dictionary<string, string> FilterDefaults { get; set; }
    }
}


