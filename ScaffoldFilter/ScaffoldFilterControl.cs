using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Resources;
using System.Web.DynamicData;

namespace ScaffoldFilter
{
    public class ScaffoldFilterControl : Control
    {
        private const string s_dynamicDataFolderVirtualPath = "~/DynamicData/Filters/";

        private Func<FilterFields, ScaffoldFilterUserControl> _filterLoader;
        private ScaffoldFilterUserControl _filterUserControl;
        private FilterFields _filterFields = null;

        public ScaffoldFilterControl() : this(CreateUserControl) 
        {
        }

        public ScaffoldFilterControl(Func<FilterFields, ScaffoldFilterUserControl> filterLoader)
        {
            _filterLoader = filterLoader;
        }

        public static ScaffoldFilterUserControl CreateUserControl(FilterFields ff)
        {
            return CreateFilterControl(ff);
        }

        public static ScaffoldFilterUserControl CreateFilterControl(FilterFields ff)
        {
            if (ff == null) {
                throw new ArgumentNullException("FilterFields");
            }

            string filterTemplatePath = GetFilterVirtualPath(ff);

            if (filterTemplatePath != null) {
                return (ScaffoldFilterUserControl)BuildManager.CreateInstanceFromVirtualPath(filterTemplatePath, typeof(ScaffoldFilterUserControl));
            }

            return null;
        }

        public static string GetFilterVirtualPath(FilterFields ff)
        {
            string filterControlName = BuildFilterVirtualPath(ff);
            return VirtualPathUtility.Combine(s_dynamicDataFolderVirtualPath, filterControlName + ".ascx");
        }

        private static string BuildFilterVirtualPath(FilterFields ff)
        {
            string filterControlName = null;
            if (!String.IsNullOrEmpty(ff.FilterUIHint)) {
                filterControlName = ff.FilterUIHint;
            }

            filterControlName = filterControlName ?? GetDefaultFilterControlName(ff.FieldProp.PropertyType, ff.ModelName);
            return filterControlName;
        }

        private static string GetDefaultFilterControlName(Type column, string modelName)
        {
            if (column == typeof(int))
            {
                return ScaffoldConst.s_foreignKeyFilter;
            }
            else if (column == typeof(bool))
            {
                return ScaffoldConst.s_booleanFilter;
            }
            else if (column.IsEnum)
            {
                return ScaffoldConst.s_enumerationFilter;
            }
            else
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture,
                     "The column &apos;{0}&apos; in model &apos;{1}&apos; does not have a default filter",
                    column.Name,
                    modelName));
            }
        }

        public event FilterChangeEventHandler FilterChanged;
        public event FilterLoadEventHandler FilterLoad;

        /// <summary>
        /// Returns the filter template that was created for this control.
        /// </summary>
        public Control FilterTemplate {
            get {
                return _filterUserControl;
            }
        }

        protected override void Render(HtmlTextWriter writer) {
            if (DesignMode) {
                writer.Write("[" + GetType().Name + "]");
            }
            else {
                base.Render(writer);
            }
        }

        private void EnsureInit()
        {
            if (_filterUserControl == null)
            {
                if (_filterFields != null)
                {
                    _filterUserControl = _filterLoader(_filterFields);
                    _filterUserControl.Initialize(_filterFields);
                    _filterUserControl.FilterChanged += new FilterChangeEventHandler(Child_FilterChanged);
                    _filterUserControl.FilterLoad+= new FilterLoadEventHandler(Child_FilterLoad);
                    Controls.Add(_filterUserControl);
                }
                else
                {
                    throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture,
                            "The control {0}, with name {1}, has not been data bound",
                            ID,
                            typeof(ScaffoldFilterUserControl).FullName));
                }
            }
        }

        private void Child_FilterChanged(object sender, FilterChangeEventArgs e)
        {
            FilterChangeEventHandler eventHandler = FilterChanged;
            if (eventHandler != null) {
                eventHandler(sender, e);
            }
        }

        private void Child_FilterLoad(FilterLoadEventArgs e)
        {
            FilterLoadEventHandler eventHandler = FilterLoad;
            if (eventHandler != null) {
                eventHandler(e);
            }
        }

        protected override void DataBind(bool raiseOnDataBinding)
        {
            ScaffoldingFilterRepeater.FilterRepeaterItem fri = (ScaffoldingFilterRepeater.FilterRepeaterItem)this.NamingContainer;
            _filterFields = null;

            if (fri is INamingContainer)
            {
                if (fri.DataItem is FilterFields)
                {
                    _filterFields = (FilterFields)fri.DataItem;
                }
            }
        }

        public void Initialize()
        {
            EnsureInit();
        }
    }
}

