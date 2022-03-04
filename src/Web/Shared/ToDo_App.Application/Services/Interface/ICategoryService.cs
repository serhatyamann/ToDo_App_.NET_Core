using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.Application.Models.Category;
using ToDo_App.Application.Models.VMs;
using ToDo_App.Domain.Entities.Concrete;

namespace ToDo_App.Application.Services.Interface
{
    public interface ICategoryService
    {
        Task Create(CreateCategoryModel model);
        Task Update(UpdateCategoryModel model);
        Task Delete(int id);

        Task<UpdateCategoryModel> GetById(int id);
        Task<List<GetCategoryVM>> GetCategories(string id);
        Task<List<GetCategoryVM>> GetDeletedCategories(string id);

        Task<Category> GetBySlug(string slug);
        Task<Category> GetCategoryById(int id);
    }
}
