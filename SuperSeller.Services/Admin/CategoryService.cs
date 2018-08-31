using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SuperSeller.Common.Admin.BindingModels;
using SuperSeller.Common.Admin.ViewModels;
using SuperSeller.Data;
using SuperSeller.Models;
using SuperSeller.Services.Admin.Interfaces;

namespace SuperSeller.Services.Admin
{
    public class CategoryService : BaseEFService, ICategoryService
    {
        public CategoryService(ApplicationDbContext dbContext, IMapper mapper) 
            :base(dbContext, mapper)
        {
        }

        public async Task<bool> CreateCategory(CreateCategoryBindingModel model)
        {
            var category = DbContext.Categories.FirstOrDefault(c => c.Name == model.Name);

            if (category != null)
            {
                return false;
            }

            category = new Category()
            {
                Name = model.Name
            };

            DbContext.Categories.Add(category);
            await DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CreateSubCategory(CreateSubCategoryBindingModel model)
        {
            var subCategory = DbContext.SubCategories.FirstOrDefault(sc => sc.Name == model.Name);

            if (subCategory != null)
            {
                return false;
            }

            subCategory = new SubCategory()
            {
                Name = model.Name
            };

            var category = DbContext.Categories.Find(model.CategoryId);
            category.SubCategories.Add(subCategory);
            await DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<SelectCategoryViewModel>> GetAllSelectedCategories()
        {
            var dbCategories = await DbContext.Categories.ToListAsync();
            var categories = Mapper.Map<List<SelectCategoryViewModel>>(dbCategories);
            return categories;
        }

        public async Task<CategoryViewModel> GetCategory(int id)
        {
            var dbCategory = await DbContext.Categories.Include(c => c.SubCategories).FirstOrDefaultAsync(c => c.Id == id);
            var category = Mapper.Map<CategoryViewModel>(dbCategory);
            return category;
        }

        public async Task<List<CategoryConciseViewModel>> GetAllCategories()
        {
            var dbCategories = await DbContext.Categories.Include(c => c.SubCategories).ToListAsync();
            var categories = Mapper.Map<List<CategoryConciseViewModel>>(dbCategories);
            return categories;
        }

        public void Delete(int id)
        {
            var category = DbContext.Categories.Find(id);

            DbContext.Categories.Remove(category);

            DbContext.SaveChanges();
        }
    }
}