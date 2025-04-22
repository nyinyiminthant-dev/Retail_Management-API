using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.DTOs;
using REPOSITORY.Repositories.IRepositories;

namespace REPOSITORY.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        IOrderRepository Order { get; }

        IUserRepository User { get; }

        AppSettings AppSettings { get; set; }
        Task<int> SaveAsync();
    }
    
    
}
