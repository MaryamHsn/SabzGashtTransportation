using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.Enumration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SabzGashtTransportation.ViewModel
{
  
    public class LogDriverRoutViewModel
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public bool HasDelay { get; set; }
        public decimal? FinePrice { get; set; } 
        public DateTime DoDate { get; set; }
        [RegularExpression(@"^$|^((1[34][0-9][0-9] |[0-9][0-9])(\/|\-)([0 ۰]{0,1}[۱-۶ 1-6])(\/|\-)([0 ۰]{0,1}[۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}(\/|\-)([۰0]{0,1}[۷-۹ 7-9]|[1۱][۰۱۲012])(\/|\-)([۰0]{0,1}[1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "تاریخ وارد شده نامعتبر است.فرمت صحیح به صورت سال/ماه/روز است")]
        public string DoDateString { get; set; }    
        public int? DriverRoutId { get; set; }
        public int? DriverId { get; set; }
        public string DriverFullName { get; set; } 
        public int?  RoutId { get; set; } 
        public string RoutRegionName { get; set; }
        public DateTime? RoutStartDate { get; set; }
        [RegularExpression(@"^$|^((1[34][0-9][0-9] |[0-9][0-9])(\/|\-)([0 ۰]{0,1}[۱-۶ 1-6])(\/|\-)([0 ۰]{0,1}[۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}(\/|\-)([۰0]{0,1}[۷-۹ 7-9]|[1۱][۰۱۲012])(\/|\-)([۰0]{0,1}[1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "تاریخ وارد شده نامعتبر است.فرمت صحیح به صورت سال/ماه/روز است")]
        public string RoutStartDateString { get; set; }
        public int? RoutIsTemporary { get; set; }
        public int? RoutShiftType { get; set; }
        public int? RoutTransactionType { get; set; }//عادی- نیمراه تک-
        public RoutTransactionTypeEnum RoutTransactionTypeEnum { get; set; }
        public string RoutEnterTimeString { get; set; }
        public TimeSpan RoutEnterTime { get; set; }
        public  DriverRoutTbl DriverRoutTbl { get; set; }
        public  DriverTbl DriverTbl { get; set; }
        public  RoutTbl RoutTbl { get; set; }
        public IEnumerable<DriverTbl> DriverTblList { get; set; }
        public IEnumerable<RoutTbl> RoutTblList { get; set; } 
        public WorkDoneEnum WorkDoneEnum { get; set; }
        public WorkTemporaryEnum WorkTemporaryEnum { get; set; }

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
    }
}
