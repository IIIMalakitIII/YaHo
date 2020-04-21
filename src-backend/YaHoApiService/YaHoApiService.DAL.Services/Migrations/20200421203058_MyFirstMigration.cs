using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YaHoApiService.DAL.Services.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Phone = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 300, nullable: true),
                    Balance = table.Column<int>(nullable: true),
                    Hold = table.Column<int>(nullable: true),
                    InitialDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 300, nullable: true),
                    Rating = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    DeliveryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 300, nullable: true),
                    Rating = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.DeliveryId);
                    table.ForeignKey(
                        name: "FK_Deliveries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerReviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 300, nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    Mark = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerReviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_CustomerReviews_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerReviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InitialDate = table.Column<DateTime>(nullable: false),
                    DeliveryPlace = table.Column<string>(maxLength: 100, nullable: true),
                    DeliverDate = table.Column<DateTime>(nullable: true),
                    Bargain = table.Column<bool>(nullable: false),
                    ExpectedDate = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    Comment = table.Column<string>(maxLength: 300, nullable: true),
                    DeliveryFrom = table.Column<string>(maxLength: 100, nullable: true),
                    OrderStatus = table.Column<int>(nullable: true),
                    ExpectedDateFault = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryReviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 300, nullable: true),
                    DeliveryId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Mark = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryReviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_DeliveryReviews_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "DeliveryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryReviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderRequests",
                columns: table => new
                {
                    OrderRequestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(nullable: false),
                    DeliveryId = table.Column<int>(nullable: false),
                    Approved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRequests", x => x.OrderRequestId);
                    table.ForeignKey(
                        name: "FK_OrderRequests_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "DeliveryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderRequests_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    Tax = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 300, nullable: true),
                    Link = table.Column<string>(maxLength: 300, nullable: true),
                    ProductName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    MediaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    FilePath = table.Column<string>(maxLength: 300, nullable: true),
                    ContentType = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.MediaId);
                    table.ForeignKey(
                        name: "FK_Media_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Balance", "Description", "Email", "FirstName", "Hold", "InitialDate", "LastName", "Phone" },
                values: new object[,]
                {
                    { 1, 500, "Hello", "user_1@gmail.com", "User_1", 0, new DateTime(2020, 4, 21, 18, 30, 57, 889, DateTimeKind.Utc).AddTicks(8250), "User_1", "+380500832005" },
                    { 2, 700, "Hello", "user_2@gmail.com", "User_2", 0, new DateTime(2020, 4, 21, 17, 30, 57, 889, DateTimeKind.Utc).AddTicks(9271), "User_2", "+380500832006" },
                    { 3, 800, "Hello", "user_3@gmail.com", "User_3", null, new DateTime(2020, 4, 21, 16, 30, 57, 889, DateTimeKind.Utc).AddTicks(9308), "User_3", "+380500832007" },
                    { 4, 200, "Hello", "user_4@gmail.com", "User_4", 0, new DateTime(2020, 4, 21, 15, 30, 57, 889, DateTimeKind.Utc).AddTicks(9313), "User_4", "+380500832008" },
                    { 5, 1200, "Hello", "user_5@gmail.com", "User_5", 0, new DateTime(2020, 4, 21, 14, 30, 57, 889, DateTimeKind.Utc).AddTicks(9317), "User_5", "+380500832015" },
                    { 6, 600, "Hello", "user_6@gmail.com", "User_6", 0, new DateTime(2020, 4, 21, 13, 30, 57, 889, DateTimeKind.Utc).AddTicks(9326), "User_6", "+380500833005" },
                    { 7, 500, "Hello", "user_7@gmail.com", "User_7", null, new DateTime(2020, 4, 21, 12, 30, 57, 889, DateTimeKind.Utc).AddTicks(9329), "User_7", "+380500832105" },
                    { 8, 500, "Hello", "user_8@gmail.com", "User_8", 0, new DateTime(2020, 4, 21, 18, 30, 57, 889, DateTimeKind.Utc).AddTicks(9332), "User_8", "+180500832005" },
                    { 9, 500, "Hello", "user_9@gmail.com", "User_9", null, new DateTime(2020, 4, 21, 18, 30, 57, 889, DateTimeKind.Utc).AddTicks(9335), "User_9", "+380590832005" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Description", "Rating", "UserId" },
                values: new object[,]
                {
                    { 1, "Hello", 0, 1 },
                    { 8, "Hello", 0, 8 },
                    { 7, "Hello", 0, 7 },
                    { 6, "Hello", 0, 6 },
                    { 9, "Hello", 0, 9 },
                    { 4, "Hello", 0, 4 },
                    { 5, "Hello", 0, 5 },
                    { 3, "Hello", 0, 3 },
                    { 2, "Hello", 0, 2 }
                });

            migrationBuilder.InsertData(
                table: "Deliveries",
                columns: new[] { "DeliveryId", "Description", "Rating", "UserId" },
                values: new object[,]
                {
                    { 4, "Hello", 0, 4 },
                    { 5, "Hello", 0, 5 },
                    { 2, "Hello", 0, 2 },
                    { 6, "Hello", 0, 6 },
                    { 7, "Hello", 0, 7 },
                    { 1, "Hello", 0, 1 },
                    { 8, "Hello", 0, 8 },
                    { 3, "Hello", 0, 3 },
                    { 9, "Hello", 0, 9 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "Bargain", "Comment", "CustomerId", "DeliverDate", "DeliveryFrom", "DeliveryPlace", "ExpectedDate", "ExpectedDateFault", "InitialDate", "OrderStatus", "Title" },
                values: new object[,]
                {
                    { 1, false, "Hello", 1, null, "USA, New York", "Ukraine, Kharkov", new DateTime(2020, 4, 26, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(1021), new DateTime(2020, 4, 27, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(1987), new DateTime(2020, 4, 21, 18, 30, 57, 897, DateTimeKind.Utc).AddTicks(2681), 2, "PCR test" },
                    { 2, false, "Hello", 2, null, "USA, New York", "Ukraine, Kharkov", new DateTime(2020, 4, 26, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4108), new DateTime(2020, 4, 27, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4159), new DateTime(2020, 4, 21, 17, 30, 57, 897, DateTimeKind.Utc).AddTicks(4175), 2, "PCR test" },
                    { 3, false, "Hello", 3, null, "USA, New York", "Ukraine, Kharkov", new DateTime(2020, 4, 26, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4203), new DateTime(2020, 4, 27, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4205), new DateTime(2020, 4, 21, 16, 30, 57, 897, DateTimeKind.Utc).AddTicks(4208), 2, "PCR test" },
                    { 5, false, "Hello", 4, null, "USA, New York", "Ukraine, Kharkov", new DateTime(2020, 4, 26, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4211), new DateTime(2020, 4, 27, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4213), new DateTime(2020, 4, 21, 15, 30, 57, 897, DateTimeKind.Utc).AddTicks(4216), 2, "PCR test" },
                    { 4, false, "Hello", 4, null, "USA, New York", "Ukraine, Kharkov", new DateTime(2020, 4, 26, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4219), new DateTime(2020, 4, 27, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4221), new DateTime(2020, 4, 21, 14, 30, 57, 897, DateTimeKind.Utc).AddTicks(4223), 2, "PCR test" },
                    { 6, false, "Hello", 6, null, "USA, New York", "Ukraine, Kharkov", new DateTime(2020, 4, 26, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4235), new DateTime(2020, 4, 27, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4237), new DateTime(2020, 4, 21, 13, 30, 57, 897, DateTimeKind.Utc).AddTicks(4239), 2, "PCR test" },
                    { 7, false, "Hello", 7, null, "USA, New York", "Ukraine, Kharkov", new DateTime(2020, 4, 26, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4243), new DateTime(2020, 4, 27, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4245), new DateTime(2020, 4, 21, 12, 30, 57, 897, DateTimeKind.Utc).AddTicks(4247), 2, "PCR test" },
                    { 8, false, "Hello", 8, null, "USA, New York", "Ukraine, Kharkov", new DateTime(2020, 4, 26, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4251), new DateTime(2020, 4, 27, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4253), new DateTime(2020, 4, 21, 18, 30, 57, 897, DateTimeKind.Utc).AddTicks(4255), 2, "PCR test" },
                    { 9, false, "Hello", 9, null, "USA, New York", "Ukraine, Kharkov", new DateTime(2020, 4, 26, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4258), new DateTime(2020, 4, 27, 20, 30, 57, 897, DateTimeKind.Utc).AddTicks(4260), new DateTime(2020, 4, 21, 18, 30, 57, 897, DateTimeKind.Utc).AddTicks(4263), 2, "PCR test" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "Link", "OrderId", "Price", "ProductName", "Tax" },
                values: new object[,]
                {
                    { 1, "Hello", "Nothing", 1, 600, "PCR test", 10 },
                    { 2, "Hello", "Nothing", 2, 600, "PCR test", 10 },
                    { 3, "Hello", "Nothing", 3, 600, "PCR test", 10 },
                    { 5, "Hello", "Nothing", 5, 600, "PCR test", 10 },
                    { 4, "Hello", "Nothing", 4, 600, "PCR test", 10 },
                    { 6, "Hello", "Nothing", 6, 600, "PCR test", 10 },
                    { 7, "Hello", "Nothing", 7, 600, "PCR test", 10 },
                    { 8, "Hello", "Nothing", 8, 600, "PCR test", 10 },
                    { 9, "Hello", "Nothing", 9, 600, "PCR test", 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerReviews_CustomerId",
                table: "CustomerReviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerReviews_UserId",
                table: "CustomerReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_UserId",
                table: "Deliveries",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryReviews_DeliveryId",
                table: "DeliveryReviews",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryReviews_UserId",
                table: "DeliveryReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Media_ProductId",
                table: "Media",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRequests_DeliveryId",
                table: "OrderRequests",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRequests_OrderId",
                table: "OrderRequests",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderId",
                table: "Products",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerReviews");

            migrationBuilder.DropTable(
                name: "DeliveryReviews");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "OrderRequests");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
