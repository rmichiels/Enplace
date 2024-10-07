using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enplace.Service.Entities
{
    public class UserMenuRecipe
    {
        public int MenuID { get; set; }
        public int RecipeID { get; set; }

        public virtual UserMenu Menu { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
