using Dotissi.AzureTable.LiteClient.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dotissi.AzureTable.LiteClient
{
    public class AzureTableClient
    {

        string uri;
        string accountName;
        string accountKey;
        public AzureTableClient(string accountName,string accountKey)
        {
            this.accountName = accountName;
            this.accountKey = accountKey;
            this.uri = string.Format("https://{0}.table.core.windows.net", accountName);
        }
        public AzureTableClient(string uri, string accountName, string accountKey)
        {
            this.accountName = accountName;
            this.accountKey = accountKey;
            this.uri = uri;
        }
        public AzureTable GetTableReference(string tableName)
        {
            return new AzureTable(this.uri, this.accountName, this.accountKey, tableName);
        }
        
       
    }
}
