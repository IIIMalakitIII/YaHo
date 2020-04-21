﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YaHo.YaHoApiService.DAL.Services.Context;

namespace YaHoApiService.DAL.Services.Migrations
{
    [DbContext(typeof(YaHoContext))]
    partial class YaHoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("YaHo.YaHoApiService.DAL.Data.Entities.CustomerDbo", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CustomerId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            Description = "Hello",
                            Rating = 0,
                            UserId = 1
                        },
                        new
                        {
                            CustomerId = 2,
                            Description = "Hello",
                            Rating = 0,
                            UserId = 2
                        },
                        new
                        {
                            CustomerId = 3,
                            Description = "Hello",
                            Rating = 0,
                            UserId = 3
                        },
                        new
                        {
                            CustomerId = 4,
                            Description = "Hello",
                            Rating = 0,
                            UserId = 4
                        },
                        new
                        {
                            CustomerId = 5,
                            Description = "Hello",
                            Rating = 0,
                            UserId = 5
                        },
                        new
                        {
                            CustomerId = 6,
                            Description = "Hello",
                            Rating = 0,
                            UserId = 6
                        },
                        new
                        {
                            CustomerId = 7,
                            Description = "Hello",
                            Rating = 0,
                            UserId = 7
                        },
                        new
                        {
                            CustomerId = 8,
                            Description = "Hello",
                            Rating = 0,
                            UserId = 8
                        },
                        new
                        {
                            CustomerId = 9,
                            Description = "Hello",
                            Rating = 0,
                            UserId = 9
                        });
                });

            modelBuilder.Entity("YaHo.YaHoApiService.DAL.Data.Entities.CustomerReviewDbo", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<int?>("Mark")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReviewId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("UserId");

                    b.ToTable("CustomerReviews");
                });

            modelBuilder.Entity("YaHo.YaHoApiService.DAL.Data.Entities.DeliveryDbo", b =>
                {
                    b.Property<int>("DeliveryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("DeliveryId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Deliveries");

                    b.HasData(
                        new
                        {
                            DeliveryId = 1,
                            Description = "Hello",
                            Rating = 0,
                            UserId = 1
                        },
                        new
                        {
                            DeliveryId = 2,
                            Description = "Hello",
                            Rating = 0,
                            UserId = 2
                        },
                        new
                        {
                            DeliveryId = 3,
                            Description = "Hello",
                            Rating = 0,
                            UserId = 3
                        },
                        new
                        {
                            DeliveryId = 4,
                            Description = "Hello",
                            Rating = 0,
                            UserId = 4
                        },
                        new
                        {
                            DeliveryId = 5,
                            Description = "Hello",
                            Rating = 0,
                            UserId = 5
                        },
                        new
                        {
                            DeliveryId = 6,
                            Description = "Hello",
                            Rating = 0,
                            UserId = 6
                        },
                        new
                        {
                            DeliveryId = 7,
                            Description = "Hello",
                            Rating = 0,
                            UserId = 7
                        },
                        new
                        {
                            DeliveryId = 8,
                            Description = "Hello",
                            Rating = 0,
                            UserId = 8
                        },
                        new
                        {
                            DeliveryId = 9,
                            Description = "Hello",
                            Rating = 0,
                            UserId = 9
                        });
                });

            modelBuilder.Entity("YaHo.YaHoApiService.DAL.Data.Entities.DeliveryReviewDbo", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DeliveryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<int?>("Mark")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReviewId");

                    b.HasIndex("DeliveryId");

                    b.HasIndex("UserId");

                    b.ToTable("DeliveryReviews");
                });

            modelBuilder.Entity("YaHo.YaHoApiService.DAL.Data.Entities.MediaDbo", b =>
                {
                    b.Property<int>("MediaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentType")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("MediaId");

                    b.HasIndex("ProductId");

                    b.ToTable("Media");
                });

            modelBuilder.Entity("YaHo.YaHoApiService.DAL.Data.Entities.OrderDbo", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Bargain")
                        .HasColumnType("bit");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeliverDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeliveryFrom")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("DeliveryPlace")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("ExpectedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpectedDateFault")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("InitialDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            Bargain = false,
                            Comment = "Hello",
                            CustomerId = 1,
                            DeliveryFrom = "USA, New York",
                            DeliveryPlace = "Ukraine, Kharkov",
                            ExpectedDate = new DateTime(2020, 4, 26, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(1021),
                            ExpectedDateFault = new DateTime(2020, 4, 27, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(1987),
                            InitialDate = new DateTime(2020, 4, 21, 18, 30, 57, 897, DateTimeKind.Utc).AddTicks(2681),
                            OrderStatus = 2,
                            Title = "PCR test"
                        },
                        new
                        {
                            OrderId = 2,
                            Bargain = false,
                            Comment = "Hello",
                            CustomerId = 2,
                            DeliveryFrom = "USA, New York",
                            DeliveryPlace = "Ukraine, Kharkov",
                            ExpectedDate = new DateTime(2020, 4, 26, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4108),
                            ExpectedDateFault = new DateTime(2020, 4, 27, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4159),
                            InitialDate = new DateTime(2020, 4, 21, 17, 30, 57, 897, DateTimeKind.Utc).AddTicks(4175),
                            OrderStatus = 2,
                            Title = "PCR test"
                        },
                        new
                        {
                            OrderId = 3,
                            Bargain = false,
                            Comment = "Hello",
                            CustomerId = 3,
                            DeliveryFrom = "USA, New York",
                            DeliveryPlace = "Ukraine, Kharkov",
                            ExpectedDate = new DateTime(2020, 4, 26, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4203),
                            ExpectedDateFault = new DateTime(2020, 4, 27, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4205),
                            InitialDate = new DateTime(2020, 4, 21, 16, 30, 57, 897, DateTimeKind.Utc).AddTicks(4208),
                            OrderStatus = 2,
                            Title = "PCR test"
                        },
                        new
                        {
                            OrderId = 5,
                            Bargain = false,
                            Comment = "Hello",
                            CustomerId = 4,
                            DeliveryFrom = "USA, New York",
                            DeliveryPlace = "Ukraine, Kharkov",
                            ExpectedDate = new DateTime(2020, 4, 26, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4211),
                            ExpectedDateFault = new DateTime(2020, 4, 27, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4213),
                            InitialDate = new DateTime(2020, 4, 21, 15, 30, 57, 897, DateTimeKind.Utc).AddTicks(4216),
                            OrderStatus = 2,
                            Title = "PCR test"
                        },
                        new
                        {
                            OrderId = 4,
                            Bargain = false,
                            Comment = "Hello",
                            CustomerId = 4,
                            DeliveryFrom = "USA, New York",
                            DeliveryPlace = "Ukraine, Kharkov",
                            ExpectedDate = new DateTime(2020, 4, 26, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4219),
                            ExpectedDateFault = new DateTime(2020, 4, 27, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4221),
                            InitialDate = new DateTime(2020, 4, 21, 14, 30, 57, 897, DateTimeKind.Utc).AddTicks(4223),
                            OrderStatus = 2,
                            Title = "PCR test"
                        },
                        new
                        {
                            OrderId = 6,
                            Bargain = false,
                            Comment = "Hello",
                            CustomerId = 6,
                            DeliveryFrom = "USA, New York",
                            DeliveryPlace = "Ukraine, Kharkov",
                            ExpectedDate = new DateTime(2020, 4, 26, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4235),
                            ExpectedDateFault = new DateTime(2020, 4, 27, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4237),
                            InitialDate = new DateTime(2020, 4, 21, 13, 30, 57, 897, DateTimeKind.Utc).AddTicks(4239),
                            OrderStatus = 2,
                            Title = "PCR test"
                        },
                        new
                        {
                            OrderId = 7,
                            Bargain = false,
                            Comment = "Hello",
                            CustomerId = 7,
                            DeliveryFrom = "USA, New York",
                            DeliveryPlace = "Ukraine, Kharkov",
                            ExpectedDate = new DateTime(2020, 4, 26, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4243),
                            ExpectedDateFault = new DateTime(2020, 4, 27, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4245),
                            InitialDate = new DateTime(2020, 4, 21, 12, 30, 57, 897, DateTimeKind.Utc).AddTicks(4247),
                            OrderStatus = 2,
                            Title = "PCR test"
                        },
                        new
                        {
                            OrderId = 8,
                            Bargain = false,
                            Comment = "Hello",
                            CustomerId = 8,
                            DeliveryFrom = "USA, New York",
                            DeliveryPlace = "Ukraine, Kharkov",
                            ExpectedDate = new DateTime(2020, 4, 26, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4251),
                            ExpectedDateFault = new DateTime(2020, 4, 27, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4253),
                            InitialDate = new DateTime(2020, 4, 21, 18, 30, 57, 897, DateTimeKind.Utc).AddTicks(4255),
                            OrderStatus = 2,
                            Title = "PCR test"
                        },
                        new
                        {
                            OrderId = 9,
                            Bargain = false,
                            Comment = "Hello",
                            CustomerId = 9,
                            DeliveryFrom = "USA, New York",
                            DeliveryPlace = "Ukraine, Kharkov",
                            ExpectedDate = new DateTime(2020, 4, 26, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4258),
                            ExpectedDateFault = new DateTime(2020, 4, 27, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4260),
                            InitialDate = new DateTime(2020, 4, 21, 18, 30, 57, 897, DateTimeKind.Utc).AddTicks(4263),
                            OrderStatus = 2,
                            Title = "PCR test"
                        });
                });

            modelBuilder.Entity("YaHo.YaHoApiService.DAL.Data.Entities.OrderRequestDbo", b =>
                {
                    b.Property<int>("OrderRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Approved")
                        .HasColumnType("bit");

                    b.Property<int>("DeliveryId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("OrderRequestId");

                    b.HasIndex("DeliveryId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderRequests");
                });

            modelBuilder.Entity("YaHo.YaHoApiService.DAL.Data.Entities.ProductDbo", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("Tax")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("OrderId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            Description = "Hello",
                            Link = "Nothing",
                            OrderId = 1,
                            Price = 600,
                            ProductName = "PCR test",
                            Tax = 10
                        },
                        new
                        {
                            ProductId = 2,
                            Description = "Hello",
                            Link = "Nothing",
                            OrderId = 2,
                            Price = 600,
                            ProductName = "PCR test",
                            Tax = 10
                        },
                        new
                        {
                            ProductId = 3,
                            Description = "Hello",
                            Link = "Nothing",
                            OrderId = 3,
                            Price = 600,
                            ProductName = "PCR test",
                            Tax = 10
                        },
                        new
                        {
                            ProductId = 4,
                            Description = "Hello",
                            Link = "Nothing",
                            OrderId = 4,
                            Price = 600,
                            ProductName = "PCR test",
                            Tax = 10
                        },
                        new
                        {
                            ProductId = 5,
                            Description = "Hello",
                            Link = "Nothing",
                            OrderId = 5,
                            Price = 600,
                            ProductName = "PCR test",
                            Tax = 10
                        },
                        new
                        {
                            ProductId = 6,
                            Description = "Hello",
                            Link = "Nothing",
                            OrderId = 6,
                            Price = 600,
                            ProductName = "PCR test",
                            Tax = 10
                        },
                        new
                        {
                            ProductId = 7,
                            Description = "Hello",
                            Link = "Nothing",
                            OrderId = 7,
                            Price = 600,
                            ProductName = "PCR test",
                            Tax = 10
                        },
                        new
                        {
                            ProductId = 8,
                            Description = "Hello",
                            Link = "Nothing",
                            OrderId = 8,
                            Price = 600,
                            ProductName = "PCR test",
                            Tax = 10
                        },
                        new
                        {
                            ProductId = 9,
                            Description = "Hello",
                            Link = "Nothing",
                            OrderId = 9,
                            Price = 600,
                            ProductName = "PCR test",
                            Tax = 10
                        });
                });

            modelBuilder.Entity("YaHo.YaHoApiService.DAL.Data.Entities.UserDbo", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Balance")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("Hold")
                        .HasColumnType("int");

                    b.Property<DateTime>("InitialDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Balance = 500,
                            Description = "Hello",
                            Email = "user_1@gmail.com",
                            FirstName = "User_1",
                            Hold = 0,
                            InitialDate = new DateTime(2020, 4, 21, 18, 30, 57, 889, DateTimeKind.Utc).AddTicks(8250),
                            LastName = "User_1",
                            Phone = "+380500832005"
                        },
                        new
                        {
                            UserId = 2,
                            Balance = 700,
                            Description = "Hello",
                            Email = "user_2@gmail.com",
                            FirstName = "User_2",
                            Hold = 0,
                            InitialDate = new DateTime(2020, 4, 21, 17, 30, 57, 889, DateTimeKind.Utc).AddTicks(9271),
                            LastName = "User_2",
                            Phone = "+380500832006"
                        },
                        new
                        {
                            UserId = 3,
                            Balance = 800,
                            Description = "Hello",
                            Email = "user_3@gmail.com",
                            FirstName = "User_3",
                            InitialDate = new DateTime(2020, 4, 21, 16, 30, 57, 889, DateTimeKind.Utc).AddTicks(9308),
                            LastName = "User_3",
                            Phone = "+380500832007"
                        },
                        new
                        {
                            UserId = 4,
                            Balance = 200,
                            Description = "Hello",
                            Email = "user_4@gmail.com",
                            FirstName = "User_4",
                            Hold = 0,
                            InitialDate = new DateTime(2020, 4, 21, 15, 30, 57, 889, DateTimeKind.Utc).AddTicks(9313),
                            LastName = "User_4",
                            Phone = "+380500832008"
                        },
                        new
                        {
                            UserId = 5,
                            Balance = 1200,
                            Description = "Hello",
                            Email = "user_5@gmail.com",
                            FirstName = "User_5",
                            Hold = 0,
                            InitialDate = new DateTime(2020, 4, 21, 14, 30, 57, 889, DateTimeKind.Utc).AddTicks(9317),
                            LastName = "User_5",
                            Phone = "+380500832015"
                        },
                        new
                        {
                            UserId = 6,
                            Balance = 600,
                            Description = "Hello",
                            Email = "user_6@gmail.com",
                            FirstName = "User_6",
                            Hold = 0,
                            InitialDate = new DateTime(2020, 4, 21, 13, 30, 57, 889, DateTimeKind.Utc).AddTicks(9326),
                            LastName = "User_6",
                            Phone = "+380500833005"
                        },
                        new
                        {
                            UserId = 7,
                            Balance = 500,
                            Description = "Hello",
                            Email = "user_7@gmail.com",
                            FirstName = "User_7",
                            InitialDate = new DateTime(2020, 4, 21, 12, 30, 57, 889, DateTimeKind.Utc).AddTicks(9329),
                            LastName = "User_7",
                            Phone = "+380500832105"
                        },
                        new
                        {
                            UserId = 8,
                            Balance = 500,
                            Description = "Hello",
                            Email = "user_8@gmail.com",
                            FirstName = "User_8",
                            Hold = 0,
                            InitialDate = new DateTime(2020, 4, 21, 18, 30, 57, 889, DateTimeKind.Utc).AddTicks(9332),
                            LastName = "User_8",
                            Phone = "+180500832005"
                        },
                        new
                        {
                            UserId = 9,
                            Balance = 500,
                            Description = "Hello",
                            Email = "user_9@gmail.com",
                            FirstName = "User_9",
                            InitialDate = new DateTime(2020, 4, 21, 18, 30, 57, 889, DateTimeKind.Utc).AddTicks(9335),
                            LastName = "User_9",
                            Phone = "+380590832005"
                        });
                });

            modelBuilder.Entity("YaHo.YaHoApiService.DAL.Data.Entities.CustomerDbo", b =>
                {
                    b.HasOne("YaHo.YaHoApiService.DAL.Data.Entities.UserDbo", "User")
                        .WithOne("Customer")
                        .HasForeignKey("YaHo.YaHoApiService.DAL.Data.Entities.CustomerDbo", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("YaHo.YaHoApiService.DAL.Data.Entities.CustomerReviewDbo", b =>
                {
                    b.HasOne("YaHo.YaHoApiService.DAL.Data.Entities.CustomerDbo", "Customer")
                        .WithMany("CustomerReviews")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("YaHo.YaHoApiService.DAL.Data.Entities.UserDbo", "User")
                        .WithMany("CustomerReviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("YaHo.YaHoApiService.DAL.Data.Entities.DeliveryDbo", b =>
                {
                    b.HasOne("YaHo.YaHoApiService.DAL.Data.Entities.UserDbo", "User")
                        .WithOne("Delivery")
                        .HasForeignKey("YaHo.YaHoApiService.DAL.Data.Entities.DeliveryDbo", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("YaHo.YaHoApiService.DAL.Data.Entities.DeliveryReviewDbo", b =>
                {
                    b.HasOne("YaHo.YaHoApiService.DAL.Data.Entities.DeliveryDbo", "Delivery")
                        .WithMany("DeliveryReviews")
                        .HasForeignKey("DeliveryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("YaHo.YaHoApiService.DAL.Data.Entities.UserDbo", "User")
                        .WithMany("DeliveryReviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("YaHo.YaHoApiService.DAL.Data.Entities.MediaDbo", b =>
                {
                    b.HasOne("YaHo.YaHoApiService.DAL.Data.Entities.ProductDbo", "Product")
                        .WithMany("Media")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("YaHo.YaHoApiService.DAL.Data.Entities.OrderDbo", b =>
                {
                    b.HasOne("YaHo.YaHoApiService.DAL.Data.Entities.CustomerDbo", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("YaHo.YaHoApiService.DAL.Data.Entities.OrderRequestDbo", b =>
                {
                    b.HasOne("YaHo.YaHoApiService.DAL.Data.Entities.DeliveryDbo", "Delivery")
                        .WithMany("OrderRequests")
                        .HasForeignKey("DeliveryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("YaHo.YaHoApiService.DAL.Data.Entities.OrderDbo", "Order")
                        .WithMany("OrderRequests")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("YaHo.YaHoApiService.DAL.Data.Entities.ProductDbo", b =>
                {
                    b.HasOne("YaHo.YaHoApiService.DAL.Data.Entities.OrderDbo", "Order")
                        .WithMany("Products")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
