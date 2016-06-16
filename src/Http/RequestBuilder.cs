using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dotissi.AzureTable.LiteClient.Http
{
    class RequestBuilder
    {
        string uriBase;
        public RequestBuilder(string uri)
        {
            this.uriBase = string.Format(CultureInfo.InvariantCulture, "{0}", uri);

        }

        public HttpRequestMessage BuildGetRequest(string endUriFragment, Dictionary<string, string> parameters)
        {
            HttpRequestMessage messageReq = new HttpRequestMessage();
            messageReq.Method = HttpMethod.Get;
            string uriFragment = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", uriBase, endUriFragment);


            string queryString = GetQueryString(parameters);

            messageReq.RequestUri = new Uri(CombinePathAndQuery(uriFragment, queryString));
            return messageReq;
        }

        public HttpRequestMessage BuildSubmitRequest(string endUriFragment, object content,HTTPSubmitType type)
        {
            HttpRequestMessage messageReq = new HttpRequestMessage();
            messageReq.Method = new HttpMethod(type.ToString());
            string uriFragment = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", uriBase, endUriFragment);
            JObject jobj= JObject.FromObject(content);
            var propTypes = ReflectionHelper.GetPropertiesTypes(content);
            foreach (var key in propTypes.Keys)
            {
                if (propTypes[key] == typeof(Int64))
                {
                    jobj[key+ "@odata.type"]= "Edm.Int64";
                    jobj[key] = jobj[key].ToString();
                }
                else if (propTypes[key] == typeof(DateTime) || propTypes[key] == typeof(DateTimeOffset))
                {
                    jobj[key + "@odata.type"] = "Edm.DateTime";
                }
                else if (propTypes[key] == typeof(Guid))
                {
                    jobj[key + "@odata.type"] = "Edm.Guid";
                }
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(jobj);
            StringContent contentReq = new StringContent(json, Encoding.UTF8, "application/json");
            messageReq.Content = contentReq;
            messageReq.RequestUri = new Uri(uriFragment);

            return messageReq;
        }


        public HttpRequestMessage BuildDeleteRequest(string endUriFragment)
        {
            HttpRequestMessage messageReq = new HttpRequestMessage();
            messageReq.Method = HttpMethod.Delete;
            string uriFragment = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", uriBase, endUriFragment);

            messageReq.RequestUri = new Uri(uriFragment);
            return messageReq;
        }

        public static string GetQueryString(IDictionary<string, string> parameters)
        {
            string parametersString = null;

            if (parameters != null && parameters.Count > 0)
            {
                parametersString = "";
                string formatString = "{0}={1}";
                foreach (var parameter in parameters)
                {
                    string escapedKey = parameter.Key;
                    string escapedValue = Uri.EscapeDataString(parameter.Value);
                    parametersString += string.Format(CultureInfo.InvariantCulture,
                                                      formatString,
                                                      escapedKey,
                                                      escapedValue);
                    formatString = "&{0}={1}";
                }
            }

            return parametersString;
        }
        public static string CombinePathAndQuery(string path, string queryString)
        {
            if (!string.IsNullOrEmpty(queryString))
            {
                path = string.Format(CultureInfo.InvariantCulture, "{0}?{1}", path, queryString.TrimStart('?'));
            }

            return path;
        }
    }
}
