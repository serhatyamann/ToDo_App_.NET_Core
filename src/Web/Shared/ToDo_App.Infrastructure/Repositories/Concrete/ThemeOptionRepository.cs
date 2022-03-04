using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.Domain.Entities.Concrete;
using ToDo_App.Domain.Repositories.Interface.EntityType;
using ToDo_App.Infrastructure.Context;
using ToDo_App.Infrastructure.Repositories.Abstract;

namespace ToDo_App.Infrastructure.Repositories.Concrete
{
    public class ThemeOptionRepository: BaseRepository<ThemeOption>, IThemeOptionRepository
    {
        public ThemeOptionRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
