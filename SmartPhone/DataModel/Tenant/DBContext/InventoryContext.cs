using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataModel
{
    public partial class InventoryContext : DbContext
    {
        public InventoryContext()
        {

        }
        public InventoryContext(DbContextOptions<InventoryContext> options)
        : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=ARTHUR;Database=Arthur_inventory;Trusted_Connection=True;");
            }
        }

        public virtual DbSet<Audit> Audit { get; set; }
        public virtual DbSet<AuditField> AuditField { get; set; }
        public virtual DbSet<CbankDetail> CbankDetail { get; set; }
        public virtual DbSet<Channel> Channel { get; set; }
        public virtual DbSet<Cidentification> Cidentification { get; set; }
        public virtual DbSet<Cincome> Cincome { get; set; }
        public virtual DbSet<CommonIndustry> CommonIndustry { get; set; }
        public virtual DbSet<CommonOrganisationType> CommonOrganisationType { get; set; }
        public virtual DbSet<CommonTimezone> CommonTimezone { get; set; }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<ContractOrigin> ContractOrigin { get; set; }
        public virtual DbSet<ContractPaymentPlan> ContractPaymentPlan { get; set; }
        public virtual DbSet<ContractProduct> ContractProduct { get; set; }
        public virtual DbSet<Crelation> Crelation { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Delivery> Delivery { get; set; }
        public virtual DbSet<EmailSetting> EmailSetting { get; set; }
        public virtual DbSet<ErrorLog> ErrorLog { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductDescription> ProductDescription { get; set; }
        public virtual DbSet<ProductImage> ProductImage { get; set; }
        public virtual DbSet<ProductPayPlan> ProductPayPlan { get; set; }
        public virtual DbSet<ProductProperty> ProductProperty { get; set; }
        public virtual DbSet<Purchase> Purchase { get; set; }
        public virtual DbSet<PurchaseProduct> PurchaseProduct { get; set; }
        public virtual DbSet<Recall> Recall { get; set; }
        public virtual DbSet<Setting> Setting { get; set; }
        public virtual DbSet<SLink> SLink { get; set; }
        public virtual DbSet<Statement> Statement { get; set; }
        public virtual DbSet<StatementDetail> StatementDetail { get; set; }
        public virtual DbSet<Subscriber> Subscriber { get; set; }
        public virtual DbSet<SubscriberEmail> SubscriberEmail { get; set; }
        public virtual DbSet<SubscriberNotification> SubscriberNotification { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<SUser> SUser { get; set; }
        public virtual DbSet<SUserLink> SUserLink { get; set; }
        public virtual DbSet<SUserLog> SUserLog { get; set; }
        public virtual DbSet<SystemEmailSetting> SystemEmailSetting { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }
        public virtual DbSet<WarehouseAdjustment> WarehouseAdjustment { get; set; }
        public virtual DbSet<WarehouseInitialization> WarehouseInitialization { get; set; }
        public virtual DbSet<WarehousePrductRecord> WarehousePrductRecord { get; set; }
        public virtual DbSet<WarehouseProduct> WarehouseProduct { get; set; }
        public virtual DbSet<WarehouseTransfer> WarehouseTransfer { get; set; }
        public virtual DbSet<WarehouseTransferDetail> WarehouseTransferDetail { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audit>(entity =>
            {
                entity.Property(e => e.Entity)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EntityId).HasDefaultValueSql("((0))");

                entity.Property(e => e.EventDtUtc).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AuditField>(entity =>
            {
                entity.Property(e => e.Field)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NewValue).IsUnicode(false);

                entity.Property(e => e.OldValue).IsUnicode(false);

                entity.HasOne(d => d.Audit)
                    .WithMany(p => p.AuditField)
                    .HasForeignKey(d => d.AuditId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditField_Audit");
            });

            modelBuilder.Entity<CbankDetail>(entity =>
            {
                entity.ToTable("CBankDetail");

                entity.Property(e => e.AccountNumber).HasColumnType("char(7)");

                entity.Property(e => e.Bank).HasColumnType("char(2)");

                entity.Property(e => e.BankBranch)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Branch).HasColumnType("char(4)");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");

                entity.Property(e => e.NameOnAcc)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Note)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Suffix).HasColumnType("char(3)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CbankDetail)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_CBankDetail_Customer");
            });

            modelBuilder.Entity<Channel>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Cidentification>(entity =>
            {
                entity.ToTable("CIdentification");

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");

                entity.Property(e => e.ExpiryDate).HasMaxLength(30);

                entity.Property(e => e.Idnum)
                    .HasColumnName("IDNum")
                    .HasMaxLength(20);

                entity.Property(e => e.Idtype)
                    .HasColumnName("IDType")
                    .HasMaxLength(50);

                entity.Property(e => e.Version).HasMaxLength(50);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Cidentification)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_CIdentification_Customer");
            });

            modelBuilder.Entity<Cincome>(entity =>
            {
                entity.ToTable("CIncome");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");

                entity.Property(e => e.EmployeeType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LengthOfService)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Occupation)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PayAfterTax).HasColumnType("money");

                entity.Property(e => e.PayPeriod)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Wages).HasColumnType("money");

                entity.Property(e => e.WithdrawDay)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Cincome)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_CIncome_Customer");
            });

            modelBuilder.Entity<CommonIndustry>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CommonOrganisationType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CommonTimezone>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.Property(e => e.AccountFee)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.BankInfo).HasMaxLength(100);

                entity.Property(e => e.BookingFee)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CompleteStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ContractSignDate).HasColumnType("date");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerAddress).HasMaxLength(200);

                entity.Property(e => e.CustomerEmail).HasMaxLength(100);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CustomerPhone).HasMaxLength(200);

                entity.Property(e => e.DebitFrequency).HasMaxLength(20);

                entity.Property(e => e.DebitPerAmount).HasColumnType("money");

                entity.Property(e => e.DebitStartDate).HasColumnType("date");

                entity.Property(e => e.DeliverComment).HasColumnType("ntext");

                entity.Property(e => e.DeliverDate).HasColumnType("datetime");

                entity.Property(e => e.DeliveryAfterPayment).HasDefaultValueSql("((0))");

                entity.Property(e => e.DeliveryFee)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Note).HasColumnType("ntext");

                entity.Property(e => e.OthersFee)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RefType).HasMaxLength(50);

                entity.Property(e => e.Sid)
                    .HasColumnName("SId")
                    .HasMaxLength(30);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Agree)
                    .WithMany(p => p.ContractAgree)
                    .HasForeignKey(d => d.AgreeId)
                    .HasConstraintName("FK_Contract_S_UserAgree");

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.ChannelId)
                    .HasConstraintName("FK_Contract_Channel");

                entity.HasOne(d => d.Delivery)
                    .WithMany(p => p.ContractDelivery)
                    .HasForeignKey(d => d.DeliveryId)
                    .HasConstraintName("FK_Contract_S_UserDelivery");

                entity.HasOne(d => d.Sales)
                    .WithMany(p => p.ContractSales)
                    .HasForeignKey(d => d.SalesId)
                    .HasConstraintName("FK_Contract_S_UserSales");

                entity.HasOne(d => d.Setting)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.SettingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_Setting");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ContractUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Contract_S_User");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_Warehouse");
            });

            modelBuilder.Entity<ContractOrigin>(entity =>
            {
                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<ContractPaymentPlan>(entity =>
            {
                entity.Property(e => e.ExecuteDate).HasColumnType("date");

                entity.Property(e => e.Money).HasColumnType("money");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.ContractPaymentPlan)
                    .HasForeignKey(d => d.ContractId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractPaymentPlan_Contract");
            });

            modelBuilder.Entity<ContractProduct>(entity =>
            {
                entity.Property(e => e.BasicPrice)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.SeriesNumber).HasMaxLength(100);

                entity.Property(e => e.Type).HasMaxLength(5);

                entity.Property(e => e.Unit).HasMaxLength(50);

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.ContractProduct)
                    .HasForeignKey(d => d.ContractId)
                    .HasConstraintName("FK_ContractProduct_Contract");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ContractProduct)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractProduct_Product");
            });

            modelBuilder.Entity<Crelation>(entity =>
            {
                entity.ToTable("CRelation");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HouseNum)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Relationship)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StreetName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Suburb)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Crelation)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_CRelation_Customer");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.BlacklistId).HasColumnName("BlacklistID");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Comment).HasColumnType("ntext");

                entity.Property(e => e.CompanyReferenceNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Credit).HasColumnType("money");

                entity.Property(e => e.CustomerReference)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HomePhone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.HouseNum)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LivePeriod)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MobilePhone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NumOfDependant)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.OtherName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostCode).HasColumnType("char(5)");

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.ResidentalDetail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SalesReferenceNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StreetName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Suburb)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SurName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.TrustBalanceCl)
                    .HasColumnName("TrustBalanceCL")
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TrustBalanceCp)
                    .HasColumnName("TrustBalanceCP")
                    .HasColumnType("money");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.Property(e => e.WorkPhone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Setting)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.SettingId)
                    .HasConstraintName("FK_Customer_Setting");
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.Property(e => e.ContractId).HasColumnName("Contract_Id");

                entity.Property(e => e.CourierName).IsUnicode(false);

                entity.Property(e => e.CourierTrackingNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryStaffName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiptNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SalesReferenceNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmailSetting>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Body).IsRequired();

                entity.Property(e => e.Sender)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.Property(e => e.EventUtc).HasColumnType("datetime");

                entity.Property(e => e.MessageDump)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.MethodName).HasMaxLength(50);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.BarCode).HasMaxLength(100);

                entity.Property(e => e.BasicPrice)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PayPal).HasColumnType("ntext");

                entity.Property(e => e.PerPay).HasColumnType("money");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ProductCode).HasMaxLength(50);

                entity.Property(e => e.Profile).HasMaxLength(200);

                entity.Property(e => e.Rrp)
                    .HasColumnName("RRP")
                    .HasColumnType("money");

                entity.Property(e => e.SeriesNumber).HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.TotalValue)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Type).HasMaxLength(5);

                entity.Property(e => e.Unit).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Product_ProductCategory");

                entity.HasOne(d => d.Setting)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.SettingId)
                    .HasConstraintName("FK_Product_Setting");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Product_Supplier");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_ProductCategory_ProductCategory");

                entity.HasOne(d => d.Setting)
                    .WithMany(p => p.ProductCategory)
                    .HasForeignKey(d => d.SettingId)
                    .HasConstraintName("FK_ProductCategory_Setting");
            });

            modelBuilder.Entity<ProductDescription>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductDescription)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductDescription_ProductDescription");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.Profile).HasMaxLength(200);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImage)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ProductImage_Product");
            });

            modelBuilder.Entity<ProductPayPlan>(entity =>
            {
                entity.Property(e => e.PayPlan)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Term)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductPayPlan)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductPayPlan_Product");
            });

            modelBuilder.Entity<ProductProperty>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductProperty)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductDetail_Product");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.CompleteStatus).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DeliveryFee)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeliveryName).HasMaxLength(50);

                entity.Property(e => e.OtherFee)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PostCode).HasMaxLength(50);

                entity.Property(e => e.SUserId).HasColumnName("S_UserId");

                entity.Property(e => e.State).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.StreetAddress).HasMaxLength(100);

                entity.Property(e => e.Suburb).HasMaxLength(50);

                entity.Property(e => e.SupplierName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SupplierReference).HasMaxLength(50);

                entity.HasOne(d => d.SUser)
                    .WithMany(p => p.Purchase)
                    .HasForeignKey(d => d.SUserId)
                    .HasConstraintName("FK_Purchase_S_User");

                entity.HasOne(d => d.WareHouse)
                    .WithMany(p => p.Purchase)
                    .HasForeignKey(d => d.WareHouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchase_Warehouse");
            });

            modelBuilder.Entity<PurchaseProduct>(entity =>
            {
                entity.Property(e => e.Comment).HasMaxLength(500);

                entity.Property(e => e.ProductDescription).HasMaxLength(200);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Unit).HasMaxLength(50);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PurchaseProduct)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseProduct_Product");

                entity.HasOne(d => d.Purchase)
                    .WithMany(p => p.PurchaseProduct)
                    .HasForeignKey(d => d.PurchaseId)
                    .HasConstraintName("FK_PurchaseProduct_Purchase");
            });

            modelBuilder.Entity<Recall>(entity =>
            {
                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.AmountPaid).HasColumnType("money");

                entity.Property(e => e.ArrearsAmount).HasColumnType("money");

                entity.Property(e => e.ArrearsFee)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CcreferenceId).HasColumnName("CCReference_Id");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.PpsendDate)
                    .HasColumnName("PPSendDate")
                    .HasColumnType("date");

                entity.Property(e => e.ProductCategory)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Spdate)
                    .HasColumnName("SPDate")
                    .HasColumnType("date");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TotalArrearsAmount).HasColumnType("money");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ContactDdinumber)
                    .HasColumnName("ContactDDINumber")
                    .HasMaxLength(50);

                entity.Property(e => e.ContactEmail).HasMaxLength(50);

                entity.Property(e => e.ContactFaxNumber).HasMaxLength(50);

                entity.Property(e => e.ContactFirstName).HasMaxLength(50);

                entity.Property(e => e.ContactLastName).HasMaxLength(50);

                entity.Property(e => e.ContactMobileNumber).HasMaxLength(50);

                entity.Property(e => e.ContactOfficePhone).HasMaxLength(50);

                entity.Property(e => e.ContactPhoneNumber).HasMaxLength(50);

                entity.Property(e => e.ContactTollFreeNumber).HasMaxLength(50);

                entity.Property(e => e.ContactWebsite).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Gst)
                    .HasColumnName("GST")
                    .HasMaxLength(50);

                entity.Property(e => e.Logo).HasMaxLength(200);

                entity.Property(e => e.PhysicalCity).HasMaxLength(50);

                entity.Property(e => e.PhysicalCountry).HasMaxLength(50);

                entity.Property(e => e.PhysicalName).HasMaxLength(50);

                entity.Property(e => e.PhysicalPostalCode).HasMaxLength(50);

                entity.Property(e => e.PhysicalState).HasMaxLength(50);

                entity.Property(e => e.PhysicalStreetAddress).HasMaxLength(50);

                entity.Property(e => e.PhysicalSuburb).HasMaxLength(50);

                entity.Property(e => e.PostalAddress).HasMaxLength(50);

                entity.Property(e => e.PostalCity).HasMaxLength(50);

                entity.Property(e => e.PostalCode).HasMaxLength(50);

                entity.Property(e => e.PostalCountry).HasMaxLength(50);

                entity.Property(e => e.PostalName).HasMaxLength(50);

                entity.Property(e => e.PostalState).HasMaxLength(50);

                entity.Property(e => e.PostalSuburb).HasMaxLength(50);

                entity.Property(e => e.TradingName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Website).HasMaxLength(50);

                entity.HasOne(d => d.Industry)
                    .WithMany(p => p.Setting)
                    .HasForeignKey(d => d.IndustryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Setting_CommonIndustry");

                entity.HasOne(d => d.OrganisationType)
                    .WithMany(p => p.Setting)
                    .HasForeignKey(d => d.OrganisationTypeId)
                    .HasConstraintName("FK_Setting_CommonOrganisationType");
            });

            modelBuilder.Entity<SLink>(entity =>
            {
                entity.ToTable("S_Link");

                entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Control)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Link)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Statement>(entity =>
            {
                entity.Property(e => e.Money).HasColumnType("money");

                entity.Property(e => e.OperationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<StatementDetail>(entity =>
            {
                entity.Property(e => e.BankDate).HasColumnType("date");

                entity.Property(e => e.CcreferenceId).HasColumnName("CCReference_Id");

                entity.Property(e => e.Money)
                    .HasColumnName("money")
                    .HasColumnType("money");

                entity.Property(e => e.Reference).HasMaxLength(20);

                entity.Property(e => e.StatementId).HasColumnName("Statement_Id");

                entity.HasOne(d => d.Statement)
                    .WithMany(p => p.StatementDetail)
                    .HasForeignKey(d => d.StatementId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_StatementDetail_Statement");
            });

            modelBuilder.Entity<Subscriber>(entity =>
            {
                entity.Property(e => e.CreatedUtc).HasColumnName("CreatedUTC");

                entity.Property(e => e.DeviceId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeviceOs)
                    .IsRequired()
                    .HasColumnName("DeviceOS")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ExpiresUtc).HasColumnName("ExpiresUTC");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VerifiedUtc).HasColumnName("VerifiedUTC");
            });

            modelBuilder.Entity<SubscriberEmail>(entity =>
            {
                entity.Property(e => e.CreatedUtc).HasColumnName("CreatedUTC");

                entity.Property(e => e.EmailPhone)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Token)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.VerifiedUtc).HasColumnName("VerifiedUTC");

                entity.HasOne(d => d.Subscriber)
                    .WithMany(p => p.SubscriberEmail)
                    .HasForeignKey(d => d.SubscriberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubsciberEmail_Subscriber");
            });

            modelBuilder.Entity<SubscriberNotification>(entity =>
            {
                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.SubscriberNotification)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubscriberNotification_Notification");

                entity.HasOne(d => d.Subscriber)
                    .WithMany(p => p.SubscriberNotification)
                    .HasForeignKey(d => d.SubscriberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubscriberNotification_Subscriber");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.BankBranch).HasMaxLength(50);

                entity.Property(e => e.BankName).HasMaxLength(50);

                entity.Property(e => e.ContactDdinumber)
                    .HasColumnName("ContactDDINumber")
                    .HasMaxLength(50);

                entity.Property(e => e.ContactEmail).HasMaxLength(50);

                entity.Property(e => e.ContactFaxNumber).HasMaxLength(50);

                entity.Property(e => e.ContactFirstName).HasMaxLength(50);

                entity.Property(e => e.ContactLastName).HasMaxLength(50);

                entity.Property(e => e.ContactMobileNumber).HasMaxLength(50);

                entity.Property(e => e.ContactOfficePhone).HasMaxLength(50);

                entity.Property(e => e.ContactPhoneNumber).HasMaxLength(50);

                entity.Property(e => e.ContactTollFreeNumber).HasMaxLength(50);

                entity.Property(e => e.ContactWebsite).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Gstnumber)
                    .HasColumnName("GSTNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Note).HasColumnType("text");

                entity.Property(e => e.PaymentTerm).HasMaxLength(50);

                entity.Property(e => e.PhysicalCity).HasMaxLength(50);

                entity.Property(e => e.PhysicalCountry).HasMaxLength(50);

                entity.Property(e => e.PhysicalName).HasMaxLength(50);

                entity.Property(e => e.PhysicalPostalCode).HasMaxLength(50);

                entity.Property(e => e.PhysicalState).HasMaxLength(50);

                entity.Property(e => e.PhysicalStreetAddress).HasMaxLength(50);

                entity.Property(e => e.PhysicalSuburb).HasMaxLength(50);

                entity.Property(e => e.PostalAddress).HasMaxLength(50);

                entity.Property(e => e.PostalCity).HasMaxLength(50);

                entity.Property(e => e.PostalCode).HasMaxLength(50);

                entity.Property(e => e.PostalCountry).HasMaxLength(50);

                entity.Property(e => e.PostalName).HasMaxLength(50);

                entity.Property(e => e.PostalState).HasMaxLength(50);

                entity.Property(e => e.PostalSuburb).HasMaxLength(50);

                entity.Property(e => e.SUserId).HasColumnName("S_UserId");

                entity.Property(e => e.Sid)
                    .IsRequired()
                    .HasColumnName("SId")
                    .HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.TaxRate).HasMaxLength(50);
            });

            modelBuilder.Entity<SUser>(entity =>
            {
                entity.ToTable("S_User");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Passport).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Profile).HasMaxLength(200);

                entity.Property(e => e.Salt).HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.WorkTime).HasMaxLength(50);

                entity.HasOne(d => d.Setting)
                    .WithMany(p => p.SUser)
                    .HasForeignKey(d => d.SettingId)
                    .HasConstraintName("FK_S_User_Setting");
            });

            modelBuilder.Entity<SUserLink>(entity =>
            {
                entity.ToTable("S_UserLink");

                entity.Property(e => e.SLinkId).HasColumnName("S_LinkId");

                entity.Property(e => e.SUserId).HasColumnName("S_UserId");

                entity.HasOne(d => d.SLink)
                    .WithMany(p => p.SUserLink)
                    .HasForeignKey(d => d.SLinkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_S_UserLink_S_Link");

                entity.HasOne(d => d.SUser)
                    .WithMany(p => p.SUserLink)
                    .HasForeignKey(d => d.SUserId)
                    .HasConstraintName("FK_S_UserLink_S_User");
            });

            modelBuilder.Entity<SUserLog>(entity =>
            {
                entity.ToTable("S_UserLog");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Operation).HasMaxLength(500);

                entity.Property(e => e.Section).HasMaxLength(50);

                entity.Property(e => e.SuserId).HasColumnName("SUser_id");

                entity.HasOne(d => d.Suser)
                    .WithMany(p => p.SUserLog)
                    .HasForeignKey(d => d.SuserId)
                    .HasConstraintName("FK_S_UserLog_S_User");
            });

            modelBuilder.Entity<SystemEmailSetting>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Body).IsRequired();

                entity.Property(e => e.Sender)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Sid)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Setting)
                    .WithMany(p => p.Warehouse)
                    .HasForeignKey(d => d.SettingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Warehouse_Setting");
            });

            modelBuilder.Entity<WarehouseAdjustment>(entity =>
            {
                entity.Property(e => e.OperationDate).HasColumnType("datetime");

                entity.Property(e => e.SUserId).HasColumnName("S_UserId");

                entity.HasOne(d => d.SUser)
                    .WithMany(p => p.WarehouseAdjustment)
                    .HasForeignKey(d => d.SUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WarehouseAdjustment_Product");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.WarehouseAdjustment)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WarehouseAdjustment_Warehouse");
            });

            modelBuilder.Entity<WarehouseInitialization>(entity =>
            {
                entity.Property(e => e.OperationDate).HasColumnType("datetime");

                entity.Property(e => e.SUserId).HasColumnName("S_UserId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.WarehouseInitialization)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WarehouseInitialization_Product");

                entity.HasOne(d => d.SUser)
                    .WithMany(p => p.WarehouseInitialization)
                    .HasForeignKey(d => d.SUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WarehouseInitialization_S_User");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.WarehouseInitialization)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WarehouseInitialization_Warehouse");
            });

            modelBuilder.Entity<WarehousePrductRecord>(entity =>
            {
                entity.Property(e => e.Operation)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SUserId).HasColumnName("S_UserId");

                entity.HasOne(d => d.WarehouseProduct)
                    .WithMany(p => p.WarehousePrductRecord)
                    .HasForeignKey(d => d.WarehouseProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WarehousePrductRecord_WarehouseProduct");
            });

            modelBuilder.Entity<WarehouseProduct>(entity =>
            {
                entity.Property(e => e.High).HasDefaultValueSql("((10))");

                entity.Property(e => e.Low).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.WarehouseProduct)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WarehouseProduct_Product");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.WarehouseProduct)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WarehouseProduct_Warehouse");
            });

            modelBuilder.Entity<WarehouseTransfer>(entity =>
            {
                entity.Property(e => e.SUserId).HasColumnName("S_UserId");

                entity.HasOne(d => d.From)
                    .WithMany(p => p.WarehouseTransferFrom)
                    .HasForeignKey(d => d.FromId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WarehouseTransfer_Warehouse");

                entity.HasOne(d => d.SUser)
                    .WithMany(p => p.WarehouseTransfer)
                    .HasForeignKey(d => d.SUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WarehouseTransfer_S_User");

                entity.HasOne(d => d.To)
                    .WithMany(p => p.WarehouseTransferTo)
                    .HasForeignKey(d => d.ToId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WarehouseTransfer_Warehouse1");
            });

            modelBuilder.Entity<WarehouseTransferDetail>(entity =>
            {
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.WarehouseTransferDetail)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WarehouseTransferDetail_Product");

                entity.HasOne(d => d.WarehouseTransfer)
                    .WithMany(p => p.WarehouseTransferDetail)
                    .HasForeignKey(d => d.WarehouseTransferId)
                    .HasConstraintName("FK_WarehouseTransferDetail_WarehouseTransfer");
            });
        }
    }
}
