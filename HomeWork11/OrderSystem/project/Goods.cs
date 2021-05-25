using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem
{
    public class Goods
    {
        public Goods() { }
        public Goods(long id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public override string ToString()
        {
            return "goodsId:" + Id + ",goodsName:" + Name + ",goodsPrice:" + Price;
        }
    }
}
