using System.Collections.Generic;
using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.Enumration;

namespace SabzGashtTransportation.ViewModel
{
    using System;  
    public class RoutViewModel
    {
        public int RoutID { get; set; }
        public string Name { get; set; }
        public int ShiftType { get; set; }
        public int RoutTransactionType { get; set; }
        public TimeSpan EnterTime { get; set; }
        public string EnterTimeString { get; set; }
        public TimeSpan ExitTime { get; set; }
        public string ExitTimeString { get; set; }
        public DateTime StartDate { get; set; }
        public string StartDateString { get; set; }
        public DateTime EndDate { get; set; }
        public string EndDateString { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public int AutomobileTypeId { get; set; }
        public int HasCooler { get; set; }
        public int IsBus { get; set; }
        public decimal AgreementPrice { get; set; }
        public int Count { get; set; }
        public int Allocate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateString { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedDateString { get; set; }

        public  AutomobileTypeTbl AutomobileTypeTbl { get; set; }
        public ICollection<RegionTbl> RegionTblList { get; set; }
        public AutomobileTypeEnum IsBusEnum { get; set; }
        public HasCoolerEnum HasCoolerEnum { get; set; }

        //public virtual ICollection<DriverRoutTbl> DriverRoutTbls { get; set; }
        //public virtual RegionTbl RegionTbl { get; set; }
    }
}
