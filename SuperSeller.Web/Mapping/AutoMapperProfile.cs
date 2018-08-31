using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SuperSeller.Common.Admin.ViewModels;
using SuperSeller.Common.User.ViewModels;
using SuperSeller.Models;

namespace SuperSeller.Web.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<Category, CategoryViewModel>();
            this.CreateMap<Category, CategoryConciseViewModel>()
                .ForMember(cv => cv.SubCotegoriesCount, opts => opts.MapFrom(c => c.SubCategories.Count));
            this.CreateMap<SubCategory, SubCategoryConciseViewModel>()
                .ForMember(sc => sc.AdsCount, opts => opts.MapFrom(c => c.Ads.Count));
            this.CreateMap<User, UsersConciseViewModel>()
                .ForMember(uv => uv.Ads, opts => opts.MapFrom(u => u.Ads.Count))
                .ForMember(uv => uv.isLock, opts => opts.MapFrom(u => u.LockoutEnd != null && u.LockoutEnabled));
            this.CreateMap<Ad, AdsConciseViewModel>()
                .ForMember(av => av.Condition, opts => opts.MapFrom(a => a.Condition.Name));
            this.CreateMap<Ad, AdsViewModel>()
                .ForMember(av => av.Condition, opts => opts.MapFrom(a => a.Condition.Name))
                .ForMember(av => av.CreatorName, opts => opts.MapFrom(a => a.Creator.FullName))
                .ForMember(av => av.CreatorPicture, opts => opts.MapFrom(a => a.Creator.ImagePath));
            this.CreateMap<Ad, AdsRangeViewModel>()
                .ForMember(av => av.Image, opts => opts.MapFrom(a => a.Pictures.First().Path));
        }
    }
}