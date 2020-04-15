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
        public HasCoolerEnum HasCoolerEnum { get; set; }
        public AutomobileTypeEnum IsBusEnum { get; set; }
    }
}
