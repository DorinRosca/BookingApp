﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using CarBookingApp.Features.Cars.Entities;
using CarBookingApp.Features.Statuses.Entities;

namespace CarBookingApp.Features.Orders.Entities
{
    [Table("Order")]
    public class Order
    {

        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        [Required, ForeignKey(nameof(Car))]
        public int CarId { get; set; }

        public Car Car { get; set; }

        [Required, Column(TypeName = "datetime")]
        public DateTime RentalStartDate { get; set; }

        [Required, Column(TypeName = "datetime")]
        public DateTime RentalEndDate { get; set; }

        [Required, Range(0, double.MaxValue), Column(TypeName = "decimal(8,2)")]
        public double TotalAmount { get; set; }

        [Required, ForeignKey(nameof(Status))]
        public byte StatusId { get; set; }
        public Status Status { get; set; }
        public IdentityUser User { get; set; }

    }
}
