using Sabz.DomainClasses.DTO;

namespace SabzGashtTransportation.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //public   class Driver
    //{  
    //    public int DriverId { get; set; } 
    //    public string FirstName { get; set; } 
    //    public string LastName { get; set; } 
    //    public string FatherName { get; set; }
    //    public string NationalCode { get; set; }
    //    public string LicenceCode { get; set; }
    //    public DateTime? BirthDate { get; set; }
    //    public int AutomobileId { get; set; }

    //    public string Address { get; set; } 
    //    public string Phone1 { get; set; }
    //    public string Phone2 { get; set; }
    //    public bool IsActive { get; set; }
    //    public DateTime CreatedDate { get; set; }
    //    public string CreatedDateString { get; set; }
    //    public DateTime .ModifiedDate { get; set; }
    //    public string .ModifiedDateString { get; set; }
    //    public virtual Automobile AutomobileTbl { get; set; }
    //    public string Number { get; set; }
    //    public string Shasi { get; set; }
    //    public string CreateYear { get; set; }
    //    public string HasCooler { get; set; }
    //    public virtual List<Automobile> AutomobileList{ get; set; }

    //    // public virtual ICollection<AccidentTbl> AccidentTbls { get; set; }
    //    // public virtual ICollection<DriverRoutTbl> DriverRoutTbls { get; set; }
    //    //  public virtual ICollection<RepairmentTbl> RepairmentTbls { get; set; }
    //    //  public virtual ICollection<PaymentTbl> PaymentTbls { get; set; }
    //}
    public   class DriverViewModel
    {
        public int DriverId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string NationalCode { get; set; }
        public string LicenceCode { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthDateString { get; set; }
        public int AutomobileId { get; set; }
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateString { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedDateString { get; set; }

        public virtual ICollection<AccidentTbl> Accidents { get; set; }

        public  AutomobileTbl Automobile { get; set; }
        public IEnumerable<AutomobileTbl >Automobiles { get; set; }

        public IEnumerable<DriverRoutTbl> DriverRouts { get; set; }

         public IEnumerable<RepairmentTbl> Repairments { get; set; }

          public IEnumerable<PaymentTbl> Payments{ get; set; }
    }
}
