using DataAccess.Entities;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Base.Infrastructure
{
    public class GlossaryDbInitizilizer : CreateDatabaseIfNotExists<GlossaryDbContext> //DropCreateDatabaseAlways<GlossaryDbContext> // CreateDatabaseIfNotExists<GlossaryDbContext> //DropCreateDatabaseIfModelChanges<GlossaryDbContext>
    {
        protected override void Seed(GlossaryDbContext context)
        {
            base.Seed(context);

            //var user = new User
            //{
            //    Active = true,
            //    FirstName = "Daniel",
            //    LastName = "Barac",
            //    Password = "1234",
            //    Email = "admin@yahoo.com"
            //};

            //context.Users.Add(user);

            //context.SaveChanges();
        }
    }
}
