namespace Sabz.DomainClasses.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LogDriverRoutTbl")]
    public partial class LogDriverRoutTbl : BaseEntity<int>
    {
        public bool IsDone { get; set; }
        public decimal? FinePrice { get; set; }
        [Column(TypeName = "date")]
        public DateTime DoDate { get; set; }
        [ForeignKey("DriverRoutTbl")]
        public int DriverRoutId { get; set; }
        public virtual DriverRoutTbl DriverRoutTbl { get; set; }
    }
}
