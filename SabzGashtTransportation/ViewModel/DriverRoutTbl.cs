namespace SabzGashtTransportation.ViewModel
{
    using System;
    using System.Collections.Generic; 
    public class DriverRoutTbl
    { 
        public int Id { get; set; }

        public int DriverId { get; set; }
        public string DriverFirstName { get; set; }
        public string DriverLastName { get; set; }

        public int RoutId { get; set; }
        public string RoutName { get; set; }
        public string RoutShiftType { get; set; }
        public string RoutEnterTimeString { get; set; }
        public string RoutExitTimeString { get; set; }
        public int IsTemporary { get; set; }
        public bool IsActive { get; set; }
        public DateTime CFDate { get; set; }
        public string CFDateString { get; set; }
        public DateTime LFDate { get; set; }
        public string LFDateString { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone1 { get; set; } 
        //public virtual ICollection<LogRoutDriverTbl> LogRoutDriverTbls { get; set; }
    }
}
