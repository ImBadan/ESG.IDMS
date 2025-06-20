using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESG.IDMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApproverSetup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ApprovalSetupType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkflowName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkflowDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailSubject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApproverSetup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 36, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffectedColumns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryKey = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    TraceId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireArmsBrandModel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireArmsBrandModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FurnitureAndFixtureBrandModel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurnitureAndFixtureBrandModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OtherEquipmentBrandModel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherEquipmentBrandModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RadioEquipmentBrandModel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RadioEquipmentBrandModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Remarks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remarks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ReportName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QueryType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportOrChartType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDistinct = table.Column<bool>(type: "bit", nullable: false),
                    QueryString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOnDashboard = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOnReportModule = table.Column<bool>(type: "bit", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadProcessor",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Module = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UploadType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExceptionFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadProcessor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleBrandModel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleBrandModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalRecord",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ApproverSetupId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    DataId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalRecord_ApproverSetup_ApproverSetupId",
                        column: x => x.ApproverSetupId,
                        principalTable: "ApproverSetup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApproverAssignment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ApproverType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApproverSetupId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    ApproverUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApproverRoleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApproverAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApproverAssignment_ApproverSetup_ApproverSetupId",
                        column: x => x.ApproverSetupId,
                        principalTable: "ApproverSetup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportAIIntegration",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Input = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Output = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportAIIntegration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportAIIntegration_Report_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Report",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportQueryFilter",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomDropdownValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DropdownTableKeyAndValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DropdownFilter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportQueryFilter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportQueryFilter_Report_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Report",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportRoleAssignment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportRoleAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportRoleAssignment_Report_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Report",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FireArms",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ItemQty = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FireArmsBrandModelId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    LocationId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    StatusId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    RemarksId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    FreeTextRemarks = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IssuedTo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireArms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FireArms_FireArmsBrandModel_FireArmsBrandModelId",
                        column: x => x.FireArmsBrandModelId,
                        principalTable: "FireArmsBrandModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FireArms_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FireArms_Remarks_RemarksId",
                        column: x => x.RemarksId,
                        principalTable: "Remarks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FireArms_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FurnitureAndFixture",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ItemQty = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FurnitureAndFixtureBrandModelId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    LocationId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    StatusId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    RemarksId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    FreeTextRemarks = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IssuedTo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurnitureAndFixture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FurnitureAndFixture_FurnitureAndFixtureBrandModel_FurnitureAndFixtureBrandModelId",
                        column: x => x.FurnitureAndFixtureBrandModelId,
                        principalTable: "FurnitureAndFixtureBrandModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FurnitureAndFixture_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FurnitureAndFixture_Remarks_RemarksId",
                        column: x => x.RemarksId,
                        principalTable: "Remarks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FurnitureAndFixture_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OtherEquipment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ItemQty = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OtherEquipmentBrandModelId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    LocationId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    StatusId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    RemarksId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    FreeTextRemarks = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IssuedTo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherEquipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtherEquipment_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OtherEquipment_OtherEquipmentBrandModel_OtherEquipmentBrandModelId",
                        column: x => x.OtherEquipmentBrandModelId,
                        principalTable: "OtherEquipmentBrandModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OtherEquipment_Remarks_RemarksId",
                        column: x => x.RemarksId,
                        principalTable: "Remarks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OtherEquipment_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RadioEquipment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ItemQty = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RadioEquipmentBrandModelId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    LocationId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    StatusId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    RemarksId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    FreeTextRemarks = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IssuedTo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RadioEquipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RadioEquipment_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RadioEquipment_RadioEquipmentBrandModel_RadioEquipmentBrandModelId",
                        column: x => x.RadioEquipmentBrandModelId,
                        principalTable: "RadioEquipmentBrandModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RadioEquipment_Remarks_RemarksId",
                        column: x => x.RemarksId,
                        principalTable: "Remarks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RadioEquipment_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ItemQty = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    VehicleBrandModelId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    LocationId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    StatusId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    RemarksId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    FreeTextRemarks = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IssuedTo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicle_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicle_Remarks_RemarksId",
                        column: x => x.RemarksId,
                        principalTable: "Remarks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicle_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicle_VehicleBrandModel_VehicleBrandModelId",
                        column: x => x.VehicleBrandModelId,
                        principalTable: "VehicleBrandModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Approval",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ApproverUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovalRecordId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusUpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmailSendingStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailSendingRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailSendingDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovalRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approval", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Approval_ApprovalRecord_ApprovalRecordId",
                        column: x => x.ApprovalRecordId,
                        principalTable: "ApprovalRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Approval_ApprovalRecordId",
                table: "Approval",
                column: "ApprovalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_CreatedBy",
                table: "Approval",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_Entity",
                table: "Approval",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_Id",
                table: "Approval",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_LastModifiedBy",
                table: "Approval",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_LastModifiedDate",
                table: "Approval",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRecord_ApproverSetupId",
                table: "ApprovalRecord",
                column: "ApproverSetupId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRecord_CreatedBy",
                table: "ApprovalRecord",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRecord_Entity",
                table: "ApprovalRecord",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRecord_Id",
                table: "ApprovalRecord",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRecord_LastModifiedBy",
                table: "ApprovalRecord",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRecord_LastModifiedDate",
                table: "ApprovalRecord",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverAssignment_ApproverSetupId",
                table: "ApproverAssignment",
                column: "ApproverSetupId");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverAssignment_CreatedBy",
                table: "ApproverAssignment",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverAssignment_Entity",
                table: "ApproverAssignment",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverAssignment_Id",
                table: "ApproverAssignment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverAssignment_LastModifiedBy",
                table: "ApproverAssignment",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverAssignment_LastModifiedDate",
                table: "ApproverAssignment",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverSetup_CreatedBy",
                table: "ApproverSetup",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverSetup_Entity",
                table: "ApproverSetup",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverSetup_Id",
                table: "ApproverSetup",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverSetup_LastModifiedBy",
                table: "ApproverSetup",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverSetup_LastModifiedDate",
                table: "ApproverSetup",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_DateTime",
                table: "AuditLogs",
                column: "DateTime");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_Id",
                table: "AuditLogs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_PrimaryKey",
                table: "AuditLogs",
                column: "PrimaryKey");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_TraceId",
                table: "AuditLogs",
                column: "TraceId");

            migrationBuilder.CreateIndex(
                name: "IX_FireArms_CreatedBy",
                table: "FireArms",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FireArms_Entity",
                table: "FireArms",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_FireArms_FireArmsBrandModelId",
                table: "FireArms",
                column: "FireArmsBrandModelId");

            migrationBuilder.CreateIndex(
                name: "IX_FireArms_Id",
                table: "FireArms",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FireArms_LastModifiedBy",
                table: "FireArms",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FireArms_LastModifiedDate",
                table: "FireArms",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_FireArms_LocationId",
                table: "FireArms",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_FireArms_RemarksId",
                table: "FireArms",
                column: "RemarksId");

            migrationBuilder.CreateIndex(
                name: "IX_FireArms_StatusId",
                table: "FireArms",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_FireArmsBrandModel_CreatedBy",
                table: "FireArmsBrandModel",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FireArmsBrandModel_Entity",
                table: "FireArmsBrandModel",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_FireArmsBrandModel_Id",
                table: "FireArmsBrandModel",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FireArmsBrandModel_LastModifiedBy",
                table: "FireArmsBrandModel",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FireArmsBrandModel_LastModifiedDate",
                table: "FireArmsBrandModel",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureAndFixture_CreatedBy",
                table: "FurnitureAndFixture",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureAndFixture_Entity",
                table: "FurnitureAndFixture",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureAndFixture_FurnitureAndFixtureBrandModelId",
                table: "FurnitureAndFixture",
                column: "FurnitureAndFixtureBrandModelId");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureAndFixture_Id",
                table: "FurnitureAndFixture",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureAndFixture_LastModifiedBy",
                table: "FurnitureAndFixture",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureAndFixture_LastModifiedDate",
                table: "FurnitureAndFixture",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureAndFixture_LocationId",
                table: "FurnitureAndFixture",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureAndFixture_RemarksId",
                table: "FurnitureAndFixture",
                column: "RemarksId");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureAndFixture_StatusId",
                table: "FurnitureAndFixture",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureAndFixtureBrandModel_CreatedBy",
                table: "FurnitureAndFixtureBrandModel",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureAndFixtureBrandModel_Entity",
                table: "FurnitureAndFixtureBrandModel",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureAndFixtureBrandModel_Id",
                table: "FurnitureAndFixtureBrandModel",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureAndFixtureBrandModel_LastModifiedBy",
                table: "FurnitureAndFixtureBrandModel",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureAndFixtureBrandModel_LastModifiedDate",
                table: "FurnitureAndFixtureBrandModel",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Location_CreatedBy",
                table: "Location",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Location_Entity",
                table: "Location",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Location_Id",
                table: "Location",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Location_LastModifiedBy",
                table: "Location",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Location_LastModifiedDate",
                table: "Location",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_OtherEquipment_CreatedBy",
                table: "OtherEquipment",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_OtherEquipment_Entity",
                table: "OtherEquipment",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_OtherEquipment_Id",
                table: "OtherEquipment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OtherEquipment_LastModifiedBy",
                table: "OtherEquipment",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_OtherEquipment_LastModifiedDate",
                table: "OtherEquipment",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_OtherEquipment_LocationId",
                table: "OtherEquipment",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherEquipment_OtherEquipmentBrandModelId",
                table: "OtherEquipment",
                column: "OtherEquipmentBrandModelId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherEquipment_RemarksId",
                table: "OtherEquipment",
                column: "RemarksId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherEquipment_StatusId",
                table: "OtherEquipment",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherEquipmentBrandModel_CreatedBy",
                table: "OtherEquipmentBrandModel",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_OtherEquipmentBrandModel_Entity",
                table: "OtherEquipmentBrandModel",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_OtherEquipmentBrandModel_Id",
                table: "OtherEquipmentBrandModel",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OtherEquipmentBrandModel_LastModifiedBy",
                table: "OtherEquipmentBrandModel",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_OtherEquipmentBrandModel_LastModifiedDate",
                table: "OtherEquipmentBrandModel",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_RadioEquipment_CreatedBy",
                table: "RadioEquipment",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RadioEquipment_Entity",
                table: "RadioEquipment",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_RadioEquipment_Id",
                table: "RadioEquipment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RadioEquipment_LastModifiedBy",
                table: "RadioEquipment",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RadioEquipment_LastModifiedDate",
                table: "RadioEquipment",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_RadioEquipment_LocationId",
                table: "RadioEquipment",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_RadioEquipment_RadioEquipmentBrandModelId",
                table: "RadioEquipment",
                column: "RadioEquipmentBrandModelId");

            migrationBuilder.CreateIndex(
                name: "IX_RadioEquipment_RemarksId",
                table: "RadioEquipment",
                column: "RemarksId");

            migrationBuilder.CreateIndex(
                name: "IX_RadioEquipment_StatusId",
                table: "RadioEquipment",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RadioEquipmentBrandModel_CreatedBy",
                table: "RadioEquipmentBrandModel",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RadioEquipmentBrandModel_Entity",
                table: "RadioEquipmentBrandModel",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_RadioEquipmentBrandModel_Id",
                table: "RadioEquipmentBrandModel",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RadioEquipmentBrandModel_LastModifiedBy",
                table: "RadioEquipmentBrandModel",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RadioEquipmentBrandModel_LastModifiedDate",
                table: "RadioEquipmentBrandModel",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Remarks_CreatedBy",
                table: "Remarks",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Remarks_Entity",
                table: "Remarks",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Remarks_Id",
                table: "Remarks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Remarks_LastModifiedBy",
                table: "Remarks",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Remarks_LastModifiedDate",
                table: "Remarks",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Report_CreatedBy",
                table: "Report",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Report_Entity",
                table: "Report",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Report_Id",
                table: "Report",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Report_LastModifiedBy",
                table: "Report",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Report_LastModifiedDate",
                table: "Report",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportAIIntegration_CreatedBy",
                table: "ReportAIIntegration",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportAIIntegration_Entity",
                table: "ReportAIIntegration",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportAIIntegration_Id",
                table: "ReportAIIntegration",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReportAIIntegration_LastModifiedBy",
                table: "ReportAIIntegration",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportAIIntegration_LastModifiedDate",
                table: "ReportAIIntegration",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportAIIntegration_ReportId",
                table: "ReportAIIntegration",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportQueryFilter_CreatedBy",
                table: "ReportQueryFilter",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportQueryFilter_Entity",
                table: "ReportQueryFilter",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportQueryFilter_Id",
                table: "ReportQueryFilter",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReportQueryFilter_LastModifiedBy",
                table: "ReportQueryFilter",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportQueryFilter_LastModifiedDate",
                table: "ReportQueryFilter",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportQueryFilter_ReportId",
                table: "ReportQueryFilter",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRoleAssignment_CreatedBy",
                table: "ReportRoleAssignment",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRoleAssignment_Entity",
                table: "ReportRoleAssignment",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRoleAssignment_Id",
                table: "ReportRoleAssignment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRoleAssignment_LastModifiedBy",
                table: "ReportRoleAssignment",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRoleAssignment_LastModifiedDate",
                table: "ReportRoleAssignment",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRoleAssignment_ReportId",
                table: "ReportRoleAssignment",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Status_CreatedBy",
                table: "Status",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Status_Entity",
                table: "Status",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Status_Id",
                table: "Status",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Status_LastModifiedBy",
                table: "Status",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Status_LastModifiedDate",
                table: "Status",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_UploadProcessor_CreatedBy",
                table: "UploadProcessor",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UploadProcessor_Entity",
                table: "UploadProcessor",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_UploadProcessor_Id",
                table: "UploadProcessor",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UploadProcessor_LastModifiedBy",
                table: "UploadProcessor",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UploadProcessor_LastModifiedDate",
                table: "UploadProcessor",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_CreatedBy",
                table: "Vehicle",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_Entity",
                table: "Vehicle",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_Id",
                table: "Vehicle",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_LastModifiedBy",
                table: "Vehicle",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_LastModifiedDate",
                table: "Vehicle",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_LocationId",
                table: "Vehicle",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_RemarksId",
                table: "Vehicle",
                column: "RemarksId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_StatusId",
                table: "Vehicle",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_VehicleBrandModelId",
                table: "Vehicle",
                column: "VehicleBrandModelId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleBrandModel_CreatedBy",
                table: "VehicleBrandModel",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleBrandModel_Entity",
                table: "VehicleBrandModel",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleBrandModel_Id",
                table: "VehicleBrandModel",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleBrandModel_LastModifiedBy",
                table: "VehicleBrandModel",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleBrandModel_LastModifiedDate",
                table: "VehicleBrandModel",
                column: "LastModifiedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Approval");

            migrationBuilder.DropTable(
                name: "ApproverAssignment");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "FireArms");

            migrationBuilder.DropTable(
                name: "FurnitureAndFixture");

            migrationBuilder.DropTable(
                name: "OtherEquipment");

            migrationBuilder.DropTable(
                name: "RadioEquipment");

            migrationBuilder.DropTable(
                name: "ReportAIIntegration");

            migrationBuilder.DropTable(
                name: "ReportQueryFilter");

            migrationBuilder.DropTable(
                name: "ReportRoleAssignment");

            migrationBuilder.DropTable(
                name: "UploadProcessor");

            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropTable(
                name: "ApprovalRecord");

            migrationBuilder.DropTable(
                name: "FireArmsBrandModel");

            migrationBuilder.DropTable(
                name: "FurnitureAndFixtureBrandModel");

            migrationBuilder.DropTable(
                name: "OtherEquipmentBrandModel");

            migrationBuilder.DropTable(
                name: "RadioEquipmentBrandModel");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Remarks");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "VehicleBrandModel");

            migrationBuilder.DropTable(
                name: "ApproverSetup");
        }
    }
}
