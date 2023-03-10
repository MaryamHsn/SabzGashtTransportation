using System;
using System.Collections.Generic;
using Sabz.DomainClasses.DTO;

namespace SabzGashtTransportation.ViewModel
{ 
    public class PaymentViewModel
    { 
        public int PaymentId { get; set; }
        public decimal? Insurance { get; set; }
        public decimal? PreHelpCost { get; set; }
        public decimal? Fine { get; set; }
        public decimal? Tax { get; set; }
        public decimal? AccidentCost { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedDateString { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedDateString { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateDateString { get; set; }
        public int? DriverId { get; set; }
        public string DriverFirstName { get; set; }
        public string DriverLastName { get; set; }
        public string DriverFullName { get; set; }
        public string DriverPhone { get; set; }
        public IEnumerable<DriverTbl> Drivers{ get; set; }
    }
}
