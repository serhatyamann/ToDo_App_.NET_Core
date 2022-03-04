using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.Domain.Entities.Concrete;
using ToDo_App.Domain.Repositories.Interface.Base;

namespace ToDo_App.Domain.Repositories.Interface.EntityType
{
    public interface IThemeOptionRepository: IBaseRepository<ThemeOption>
    {
    }
}
