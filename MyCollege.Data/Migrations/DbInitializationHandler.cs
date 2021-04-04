
namespace MyCollege.Data.Migrations
{
    public static class DbInitializationHandler
    {
        public static void Initialize()
        {
            using (var db = new Context())
            {
                if (!db.Database.Exists())
                {
                    db.Database.Initialize(true);
                }                
            }
        }
    }
}
