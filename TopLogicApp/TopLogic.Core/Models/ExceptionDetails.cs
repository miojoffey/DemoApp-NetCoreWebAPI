using System;

namespace TopLogic.Core.Models
{
    public class ExceptionDetails
    {
        public string ExceptionType { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime DateRecorded { get; set; }
    }
}
