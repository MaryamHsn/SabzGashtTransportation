using System.ComponentModel.DataAnnotations;
using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.Enumration;

namespace SabzGashtTransportation.ViewModel
{
    using System;
    using System.Collections.Generic;
    public class AutomobileViewModel
    {
        public int AutoId { get; set; }
        public string Number { get; set; }
        public string Shasi { get; set; }
        public string CreateYear { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateString { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedDateString { get; set; }
        public int? AutomobileTypeId { get; set; }
        public int HasCooler { get; set; }
        public int IsBus { get; set; }
        public  List<AutomobileTypeTbl> AutomobileTypeList { get; set; }
        public  AutomobileTypeTbl AutomobileType { get; set; }
        public AutomobileTypeEnum IsBusEnum { get; set; }
        public HasCoolerEnum HasCoolerEnum { get; set; }

        // public virtual AutomobileTypeTbl AutomobileTypeTbl { get; set; } 
        //public virtual ICollection<DriverTbl> DriverTbls { get; set; }
        //public virtual ICollection<AccidentTbl> AccidentTbls { get; set; }
    }
}
