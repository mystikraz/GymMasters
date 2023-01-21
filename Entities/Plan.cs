using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Plan : BaseEntity
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public double Price { get; set; }
        public string Notes { get; set; }
        public virtual ICollection<Membership>? Memberships { get; set; }

    }
}
