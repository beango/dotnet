using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace beango.speedm
{
    public delegate void SetString<TValue>(TValue value);
    public delegate void SetValue<T>(T value);

    public class ToEntityByDelegate<T>
    {
        public List<T> GetEntity(DataTable dt)
        {
            List<T> lst = new List<T>();
            Delegate setDelegate;

            foreach (DataRow dr in dt.Rows)
            {
                T model = (T)Activator.CreateInstance(typeof(T));
                foreach (DataColumn dc in dt.Columns)
                {
                    var pi = model.GetType().GetProperties().FirstOrDefault(p => p.Name.ToLower() == dc.ColumnName.ToLower());
                    if (pi != null && pi.CanWrite)
                    {
                        object value = dr[pi.Name];
                        if (value != DBNull.Value)
                        {
                            if (pi.PropertyType.IsEnum)
                            {
                                setDelegate = CreateSetDelegate(pi, model, dc.ColumnName);
                                //这里改变类型
                                setDelegate.DynamicInvoke(Enum.Parse(pi.PropertyType, value.ToString().Trim(), true));
                            }
                            else if (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                setDelegate = CreateSetDelegate(pi, model, dc.ColumnName);
                                //这里改变类型
                                setDelegate.DynamicInvoke(Convert.ChangeType(value, Nullable.GetUnderlyingType(pi.PropertyType)));
                            }
                            else
                            {
                                setDelegate = CreateSetDelegate(pi, model, dc.ColumnName);
                                //这里改变类型
                                setDelegate.DynamicInvoke(Convert.ChangeType(value, GetPropertyType(dc.ColumnName)));
                            }
                        }
                    }
                }
                lst.Add(model);
            }

            return lst;
        }

        private static Delegate CreateSetDelegate(PropertyInfo pi,T model, string propertyName)
        {
            MethodInfo mi = pi.GetSetMethod();
            //这里构造泛型委托类型
            Type delType = typeof(SetValue<>).MakeGenericType(GetPropertyType(propertyName));

            return Delegate.CreateDelegate(delType, model, mi);
        }

        private static Type GetPropertyType(string propertyName)
        {
            return typeof(T).GetProperties().FirstOrDefault(p => p.Name.ToLower() == propertyName.ToLower()).PropertyType;
        }
    }
}
