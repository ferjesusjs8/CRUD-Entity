using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CRUD_Entity.Models
{
    public class Context : DbContext
    {
        public DbSet<Piloto> Piloto { get; set; }
        public DbSet<Aviao> Aviao { get; set; }
    }
}