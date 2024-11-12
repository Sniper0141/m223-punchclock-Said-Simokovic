using Microsoft.EntityFrameworkCore;

namespace M223PunchclockDotnet.Model {

    public class DatabaseContext : DbContext 
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options){

        }
        
        public virtual DbSet<Entry> Entries { get; set;}
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Entry>().ToTable("Entry");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Tag>().ToTable("Tag");
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseNpgsql("Host=db;Database=postgres;Username=postgres;Password=postgres");
    }
}