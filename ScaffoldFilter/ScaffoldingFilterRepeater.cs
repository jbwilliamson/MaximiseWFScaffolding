using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.DynamicData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace ScaffoldFilter
{
    /// <summary>
    /// Repeater Control that enumerates over all the Scaffold filterable columns in a model
    /// </summary>
    [ToolboxItem(false)]
    [ParseChildren(true)]
    public class ScaffoldingFilterRepeater : Control 
    {
        private List<ScaffoldFilterControl> _filters = new List<ScaffoldFilterControl>();
        private bool _initialized = false;
        private string _modelName;

        /// <summary>
        /// Name of the model being filtered on
        /// </summary>
        [Category("Data"),
        DefaultValue((string)null),
        Themeable(false)]
        public string ModelName
        {
            get
            {
                return _modelName ?? String.Empty;
            }
            set
            {
                if (!ModelName.Equals(value)) {
                    _modelName = value;
                }
            }
        }

        /// <summary>
        /// The ID of a ScaffoldFilter control inside of the template that will be used configured to be a filter for a particular column.
        /// The default value is "ScaffoldFilter"
        /// </summary>
        [
        Category("Behavior"),
        DefaultValue("ScaffoldFilterControl"),
        Themeable(false),
        IDReferenceProperty(typeof(ScaffoldFilterUserControl)),
        ]
        public string ScaffoldFilterContainerId
        {
            get {
                string id = ViewState["__FilterContainerId"] as string;
                return String.IsNullOrEmpty(id) ? "ScaffoldFilter" : id;
            }
            set {
                ViewState["__FilterContainerId"] = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Don't do anything in Design mode
            if (DesignMode)
            {
                return;
            }

            Page.InitComplete += new EventHandler(Page_InitComplete);
        }

        /// <summary>
        /// The template in which the layout of each filter can be specified. Just like ItemTempalte in Repeater.
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(INamingContainer))]
        public virtual ITemplate ItemTemplate { get; set; }

        public ScaffoldingFilterRepeater() 
        {
        }

        protected override void OnPreRender(EventArgs e) 
        {
            if (_filters.Count == 0) {
                this.Visible = false;
            }
            
            base.OnPreRender(e);
        }

        protected void Page_InitComplete(object sender, EventArgs e) 
        {
            if (_initialized) {
                return;
            }
            
            int itemIndex = 0;

            if (_modelName != null && _modelName != String.Empty)
            {
                Type efModelType = Type.GetType(_modelName);
                if (efModelType != null)
                {
                    foreach (PropertyInfo prop in efModelType.GetProperties())
                    {
                        FilterFields ff = new FilterFields();

                        var filterUIAttribute = (FilterUIHintAttribute)prop.GetCustomAttribute(typeof(FilterUIHintAttribute), true);
                        ff.FilterUIHint = String.Empty;
                        ff.ForeignText = String.Empty;
                        ff.SortField = String.Empty;
                        ff.SortDescending = false;

                        if (filterUIAttribute != null)
                        {
                            if (filterUIAttribute.FilterUIHint != String.Empty) {
                                ff.FilterUIHint = GetFilterUIHintByName(filterUIAttribute.FilterUIHint);
                            }
                            else {
                                ff.FilterUIHint = GetFilterUIHintByType(prop.GetType());
                            }

                            if (ff.FilterUIHint != String.Empty)
                            {                                
                                var disAttribute = (DisplayAttribute)prop.GetCustomAttribute(typeof(DisplayAttribute), true);
                                ff.DisplayName = "";

                                if (disAttribute == null) {
                                    var dnaAttribute = (DisplayNameAttribute)prop.GetCustomAttribute(typeof(DisplayNameAttribute), true);
                                    ff.DisplayName = dnaAttribute != null && !String.IsNullOrWhiteSpace(dnaAttribute.DisplayName) ? dnaAttribute.DisplayName : prop.Name;
                                }
                                else {
                                    ff.DisplayName = disAttribute != null && !String.IsNullOrWhiteSpace(disAttribute.Name) ? disAttribute.Name : prop.Name;
                                }

                                FilterRepeaterItem item = new FilterRepeaterItem() { DataItemIndex = itemIndex, DisplayIndex = itemIndex };
                                itemIndex++;
                                ItemTemplate.InstantiateIn(item);
                                Controls.Add(item);

                                ScaffoldFilterControl filter = item.FindControl(ScaffoldFilterContainerId) as ScaffoldFilterControl;

                                if (filter == null) {
                                    throw new InvalidOperationException(
                                        String.Format(CultureInfo.CurrentCulture,
                                            "Could not find Control {1}, in template {0}, with ID {2}",
                                            ID,
                                            typeof(ScaffoldFilterUserControl).FullName,
                                            ScaffoldFilterContainerId));
                                }
                                
                                ff.FieldName = prop.Name;

                                ff.ForeignField = "";
                                if (ff.FilterUIHint == ScaffoldConst.s_foreignKeyFilter)
                                {
                                    ff.ForeignField = CheckForeignKeyName(efModelType, ff.FieldName);

                                    object cpTextDisplay = null;
                                    if (filterUIAttribute.ControlParameters.ContainsKey(ScaffoldConst.ForeignKey_TextField)) {
                                        cpTextDisplay = filterUIAttribute.ControlParameters[ScaffoldConst.ForeignKey_TextField];
                                    }

                                    object cpSorting = null;
                                    if (filterUIAttribute.ControlParameters.ContainsKey(ScaffoldConst.ForeignKey_RequireSorting)) {
                                        cpSorting = filterUIAttribute.ControlParameters[ScaffoldConst.ForeignKey_RequireSorting];
                                    }

                                    object cpDescendingOrder = null;
                                    if (filterUIAttribute.ControlParameters.ContainsKey(ScaffoldConst.ForeignKey_SortDesending)) {
                                        cpDescendingOrder = filterUIAttribute.ControlParameters[ScaffoldConst.ForeignKey_SortDesending];
                                    }
                                    
                                    if (cpTextDisplay != null) {
                                        ff.ForeignText = cpTextDisplay.ToString();
                                    }

                                    if (cpSorting != null) {
                                        ff.SortField = cpSorting.ToString();
                                    }

                                    if (cpDescendingOrder != null) {
                                        ff.SortDescending = Convert.ToBoolean(cpDescendingOrder);
                                    }                        
                                }

                                ff.ModelName = _modelName;
                                ff.FieldProp = prop;

                                item.DataItem = ff;
                                item.DataBind();
                                item.DataItem = null;
                                _filters.Add(filter);
                            }
                        }
                    }

                    _filters.ForEach(f => f.Initialize());
                    _initialized = true;
                }
                else
                {
                    throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, "{0} make sure its ModelName property contains a valid name of a EF Model within this project", ID));
                }
            }
            else
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, "{0} make sure its ModelName property contains a valid name of a EF Model within this project", ID));
            }
        }

        // Find the foreign key class for the name of the primary key, might be different than the name given in the main class (NationId => id)
        // Would need to use the [ForeignKey("")], to specify the name if different 
        private string CheckForeignKeyName(Type efModelType, string fieldName)
        {
            string keyName = string.Empty;
            string idName = string.Empty;

            foreach (PropertyInfo prop in efModelType.GetProperties().Where(p => p.PropertyType.IsClass == true && p.PropertyType != typeof(string)))
            {
                var foreignAttribute = (ForeignKeyAttribute)prop.GetCustomAttribute(typeof(ForeignKeyAttribute), true);
                if (foreignAttribute != null)
                {
                    if (fieldName == foreignAttribute.Name)
                    {
                        foreach (PropertyInfo propForeign in prop.PropertyType.GetProperties())
                        {
                            if ((idName == string.Empty && propForeign.Name.Contains("id")) || propForeign.Name.ToLower().Equals("id")) {
                                idName = propForeign.Name;
                            }

                            var keyAttribute = (KeyAttribute)propForeign.GetCustomAttribute(typeof(KeyAttribute), true);
                            if (keyAttribute != null)
                            {
                                keyName = propForeign.Name;
                                break;
                            }
                        }
                        break;
                    }
                }
            }

            if (keyName == string.Empty)
            {
                if (idName == string.Empty) {
                    keyName = fieldName;
                }
                else {
                    keyName = idName;
                }
            }

            return keyName;
        }

        private string GetFilterUIHintByName(string FilterUIHint)
        {
            //if (FilterUIHint == ScaffoldConst.s_textFilter)
            //{
            //    return ScaffoldConst.s_textFilter;
            //}

            //if (FilterUIHint == ScaffoldConst.s_booleanFilter)
            //{
            //    return ScaffoldConst.s_booleanFilter;
            //}

            if (FilterUIHint == ScaffoldConst.s_enumerationFilter)
            {
                return ScaffoldConst.s_enumerationFilter;
            }

            //if (FilterUIHint == ScaffoldConst.s_datatimeFilter)
            //{
            //    return ScaffoldConst.s_datatimeFilter;
            //}

            if (FilterUIHint == ScaffoldConst.s_foreignKeyFilter)
            {
                return ScaffoldConst.s_foreignKeyFilter;
            }

            return null;
        }

        private string GetFilterUIHintByType(Type column)
        {
            if (column == typeof(int)) {
                return ScaffoldConst.s_foreignKeyFilter;
            }

            //if (column == typeof(string)) {
            //    return ScaffoldConst.s_textFilter;
            //}
            
            //if (column == typeof(bool)) {
            //    return ScaffoldConst.s_booleanFilter;
            //}

            if (column.IsEnum) {
                return ScaffoldConst.s_enumerationFilter;
            }

            //if (column == typeof(DateTime)) {
            //    return ScaffoldConst.s_datatimeFilter;
            //}

            return null;
        }

        public class FilterRepeaterItem : Control, IDataItemContainer 
        {
            public object DataItem { get; internal set; }
            public int DataItemIndex { get; internal set; }
            public int DisplayIndex { get; internal set; }
        }
    }
}
