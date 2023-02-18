using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Checkin: BaseEntity
    {
        public int MemberId { get; set; }
        //public int PlanId { get; set; }
        [NotMapped]
        public string? Status { get; set; }
        [NotMapped]
        public DateTime? EndDate { get; set; }
        public virtual Member? Member { get; set; }
    }
}
