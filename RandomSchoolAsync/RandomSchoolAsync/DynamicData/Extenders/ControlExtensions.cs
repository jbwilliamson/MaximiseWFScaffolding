using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;

namespace RandomSchoolAsync.Extenders
{
    public static class ControlExtensions
    {
        public static void SetDataMethodsObject(this DataBoundControl dataControl, object dataMethodObject)
        {
            dataControl.CallingDataMethods += (s, e) => e.DataMethodsObject = dataMethodObject;
        }

        public static void RedirectToRouteOnItemInserted(this FormView formView, string routeName)
        {
            formView.ItemInserted += (s, e) =>
            {
                if (formView.Page.ModelState.IsValid)
                {
                    formView.Page.Response.Redirect(routeName);
                }
            };
        }

        public static void RedirectToRouteOnItemDeleted(this FormView formView, string routeName)
        {
            formView.ItemDeleted += (s, e) =>
            {
                if (formView.Page.ModelState.IsValid)
                {
                    formView.Page.Response.Redirect(routeName);
                }
            };
        }

        public static void RedirectToRouteOnItemCommand(this FormView formView, string routeName)
        {
            formView.ItemCommand += (s, e) =>
            {
                if (e.CommandName.Equals("Cancel"))
                {
                    formView.Page.Response.Redirect(routeName);
                }
            };
        }

        public static void RedirectToRouteOnItemUpdated(this FormView formView, string routeName)
        {
            formView.ItemUpdated += (s, e) =>
            {
                if (formView.Page.ModelState.IsValid)
                {
                    formView.Page.Response.Redirect(routeName);
                }
            };
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderBy");
        }
        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderByDescending");
        }
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenBy");
        }
        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }
        static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }
    }
}
