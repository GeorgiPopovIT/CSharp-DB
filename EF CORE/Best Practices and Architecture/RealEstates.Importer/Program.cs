using RealEstates.Data;
//using RealEstates.Models;
using RealEstates.Services;
using RealEstates.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RealEstates.Importer
{
    class Program
    {
        static void Main(string[] args)
        {
            ImportJson("imot.bg-houses-Sofia-raw-data-2021-03-18.json");
            Console.WriteLine();
            ImportJson("imot.bg-raw-data-2021-03-18.json");
        }
        public static void ImportJson(string fileName)
        {
            var json = File.ReadAllText(fileName);
            var db = new ApplicationDbContext();
            IPropertiesService propertiesService = new PropertyService(db);

            var properties = JsonSerializer.Deserialize<IEnumerable<PropertyAsJson>>(json);
            // ?? db.Properties.AddRange((IEnumerable<PropertyObject>)properties);
            foreach (var jsonProp in properties)
            {
                propertiesService.Add(jsonProp.District, jsonProp.Price, jsonProp.Floor,
                    jsonProp.TotalFloors, jsonProp.Size, jsonProp.YardSize, jsonProp.Year,
                    jsonProp.Type, jsonProp.BuildingType);
            }
        }
    }
}
