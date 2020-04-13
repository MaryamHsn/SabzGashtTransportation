using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.Enumration;
using System;
using System.Collections.Generic;

namespace SabzGashtTransportation.ViewModel
{
  
    public class LogDriverRoutViewModel
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public decimal? FinePrice { get; set; } 
        public DateTime DoDate { get; set; }
        public string DoDateString { get; set; }
        //public DateTime? CDate { get; set; }
        //public string CDateString { get; set; }
        //public DateTime? LDate { get; set; }
        //public string LDateString { get; set; }      
        public int DriverRoutId { get; set; }
        public int DriverId { get; set; }
        public string DriverFullName { get; set; }
        //public string DriverPhone { get; set; }
        public int  RoutId { get; set; }
        public string RoutName { get; set; }
        public string RoutRegionName { get; set; }
        public DateTime RoutStartDate { get; set; }
        public string RoutStartDateString { get; set; }
        public int? RoutIsTemporary { get; set; }
        public int RoutShiftType { get; set; }
        public int RoutTransactionType { get; set; }//عادی- نیمراه تک-
        public RoutTransactionTypeEnum RoutTransactionTypeEnum { get; set; }
        public string RoutEnterTimeString { get; set; }
        public string RoutExitTimeString { get; set; }
        public  DriverRoutTbl DriverRoutTbl { get; set; }
        public  DriverTbl DriverTbl { get; set; }
        public  RoutTbl RoutTbl { get; set; }
        public IEnumerable<DriverTbl> DriverTblList { get; set; }
        public IEnumerable<RoutTbl> RoutTblList { get; set; }
        //public LogDriverRoutTbl LogDriverRoutTbl { get; set; }
        //public IEnumerable<LogDriverRoutTbl> LogDriverRoutTblList { get; set; }
        public WorkDoneEnum WorkDoneEnum { get; set; }
        public WorkTemporaryEnum WorkTemporaryEnum { get; set; }
    }
}
