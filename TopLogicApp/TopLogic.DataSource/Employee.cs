using System;
using System.Collections.Generic;

#nullable disable

namespace TopLogic.DataSource
{
    public partial class Employee
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsMale { get; set; }
        public string MaritalStatus { get; set; }
        public decimal Salary { get; set; }
    }
}
