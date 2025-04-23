using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.ApplicationConfig;

namespace MODEL.Entities;

[Table("User_Tbl")]
public class User
{

    [Key]
    public int UserID { get; set; }
    public string Name { get; set; } 
    public string Email { get; set; }
    public string Password { get; set; }


    public  DateTime CreateAt { get; set; } = DateTime.Now;

    public DateTime UpdateAt { get; set; } = DateTime.Now;

    public string Status { get; set; }

    public string? OTP { get; set; }

    public DateTime OTP_Exp { get; set; } = DateTime.Now.AddMinutes(5);

    public string Role { get; set; }

}
