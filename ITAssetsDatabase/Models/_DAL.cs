using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace ITAssetsDatabase.Models
{


    public class ITAssetsDatabaseDBContext : DbContext
    {  
       

        // KnowledgeBase
        
        public DbSet<Hostname> Hostnames { get; set; }
        public DbSet<Hostname_temp> Hostname_temp { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ADLogon> ADLogons { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleCategory> ArticleCategorys { get; set; }
        public DbSet<ArticleType> ArticleTypes { get; set; }
        public DbSet<Upload> Uploads { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }
        public DbSet<UploadedFileKB> UploadedFilesKB { get; set; }

        // Assets Register
        
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetAssigned> AssetAssigned { get; set; }
        public DbSet<AssetStatus> AssetStatus { get; set; }
        public DbSet<AssetAssignee> AssetAssignees { get; set; }
        public DbSet<Build> Builds { get; set; } 
        public DbSet<Device> Devices { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AssetAction> AssetActions { get; set; }
        public DbSet<AssetSignoff> AssetSignoffs { get; set; }

        public DbSet<Staff> Staff { get; set; }
        public DbSet<BusinessArea> BusinessAreas { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductMake> ProductMakes { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<SupplierContact> SupplierContacts { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

         }
    }
 


}
 