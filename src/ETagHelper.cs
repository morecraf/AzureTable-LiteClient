using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotissi.AzureTable.LiteClient
{
    public static class ETagHelper
    {
        public static DateTimeOffset ParseETagForTimestamp(string etag)
        {
            if (etag.StartsWith("W/", StringComparison.Ordinal))
            {
                etag = etag.Substring(2);
            }
            string prefix = "\"datetime'";
            etag = etag.Substring(prefix.Length, etag.Length - 2 - prefix.Length);
            return DateTimeOffset.Parse(Uri.UnescapeDataString(etag), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
        }
        public static string GetETagFromTimestamp(string timeStampString)
        {
            timeStampString = Uri.EscapeDataString(timeStampString);
            return "W/\"datetime'" + timeStampString + "'\"";
        }
       
        public static string GetETagFromTimestamp(DateTimeOffset timeStamp)
        {
            var timeStampString= timeStamp.ToString("R", CultureInfo.InvariantCulture);
            return GetETagFromTimestamp(timeStampString);
        }
    }
}
