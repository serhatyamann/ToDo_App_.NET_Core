using ToDo_App.Domain.Repositories.Interface.EntityType;
using ToDo_App.Domain.Entities.Concrete;
using ToDo_App.Infrastructure.Context;
using ToDo_App.Infrastructure.Repositories.Abstract;

namespace ToDo_App.Infrastructure.Repositories.Concrete
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
