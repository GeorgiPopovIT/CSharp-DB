using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.DTO.Export;
using CarDealer.DTO.Import;
using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        private static MapperConfiguration config;
        private static IMapper mapper;
        public static void Main(string[] args)
        {
            var db = new CarDealerContext();
            //var pathRead = "../../../Datasets/";
            //InitializeMapper();
            //ex.9
            //var xml = File.ReadAllText(pathRead +"suppliers.xml");
            //Console.WriteLine(ImportSuppliers(db, xml));

            //ex.10
            //var xml = File.ReadAllText(pathRead + "parts.xml");
            //Console.WriteLine(ImportParts(db, xml));

            //ex.11
            //var xml = File.ReadAllText(pathRead + "cars.xml");
            //Console.WriteLine(ImportCars(db, xml));

            //ex.12
            //var xml = File.ReadAllText(pathRead + "customers.xml");
            //Console.WriteLine(ImportCustomers(db,xml));

            //ex.13
            //var xml = File.ReadAllText(pathRead + "sales.xml");
            //Console.WriteLine(ImportSales(db,xml));

            //ex.14
            //var xml = GetCarsWithDistance(db);
            //File.WriteAllText("cars.xml",xml);

            //ex.15
            //var xml = GetCarsFromMakeBmw(db);
            //File.WriteAllText("bmw-cars.xml", xml);

            //ex.16
            //var xml = GetLocalSuppliers(db);
            //File.WriteAllText("local-suppliers.xml", xml);

            //ex.17
            //var xml = GetCarsWithTheirListOfParts(db);
            //File.WriteAllText("cars-and-parts.xml", xml);

            //ex.18
            //var xml = GetTotalSalesByCustomer(db);
            //File.WriteAllText("customers-total-sales.xml", xml);

            //ex.19
            //var xml = GetSalesWithAppliedDiscount(db);
            //File.WriteAllText("sales-discounts.xml", xml);
        }
        //ex.9
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(List<SuppliersImport>), new XmlRootAttribute("Suppliers"));

            var suppliersDtos = (List<SuppliersImport>)serializer.Deserialize(new StringReader(inputXml));

            var suppliers = mapper.Map<List<Supplier>>(suppliersDtos);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {context.Suppliers.Count()}";

        }
        //ex.10
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            InitializeMapper();
            var serializer = new XmlSerializer(typeof(List<PartDTO>), new XmlRootAttribute("Parts"));

            var partsDtos = (List<PartDTO>)serializer.Deserialize(new StringReader(inputXml));

            var suppliers = context.Suppliers.Select(s => s.Id);
            var parts = mapper.Map<List<Part>>(partsDtos).Where(p => suppliers.Contains(p.SupplierId));

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {context.Parts.Count()}";
        }
        //ex.11
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            //InitializeMapper();
            var serializer = new XmlSerializer(typeof(List<CarDTO>), new XmlRootAttribute("Cars"));

            var carsDto = (List<CarDTO>)serializer.Deserialize(new StringReader(inputXml));

            var allParts = context.Parts.Select(x => x.Id).Distinct().ToList();

            var cars = new List<Car>();

            foreach (var currentCar in carsDto)
            {
                var distinctParts = currentCar.Parts.Select(p => p.PartId).Distinct();
                var parts = distinctParts.Intersect(allParts);

                var car = new Car
                {
                    Make = currentCar.Make,
                    Model = currentCar.Model,
                    TravelledDistance = currentCar.TraveledDistance
                };

                foreach (var part in parts)
                {
                    var partCar = new PartCar
                    {
                        PartId = part
                    };
                    car.PartCars.Add(partCar);
                }
                cars.Add(car);
            }

            //var cars = mapper.Map<List<Car>>(carsDto);


            context.Cars.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {context.Cars.Count()}";
        }
        //ex.12
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            InitializeMapper();

            var serializer = new XmlSerializer(typeof(List<CustomerDTO>), new XmlRootAttribute("Customers"));
            var customersDtos = (List<CustomerDTO>)serializer.Deserialize(new StringReader(inputXml));

            var customers = mapper.Map<List<Customer>>(customersDtos);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}";
        }
        //ex.13
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            InitializeMapper();

            var serializer = new XmlSerializer(typeof(List<SalesDTO>), new XmlRootAttribute("Sales"));
            var salesDtos = (List<SalesDTO>)serializer.Deserialize(new StringReader(inputXml));

            var carsIds = context.Cars.Select(c => c.Id);

            var sales = mapper.Map<List<Sale>>(salesDtos)
                .Where(s => carsIds.Contains(s.CarId)).ToList();

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}";
        }
        //ex.14
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            StringBuilder sb = new StringBuilder();

            InitializeMapper();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            var seriliazer = new XmlSerializer(typeof(List<AllCarsDTO>), new XmlRootAttribute("cars")); ;


            var allCars = context.Cars
                .Where(c => c.TravelledDistance > 2000000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .ProjectTo<AllCarsDTO>(config)
                .ToList();

            seriliazer.Serialize(new StringWriter(sb), allCars, ns);

            return sb.ToString().Trim();
        }
        //ex.15
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            InitializeMapper();
            StringBuilder sb = new StringBuilder();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            var bmws = context.Cars
                .Where(c => c.Make == "BMW")
                .ProjectTo<BmwDTO>(config)
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ToList();

            var seriliazer = new XmlSerializer(typeof(List<BmwDTO>), new XmlRootAttribute("cars"));
            seriliazer.Serialize(new StringWriter(sb), bmws, ns);

            return sb.ToString().Trim();
        }
        //ex.16
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            //InitializeMapper();
            StringBuilder sb = new StringBuilder();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            //var suppliers = context.Suppliers
            //    .Where(s => s.IsImporter == false)
            //    .ProjectTo<SuppliersDTO>(config)
            //    .ToArray();
            var suppliers = context.Suppliers
                .Where(s => !s.IsImporter)
                .Select(s => new SuppliersDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToArray();

            var serializer = new XmlSerializer(typeof(SuppliersDTO[]), new XmlRootAttribute("suppliers"));
            serializer.Serialize(new StringWriter(sb), suppliers, ns);


            return sb.ToString().Trim();
        }
        //ex.17
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            // InitializeMapper();
            StringBuilder sb = new StringBuilder();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            var cars = context.Cars
               .Select(c => new CarOuputModel
               {
                   Make = c.Make,
                   Model = c.Model,
                   TravelledDistance = c.TravelledDistance,
                   Parts = c.PartCars.Select(pc => new PartOutputDTO
                   {
                       Name = pc.Part.Name,
                       Price = pc.Part.Price
                   })
                   .OrderByDescending(pc => pc.Price)
                   .ToList()
               })
               .OrderByDescending(c => c.TravelledDistance)
               .ThenBy(c => c.Model)
               .Take(5)
               .ToList();


            var serializer = new XmlSerializer(typeof(List<CarOuputModel>), new XmlRootAttribute("cars"));
            serializer.Serialize(new StringWriter(sb), cars, ns);


            return sb.ToString().Trim();
        }
        //ex.18
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            //InitializeMapper();
            StringBuilder sb = new StringBuilder();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            //var customers = context.Customers
            //     .Where(c => c.Sales.Any())
            //    .ProjectTo<CustomersOuputModel>(config)
            //    .OrderByDescending(c => c.SpentMoney)
            //    .ToList();
            var customers = context.Customers
                .Where(c => c.Sales.Any())
                .Select(c => new CustomersOuputModel
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count,
                    SpentMoney = c.Sales.Select(x => x.Car)
                                            .SelectMany(x => x.PartCars)
                                                .Sum(x => x.Part.Price)
                })
                .OrderByDescending(c => c.SpentMoney)
                .ToList();

            var serializer = new XmlSerializer(typeof(List<CustomersOuputModel>), new XmlRootAttribute("customers"));
            serializer.Serialize(new StringWriter(sb), customers, ns);


            return sb.ToString().Trim();
        }
        //ex.19
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            InitializeMapper();
            StringBuilder sb = new StringBuilder();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            var sales = context.Sales
                .Select(s => new SalesOuputModel
                {
                    Car = new CarOuputDTO
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    Discount = s.Discount,
                    CustomerName = s.Customer.Name,
                    Price = s.Car.PartCars.Sum(pc => pc.Part.Price),
                    PriceWithDiscount = s.Car.PartCars.Sum(pc => pc.Part.Price)
                    - s.Car.PartCars.Sum(pc => pc.Part.Price) * s.Discount / 100m
                })
                .ToList();

            var serializer = new XmlSerializer(typeof(List<SalesOuputModel>), new XmlRootAttribute("sales"));
            serializer.Serialize(new StringWriter(sb), sales, ns);


            return sb.ToString().Trim();
        }
        private static void InitializeMapper()
        {
            config = new MapperConfiguration(cfg =>
           {
               cfg.AddProfile<CarDealerProfile>();
           });
            mapper = config.CreateMapper();
        }
    }
}