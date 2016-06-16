using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dotissi.AzureTable.LiteClient.Exceptions
{
    class StorageException:Exception
    {
        public StorageException(string message):base(message)
        {

        }
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorDescription { get; set; }
    }
}
