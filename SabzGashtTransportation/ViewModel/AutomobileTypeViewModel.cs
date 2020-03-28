using Sabz.ServiceLayer.Enumration;

namespace SabzGashtTransportation.ViewModel
{
    using Sabz.DomainClasses;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations; 
    public class AutomobileTypeViewModel
    {  
        public int Id { get; set; }
        public bool HasCooler { get; set; }
        public string Description { get; set; }
        public int? IsBus { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateString { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedDateString { get; set; }

        public HasCoolerEnum HasCoolerEnum { get; set; }

        public AutomobileTypeEnum IsBusEnum { get; set; }
        //public virtual ICollection<AutomobileTbl> AutomobileTbls { get; set; }
        //public virtual ICollection<RoutTbl> RoutTbls { get; set; }
    }
}
