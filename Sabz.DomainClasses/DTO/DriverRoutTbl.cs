namespace Sabz.DomainClasses.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DriverRoutTbl")]
    public partial class DriverRoutTbl : BaseEntity<int>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DriverRoutTbl()
        {
            LogDriverRoutTbls = new HashSet<LogDriverRoutTbl>();
        }
        [ForeignKey("DriverTbl")]
        public int DriverId { get; set; }
        [ForeignKey("RoutTbl")]
        public int RoutId { get; set; }
        public int IsTemporary { get; set; }
        public decimal RoutPrice { get; set; }
        public virtual DriverTbl DriverTbl { get; set; }
        public virtual RoutTbl RoutTbl { get; set; }
      //  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LogDriverRoutTbl> LogDriverRoutTbls { get; set; }
    }
}
