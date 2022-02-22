namespace WMDAApi.Utils
{
    public class ErrorData
    {
        public ErrorData()
        {

        }
        public ErrorData(string code, string message)
        {
            Code = code;
            Message = message;
        }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
