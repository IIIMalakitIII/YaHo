using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using YaHo.YaHoApiService.DAL.Data.Entities;

namespace YaHo.YaHoApiService.DAL.Services.DataBuilders
{
    class UserDataBuilder : BaseDataBuilder
    {
        public UserDataBuilder(ModelBuilder modelBuilder) : base(modelBuilder) { }

        public override void SetData()
        {
            var users = new List<UserDbo>
            {
                new UserDbo
                {
                    UserId = 1,
                    Phone = "+380500832005",
                    FirstName = "User_1",
                    LastName = "User_1",
                    Description = "Hello",
                    Email = "user_1@gmail.com",
                    Hold = 0,
                    Balance = 500,
                    InitialDate = DateTime.UtcNow.AddHours(-2)
                },
                new UserDbo
                {
                    UserId = 2,
                    Phone = "+380500832006",
                    FirstName = "User_2",
                    LastName = "User_2",
                    Description = "Hello",
                    Email = "user_2@gmail.com",
                    Hold = 0,
                    Balance = 700,
                    InitialDate = DateTime.UtcNow.AddHours(-3)
                },
                new UserDbo
                {
                    UserId = 3,
                    Phone = "+380500832007",
                    FirstName = "User_3",
                    LastName = "User_3",
                    Description = "Hello",
                    Email = "user_3@gmail.com",
                    Balance = 800,
                    InitialDate = DateTime.UtcNow.AddHours(-4)
                },
                new UserDbo
                {
                    UserId = 4,
                    Phone = "+380500832008",
                    FirstName = "User_4",
                    LastName = "User_4",
                    Description = "Hello",
                    Email = "user_4@gmail.com",
                    Hold = 0,
                    Balance = 200,
                    InitialDate = DateTime.UtcNow.AddHours(-5)
                },
                new UserDbo
                {
                    UserId = 5,
                    Phone = "+380500832015",
                    FirstName = "User_5",
                    LastName = "User_5",
                    Description = "Hello",
                    Email = "user_5@gmail.com",
                    Hold = 0,
                    Balance = 1200,
                    InitialDate = DateTime.UtcNow.AddHours(-6)
                },
                new UserDbo
                {
                    UserId = 6,
                    Phone = "+380500833005",
                    FirstName = "User_6",
                    LastName = "User_6",
                    Description = "Hello",
                    Email = "user_6@gmail.com",
                    Hold = 0,
                    Balance = 600,
                    InitialDate = DateTime.UtcNow.AddHours(-7)
                },
                new UserDbo
                {
                    UserId = 7,
                    Phone = "+380500832105",
                    FirstName = "User_7",
                    LastName = "User_7",
                    Description = "Hello",
                    Email = "user_7@gmail.com",
                    Balance = 500,
                    InitialDate = DateTime.UtcNow.AddHours(-8)
                },
                new UserDbo
                {
                    UserId = 8,
                    Phone = "+180500832005",
                    FirstName = "User_8",
                    LastName = "User_8",
                    Description = "Hello",
                    Email = "user_8@gmail.com",
                    Hold = 0,
                    Balance = 500,
                    InitialDate = DateTime.UtcNow.AddHours(-2)
                },
                new UserDbo
                {
                    UserId = 9,
                    Phone = "+380590832005",
                    FirstName = "User_9",
                    LastName = "User_9",
                    Description = "Hello",
                    Email = "user_9@gmail.com",
                    Balance = 500,
                    InitialDate = DateTime.UtcNow.AddHours(-2)
                }
            };

            ModelBuilder.Entity<UserDbo>()
                .HasData(users);
        }
    }
}
