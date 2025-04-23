using MODEL;
using MODEL.Entities;
using REPOSITORY.Repositories.IRepositories;

namespace REPOSITORY.Repositories.Repositories;

internal class CategoryRepositoroy : GenericRepository<Category> , ICategoryRepository
{
    
    public CategoryRepositoroy(DataContext context) : base(context)
    {
    }
    
}