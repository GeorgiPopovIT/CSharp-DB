using RealEstates.Data;
using RealEstates.Services;
using RealEstates.Services.Contracts;
using System;
using System.Text;

namespace RealEstates.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            var db = new ApplicationDbContext();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please choose a command");
                Console.WriteLine("1. Property search");
                Console.WriteLine("2. Most expensive districts");
                Console.WriteLine("3. Average price per square meter");
                Console.WriteLine("4.Top 10 Cheapes districts");
                Console.WriteLine("0. EXIT");
                string input = Console.ReadLine();
                bool parsed = int.TryParse(input, out int option);
                if (parsed && option == 0)
                {
                    break;
                }
                if (parsed && option >= 1 && option <= 4)
                {
                    switch (option)
                    {
                        case 1:
                            PropertySearch(db);
                            break;
                        case 2:
                            MostExpensiveDistricts(db);
                            break;
                        case 3:
                            AveragePricePerSquareMeter(db);
                            break;
                        case 4:
                            Top10CheapestDistricts(db);
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine("Press any key..");
                Console.ReadKey();
            }
        }

        private static void Top10CheapestDistricts(ApplicationDbContext db)
        {
            IDistrictService districtService = new DistrictsService(db);
            var cheapestDisticts = districtService.GetCheapestDistrits();
            foreach (var district in cheapestDisticts)
            {
                Console.WriteLine($"{district.Name} - {district.AveragePricePerSquareMeter:F2}, ({district.PropertiesCount})");
            }
        }

        private static void PropertySearch(ApplicationDbContext db)
        {
            Console.Write("Min price:");
            int minPrice = int.Parse(Console.ReadLine());
            Console.Write("Max price:");
            int maxPrice = int.Parse(Console.ReadLine());
            Console.Write("Min size:");
            int minSize = int.Parse(Console.ReadLine());
            Console.Write("Max size:");
            int maxSize = int.Parse(Console.ReadLine());
            IPropertiesService service = new PropertyService(db);
            var properties = service.Search(minPrice, maxPrice, minSize, maxSize);
            foreach (var property in properties)
            {
                Console.WriteLine($"{property.DistrictName}; {property.BuildingType}; {property.PropertyType} => {property.Price}€ => {property.Size}m²");
            }
        }
        private static void MostExpensiveDistricts(ApplicationDbContext db)
        {
            Console.Write("Districts count:");
            int count = int.Parse(Console.ReadLine());
            IDistrictService districtService = new DistrictsService(db);
            var districts = districtService.GetMostExpensiveDistricts(count);
            foreach (var district in districts)
            {
                Console.WriteLine($"{district.Name} => {district.AveragePricePerSquareMeter:0.00}€/m² ({district.PropertiesCount})");
            }
        }
        private static void AveragePricePerSquareMeter(ApplicationDbContext dbContext)
        {
            IPropertiesService propertiesService = new PropertyService(dbContext);
            Console.WriteLine($"Average price per Square Meter: {propertiesService.AveragePricePerSquareMeter():F2}");
        }
    }
}
