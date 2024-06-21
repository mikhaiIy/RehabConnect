using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RehabConnect.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSupports",
                columns: table => new
                {
                    CSID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CSName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CSIC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CSAge = table.Column<int>(type: "int", nullable: false),
                    CSSex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CSReligion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CSNationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CSPhoneNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CSAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CSEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSupports", x => x.CSID);
                });

            migrationBuilder.CreateTable(
                name: "ParentDetails",
                columns: table => new
                {
                    ParentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherPhoneNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherIC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherRace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherOccupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherPostcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherWorkAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherPhoneNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherIC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherRace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherOccupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherPostcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherWorkAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HouseholdIncome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentDetails", x => x.ParentID);
                });

            migrationBuilder.CreateTable(
                name: "Roadmap",
                columns: table => new
                {
                    RoadmapId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roadmap", x => x.RoadmapId);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleID);
                });

            migrationBuilder.CreateTable(
                name: "Therapists",
                columns: table => new
                {
                    TherapistID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TherapistName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TherapistIC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TherapistAge = table.Column<int>(type: "int", nullable: false),
                    TherapistSex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TherapistReligion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TherapistNationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TherapistPhoneNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TherapistAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TherapistEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Therapists", x => x.TherapistID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Invoices",
                columns: table => new
                {
                    InvoiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ParentID = table.Column<int>(type: "int", nullable: false),
                    DateIssued = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceID);
                    table.ForeignKey(
                        name: "FK_Invoices_ParentDetails_ParentID",
                        column: x => x.ParentID,
                        principalTable: "ParentDetails",
                        principalColumn: "ParentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChildName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChildIC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChildAge = table.Column<int>(type: "int", nullable: false),
                    ChildDOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChildPassportNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildNationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChildRace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChildBirthPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChildSex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChildAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChildPostcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChildCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChildCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pediatricians = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HospReccommendation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeadlineDiagnose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosisOrCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OccupationalTheraphyPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpeechTheoryPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OthersUnitPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentDetailParentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_Students_ParentDetails_ParentDetailParentID",
                        column: x => x.ParentDetailParentID,
                        principalTable: "ParentDetails",
                        principalColumn: "ParentID");
                });

            migrationBuilder.CreateTable(
                name: "Steps",
                columns: table => new
                {
                    StepId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoadmapId = table.Column<int>(type: "int", nullable: false),
                    StepNo = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceWeekday = table.Column<int>(type: "int", nullable: true),
                    PriceWeekend = table.Column<int>(type: "int", nullable: true),
                    CombinedPricing = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steps", x => x.StepId);
                    table.ForeignKey(
                        name: "FK_Steps_Roadmap_RoadmapId",
                        column: x => x.RoadmapId,
                        principalTable: "Roadmap",
                        principalColumn: "RoadmapId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    TherapistID = table.Column<int>(type: "int", nullable: true),
                    CSID = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_CustomerSupports_CSID",
                        column: x => x.CSID,
                        principalTable: "CustomerSupports",
                        principalColumn: "CSID");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Therapists_TherapistID",
                        column: x => x.TherapistID,
                        principalTable: "Therapists",
                        principalColumn: "TherapistID");
                });

            migrationBuilder.CreateTable(
                name: "Billings",
                columns: table => new
                {
                    BillingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billings", x => x.BillingID);
                    table.ForeignKey(
                        name: "FK_Billings_Invoices_InvoiceID",
                        column: x => x.InvoiceID,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    TherapistID = table.Column<int>(type: "int", nullable: false),
                    CSID = table.Column<int>(type: "int", nullable: false),
                    SessionID = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportID);
                    table.ForeignKey(
                        name: "FK_Reports_CustomerSupports_CSID",
                        column: x => x.CSID,
                        principalTable: "CustomerSupports",
                        principalColumn: "CSID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_Therapists_TherapistID",
                        column: x => x.TherapistID,
                        principalTable: "Therapists",
                        principalColumn: "TherapistID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    SessionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumOfSession = table.Column<int>(type: "int", nullable: false),
                    PriceWeekday = table.Column<int>(type: "int", nullable: true),
                    PriceWeekend = table.Column<int>(type: "int", nullable: true),
                    StepId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.SessionID);
                    table.ForeignKey(
                        name: "FK_Programs_Steps_StepId",
                        column: x => x.StepId,
                        principalTable: "Steps",
                        principalColumn: "StepId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Sessions",
                columns: table => new
                {
                    SessionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    TherapistID = table.Column<int>(type: "int", nullable: false),
                    CSID = table.Column<int>(type: "int", nullable: false),
                    ProgramID = table.Column<int>(type: "int", nullable: false),
                    ScheduleID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionID);
                    table.ForeignKey(
                        name: "FK_Sessions_CustomerSupports_CSID",
                        column: x => x.CSID,
                        principalTable: "CustomerSupports",
                        principalColumn: "CSID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sessions_Programs_ProgramID",
                        column: x => x.ProgramID,
                        principalTable: "Programs",
                        principalColumn: "SessionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sessions_Schedules_ScheduleID",
                        column: x => x.ScheduleID,
                        principalTable: "Schedules",
                        principalColumn: "ScheduleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sessions_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sessions_Therapists_TherapistID",
                        column: x => x.TherapistID,
                        principalTable: "Therapists",
                        principalColumn: "TherapistID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionID = table.Column<int>(type: "int", nullable: false),
                    ParentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_ParentDetails_ParentID",
                        column: x => x.ParentID,
                        principalTable: "ParentDetails",
                        principalColumn: "ParentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Programs_SessionID",
                        column: x => x.SessionID,
                        principalTable: "Programs",
                        principalColumn: "SessionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ParentDetails",
                columns: new[] { "ParentID", "FatherAddress", "FatherCity", "FatherCountry", "FatherEmail", "FatherIC", "FatherName", "FatherOccupation", "FatherPhoneNum", "FatherPostcode", "FatherRace", "FatherWorkAddress", "HouseholdIncome", "MotherAddress", "MotherCity", "MotherCountry", "MotherEmail", "MotherIC", "MotherName", "MotherOccupation", "MotherPhoneNum", "MotherPostcode", "MotherRace", "MotherWorkAddress", "UserId" },
                values: new object[] { 1, "123 Main St", "City", "Country", "john.doe@example.com", "A1234567", "John Doe", "Occupation", "123456789", "12345", "Race", null, "50000", "123 Main St", "City", "Country", "jane.doe@example.com", "B7654321", "Jane Doe", "Occupation", "987654321", "12345", "Race", null, null });

            migrationBuilder.InsertData(
                table: "Roadmap",
                columns: new[] { "RoadmapId", "Name" },
                values: new object[,]
                {
                    { 1, "Idzmir Hub Roadmap" },
                    { 2, "Test 2" }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceID", "DateIssued", "DueDate", "ParentID", "TotalAmount" },
                values: new object[] { 1, new DateTime(2024, 6, 21, 1, 11, 59, 337, DateTimeKind.Local).AddTicks(2256), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1000m });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "StepId", "CombinedPricing", "PriceWeekday", "PriceWeekend", "RoadmapId", "StepNo", "Title" },
                values: new object[,]
                {
                    { 1, true, 150, 200, 1, 1, "Screening and Consultation" },
                    { 2, true, 700, 800, 1, 2, "Full Assessment" },
                    { 3, false, null, null, 1, 3, "Intervention with consistency" },
                    { 4, true, 700, 800, 1, 4, "Re-assessment" },
                    { 5, false, null, null, 1, 5, "Road to School" },
                    { 6, false, null, null, 2, 1, "Test1" },
                    { 7, false, null, null, 2, 2, "Test2" }
                });

            migrationBuilder.InsertData(
                table: "Programs",
                columns: new[] { "SessionID", "NumOfSession", "PriceWeekday", "PriceWeekend", "ProgramName", "StepId" },
                values: new object[,]
                {
                    { 1, 1, null, null, "Consultation", 1 },
                    { 2, 3, null, null, "Assessment", 2 },
                    { 3, 1, null, null, "Full Development Report", 2 },
                    { 4, 4, 500, 600, "Program A", 3 },
                    { 5, 8, 800, 900, "Program B", 3 },
                    { 6, 12, 1100, 1200, "Program C", 3 },
                    { 7, 20, 2000, 2100, "Program D", 3 },
                    { 8, 3, null, null, "Assessment", 4 },
                    { 9, 1, null, null, "Full Development Report", 4 },
                    { 10, 4, 1000, 1200, "Ready to School A", 5 },
                    { 11, 8, 2000, 2200, "Ready to School B", 5 }
                });

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
                name: "IX_AspNetUsers_CSID",
                table: "AspNetUsers",
                column: "CSID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TherapistID",
                table: "AspNetUsers",
                column: "TherapistID");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Billings_InvoiceID",
                table: "Billings",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ParentID",
                table: "Invoices",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_StepId",
                table: "Programs",
                column: "StepId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_CSID",
                table: "Reports",
                column: "CSID");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_StudentID",
                table: "Reports",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_TherapistID",
                table: "Reports",
                column: "TherapistID");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CSID",
                table: "Sessions",
                column: "CSID");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ProgramID",
                table: "Sessions",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ScheduleID",
                table: "Sessions",
                column: "ScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_StudentID",
                table: "Sessions",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_TherapistID",
                table: "Sessions",
                column: "TherapistID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_ParentID",
                table: "ShoppingCarts",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_SessionID",
                table: "ShoppingCarts",
                column: "SessionID");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_RoadmapId",
                table: "Steps",
                column: "RoadmapId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ParentDetailParentID",
                table: "Students",
                column: "ParentDetailParentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "Billings");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "CustomerSupports");

            migrationBuilder.DropTable(
                name: "Therapists");

            migrationBuilder.DropTable(
                name: "ParentDetails");

            migrationBuilder.DropTable(
                name: "Steps");

            migrationBuilder.DropTable(
                name: "Roadmap");
        }
    }
}
