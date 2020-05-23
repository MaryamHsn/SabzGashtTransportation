namespace Sabz.DomainClasses.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RegionTbl")]
    public partial class RegionTbl : BaseEntity<int>
    {
        public RegionTbl()
        {
            RoutTbls = new HashSet<RoutTbl>();
        }
        [Required]
        [StringLength(50)]
        public string RegionName { get; set; }
        public virtual ICollection<RoutTbl> RoutTbls { get; set; }
    }
}
