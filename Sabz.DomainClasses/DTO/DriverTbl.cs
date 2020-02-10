namespace Sabz.DomainClasses.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DriverTbl")]
    public partial class DriverTbl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DriverTbl()
        {
            AccidentTbls = new HashSet<AccidentTbl>();
            DriverRoutTbls = new HashSet<DriverRoutTbl>();
            RepairmentTbls = new HashSet<RepairmentTbl>();
        }

        [Key]
        public int DriverId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string FatherName { get; set; }

        [StringLength(10)]
        public string NationalCode { get; set; }

        [StringLength(10)]
        public string LicenceCode { get; set; }

        public DateTime? BirthDate { get; set; }

        public int AutomobileId { get; set; }

        public string Address { get; set; }

        [StringLength(15)]
        public string Phone1 { get; set; }

        [StringLength(15)]
        public string Phone2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccidentTbl> AccidentTbls { get; set; }

        public virtual AutomobileTbl AutomobileTbl { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DriverRoutTbl> DriverRoutTbls { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RepairmentTbl> RepairmentTbls { get; set; }
    }
}
