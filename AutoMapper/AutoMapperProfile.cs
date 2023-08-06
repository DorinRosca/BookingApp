using AutoMapper;
using CarBookingApp.Features.Brands.Command.Create;
using CarBookingApp.Features.Brands.Command.Edit;
using CarBookingApp.Features.Brands.Entities;
using CarBookingApp.Features.Brands.ViewModel;
using CarBookingApp.Features.Cars.Command.Create;
using CarBookingApp.Features.Cars.Command.Edit;
using CarBookingApp.Features.Cars.Entities;
using CarBookingApp.Features.Cars.ViewModel;
using CarBookingApp.Features.Drives.Command.Create;
using CarBookingApp.Features.Drives.Command.Edit;
using CarBookingApp.Features.Drives.Entities;
using CarBookingApp.Features.Drives.ViewModel;
using CarBookingApp.Features.FuelTypes.Command.Create;
using CarBookingApp.Features.FuelTypes.Command.Edit;
using CarBookingApp.Features.FuelTypes.Entities;
using CarBookingApp.Features.FuelTypes.ViewModel;
using CarBookingApp.Features.Orders.Command.Create;
using CarBookingApp.Features.Orders.Entities;
using CarBookingApp.Features.Orders.ViewModel;
using CarBookingApp.Features.Roles.Command.Create;
using CarBookingApp.Features.Roles.Command.Edit;
using CarBookingApp.Features.Roles.ViewModel;
using CarBookingApp.Features.Statuses.Command.Create;
using CarBookingApp.Features.Statuses.Command.Edit;
using CarBookingApp.Features.Statuses.Entities;
using CarBookingApp.Features.Statuses.ViewModel;
using CarBookingApp.Features.Transmissions.Command.Create;
using CarBookingApp.Features.Transmissions.Command.Edit;
using CarBookingApp.Features.Transmissions.Entities;
using CarBookingApp.Features.Transmissions.ViewModel;
using CarBookingApp.Features.Vehicles.Command.Create;
using CarBookingApp.Features.Vehicles.Command.Edit;
using CarBookingApp.Features.Vehicles.Entities;
using CarBookingApp.Features.Vehicles.ViewModel;
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
               CreateMap<CreateCarCommand,Car>();

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

               CreateMap<StatusViewModel, CreateStatusCommand>();
               CreateMap<EditStatusCommand, Status>();

               CreateMap<VehicleViewModel, CreateVehicleCommand>();
               CreateMap<CreateVehicleCommand,Vehicle>();
               CreateMap<EditVehicleCommand, Vehicle>();

               CreateMap<TransmissionViewModel, CreateTransmissionCommand>();
               CreateMap<CreateTransmissionCommand, Transmission>();
               CreateMap<EditTransmissionCommand, Transmission>();

               CreateMap<FuelTypeViewModel, CreateFuelTypeCommand>();
               CreateMap<CreateFuelTypeCommand, FuelType>();
               CreateMap<EditFuelTypeCommand, FuelType>();

               CreateMap<BrandViewModel, CreateBrandCommand>();
               CreateMap<CreateBrandCommand, Brand>();
               CreateMap<EditBrandCommand, Brand>();

               CreateMap<DriveViewModel, CreateDriveCommand>();
               CreateMap<CreateDriveCommand, Drive>();
               CreateMap<EditDriveCommand, Drive>();

               CreateMap<EditCarCommand, Car>();

               CreateMap<CreateRoleCommand, IdentityRole>();
               CreateMap<EditRoleCommand, IdentityRole>();

               
               CreateMap<CreateOrderCommand, Order>();

          }
     }
}
