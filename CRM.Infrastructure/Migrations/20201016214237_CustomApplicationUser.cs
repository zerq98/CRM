using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Infrastructure.Migrations
{
    public partial class CustomApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PrivateEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddDate = table.Column<DateTime>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddDate = table.Column<DateTime>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddDate = table.Column<DateTime>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddDate = table.Column<DateTime>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NIP = table.Column<string>(nullable: true),
                    REGON = table.Column<string>(nullable: true),
                    KRSNumber = table.Column<string>(nullable: true),
                    DealSize = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    CustomerStatusId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    AddressDetailsId = table.Column<int>(nullable: false),
                    ContactInformationId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_CustomerStatuses_CustomerStatusId",
                        column: x => x.CustomerStatusId,
                        principalTable: "CustomerStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AddressDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddDate = table.Column<DateTime>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    BuildingNumber = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    PostCode = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressDetails_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressDetails_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerContactInformations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddDate = table.Column<DateTime>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContactInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerContactInformations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerContacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddDate = table.Column<DateTime>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    CompanyContactInformationId = table.Column<int>(nullable: false),
                    CustomerContactInformationId = table.Column<int>(nullable: true),
                    ContactDetail = table.Column<string>(nullable: true),
                    ContactTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerContacts_ContactTypes_ContactTypeId",
                        column: x => x.ContactTypeId,
                        principalTable: "ContactTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerContacts_CustomerContactInformations_CustomerContactInformationId",
                        column: x => x.CustomerContactInformationId,
                        principalTable: "CustomerContactInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "AddDate", "Code", "ModificationDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "US", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "United States" },
                    { 154, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "New Caledonia" },
                    { 155, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NZ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "New Zealand" },
                    { 156, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nicaragua" },
                    { 157, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Niger" },
                    { 158, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nigeria" },
                    { 159, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NU", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Niue" },
                    { 160, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Norfork Island" },
                    { 161, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Northern Mariana Islands" },
                    { 162, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Norway" },
                    { 163, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "OM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Oman" },
                    { 164, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pakistan" },
                    { 165, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Palau" },
                    { 153, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Netherlands Antilles" },
                    { 166, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Panama" },
                    { 168, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Paraguay" },
                    { 169, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Peru" },
                    { 170, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PH", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Philippines" },
                    { 171, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pitcairn" },
                    { 172, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Poland" },
                    { 173, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Portugal" },
                    { 174, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Puerto Rico" },
                    { 175, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "QA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Qatar" },
                    { 176, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Republic of South Sudan" },
                    { 177, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "RE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reunion" },
                    { 178, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "RO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Romania" },
                    { 179, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "RU", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Russian Federation" },
                    { 167, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Papua New Guinea" },
                    { 180, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "RW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rwanda" },
                    { 152, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Netherlands" },
                    { 150, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nauru" },
                    { 124, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "LI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Liechtenstein" },
                    { 125, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "LT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lithuania" },
                    { 126, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "LU", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Luxembourg" },
                    { 127, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Macau" },
                    { 128, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Macedonia" },
                    { 129, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Madagascar" },
                    { 130, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Malawi" },
                    { 131, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Malaysia" },
                    { 132, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MV", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maldives" },
                    { 133, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ML", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mali" },
                    { 134, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Malta" },
                    { 135, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MH", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marshall Islands" },
                    { 151, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nepal" },
                    { 136, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MQ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Martinique" },
                    { 138, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MU", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mauritius" },
                    { 139, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mayotte" },
                    { 140, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MX", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mexico" },
                    { 141, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Micronesia, Federated States of" },
                    { 142, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Moldova, Republic of" },
                    { 143, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Monaco" },
                    { 144, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mongolia" },
                    { 145, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Montserrat" },
                    { 146, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Morocco" },
                    { 147, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MZ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mozambique" },
                    { 148, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Myanmar" },
                    { 149, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Namibia" },
                    { 137, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mauritania" },
                    { 181, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "KN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Saint Kitts and Nevis" },
                    { 182, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "LC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Saint Lucia" },
                    { 183, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Saint Vincent and the Grenadines" },
                    { 215, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tokelau" },
                    { 216, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tonga" },
                    { 217, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trinidad and Tobago" },
                    { 218, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tunisia" },
                    { 219, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Turkey" },
                    { 220, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Turkmenistan" },
                    { 221, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Turks and Caicos Islands" },
                    { 222, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TV", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tuvalu" },
                    { 223, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "UG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Uganda" },
                    { 224, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "UA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ukraine" },
                    { 225, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "United Arab Emirates" },
                    { 226, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GB", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "United Kingdom" },
                    { 214, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Togo" },
                    { 227, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "UM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "United States minor outlying islands" },
                    { 229, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "UZ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Uzbekistan" },
                    { 230, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VU", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vanuatu" },
                    { 231, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vatican City State" },
                    { 232, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Venezuela" },
                    { 233, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vietnam" },
                    { 234, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Virgin Islands (British)" },
                    { 235, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Virgin Islands (U.S.)" },
                    { 236, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "WF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wallis and Futuna Islands" },
                    { 237, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EH", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Western Sahara" },
                    { 238, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "YE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yemen" },
                    { 239, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "YU", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yugoslavia" },
                    { 240, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ZR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zaire" },
                    { 228, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "UY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Uruguay" },
                    { 213, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TH", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thailand" },
                    { 212, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TZ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tanzania, United Republic of" },
                    { 211, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TJ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tajikistan" },
                    { 184, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "WS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samoa" },
                    { 185, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "San Marino" },
                    { 186, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ST", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sao Tome and Principe" },
                    { 187, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Saudi Arabia" },
                    { 188, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Senegal" },
                    { 189, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "RS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Serbia" },
                    { 190, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seychelles" },
                    { 191, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sierra Leone" },
                    { 192, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Singapore" },
                    { 193, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Slovakia" },
                    { 194, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Slovenia" },
                    { 195, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SB", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Solomon Islands" },
                    { 196, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Somalia" },
                    { 197, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ZA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "South Africa" },
                    { 198, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "South Georgia South Sandwich Islands" },
                    { 199, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ES", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spain" },
                    { 200, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "LK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sri Lanka" },
                    { 201, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SH", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "St. Helena" },
                    { 202, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "St. Pierre and Miquelon" },
                    { 203, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sudan" },
                    { 204, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Suriname" },
                    { 205, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SJ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Svalbarn and Jan Mayen Islands" },
                    { 206, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SZ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Swaziland" },
                    { 207, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sweden" },
                    { 208, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CH", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Switzerland" },
                    { 209, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Syrian Arab Republic" },
                    { 210, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Taiwan" },
                    { 123, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "LY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Libyan Arab Jamahiriya" },
                    { 122, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "LR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Liberia" },
                    { 121, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "LS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lesotho" },
                    { 120, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "LB", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lebanon" },
                    { 33, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "IO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "British lndian Ocean Territory" },
                    { 34, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brunei Darussalam" },
                    { 35, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bulgaria" },
                    { 36, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Burkina Faso" },
                    { 37, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Burundi" },
                    { 38, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "KH", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cambodia" },
                    { 39, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cameroon" },
                    { 40, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CV", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cape Verde" },
                    { 41, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "KY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cayman Islands" },
                    { 42, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Central African Republic" },
                    { 43, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chad" },
                    { 44, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chile" },
                    { 32, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brazil" },
                    { 45, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "China" },
                    { 47, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cocos (Keeling) Islands" },
                    { 48, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Colombia" },
                    { 49, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "KM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comoros" },
                    { 50, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Congo" },
                    { 51, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cook Islands" },
                    { 52, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Costa Rica" },
                    { 53, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Croatia (Hrvatska)" },
                    { 54, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CU", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cuba" },
                    { 55, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cyprus" },
                    { 56, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CZ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Czech Republic" },
                    { 57, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Democratic Republic of Congo" },
                    { 58, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Denmark" },
                    { 46, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CX", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Christmas Island" },
                    { 31, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BV", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bouvet Island" },
                    { 30, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Botswana" },
                    { 29, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bosnia and Herzegovina" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Canada" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Afghanistan" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Albania" },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DZ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Algeria" },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "American Samoa" },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Andorra" },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Angola" },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anguilla" },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Antarctica" },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Antigua and/or Barbuda" },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Argentina" },
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Armenia" },
                    { 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aruba" },
                    { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AU", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Australia" },
                    { 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Austria" },
                    { 17, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AZ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Azerbaijan" },
                    { 18, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bahamas" },
                    { 19, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BH", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bahrain" },
                    { 20, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bangladesh" },
                    { 21, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BB", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Barbados" },
                    { 22, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Belarus" },
                    { 23, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Belgium" },
                    { 24, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BZ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Belize" },
                    { 25, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BJ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Benin" },
                    { 26, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bermuda" },
                    { 27, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bhutan" },
                    { 28, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bolivia" },
                    { 59, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DJ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Djibouti" },
                    { 241, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ZM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zambia" },
                    { 60, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dominica" },
                    { 62, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "East Timor" },
                    { 94, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Haiti" },
                    { 95, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Heard and Mc Donald Islands" },
                    { 96, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Honduras" },
                    { 97, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hong Kong" },
                    { 98, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HU", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hungary" },
                    { 99, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "IS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Iceland" },
                    { 100, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "IN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "India" },
                    { 101, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ID", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Indonesia" },
                    { 102, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "IR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Iran {Islamic Republic of}" },
                    { 103, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "IQ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Iraq" },
                    { 104, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "IE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ireland" },
                    { 105, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "IL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Israel" },
                    { 93, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guyana" },
                    { 106, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "IT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Italy" },
                    { 108, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "JM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jamaica" },
                    { 109, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "JP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Japan" },
                    { 110, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "JO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jordan" },
                    { 111, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "KZ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kazakhstan" },
                    { 112, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "KE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kenya" },
                    { 113, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "KI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kiribati" },
                    { 114, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "KP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Korea, Democratic People's Republic of" },
                    { 115, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "KR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Korea, Republic of" },
                    { 116, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "KW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kuwait" },
                    { 117, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "KG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kyrgyzstan" },
                    { 118, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "LA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lao People's Democratic Republic" },
                    { 119, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "LV", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Latvia" },
                    { 107, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ivory Coast" },
                    { 92, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guinea-Bissau" },
                    { 91, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guinea" },
                    { 90, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guatemala" },
                    { 63, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ecudaor" },
                    { 64, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Egypt" },
                    { 65, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SV", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "El Salvador" },
                    { 66, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GQ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Equatorial Guinea" },
                    { 67, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ER", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eritrea" },
                    { 68, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Estonia" },
                    { 69, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ET", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ethiopia" },
                    { 70, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Falkland Islands (Malvinas)" },
                    { 71, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faroe Islands" },
                    { 72, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FJ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fiji" },
                    { 73, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Finland" },
                    { 74, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "France" },
                    { 75, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FX", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "France, Metropolitan" },
                    { 76, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "French Guiana" },
                    { 77, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "French Polynesia" },
                    { 78, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "French Southern Territories" },
                    { 79, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gabon" },
                    { 80, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gambia" },
                    { 81, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Georgia" },
                    { 82, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Germany" },
                    { 83, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GH", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ghana" },
                    { 84, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gibraltar" },
                    { 85, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Greece" },
                    { 86, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Greenland" },
                    { 87, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grenada" },
                    { 88, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guadeloupe" },
                    { 89, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GU", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guam" },
                    { 61, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dominican Republic" },
                    { 242, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ZW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zimbabwe" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressDetails_CountryId",
                table: "AddressDetails",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressDetails_CustomerId",
                table: "AddressDetails",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContactInformations_CustomerId",
                table: "CustomerContactInformations",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_ContactTypeId",
                table: "CustomerContacts",
                column: "ContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_CustomerContactInformationId",
                table: "CustomerContacts",
                column: "CustomerContactInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerStatusId",
                table: "Customers",
                column: "CustomerStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressDetails");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CustomerContacts");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ContactTypes");

            migrationBuilder.DropTable(
                name: "CustomerContactInformations");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "CustomerStatuses");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
