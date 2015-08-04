using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace ScaffoldFilter
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class GridViewAttribute : Attribute
    {
        public GridViewAttribute(bool _display)
        {
            this.Display = _display;
        }

        private bool display;

        public bool Display
        {
            get { return this.display; }
            set { this.display = value; }
        }

        public bool GetDisplay()
        {
            return this.display;
        }
    }

}
