using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CRUD_Entity.Models
{
    public class Context : DbContext
    {
        public Context()
        {
            Database.SetInitializer(new DbInitializer());
        }
        public DbSet<Piloto> Piloto { get; set; }
        public DbSet<Aviao> Aviao { get; set; }
    }

    public class DbInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            base.Seed(context);
        }
    }


}