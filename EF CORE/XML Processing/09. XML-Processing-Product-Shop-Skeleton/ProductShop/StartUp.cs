using AutoMapper;
using AutoMapper.QueryableExtensions;
using ProductShop.Data;
using ProductShop.Dtos.Export;
using ProductShop.Dtos.Import;
using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ProductShop
{
    public class StartUp
    {
        private static IMapper mapper;
        private static MapperConfiguration config;
        public static void Main(string[] args)
        {
            var db = new ProductShopContext();
            //var path = "../../../Datasets";
            //var writePath = "../../../Results/";

            //InitializeMapper();

            //ex.1
            //var xml = File.ReadAllText("../../../Datasets/users.xml");
            //Console.WriteLine(ImportUsers(db, xml));

            //ex.2
            //var xml = File.ReadAllText(path + "/products.xml.");
            //Console.WriteLine(ImportProducts(db, xml));

            //ex.3
            //var xml = File.ReadAllText(path + "/categories.xml");
            //Console.WriteLine(ImportCategories(db,xml));

            //ex.4
            //var xml = File.ReadAllText(path + "/categories-products.xml");
            //Console.WriteLine(ImportCategoryProducts(db,xml));

            //ex.5
            //var xml = GetProductsInRange(db);
            //File.WriteAllText("products-in-range.xml", xml);

            //ex.6
            //var xml = GetSoldProducts(db);
            //File.WriteAllText("users-sold-products.xml", xml);

            //ex.7
            //var xml = GetCategoriesByProductsCount(db);
            //File.WriteAllText("categories-by-products.xml", xml);

            //ex.8
            var xml = GetUsersWithProducts(db);
            File.WriteAllText("users-and-products.xml", xml);
        }
        //ex.1
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<UserDTO>), new XmlRootAttribute("Users"));
            var desUsersDtos = (List<UserDTO>)serializer.Deserialize(new StringReader(inputXml));

            var users = mapper.Map<List<User>>(desUsersDtos);

            context.Users.AddRange(users);
            context.SaveChanges();
            return $"Successfully imported {users.Count}";
        }
        //ex.2
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(List<ProductDTO>), new XmlRootAttribute("Products"));

            var dtos = (List<ProductDTO>)serializer.Deserialize(new StringReader(inputXml));

            var products = mapper.Map<List<Product>>(dtos);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count}";
        }
        //ex.3
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(List<CategoriesDTO>), new XmlRootAttribute("Categories"));

            var dtos = (List<CategoriesDTO>)serializer.Deserialize(new StringReader(inputXml));


            var categories = mapper.Map<List<Category>>(dtos);

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }
        //ex.4
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {

            var serializer = new XmlSerializer(typeof(List<CategoriesProductsDTO>), new XmlRootAttribute("CategoryProducts"));

            var dtos = (List<CategoriesProductsDTO>)serializer.Deserialize(new StringReader(inputXml));

            var categoryIds = context.Categories.Select(c => c.Id);
            var productsIds = context.Products.Select(c => c.Id);

            var categoryProducts = mapper.Map<List<CategoryProduct>>(dtos)
                .Where(cp => categoryIds.Any(c => cp.CategoryId == c)
                && productsIds.Any(p => cp.ProductId == p))
                .ToList(); ;

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }
        //ex.5
        public static string GetProductsInRange(ProductShopContext context)
        {
            XmlSerializerNamespaces @namespace = new XmlSerializerNamespaces();
            @namespace.Add(string.Empty, string.Empty);

            StringBuilder sb = new StringBuilder();

            //var products = context.Products
            //    .Where(p => p.Price >= 500 && p.Price <= 1000)
            //    .OrderBy(p => p.Price)
            //    .Take(10)
            //    .ProjectTo<ExportProductsDTO>(config)
            //    .ToList();
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Take(10)
                .Select(p => new ExportProductsDTO
                {
                    Name = p.Name,
                    Price = p.Price,
                    BuyerName = p.Buyer.FirstName + " " + p.Buyer.LastName
                })
                .ToList();

            var seriliazer = new XmlSerializer(typeof(List<ExportProductsDTO>), new XmlRootAttribute("Products"));


            seriliazer.Serialize(new StringWriter(sb), products, @namespace);

            return sb.ToString().Trim();
        }
        //ex.6
        public static string GetSoldProducts(ProductShopContext context)
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            //var soldProducts = context.Users
            //    .Where(u => u.ProductsSold.Count >= 1)
            //    .OrderBy(u => u.LastName)
            //    .ThenBy(u => u.FirstName)
            //    .Take(5)
            //    .ProjectTo<SoldProductsDTO>(config)
            //    .ToList();
            var soldProducts = context.Users
                .Where(u => u.ProductsSold.Count >= 1)
               .OrderBy(u => u.LastName)
               .ThenBy(u => u.FirstName)
               .Take(5)
               .Select(u => new SoldProductsDTO
               {
                   FirstName = u.FirstName,
                   LastName = u.LastName,
                   SoldProducts = u.ProductsSold.Select(ps => new ProductToSoldDTO
                   {
                       Name = ps.Name,
                       Price = ps.Price
                   })
                   .ToList()
               })
               .ToList();

            var seriliazer = new XmlSerializer(typeof(List<SoldProductsDTO>), new XmlRootAttribute("Users"));


            seriliazer.Serialize(new StringWriter(sb), soldProducts, ns);

            return sb.ToString().Trim();
        }
        //ex.7
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            //var categories = context.Categories
            //    .ProjectTo<CategoryByProductDTO>(config)
            //    .OrderByDescending(c => c.Count)
            //    .ThenBy(c => c.TotalRevenue)
            //    .ToList();

            var categories = context.Categories
                .Select(c => new CategoryByProductDTO
                {
                    Name = c.Name,
                    Count = c.CategoryProducts.Count,
                    AveragePrice = c.CategoryProducts.Average(c => c.Product.Price),
                    TotalRevenue = c.CategoryProducts.Sum(c => c.Product.Price)
                })
                .OrderByDescending(c => c.Count)
                .ThenBy(c => c.TotalRevenue)
                .ToList();
            var serializer = new XmlSerializer(typeof(List<CategoryByProductDTO>), new XmlRootAttribute("Categories"));

            serializer.Serialize(new StringWriter(sb), categories, ns);


            return sb.ToString().Trim();
        }
        //ex.8
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            //var usersProducts = context.Users
            //    .Where(u => u.ProductsSold.Count >= 1)
            //    .ProjectTo<UserAndProductsDTO>(config)
            //    .OrderByDescending(u => u.Count)

            var users = new UsersDTO()
            {
                Count = context.Users.Count(u => u.ProductsSold.Any(ps => ps.BuyerId != null)),
                Users = context.Users
                 .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
                 .OrderByDescending(u => u.ProductsSold.Count)
                    .Take(10)
                    .Select(u => new UserAndProductsDTO
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Age = u.Age,
                        SoldProducts = new ProductToUsersDTO()
                        {
                            Count = u.ProductsSold.Count,
                            Products = u.ProductsSold
                            .Select(ps => new ProductToSoldDTO
                            {
                                Name = ps.Name,
                                Price = ps.Price
                            })
                            .OrderByDescending(p => p.Price)
                                .ToList()
                        }
                    })
                    .ToList()
            };

            var serializer = new XmlSerializer(typeof(UsersDTO), new XmlRootAttribute("Users"));
            serializer.Serialize(new StringWriter(sb), users, ns);

            return sb.ToString().Trim();
        }
        private static void InitializeMapper()
        {
            config = new MapperConfiguration(cfg =>
           {
               cfg.AddProfile<ProductShopProfile>();
           });

            mapper = config.CreateMapper();
        }
    }
}