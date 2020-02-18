namespace Sabz.DomainClasses.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RegionTbl")]
    public partial class RegionTbl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RegionTbl()
        {
            RoutTbls = new HashSet<RoutTbl>();
        }

        [Key]
        public int RegionId { get; set; }

        [Required]
        [StringLength(50)]
        public string RegionName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CFDate { get; set; }
        public DateTime LFDate { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoutTbl> RoutTbls { get; set; }
    }
}
