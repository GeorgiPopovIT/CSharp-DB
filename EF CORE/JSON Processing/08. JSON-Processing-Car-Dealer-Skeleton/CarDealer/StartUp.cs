using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        private static IMapper mapper;
        public static void Main(string[] args)
        {
            var dirPath = @"../../../Results";
            var db = new CarDealerContext();
            //ex.9
            //var inputJson = File.ReadAllText("../../../Datasets/suppliers.json");
            //Console.WriteLine(ImportSuppliers(db, inputJson));

            //ex.10
            //var inputJson = File.ReadAllText("../../../Datasets/parts.json");
            //Console.WriteLine(ImportParts(db, inputJson));

            //ex.11
            //var inputJson = File.ReadAllText("../../../Datasets/cars.json");
            //Console.WriteLine(ImportCars(db, inputJson));

            //еx.12
            //var inputJson = File.ReadAllText("../../../Datasets/customers.json");
            //Console.WriteLine(ImportCustomers(db, inputJson));

            //ex.13
            //var inputJson = File.ReadAllText("../../../Datasets/sales.json");
            //Console.WriteLine(ImportSales(db, inputJson));

            //ex.14
            //var json = GetOrderedCustomers(db);
            //File.WriteAllText(dirPath +"/ordered-customers.json", json);

            //ex.15
            var json = GetCarsFromMakeToyota(db);
            File.WriteAllText(dirPath + "/toyota-cars.json", json);
        }
        //ex.9
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(inputJson);
            context.Suppliers.AddRange(suppliers);

            var result = context.SaveChanges();

            return $"Successfully imported {result}.";
        }
        //ex.10
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var parts = JsonConvert.DeserializeObject<List<Part>>(inputJson);

            var suppliers = context.Suppliers.Select(s => s.Id);

            parts = parts
                .Where(p => suppliers.Any(s => s == p.SupplierId))
                .ToList();

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count}.";
        }
        //ex.11
        public static string ImportCars(CarDealerContext context, string inputJson)
        {

            var carsDtos = JsonConvert.DeserializeObject<List<CarDTO>>(inputJson);

            List<Car> cars = new List<Car>();
            foreach (var carDto in carsDtos)
            {
                Car car = new Car()
                {
                    Make = carDto.Make,
                    Model = carDto.Model,
                    TravelledDistance = carDto.TravelledDistance
                };

                foreach (int partId in carDto.PartsId.Distinct())
                {
                    car.PartCars.Add(new PartCar()
                    {
                        Car = car,
                        PartId = partId
                    });
                }

                cars.Add(car);
            }

            context.Cars.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}.";
        }
        //ex.12
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<List<Customer>>(inputJson);

            context.Customers.AddRange(customers);

            context.SaveChanges();
            return $"Successfully imported {context.Customers.Count()}.";
        }
        //ex.13
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<List<Sale>>(inputJson);

            context.Sales.AddRange(sales);

            context.SaveChanges();
            return $"Successfully imported {context.Sales.Count()}.";
        }

        //ex.14
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(c => new
                {
                    c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy"),
                    c.IsYoungDriver
                })
                .ToList();

            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var json = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return json;

        }
        //ex.15
         public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new
                {
                    c.Id,
                    c.Make,
                    c.Model,
                    c.TravelledDistance
                })
                .ToList();

            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var json = JsonConvert.SerializeObject(cars,Formatting.Indented,settings);

            return json;
        }
        //ex.16
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var carsPars = context.Cars
                .Select(c => new
                {
                    c.Make,
                    c.Model,
                    c.TravelledDistance,
                    Parts = c.PartCars.Select(p => new
                    {
                        Name = p.Part.Name,
                        Parts = p.Part.Price.ToString("f2")
                    })
                })
                .ToList();


                 var settings = new JsonSerializerSettings()
                 {
                     ContractResolver = new CamelCasePropertyNamesContractResolver()
                 };

            var json = JsonConvert.SerializeObject(carsPars, Formatting.Indented, settings);

            return json;
        }
        private static void InitializeMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });
            mapper = config.CreateMapper();
        }
    }
}