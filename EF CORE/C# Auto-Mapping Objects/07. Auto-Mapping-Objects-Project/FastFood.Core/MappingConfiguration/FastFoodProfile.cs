﻿namespace FastFood.Core.MappingConfiguration
{
    using AutoMapper;
    using FastFood.Core.ViewModels.Categories;
    using FastFood.Core.ViewModels.Employees;
    using FastFood.Core.ViewModels.Items;
    using FastFood.Core.ViewModels.Orders;
    using FastFood.Models;
    using System;
    using ViewModels.Positions;

    public class FastFoodProfile : Profile
    {
        public FastFoodProfile()
        {
            //Positions
            this.CreateMap<CreatePositionInputModel, Position>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.PositionName));

            this.CreateMap<Position, PositionsAllViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            //Orders
            this.CreateMap<CreateOrderInputModel, Order>()
                .ForMember(x => x.DateTime,y => y.MapFrom(s => DateTime.UtcNow));

            this.CreateMap<Order, OrderAllViewModel>()
                .ForMember(x => x.OrderId, y => y.MapFrom(x => x.Id));
                //.ForMember(x => x.Employee, y => y.)

            //Items
            this.CreateMap<Item, ItemsAllViewModels>();
                //.ForMember(x => x.Category, y => y.MapFrom(x => x.Category.Name));

            this.CreateMap<CreateItemInputModel, Item>();

            this.CreateMap<Category, CreateItemViewModel>()
                .ForMember(x => x.CategoryId, opt => opt.MapFrom(y => y.Id));
            //Category
            this.CreateMap<CreateCategoryInputModel, Category>()
                .ForMember(x => x.Name, y => y.MapFrom(x => x.CategoryName));

            this.CreateMap<Category, CategoryAllViewModel>();

            //Employees
            this.CreateMap<Position, RegisterEmployeeViewModel>()
                .ForMember(x => x.PositionId, y => y.MapFrom(x => x.Id));

            this.CreateMap<RegisterEmployeeInputModel,Employee>();

            this.CreateMap<Employee, EmployeesAllViewModel>()
                .ForMember(x => x.Position, y => y
                .MapFrom(x => x.Position.Name));

           
        }
    }
}