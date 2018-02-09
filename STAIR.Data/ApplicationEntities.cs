using Microsoft.AspNet.Identity.EntityFramework;
using STAIR.Model.Models;
using STAIR.Model.Models.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAIR.Data.Models
{
    public class ApplicationEntities : DbContext
    {
        public ApplicationEntities()
            : base("DBConnection")
        {
            /**
             * It's not necessary to remove the virtual keyword from the navigation properties (which would make lazy loading completely 
             * impossible for the model). It's enough to disable proxy creation (which disables lazy loading as well) for the specific circumstances 
             * where proxies are disturbing, like serialization
             */
            //this.Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<ActionLog> ActionLogs { get; set; }
        public DbSet<sys_menu> sys_menu { get; set; }
        public DbSet<sys_user> sys_user { get; set; }
        public DbSet<sys_user_menu_access> sys_user_menu_access { get; set; }
        public DbSet<sys_user_type> sys_user_type { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<GroupGoal>()
            //            .HasOptional(e => e.Focus)
            //            .WithMany()
            //            .HasForeignKey(e => e.FocusId)
            //            .WillCascadeOnDelete();

            //modelBuilder.IncludeMetadataInDatabase = false;
            Database.SetInitializer<ApplicationEntities>(null);
            modelBuilder.Configurations.Add(new ActionLogMap());
            modelBuilder.Configurations.Add(new sys_menuMap());
            modelBuilder.Configurations.Add(new sys_userMap());
            modelBuilder.Configurations.Add(new sys_user_menu_accessMap());
            modelBuilder.Configurations.Add(new sys_user_typeMap());
            // Remove metadata convention
#pragma warning disable CS0618 // Type or member is obsolete
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
#pragma warning restore CS0618 // Type or member is obsolete

            // Remove the pluralization
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        //public DbSet<ActionLog> ActionLogs { get; set; }
        //public DbSet<AircraftComponent> AircraftComponents { get; set; }
        //public DbSet<AircraftComponentDetail> AircraftComponentDetails { get; set; }
        //public DbSet<AircraftMajorComponent> AircraftMajorComponents { get; set; }
        //public DbSet<AircraftModelFamily> AircraftModelFamilies { get; set; }
        //public DbSet<AircraftOverview> AircraftOverviews { get; set; }
        //public DbSet<City> Cities { get; set; }
        //public DbSet<Company> Companies { get; set; }
        //public DbSet<ComparativeStatement> ComparativeStatements { get; set; }
        //public DbSet<ComparativeStatementDetail> ComparativeStatementDetails { get; set; }
        //public DbSet<ComparativeStatementSupplier> ComparativeStatementSuppliers { get; set; }
        //public DbSet<ComponentType> ComponentTypes { get; set; }
        //public DbSet<Country> Countries { get; set; }
        //public DbSet<Currency> Currencies { get; set; }
        //public DbSet<Department> Departments { get; set; }
        //public DbSet<Designation> Designations { get; set; }
        //public DbSet<Employee> Employees { get; set; }
        //public DbSet<EmploymentHistory> EmploymentHistories { get; set; }
        //public DbSet<ExternalDepartment> ExternalDepartments { get; set; }
        //public DbSet<GoodsReceive> GoodsReceives { get; set; }
        //public DbSet<GoodsReceiveDetail> GoodsReceiveDetails { get; set; }
        //public DbSet<InventoryHistory> InventoryHistories { get; set; }
        //public DbSet<InventoryOpening> InventoryOpenings { get; set; }
        //public DbSet<ItemBalance> ItemBalances { get; set; }
        //public DbSet<ItemDemand> ItemDemands { get; set; }
        //public DbSet<ItemDemandDetail> ItemDemandDetails { get; set; }
        //public DbSet<ItemIssue> ItemIssues { get; set; }
        //public DbSet<ItemIssueDetail> ItemIssueDetails { get; set; }
        //public DbSet<ItemReceiptVoucher> ItemReceiptVouchers { get; set; }
        //public DbSet<ItemReceiptVoucherDetail> ItemReceiptVoucherDetails { get; set; }
        //public DbSet<ItemScrapExpireOut> ItemScrapExpireOuts { get; set; }
        //public DbSet<ItemScrapExpireOutDetail> ItemScrapExpireOutDetails { get; set; }
        //public DbSet<LCEntry> LCEntries { get; set; }
        //public DbSet<LCShipment> LCShipments { get; set; }
        //public DbSet<LoanItem> LoanItems { get; set; }
        //public DbSet<Location> Locations { get; set; }
        //public DbSet<Manufacturer> Manufacturers { get; set; }
        //public DbSet<Module> Modules { get; set; }
        //public DbSet<NotificationSetting> NotificationSettings { get; set; }
        //public DbSet<PartCondition> PartConditions { get; set; }
        //public DbSet<ProcurementQuoteFile> ProcurementQuoteFiles { get; set; }
        //public DbSet<ProcurementQuoteRequest> ProcurementQuoteRequests { get; set; }
        //public DbSet<ProcurementQuoteRequestItem> ProcurementQuoteRequestItems { get; set; }
        //public DbSet<ProcurementQuoteRequestItemSupplier> ProcurementQuoteRequestItemSuppliers { get; set; }
        //public DbSet<ProcurementRequisition> ProcurementRequisitions { get; set; }
        //public DbSet<ProcurementRequisitionDetail> ProcurementRequisitionDetails { get; set; }
        //public DbSet<ProcurementRequisitionMaxNumber> ProcurementRequisitionMaxNumbers { get; set; }
        //public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        //public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        //public DbSet<PurchaseOrderInvoiceRelatedDoc> PurchaseOrderInvoiceRelatedDocs { get; set; }
        //public DbSet<PurchaseOrderLCRelatedDoc> PurchaseOrderLCRelatedDocs { get; set; }
        //public DbSet<PurchaseOrderMaxNumber> PurchaseOrderMaxNumbers { get; set; }
        //public DbSet<PurchaseOrderSupplierQuoteRequest> PurchaseOrderSupplierQuoteRequests { get; set; }
        //public DbSet<QuoteCollection> QuoteCollections { get; set; }
        //public DbSet<QuoteCollectionDetail> QuoteCollectionDetails { get; set; }
        //public DbSet<QuoteDetailCondition> QuoteDetailConditions { get; set; }
        //public DbSet<Rack> Racks { get; set; }
        //public DbSet<RackRow> RackRows { get; set; }
        //public DbSet<RackRowBin> RackRowBins { get; set; }
        //public DbSet<RecieptVoucher> RecieptVouchers { get; set; }
        //public DbSet<ReportConfiguration> ReportConfigurations { get; set; }
        //public DbSet<ReportConfigurationDetail> ReportConfigurationDetails { get; set; }
        //public DbSet<ReportTemplate> ReportTemplates { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<RoleSubModuleItem> RoleSubModuleItems { get; set; }
        //public DbSet<Room> Rooms { get; set; }
        //public DbSet<SerialInitializer> SerialInitializers { get; set; }
        //public DbSet<StockInward> StockInwards { get; set; }
        //public DbSet<Store> Stores { get; set; }
        //public DbSet<SubModule> SubModules { get; set; }
        //public DbSet<SubModuleItem> SubModuleItems { get; set; }
        //public DbSet<Supplier> Suppliers { get; set; }
        //public DbSet<SupplierAircraftComponent> SupplierAircraftComponents { get; set; }
        //public DbSet<UnitOfMeasurement> UnitOfMeasurements { get; set; }
        //public DbSet<UnserviceableItem> UnserviceableItems { get; set; }
        //public DbSet<UnUsableItemReturn> UnUsableItemReturns { get; set; }
        //public DbSet<UnUsableItemReturnDetail> UnUsableItemReturnDetails { get; set; }
        //public DbSet<User> Users { get; set; }
        //public DbSet<Workflowaction> Workflowactions { get; set; }
        //public DbSet<WorkflowactionSetting> WorkflowactionSettings { get; set; }
        //public DbSet<Workshop> Workshops { get; set; }
        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}
