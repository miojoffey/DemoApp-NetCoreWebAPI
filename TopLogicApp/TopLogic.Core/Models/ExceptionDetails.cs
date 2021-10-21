using System;

namespace TopLogic.Core.Models
{
    public class ExceptionDetails
    {
        public int StatusCode { get; set; }
        public string ExceptionType { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime DateRecorded { get; set; }
    }
}
