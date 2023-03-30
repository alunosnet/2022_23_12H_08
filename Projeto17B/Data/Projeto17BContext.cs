using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Projeto17B.Data
{
    public class Projeto17BContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Projeto17BContext() : base("name=Projeto17BContext")
        {
        }

        public System.Data.Entity.DbSet<Projeto17B.Models.Roupas> Roupas { get; set; }


        public System.Data.Entity.DbSet<Projeto17B.Models.Compras> Compras { get; set; }

        public System.Data.Entity.DbSet<Projeto17B.Models.Utilizador> Utilizadors { get; set; }
    }
}
