using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enplace.Service.Entities
{
    public class TracedEntity
    {
        public DateTime CreatedOn { get; set; }
        public int? CreatedByID { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int? ModifiedByID { get; set; }
    }
}
