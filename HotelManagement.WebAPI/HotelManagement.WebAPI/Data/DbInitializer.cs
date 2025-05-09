using System;
using System.Collections.Generic;
using System.Data.Entity;
using HotelManagement.WebAPI.Models;

namespace HotelManagement.WebAPI.Data
{
    public class DbInitializer : CreateDatabaseIfNotExists<HotelDbContext>
    {
        protected override void Seed(HotelDbContext context)
        {
            // Adăugăm hoteluri
            var hotels = new List<Hotel>
            {
                new Hotel
                {
                    Name = "Grand Plaza Hotel",
                    Address = "123 Main Street",
                    City = "Bucharest",
                    Country = "Romania",
                    Stars = 5,
                    ContactPhone = "+40-721-123-456",
                    Email = "info@grandplaza.ro",
                    IsActive = true
                },
                new Hotel
                {
                    Name = "Seaside Resort",
                    Address = "45 Beach Boulevard",
                    City = "Constanta",
                    Country = "Romania",
                    Stars = 4,
                    ContactPhone = "+40-722-345-678",
                    Email = "contact@seasideresort.ro",
                    IsActive = true
                },
                new Hotel
                {
                    Name = "Mountain View Lodge",
                    Address = "78 Pine Street",
                    City = "Brasov",
                    Country = "Romania",
                    Stars = 3,
                    ContactPhone = "+40-723-567-890",
                    Email = "info@mountainview.ro",
                    IsActive = true
                }
            };

            context.Hotels.AddRange(hotels);
            context.SaveChanges();

            // Adăugăm tipuri de camere
            var roomTypes = new List<RoomType>
            {
                new RoomType
                {
                    Name = "Single Room",
                    Description = "A room assigned to one person.",
                    MaxOccupancy = 1,
                    BasePrice = 80.00m
                },
                new RoomType
                {
                    Name = "Double Room",
                    Description = "A room assigned to two people.",
                    MaxOccupancy = 2,
                    BasePrice = 120.00m
                },
                new RoomType
                {
                    Name = "Suite",
                    Description = "A luxurious room with separate living area.",
                    MaxOccupancy = 4,
                    BasePrice = 250.00m
                }
            };

            context.RoomTypes.AddRange(roomTypes);
            context.SaveChanges();

            // Adăugăm camere
            var rooms = new List<Room>();

            // Camere pentru primul hotel
            for (int i = 1; i <= 5; i++)
            {
                rooms.Add(new Room
                {
                    RoomNumber = $"10{i}",
                    HotelId = hotels[0].Id,
                    RoomTypeId = roomTypes[0].Id,
                    Floor = 1,
                    IsClean = true,
                    IsOccupied = false,
                    NeedsRepair = false
                });
            }

            // Camere pentru al doilea hotel
            for (int i = 1; i <= 5; i++)
            {
                rooms.Add(new Room
                {
                    RoomNumber = $"20{i}",
                    HotelId = hotels[1].Id,
                    RoomTypeId = roomTypes[1].Id,
                    Floor = 2,
                    IsClean = true,
                    IsOccupied = false,
                    NeedsRepair = false
                });
            }

            context.Rooms.AddRange(rooms);
            context.SaveChanges();

            // Adăugăm clienți
            var customers = new List<Customer>
            {
                new Customer
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Phone = "+40-721-111-222",
                    Address = "123 Customer Street",
                    City = "Bucharest",
                    Country = "Romania",
                    IdNumber = "XX123456",
                    DateOfBirth = new DateTime(1985, 5, 15),
                    RegistrationDate = DateTime.Now.AddMonths(-3),
                    IsVIP = false
                },
                new Customer
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    Phone = "+40-722-333-444",
                    Address = "456 Customer Avenue",
                    City = "Cluj-Napoca",
                    Country = "Romania",
                    IdNumber = "XX789012",
                    DateOfBirth = new DateTime(1990, 8, 22),
                    RegistrationDate = DateTime.Now.AddMonths(-2),
                    IsVIP = true
                }
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();

            // Adăugăm servicii
            var services = new List<Service>
            {
                new Service
                {
                    Name = "Room Service",
                    Description = "Food and beverage service delivered to the room",
                    Price = 20.00m,
                    Category = "RoomService",
                    IsAvailable = true
                },
                new Service
                {
                    Name = "Spa Access",
                    Description = "Access to the hotel's spa facilities",
                    Price = 50.00m,
                    Category = "Spa",
                    IsAvailable = true
                },
                new Service
                {
                    Name = "Airport Transfer",
                    Description = "Transportation between hotel and airport",
                    Price = 30.00m,
                    Category = "Transportation",
                    IsAvailable = true
                }
            };

            context.Services.AddRange(services);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}