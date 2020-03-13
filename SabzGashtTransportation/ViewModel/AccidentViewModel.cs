using System.Web.Mvc;
using Sabz.DomainClasses.DTO;

namespace SabzGashtTransportation.ViewModel
{
    using System;
    using System.Collections.Generic;

    public class AccidentViewModel
    {
        #region  useMapper
        public int AccidentId { get; set; }
        public int? UseInsurence { get; set; }
        public decimal? Cost { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateDateString { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedDateString { get; set; }
        public int DriverId { get; set; }
        public string DriverFirstName { get; set; }
        public string DriverLastName { get; set; }
        public string DriverFullName { get; set; }
        public int AutomobileId { get; set; }
        public string AutomobileNumber { get; set; }
        public string AutomobileShasi { get; set; }
        public string HasCooler { get; set; }

        public DriverTbl SelectedDriver { get; set; }
        public IEnumerable<DriverTbl> Drivers { get; set; }
        public AutomobileTbl SelectedAutomobile { get; set; }
        public IEnumerable<AutomobileTbl> Automobiles { get; set; }

        //public List<DriverTbl> DriverTblList { get; set; }
        //public virtual DriverTbl DriverTbl { get; set; }
        //public List<AutomobileTbl> AutomobileTblList { get; set; }
        //public virtual AutomobileTbl AutomobileTbl { get; set; }
        #endregion

        #region collect
        //public AccidentTbl Accident { get; set; }
        //public DriverTbl Driver{ get; set; }
        //public AutomobileTbl  Automobile  { get; set; }
        //public ICollection<AccidentTbl> Accidents{ get; set; }
        //public ICollection<DriverTbl> Drivers { get; set; }
        //public ICollection<AutomobileTbl> Automobiles { get; set; }
        #endregion

    }
}
