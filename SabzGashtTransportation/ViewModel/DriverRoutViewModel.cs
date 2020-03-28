using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.Enumration;

namespace SabzGashtTransportation.ViewModel
{
    using System;
    using System.Collections.Generic; 
    public class DriverRoutViewModel
    { 
        public int Id { get; set; }

        public int DriverId { get; set; }
        public string DriverFirstName { get; set; }
        public string DriverLastName { get; set; }
        public string DriverFullName { get; set; }
        public int RoutId { get; set; }
        public string RoutName { get; set; }
        public string RoutShiftType { get; set; }
        public string RoutEnterTimeString { get; set; }
        public string RoutExitTimeString { get; set; }
        public int IsTemporary { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateString { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedDateString { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone1 { get; set; }
        public IEnumerable<DriverTbl> DriverList{ get; set; }
        public IEnumerable<RoutTbl> RoutList { get; set; }
        public DriverTbl Driver{ get; set; }
        public RoutTbl Rout{ get; set; }

        public RoutTypeEnum RoutTypeEnum { get; set; }
        //public virtual ICollection<LogDriverRoutTbl> LogDriverRoutTbls { get; set; }
    }
}
