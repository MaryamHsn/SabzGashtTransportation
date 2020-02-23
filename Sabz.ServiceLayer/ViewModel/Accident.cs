namespace Sabz.ServiceLayer.ViewModel
{
    using System;
    using System.Collections.Generic;

    public class Accident
    {
        public int AccidentId { get; set; }
        public int? UseInsurence { get; set; }
        public decimal? Cost { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CFDate { get; set; }
        public string CFDateString { get; set; }
        public DateTime LFDate { get; set; }
        public string LFDateString { get; set; }
        public int? DriverId { get; set; }
        public string DriverFirstName { get; set; }
        public string DriverLastName { get; set; }
        public int? AutomobileId { get; set; }
        public string AutomobileNumber { get; set; }
        public string AutomobileShasi{ get; set; }
        public string HasCooler { get; set; }
        public List<Driver> DriverTblList { get; set; }
        public virtual Driver DriverTbl { get; set; }
        public List<Automobile> AutomobileTblList { get; set; }
        public virtual Automobile AutomobileTbl { get; set; }

    }
}
