using AutoMapper;
using ProductShop.Dtos.Export;
using ProductShop.Dtos.Import;
using ProductShop.Models;
using System.Linq;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<UserDTO, User>();

            this.CreateMap<CategoriesDTO, Category>()
                .ForAllMembers(opt => opt.Condition(src => src.Name != null));

            this.CreateMap<ProductDTO, Product>();

            this.CreateMap<CategoriesProductsDTO, CategoryProduct>();

            this.CreateMap<Product, ExportProductsDTO>()
                .ForMember(x => x.BuyerName, y => y
                .MapFrom(z => z.Buyer.FirstName + " " + z.Buyer.LastName));

            this.CreateMap<Product, ProductToSoldDTO>();
            this.CreateMap<User, SoldProductsDTO>()
                .ForMember(x => x.SoldProducts, y => y.MapFrom(z => z.ProductsSold));
            this.CreateMap<ProductToSoldDTO, SoldProductsDTO>();

            this.CreateMap<Category, CategoryByProductDTO>()
                .ForMember(x => x.Count, y => y.MapFrom(z => z.CategoryProducts.Count))
                .ForMember(x => x.AveragePrice, y => y.MapFrom(z => z.CategoryProducts.Average(c => c.Product.Price)))
                .ForMember(x => x.TotalRevenue, y => y.MapFrom(z => z.CategoryProducts.Sum(c => c.Product.Price)));

            this.CreateMap<Product, ProductToUsersDTO>();
            this.CreateMap<ProductToUsersDTO, UserAndProductsDTO>();
                
            this.CreateMap<User, UserAndProductsDTO>();
            
        }
    }
}
