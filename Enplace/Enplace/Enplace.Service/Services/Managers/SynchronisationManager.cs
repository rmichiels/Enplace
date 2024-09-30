using Enplace.Service.Database;
using Enplace.Service.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enplace.Service.Services.Managers
{
    public class SynchronisationManager(SSDBContext SqlServerContext, LiteDBContext LiteContext)
    {
        private readonly SSDBContext _azureDB = SqlServerContext;
        private readonly LiteDBContext _liteDB = LiteContext;

        public void DownSync()
        {
            try
            {
                var migrations = _liteDB.Database.GetPendingMigrations();
                _liteDB.Database.Migrate();

                _liteDB.Users.AddRange(_azureDB.Users.ToList());

                _liteDB.AddRange(_azureDB.Set<Measurement>());


                _liteDB.IngredientCategories.AddRange(_azureDB.IngredientCategories);
                _liteDB.AddRange(_azureDB.Set<Ingredient>());


                _liteDB.AddRange(_azureDB.Set<Recipe>());
                _liteDB.AddRange(_azureDB.Set<RecipeStep>());


                _liteDB.AddRange(_azureDB.Set<RecipeIngredient>());


                _liteDB.AddRange(_azureDB.Set<UserMenu>());

                _liteDB.SaveChanges();
            }
            catch
            {

            }
        }

    }
}
