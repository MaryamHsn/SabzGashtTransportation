namespace Sabz.DomainClasses.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LogRoutDriverTbl")]
    public partial class LogRoutDriverTbl : BaseEntity<int>
    {
        public int Id { get; set; }

        public bool IsDone { get; set; }

        public decimal? FinePrice { get; set; }

        [Column(TypeName = "date")]
        public DateTime DoDate { get; set; }

        public DateTime? CDate { get; set; }

        public DateTime? LDate { get; set; }
        public int DriverRoutId { get; set; }

        public int? IsTemporary { get; set; }

        public virtual DriverRoutTbl DriverRoutTbl { get; set; }
    }
}
