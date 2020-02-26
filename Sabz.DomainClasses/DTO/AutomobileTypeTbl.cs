namespace Sabz.DomainClasses.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AutomobileTypeTbl")]
    public partial class AutomobileTypeTbl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AutomobileTypeTbl()
        {
            AutomobileTbls = new HashSet<AutomobileTbl>();
            RoutTbls = new HashSet<RoutTbl>();
        }

        [Key]
        public int AutoTypeId { get; set; }

        public bool HasCooler { get; set; }

        public string Description { get; set; }

        public int IsBus { get; set; }
        public bool IsActive { get; set; }
        public DateTime CFDate { get; set; }
        public DateTime LFDate { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AutomobileTbl> AutomobileTbls { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoutTbl> RoutTbls { get; set; }
    }
}
