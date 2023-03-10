namespace Sabz.DomainClasses.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RoutTbl")]
    public partial class RoutTbl : BaseEntity<int>
    {
        public RoutTbl()
        {
            DriverRoutTbls = new HashSet<DriverRoutTbl>();
        }
        public int ShiftType { get; set; }
        public int RoutTransactionType { get; set; }//عادی- نیمراه تک-
        public TimeSpan EnterTime { get; set; } 
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; } 
        [ForeignKey("RegionTbl")]
        public int RegionId { get; set; }
        [ForeignKey("AutomobileTypeTbl")]
        public int AutomobileTypeId { get; set; } 
        public int Count { get; set; }

        public virtual ICollection<DriverRoutTbl> DriverRoutTbls { get; set; }
        public virtual RegionTbl RegionTbl { get; set; }
        public virtual AutomobileTypeTbl AutomobileTypeTbl { get; set; }

    }
}
