using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entities;



[Table("Order_Tbl")]
public class Order
{

    [Key]
    public int OrderId { get; set; }
    public int ProductId { get; set; }

    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Profit { get; set; }

    public string Status { get; set; }
}

