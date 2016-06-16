using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotissi.AzureTable.LiteClient
{
    public class TableResult
    {
        public string ETag { get; set; }
        public int HttpStatusCode { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
