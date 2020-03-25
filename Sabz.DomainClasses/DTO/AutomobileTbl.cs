namespace Sabz.DomainClasses.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AutomobileTbl")]
    public partial class AutomobileTbl : BaseEntity<int>
    {
     //   [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AutomobileTbl()
        {
            DriverTbls = new HashSet<DriverTbl>();
        }
        [StringLength(13)]
        public string Number { get; set; }
        [StringLength(10)]
        public string Shasi { get; set; }
        [StringLength(10)]
        public string CreateYear { get; set; }

        [ForeignKey("AutomobileTypeTbl")]
        public int AutomobileTypeId { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual AutomobileTypeTbl AutomobileTypeTbl { get; set; }
       // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DriverTbl> DriverTbls { get; set; }
       // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccidentTbl> AccidentTbls { get; set; }
    }
}
