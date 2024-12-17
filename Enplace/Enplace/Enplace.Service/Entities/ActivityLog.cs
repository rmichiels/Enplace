using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enplace.Service.Entities
{
    public class ActivityLog : TracedEntity
    {
        public int UserID { get; set; }
        public string Topic { get; set; }
        public int ItemID { get; set; }

        [Timestamp]
        public byte[] Stamp { get; set; }

        public virtual User User { get; set; }
    }
}
