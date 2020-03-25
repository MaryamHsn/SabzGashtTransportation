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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RoutTbl()
        {
            DriverRoutTbls = new HashSet<DriverRoutTbl>();
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int ShiftType { get; set; }
        public int RoutTransactionType { get; set; }
        public TimeSpan EnterTime { get; set; }
        public TimeSpan? ExitTime { get; set; }
        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }
        [ForeignKey("RegionTbl")]
        public int RegionId { get; set; }
        [ForeignKey("AutomobileTypeTbl")]
        public int AutomobileTypeId { get; set; }
        public decimal AgreementPrice { get; set; }
        public int Count { get; set; }

       // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DriverRoutTbl> DriverRoutTbls { get; set; }
        public virtual RegionTbl RegionTbl { get; set; }
        public virtual AutomobileTypeTbl AutomobileTypeTbl { get; set; }

    }
}
