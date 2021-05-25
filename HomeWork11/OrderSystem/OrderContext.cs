using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem   
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    class OrderContext : DbContext
    {
        public OrderContext() : base("OrderDatabase")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<OrderContext>());
        }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<Buyer> Buyers { get; set; }

    }
}
