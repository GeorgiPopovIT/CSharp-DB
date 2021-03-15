using AutoMapper;
using ProductShop.DTO;
using ProductShop.Models;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            //CreateMap<Product, ProductDTO>()
            //    .ForMember(x => x.SellerFullName,
            //    y => y.MapFrom(x => x.Seller.FirstName + " " + x.Seller.LastName));

            CreateMap<Product, ProductUserDTO>()
                .ForMember(x => x.BuyerFirstName, y => y.MapFrom(x => x.Buyer.FirstName))
                .ForMember(x => x.BuyerLastName, y => y.MapFrom(x => x.Buyer.LastName));

            //CategotyDTo
            //CreateMap<Category,CategoryDTO>()
            //    .ForMember(x => x.AveragePrice,y => y.MapFrom(x => x.CategoryProducts.Se))
        }
    }
}
