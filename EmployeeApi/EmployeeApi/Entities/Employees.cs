using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApi.Entities
{
    public class Employees
    {
        private string _employeeId;
        [Key]
        public string EmployeeId 
        {
            get {
                return _employeeId;
            }
            set { _employeeId = value; }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public int Age { get; set; }

        public virtual EmployeeAddress Address { get; set; }
    }

    public class EmployeeAddress
    {
        private string _addressId;
        [ForeignKey("Employees")]
        public string EmployeeId { get; set; }
        [Key]        
        public string AddressId
        {
            get
            {
                return _addressId;
            }
            set { _addressId = value; }
        }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int Zipcode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public virtual Employees Employees { get; set; }
    }  

   
}
