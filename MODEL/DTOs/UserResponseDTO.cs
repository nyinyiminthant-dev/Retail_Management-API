using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.ApplicationConfig;
using MODEL.Entities;

namespace MODEL.DTOs;

public class UserResponseDTO : Common
{
    public User Data { get; set; }
}

public class UserListResponseDTO : Common
{
    public List<User> Data { get; set; }
    public string? Token { get; set; }
}   

