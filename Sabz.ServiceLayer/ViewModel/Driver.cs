using Sabz.DomainClasses.DTO;

namespace Sabz.ServiceLayer.ViewModel
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
    //    public DateTime CFDate { get; set; }
    //    public string CFDateString { get; set; }
    //    public DateTime LFDate { get; set; }
    //    public string lFDateString { get; set; }
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
    public   class Driver
    {
        public int DriverId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string NationalCode { get; set; }
        public string LicenceCode { get; set; }

        public DateTime? BirthDate { get; set; }

        public int AutomobileId { get; set; }

        public string Address { get; set; }

        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public bool IsActive { get; set; }
        public DateTime CFDate { get; set; }
        public DateTime LFDate { get; set; }
         public virtual ICollection<AccidentTbl> AccidentTbls { get; set; }

        public virtual AutomobileTbl AutomobileTbl { get; set; }

         public virtual ICollection<DriverRoutTbl> DriverRoutTbls { get; set; }

         public virtual ICollection<RepairmentTbl> RepairmentTbls { get; set; }

          public virtual ICollection<PaymentTbl> PaymentTbls { get; set; }
    }
}
