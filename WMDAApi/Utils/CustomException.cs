using System;
using System.Collections.Generic;

namespace WMDAApi.Utils
{
    public class CustomException : Exception
    {
        public List<ErrorData> Errors { get; } = new List<ErrorData>();
        public CustomException(ErrorData error) : base(error.Message)
        {
            Errors.Add(error);
        }
        public CustomException(List<ErrorData> errors)
        {
            Errors = errors;
        }
    }
}
