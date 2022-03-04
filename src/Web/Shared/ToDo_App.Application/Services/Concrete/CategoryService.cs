using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo_App.Application.Models.Category;
using ToDo_App.Application.Models.VMs;
using ToDo_App.Application.Services.Interface;
using ToDo_App.Domain.Entities.Concrete;
using ToDo_App.Domain.Enums;
using ToDo_App.Domain.Repositories.Interface.EntityType;
using Microsoft.AspNetCore.Identity;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ToDo_App.Application.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this._categoryRepository = categoryRepository;
            this._mapper = mapper;
        }
        public async Task Create(CreateCategoryModel model)
        {
            var category = _mapper.Map<Category>(model);
            await _categoryRepository.Add(category);
        }

        public async Task Update(UpdateCategoryModel model)
        {
            var category = _mapper.Map<Category>(model);

            await _categoryRepository.Update(category);
        }

        public async Task Delete(int id)
        {
            var category = await _categoryRepository.GetDefault(x => x.Id == id);
            if (category != null)
            {
                _categoryRepository.Delete(category);
            }
        }

        public async Task<UpdateCategoryModel> GetById(int id)
        {
            var category = await _categoryRepository.GetFilteredFirstOrDefault(
                selector: x => new GetCategoryVM
                {
                    Id = x.Id,
                    Name = x.Name
                },
                expression: x => x.Id == id && x.Status != Status.Passive);

            var updatedCategory = _mapper.Map<UpdateCategoryModel>(category);

            return updatedCategory;
        }

        public async Task<Category> GetBySlug(string slug)
        {
            var category = await _categoryRepository.GetDefault(x => x.Slug == slug);
            return category;
        }

        public async Task<List<GetCategoryVM>> GetCategories(string id)
        {
            var categoryList = await _categoryRepository.GetFilteredList(
                selector: x => new GetCategoryVM
                {
                    Id = x.Id,
                    Name = x.Name,
                },
                expression: x => x.Status != Status.Passive && x.AppUserId == id);

            return categoryList;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetDefault(x => x.Id == id);
            return category;
        }

        public async Task<List<GetCategoryVM>> GetDeletedCategories(string id)
        {
            var categoryList = await _categoryRepository.GetFilteredList(
                selector: x => new GetCategoryVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    DeleteDate = x.DeleteDate

                },
                expression: x => x.Status == Status.Passive && x.AppUserId == id);
            return categoryList;
        }
    }
}
