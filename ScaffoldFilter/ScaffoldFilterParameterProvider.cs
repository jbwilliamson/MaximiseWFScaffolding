using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace ScaffoldFilter
{
    public class ScaffoldFilterParameterProvider : IValueProvider
    {
        private HttpRequestBase request = null;
        Dictionary<string, string> FilterDefaults = new Dictionary<string, string>();
        private const string SFFilter = "SFFilter";

        public ScaffoldFilterParameterProvider(HttpRequestBase request)
        {
            this.request = request;
        }

        public bool ContainsPrefix(string prefix)
        {
            throw new NotImplementedException();
        }

        public ValueProviderResult GetValue(string key)
        {
            string result = "";

            foreach (var value in request.Form.AllKeys.Where(k => k.Contains(SFFilter)).ToArray<string>())
            {
                if (!string.IsNullOrEmpty(request.Form[value])) {
                    result += request.Form[value] + ",";
                }
            }

            if (result.Length == 0)
            {
                System.Web.SessionState.HttpSessionState s = System.Web.HttpContext.Current.Session;

                if (s != null)
                {
                    if (key != null)
                    {
                        if (s[key] != null)
                        {
                            FilterDefaults = (Dictionary<string, string>)s[key];

                            foreach (KeyValuePair<string, string> kvp in FilterDefaults)
                            {
                                result += kvp.Value + ",";
                            }
                        }
                    }
                }
            }

            if (result.Length > 0) {
                result = result.Substring(0, result.Length - 1);
            }

            return new ValueProviderResult(result, result, System.Threading.Thread.CurrentThread.CurrentCulture);
        }
    }
}