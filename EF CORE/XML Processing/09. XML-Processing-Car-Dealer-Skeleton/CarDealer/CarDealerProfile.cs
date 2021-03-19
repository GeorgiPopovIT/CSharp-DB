using AutoMapper;
using CarDealer.DTO.Export;
using CarDealer.DTO.Import;
using CarDealer.Models;
using System.Collections.Generic;
using System.Linq;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            //ex.9
            this.CreateMap<SuppliersImport, Supplier>();
            //ex.10
            this.CreateMap<PartDTO, Part>();
            //ex.11
            //this.CreateMap<CarPartId, CarDTO>();
            //ex.12
            this.CreateMap<CustomerDTO, Customer>();
            //ex.13
            this.CreateMap<SalesDTO, Sale>();
            //ex.14
            this.CreateMap<Car, AllCarsDTO>();
            //ex.15
            this.CreateMap<Car, BmwDTO>().ReverseMap();
            //ex.16
            this.CreateMap<Supplier, SuppliersDTO>()
                .ForMember(x => x.PartsCount, y => y.MapFrom(z => z.Parts.Count));
            //ex.17
            //this.CreateMap<Part, PartOutputDTO>();
            //this.CreateMap<PartOutputDTO, CarOuputModel>();

            //this.CreateMap<Car, CarOuputModel>();
            //ex.18
            this.CreateMap<Customer, CustomersOuputModel>()
                .ForMember(c => c.FullName, x => x.MapFrom(y => y.Name))
                .ForMember(c => c.BoughtCars, x => x.MapFrom(y => y.Sales.Count));
            this.CreateMap<Sale, CustomersOuputModel>()
                .ForMember(c => c.SpentMoney, y => y.MapFrom(z =>z.Car.PartCars.Sum(pc => pc.Part.Price)));


        }
    }
}
