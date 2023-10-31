using AutoMapper;
using Booking.Application.Features.Brand;
using Booking.Application.Features.Brand.Commands.Add;
using Booking.Application.Features.Car;
using Booking.Application.Features.Car.Commands.Add;
using Booking.Application.Features.Car.Commands.Update;
using Booking.Application.Features.Drive;
using Booking.Application.Features.FuelType;
using Booking.Application.Features.Order;
using Booking.Application.Features.Order.Commands.Add;
using Booking.Application.Features.Role;
using Booking.Application.Features.Status;
using Booking.Application.Features.Transmission;
using Booking.Application.Features.User;
using Booking.Application.Features.Vehicle;
using Booking.Application.Features.Vehicle.Commands.Add;
using Booking.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Booking.Application.AutoMapper
{
    public class AutoMapperProfile:Profile
     {
          public AutoMapperProfile()
          {
               //Brand
               CreateMap<AddBrandCommand, Brand>().ReverseMap();
               CreateMap<Brand, BrandModel>().ReverseMap();

               //Car
               CreateMap<AddCarCommand, Car>().ReverseMap();
               CreateMap<Car, CarModel>().ReverseMap();
               CreateMap<CarModel, CarViewModel>().ReverseMap();
               CreateMap<Car, CarViewModel>().ReverseMap();
               CreateMap<UpdateCarCommand, Car>().ReverseMap();
               //Drive
               CreateMap<Drive, DriveModel>().ReverseMap();
               //FuelType
               CreateMap<FuelType, FuelTypeModel>().ReverseMap();
               //Transmission
               CreateMap<Transmission, TransmissionModel>().ReverseMap();
               //Vehicle
               CreateMap<Vehicle, VehicleModel>().ReverseMap();
               //Role
               CreateMap<IdentityRole, RoleModel>().ReverseMap();
               //Status
               CreateMap<Status, StatusModel>().ReverseMap();
               //Vehicle
               CreateMap<AddVehicleCommand, Vehicle>().ReverseMap();
               //Order
               CreateMap<AddOrderCommand, Order>().ReverseMap();
               CreateMap<AddOrderCommand, OrderModel>().ReverseMap();
               CreateMap<Order, OrderModel>().ReverseMap();
               CreateMap<Order, OrderModel>()
                    .ForMember(dest => dest.Car, opt => opt.MapFrom(src => src.Car))
                    .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

               //User
               CreateMap<ApplicationUser, UserModel>();

          }
     }
}
