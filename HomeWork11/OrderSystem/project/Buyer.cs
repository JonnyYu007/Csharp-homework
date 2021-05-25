using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem
{
    public class Buyer
    {
        public Buyer() { }
        public Buyer(long id, string name)
        {
            Id = id;
            //设置客户姓名
            Name = name;
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return "buyerId:" + Id + ",buyerName:" + Name;
        }
    }
}
