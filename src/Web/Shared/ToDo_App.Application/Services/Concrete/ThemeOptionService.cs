using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.Application.Models.VMs;
using ToDo_App.Application.Services.Interface;
using ToDo_App.Domain.Entities.Concrete;
using ToDo_App.Domain.Enums;
using ToDo_App.Domain.Repositories.Interface.EntityType;

namespace ToDo_App.Application.Services.Concrete
{
    public class ThemeOptionService : IThemeOptionService
    {
        private readonly IThemeOptionRepository _themeOptionRepository;
        private readonly IMapper _mapper;

        public ThemeOptionService(IThemeOptionRepository themeOptionRepository, IMapper mapper)
        {
            _themeOptionRepository = themeOptionRepository;
            _mapper = mapper;
        }

        public async Task Create(GetThemeOptionsVM model)
        {
            var option = _mapper.Map<ThemeOption>(model);
            await _themeOptionRepository.Add(option);
        }

        public async Task<GetThemeOptionsVM> GetThemeOptionForUser(string id)
        {
            var themeOption = await _themeOptionRepository.GetFilteredFirstOrDefault(
               selector: x => new GetThemeOptionsVM
               {
                   Id = x.Id,
                   AppUserId = x.AppUserId,
                   ThemeColor = x.ThemeColor
               },
               expression: x => x.AppUserId == id);

            return themeOption;
        }

        public async Task Update(GetThemeOptionsVM model)
        {
            var option = _mapper.Map<ThemeOption>(model);
            option.Status = Status.Modified;
            option.UpdateDate = DateTime.Now;
            await _themeOptionRepository.Update(option);
        }
    }
}
