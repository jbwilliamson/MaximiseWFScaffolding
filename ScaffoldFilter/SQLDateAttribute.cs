using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace ScaffoldFilter
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SQLDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return IsValidSqlDateTimeNative(value);
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }

        private bool IsValidSqlDateTimeNative(object someval)
        {
            bool valid = false;
            string dt = Convert.ToString(someval);

            DateTime testDate = DateTime.MinValue;
            System.Data.SqlTypes.SqlDateTime sdt;
            if (DateTime.TryParse(dt, out testDate))
            {
                try
                {
                    sdt = new System.Data.SqlTypes.SqlDateTime(testDate);
                    valid = true;
                }
                catch
                {
                }
            }

            return valid;
        }
    }
}
