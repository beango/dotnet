using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace beango.speedm
{
    public static class ToEntityByExpression
    {
        public static List<T> GetEntity<T>(DataTable dt)
        {
            List<T> lst = new List<T>();
            Dictionary<string, Func<T, object, object>> dic = new Dictionary<string, Func<T, object, object>>();
            List<string> cols = new List<string>();
            foreach (DataColumn dc in dt.Columns)
            {
                PropertyInfo pi = typeof(T).GetProperties().FirstOrDefault(p => p.Name.ToLower() == dc.ColumnName.ToLower());
                if (pi != null && !dic.ContainsKey(dc.ColumnName))
                {
                    Func<T, object, object> fc = SetDelegate<T>(pi.GetSetMethod(), pi.PropertyType);
                    dic.Add(dc.ColumnName, fc);
                    cols.Add(dc.ColumnName);
                }
            }

            foreach (DataRow dr in dt.Rows)
            {
                T model = (T)Activator.CreateInstance(typeof(T));

                foreach (string dc in cols)
                {
                    object value = dr[dc];
                    if (value != DBNull.Value)
                    {
                        Func<T, object, object> fc = dic[dc];
                        fc(model, value);
                    }
                }
                lst.Add(model);
            }

            return lst;
        }



        static Func<T, object, object> SetDelegate<T>(MethodInfo m, Type type)
        {
            ParameterExpression param_obj = Expression.Parameter(typeof(T), "obj");
            ParameterExpression param_val = Expression.Parameter(typeof(object), "val");
            UnaryExpression body_val = Expression.Convert(param_val, type);
            MethodCallExpression body = Expression.Call(param_obj, m, body_val);
            Action<T, object> set = Expression.Lambda<Action<T, object>>(body, param_obj, param_val).Compile();
            if (type.IsEnum)
            {
                return (instance, v) =>
                 {
                     set(instance, Enum.Parse(type, v.ToString()));
                     return null;
                 };
            }
            else
            {
                return (instance, v) =>
                           {
                               set(instance, v);
                               return null;
                           };
            }

        }
    }
}
