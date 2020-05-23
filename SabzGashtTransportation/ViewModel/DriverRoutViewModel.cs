using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.Enumration;

namespace SabzGashtTransportation.ViewModel
{
    using System;
    using System.Collections.Generic; 
    public class DriverRoutViewModel
    { 
        public string RoutRegionName { get; set; }
        public DateTime RoutStartDate { get; set; }
        public string RoutStartDateString { get; set; }
        public int? DriverId { get; set; }
        public string DriverFullName { get; set; }
        public int? RoutId { get; set; }
        public string RoutName { get; set; }
        public int? RoutShiftType { get; set; }
        public ShiftTypeEnum ShiftTypeEnum { get; set; }
        public int? RoutTransactionType { get; set; }//عادی- نیمراه تک-
        public RoutTransactionTypeEnum RoutTransactionTypeEnum { get; set; }
        public string RoutEnterTimeString { get; set; }
        public string RoutExitTimeString { get; set; }
        public string Phone1 { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateDateString { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedDateString { get; set; }
        public IList<DriverViewModel> DriverList{ get; set; }
        public IList<DriverViewModel> AllocateDriverList{ get; set; }
        public IList<DriverViewModel> RemailDriverList{ get; set; }
        public IList<RoutTbl> RoutList { get; set; }
        public DriverViewModel Driver { get; set; }
        public RoutTbl Rout{ get; set; }
    }
}
