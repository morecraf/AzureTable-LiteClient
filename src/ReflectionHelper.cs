using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace Dotissi.AzureTable.LiteClient
{
    static class ReflectionHelper
    {
        public static string GetPropertyStringValue(object entity, string propName)
        {
            TypeInfo typeInfo = entity.GetType().GetTypeInfo();
            var pinfo = typeInfo.GetDeclaredProperty(propName);
            //TODO better exceptio
            if (pinfo == null)
                throw new Exception(string.Format("Entity does not have '{0}' property defined", propName));
            return pinfo.GetValue(entity).ToString();

        }
        public static Dictionary<string,Type> GetPropertiesTypes(object entity)
        {
            TypeInfo typeInfo = entity.GetType().GetTypeInfo();
            Dictionary<string, Type> dict = new Dictionary<string, Type>();
            foreach (var pinfo in typeInfo.DeclaredProperties)
            {
                dict.Add(pinfo.Name, pinfo.PropertyType);
            }
            return dict;
        }
       
    }
}
