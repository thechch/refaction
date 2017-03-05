using AutoMapper;
using Xero.RefactorMe.Model;
using Xero.RefactorMe.Web.ViewModels;

namespace Xero.RefactorMe.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductViewModel, Product>()
                .ReverseMap();
            CreateMap<ProductOptionViewModel, ProductOption>()
                .ReverseMap();
        }
    }
}