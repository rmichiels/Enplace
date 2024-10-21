using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enplace.Service.DTO
{
    public class Response<TResponse>
    {
        public List<TResponse>? Data { get; set; }
        public Pagination? Pagination { get; set; }
    }
}
