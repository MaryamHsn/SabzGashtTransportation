using Sabz.DomainClasses.DTO;

namespace Sabz.ServiceLayer.ViewModel
{
    using System;
    using System.Collections.Generic; 
    public class DriverRoutViewModel
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
        public virtual List<DriverTbl> DriverList{ get; set; }
        public virtual List<RoutTbl> RoutList { get; set; }

        //public virtual ICollection<LogRoutDriverTbl> LogRoutDriverTbls { get; set; }
    }
}