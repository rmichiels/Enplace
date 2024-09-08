using Enplace.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Enplace.Service.Entities
{
    public class RecipeImage :ILabeled
    {
        public int Id { get; set; } 
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public ImageSize Size { get; set; } = ImageSize.Shard;

        [JsonIgnore]
        public virtual Recipe Recipe { get; set; }
    }
}
