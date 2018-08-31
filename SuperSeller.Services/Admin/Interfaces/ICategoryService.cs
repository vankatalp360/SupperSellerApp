using System.Collections.Generic;
using System.Threading.Tasks;
using SuperSeller.Common.Admin.BindingModels;
using SuperSeller.Common.Admin.ViewModels;

namespace SuperSeller.Services.Admin.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> CreateCategory(CreateCategoryBindingModel model);
        Task<bool> CreateSubCategory(CreateSubCategoryBindingModel model);
        Task<List<SelectCategoryViewModel>> GetAllSelectedCategories();
        Task<CategoryViewModel> GetCategory(int id);
        Task<List<CategoryConciseViewModel>> GetAllCategories();
        void Delete(int id);
    }
}