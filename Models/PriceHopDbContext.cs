using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PriceHop.Models;

public partial class PriceHopDbContext : DbContext
{
    public PriceHopDbContext()
    {
    }

    public PriceHopDbContext(DbContextOptions<PriceHopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<NW_Category> Categories { get; set; }

    public virtual DbSet<PNS_Category> Categories1 { get; set; }

    public virtual DbSet<NW_CategoryTree> CategoryTrees { get; set; }

    public virtual DbSet<PNS_CategoryTree> CategoryTrees1 { get; set; }

    public virtual DbSet<NW_Facet> Facets { get; set; }

    public virtual DbSet<PNS_Facet> Facets1 { get; set; }

    public virtual DbSet<NW_MarketingInitiative> MarketingInitiatives { get; set; }

    public virtual DbSet<PNS_MarketingInitiative> MarketingInitiatives1 { get; set; }

    public virtual DbSet<NW_Pricing> Pricings { get; set; }

    public virtual DbSet<PNS_Pricing> Pricings1 { get; set; }

    public virtual DbSet<NW_Product> Products { get; set; }

    public virtual DbSet<PNS_Product> Products1 { get; set; }

    public virtual DbSet<NW_Promotion> Promotions { get; set; }

    public virtual DbSet<PNS_Promotion> Promotions1 { get; set; }

    public virtual DbSet<NW_StagingProductsRaw> StagingProductsRaws { get; set; }

    public virtual DbSet<PNS_StagingProductsRaw> StagingProductsRaws1 { get; set; }

    public virtual DbSet<NW_Store> NWStores { get; set; }

    public virtual DbSet<PNS_Store> PNSStores { get; set; }

    public virtual DbSet<WW_Store> WWStores { get; set; }

    public virtual DbSet<NW_VariableWeight> VariableWeights { get; set; }

    public virtual DbSet<PNS_VariableWeight> VariableWeights1 { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:pricehop.database.windows.net,1433;Initial Catalog=PriceHop_db;Persist Security Info=False;User ID=CloudSA029d6328;Password=165Tancreds!@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NW_Category>(entity =>
        {
            entity.HasKey(e => new { e.CategoryId, e.StoreId }).HasName("PK_NewWorld_Categories");

            entity.ToTable("Categories", "NewWorld");

            entity.HasIndex(e => new { e.CategoryName, e.StoreId }, "UX_NewWorld_Categories_NamePerStore").IsUnique();

            entity.Property(e => e.CategoryId)
                .ValueGeneratedOnAdd()
                .HasColumnName("CategoryID");
            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.CategoryName).HasMaxLength(255);
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.ParentCategoryId).HasColumnName("ParentCategoryID");

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.InverseCategoryNavigation)
                .HasForeignKey(d => new { d.ParentCategoryId, d.StoreId })
                .HasConstraintName("FK_NewWorld_Categories_Parent");
        });

        modelBuilder.Entity<PNS_Category>(entity =>
        {
            entity.HasKey(e => new { e.CategoryId, e.StoreId }).HasName("PK_PaknSave_Categories");

            entity.ToTable("Categories", "PaknSave");

            entity.HasIndex(e => new { e.CategoryName, e.StoreId }, "UX_PaknSave_Categories_NamePerStore").IsUnique();

            entity.Property(e => e.CategoryId)
                .ValueGeneratedOnAdd()
                .HasColumnName("CategoryID");
            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.CategoryName).HasMaxLength(255);
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.ParentCategoryId).HasColumnName("ParentCategoryID");

            entity.HasOne(d => d.Category1Navigation).WithMany(p => p.InverseCategory1Navigation)
                .HasForeignKey(d => new { d.ParentCategoryId, d.StoreId })
                .HasConstraintName("FK_PaknSave_Categories_Parent");
        });

        modelBuilder.Entity<NW_CategoryTree>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.Level0, e.Level1, e.Level2 });

            entity.ToTable("CategoryTrees", "NewWorld");

            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("ProductID");
            entity.Property(e => e.Level0).HasMaxLength(100);
            entity.Property(e => e.Level1)
                .HasMaxLength(100)
                .HasDefaultValue("");
            entity.Property(e => e.Level2)
                .HasMaxLength(100)
                .HasDefaultValue("");
            entity.Property(e => e.AppBadgeUrl).HasMaxLength(500);
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.WebBadgeUrl).HasMaxLength(500);

            entity.HasOne(d => d.Product).WithMany(p => p.CategoryTrees)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CatTree_Products");
        });

        modelBuilder.Entity<PNS_CategoryTree>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.Level0, e.Level1, e.Level2 });

            entity.ToTable("CategoryTrees", "PaknSave");

            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("ProductID");
            entity.Property(e => e.Level0).HasMaxLength(100);
            entity.Property(e => e.Level1)
                .HasMaxLength(100)
                .HasDefaultValue("");
            entity.Property(e => e.Level2)
                .HasMaxLength(100)
                .HasDefaultValue("");
            entity.Property(e => e.AppBadgeUrl).HasMaxLength(500);
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.WebBadgeUrl).HasMaxLength(500);

            entity.HasOne(d => d.Product).WithMany(p => p.CategoryTree1s)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CatTree_Products");
        });

        modelBuilder.Entity<NW_Facet>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.ItemCode });

            entity.ToTable("Facets", "NewWorld");

            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("ProductID");
            entity.Property(e => e.ItemCode).HasMaxLength(50);
            entity.Property(e => e.ItemDesc).HasMaxLength(200);
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Product).WithMany(p => p.Facets)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Facets_Products");
        });

        modelBuilder.Entity<PNS_Facet>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.ItemCode });

            entity.ToTable("Facets", "PaknSave");

            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("ProductID");
            entity.Property(e => e.ItemCode).HasMaxLength(50);
            entity.Property(e => e.ItemDesc).HasMaxLength(200);
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Product).WithMany(p => p.Facet1s)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Facets_Products");
        });

        modelBuilder.Entity<NW_MarketingInitiative>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Marketin__B40CC6ED0A83A8BC");

            entity.ToTable("MarketingInitiatives", "NewWorld");

            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("ProductID");
            entity.Property(e => e.InitiativeCode).HasMaxLength(50);
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.ThemeCode).HasMaxLength(50);

            entity.HasOne(d => d.Product).WithOne(p => p.MarketingInitiative)
                .HasForeignKey<NW_MarketingInitiative>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MktInit_Products");
        });

        modelBuilder.Entity<PNS_MarketingInitiative>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Marketin__B40CC6EDEDCA180D");

            entity.ToTable("MarketingInitiatives", "PaknSave");

            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("ProductID");
            entity.Property(e => e.InitiativeCode).HasMaxLength(50);
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.ThemeCode).HasMaxLength(50);

            entity.HasOne(d => d.Product).WithOne(p => p.MarketingInitiative1)
                .HasForeignKey<PNS_MarketingInitiative>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MktInit_Products");
        });

        modelBuilder.Entity<NW_Pricing>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Pricing__B40CC6ED4F654648");

            entity.ToTable("Pricing", "NewWorld");

            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("ProductID");
            entity.Property(e => e.CompMeasureDescription).HasMaxLength(50);
            entity.Property(e => e.CompUnitQuantityUoM).HasMaxLength(20);
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.PromoId)
                .HasMaxLength(50)
                .HasColumnName("PromoID");

            entity.HasOne(d => d.Product).WithOne(p => p.Pricing)
                .HasForeignKey<NW_Pricing>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pricing_Products");
        });

        modelBuilder.Entity<PNS_Pricing>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Pricing__B40CC6EDE5C78587");

            entity.ToTable("Pricing", "PaknSave");

            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("ProductID");
            entity.Property(e => e.CompMeasureDescription).HasMaxLength(50);
            entity.Property(e => e.CompUnitQuantityUoM).HasMaxLength(20);
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.PromoId)
                .HasMaxLength(50)
                .HasColumnName("PromoID");

            entity.HasOne(d => d.Product).WithOne(p => p.Pricing1)
                .HasForeignKey<PNS_Pricing>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pricing_Products");
        });

        modelBuilder.Entity<NW_Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6EDAE8C2351");

            entity.ToTable("Products", "NewWorld");

            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("ProductID");
            entity.Property(e => e.Brand).HasMaxLength(200);
            entity.Property(e => e.DisplayName).HasMaxLength(100);
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.OriginStatement).HasMaxLength(500);
            entity.Property(e => e.SaleType).HasMaxLength(20);
        });

        modelBuilder.Entity<PNS_Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6EDEFACABFA");

            entity.ToTable("Products", "PaknSave");

            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("ProductID");
            entity.Property(e => e.Brand).HasMaxLength(200);
            entity.Property(e => e.DisplayName).HasMaxLength(100);
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.OriginStatement).HasMaxLength(500);
            entity.Property(e => e.SaleType).HasMaxLength(20);
        });

        modelBuilder.Entity<NW_Promotion>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.PromoId });

            entity.ToTable("Promotions", "NewWorld");

            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("ProductID");
            entity.Property(e => e.PromoId)
                .HasMaxLength(50)
                .HasColumnName("PromoID");
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.RewardType).HasMaxLength(50);

            entity.HasOne(d => d.Product).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Promos_Products");
        });

        modelBuilder.Entity<PNS_Promotion>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.PromoId });

            entity.ToTable("Promotions", "PaknSave");

            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("ProductID");
            entity.Property(e => e.PromoId)
                .HasMaxLength(50)
                .HasColumnName("PromoID");
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.RewardType).HasMaxLength(50);

            entity.HasOne(d => d.Product).WithMany(p => p.Promotion1s)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Promos_Products");
        });

        modelBuilder.Entity<NW_StagingProductsRaw>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Staging_ProductsRaw", "NewWorld");

            entity.Property(e => e.RetrievedAt).HasDefaultValueSql("(sysutcdatetime())");
        });

        modelBuilder.Entity<PNS_StagingProductsRaw>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Staging_ProductsRaw", "PaknSave");

            entity.Property(e => e.RetrievedAt).HasDefaultValueSql("(sysutcdatetime())");
        });

        modelBuilder.Entity<NW_Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Stores__3B82F0E1D34854CF");

            entity.ToTable("Stores", "NewWorld");

            entity.Property(e => e.StoreId)
                .ValueGeneratedNever()
                .HasColumnName("StoreID");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Banner).HasMaxLength(10);
            entity.Property(e => e.DefaultCollectType).HasMaxLength(50);
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.LiquorLicenseUrl).HasMaxLength(500);
            entity.Property(e => e.LocalPhone).HasMaxLength(50);
            entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.PhysicalAdditionalCity).HasMaxLength(100);
            entity.Property(e => e.PhysicalCityName).HasMaxLength(100);
            entity.Property(e => e.PhysicalCountryCode).HasMaxLength(10);
            entity.Property(e => e.PhysicalCountryName).HasMaxLength(100);
            entity.Property(e => e.PhysicalDistrictName).HasMaxLength(100);
            entity.Property(e => e.PhysicalPostalCode).HasMaxLength(20);
            entity.Property(e => e.PhysicalRegionName).HasMaxLength(100);
            entity.Property(e => e.PhysicalStoreCode).HasMaxLength(50);
            entity.Property(e => e.PhysicalStreetName).HasMaxLength(200);
            entity.Property(e => e.Region).HasMaxLength(50);
            entity.Property(e => e.SalesOrgId).HasMaxLength(50);
            entity.Property(e => e.StoreName).HasMaxLength(200);
        });

        modelBuilder.Entity<PNS_Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Stores__3B82F0E17F0A0F20");

            entity.ToTable("Stores", "PaknSave");

            entity.Property(e => e.StoreId)
                .ValueGeneratedNever()
                .HasColumnName("StoreID");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Banner).HasMaxLength(10);
            entity.Property(e => e.DefaultCollectType).HasMaxLength(50);
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.LiquorLicenseUrl).HasMaxLength(500);
            entity.Property(e => e.LocalPhone).HasMaxLength(50);
            entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.PhysicalAdditionalCity).HasMaxLength(100);
            entity.Property(e => e.PhysicalCityName).HasMaxLength(100);
            entity.Property(e => e.PhysicalCountryCode).HasMaxLength(10);
            entity.Property(e => e.PhysicalCountryName).HasMaxLength(100);
            entity.Property(e => e.PhysicalDistrictName).HasMaxLength(100);
            entity.Property(e => e.PhysicalPostalCode).HasMaxLength(20);
            entity.Property(e => e.PhysicalRegionName).HasMaxLength(100);
            entity.Property(e => e.PhysicalStoreCode).HasMaxLength(50);
            entity.Property(e => e.PhysicalStreetName).HasMaxLength(200);
            entity.Property(e => e.Region).HasMaxLength(50);
            entity.Property(e => e.SalesOrgId).HasMaxLength(50);
            entity.Property(e => e.StoreName).HasMaxLength(200);
        });

        modelBuilder.Entity<WW_Store>(entity =>
        {
            entity.HasKey(e => e.SiteId).HasName("PK__Stores__B9DCB90363FB279D");

            entity.ToTable("Stores", "Woolworths");

            entity.Property(e => e.SiteId)
                .ValueGeneratedNever()
                .HasColumnName("SiteID");
            entity.Property(e => e.AddressLine1).HasMaxLength(255);
            entity.Property(e => e.AddressLine2).HasMaxLength(255);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.Division).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Extra1).HasMaxLength(50);
            entity.Property(e => e.Extra10).HasMaxLength(50);
            entity.Property(e => e.Extra11).HasMaxLength(50);
            entity.Property(e => e.Extra12).HasMaxLength(50);
            entity.Property(e => e.Extra13).HasMaxLength(50);
            entity.Property(e => e.Extra14).HasMaxLength(50);
            entity.Property(e => e.Extra15).HasMaxLength(50);
            entity.Property(e => e.Extra2).HasMaxLength(50);
            entity.Property(e => e.Extra3).HasMaxLength(50);
            entity.Property(e => e.Extra4).HasMaxLength(50);
            entity.Property(e => e.Extra5).HasMaxLength(50);
            entity.Property(e => e.Extra6).HasMaxLength(50);
            entity.Property(e => e.Extra7).HasMaxLength(50);
            entity.Property(e => e.Extra8).HasMaxLength(50);
            entity.Property(e => e.Extra9).HasMaxLength(50);
            entity.Property(e => e.GeoLevel).HasMaxLength(50);
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Postcode).HasMaxLength(10);
            entity.Property(e => e.State).HasMaxLength(10);
            entity.Property(e => e.Suburb).HasMaxLength(100);
        });

        modelBuilder.Entity<NW_VariableWeight>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Variable__B40CC6ED2F716E1B");

            entity.ToTable("VariableWeight", "NewWorld");

            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("ProductID");
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.StepUoM).HasMaxLength(20);
            entity.Property(e => e.UoM).HasMaxLength(20);

            entity.HasOne(d => d.Product).WithOne(p => p.VariableWeight)
                .HasForeignKey<NW_VariableWeight>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VarWgt_Products");
        });

        modelBuilder.Entity<PNS_VariableWeight>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Variable__B40CC6EDF7263A86");

            entity.ToTable("VariableWeight", "PaknSave");

            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("ProductID");
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.StepUoM).HasMaxLength(20);
            entity.Property(e => e.UoM).HasMaxLength(20);

            entity.HasOne(d => d.Product).WithOne(p => p.VariableWeight1)
                .HasForeignKey<PNS_VariableWeight>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VarWgt_Products");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
