using Sabz.DomainClasses.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SabzGashtTransportation.ViewModel
{
    public   class DriverViewModel
    {
        public int DriverId { get; set; }
        [Required(ErrorMessage = "پر کردن مقدار اجباری است")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string FullName { get; set; }
        public string BankAccountNumber { get; set; }
        public string NationalCode { get; set; }
        public string LicenceCode { get; set; }
        public DateTime? BirthDate { get; set; }
        [RegularExpression(@"^$|^((1[34][0-9][0-9] |[0-9][0-9])(\/|\-)([0 ۰]{0,1}[۱-۶ 1-6])(\/|\-)([0 ۰]{0,1}[۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}(\/|\-)([۰0]{0,1}[۷-۹ 7-9]|[1۱][۰۱۲012])(\/|\-)([۰0]{0,1}[1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "تاریخ وارد شده نامعتبر است.فرمت صحیح به صورت سال/ماه/روز است")]
        public string BirthDateString { get; set; }
        public int? AutomobileId { get; set; }
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public bool IsSelected { get; set; } 
        public IEnumerable<AutomobileTbl >Automobiles { get; set; } 
    }
}
