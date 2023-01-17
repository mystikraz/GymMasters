using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Trainer: BaseEntity
    {
        public string FirstName { get; set; }
        public string lastName { get; set; }
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public string Notes { get; set; }
        public virtual ICollection<Member>? Members { get; set; }
    }
}
