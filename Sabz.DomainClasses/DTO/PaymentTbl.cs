using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabz.DomainClasses.DTO
{
    [Table("PaymentTbl")]
    public class PaymentTbl : BaseEntity<int>
    {
        [Key]
        public int PaymentId { get; set; }
        public decimal? Insurance { get; set; }
        public decimal? PreHelpCost { get; set; }
        public decimal? Fine { get; set; }
        public decimal? Tax { get; set; }
        public decimal? AccidentCost { get; set; }
        public DateTime CreateDate { get; set; }
        public int? DriverId { get; set; }
        public virtual DriverTbl DriverTbl{ get; set; }
    }
}
