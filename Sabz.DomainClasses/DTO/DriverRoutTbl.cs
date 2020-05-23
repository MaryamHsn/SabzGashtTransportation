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
       public DriverRoutTbl()
        {
            LogDriverRoutTbls = new HashSet<LogDriverRoutTbl>();
        }
        [ForeignKey("DriverTbl")]
        public int DriverId { get; set; }
        [ForeignKey("RoutTbl")]
        public int RoutId { get; set; }
        public virtual DriverTbl DriverTbl { get; set; }
        public virtual RoutTbl RoutTbl { get; set; }
        public virtual ICollection<LogDriverRoutTbl> LogDriverRoutTbls { get; set; }
    }
}
