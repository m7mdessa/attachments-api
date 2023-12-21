using Domain.Entites;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure
{
    public partial class AttachmentsContext : DbContext
    {
        public AttachmentsContext()
        {
        }

        public AttachmentsContext(DbContextOptions<AttachmentsContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Attachment> Attachments { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=test;Username=postgres;Password=mohammad1999;Include Error Detail=true");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.HasDefaultSchema("Attachments");
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new AttachmentConfiguration());



        }

    }

}
