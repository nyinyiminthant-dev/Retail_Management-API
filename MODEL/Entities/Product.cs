using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entities;

[Table("Product_Tbl")]
public class Product
{
    [Key]
    public int ProductId { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public decimal Profit { get; set; }
    
    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdateDate { get; set; }
    
    public string? Img { get; set; }
    
    

    public string IsOut { get; set; }
}
