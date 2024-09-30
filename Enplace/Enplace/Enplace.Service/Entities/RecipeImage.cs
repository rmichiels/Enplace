using Enplace.Service.Contracts;
using System.Text.Json.Serialization;

namespace Enplace.Service.Entities
{
    public class RecipeImage : ILabeled
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
