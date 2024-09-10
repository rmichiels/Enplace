using Enplace.Service;
using Enplace.Service.Contracts;
using Enplace.Service.Entities;

namespace Enplace.Library
{
    public class EnplaceContext
    {
        public EnplaceContext()
        {

        }
        public User? User { get; set; }
        public List<Measurement> Measurements { get; set; } = [];
        public List<IngredientCategory> Categories { get; set; } = [];
    }
}
