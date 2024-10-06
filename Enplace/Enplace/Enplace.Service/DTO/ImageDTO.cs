using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enplace.Service.DTO
{
    public class ImageDTO
    {
        public string MIME { get; set; } = string.Empty;
        public byte[] Data { get; set; } = [];
        public ImageSize Size { get; set; } = ImageSize.Header;
    }
}
