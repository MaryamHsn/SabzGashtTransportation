using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.Enumration;

namespace SabzGashtTransportation.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class DriverRoutViewModel
    {
        public int? RegionId { get; set; }
        public string RoutRegionName { get; set; }
        public DateTime? RoutStartDate { get; set; }
        [RegularExpression(@"^$|^((1[34][0-9][0-9] |[0-9][0-9])(\/|\-)([0 ۰]{0,1}[۱-۶ 1-6])(\/|\-)([0 ۰]{0,1}[۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}(\/|\-)([۰0]{0,1}[۷-۹ 7-9]|[1۱][۰۱۲012])(\/|\-)([۰0]{0,1}[1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "تاریخ وارد شده نامعتبر است.فرمت صحیح به صورت سال/ماه/روز است")]
        public string RoutStartDateString { get; set; }
        public int? DriverId { get; set; }
        public string DriverFullName { get; set; }
        public int? RoutId { get; set; }
        public int? RoutShiftType { get; set; }
        public ShiftTypeEnum ShiftTypeEnum { get; set; }
        public int? RoutTransactionType { get; set; }//عادی- نیمراه تک-
        public RoutTransactionTypeEnum RoutTransactionTypeEnum { get; set; }
        [RegularExpression(@"^$|^((1[34][0-9][0-9] |[0-9][0-9])(\/|\-)([0 ۰]{0,1}[۱-۶ 1-6])(\/|\-)([0 ۰]{0,1}[۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}(\/|\-)([۰0]{0,1}[۷-۹ 7-9]|[1۱][۰۱۲012])(\/|\-)([۰0]{0,1}[1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "تاریخ وارد شده نامعتبر است.فرمت صحیح به صورت سال/ماه/روز است")]
        public string RoutEnterTimeString { get; set; }
        public string Phone1 { get; set; }
        public int? Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        [RegularExpression(@"^$|^((1[34][0-9][0-9] |[0-9][0-9])(\/|\-)([0 ۰]{0,1}[۱-۶ 1-6])(\/|\-)([0 ۰]{0,1}[۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}(\/|\-)([۰0]{0,1}[۷-۹ 7-9]|[1۱][۰۱۲012])(\/|\-)([۰0]{0,1}[1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "تاریخ وارد شده نامعتبر است.فرمت صحیح به صورت سال/ماه/روز است")]
        public string CreateDateString { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [RegularExpression(@"^$|^((1[34][0-9][0-9] |[0-9][0-9])(\/|\-)([0 ۰]{0,1}[۱-۶ 1-6])(\/|\-)([0 ۰]{0,1}[۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}(\/|\-)([۰0]{0,1}[۷-۹ 7-9]|[1۱][۰۱۲012])(\/|\-)([۰0]{0,1}[1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "تاریخ وارد شده نامعتبر است.فرمت صحیح به صورت سال/ماه/روز است")]
        public string ModifiedDateString { get; set; } 
        public IList<DriverViewModel> DriverList { get; set; }
        public IList<DriverViewModel> AllocateDriverListRegion { get; set; }
        public IList<DriverViewModel> RemailDriverListRegion { get; set; }
        public IList<DriverViewModel> AllocateDriverListOtherRegion { get; set; }
        public IList<DriverViewModel> RemailDriverListOtherRegion { get; set; }


        //public List<DriverViewModel>  DriverListRegion { get; set; }
        //public List<DriverViewModel> DriverListOtherRegion { get; set; }
        public IList<RoutTbl> RoutList { get; set; }
        public DriverViewModel Driver { get; set; }
        public RoutTbl Rout { get; set; }
     
    }
}
