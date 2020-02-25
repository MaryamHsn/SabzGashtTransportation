using System; 

namespace Sabz.ServiceLayer.ViewModel
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
        public DateTime CFDate { get; set; }
        public string CFDateString { get; set; }
        public DateTime LFDate { get; set; }
        public string LFDateString { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateDateString { get; set; }
        public int? DriverId { get; set; }
        public string DriverFirstName { get; set; }
        public string DriverLastName { get; set; }
        public string DriverPhone { get; set; }

    }
}
