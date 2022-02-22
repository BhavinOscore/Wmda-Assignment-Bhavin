using Newtonsoft.Json.Converters;

namespace WMDAApi.Utils
{
    public class JsonDateFormatConverter : IsoDateTimeConverter
    {
        public JsonDateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
