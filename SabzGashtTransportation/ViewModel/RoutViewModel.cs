using System.Collections.Generic;
using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.Enumration;

namespace SabzGashtTransportation.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RoutViewModel
    {
        public int RoutID { get; set; } 
        public int ShiftType { get; set; }
        public string ShiftTypeString { get; set; }
        public ShiftTypeEnum ShiftTypeEnum { get; set; }
        public int RoutTransactionType { get; set; } 
        public RoutTransactionTypeEnum RoutTransactionTypeEnum { get; set; }
        public TimeSpan EnterTime { get; set; }
        public DateTime StartDate { get; set; }
        [RegularExpression(@"^$|^((1[34][0-9][0-9] |[0-9][0-9])(\/|\-)([0 ۰]{0,1}[۱-۶ 1-6])(\/|\-)([0 ۰]{0,1}[۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}(\/|\-)([۰0]{0,1}[۷-۹ 7-9]|[1۱][۰۱۲012])(\/|\-)([۰0]{0,1}[1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "تاریخ وارد شده نامعتبر است.فرمت صحیح به صورت سال/ماه/روز است")]
        public string StartDateString { get; set; }
        public int? RegionId { get; set; }
        public string RegionName { get; set; }
        public int? AutomobileTypeId { get; set; }
        public int? HasCooler { get; set; }
        public int? IsBus { get; set; }
        public int? Count { get; set; }
        public int? Allocate { get; set; }
        public int? RemainAllocate { get; set; }
        public bool IsActive { get; set; }
        public int? RoutTransactionSingleBus { get; set; }
        public int? RoutTransactionSingleMiniBus { get; set; }
        public int? RoutTransactionSingleHasCoolerBus { get; set; }
        public int? RoutTransactionSingleHasCoolerMiniBus { get; set; }
        public int? RoutTransactionRegularBus { get; set; }
        public int? RoutTransactionRegularMiniBus { get; set; }
        public int? RoutTransactionRegularHasCoolerBus { get; set; }
        public int? RoutTransactionRegularHasCoolerMiniBus { get; set; }
        public int? RoutTransactionThereeFourBus { get; set; }
        public int? RoutTransactionThereeFourMiniBus { get; set; }
        public int? RoutTransactionThereeFourHasCoolerBus { get; set; }
        public int? RoutTransactionThereeFourHasCoolerMiniBus { get; set; }
        public int? RoutTransactionFiveSevenBus { get; set; }
        public int? RoutTransactionFiveSevenMiniBus { get; set; }
        public int? RoutTransactionFiveSevenHasCoolerBus { get; set; }
        public int? RoutTransactionFiveSevenHasCoolerMiniBus { get; set; }
        public AutomobileTypeTbl AutomobileTypeTbl { get; set; }
        public ICollection<RegionTbl> RegionTblList { get; set; }
        public AutomobileTypeEnum IsBusEnum { get; set; }
        public HasCoolerEnum HasCoolerEnum { get; set; }
    }
}
