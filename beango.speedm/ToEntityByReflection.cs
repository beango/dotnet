using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace beango.speedm
{
    public class ToEntityByReflection<T>
    {
        public List<T> GetEntity(DataTable dt)
        {
            List<T> lst = new List<T>();
            Type type = typeof(T);

            foreach (DataRow dr in dt.Rows)
            {
                T model = (T)Activator.CreateInstance(typeof(T));
                foreach (PropertyInfo pi in type.GetProperties())
                {
                    if (!pi.CanWrite)
                        continue;
                    object value = dr[pi.Name];
                    if (value != DBNull.Value)
                    {
                        if (pi.PropertyType.IsEnum)
                        {
                            pi.SetValue(model, Enum.Parse(pi.PropertyType, value.ToString().Trim(), true), null);
                        }
                        else if (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            pi.SetValue(model, Convert.ChangeType(value, System.Nullable.GetUnderlyingType(pi.PropertyType)), null);
                        }
                        else
                        {
                            pi.SetValue(model, Convert.ChangeType(value, pi.PropertyType), null);
                        }
                    }
                }
                lst.Add(model);
            }
            return lst;
        }
    }
}
