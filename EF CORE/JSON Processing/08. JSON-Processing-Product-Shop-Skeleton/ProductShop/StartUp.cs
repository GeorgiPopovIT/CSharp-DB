using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using ProductShop.Data;
using ProductShop.DTO;
using ProductShop.Models;


namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new ProductShopContext();
            //ex.2
            //var inputJson = File.ReadAllText("../../../Datasets/users.json");
            //Console.WriteLine(ImportUsers(db, inputJson));

            //ex.3
            //var inputJson = File.ReadAllText("../../../Datasets/products.json");
            //Console.WriteLine(ImportProducts(db, inputJson));

            //ex.4
            //var inputJson = File.ReadAllText("../../../Datasets/categories.json");
            //db.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Categories',RESEED,0);");
            //Console.WriteLine(ImportCategories(db, inputJson));

            //ex.5
            // var inputjson = File.ReadAllText("../../../Datasets/categories-products.json");
            //Console.WriteLine(ImportCategoryProducts(db, inputjson));

            //ex.6
            //var json = GetProductsInRange(db);
            //File.WriteAllText("../../../Results/products-in-range.json", json);

            //ex.7
            //var json = GetSoldProducts(db);
            //File.WriteAllText("../../../Results/users-sold-products.json", json);

            //ex.8
            //var json = GetCategoriesByProductsCount(db);
            //File.WriteAllText("../../../Results/categories-by-products.json", json);

            //ex.9
            var json = GetUsersWithProducts(db);
            File.WriteAllText("../../../Results/users-and-products.json", json);
        }
        //ex.2
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {

            var users = JsonConvert.DeserializeObject<List<User>>(inputJson);
            context.AddRange(users);

            var result = context.SaveChanges();
            return $"Successfully imported {result}";

        }
        //ex.3
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<List<Product>>(inputJson);
            context.AddRange(products);

            var result = context.SaveChanges();
            return $"Successfully imported {result}";
        }
        //ex.4
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert.DeserializeObject<List<Category>>(inputJson)
                .Where(p => p.Name != null);

            
            context.AddRange(categories);

            var result = context.SaveChanges();

            return $"Successfully imported {result}";
        }
        //ex.5
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var carProducts = JsonConvert
                    .DeserializeObject<List<CategoryProduct>>(inputJson);

            context.AddRange(carProducts);
            var result = context.SaveChanges();

            return $"Successfully imported {result}";
        }
        //ex.6
        public static string GetProductsInRange(ProductShopContext context)
        {
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<ProductShopProfile>();
                });

            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .ProjectTo<ProductDTO>(config)
                .OrderBy(p => p.Price).ToList();

            var json = JsonConvert.SerializeObject(products, Formatting.Indented);

            return json;
        }
        //ex.7
        //!1
        public static string GetSoldProducts(ProductShopContext context)
        {
            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<ProductShopProfile>();
            //});
            //var users = context.Users
            //    .Where(p => p.ProductsSold.Count >= 1 
            //    && p.ProductsSold.Any(b => b.Buyer != null))
            //    .OrderBy(p => p.LastName)
            //    .ThenBy(p => p.FirstName)
            //    .ProjectTo<UserDTO>(config)
            //    .ToList();

            var users = context.Users
                .Where(p => p.ProductsSold.Count >= 1
                && !p.ProductsSold.Any(ps => ps.Buyer != null))
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .Select(p => new
                {
                    p.FirstName,
                    p.LastName,
                    soldProducts = p.ProductsSold
                    .Select(ps => new
                    {
                        name = ps.Name,
                        price = ps.Price,
                        buyerFirstName = ps.Buyer.FirstName,
                        buyerLastName = ps.Buyer.LastName
                    })
                })
                .ToList();

            var resolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = resolver
            };

            var json = JsonConvert.SerializeObject(users, Formatting.Indented, settings);

            return json;
        }
        //ex.8
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories.Select(c => new
            {
                category = c.Name,
                productsCount = c.CategoryProducts.Count,
                averagePrice = c.CategoryProducts.Select(p => p.Product.Price).Average().ToString("f2"),
                totalRevenue = c.CategoryProducts.Select(p => p.Product.Price).Sum().ToString("f2")
            })
             .OrderByDescending(c => c.productsCount)
             .ToList();
            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<ProductShopProfile>();
            //});

            //var categories = context.Categories
            //       .ProjectTo<CategoryDTO>(config)
            //       .ToList();

            var json = JsonConvert.SerializeObject(categories, Formatting.Indented);


            return json;
        }
        //ex.9
        //!2
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Any(ps => ps.Buyer != null))
                .Select(u => new UserExportDTO
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = new SoldProductsDTO()
                    {
                        Count = u.ProductsSold.Count(p => p.Buyer != null),
                        Products = u.ProductsSold
                        .Where(p => p.Buyer != null)
                        .Select(p => new ProductDTO()
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                        .ToList()
                    }
                })
                .OrderByDescending(u => u.SoldProducts.Count)
                .ToList();

            var setting = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };
            var json = JsonConvert.SerializeObject(users, Formatting.Indented, setting);

            return json;
        }
    }
}