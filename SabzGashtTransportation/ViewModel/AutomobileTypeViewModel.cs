using Sabz.ServiceLayer.Enumration;

namespace SabzGashtTransportation.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations; 
    public class AutomobileTypeViewModel
    {  
        public int AutoTypeId { get; set; }
        public bool HasCooler { get; set; }
        public string Description { get; set; }
        public int? IsBus { get; set; }
        public bool IsActive { get; set; }
        public DateTime CFDate { get; set; }
        public string CFDateString { get; set; }
        public DateTime LFDate { get; set; }
        public string LFDateString { get; set; }

        public HasCoolerEnum HasCoolerEnum { get; set; }

        public AutomobileTypeEnum IsBusEnum { get; set; }
        //public virtual ICollection<AutomobileTbl> AutomobileTbls { get; set; }
        //public virtual ICollection<RoutTbl> RoutTbls { get; set; }
    }
}
