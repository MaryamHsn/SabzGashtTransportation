using Sabz.DomainClasses.DTO;
using System;
using System.Collections.Generic;

namespace SabzGashtTransportation.ViewModel
{
    public   class DriverViewModel
    {
        public int DriverId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string FullName { get; set; }
        public string NationalCode { get; set; }
        public string LicenceCode { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthDateString { get; set; }
        public int AutomobileId { get; set; }
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public bool IsSelected { get; set; }
        //public virtual ICollection<AccidentTbl> Accidents { get; set; }
        //public  AutomobileTbl Automobile { get; set; }
        public IEnumerable<AutomobileTbl >Automobiles { get; set; }
        //public IEnumerable<DriverRoutTbl> DriverRouts { get; set; }
        // public IEnumerable<RepairmentTbl> Repairments { get; set; }
        //  public IEnumerable<PaymentTbl> Payments{ get; set; }
    }
}
