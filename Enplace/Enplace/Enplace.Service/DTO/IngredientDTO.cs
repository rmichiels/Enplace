using Enplace.Service.Contracts;
using Enplace.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enplace.Service.DTO
{
    public class IngredientDTO : ILabeled
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public IngredientCategory Category { get; set; }
        public Measurement Measurement { get; set; }
        public decimal Quantity { get; set; }
        public string? Comment { get; set; }
    }
}
