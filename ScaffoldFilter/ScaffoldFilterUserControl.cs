using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.DynamicData;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Globalization;

namespace ScaffoldFilter
{
    public class ScaffoldFilterUserControl : UserControl
    {
        private FilterFields _filterFields { get; set; }
        private ColumnFilter _columnFilter;
        public event FilterChangeEventHandler FilterChanged;
        public event FilterLoadEventHandler FilterLoad;

        public void Initialize(FilterFields ff)
        {
            _filterFields = ff;
            _columnFilter = new ColumnFilter();
            _columnFilter.FieldName = ff.FieldName;
            _columnFilter.ForeignField = ff.ForeignField;
        }

        /// <summary>
        /// The column this filter applies to
        /// </summary>
        public ColumnFilter Column
        {
            get {
                return _columnFilter;
            }
        }

        /// <summary>
        /// Returns the data control that handles the filter inside the filter template. Can be null if the filter template does not override it.
        /// </summary>
        public virtual Control FilterControl {
            get {
                return null;
            }
        }

        /// <summary>
        /// Populate a ListControl with all the items in an enum
        /// </summary>
        /// <param name="listControl"></param>
        public void PopulateListControl(ListControl listControl)
        {
            if (_filterFields.FieldProp != null) {
                FillEnumListControl(listControl, _filterFields.FieldName, _filterFields.FieldProp.PropertyType);
            }
        }

        /// <summary>
        /// Populate a ListControl with all the items in the foreign table
        /// </summary>
        /// <param name="listControl"></param>
        /// <param name="dataSource"></param>
        /// <parm name="valueField"></parm>
        /// <parm name="defaultTextField"></parm>
        protected void PopulateListControl(ListControl listControl, IQueryable dataSource, string valueField)
        {
            var filterdata = new List<KeyValuePair<string, string>>();
            string textField = string.Empty;

            if (dataSource != null)
            {
                if (_filterFields.ForeignText != string.Empty)
                {
                    if (CheckDropDownTextField(_filterFields.ForeignText, dataSource.ElementType)) {
                        textField = _filterFields.ForeignText;
                    }
                }

                if (textField == string.Empty) {
                    textField = FindDropDownTextField(Column.FieldName, dataSource.ElementType);
                }

                foreach (object dataItem in dataSource) {
                    filterdata.Add(new KeyValuePair<string, string>(DataBinder.GetPropertyValue(dataItem, textField, null), DataBinder.GetPropertyValue(dataItem, valueField, null)));
                }

                if (_filterFields.SortField == "Yes")
                {
                    if (_filterFields.SortDescending)
                        filterdata.Sort(CompareDesc);
                    else
                        filterdata.Sort(CompareAsc);
                }

                foreach (KeyValuePair<string, string> keyvalue in filterdata) {
                    listControl.Items.Add(new ListItem(keyvalue.Key, _filterFields.FieldName + ":" + keyvalue.Value));
                }
            }
        }

        /// <summary>
        /// Check the specifed textfield for the filter does exist in the class
        /// </summary>
        /// <param name="keyName">Field name to use drop down list text</param>
        /// <param name="filterType">Class property type</param>
        private bool CheckDropDownTextField(string keyName, Type filterType)
        {
            PropertyInfo property = null;
            property = filterType.GetProperties(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(prop => prop.Name == keyName);

            if (property == null)
                return false;

            return true;
        }

        /// <summary>
        /// Find the first text field in the foreign table class and return its name 
        /// </summary>
        /// <param name="keyName">Default text field name</param>
        /// <param name="filterType">Class property type</param>
        private string FindDropDownTextField(string keyName, Type filterType)
        {
            PropertyInfo property = null;

            if ( property == null) {
                property = filterType.GetProperties(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(prop => prop.PropertyType == typeof(string));
            }

            if (property == null)
                return keyName;

            return property.Name;
        }

        /// <summary>
        /// Populate the dropdownlist control for enum entities
        /// </summary>
        /// <param name="list">drop down list control</param>
        /// <param name="fieldName">name of filter field </param>
        /// /// <param name="enumType">enum type</param>
        private void FillEnumListControl(ListControl list, string fieldName, Type enumType)
        {
            foreach (DictionaryEntry entry in GetEnumNamesAndValues(enumType)) {
                list.Items.Add(new ListItem((string)entry.Key, fieldName + ":" + (string)entry.Value));
            }
        }

        private IOrderedDictionary GetEnumNamesAndValues(Type enumType)
        {
            OrderedDictionary result = new OrderedDictionary();
            var enumEntries = from e in Enum.GetValues(enumType).OfType<object>()
                              select new EnumEntry
                              {
                                  Name = Enum.GetName(enumType, e),
                                  UnderlyingValue = GetUnderlyingTypeValue(enumType, e)
                              };
            foreach (var entry in enumEntries.OrderBy(e => e.UnderlyingValue)) {
                result.Add(entry.Name, entry.UnderlyingValue.ToString());
            }
            return result;
        }

        private struct EnumEntry
        {
            public string Name { get; set; }
            public object UnderlyingValue { get; set; }
        }

        private object GetUnderlyingTypeValue(Type enumType, object enumValue)
        {
            return Convert.ChangeType(enumValue, Enum.GetUnderlyingType(enumType), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Raises the FilterChanged event. This is necessary to notify the data source that the filter selection
        /// has changed and the query needs to be reevaluated.
        /// </summary>
        protected void OnFilterChanged(string selectedValue, string fieldName) 
        {
            FilterChangeEventArgs fcea = new FilterChangeEventArgs();
            fcea.SelectedValue = selectedValue;
            fcea.FieldName = fieldName;
            FilterChangeEventHandler eventHandler = FilterChanged;
            if (eventHandler != null) {
                eventHandler(this, fcea);
            }
        }

        /// <summary>
        /// Raises the FilterLoad event. Used to retrieve any default selected value for the filter
        /// </summary>
        protected void OnFilterLoad(FilterLoadEventArgs flea)
        {
            FilterLoadEventHandler eventHandler = FilterLoad;
            if (eventHandler != null) {
                eventHandler(flea);
            }
        }

        static int CompareAsc(KeyValuePair<string, string> a, KeyValuePair<string, string> b)
        {
            return a.Key.CompareTo(b.Key);
        }

        static int CompareDesc(KeyValuePair<string, string> a, KeyValuePair<string, string> b)
        {
            return b.Key.CompareTo(a.Key);
        }
    }
}
