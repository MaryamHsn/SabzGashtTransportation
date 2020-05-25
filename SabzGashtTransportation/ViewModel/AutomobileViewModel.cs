using System.ComponentModel.DataAnnotations;
using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.Enumration;

namespace SabzGashtTransportation.ViewModel
{
    using System;
    using System.Collections.Generic;
    public class AutomobileViewModel
    {
        public int AutoId { get; set; }
        [Required(ErrorMessage = "پر کردن مقدار اجباری است")]
        public string Number { get; set; }
        [Required(ErrorMessage = "پر کردن مقدار اجباری است")]
        public string CreateYear { get; set; }
        public int? AutomobileTypeId { get; set; }
        public int? HasCooler { get; set; }
        public int? IsBus { get; set; }
        public  AutomobileTypeTbl AutomobileType { get; set; }
        public AutomobileTypeEnum IsBusEnum { get; set; }
        public HasCoolerEnum HasCoolerEnum { get; set; }
    }
}
