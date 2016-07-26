using Dotissi.AzureTable.LiteClient.Crypto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Dotissi.AzureTable.LiteClient.Http
{
    internal class Signature
    {
       
        private const string TimestampHeaderName = "x-ms-date";
        private const string AuthorizationHeaderName = "Authorization";
        private const string AuthenticationType = "SharedKeyLite";
        string account;
        string key;
        public Signature(string account, string key)
        {
            this.account = account;
            this.key = key ;
        }
        public void SignMessage(HttpRequestMessage message)
        {
            string date = DateTime.UtcNow.ToString("R", CultureInfo.InvariantCulture);
            message.Headers.Add(TimestampHeaderName, date);
            message.Headers.Add("Accept", "application/json;odata=minimalmetadata");
            message.Headers.Add("x-ms-version", "2015-04-05");
            string signature = MakeSignature(message,date);
            string authorizationString = string.Format("{0} {1}:{2}", AuthenticationType, account, signature);
            message.Headers.Add(AuthorizationHeaderName, authorizationString);
        }
        private string MakeSignature(HttpRequestMessage request,string date)
        {
            var resource = request.RequestUri.PathAndQuery;
            if (resource.Contains("?"))
            {
                resource = resource.Substring(0, resource.IndexOf("?"));
            }
            var signatureString =String.Format("{0}\n/{1}{2}", date, account,resource) ;
            HmacSha256 hmac = new HmacSha256(Convert.FromBase64String(key));
            return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(signatureString)));
        }
       
    }
}
