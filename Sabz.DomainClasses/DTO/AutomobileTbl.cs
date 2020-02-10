namespace Sabz.DomainClasses.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AutomobileTbl")]
    public partial class AutomobileTbl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AutomobileTbl()
        {
            DriverTbls = new HashSet<DriverTbl>();
        }

        [Key]
        public int AutoId { get; set; }

        [StringLength(13)]
        public string Number { get; set; }

        [StringLength(10)]
        public string Shasi { get; set; }

        [StringLength(10)]
        public string CreateYear { get; set; }

        public int? AutomobileTypeId { get; set; }

        public virtual AutomobileTypeTbl AutomobileTypeTbl { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DriverTbl> DriverTbls { get; set; }
    }
}
