using System;

namespace TopLogicApp.API.DTO
{
    public class EmployeeDTO
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
