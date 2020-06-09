using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabz.DomainClasses.DTO
{
    [Table("BankAccountNumberTbl")]
    public partial class BankAccountNumberTbl : BaseEntity<int>
    { 
        [ForeignKey("DriverTbl")]
        public int DriverId { get; set; }
        public int RegionId { get; set; }
        public string BankAccountNumber{ get; set; }
        public string BankName { get; set; }
        public string BankBranchCode { get; set; }
        public virtual DriverTbl DriverTbl { get; set; } 
    }
}
