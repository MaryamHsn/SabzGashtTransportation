using Sabz.DomainClasses.DTO;

namespace SabzGashtTransportation.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema; 
    public   class RegionViewModel
    { 
        public int RegionId { get; set; }
        [Required(ErrorMessage = "پر کردن مقدار اجباری است")]
        public string RegionName { get; set; }      
     //   public  RoutTbl RoutTbl { get; set; }
    }
}
