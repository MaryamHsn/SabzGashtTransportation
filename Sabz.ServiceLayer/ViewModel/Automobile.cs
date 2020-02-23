using System.ComponentModel.DataAnnotations;
using Sabz.DomainClasses.DTO;

namespace Sabz.ServiceLayer.ViewModel
{
    using System;
    using System.Collections.Generic;
    public class Automobile
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
        public string HasCooler { get; set; }
        public string IsBus { get; set; }
        public virtual List<AutomobileType> AutomobileTypeList { get; set; }

        // public virtual AutomobileTypeTbl AutomobileTypeTbl { get; set; } 
        //public virtual ICollection<DriverTbl> DriverTbls { get; set; }
        //public virtual ICollection<AccidentTbl> AccidentTbls { get; set; }
    }
}
