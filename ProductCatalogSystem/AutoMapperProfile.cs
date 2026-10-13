using AutoMapper;
using ProductCatalogSystem.ViewModel;
using ProductCatalogSystem.Models;

namespace ProductCatalogSystem
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserViewModel>();

            CreateMap<UserViewModel, User>();
            CreateMap<CreateUserViewModel, User>();
            CreateMap<BrandViewModel, Brand>();
            CreateMap<Brand, BrandViewModel>();
            CreateMap<Review, ReviewViewModel>();
            CreateMap< ReviewViewModel, Review>();
        }
        
    }
}
