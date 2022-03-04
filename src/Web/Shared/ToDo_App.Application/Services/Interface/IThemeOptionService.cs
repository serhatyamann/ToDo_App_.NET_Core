using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.Application.Models.VMs;

namespace ToDo_App.Application.Services.Interface
{
    public interface IThemeOptionService
    {
        Task<GetThemeOptionsVM> GetThemeOptionForUser(string id);
        Task Create(GetThemeOptionsVM model);
        Task Update(GetThemeOptionsVM model);
    }
}
