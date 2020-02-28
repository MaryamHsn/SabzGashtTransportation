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
        public DateTime CFDate { get; set; }
        public string CFDateString { get; set; }
        public DateTime LFDate { get; set; }
        public string LFDateString { get; set; }
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
