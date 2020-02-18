namespace Sabz.DomainClasses.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DriverRoutTbl")]
    public partial class DriverRoutTbl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DriverRoutTbl()
        {
            LogRoutDriverTbls = new HashSet<LogRoutDriverTbl>();
        }

        public int Id { get; set; }

        public int DriverId { get; set; }

        public int RoutId { get; set; }

        public int IsTemporary { get; set; }
        public bool IsActive { get; set; }
        public DateTime CFDate { get; set; }
        public DateTime LFDate { get; set; }
        public virtual DriverTbl DriverTbl { get; set; }

        public virtual RoutTbl RoutTbl { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LogRoutDriverTbl> LogRoutDriverTbls { get; set; }
    }
}
