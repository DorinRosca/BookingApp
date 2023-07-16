using AutoMapper;
using Car_Booking_App.Entities;
using CarBookingApp.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CarBookingApp.AutoMapper
{
     public class AutoMapperProfile:Profile
     {
          public AutoMapperProfile()
          {
               CreateMap<Drive, DriveViewModel>();
               CreateMap<DriveViewModel, Drive>();

               CreateMap<Brand, BrandViewModel>();
               CreateMap<BrandViewModel,Brand>();

               CreateMap<FuelType,FuelTypeViewModel>();
               CreateMap<FuelTypeViewModel,FuelType>();

               CreateMap<Transmission,TransmissionViewModel>();
               CreateMap<TransmissionViewModel,Transmission>();

               CreateMap<Vehicle,VehicleViewModel>();
               CreateMap<VehicleViewModel,Vehicle>();

               CreateMap<Car, CarCreateViewModel>();
               CreateMap<CarCreateViewModel,Car>();

               CreateMap<Car, CarViewModel>();
               CreateMap<CarViewModel, Car>();

               CreateMap<Car, CarEditViewModel>();
               CreateMap<CarEditViewModel, Car>();

               CreateMap<Status, StatusViewModel>();
               CreateMap<StatusViewModel,Status>();

               CreateMap<IdentityRole, RoleViewModel>();
               CreateMap<RoleViewModel,IdentityRole>();

               CreateMap<Order, OrderViewModel>();
               CreateMap<OrderViewModel, Order>();



          }
     }
}
