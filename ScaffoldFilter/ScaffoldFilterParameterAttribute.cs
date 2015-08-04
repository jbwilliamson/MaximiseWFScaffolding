using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace ScaffoldFilter
{
    public class ScaffoldFilterParameterAttribute : ValueProviderSourceAttribute
    {
        private string key = null;

        public ScaffoldFilterParameterAttribute(string key)
        {
            this.key = key;
        }

        public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
        {
            return new ScaffoldFilterParameterProvider(modelBindingExecutionContext.HttpContext.Request);
        }

        public override string GetModelName()
        {
            return key;
        }
    }
}