using AutoMapper;
using Xero.RefactorMe.Model;
using Xero.RefactorMe.Web.ViewModels;

namespace Xero.RefactorMe.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(vm => vm.ProductId, m => m.MapFrom(p => p.Id))
                .ReverseMap();
            CreateMap<ProductOption, ProductOptionViewModel>()
                .ForMember(vm => vm.ProductId, m => m.MapFrom(po => po.ProductId))
                .ReverseMap();
        }
    }
}