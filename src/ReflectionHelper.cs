using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Dynamic;

namespace Dotissi.AzureTable.LiteClient
{
    static class ReflectionHelper
    {
        public static string GetPropertyStringValue(object entity, string propName)
        {
            Type t = entity.GetType();
            if (t == typeof(ExpandoObject))
            {
                var dict = (IDictionary<String, Object>)entity;
                if (!dict.ContainsKey(propName))
                {
                    throw new Exception(string.Format("Entity does not have '{0}' property defined", propName));
                }
                return dict[propName] as string;
            }
            else
            {
                TypeInfo typeInfo = t.GetTypeInfo();
                var pinfo = typeInfo.GetDeclaredProperty(propName);
                //TODO better exceptio
                if (pinfo == null)
                    throw new Exception(string.Format("Entity does not have '{0}' property defined", propName));
                return pinfo.GetValue(entity).ToString();
            }

        }
        public static Dictionary<string,Type> GetPropertiesTypes(object entity)
        {
            Type t = entity.GetType();
            Dictionary<string, Type> dict = new Dictionary<string, Type>();
            if (t == typeof(ExpandoObject))
            {
                var propsDict = (IDictionary<String, Object>)entity;
                foreach (var kv in propsDict)
                {
                    if (kv.Value != null)
                    {
                        dict.Add(kv.Key, kv.Value.GetType());
                    }
                }
            }
            else
            {
                TypeInfo typeInfo = t.GetTypeInfo();
                foreach (var pinfo in typeInfo.DeclaredProperties)
                {
                    dict.Add(pinfo.Name, pinfo.PropertyType);
                }
                
            }
            return dict;
        }
       
    }
}
