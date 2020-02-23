namespace Sabz.ServiceLayer.ViewModel
{
    using System;  
    public class Rout
    {
        public int RoutID { get; set; }
        public string Name { get; set; }
        public string ShiftType { get; set; }
        public TimeSpan EnterTime { get; set; }
        public string EnterTimeString { get; set; }
        public TimeSpan ExitTime { get; set; }
        public string ExitTimeString { get; set; }
        public DateTime? StartDate { get; set; }
        public string StartDateString { get; set; }
        public DateTime? EndTime { get; set; }
        public string EndTimeString { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public int AutomobileTypeId { get; set; }
        public bool HasCooler { get; set; }
        public decimal AgreementPrice { get; set; }
        public decimal DriverPrice { get; set; }
        public int Count { get; set; }
        public bool IsActive { get; set; }
        public DateTime CFDate { get; set; }
        public string CFDateString { get; set; }
        public DateTime LFDate { get; set; }
        public string LFDateString { get; set; }

        //public virtual AutomobileTypeTbl AutomobileTypeTbl { get; set; }
        //public virtual ICollection<DriverRoutTbl> DriverRoutTbls { get; set; }
        //public virtual RegionTbl RegionTbl { get; set; }
    }
}
