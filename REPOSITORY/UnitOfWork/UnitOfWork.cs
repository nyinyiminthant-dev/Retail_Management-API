using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MODEL;
using MODEL.DTOs;
using MODEL.Entities;
using REPOSITORY.Repositories.IRepositories;
using REPOSITORY.Repositories.Repositories;

namespace REPOSITORY.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DataContext _dataContext;

        public UnitOfWork(DataContext dataContext, IOptions<AppSettings> appsettings)
        {
            _dataContext = dataContext;
            Product = new ProductRepository(_dataContext);
            Order = new OrderRepository(_dataContext);
            User = new UserRepository(_dataContext);
            AppSettings = appsettings.Value;

        }

        public IProductRepository Product { get; set; }

        public IOrderRepository Order { get; set; }

        public IUserRepository User { get; set; }

        public AppSettings AppSettings { get; set; }

        public void Dispose()
        {
            _dataContext.Dispose();
        }

        public Task<int> SaveAsync()
        {
            return  _dataContext.SaveChangesAsync();
        }
    }
}
